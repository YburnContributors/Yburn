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

		public Process PlotCentralMagneticFieldStrength()
		{
			CreateDataFile(CreateCentralMagneticFieldStrengthDataList);
			CreateCentralMagneticFieldStrengthPlotFile();

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private delegate List<List<double>> DataListCreator();

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
			if(Math.Abs(PointChargeVelocity) > 1)
			{
				throw new Exception("|PointChargeVelocity| > 1.");
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
				+ " with velocity v = " + PointChargeVelocity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"eH_{/Symbol f}/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(
				EMFCalculationMethodSelection, item => item.ToString());
			AppendPlotCommands(plotFile, titleList);

			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private List<List<double>> CreatePointChargeAzimutalMagneticFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetPointChargeFieldEffectiveTimeValueList();
			dataList.Add(effectiveTimeValues);

			AddPointChargeAzimutalMagneticFieldLists(dataList, effectiveTimeValues);

			return dataList;
		}

		private void AddPointChargeAzimutalMagneticFieldLists(
			List<List<double>> dataList,
			List<double> effectiveTimeValues
			)
		{
			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				List<double> fieldValues =
					GetPointChargeAzimutalMagneticFieldValueList(effectiveTimeValues, method);
				dataList.Add(fieldValues);
			}
		}

		private List<double> GetPointChargeAzimutalMagneticFieldValueList(
			List<double> effectiveTimeValues,
			EMFCalculationMethod method
			)
		{
			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			List<double> fieldValues = new List<double>();
			PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
				method, QGPConductivityMeV, PointChargeVelocity);

			foreach(double effectiveTimeValue in effectiveTimeValues)
			{
				fieldValues.Add(normalization * emf.CalculatePointChargeAzimutalMagneticField(
					effectiveTimeValue, RadialDistance));
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
				+ " with velocity v = " + PointChargeVelocity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"e|E_{z}|/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(
				EMFCalculationMethodSelection, item => item.ToString());
			AppendPlotCommands(plotFile, titleList);

			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private List<List<double>> CreatePointChargeLongitudinalElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetPointChargeFieldEffectiveTimeValueList();
			dataList.Add(effectiveTimeValues);

			AddPointChargeLongitudinalElectricFieldLists(dataList, effectiveTimeValues);

			return dataList;
		}

		private void AddPointChargeLongitudinalElectricFieldLists(
			List<List<double>> dataList,
			List<double> effectiveTimeValues
			)
		{
			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				List<double> fieldValues =
					GetPointChargeLongitudinalElectricFieldValueList(effectiveTimeValues, method);
				dataList.Add(fieldValues);
			}
		}

		private List<double> GetPointChargeLongitudinalElectricFieldValueList(
			List<double> effectiveTimeValues,
			EMFCalculationMethod method
			)
		{
			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			List<double> fieldValues = new List<double>();
			PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
				method, QGPConductivityMeV, PointChargeVelocity);

			foreach(double effectiveTimeValue in effectiveTimeValues)
			{
				fieldValues.Add(Math.Abs(
					normalization * emf.CalculatePointChargeLongitudinalElectricField(
						effectiveTimeValue, RadialDistance)));
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
				+ " with velocity v = " + PointChargeVelocity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"eE_{r}/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(
				EMFCalculationMethodSelection, item => item.ToString());
			AppendPlotCommands(plotFile, titleList);

			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private List<List<double>> CreatePointChargeRadialElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetPointChargeFieldEffectiveTimeValueList();
			dataList.Add(effectiveTimeValues);

			AddPointChargeRadialElectricFieldLists(dataList, effectiveTimeValues);

			return dataList;
		}

		private void AddPointChargeRadialElectricFieldLists(
			List<List<double>> dataList,
			List<double> effectiveTimeValues
			)
		{
			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				List<double> fieldValues =
					GetPointChargeRadialElectricFieldValueList(effectiveTimeValues, method);
				dataList.Add(fieldValues);
			}
		}

		private List<double> GetPointChargeRadialElectricFieldValueList(
			List<double> effectiveTimeValues,
			EMFCalculationMethod method
			)
		{
			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			List<double> fieldValues = new List<double>();
			PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
				method, QGPConductivityMeV, PointChargeVelocity);

			foreach(double effectiveTimeValue in effectiveTimeValues)
			{
				fieldValues.Add(normalization * emf.CalculatePointChargeRadialElectricField(
					effectiveTimeValue, RadialDistance));
			}

			return fieldValues;
		}

		private List<double> GetPointChargeFieldEffectiveTimeValueList()
		{
			List<double> effectiveTimeValues = new List<double>();

			// avoid possible divergences in the fields at StartEffectiveTime = 0
			if(StartEffectiveTime > 0)
			{
				effectiveTimeValues.Add(StartEffectiveTime);
			}

			double step = (StopEffectiveTime - StartEffectiveTime) / EffectiveTimeSamples;
			for(int i = 1; i <= EffectiveTimeSamples; i++)
			{
				effectiveTimeValues.Add(StartEffectiveTime + step * i);
			}

			return effectiveTimeValues;
		}

		private void CreateCentralMagneticFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Central magnetic field strength");
			plotFile.AppendLine("set xlabel \"t (fm/c)\"");
			plotFile.AppendLine("set ylabel \"eB_{y}/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(
				EMFCalculationMethodSelection, item => item.ToString());
			AppendPlotCommands(plotFile, titleList);

			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private List<List<double>> CreateCentralMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetCentralMagneticFieldStrengthTimeValueList();
			dataList.Add(effectiveTimeValues);

			AddCentralMagneticFieldStrengthLists(dataList, effectiveTimeValues);

			return dataList;
		}

		private void AddCentralMagneticFieldStrengthLists(
			List<List<double>> dataList,
			List<double> effectiveTimeValues
			)
		{
			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				List<double> fieldValues =
					GetCentralMagneticFieldStrengthValueList(effectiveTimeValues, method);
				dataList.Add(fieldValues);
			}
		}

		private List<double> GetCentralMagneticFieldStrengthValueList(
			List<double> timeValues,
			EMFCalculationMethod method
			)
		{
			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			List<double> fieldValues = new List<double>();
			FireballElectromagneticField emf =
				new FireballElectromagneticField(CreateFireballParam(method));

			foreach(double timeValue in timeValues)
			{
				fieldValues.Add(normalization * emf.CalculateMagneticField(
					timeValue, new EuclideanVector3D(0, 0, 0)).Y);
			}

			return fieldValues;
		}

		private List<double> GetCentralMagneticFieldStrengthTimeValueList()
		{
			List<double> timeValues = new List<double>();

			double step = (StopEffectiveTime - StartEffectiveTime) / EffectiveTimeSamples;
			for(int i = 0; i <= EffectiveTimeSamples; i++)
			{
				timeValues.Add(StartEffectiveTime + step * i);
			}

			return timeValues;
		}
	}
}