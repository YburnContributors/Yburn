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

		public Process PlotSingleNucleusMagneticFieldStrength()
		{
			CreateDataFile(CreateSingleNucleusMagneticFieldStrengthDataList);
			CreateSingleNucleusMagneticFieldStrengthPlotFile();

			return StartGnuplot();
		}

		public Process PlotCentralMagneticFieldStrength()
		{
			CreateDataFile(CreateCentralMagneticFieldStrengthDataList);
			CreateCentralMagneticFieldStrengthPlotFile();

			return StartGnuplot();
		}

		public Process PlotAverageMagneticFieldStrength()
		{
			CreateDataFile(CreateAverageMagneticFieldStrengthDataList);
			CreateAverageMagneticFieldStrengthPlotFile();

			return StartGnuplot();
		}

		public Process PlotOrthoParaStateOverlap()
		{
			CreateDataFile(CreateOrthoParaStateOverlapDataList);
			CreateOrthoParaStateOverlapPlotFile();

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

		private double[] ParticleRapidityValues = new double[] { -3, -2, -1, 0, 1, 2, 3 };

		private void CreateSingleNucleusMagneticFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Single nucleus magnetic field strength");
			plotFile.AppendLine("set xlabel \"{/Symbol r} (fm)\"");
			plotFile.AppendLine("set ylabel \"|B| (1/fm^2)\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(
				ParticleRapidityValues, value => "y = " + value.ToString());
			AppendPlotCommands(plotFile, titleList, "linespoints");

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateSingleNucleusMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> radialDistanceValues = GetLinearAbscissaList(0, 20, 20);
			radialDistanceValues.Remove(0);
			dataList.Add(radialDistanceValues);

			foreach(double rapidityValue in ParticleRapidityValues)
			{
				FireballParam param = CreateFireballParam(
					EMFCalculationMethod.DiffusionApproximation);

				PlotFunction fieldValue = radialDistance =>
				{
					FireballElectromagneticField emf = new FireballElectromagneticField(param);
					NuclearDensityFunction densityA;
					NuclearDensityFunction densityB;

					NuclearDensityFunction.CreateProtonDensityFunctionPair(
						param, out densityA, out densityB);

					return emf.CalculateSingleNucleusMagneticFieldInLCF(
						param.FormationTimesFm[0],
						new EuclideanVector2D(radialDistance, 0),
						rapidityValue,
						param.ParticleVelocity,
						densityA).Norm;
				};

				AddPlotFunctionLists(dataList, radialDistanceValues, fieldValue);
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
			plotFile.AppendLine("set xlabel \"b (fm)\"");
			plotFile.AppendLine("set ylabel \"|B(0,0,0)| (1/fm^2)\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			AppendPlotCommands(plotFile, EMFCalculationMethodSelectionTitleList, "linespoints");

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateCentralMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 30, 15);
			dataList.Add(impactParamValues);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				FireballParam param = CreateFireballParam(method);

				PlotFunction fieldValue = impactParam =>
				{
					param.ImpactParameterFm = impactParam;
					FireballElectromagneticField emf = new FireballElectromagneticField(param);

					return emf.CalculateMagneticFieldInCMS(
						param.FormationTimesFm[0], new EuclideanVector3D(0, 0, 0)).Norm;
				};

				AddPlotFunctionLists(dataList, impactParamValues, fieldValue);
			}

			return dataList;
		}

		private void CreateAverageMagneticFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Average magnetic field strength");
			plotFile.AppendLine("set xlabel \"b (fm)\"");
			plotFile.AppendLine("set ylabel \"<B> (1/fm^2)\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile, EMFCalculationMethod.DiffusionApproximation.ToString(), "linespoints");

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateAverageMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 30, 15);
			dataList.Add(impactParamValues);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			PlotFunction fieldValue = impactParam =>
			{
				param.ImpactParameterFm = impactParam;
				MagneticFieldStrengthAverager avg = new MagneticFieldStrengthAverager(param);

				return avg.CalculateAverageMagneticFieldStrengthInLCF(QuadraturePrecision.Use8Points);
			};

			AddPlotFunctionLists(dataList, impactParamValues, fieldValue);

			return dataList;
		}

		private void CreateOrthoParaStateOverlapPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Ortho/para state overlap");
			plotFile.AppendLine("set xlabel \"b (fm)\"");
			plotFile.AppendLine("set ylabel \"|<Y(B)|{/Symbol h}_b(0)>|^2\"");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile, EMFCalculationMethod.DiffusionApproximation.ToString(), "linespoints");

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateOrthoParaStateOverlapDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 30, 15);
			dataList.Add(impactParamValues);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			PlotFunction shiftValue = impactParam =>
			{
				param.ImpactParameterFm = impactParam;
				MagneticFieldStrengthAverager avg = new MagneticFieldStrengthAverager(param);

				return avg.CalculateOverlapBetweenParaAndShiftedOrthoState(QuadraturePrecision.Use8Points);
			};

			AddPlotFunctionLists(dataList, impactParamValues, shiftValue);

			return dataList;
		}
	}
}