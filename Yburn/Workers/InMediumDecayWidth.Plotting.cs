using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Yburn.Fireball;
using Yburn.QQState;

namespace Yburn.Workers
{
	partial class InMediumDecayWidth
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public Process PlotInMediumDecayWidthsVersusMediumTemperature()
		{
			MediumVelocities = new List<double> { MediumVelocities[0] };
			CalculateInMediumDecayWidth();

			StringBuilder plotFile = new StringBuilder();
			AppendHeader_InMediumDecayWidthsVersusMediumTemperature(plotFile);
			plotFile.AppendLine();

			string[][] titleList = new string[DecayWidthEvaluationTypes.Count][];
			for(int i = 0; i < DecayWidthEvaluationTypes.Count; i++)
			{
				titleList[i] = BottomiumStates.ConvertAll(
					state => GetBottomiumStateGnuplotCode(state) + ", "
					+ DecayWidthEvaluationTypes[i].ToUIString()).ToArray();
			}

			AppendPlotCommands(
				plotFile,
				abscissaColumn: 1,
				firstOrdinateColumn: 3,
				titles: titleList);

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		public Process PlotInMediumDecayWidthsVersusMediumVelocity()
		{
			MediumTemperatures = new List<double> { MediumTemperatures[0] };
			CalculateInMediumDecayWidth();

			StringBuilder plotFile = new StringBuilder();
			AppendHeader_InMediumDecayWidthsVersusMediumVelocity(plotFile);
			plotFile.AppendLine();

			string[][] titleList = new string[DecayWidthEvaluationTypes.Count][];
			for(int i = 0; i < DecayWidthEvaluationTypes.Count; i++)
			{
				titleList[i] = BottomiumStates.ConvertAll(
					state => GetBottomiumStateGnuplotCode(state) + ", "
					+ DecayWidthEvaluationTypes[i].ToUIString()).ToArray();
			}

			AppendPlotCommands(
				plotFile,
				abscissaColumn: 2,
				firstOrdinateColumn: 3,
				titles: titleList);

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		public Process PlotDecayWidthEvaluatedAtDopplerShiftedTemperature()
		{
			MediumTemperatures = new List<double> { MediumTemperatures[0] };
			MediumVelocities = new List<double> { MediumVelocities[0] };

			CreateDataFile(CreateDecayWidthEvaluatedAtDopplerShiftedTemperatureDataList);
			CreateDecayWidthEvaluatedAtDopplerShiftedTemperaturePlotFile();

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string GetBottomiumStateGnuplotCode(
			BottomiumState state
			)
		{
			switch(state)
			{
				case BottomiumState.Y1S:
					return "{/Symbol U}(1S)";

				case BottomiumState.x1P:
					return "{/Symbol c}_{b}(1P)";

				case BottomiumState.Y2S:
					return "{/Symbol U}(2S)";

				case BottomiumState.x2P:
					return "{/Symbol c}_{b}(2P)";

				case BottomiumState.Y3S:
					return "{/Symbol U}(3S)";

				case BottomiumState.x3P:
					return "{/Symbol c}_{b}(3P)";

				default:
					throw new Exception("Invalid BottomiumState.");
			}
		}

		private static string GetDecayWidthTypeGnuplotCode(
			DecayWidthType type
			)
		{
			switch(type)
			{
				case DecayWidthType.GammaDamp:
					return "{/Symbol G}^{damp}_{nl}";

				case DecayWidthType.GammaDiss:
					return "{/Symbol G}^{diss}_{nl}";

				case DecayWidthType.GammaTot:
					return "{/Symbol G}^{tot}_{nl}";

				default:
					throw new Exception("Invalid DecayWidthType.");
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void SetColorsForBottomiumStates(
			StringBuilder plotFile
			)
		{
			for(int i = 8; i > BottomiumStates.Count; i--)
			{
				plotFile.AppendLine(string.Format("unset linetype {0}", i));
			}
			plotFile.AppendLine("set linetype cycle " + BottomiumStates.Count);
		}

		private void AppendHeader_InMediumDecayWidthsVersusMediumTemperature(
			StringBuilder plotFile
			)
		{
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,600");
			plotFile.AppendLine();
			plotFile.AppendLine("set key spacing 1.1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title '" + InMediumDecayWidthPlottingTitle
				+ " for medium velocity |u| = " + MediumVelocities[0].ToUIString() + " c" + "'");
			plotFile.AppendLine("set xlabel 'T (MeV)'");
			plotFile.AppendLine("set ylabel '" + GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)'");
			plotFile.AppendLine();
			SetColorsForBottomiumStates(plotFile);
		}

		private void AppendHeader_InMediumDecayWidthsVersusMediumVelocity(
			StringBuilder plotFile
			)
		{
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,600");
			plotFile.AppendLine();
			plotFile.AppendLine("set key spacing 1.1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title '" + InMediumDecayWidthPlottingTitle
				+ " for medium temperature T = " + MediumTemperatures[0].ToUIString() + " MeV" + "'");
			plotFile.AppendLine("set xlabel 'u (c)'");
			plotFile.AppendLine("set ylabel '" + GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)'");
			plotFile.AppendLine();
			SetColorsForBottomiumStates(plotFile);
		}

		private string InMediumDecayWidthPlottingTitle
		{
			get
			{
				return "In-medium decay widths " + GetDecayWidthTypeGnuplotCode(DecayWidthType);
			}
		}

		private void CreateDecayWidthEvaluatedAtDopplerShiftedTemperaturePlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'In-medium decay width "
				+ GetDecayWidthTypeGnuplotCode(DecayWidthType)
				+ " evaluated at a Doppler-shifted temperature"
				+ " for medium temperature T = " + MediumTemperatures[0].ToUIString() + " MeV"
				+ " and medium velocity |u| = " + MediumVelocities[0].ToUIString() + " c" + "'");
			plotFile.AppendLine("set xlabel 'cos({/Symbol q})'");
			plotFile.AppendLine("set ylabel '"
				+ GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)'");
			plotFile.AppendLine();
			SetColorsForBottomiumStates(plotFile);
			plotFile.AppendLine();
			plotFile.AppendLine("set style fill transparent solid 0.2");
			plotFile.AppendLine();

			string[] titleList =
				BottomiumStates.ConvertAll(state => GetBottomiumStateGnuplotCode(state)).ToArray();

			AppendPlotCommands(plotFile, titles: titleList);

			for(int i = 0; i < titleList.Length; i++)
			{
				plotFile.AppendFormat("'{0}' index 0 using 1:"
					+ "(stringcolumn({2}) eq 'Infinity' ? column({1}) : column({2}))"
					+ " with lines dashtype '-' notitle,\\",
					DataFileName, i + 2, i + 2 + titleList.Length);
				plotFile.AppendLine();
			}

			for(int i = 0; i < titleList.Length; i++)
			{
				plotFile.AppendFormat("'{0}' index 0 using 1:{1}:"
					+ "(stringcolumn({2}) eq 'Infinity' ? column({1}) : column({2}))"
					+ " with filledcurves notitle,\\",
					DataFileName, i + 2, i + 2 + titleList.Length);
				plotFile.AppendLine();
			}

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateDecayWidthEvaluatedAtDopplerShiftedTemperatureDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> cosineValues = GetLinearAbscissaList(-1, 1, NumberAveragingAngles);
			dataList.Add(cosineValues);

			List<DecayWidthAverager> averagers = new List<DecayWidthAverager>();
			foreach(BottomiumState state in BottomiumStates)
			{
				List<QQDataSet> dataSets = DecayWidthProvider.GetBoundStateDataSets(
					YburnConfigFile.QQDataPathFile,
					PotentialTypes,
					state);

				averagers.Add(new DecayWidthAverager(
					dataSets, DecayWidthType, QGPFormationTemperature, NumberAveragingAngles));
			}

			foreach(DecayWidthAverager averager in averagers)
			{
				PlotFunction function = cosine =>
					averager.GetDecayWidthEvaluatedAtDopplerShiftedTemperature(
						MediumTemperatures[0], MediumVelocities[0], cosine);

				AddPlotFunctionLists(dataList, cosineValues, function);
			}

			foreach(DecayWidthAverager averager in averagers)
			{
				PlotFunction function = cosine => averager.GetEffectiveDecayWidth(
					MediumTemperatures[0], 0, DecayWidthEvaluationType.UnshiftedTemperature);

				AddPlotFunctionLists(dataList, cosineValues, function);
			}

			return dataList;
		}
	}
}
