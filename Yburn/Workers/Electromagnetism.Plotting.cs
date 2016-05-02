using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Yburn.Fireball;

namespace Yburn.Workers
{
	public partial class Electromagnetism
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public Process PlotPointChargeAzimutalMagneticField()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(CreatePointChargeAzimutalMagneticFieldDataList);
			CreatePointChargeAzimutalMagneticFieldPlotFile();

			return StartGnuplot();
		}

		public Process PlotPointChargeLongitudinalElectricField()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(CreatePointChargeLongitudinalElectricFieldDataList);
			CreatePointChargeLongitudinalElectricFieldPlotFile();

			return StartGnuplot();
		}

		public Process PlotPointChargeRadialElectricField()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(CreatePointChargeRadialElectricFieldDataList);
			CreatePointChargeRadialElectricFieldPlotFile();

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private int EffectiveTimeSamples;

		private EMFCalculationMethod[] EMFCalculationMethodSelection = new EMFCalculationMethod[0];

		private double LorentzFactor;

		private double RadialDistance;

		private double StartEffectiveTime;

		private double StopEffectiveTime;

		private delegate List<List<double>> DataListCreator();

		private string DataFileName = string.Empty;

		private string DataPathFile
		{
			get
			{
				return YburnConfigFile.OutputPath + DataFileName;
			}
		}

		private string FormattedDataPathFile
		{
			get
			{
				return DataPathFile.Replace("\\", "/");
			}
		}

		private string FormattedPlotPathFile
		{
			get
			{
				return FormattedDataPathFile + ".plt";
			}
		}

		private void CreateDataFile(
			DataListCreator dataListCreator
			)
		{
			List<List<double>> dataList = dataListCreator();

			StringBuilder plotFile = new StringBuilder();
			for(int i = 0; i < dataList[0].Count; i++)
			{
				WriteLine(dataList, plotFile, i);
			}

			File.WriteAllText(DataPathFile, plotFile.ToString());
		}

		private void WriteLine(
			List<List<double>> dataList,
			StringBuilder plotFile,
			int index
			)
		{
			foreach(List<double> list in dataList)
			{
				plotFile.AppendFormat("{0,-25}",
					list[index].ToString());
			}
			plotFile.AppendLine();
		}

		private void AppendPlotCommands(
			StringBuilder plotFile,
			string[] titleList
			)
		{
			bool isFirstCommand = true;
			int plotColumn = 2;

			foreach(string title in titleList)
			{
				string formattedTitle = title.Replace("_", "\\\\_");
				if(isFirstCommand)
				{
					plotFile.AppendFormat("plot \"{0}\" using 1:{1} with lines title \"{2}\"",
						FormattedDataPathFile, plotColumn, formattedTitle);
					isFirstCommand = false;
				}
				else
				{
					plotFile.AppendLine(", \\");
					plotFile.AppendFormat("\"{0}\" using 1:{1} with lines title \"{2}\"",
						FormattedDataPathFile, plotColumn, formattedTitle);
				}
				plotColumn++;
			}
		}

		private Process StartGnuplot()
		{
			return Process.Start("wgnuplot", "\"" + FormattedPlotPathFile + "\" --persist");
		}

		private void AssertInputValid_PlotPointChargeField()
		{
			if(LorentzFactor < 1)
			{
				throw new Exception("LorentzFactor < 1.");
			}
			if(RadialDistance < 0)
			{
				throw new Exception("RadialDistance < 0.");
			}
			if(StartEffectiveTime < 0)
			{
				throw new Exception("StartEffectiveTime < 0.");
			}
			if(StopEffectiveTime <= StartEffectiveTime)
			{
				throw new Exception("StopEffectiveTime <= StartEffectiveTime.");
			}
			if(EffectiveTimeSamples < 1)
			{
				throw new Exception("EffectiveTimeSamples < 1.");
			}
			if(string.IsNullOrEmpty(DataFileName))
			{
				throw new Exception("DataFileName invalid.");
			}
		}

		private void CreatePointChargeAzimutalMagneticFieldPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Azimutal magnetic field of a point charge"
				+ " with Lorentz factor {/Symbol g} = " + LorentzFactor.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"eH_{/Symbol f}/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(EMFCalculationMethodSelection, item => item.ToString());
			AppendPlotCommands(plotFile, titleList);

			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private List<List<double>> CreatePointChargeAzimutalMagneticFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetPointChargeFieldTimeValueList();
			dataList.Add(timeValues);

			AddPointChargeAzimutalMagneticFieldLists(dataList, timeValues);

			return dataList;
		}

		private void AddPointChargeAzimutalMagneticFieldLists(
			List<List<double>> dataList,
			List<double> timeValues
			)
		{
			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				List<double> fieldValues =
					GetPointChargeAzimutalMagneticFieldValueList(timeValues, method);
				dataList.Add(fieldValues);
			}
		}

		private List<double> GetPointChargeAzimutalMagneticFieldValueList(
			List<double> timeValues,
			EMFCalculationMethod method
			)
		{
			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.MeanPionMass)
				* (PhysConst.HBARC / PhysConst.MeanPionMass);
			List<double> fieldValues = new List<double>();
			ElectromagneticField emf = ElectromagneticField.Create(CreateFireballParam(method));
			foreach(double timeValue in timeValues)
			{
				fieldValues.Add(normalization * emf.CalculatePointChargeAzimutalMagneticField(
					timeValue, RadialDistance, LorentzFactor));
			}

			return fieldValues;
		}

		private void CreatePointChargeLongitudinalElectricFieldPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Longitudinal electric field of a point charge"
				+ " with Lorentz factor {/Symbol g} = " + LorentzFactor.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"e|E_{z}|/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(EMFCalculationMethodSelection, item => item.ToString());
			AppendPlotCommands(plotFile, titleList);

			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private List<List<double>> CreatePointChargeLongitudinalElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetPointChargeFieldTimeValueList();
			dataList.Add(timeValues);

			AddPointChargeLongitudinalElectricFieldLists(dataList, timeValues);

			return dataList;
		}

		private void AddPointChargeLongitudinalElectricFieldLists(
			List<List<double>> dataList,
			List<double> timeValues
			)
		{
			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				List<double> fieldValues =
					GetPointChargeLongitudinalElectricFieldValueList(timeValues, method);
				dataList.Add(fieldValues);
			}
		}

		private List<double> GetPointChargeLongitudinalElectricFieldValueList(
			List<double> timeValues,
			EMFCalculationMethod method
			)
		{
			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.MeanPionMass)
				* (PhysConst.HBARC / PhysConst.MeanPionMass);
			List<double> fieldValues = new List<double>();
			ElectromagneticField emf = ElectromagneticField.Create(CreateFireballParam(method));
			foreach(double timeValue in timeValues)
			{
				fieldValues.Add(normalization * Math.Abs(emf.CalculatePointChargeLongitudinalElectricField(
					timeValue, RadialDistance, LorentzFactor)));
			}

			return fieldValues;
		}

		private void CreatePointChargeRadialElectricFieldPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Radial electric field of a point charge"
				+ " with Lorentz factor {/Symbol g} = " + LorentzFactor.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"eE_{r}/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(EMFCalculationMethodSelection, item => item.ToString());
			AppendPlotCommands(plotFile, titleList);

			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private List<List<double>> CreatePointChargeRadialElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetPointChargeFieldTimeValueList();
			dataList.Add(timeValues);

			AddPointChargeRadialElectricFieldLists(dataList, timeValues);

			return dataList;
		}

		private void AddPointChargeRadialElectricFieldLists(
			List<List<double>> dataList,
			List<double> timeValues
			)
		{
			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				List<double> fieldValues =
					GetPointChargeRadialElectricFieldValueList(timeValues, method);
				dataList.Add(fieldValues);
			}
		}

		private List<double> GetPointChargeRadialElectricFieldValueList(
			List<double> timeValues,
			EMFCalculationMethod method
			)
		{
			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.MeanPionMass)
				* (PhysConst.HBARC / PhysConst.MeanPionMass);
			List<double> fieldValues = new List<double>();
			ElectromagneticField emf = ElectromagneticField.Create(CreateFireballParam(method));
			foreach(double timeValue in timeValues)
			{
				fieldValues.Add(normalization * emf.CalculatePointChargeRadialElectricField(
					timeValue, RadialDistance, LorentzFactor));
			}

			return fieldValues;
		}

		private List<double> GetPointChargeFieldTimeValueList()
		{
			List<double> timeValues = new List<double>();

			// avoid possible divergences in the fields at StartEffectiveTime = 0
			if(StartEffectiveTime > 0)
			{
				timeValues.Add(StartEffectiveTime);
			}

			double step = (StopEffectiveTime - StartEffectiveTime) / EffectiveTimeSamples;
			for(int i = 1; i <= EffectiveTimeSamples; i++)
			{
				timeValues.Add(StartEffectiveTime + step * i);
			}

			return timeValues;
		}
	}
}