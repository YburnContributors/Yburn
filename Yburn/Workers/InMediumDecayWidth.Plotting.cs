﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Yburn.Fireball;
using Yburn.FormatUtil;
using Yburn.QQState;

namespace Yburn.Workers
{
	partial class InMediumDecayWidth
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public Process PlotDecayWidthsFromQQDataFile()
		{
			List<DataListCreator> creators = new List<DataListCreator>();
			foreach(BottomiumState state in BottomiumStates)
			{
				creators.Add(() => CreateDecayWidthsFromQQDataFileDataList(state));
			}
			creators.Add(CreateDecayWidthsFromQQDataFileContinuousDataList);

			CreateDataFile(creators.ToArray());
			CreateDecayWidthsFromQQDataFilePlotFile();

			return StartGnuplot();
		}

		public Process PlotEnergiesFromQQDataFile()
		{
			List<DataListCreator> creators = new List<DataListCreator>();
			foreach(BottomiumState state in BottomiumStates)
			{
				creators.Add(() => CreateEnergiesFromQQDataFileDataList(state));
			}
			creators.Add(CreateEnergiesFromQQDataFileContinuousDataList);

			CreateDataFile(creators.ToArray());
			CreateEnergiesFromQQDataFilePlotFile();

			return StartGnuplot();
		}

		public Process PlotInMediumDecayWidthsVersusMediumTemperature()
		{
			MediumVelocities = new List<double> { MediumVelocities[0] };
			CalculateInMediumDecayWidth();

			StringBuilder plotFile = new StringBuilder();
			AppendHeader_InMediumDecayWidthsVersusMediumTemperature(plotFile);
			plotFile.AppendLine();

			string[][] titleList = new string[DopplerShiftEvaluationTypes.Count][];
			for(int i = 0; i < DopplerShiftEvaluationTypes.Count; i++)
			{
				titleList[i] = BottomiumStates.ConvertAll(
					state => GetBottomiumStateGnuplotCode(state) + ", "
					+ DopplerShiftEvaluationTypes[i].ToUIString()).ToArray();
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
			MediumTemperatures_MeV = new List<double> { MediumTemperatures_MeV[0] };
			CalculateInMediumDecayWidth();

			StringBuilder plotFile = new StringBuilder();
			AppendHeader_InMediumDecayWidthsVersusMediumVelocity(plotFile);
			plotFile.AppendLine();

			string[][] titleList = new string[DopplerShiftEvaluationTypes.Count][];
			for(int i = 0; i < DopplerShiftEvaluationTypes.Count; i++)
			{
				titleList[i] = BottomiumStates.ConvertAll(
					state => GetBottomiumStateGnuplotCode(state) + ", "
					+ DopplerShiftEvaluationTypes[i].ToUIString()).ToArray();
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
			MediumTemperatures_MeV = new List<double> { MediumTemperatures_MeV[0] };
			MediumVelocities = new List<double> { MediumVelocities[0] };

			CreateDataFile(CreateDecayWidthEvaluatedAtDopplerShiftedTemperatureDataList);
			CreateDecayWidthEvaluatedAtDopplerShiftedTemperaturePlotFile();

			return StartGnuplot();
		}

		public Process PlotElectromagneticallyShiftedEnergies()
		{
			List<DataListCreator> creators = new List<DataListCreator>();
			foreach(BottomiumState state in BottomiumStates)
			{
				creators.Add(() => CreateElectromagneticallyShiftedEnergiesDataList(state));
			}

			CreateDataFile(creators.ToArray());
			CreateElectromagneticallyShiftedEnergiesPlotFile();

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

		private QQDataProvider CreateQQDataProvider()
		{
			return CreateQQDataProvider(ElectricDipoleAlignment);
		}

		private QQDataProvider CreateQQDataProvider(
			ElectricDipoleAlignment dipoleAlignment
			)
		{
			return new QQDataProvider(QQDataPathFile, PotentialTypes,
				DopplerShiftEvaluationType.UnshiftedTemperature, dipoleAlignment,
				DecayWidthType, QGPFormationTemperature_MeV, NumberAveragingAngles);
		}

		private List<List<double>> CreateDecayWidthsFromQQDataFileDataList(
			BottomiumState state
			)
		{
			List<List<double>> dataList = new List<List<double>>();

			List<QQDataSet> dataSets = QQDataProvider.GetBoundStateDataSets(
				QQDataPathFile, PotentialTypes, state);

			List<double> temperatures = new List<double>();
			List<double> decayWidths = new List<double>();
			foreach(QQDataSet dataSet in dataSets)
			{
				temperatures.Add(dataSet.Temperature);
				decayWidths.Add(dataSet.GetGamma(DecayWidthType));
			}

			dataList.Add(temperatures);
			dataList.Add(decayWidths);

			return dataList;
		}

		private List<List<double>> CreateDecayWidthsFromQQDataFileContinuousDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> temperatureValues = GetLinearAbscissaList(0, 800, 800);
			dataList.Add(temperatureValues);

			QQDataProvider provider = CreateQQDataProvider();

			foreach(BottomiumState state in BottomiumStates)
			{
				DecayWidthAverager averager = provider.CreateDecayWidthAverager(state);

				PlotFunction decayWidthFunction
					= temperature => averager.GetDecayWidth(temperature);

				AddPlotFunctionLists(dataList, temperatureValues, decayWidthFunction);
			}

			return dataList;
		}

		private void CreateDecayWidthsFromQQDataFilePlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Decay widths "
				+ GetDecayWidthTypeGnuplotCode(DecayWidthType) + " from QQDataFile'");
			plotFile.AppendLine("set xlabel 'T (MeV)'");
			plotFile.AppendLine("set ylabel '" + GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, BottomiumStates.Count);
			plotFile.AppendLine();
			plotFile.AppendLine("set grid");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile: plotFile,
				style: "points",
				titles: BottomiumStates.ConvertAll(
					state => new string[] { GetBottomiumStateGnuplotCode(state) + ", data file" }).ToArray());

