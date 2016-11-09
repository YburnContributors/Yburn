using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Yburn.Fireball;
using Yburn.FormatUtil;
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

		public Process PlotNucleusMagneticFieldStrengthInLCF()
		{
			CreateDataFile(CreateNucleusMagneticFieldStrengthInLCFDataList);
			CreateNucleusMagneticFieldStrengthInLCFPlotFile();

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
			* (Constants.HbarCMeVFm / Constants.RestMassPionMeV)
			* (Constants.HbarCMeVFm / Constants.RestMassPionMeV);

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
			if(Samples < 1)
			{
				throw new Exception("Samples < 1.");
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
				return EMFCalculationMethodSelection.ConvertAll(
					method => method.ToUIString()).ToArray();
			}
		}

		private void CreatePointChargeAzimutalMagneticFieldPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Azimutal magnetic field of a point charge"
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm'");
			plotFile.AppendLine("set xlabel 't - z/v (fm/c)'");
			plotFile.AppendLine("set ylabel 'eH_{/Symbol f}/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y '%g'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				style: "lines",
				titles: EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeAzimutalMagneticFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, Samples);

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
			plotFile.AppendLine("set title 'Longitudinal electric field of a point charge"
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm'");
			plotFile.AppendLine("set xlabel 't - z/v (fm/c)'");
			plotFile.AppendLine("set ylabel 'e|E_{z}|/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y '%g'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				style: "lines",
				titles: EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeLongitudinalElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, Samples);

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
			plotFile.AppendLine("set title 'Radial electric field of a point charge"
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm'");
			plotFile.AppendLine("set xlabel 't - z/v (fm/c)'");
			plotFile.AppendLine("set ylabel 'eE_{r}/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y '%g'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				style: "lines",
				titles: EMFCalculationMethodSelectionTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeRadialElectricFieldDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, Samples);

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
			plotFile.AppendLine("set title 'Field components of a point charge and nucleus"
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm'");
			plotFile.AppendLine("set xlabel 't - z/v (fm/c)'");
			plotFile.AppendLine("set ylabel '|E|, |B|'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y '%g'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 0,
				style: "lines",
				titles: titleList);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Field components of a point charge and nucleus"
				+ " with rapidity y = " + PointChargeRapidity.ToString("G6")
				+ " at effective time (t - z/v) = " + RadialDistance.ToString("G6") + " fm/c'");
			plotFile.AppendLine("set xlabel '{/Symbol r} (fm)'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 1,
				style: "lines",
				titles: titleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeAndNucleusFieldComponentsEffectiveTimeDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLinearAbscissaList(
				StartEffectiveTime, StopEffectiveTime, Samples);

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

			List<double> radialDistanceValues = GetLinearAbscissaList(0, 25, Samples);

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

		private void CreateNucleusMagneticFieldStrengthInLCFPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Single nucleus magnetic field strength in LCF'");
			plotFile.AppendLine("set xlabel '{/Symbol q}'");
			plotFile.AppendLine("set ylabel '{/Symbol r} (fm)'");
			plotFile.AppendLine("set cblabel '|B| (1/fm^2)'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateNucleusMagneticFieldStrengthInLCFDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			List<double> rapidityValues = GetLinearAbscissaList(-8, 8, Samples);
			List<double> radialDistanceValues = GetLinearAbscissaList(0, 25, Samples);

			SurfacePlotFunction function = (rapidity, radialDistance) =>
			{
				Nucleus nucleusA;
				Nucleus nucleusB;
				Nucleus.CreateNucleusPair(
					param, out nucleusA, out nucleusB);

				NucleusElectromagneticField emf = new NucleusElectromagneticField(
					param.EMFCalculationMethod.Value,
					param.QGPConductivityMeV.Value,
					param.BeamRapidity.Value,
					nucleusA);

				return emf.CalculateMagneticFieldPerFm2_LCF(
					0.4,
					radialDistance,
					0,
					rapidity,
					QuadratureOrder).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, rapidityValues, radialDistanceValues, function);

			return dataList;
		}

		private void CreateCentralMagneticFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Central magnetic field strength'");
			plotFile.AppendLine("set xlabel 't (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel '|B(0,0,0)| (1/fm^2)'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateCentralMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);
			List<double> timeValues = GetLinearAbscissaList(0, 1, Samples);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			SurfacePlotFunction function = (time, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				FireballElectromagneticField emf =
					new FireballElectromagneticField(param);

				return emf.CalculateMagneticFieldPerFm2(time, 0, 0, 0, QuadratureOrder).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, timeValues, impactParamValues, function);

			return dataList;
		}

		private void CreateAverageMagneticFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Average magnetic field strength for bb mesons'");
			plotFile.AppendLine("set xlabel '{/Symbol t} (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel '<|B|> (1/fm^2)'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateAverageMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);
			List<double> properTimeValues = GetLinearAbscissaList(0, 1, Samples);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				LCFFieldAverager avg =
					new LCFFieldAverager(param);

				return avg.CalculateAverageMagneticFieldStrengthPerFm2(
					properTime, QuadratureOrder);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

			return dataList;
		}

		private void CreateSpinStateOverlapPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Spin state overlap'");
			plotFile.AppendLine("set xlabel '{/Symbol t} (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel '|<{/Symbol Y}^+_{/Symbol U}(1S)|{/Symbol h}_b(1S)>|^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateSpinStateOverlapDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);
			List<double> properTimeValues = GetLinearAbscissaList(0, 1, Samples);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				LCFFieldAverager avg =
					new LCFFieldAverager(param);

				return avg.CalculateAverageSpinStateOverlap(properTime, QuadratureOrder);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

			return dataList;
		}
	}
}