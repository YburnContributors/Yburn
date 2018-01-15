/**************************************************************************************************
 * The class QQDataDoc is a tool class to collected and manage data on the QQ states as calculated
 * by e.g. QQBoundState. You may archive data into a file and extract it if need for, e.g., the
 * fireball simulations.
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yburn.FormatUtil;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum QQDataColumn
	{
		N,
		L,
		ColorState,
		PotentialType,
		Temperature,
		DebyeMass,
		DisplacementRMS,
		SoftScale,
		UltraSoftScale,
		BoundMass,
		Energy,
		GammaDamp,
		GammaDiss,
		GammaTot,
		None
	}

	/********************************************************************************************
	 * Structs
	 ********************************************************************************************/

	public struct QQDataSet
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQDataSet(
			int n,
			int l,
			ColorState colorState,
			PotentialType potentialType,
			double temperature,
			double debyeMass,
			double displacementRMS,
			double softScale,
			double ultraSoftScale,
			double boundMass,
			double energy,
			double gammaDamp,
			double gammaDiss,
			double gammaTot
			)
		{
			N = n;
			L = l;
			ColorState = colorState;
			PotentialType = potentialType;
			Temperature = temperature;
			DebyeMass = debyeMass;
			DisplacementRMS = displacementRMS;
			SoftScale = softScale;
			UltraSoftScale = ultraSoftScale;
			BoundMass = boundMass;
			Energy = energy;
			GammaDamp = gammaDamp;
			GammaDiss = gammaDiss;
			GammaTot = gammaTot;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly int N;

		public readonly int L;

		public readonly ColorState ColorState;

		public readonly PotentialType PotentialType;

		public readonly double Temperature;

		public readonly double DebyeMass;

		public readonly double DisplacementRMS;

		public readonly double SoftScale;

		public readonly double UltraSoftScale;

		public readonly double BoundMass;

		public readonly double Energy;

		public readonly double GammaDamp;

		public readonly double GammaDiss;

		public readonly double GammaTot;

		public double GetData(
			QQDataColumn dataColumn
			)
		{
			switch(dataColumn)
			{
				case QQDataColumn.N:
					return N;

				case QQDataColumn.L:
					return L;

				case QQDataColumn.ColorState:
					return (int)ColorState;

				case QQDataColumn.PotentialType:
					return (int)PotentialType;

				case QQDataColumn.Temperature:
					return Temperature;

				case QQDataColumn.DebyeMass:
					return DebyeMass;

				case QQDataColumn.DisplacementRMS:
					return DisplacementRMS;

				case QQDataColumn.SoftScale:
					return SoftScale;

				case QQDataColumn.UltraSoftScale:
					return UltraSoftScale;

				case QQDataColumn.BoundMass:
					return BoundMass;

				case QQDataColumn.Energy:
					return Energy;

				case QQDataColumn.GammaDamp:
					return GammaDamp;

				case QQDataColumn.GammaDiss:
					return GammaDiss;

				case QQDataColumn.GammaTot:
					return GammaTot;

				case QQDataColumn.None:
					return 0;

				default:
					throw new Exception("Invalid QQDataColumn.");
			}
		}

		public double GetGamma(
			DecayWidthType type
			)
		{
			switch(type)
			{
				case DecayWidthType.None:
					return 0;

				case DecayWidthType.GammaDamp:
					return GammaDamp;

				case DecayWidthType.GammaDiss:
					return GammaDiss;

				case DecayWidthType.GammaTot:
					return GammaTot;

				default:
					throw new Exception("Invalid DecayWidthType.");
			}
		}
	}

	public static class QQDataDoc
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		static QQDataDoc()
		{
			YburnFormat.UseYburnFormat();
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void CreateNewDataDoc(
			string pathFile,
			double accuracyAlpha, // accuracy in the running coupling
			double accuracyWave, // accuracy in the wave function
			double aggressivenessAlpha, // determines how strong Alpha_S is changed between each step
			double maxEnergy, // upper energy limit in MeV
			int eSteps, // number of energy steps
			double quarkMass, // in MeV
			double rOut, // maximum radius in fm
			int stepNumber,
			double sigma, // string coupling in fm^-2
			double tchem, // chemical freeze-out temperature in MeV
			double tcrit // critical temperature in MeV
			)
		{
			File.WriteAllText(pathFile, DataHeader(accuracyAlpha, accuracyWave, aggressivenessAlpha,
				maxEnergy, eSteps, quarkMass, rOut, stepNumber, sigma, tchem, tcrit));
		}

		public static void Write(
			string pathFile,
			QQDataSet dataSet
			)
		{
			// read whole data file
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));
			int lineIndex = FindLineIndex(allLines, dataSet.N, dataSet.L, dataSet.ColorState,
				dataSet.PotentialType, dataSet.Temperature, out bool lineFound);

			if(lineFound)
			{
				allLines[lineIndex] = MakeDataLine(dataSet);
			}
			else
			{
				allLines.Insert(lineIndex, MakeDataLine(dataSet));
			}

			File.WriteAllLines(pathFile, allLines);
		}

		public static string ReadAllText(
			string pathFile
			)
		{
			return File.ReadAllText(pathFile);
		}

		public static string MakeDataLine(
			QQDataSet dataSet
			)
		{
			return string.Format(
				" " + ColumnFormat,
				dataSet.N.ToString(),
				dataSet.L.ToString(),
				dataSet.ColorState.ToString(),
				dataSet.PotentialType.ToString(),
				dataSet.Temperature.ToString("G6"),
				dataSet.DebyeMass.ToString("G6"),
				dataSet.DisplacementRMS.ToString("G6"),
				dataSet.SoftScale.ToString("G6"),
				dataSet.UltraSoftScale.ToString("G6"),
				dataSet.BoundMass.ToString("G6"),
				dataSet.Energy.ToString("G6"),
				dataSet.GammaDamp.ToString("G6"),
				dataSet.GammaDiss.ToString("G6"),
				dataSet.GammaTot.ToString("G6"));
		}

		public static List<QQDataSet> GetDataSets(
			string pathFile,
			int n,
			int l,
			ColorState colorState,
			List<PotentialType> potentialTypes
			)
		{
			return GetDataSets(
				pathFile,
				n,
				l,
				colorState,
				potentialTypes,
				0,
				double.PositiveInfinity);
		}

		public static QQDataSet GetDataSet(
			string pathFile,
			int n,
			int l,
			ColorState colorState,
			List<PotentialType> potentialTypes,
			double temperature
			)
		{
			List<QQDataSet> dataSetList = GetDataSets(
				pathFile,
				n,
				l,
				colorState,
				potentialTypes,
				temperature,
				temperature);

			if(dataSetList.Count == 0)
			{
				throw new Exception("No archived data could be found.");
			}
			return dataSetList[0];
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly string ColumnFormat = "{0,4}{1,4}{2,12}{3,18}{4,14}{5,12}{6,12}{7,12}{8,12}{9,12}{10,12}{11,14}{12,14}{13,14}";

		private static readonly string CommentarySign = "#";

		private static readonly string Separator = string.Format(CommentarySign + ColumnFormat,
			"", "", "", "", "", "", "", "", "", "", "", "", "", "").Replace(' ', '-');

		// Extract a list of values from the QQ-data file with specified N, L, color state and
		// potential type within a given temperature region.
		// If no values are found an empty list is returned.
		private static List<QQDataSet> GetDataSets(
			string pathFile,
			int n,
			int l,
			ColorState colorState,
			List<PotentialType> potentialTypes,
			double minTemperature,
			double maxTemperature
			)
		{
			List<QQDataSet> dataSetList = new List<QQDataSet>();

			// read whole data file
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));

			// go through the data file and find the wanted data
			foreach(string currentLine in allLines)
			{
				// skip commentary
				if(IsCommentaryLine(currentLine))
				{
					continue;
				}

				QQDataSet dataSet = ExtractDataSet(currentLine);

				if(!IsDataSetValid(dataSet, n, l, colorState, potentialTypes, minTemperature, maxTemperature))
				{
					continue;
				}

				// add the data to the list and check the next line
				dataSetList.Add(dataSet);
			}

			return dataSetList;
		}

		private static QQDataSet ExtractDataSet(
			string line
			)
		{
			string[] values = SplitLineIntoValues(line);

			return new QQDataSet(
				n: int.Parse(values[(int)QQDataColumn.N]),
				l: int.Parse(values[(int)QQDataColumn.L]),
				colorState: (ColorState)Enum.Parse(
					typeof(ColorState), values[(int)QQDataColumn.ColorState]),
				potentialType: (PotentialType)Enum.Parse(
					typeof(PotentialType), values[(int)QQDataColumn.PotentialType]),
				temperature: double.Parse(values[(int)QQDataColumn.Temperature]),
				debyeMass: double.Parse(values[(int)QQDataColumn.DebyeMass]),
				displacementRMS: double.Parse(values[(int)QQDataColumn.DisplacementRMS]),
				softScale: double.Parse(values[(int)QQDataColumn.SoftScale]),
				ultraSoftScale: double.Parse(values[(int)QQDataColumn.UltraSoftScale]),
				boundMass: double.Parse(values[(int)QQDataColumn.BoundMass]),
				energy: double.Parse(values[(int)QQDataColumn.Energy]),
				gammaDamp: double.Parse(values[(int)QQDataColumn.GammaDamp]),
				gammaDiss: double.Parse(values[(int)QQDataColumn.GammaDiss]),
				gammaTot: double.Parse(values[(int)QQDataColumn.GammaTot]));
		}

		private static bool IsDataSetValid(
			QQDataSet dataSet,
			int n,
			int l,
			ColorState colorState,
			List<PotentialType> potentialTypes,
			double minTemperature,
			double maxTemperature
			)
		{
			bool isValid = (dataSet.N == n)
				&& (dataSet.L == l)
				&& (dataSet.ColorState == colorState)
				&& potentialTypes.Contains(dataSet.PotentialType)
				&& (dataSet.Temperature >= minTemperature)
				&& (dataSet.Temperature <= maxTemperature);

			return isValid;
		}

		private static string DataHeader(
			double accuracyAlpha, // accuracy in the running coupling
			double accuracyWave, // accuracy in the wave function
			double aggressivenessAlpha, // determines how strong Alpha_S is changed between each step
			double maxEnergy, // upper energy limit in MeV
			int eSteps, // number of energy steps
			double quarkMass, // in MeV
			double rOut, // maximum radius in fm
			int stepNumber,
			double sigma, // string coupling in fm^-2
			double tchem, // chemical freeze-out temperature in MeV
			double tcrit // critical temperature in MeV
			)
		{
			StringBuilder oStringBuilder = new StringBuilder();

			oStringBuilder.AppendLine(Separator);
			oStringBuilder.AppendLine(CommentarySign + "This is a QQDataDoc-file. Commentary lines may inserted using a leading \"" + CommentarySign + "\".");
			oStringBuilder.AppendLine(CommentarySign + "The data has been calculated using the following parameters (hopefully) consistently:");
			oStringBuilder.AppendLine(CommentarySign);
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "AccuracyAlpha", accuracyAlpha.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "AccuracyWaveFunction", accuracyWave.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "AggressivenessAlpha", aggressivenessAlpha.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "EnergySteps", eSteps);
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "MaxEnergy (MeV)", maxEnergy.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "MaxRadius (fm)", rOut.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "QuarkMass (MeV)", quarkMass.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "Sigma (MeV²)", sigma.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "StepNumber", stepNumber);
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "Tchem (MeV)", tchem.ToString("G4"));
			oStringBuilder.AppendFormat(CommentarySign + "{0,22}    {1,-12}" + Environment.NewLine, "Tcrit (MeV)", tcrit.ToString("G4"));
			oStringBuilder.AppendLine(Separator);
			oStringBuilder.AppendFormat(CommentarySign + ColumnFormat,
				"N", "L", "ColorState", "PotentialType", "Temperature", "DebyeMass", "√<r²>", "SoftScale", "US_Scale", "BoundMass", "Energy", "GammaDamp", "GammaDiss", "GammaTot");
			oStringBuilder.AppendLine();
			oStringBuilder.AppendFormat(CommentarySign + ColumnFormat,
				"", "", "", "", "(MeV)", "(MeV)", "(fm)", "(Mev)", "(Mev)", "(MeV)", "(MeV)", "(MeV)", "(MeV)", "(MeV)");
			oStringBuilder.AppendLine();
			oStringBuilder.AppendLine(Separator);

			return oStringBuilder.ToString();
		}

		private static int FindLineIndex(
			List<string> allLines,
			int n,
			int l,
			ColorState colorState,
			PotentialType potentialType,
			double temperature,
			out bool lineFound
			)
		{
			lineFound = false;

			int lineIndex;
			for(lineIndex = 0; lineIndex < allLines.Count; lineIndex++)
			{
				// skip commentary
				if(IsCommentaryLine(allLines[lineIndex]))
				{
					continue;
				}

				string[] values = SplitLineIntoValues(allLines[lineIndex]);

				int nComparison = int.Parse(values[(int)QQDataColumn.N]) - n;
				if(nComparison < 0)
				{
					continue;
				}
				else if(nComparison > 0)
				{
					// new N value - insert new line here...
					return lineIndex;
				}

				int lComparison = int.Parse(values[(int)QQDataColumn.L]) - l;
				if(lComparison < 0)
				{
					continue;
				}
				else if(lComparison > 0)
				{
					// new L value - insert new line here...
					return lineIndex;
				}

				int colorStateComparison = (int)Enum.Parse(typeof(ColorState), values[(int)QQDataColumn.ColorState]) - (int)colorState;
				if(colorStateComparison < 0)
				{
					continue;
				}
				else if(colorStateComparison > 0)
				{
					// new ColorState value - insert new line here...
					return lineIndex;
				}

				double temperatureComparison = double.Parse(values[(int)QQDataColumn.Temperature]) - temperature;
				if(temperatureComparison < 0)
				{
					continue;
				}
				if(temperatureComparison > 0)
				{
					// new temperature value - insert new line here...
					return lineIndex;
				}

				int potentialTypeComparison = (int)Enum.Parse(typeof(PotentialType), values[(int)QQDataColumn.PotentialType]) - (int)potentialType;
				if(potentialTypeComparison < 0)
				{
					continue;
				}
				else if(potentialTypeComparison > 0)
				{
					// new PotentialType value - insert new line here...
					return lineIndex;
				}

				// all values match - update this line...
				lineFound = true;
				return lineIndex;
			}

			return lineIndex;
		}

		private static string[] SplitLineIntoValues(
			string lines
			)
		{
			return lines.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
		}

		private static bool IsCommentaryLine(
			string line
			)
		{
			return line.StartsWith(CommentarySign);
		}
	}
}