			AppendPlotCommands(
					appendToExistingPlotCommands: true,
					index: BottomiumStates.Count,
					plotFile: plotFile,
					style: "lines noautoscale",
					titles: BottomiumStates.ConvertAll(
						state => GetBottomiumStateGnuplotCode(state) + ", continuous"));

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateEnergiesFromQQDataFileDataList(
			BottomiumState state
			)
		{
			List<List<double>> dataList = new List<List<double>>();

			List<QQDataSet> dataSets = QQDataProvider.GetBoundStateDataSets(
				QQDataPathFile, PotentialTypes, state);

			List<double> temperatures = new List<double>();
			List<double> energies = new List<double>();
			foreach(QQDataSet dataSet in dataSets)
			{
				temperatures.Add(dataSet.Temperature);
				energies.Add(dataSet.Energy);
			}

			dataList.Add(temperatures);
			dataList.Add(energies);

			return dataList;
		}

		private List<List<double>> CreateEnergiesFromQQDataFileContinuousDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> temperatureValues = GetLinearAbscissaList(0, 800, 800);
			dataList.Add(temperatureValues);

			QQDataProvider provider = CreateQQDataProvider();

			foreach(BottomiumState state in BottomiumStates)
			{
				DecayWidthAverager averager = provider.CreateDecayWidthAverager(state);

				PlotFunction energyFunction = temperature => averager.GetEnergy(temperature);

				AddPlotFunctionLists(dataList, temperatureValues, energyFunction);
			}

