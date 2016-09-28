/**************************************************************************************************
 * The class QQDataDoc is a tool class to collected and manage data on the QQ states as calculated
 * by e.g. QQBoundState. You may archive data into a file and extract it if need for, e.g., the
 * fireball simulations.
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Yburn.Fireball
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum QQDataColumns
	{
		N,
		L,
		ColorState,
		PotentialType,
		Temperature,
		DebyeMass,
		RMS,
		SoftScale,
		UltraSoftScale,
		BoundMass,
		Energy,
		GammaDamp,
		GammaDiss,
		GammaTot,
		None
	}

	public static class QQDataDoc
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		static QQDataDoc()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
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
			double alphaS, // AlphaS(QuarkMass), hard scale
			double sigma, // string coupling in fm^-2
			double sigmaEff, // effective string-coupling in fm^-1
			double tchem, // chemical freeze-out temperature in MeV
			double tcrit // critical temperature in MeV
			)
		{
			File.WriteAllText(pathFile, DataHeader(accuracyAlpha, accuracyWave, aggressivenessAlpha,
				maxEnergy, eSteps, quarkMass, rOut, stepNumber, alphaS, sigma, sigmaEff, tchem, tcrit));
		}

		public static void Write(
			string pathFile,
			int n, // principal quantum number N
			int l, // orbital quantum number L
			string colorState,
			string potentialType,
			double temperature, // in MeV
			double debyeMass, // in MeV
			double rootMeanSquare, // in fm
			double softScale, // in MeV
			double ultraSoftScale, // in MeV
			double boundMass, // in MeV
			double energy, // in MeV
			double gammaDamp, // in MeV
			double gammaDiss, // in MeV
			double gammaTot // in MeV
		)
		{
			// read whole data file
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));
			bool lineFound;
			int lineIndex = FindLineIndex(allLines, n, l, colorState, potentialType,
				temperature, out lineFound);

			if(lineFound)
			{
				allLines[lineIndex] = MakeDataLine(n, l, colorState, potentialType, temperature,
					debyeMass, rootMeanSquare, softScale, ultraSoftScale, boundMass, energy, gammaDamp,
					gammaDiss, gammaTot);
			}
			else
			{
				allLines.Insert(lineIndex, MakeDataLine(n, l, colorState, potentialType,
					temperature, debyeMass, rootMeanSquare, softScale, ultraSoftScale, boundMass, energy,
					gammaDamp, gammaDiss, gammaTot));
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
			int n, // principal quantum number N
			int l, // orbital quantum number L
			string colorState,
			string potentialType,
			double temperature, // in MeV
			double debyeMass, // Debye mass in MeV
			double rootMeanSquare, // rms  in fm
			double softScale, // in MeV
			double ultraSoftScale, // in MeV
			double boundMass, // bound mass in MeV
			double energy, // in MeV, negative
			double gammaDamp, // in MeV
			double gammaDiss, // in MeV
			double gammaTot // in MeV
		)
		{
			return string.Format(
				" {0,-8}{1,-8}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}{10,-12}{11,-14}{12,-14}{13,-14}",
				n.ToString(),
				l.ToString(),
				colorState,
				potentialType,
				temperature.ToString("G6"),
				debyeMass.ToString("G6"),
				rootMeanSquare.ToString("G6"),
				softScale.ToString("G6"),
				ultraSoftScale.ToString("G6"),
				boundMass.ToString("G6"),
				energy.ToString("G6"),
				gammaDamp.ToString("G6"),
				gammaDiss.ToString("G6"),
				gammaTot.ToString("G6"));
		}

		public static double GetValue(
			string pathFile,
			QQDataColumns dataColumn,
			int n,
			int l,
			string colorState,
			string potentialType,
			double? temperature
			)
		{
			List<KeyValuePair<double, double>> valueList = GetValueList(pathFile, dataColumn, n,
				l, colorState, new string[] { potentialType }, temperature, temperature);
			if(valueList.Count == 0)
			{
				throw new Exception("No archived data could be found.");
			}
			return valueList[0].Value;
		}

		// Extract a list of values from the QQ-data file with specified N, L, color state and
		// potential type within a given temperature region. The potential types are flaggable.
		// If no values are found an empty list is returned.
		public static List<KeyValuePair<double, double>> GetValueList(
			string pathFile,
			QQDataColumns dataColumn,
			int? n,
			int? l,
			string colorState,
			string[] potentialTypes,
			double? minTemperature,
			double? maxTemperature
			)
		{
			CheckDataColumnValid(dataColumn);

			List<KeyValuePair<double, double>> temperatureValueList = new List<KeyValuePair<double, double>>();

			// read whole data file
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));

			string sN = n.HasValue ? n.ToString() : null;
			string sL = l.HasValue ? l.ToString() : null;
			string sMinTemperature = minTemperature.HasValue ?
				minTemperature.Value.ToString("G6") : null;
			string sMaxTemperature = maxTemperature.HasValue ?
				maxTemperature.Value.ToString("G6") : null;

			// go through the data file backwards and find the wanted data
			foreach(string currentLine in allLines)
			{
				// skip commentary
				if(IsCommentaryLine(currentLine))
				{
					continue;
				}

				string[] values = SplitLineIntoValues(currentLine);
				if(!LineValuesValid(values, sN, sL, colorState, sMinTemperature,
					sMaxTemperature, potentialTypes))
				{
					continue;
				}

				// add the content to the list and check the next line
				temperatureValueList.Add(new KeyValuePair<double, double>(
					double.Parse(values[(int)QQDataColumns.Temperature]),
					double.Parse(values[(int)dataColumn])));
			}

			return temperatureValueList;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private const string Separator
			= "#------------------------------------------------------------------------------------------------------------------------------------------------------------";

		private const string CommentarySign = "#";

		private static void CheckDataColumnValid(
			QQDataColumns dataColumn
			)
		{
			if(dataColumn == QQDataColumns.None ||
				dataColumn == QQDataColumns.N ||
				dataColumn == QQDataColumns.L ||
				dataColumn == QQDataColumns.ColorState ||
				dataColumn == QQDataColumns.PotentialType ||
				dataColumn == QQDataColumns.Temperature)
			{
				throw new Exception("Invalid QQDataColumn.");
			}
		}

		private static bool LineValuesValid(
			string[] lineValues,
			string n,
			string l,
			string colorState,
			string minTemperature,
			string maxTemperature,
			string[] potentialTypes
		)
		{
			bool isValid = string.Compare(lineValues[(int)QQDataColumns.N], n) == 0
				&& string.Compare(lineValues[(int)QQDataColumns.L], l) == 0
				&& string.Compare(lineValues[(int)QQDataColumns.ColorState], colorState) == 0;

			isValid &= LineContainsPotentialType(
				lineValues[(int)QQDataColumns.PotentialType], potentialTypes);

			if(!string.IsNullOrEmpty(minTemperature))
			{
				isValid &= string.Compare(
					lineValues[(int)QQDataColumns.Temperature], minTemperature) >= 0;
			}
			if(!string.IsNullOrEmpty(maxTemperature))
			{
				isValid &= string.Compare(
					lineValues[(int)QQDataColumns.Temperature], maxTemperature) <= 0;
			}
			return isValid;
		}

		private static bool LineContainsPotentialType(
			string linePotentialType,
			string[] potentialTypes
			)
		{
			foreach(string type in potentialTypes)
			{
				if(linePotentialType == type)
				{
					return true;
				}
			}

			return false;
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
			double alphaS, // AlphaS(QuarkMass), hard scale
			double sigma, // string coupling in fm^-2
			double sigmaEff, // effective string-coupling in fm^-1
			double tchem, // chemical freeze-out temperature in MeV
			double tcrit // critical temperature in MeV
			)
		{
			StringBuilder oStringBuilder = new StringBuilder();

			oStringBuilder.AppendLine(Separator);
			oStringBuilder.AppendLine("#This is a QQDataDoc-file. Commentary lines may inserted using a leading \"#\".");
			oStringBuilder.AppendLine("#The data has been calculated using the following parameters (hopefully) consistently:");
			oStringBuilder.AppendLine("#");
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "AccuracyAlpha", accuracyAlpha.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "AccuracyWave", accuracyWave.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "AggressivenessAlpha", aggressivenessAlpha.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "Emax (MeV)", maxEnergy.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "ESteps", eSteps);
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "QuarkMass (MeV)", quarkMass.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "Rout (fm)", rOut.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "StepsNumber", stepNumber);
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "AlphaS_H", alphaS.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "Sigma (MeV^2)", sigma.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "SigmaEff (MeV^2)", sigmaEff.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "Tchem (MeV)", tchem.ToString("G4"));
			oStringBuilder.AppendFormat("#{0,22}    {1,-12}\r\n", "Tcrit (MeV)", tcrit.ToString("G4"));
			oStringBuilder.AppendLine(Separator);
			oStringBuilder.AppendLine("#N       L       ColorState  Pot.Type    Temperature DebyeMass   <r^2>^1/2   SoftScale   US_Scale    BoundMass   Energy      GammaDamp   GammaDiss   GammaTot    ");
			oStringBuilder.AppendLine("#                                        (MeV)       (MeV)       (fm)        (Mev)       (Mev)       (MeV)       (MeV)       (MeV)       (MeV)       (MeV)       ");
			oStringBuilder.AppendLine(Separator);

			return oStringBuilder.ToString();
		}

		private static int FindLineIndex(
			List<string> allLines,
			int n,
			int l,
			string colorState,
			string potentialType,
			double temperature,
			out bool lineFound
			)
		{
			string sN = n.ToString();
			string sL = l.ToString();
			string sTemperature = temperature.ToString("G6");
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

				int nComparison = string.Compare(values[(int)QQDataColumns.N], sN);
				if(nComparison < 0)
				{
					continue;
				}
				else if(nComparison > 0)
				{
					// new N value - insert new line here...
					return lineIndex;
				}

				int lComparison = string.Compare(values[(int)QQDataColumns.L], sL);
				if(lComparison < 0)
				{
					continue;
				}
				else if(lComparison > 0)
				{
					// new L value - insert new line here...
					return lineIndex;
				}

				int iColorStateComparison = string.Compare(values[(int)QQDataColumns.ColorState], colorState);
				if(iColorStateComparison < 0)
				{
					continue;
				}
				else if(iColorStateComparison > 0)
				{
					// new ColorState value - insert new line here...
					return lineIndex;
				}

				int iPotentialTypeComparison = string.Compare(values[(int)QQDataColumns.PotentialType], potentialType);
				if(iPotentialTypeComparison < 0)
				{
					continue;
				}
				else if(iPotentialTypeComparison > 0)
				{
					// new PotentialType value - insert new line here...
					return lineIndex;
				}

				double dTemperatureComparison = double.Parse(values[(int)QQDataColumns.Temperature]) - temperature;
				if(dTemperatureComparison < 0)
				{
					continue;
				}
				if(dTemperatureComparison > 0)
				{
					// new temperature value - insert new line here...
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

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/
	}
}