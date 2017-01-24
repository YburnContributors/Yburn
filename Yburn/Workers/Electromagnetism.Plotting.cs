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
			CreateDataFile(
				CreatePointChargeAzimutalMagneticFieldEffectiveTimeDataList,
				CreatePointChargeAzimutalMagneticFieldRadialDistanceDataList);
			CreatePointChargeAzimutalMagneticFieldPlotFile();

			return StartGnuplot();
		}

		public Process PlotPointChargeLongitudinalElectricField()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(
				CreatePointChargeLongitudinalElectricFieldEffectiveTimeDataList,
				CreatePointChargeLongitudinalElectricFieldRadialDistanceDataList);
			CreatePointChargeLongitudinalElectricFieldPlotFile();

			return StartGnuplot();
		}

		public Process PlotPointChargeRadialElectricField()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(
				CreatePointChargeRadialElectricFieldEffectiveTimeDataList,
				CreatePointChargeRadialElectricFieldRadialDistanceDataList);
			CreatePointChargeRadialElectricFieldPlotFile();

			return StartGnuplot();
		}

		public Process PlotPointChargeAndNucleusFields()
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

		public Process PlotAverageElectricFieldStrength()
		{
			CreateDataFile(CreateAverageElectricFieldStrengthDataList);
			CreateAverageElectricFieldStrengthPlotFile();

			return StartGnuplot();
		}

		public Process PlotAverageMagneticFieldStrength()
		{
			CreateDataFile(CreateAverageMagneticFieldStrengthDataList);
			CreateAverageMagneticFieldStrengthPlotFile();

			return StartGnuplot();
		}

		public Process PlotAverageSpinStateOverlap()
		{
			CreateDataFile(CreateAverageSpinStateOverlapDataList);
			CreateAverageSpinStateOverlapPlotFile();

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double EMFieldNormalization = Constants.ElementaryCharge
			* (Constants.HbarCMeVFm / Constants.RestMassPionMeV)
			* (Constants.HbarCMeVFm / Constants.RestMassPionMeV);

		private static readonly double FormationTime = 0.4;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void AssertInputValid_PlotPointChargeField()
		{
			if(RadialDistance < 0)
			{
				throw new Exception("RadialDistance < 0.");
			}
			if(StartTime < 0)
			{
				throw new Exception("StartTime < 0.");
			}
			if(StopTime <= StartTime)
			{
				throw new Exception("StopTime <= StartTime.");
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

		private void CreatePointChargeFieldPlotFile(
			string fieldName,
			string fieldSymbol
			)
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title '" + fieldName + " of a point charge"
				+ " with rapidity y = " + ParticleRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm'");
			plotFile.AppendLine("set xlabel 't - z/v (fm/c)'");
			plotFile.AppendLine("set ylabel 'e" + fieldSymbol + "/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale xy 10");
			plotFile.AppendLine("set format xy '%g'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, EMFCalculationMethodSelectionTitleList.Length);
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 0,
				style: "lines",
				titles: EMFCalculationMethodSelectionTitleList);

			for(int i = 0; i < EMFCalculationMethodSelectionTitleList.Length; i++)
			{
				plotFile.AppendFormat(
					"'' index 0 using 1:(-${0}) with lines dashtype '-' notitle,\\", i + 2);
				plotFile.AppendLine();
			}

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title '" + fieldName + " of a point charge"
				+ " with rapidity y = " + ParticleRapidity.ToString("G6")
				+ " at effective time (t - z/v) = " + FormationTime.ToString("G6") + " fm/c'");
			plotFile.AppendLine("set xlabel '{/Symbol r} (fm)'");
			plotFile.AppendLine();
			plotFile.AppendLine("set xrange [:30]");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 1,
				style: "lines",
				titles: EMFCalculationMethodSelectionTitleList);

			for(int i = 0; i < EMFCalculationMethodSelectionTitleList.Length; i++)
			{
				plotFile.AppendFormat(
					"'' index 1 using 1:(-${0}) with lines dashtype '-' notitle,\\", i + 2);
				plotFile.AppendLine();
			}

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeFieldEffectiveTimeDataList(
			EMFComponent component
			)
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLogarithmicAbscissaList(
				StartTime, StopTime, Samples);
			dataList.Add(effectiveTimeValues);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivity, ParticleRapidity);

				PlotFunction fieldValue = time => EMFieldNormalization
					* emf.CalculateElectromagneticField(component, time, RadialDistance);

				AddPlotFunctionLists(dataList, effectiveTimeValues, fieldValue);
			}

			return dataList;
		}

		private List<List<double>> CreatePointChargeFieldRadialDistanceDataList(
			EMFComponent component
			)
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> radialDistanceValues = GetLogarithmicAbscissaList(0.1, 100, Samples);
			dataList.Add(radialDistanceValues);

			foreach(EMFCalculationMethod method in EMFCalculationMethodSelection)
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivity, ParticleRapidity);

				PlotFunction fieldValue = radius => EMFieldNormalization
					* emf.CalculateElectromagneticField(component, FormationTime, radius);

				AddPlotFunctionLists(dataList, radialDistanceValues, fieldValue);
			}

			return dataList;
		}

		private void CreatePointChargeAzimutalMagneticFieldPlotFile()
		{
			CreatePointChargeFieldPlotFile("Azimutal magnetic field", "H_{/Symbol f}");
		}

		private List<List<double>> CreatePointChargeAzimutalMagneticFieldEffectiveTimeDataList()
		{
			return CreatePointChargeFieldEffectiveTimeDataList(
				EMFComponent.AzimutalMagneticField);
		}

		private List<List<double>> CreatePointChargeAzimutalMagneticFieldRadialDistanceDataList()
		{
			return CreatePointChargeFieldRadialDistanceDataList(
				EMFComponent.AzimutalMagneticField);
		}

		private void CreatePointChargeLongitudinalElectricFieldPlotFile()
		{
			CreatePointChargeFieldPlotFile("Longitudinal electric field", "E_{z}");
		}

		private List<List<double>> CreatePointChargeLongitudinalElectricFieldEffectiveTimeDataList()
		{
			return CreatePointChargeFieldEffectiveTimeDataList(
				EMFComponent.LongitudinalElectricField);
		}

		private List<List<double>> CreatePointChargeLongitudinalElectricFieldRadialDistanceDataList()
		{
			return CreatePointChargeFieldRadialDistanceDataList(
				EMFComponent.LongitudinalElectricField);
		}

		private void CreatePointChargeRadialElectricFieldPlotFile()
		{
			CreatePointChargeFieldPlotFile("Radial electric field", "E_{/Symbol r}");
		}

		private List<List<double>> CreatePointChargeRadialElectricFieldEffectiveTimeDataList()
		{
			return CreatePointChargeFieldEffectiveTimeDataList(EMFComponent.RadialElectricField);
		}

		private List<List<double>> CreatePointChargeRadialElectricFieldRadialDistanceDataList()
		{
			return CreatePointChargeFieldRadialDistanceDataList(EMFComponent.RadialElectricField);
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
				+ " with rapidity y = " + ParticleRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm'");
			plotFile.AppendLine("set xlabel 't - z/v (fm/c)'");
			plotFile.AppendLine("set ylabel '|E|, |B|'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale xy 10");
			plotFile.AppendLine("set format xy '%g'");
			plotFile.AppendLine();
			ReduceNumberOfColors(plotFile, titleList.Length);
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 0,
				style: "lines",
				titles: titleList);

			for(int i = 0; i < titleList.Length; i++)
			{
				plotFile.AppendFormat(
					"'' index 0 using 1:(-${0}) with lines dashtype '-' notitle,\\", i + 2);
				plotFile.AppendLine();
			}

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Field components of a point charge and nucleus"
				+ " with rapidity y = " + ParticleRapidity.ToString("G6")
				+ " at effective time (t - z/v) = " + FormationTime.ToString("G6") + " fm/c'");
			plotFile.AppendLine("set xlabel '{/Symbol r} (fm)'");
			plotFile.AppendLine();
			plotFile.AppendLine("set xrange [:30]");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 1,
				style: "lines",
				titles: titleList);

			for(int i = 0; i < titleList.Length; i++)
			{
				plotFile.AppendFormat(
					"'' index 1 using 1:(-${0}) with lines dashtype '-' notitle,\\", i + 2);
				plotFile.AppendLine();
			}

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreatePointChargeAndNucleusFieldComponentsEffectiveTimeDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> effectiveTimeValues = GetLogarithmicAbscissaList(
				StartTime, StopTime, Samples);
			dataList.Add(effectiveTimeValues);

			Nucleus nucleusA;
			Nucleus nucleusB;
			Nucleus.CreateNucleusPair(
				CreateFireballParam(EMFCalculationMethod), out nucleusA, out nucleusB);

			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity);
			NucleusElectromagneticField nucEMF = new NucleusElectromagneticField(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity, nucleusA, EMFQuadratureOrder);

			PlotFunction[] plotFunctions = {
				t => pcEMF.CalculateRadialElectricField(t, RadialDistance),
				t => pcEMF.CalculateAzimutalMagneticField(t, RadialDistance),
				t => pcEMF.CalculateLongitudinalElectricField(t, RadialDistance),
				t => nucEMF.CalculateRadialElectricField(t, RadialDistance),
				t => nucEMF.CalculateAzimutalMagneticField(t, RadialDistance),
				t => nucEMF.CalculateLongitudinalElectricField(t, RadialDistance) };

			foreach(PlotFunction function in plotFunctions)
			{
				AddPlotFunctionLists(dataList, effectiveTimeValues, function);
			}

			return dataList;
		}

		private List<List<double>> CreatePointChargeAndNucleusFieldComponentsRadialDistanceDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> radialDistanceValues = GetLogarithmicAbscissaList(0.1, 100, Samples);
			dataList.Add(radialDistanceValues);

			Nucleus nucleusA;
			Nucleus nucleusB;
			Nucleus.CreateNucleusPair(
				CreateFireballParam(EMFCalculationMethod), out nucleusA, out nucleusB);

			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity);
			NucleusElectromagneticField nucEMF = new NucleusElectromagneticField(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity, nucleusA, EMFQuadratureOrder);

			double effectiveTime = FormationTime;

			PlotFunction[] plotFunctions = {
				r => pcEMF.CalculateRadialElectricField(effectiveTime, r),
				r => pcEMF.CalculateAzimutalMagneticField(effectiveTime, r),
				r => Math.Abs(pcEMF.CalculateLongitudinalElectricField(effectiveTime, r)),
				r => nucEMF.CalculateRadialElectricField(effectiveTime, r),
				r => nucEMF.CalculateAzimutalMagneticField(effectiveTime, r),
				r => Math.Abs(nucEMF.CalculateLongitudinalElectricField(effectiveTime, r)) };

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
					param.EMFCalculationMethod,
					param.QGPConductivityMeV,
					param.BeamRapidity,
					nucleusA,
					EMFQuadratureOrder);

				return emf.CalculateMagneticFieldPerFm2_LCF(
					FormationTime, radialDistance, 0, rapidity).Norm;
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
			plotFile.AppendLine("set xrange [0.0099:]");
			plotFile.AppendLine("set logscale x 10");
			plotFile.AppendLine("set format x '%g'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateCentralMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetLogarithmicAbscissaList(0.01, 10, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			SurfacePlotFunction function = (time, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				return emf.CalculateMagneticFieldPerFm2(time, 0, 0, 0).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, timeValues, impactParamValues, function);

			return dataList;
		}

		private void CreateAverageElectricFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Average Electric field strength for bb mesons'");
			plotFile.AppendLine("set xlabel '{/Symbol t} (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel '<|E|> (1/fm^2)'");
			plotFile.AppendLine();
			plotFile.AppendLine("set xrange [0.0099:]");
			plotFile.AppendLine("set logscale x 10");
			plotFile.AppendLine("set format x '%g'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateAverageElectricFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> properTimeValues = GetLogarithmicAbscissaList(0.01, 10, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				LCFFieldAverager avg = new LCFFieldAverager(param);

				return avg.CalculateAverageElectricFieldStrengthPerFm2(properTime);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

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
			plotFile.AppendLine("set xrange [0.0099:]");
			plotFile.AppendLine("set logscale x 10");
			plotFile.AppendLine("set format x '%g'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateAverageMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> properTimeValues = GetLogarithmicAbscissaList(0.01, 10, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				LCFFieldAverager avg = new LCFFieldAverager(param);

				return avg.CalculateAverageMagneticFieldStrengthPerFm2(properTime);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

			return dataList;
		}

		private void CreateAverageSpinStateOverlapPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Spin state overlap'");
			plotFile.AppendLine("set xlabel '{/Symbol t} (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel '|<{/Symbol Y}_t^0(1S)|{/Symbol h}_b(1S)>|^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set xrange [0.0099:]");
			plotFile.AppendLine("set logscale x 10");
			plotFile.AppendLine("set format x '%g'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile);

			AppendSavePlotAsPNG(plotFile);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateAverageSpinStateOverlapDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> properTimeValues = GetLogarithmicAbscissaList(0.01, 10, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam(EMFCalculationMethod.DiffusionApproximation);

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				LCFFieldAverager avg = new LCFFieldAverager(param);

				return avg.CalculateAverageSpinStateOverlap(BottomiumState.Y1S, properTime);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

			return dataList;
		}
	}
}