			return dataList;
		}

		private void CreateEnergiesFromQQDataFilePlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Energies E from QQDataFile'");
			plotFile.AppendLine("set xlabel 'T (MeV)'");
			plotFile.AppendLine("set ylabel 'E (MeV)'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, BottomiumStates.Count);
			plotFile.AppendLine();
			plotFile.AppendLine("set grid");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile: plotFile,
				style: "points",
				titles: BottomiumStates.ConvertAll(
					state => new string[] { GetBottomiumStateGnuplotCode(state) + ", data file" }).ToArray());

			AppendPlotCommands(
					appendToExistingPlotCommands: true,
					index: BottomiumStates.Count,
					plotFile: plotFile,
					style: "lines noautoscale",
					titles: BottomiumStates.ConvertAll(
						state => GetBottomiumStateGnuplotCode(state) + ", continuous"));

			WritePlotFile(plotFile);
		}

		private void AppendHeader_InMediumDecayWidthsVersusMediumTemperature(
			StringBuilder plotFile
			)
		{
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced");
			plotFile.AppendLine();
			plotFile.AppendLine("set title '" + InMediumDecayWidthPlottingTitle
				+ " for medium velocity |u| = " + MediumVelocities[0].ToUIString() + " c" + "'");
			plotFile.AppendLine("set xlabel 'T (MeV)'");
			plotFile.AppendLine("set ylabel '" + GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, BottomiumStates.Count);
		}

		private void AppendHeader_InMediumDecayWidthsVersusMediumVelocity(
			StringBuilder plotFile
			)
		{
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced");
			plotFile.AppendLine();
			plotFile.AppendLine("set title '" + InMediumDecayWidthPlottingTitle
				+ " for medium temperature T = " + MediumTemperatures_MeV[0].ToUIString() + " MeV" + "'");
			plotFile.AppendLine("set xlabel 'u (c)'");
			plotFile.AppendLine("set ylabel '" + GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, BottomiumStates.Count);
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
			plotFile.AppendLine("set terminal windows enhanced");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'In-medium decay width "
				+ GetDecayWidthTypeGnuplotCode(DecayWidthType)
				+ " evaluated at a Doppler-shifted temperature"
				+ " for medium temperature T = " + MediumTemperatures_MeV[0].ToUIString() + " MeV"
				+ " and medium velocity |u| = " + MediumVelocities[0].ToUIString() + " c" + "'");
			plotFile.AppendLine("set xlabel 'cos({/Symbol q})'");
			plotFile.AppendLine("set ylabel '"
				+ GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, BottomiumStates.Count);
			plotFile.AppendLine();
			plotFile.AppendLine("set style fill transparent solid 0.2");
			plotFile.AppendLine();

			string[] titleList = BottomiumStates.ConvertAll(
				state => GetBottomiumStateGnuplotCode(state)).ToArray();

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

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateDecayWidthEvaluatedAtDopplerShiftedTemperatureDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> cosineValues = GetLinearAbscissaList(-1, 1, NumberAveragingAngles);
			dataList.Add(cosineValues);

			QQDataProvider provider = CreateQQDataProvider();

			List<DecayWidthAverager> averagers = new List<DecayWidthAverager>();
			foreach(BottomiumState state in BottomiumStates)
			{
				averagers.Add(provider.CreateDecayWidthAverager(state));
			}

			foreach(DecayWidthAverager averager in averagers)
			{
				PlotFunction function = cosine => averager.GetDecayWidth(
					DecayWidthAverager.GetDopplerShiftedTemperature(
						MediumTemperatures_MeV[0], MediumVelocities[0], cosine));

				AddPlotFunctionLists(dataList, cosineValues, function);
			}

			foreach(DecayWidthAverager averager in averagers)
			{
				PlotFunction function = cosine => averager.GetDecayWidth(MediumTemperatures_MeV[0]);

				AddPlotFunctionLists(dataList, cosineValues, function);
			}

			return dataList;
		}

		private void CreateElectromagneticallyShiftedEnergiesPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Electromagnetically shifted energies"
				+ " for E = " + ElectricFieldStrength_per_fm2.ToString() + " fm^{-2}"
				+ ", B = " + MagneticFieldStrength_per_fm2.ToString() + " fm^{-2}'");
			plotFile.AppendLine("set xlabel 'T (MeV)'");
			plotFile.AppendLine("set ylabel 'E (MeV)'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, BottomiumStates.Count);
			plotFile.AppendLine();
			plotFile.AppendLine("set style fill transparent solid 0.2");
			plotFile.AppendLine();

			string[] titleList = BottomiumStates.ConvertAll(
				state => GetBottomiumStateGnuplotCode(state)).ToArray();

			plotFile.AppendLine("plot\\");

			for(int i = 0; i < titleList.Length; i++)
			{
				plotFile.AppendFormat("'{0}' index {1} using 1:2 with lines title '{2}',\\",
					DataFileName, i, titleList[i]);
				plotFile.AppendLine();
			}

			for(int i = 0; i < titleList.Length; i++)
			{
				plotFile.AppendFormat("'{0}' index {1} using 1:3:4 with filledcurves notitle,\\",
					DataFileName, i);
				plotFile.AppendLine();
			}

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateElectromagneticallyShiftedEnergiesDataList(
			BottomiumState state
			)
		{
			List<List<double>> dataList = new List<List<double>> { MediumTemperatures_MeV };

			List<ElectricDipoleAlignment> alignmentList = new List<ElectricDipoleAlignment>()
			{
				ElectricDipoleAlignment.Random,
				ElectricDipoleAlignment.StrengthenBinding,
				ElectricDipoleAlignment.WeakenBinding
			};

			foreach(ElectricDipoleAlignment alignment in alignmentList)
			{
				DecayWidthAverager averager = CreateQQDataProvider(alignment).CreateDecayWidthAverager(state);

				PlotFunction function = temperature => averager.GetElectromagneticallyShiftedEnergy(
					temperature, ElectricFieldStrength_per_fm2, MagneticFieldStrength_per_fm2);

				AddPlotFunctionLists(dataList, MediumTemperatures_MeV, function);
			}

			return dataList;
		}
	}
}
