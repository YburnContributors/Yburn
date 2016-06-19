using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Yburn.Fireball;
using Yburn.PhysUtil;

namespace Yburn.Workers
{
	partial class Electromagnetism
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

		private string[] EMFCalculationMethodSelectionTitleList
		{
			get
			{
				return Array.ConvertAll(EMFCalculationMethodSelection, method => method.ToString());
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

			AppendPlotCommands(plotFile, EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeAzimutalMagneticFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);

			// avoid possible divergences in the fields at StartEffectiveTime = 0
			effectiveTimeValues.Remove(0);
			dataList.Add(effectiveTimeValues);

			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivityMeV, PointChargeVelocity);

				PlotFunction fieldValue = time => normalization
					* emf.CalculatePointChargeAzimutalMagneticField(time, RadialDistance);

				AddPlotFunctionLists(dataList, effectiveTimeValues, fieldValue);
			}

			return dataList;
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

			AppendPlotCommands(plotFile, EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeLongitudinalElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);

			// avoid possible divergences in the fields at StartEffectiveTime = 0
			effectiveTimeValues.Remove(0);
			dataList.Add(effectiveTimeValues);

			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivityMeV, PointChargeVelocity);

				PlotFunction fieldValue = time => Math.Abs(normalization
					* emf.CalculatePointChargeLongitudinalElectricField(time, RadialDistance));

				AddPlotFunctionLists(dataList, effectiveTimeValues, fieldValue);
			}

			return dataList;
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

			AppendPlotCommands(plotFile, EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeRadialElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);

			// avoid possible divergences in the fields at StartEffectiveTime = 0
			effectiveTimeValues.Remove(0);
			dataList.Add(effectiveTimeValues);

			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivityMeV, PointChargeVelocity);

				PlotFunction fieldValue = time => normalization
					* emf.CalculatePointChargeRadialElectricField(time, RadialDistance);

				AddPlotFunctionLists(dataList, effectiveTimeValues, fieldValue);
			}

			return dataList;
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

			AppendPlotCommands(plotFile, EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateCentralMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);
			dataList.Add(timeValues);

			double normalization = PhysConst.ElementaryCharge
				* (PhysConst.HBARC / PhysConst.AveragePionMass)
				* (PhysConst.HBARC / PhysConst.AveragePionMass);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				FireballElectromagneticField emf =
					new FireballElectromagneticField(CreateFireballParam(method));

				PlotFunction fieldValue = time => normalization * emf.CalculateMagneticField(
					time, new EuclideanVector3D(0, 0, 0)).Y;

				AddPlotFunctionLists(dataList, timeValues, fieldValue);
			}

			return dataList;
		}
	}
}