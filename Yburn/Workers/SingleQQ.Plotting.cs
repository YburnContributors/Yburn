﻿using Meta.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Yburn.QQState;

namespace Yburn.Workers
{
	partial class SingleQQ
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public Process PlotAlpha()
		{
			AssertInputValid_PlotAlpha();
			CreateDataFile(CreateAlphaDataList);
			CreateAlphaPlotFile();

			return StartGnuplot();
		}

		public Process PlotCrossSection()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine("set encoding utf8");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Numerical solution for the cross section'");
			plotFile.AppendLine("set xlabel 'E (MeV)'");
			plotFile.AppendLine("set ylabel '{/Symbol s} (mb)'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				style: "lines",
				titles: new string[] { "{/Symbol s}_g", "{/Symbol s}_{/Symbol p}" });

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		public Process PlotPionGDF()
		{
			double energy = Math.Max(Math.Abs(EnergyScale_MeV), PionGDF.MinEnergy);
			StringBuilder dataFile = new StringBuilder();

			double xStep = 1.0 / Samples;
			double xValue;
			for(int j = 1; j <= Samples; j++)
			{
				xValue = j * xStep;
				dataFile.AppendLine(string.Format("{0,-10}{1,-10}",
					 xValue.ToString("G6"),
					 PionGDF.GetValue(xValue, energy).ToString("G6")));
			}

			WriteDataFile(dataFile);

			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Pion gluon distribution function, Q = "
				+ energy.ToString("G6") + " MeV'");
			plotFile.AppendLine("set xlabel 'x'");
			plotFile.AppendLine("set ylabel 'g_{/Symbol p}'");
			plotFile.AppendLine();

			AppendPlotCommand(plotFile, style: "lines");

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		public Process PlotQQPotential()
		{
			AssertInputValid_PlotPotential();
			CreateDataFile(CreatePotentialDataList);
			CreatePotentialPlotFile();

			return StartGnuplot();
		}

		public Process PlotWaveFunction()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Numerical solution for the wave function'");
			plotFile.AppendLine("set xlabel 'r (fm)'");
			plotFile.AppendLine(
				"set ylabel 'Re{/Symbol Y}, Im{/Symbol Y}, |{/Symbol Y}| (fm^{-1/2})'");
			plotFile.AppendLine();
			plotFile.AppendLine("plot '" + DataFileName
				+ "' index 0 using 1:2 with lines title 'Re{/Symbol Y}', \\");
			plotFile.AppendLine("     '" + DataFileName
				+ "' index 0 using 1:3 with lines title 'Im{/Symbol Y}', \\");
			plotFile.AppendLine("     '" + DataFileName
				+ "' index 0 using 1:(sqrt($2**2+$3**2)) with lines title '|{/Symbol Y}|'");

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void AssertInputValid_PlotPotential()
		{
			if(MinRadius_fm < 0)
			{
				throw new Exception("MinRadius < 0.");
			}
			if(MaxRadius_fm <= MinRadius_fm)
			{
				throw new Exception("MaxRadius < MinRadius.");
			}
			if(Samples < 1)
			{
				throw new Exception("Samples < 1.");
			}
			if(string.IsNullOrEmpty(DataFileName))
			{
				throw new Exception("DataFileName invalid.");
			}
		}

		private List<List<double>> CreatePotentialDataList()
		{
			List<double> radiusValueList = CreateRadiusValueList();
			Potential potential = CreatePotential();

			return CreatePotentialValueList(potential, radiusValueList);
		}

		private List<List<double>> CreatePotentialValueList(
			Potential potential,
			List<double> radiusValueList
			)
		{
			List<double> realValues = new List<double>();
			List<double> imaginaryValues = new List<double>();
			foreach(double radius in radiusValueList)
			{
				Complex potentialValue = potential.Value(radius);
				realValues.Add(potentialValue.Re);
				imaginaryValues.Add(potentialValue.Im);
			}

			List<List<double>> valueList = new List<List<double>>
			{
				radiusValueList,
				realValues,
				imaginaryValues
			};

			return valueList;
		}

		private List<double> CreateRadiusValueList()
		{
			List<double> radiusValueList = new List<double>();

			double stepSize = (MaxRadius_fm - MinRadius_fm) / Samples;
			for(int i = 0; i < Samples; i++)
			{
				radiusValueList.Add(MinRadius_fm + i * stepSize);
			}

			return radiusValueList;
		}

