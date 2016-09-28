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

		public Process PlotPointChargeAndNucleusFieldComponents()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(
				CreatePointChargeAndNucleusFieldComponentsEffectiveTimeDataList,
				CreatePointChargeAndNucleusFieldComponentsRadialDistanceDataList);
			CreatePointChargeAndNucleusFieldComponentsPlotFile();

			return StartGnuplot();
		}

		public Process PlotNucleusMagneticFieldStrength()
		{
			CreateDataFile(CreateNucleusMagneticFieldStrengthDataList);
			CreateNucleusMagneticFieldStrengthPlotFile();

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

		public Process PlotSpinStateOverlap()
		{
			CreateDataFile(CreateSpinStateOverlapDataList);
			CreateSpinStateOverlapPlotFile();

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double EMFieldNormalization = Constants.ElementaryCharge
			* (Constants.HbarCMeVFm / Constants.PionAverageMassMeV)
			* (Constants.HbarCMeVFm / Constants.PionAverageMassMeV);

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void AssertInputValid_PlotPointChargeField()
		{
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
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"eH_{/Symbol f}/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			AppendPlotCommands(plotFile, "lines", EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeAzimutalMagneticFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);

			// avoid possible logplot divergences at EffectiveTime = 0
			effectiveTimeValues.Remove(0);
			dataList.Add(effectiveTimeValues);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivityMeV, PointChargeRapidity);

				PlotFunction fieldValue = time => EMFieldNormalization
					* emf.CalculateAzimutalMagneticField(time, RadialDistance);

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
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"e|E_{z}|/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			AppendPlotCommands(plotFile, "lines", EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeLongitudinalElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);

			// avoid possible logplot divergences at EffectiveTime = 0
			effectiveTimeValues.Remove(0);
			dataList.Add(effectiveTimeValues);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivityMeV, PointChargeRapidity);

				PlotFunction fieldValue = time => Math.Abs(EMFieldNormalization
					* emf.CalculateLongitudinalElectricField(time, RadialDistance));

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
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"eE_{r}/m_{/Symbol p}^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			AppendPlotCommands(plotFile, "lines", EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeRadialElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);

			// avoid possible logplot divergences at EffectiveTime = 0
			effectiveTimeValues.Remove(0);
			dataList.Add(effectiveTimeValues);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivityMeV, PointChargeRapidity);

				PlotFunction fieldValue = time => EMFieldNormalization
					* emf.CalculateRadialElectricField(time, RadialDistance);

				AddPlotFunctionLists(dataList, effectiveTimeValues, fieldValue);
			}

			return dataList;
		}

		private void CreatePointChargeAndNucleusFieldComponentsPlotFile()
		{
			string[] titleList = {
				"E^1_{/Symbol r}", "B^1_{/Symbol f}", "E^1_z",
				"E^Z_{/Symbol r}", "B^Z_{/Symbol f}", "E^Z_z" };

			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Field components of a point charge and nucleus"
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm\"");
			plotFile.AppendLine("set xlabel \"t - z/v (fm/c)\"");
			plotFile.AppendLine("set ylabel \"|E|, |B|\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			AppendPlotCommands(plotFile, 0, "lines", titleList);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Field components of a point charge and nucleus"
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at effective time (t - z/v) = " + RadialDistance.ToString("G6") + " fm/c\"");
			plotFile.AppendLine("set xlabel \"{/Symbol r} (fm)\"");
			plotFile.AppendLine();

			AppendPlotCommands(plotFile, 1, "lines", titleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeAndNucleusFieldComponentsEffectiveTimeDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, EffectiveTimeSamples);

			// avoid possible logplot divergences at EffectiveTime = 0
			effectiveTimeValues.Remove(0);
			dataList.Add(effectiveTimeValues);

			Nucleus nucleusA;
			Nucleus nucleusB;
			Nucleus.CreateNucleusPair(
				CreateFireballParam(EMFCalculationMethod), out nucleusA, out nucleusB);

			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				EMFCalculationMethod, QGPConductivityMeV, PointChargeRapidity);
			NucleusElectromagneticField nucEMF = new NucleusElectromagneticField(
				EMFCalculationMethod, QGPConductivityMeV, PointChargeRapidity, nucleusA);

			PlotFunction[] plotFunctions = {
				t => pcEMF.CalculateRadialElectricField(t, RadialDistance),
				t => pcEMF.CalculateAzimutalMagneticField(t, RadialDistance),
				t => Math.Abs(pcEMF.CalculateLongitudinalElectricField(t, RadialDistance)),
				t => nucEMF.CalculateRadialElectricField(t, RadialDistance, QuadratureOrder),
				t => nucEMF.CalculateAzimutalMagneticField(t, RadialDistance, QuadratureOrder),
				t => Math.Abs(nucEMF.CalculateLongitudinalElectricField(t, RadialDistance, QuadratureOrder)) };

			foreach(PlotFunction function in plotFunctions)
			{
				AddPlotFunctionLists(dataList, effectiveTimeValues, function);
			}

			return dataList;
		}

		private List<List<double>> CreatePointChargeAndNucleusFieldComponentsRadialDistanceDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> radialDistanceValues = GetLinearAbscissaList(
				0, 25, EffectiveTimeSamples);

			// avoid possible logplot divergences at RadialDistance = 0
			radialDistanceValues.Remove(0);
			dataList.Add(radialDistanceValues);

			Nucleus nucleusA;
			Nucleus nucleusB;
			Nucleus.CreateNucleusPair(
				CreateFireballParam(EMFCalculationMethod), out nucleusA, out nucleusB);

			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				EMFCalculationMethod, QGPConductivityMeV, PointChargeRapidity);
			NucleusElectromagneticField nucEMF = new NucleusElectromagneticField(
				EMFCalculationMethod, QGPConductivityMeV, PointChargeRapidity, nucleusA);

			double effectiveTime = 0.4;

			PlotFunction[] plotFunctions = {
				r => pcEMF.CalculateRadialElectricField(effectiveTime, r),
				r => pcEMF.CalculateAzimutalMagneticField(effectiveTime, r),
				r => Math.Abs(pcEMF.CalculateLongitudinalElectricField(effectiveTime, r)),
				r => nucEMF.CalculateRadialElectricField(effectiveTime, r, QuadratureOrder),
				r => nucEMF.CalculateAzimutalMagneticField(effectiveTime, r, QuadratureOrder),
				r => Math.Abs(nucEMF.CalculateLongitudinalElectricField(effectiveTime, r, QuadratureOrder)) };

			foreach(PlotFunction function in plotFunctions)
			{
				AddPlotFunctionLists(dataList, radialDistanceValues, function);
			}

			return dataList;
		}

		private double[] ParticleRapidityValues = new double[] { -3, -2, -1, 0, 1, 2, 3 };

		private void CreateNucleusMagneticFieldStrengthPlotFile()
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
			AppendPlotCommands(plotFile, "linespoints", titleList);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateNucleusMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> radialDistanceValues = GetLinearAbscissaList(0, 20, 20);

			// avoid possible logplot divergences at EffectiveTime = 0
			radialDistanceValues.Remove(0);
			dataList.Add(radialDistanceValues);

			foreach(double rapidityValue in ParticleRapidityValues)
			{
				FireballParam param = CreateFireballParam(
					EMFCalculationMethod.DiffusionApproximation);

				PlotFunction fieldValue = radialDistance =>
				{
					Nucleus nucleusA;
					Nucleus nucleusB;
					Nucleus.CreateNucleusPair(
						param, out nucleusA, out nucleusB);

					NucleusElectromagneticField emf = new NucleusElectromagneticField(
						param.EMFCalculationMethod,
						param.QGPConductivityMeV,
						param.BeamRapidity,
						nucleusA);

					return emf.CalculateMagneticField_LCF(
						param.FormationTimesFm[0],
						radialDistance,
						0,
						rapidityValue,
						QuadratureOrder).Norm;
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

			AppendPlotCommands(plotFile, "linespoints", EMFCalculationMethodSelectionTitleList);

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

					return emf.CalculateMagneticField(
						param.FormationTimesFm[0], 0, 0, 0, QuadratureOrder).Norm;
				};

				AddPlotFunctionLists(dataList, impactParamValues, fieldValue);
			}

			return dataList;
		}

		double[] ProperTimeValues = { 0.5, 1.0, 1.5, 2.0, 2.5, 3.0 };

		private void CreateAverageMagneticFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Average magnetic field strength for bb mesons");
			plotFile.AppendLine("set xlabel \"b (fm)\"");
			plotFile.AppendLine("set ylabel \"<B> (1/fm^2)\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(
				ProperTimeValues, value => "{/Symbol t} = " + value.ToString() + " fm/c");
			AppendPlotCommands(
				plotFile, "linespoints", titleList);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateAverageMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 25, 25);
			dataList.Add(impactParamValues);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			foreach(double properTime in ProperTimeValues)
			{
				PlotFunction fieldValue = impactParam =>
				{
					param.ImpactParameterFm = impactParam;
					param.FormationTimesFm = new double[] { properTime };
					MagneticFieldStrengthAverager avg = new MagneticFieldStrengthAverager(param);

					return avg.CalculateAverageMagneticFieldStrength_LCF(QuadratureOrder);
				};

				AddPlotFunctionLists(dataList, impactParamValues, fieldValue);
			}

			return dataList;
		}

		private void CreateSpinStateOverlapPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"Spin state overlap");
			plotFile.AppendLine("set xlabel \"b (fm)\"");
			plotFile.AppendLine("set ylabel \"|<{/Symbol Y}^+_{/Symbol U}|{/Symbol h}_b>|^2\"");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y \"%g\"");
			plotFile.AppendLine();

			string[] titleList = Array.ConvertAll(
				ProperTimeValues, value => "{/Symbol t} = " + value.ToString() + " fm/c");
			AppendPlotCommands(
				plotFile, "linespoints", titleList);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateSpinStateOverlapDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 25, 25);
			dataList.Add(impactParamValues);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			foreach(double properTime in ProperTimeValues)
			{
				PlotFunction shiftValue = impactParam =>
				{
					param.ImpactParameterFm = impactParam;
					param.FormationTimesFm = new double[] { properTime };
					MagneticFieldStrengthAverager avg = new MagneticFieldStrengthAverager(param);

					return avg.CalculateSpinStateOverlap(QuadratureOrder);
				};

				AddPlotFunctionLists(dataList, impactParamValues, shiftValue);
			}

			return dataList;
		}
	}
}