		private Potential CreatePotential()
		{
			switch(PotentialType)
			{
				case PotentialType.Complex:
					return new ComplexPotential(AlphaSoft, Sigma_MeV2, ColorState, Temperature_MeV, DebyeMass_MeV);

				case PotentialType.Complex_NoString:
					return new ComplexPotential_NoString(AlphaSoft, ColorState, Temperature_MeV, DebyeMass_MeV);

				case PotentialType.LowT:
					return new LowTemperaturePotential(
						AlphaSoft, Sigma_MeV2, ColorState, Temperature_MeV, DebyeMass_MeV);

				case PotentialType.LowT_NoString:
					return new LowTemperaturePotential_NoString(
						AlphaSoft, ColorState, Temperature_MeV, DebyeMass_MeV);

				case PotentialType.Real:
					return new RealPotential(AlphaSoft, Sigma_MeV2, ColorState, DebyeMass_MeV);

				case PotentialType.Real_NoString:
					return new RealPotential_NoString(AlphaSoft, ColorState, DebyeMass_MeV);

				case PotentialType.Tzero:
					return new VacuumPotential(AlphaSoft, Sigma_MeV2, ColorState);

				case PotentialType.Tzero_NoString:
					return new VacuumPotential_NoString(AlphaSoft, ColorState);

				case PotentialType.SpinDependent:
					return new SpinDependentPotential(
						AlphaSoft, Sigma_MeV2, ColorState, SpinState, SpinCouplingRange_fm, SpinCouplingStrength_MeV);

				default:
					throw new Exception("Invalid PotentialType.");
			}
		}

		private void CreatePotentialPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Potential (V): " + PotentialType.ToString() + "'");
			plotFile.AppendLine("set xlabel 'Radius (fm)'");
			plotFile.AppendLine("set ylabel 'Re V, Im V (MeV)'");
			plotFile.AppendLine();

			AppendPlotCommands(plotFile, style: "lines", titles: new string[] { "Re V", "Im V" });

			WritePlotFile(plotFile);
		}

		private void AssertInputValid_PlotAlpha()
		{
			if(MinEnergy_MeV < 0)
			{
				throw new Exception("MinEnergy < 0.");
			}
			if(MaxEnergy_MeV <= MinEnergy_MeV)
			{
				throw new Exception("MaxEnergy <= MinEnergy.");
			}
			if(Samples < 1)
			{
				throw new Exception("Samples < 1.");
			}
			if(string.IsNullOrEmpty(DataFileName))
			{
				throw new Exception("DataFileName invalid.");
			}
		}

		private void CreateAlphaPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Running coupling {/Symbol a}_s'");
			plotFile.AppendLine("set xlabel 'Energy (MeV)'");
			plotFile.AppendLine("set ylabel '{/Symbol a}_s'");
			plotFile.AppendLine();

			AppendAlphaPlotCommands(plotFile);

			WritePlotFile(plotFile);
		}

		private void AppendAlphaPlotCommands(
			StringBuilder plotFile
			)
		{
			bool isFirstCommand = true;
			int plotColumn = 2;

			foreach(RunningCouplingType type in RunningCouplingTypeSelection)
			{
				string title = type.ToString().Replace("_", "\\_");
				if(isFirstCommand)
				{
					plotFile.AppendFormat("plot '{0}' using 1:{1} with lines title '{2}'",
						DataFileName, plotColumn, title);
					isFirstCommand = false;
				}
				else
				{
					plotFile.AppendLine(", \\");
					plotFile.AppendFormat("'{0}' using 1:{1} with lines title '{2}'",
						DataFileName, plotColumn, title);
				}
				plotColumn++;
			}
		}

		private List<List<double>> CreateAlphaDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> energyValues = GetEnergyValueList();
			dataList.Add(energyValues);

			AddAlphaLists(dataList, energyValues);

			return dataList;
		}

		private void AddAlphaLists(
			List<List<double>> dataList,
			List<double> energyValues
			)
		{
			foreach(RunningCouplingType type in RunningCouplingTypeSelection)
			{
				List<double> alphaValues = GetAlphaValueList(energyValues, type);
				dataList.Add(alphaValues);
			}
		}

		private List<double> GetEnergyValueList()
		{
			List<double> energyValues = new List<double>();

			double step = (MaxEnergy_MeV - MinEnergy_MeV) / Samples;
			for(int i = 0; i < Samples; i++)
			{
				energyValues.Add(MinEnergy_MeV + step * i);
			}

			return energyValues;
		}

		private List<double> GetAlphaValueList(
			List<double> energyValues,
			RunningCouplingType type
			)
		{
			List<double> alphaValues = new List<double>();
			RunningCoupling alpha = RunningCoupling.Create(type);
			foreach(double energyValue in energyValues)
			{
				alphaValues.Add(alpha.Value(energyValue));
			}

			return alphaValues;
		}
	}
}
