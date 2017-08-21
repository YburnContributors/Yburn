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

		public Process PlotPointChargeAzimuthalMagneticField()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(
				CreatePointChargeAzimuthalMagneticFieldEffectiveTimeDataList,
				CreatePointChargeAzimuthalMagneticFieldRadialDistanceDataList);
			CreatePointChargeAzimuthalMagneticFieldPlotFile();

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

		public Process PlotPointChargeAndNucleusFieldComponents()
		{
			AssertInputValid_PlotPointChargeField();
			CreateDataFile(
				CreatePointChargeAndNucleusFieldComponentsEffectiveTimeDataList,
				CreatePointChargeAndNucleusFieldComponentsRadialDistanceDataList);
			CreatePointChargeAndNucleusFieldComponentsPlotFile();

			return StartGnuplot();
		}

		public Process PlotNucleusEMFStrengthInLCF()
		{
			CreateDataFile(
				CreateNucleusElectricFieldStrengthInLCFDataList,
				CreateNucleusMagneticFieldStrengthInLCFDataList);
			CreateNucleusEMFStrengthInLCFPlotFile();

			return StartGnuplot();
		}

		public Process PlotCollisionalEMFStrengthVersusTime()
		{
			CreateDataFile(
				CreateCollisionalElectricFieldStrengthVersusTimeDataList,
				CreateCollisionalMagneticFieldStrengthVersusTimeDataList);
			CreateElectromagneticFieldStrengthVersusTimePlotFile();

			return StartGnuplot();
		}

		public Process PlotCollisionalEMFStrengthVersusImpactParam()
		{
			CreateDataFile(
				CreateCollisionalElectricFieldStrengthVersusImpactParameterDataList,
				CreateCollisionalMagneticFieldStrengthVersusImpactParameterDataList);
			CreateElectromagneticFieldStrengthVersusImpactParameterPlotFile();

			return StartGnuplot();
		}

		public Process PlotCollisionalEMFStrengthVersusTimeAndImpactParam()
		{
			CreateDataFile(
				CreateCollisionalElectricFieldStrengthVersusTimeAndImpactParamDataList,
				CreateCollisionalMagneticFieldStrengthVersusTimeAndImpactParamDataList
				);
			CreateElectromagneticFieldStrengthVersusTimeAndImpactParamPlotFile();

			return StartGnuplot();
		}

		public Process PlotEMFStrengthInTransversePlane()
		{
			CreateDataFile(
				CreateElectricFieldStrengthInTransversePlaneDataList,
				CreateMagneticFieldStrengthInTransversePlaneDataList);
			CreateEMFStrengthInTransversePlanePlotFile();

			return StartGnuplot();
		}

		public Process PlotAverageCollisionalEMFStrength()
		{
			CreateDataFile(
				AverageCollisionalElectricFieldStrengthDataList,
				AverageCollisionalMagneticFieldStrengthDataList);
			CreateAverageElectromagneticFieldStrengthPlotFile();

			return StartGnuplot();
		}

		public Process PlotAverageSpinStateOverlap()
		{
			CreateDataFile(
				CreateAverageSpinStateOverlapDataList(BottomiumState.Y1S),
				CreateAverageSpinStateOverlapDataList(BottomiumState.x1P),
				CreateAverageSpinStateOverlapDataList(BottomiumState.Y2S),
				CreateAverageSpinStateOverlapDataList(BottomiumState.x2P)
				);
			CreateAverageSpinStateOverlapPlotFile();

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double EMFNormalization = Constants.ElementaryCharge
			* (Constants.HbarCMeVFm / Constants.RestMassPionMeV)
			* (Constants.HbarCMeVFm / Constants.RestMassPionMeV);

		private static readonly double FixedTime = 0.5;

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

		private string[] EMFCalculationMethodTitleList
		{
			get
			{
				return Enum.GetNames(typeof(EMFCalculationMethod));
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
			ReduceNumberOfColors(plotFile, EMFCalculationMethodTitleList.Length);
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 0,
				style: "lines",
				titles: EMFCalculationMethodTitleList);

			for(int i = 0; i < EMFCalculationMethodTitleList.Length; i++)
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
				+ " at effective time (t - z/v) = " + FixedTime.ToString("G6") + " fm/c'");
			plotFile.AppendLine("set xlabel '{/Symbol r} (fm)'");
			plotFile.AppendLine();
			plotFile.AppendLine("set xrange [:30]");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 1,
				style: "lines",
				titles: EMFCalculationMethodTitleList);

			for(int i = 0; i < EMFCalculationMethodTitleList.Length; i++)
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

			foreach(EMFCalculationMethod method in Enum.GetValues(typeof(EMFCalculationMethod)))
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivity, ParticleRapidity);

				PlotFunction fieldValue = time => EMFNormalization
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

			foreach(EMFCalculationMethod method in Enum.GetValues(typeof(EMFCalculationMethod)))
			{
				PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
					method, QGPConductivity, ParticleRapidity);

				PlotFunction fieldValue = radius => EMFNormalization
					* emf.CalculateElectromagneticField(component, FixedTime, radius);

				AddPlotFunctionLists(dataList, radialDistanceValues, fieldValue);
			}

			return dataList;
		}

		private void CreatePointChargeAzimuthalMagneticFieldPlotFile()
		{
			CreatePointChargeFieldPlotFile("Azimuthal magnetic field", "B_{/Symbol f}");
		}

		private List<List<double>> CreatePointChargeAzimuthalMagneticFieldEffectiveTimeDataList()
		{
			return CreatePointChargeFieldEffectiveTimeDataList(
				EMFComponent.AzimuthalMagneticField);
		}

		private List<List<double>> CreatePointChargeAzimuthalMagneticFieldRadialDistanceDataList()
		{
			return CreatePointChargeFieldRadialDistanceDataList(
				EMFComponent.AzimuthalMagneticField);
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
				"E^Z_{/Symbol r}", "B^Z_{/Symbol f}", "E^Z_z"
			};

			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Field components of a point charge and nucleus"
				+ " with rapidity y = " + ParticleRapidity.ToString("G6")
				+ " at radial distance {/Symbol r} = " + RadialDistance.ToString("G6") + " fm'");
			plotFile.AppendLine("set key maxrows 3");
			plotFile.AppendLine("set xlabel 't - z/v (fm/c)'");
			plotFile.AppendLine("set ylabel 'eE/m_{/Symbol p}^2, eB/m_{/Symbol p}^2'");
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
				+ " at effective time (t - z/v) = " + FixedTime.ToString("G6") + " fm/c'");
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
			Nucleus.CreateNucleusPair(CreateFireballParam(), out nucleusA, out nucleusB);

			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity);
			NucleusElectromagneticField nucEMF = new NucleusElectromagneticField(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity, nucleusA, EMFQuadratureOrder);

			PlotFunction[] plotFunctions = {
				t => EMFNormalization * pcEMF.CalculateRadialElectricField(t, RadialDistance),
				t => EMFNormalization * pcEMF.CalculateAzimuthalMagneticField(t, RadialDistance),
				t => EMFNormalization * pcEMF.CalculateLongitudinalElectricField(t, RadialDistance),
				t => EMFNormalization * nucEMF.CalculateRadialElectricField(t, RadialDistance),
				t => EMFNormalization * nucEMF.CalculateAzimuthalMagneticField(t, RadialDistance),
				t => EMFNormalization * nucEMF.CalculateLongitudinalElectricField(t, RadialDistance)
			};

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
			Nucleus.CreateNucleusPair(CreateFireballParam(), out nucleusA, out nucleusB);

			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity);
			NucleusElectromagneticField nucEMF = new NucleusElectromagneticField(
				EMFCalculationMethod, QGPConductivity, ParticleRapidity, nucleusA, EMFQuadratureOrder);

			PlotFunction[] plotFunctions = {
				r => EMFNormalization * pcEMF.CalculateRadialElectricField(FixedTime, r),
				r => EMFNormalization * pcEMF.CalculateAzimuthalMagneticField(FixedTime, r),
				r => EMFNormalization * pcEMF.CalculateLongitudinalElectricField(FixedTime, r),
				r => EMFNormalization * nucEMF.CalculateRadialElectricField(FixedTime, r),
				r => EMFNormalization * nucEMF.CalculateAzimuthalMagneticField(FixedTime, r),
				r => EMFNormalization * nucEMF.CalculateLongitudinalElectricField(FixedTime, r)
			};

			foreach(PlotFunction function in plotFunctions)
			{
				AddPlotFunctionLists(dataList, radialDistanceValues, function);
			}

			return dataList;
		}

		private void CreateNucleusEMFStrengthInLCFPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Single nucleus electric field strength in LCF'");
			plotFile.AppendLine("set xlabel '{/Symbol q}'");
			plotFile.AppendLine("set ylabel '{/Symbol r} (fm)'");
			plotFile.AppendLine("set cblabel 'e|E|/m_{/Symbol p}^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 0);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Single nucleus magnetic field strength in LCF'");
			plotFile.AppendLine("set cblabel 'e|B|/m_{/Symbol p}^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 1);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateNucleusElectricFieldStrengthInLCFDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			FireballParam param = CreateFireballParam();

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
					ParticleRapidity,
					nucleusA,
					EMFQuadratureOrder);

				return EMFNormalization * emf.CalculateElectricFieldPerFm2_LCF(
					FixedTime, radialDistance, 0, rapidity).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, rapidityValues, radialDistanceValues, function);

			return dataList;
		}

		private List<List<double>> CreateNucleusMagneticFieldStrengthInLCFDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			FireballParam param = CreateFireballParam();

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
					ParticleRapidity,
					nucleusA,
					EMFQuadratureOrder);

				return EMFNormalization * emf.CalculateMagneticFieldPerFm2_LCF(
					FixedTime, radialDistance, 0, rapidity).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, rapidityValues, radialDistanceValues, function);

			return dataList;
		}

		private void CreateElectromagneticFieldStrengthVersusTimePlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Collisional electric field strength at (0,R,0)"
				+ " for impact parameter b = " + ImpactParameterFm + " fm'");
			plotFile.AppendLine("set xlabel 't (fm/c)'");
			plotFile.AppendLine("set ylabel 'e|E|/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale xy 10");
			plotFile.AppendLine("set format xy '%g'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 0,
				style: "lines",
				titles: EMFCalculationMethodTitleList);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Collisional magnetic field strength at (0,0,0)"
				+ " for impact parameter b = " + ImpactParameterFm + " fm'");
			plotFile.AppendLine("set ylabel 'e|B|/m_{/Symbol p}^2'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 1,
				style: "lines",
				titles: EMFCalculationMethodTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateCollisionalElectricFieldStrengthVersusTimeDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetLogarithmicAbscissaList(StartTime, StopTime, Samples);
			dataList.Add(timeValues);

			FireballParam param = CreateFireballParam();

			double x = 0;
			double y = 0.5 * (NuclearRadiusAFm + NuclearRadiusBFm);
			double z = 0;

			foreach(EMFCalculationMethod method in Enum.GetValues(typeof(EMFCalculationMethod)))
			{
				param.EMFCalculationMethod = method;

				CollisionalElectromagneticField emf
					= new CollisionalElectromagneticField(param);

				PlotFunction fieldValue = time => EMFNormalization
					* emf.CalculateElectricFieldPerFm2(time, x, y, z).Norm;

				AddPlotFunctionLists(dataList, timeValues, fieldValue);
			}

			return dataList;
		}

		private List<List<double>> CreateCollisionalMagneticFieldStrengthVersusTimeDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetLogarithmicAbscissaList(StartTime, StopTime, Samples);
			dataList.Add(timeValues);

			FireballParam param = CreateFireballParam();

			double x = 0;
			double y = 0;
			double z = 0;

			foreach(EMFCalculationMethod method in Enum.GetValues(typeof(EMFCalculationMethod)))
			{
				param.EMFCalculationMethod = method;

				CollisionalElectromagneticField emf
					= new CollisionalElectromagneticField(param);

				PlotFunction fieldValue = time => EMFNormalization
					* emf.CalculateMagneticFieldPerFm2(time, x, y, z).Norm;

				AddPlotFunctionLists(dataList, timeValues, fieldValue);
			}

			return dataList;
		}

		private void CreateElectromagneticFieldStrengthVersusImpactParameterPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Collisional electric field strength at (0,R,0)"
				+ " at time t = " + FixedTime + " fm/c'");
			plotFile.AppendLine("set xlabel 'b (fm)'");
			plotFile.AppendLine("set ylabel 'e|E|/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale y 10");
			plotFile.AppendLine("set format y '%g'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 0,
				style: "lines",
				titles: EMFCalculationMethodTitleList);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Collisional magnetic field strength at (0,0,0)"
				+ " at time t = " + FixedTime + " fm/c'");
			plotFile.AppendLine("set ylabel 'e|B|/m_{/Symbol p}^2'");
			plotFile.AppendLine();

			AppendPlotCommands(
				plotFile,
				index: 1,
				style: "lines",
				titles: EMFCalculationMethodTitleList);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateCollisionalElectricFieldStrengthVersusImpactParameterDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);
			dataList.Add(impactParamValues);

			FireballParam param = CreateFireballParam();

			double x = 0;
			double y = 0.5 * (NuclearRadiusAFm + NuclearRadiusBFm);
			double z = 0;

			foreach(EMFCalculationMethod method in Enum.GetValues(typeof(EMFCalculationMethod)))
			{
				param.EMFCalculationMethod = method;

				PlotFunction fieldValue = impactParam =>
				{
					param.ImpactParameterFm = impactParam;
					CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

					return EMFNormalization * emf.CalculateElectricFieldPerFm2(FixedTime, x, y, z).Norm;
				};

				AddPlotFunctionLists(dataList, impactParamValues, fieldValue);
			}

			return dataList;
		}

		private List<List<double>> CreateCollisionalMagneticFieldStrengthVersusImpactParameterDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);
			dataList.Add(impactParamValues);

			FireballParam param = CreateFireballParam();

			double x = 0;
			double y = 0;
			double z = 0;

			foreach(EMFCalculationMethod method in Enum.GetValues(typeof(EMFCalculationMethod)))
			{
				param.EMFCalculationMethod = method;

				PlotFunction fieldValue = impactParam =>
				{
					param.ImpactParameterFm = impactParam;
					CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

					return EMFNormalization * emf.CalculateMagneticFieldPerFm2(FixedTime, x, y, z).Norm;
				};

				AddPlotFunctionLists(dataList, impactParamValues, fieldValue);
			}

			return dataList;
		}

		private void CreateElectromagneticFieldStrengthVersusTimeAndImpactParamPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Collisional electric field strength at (0,R,0)'");
			plotFile.AppendLine("set xlabel 't (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel 'e|E|/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale x 10");
			plotFile.AppendLine("set format x '%g'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 0);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Collisional magnetic field strength at (0,0,0)'");
			plotFile.AppendLine("set cblabel 'e|B|/m_{/Symbol p}^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 1);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateCollisionalElectricFieldStrengthVersusTimeAndImpactParamDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetLogarithmicAbscissaList(StartTime, StopTime, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam();

			double x = 0;
			double y = 0.5 * (NuclearRadiusAFm + NuclearRadiusBFm);
			double z = 0;

			SurfacePlotFunction function = (time, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				return EMFNormalization * emf.CalculateElectricFieldPerFm2(time, x, y, z).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, timeValues, impactParamValues, function);

			return dataList;
		}

		private List<List<double>> CreateCollisionalMagneticFieldStrengthVersusTimeAndImpactParamDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> timeValues = GetLogarithmicAbscissaList(StartTime, StopTime, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam();

			double x = 0;
			double y = 0;
			double z = 0;

			SurfacePlotFunction function = (time, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				return EMFNormalization * emf.CalculateMagneticFieldPerFm2(time, x, y, z).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, timeValues, impactParamValues, function);

			return dataList;
		}

		private void CreateEMFStrengthInTransversePlanePlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 650,600 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Electric field strength in the z = 0 plane"
				+ " at time t = " + FixedTime.ToString("G6") + " fm/c"
				+ " and impact parameter b = " + ImpactParameterFm + " fm'");
			plotFile.AppendLine("set xlabel 'x (fm)'");
			plotFile.AppendLine("set ylabel 'y (fm)'");
			plotFile.AppendLine("set cblabel 'e|E|/m_{/Symbol p}^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 0);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 650,600 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Magnetic field strength in the z = 0 plane"
				+ " at time t = " + FixedTime.ToString("G6") + " fm/c"
				+ " and impact parameter b = " + ImpactParameterFm + " fm'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 1);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateElectricFieldStrengthInTransversePlaneDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> gridValues = GetLinearAbscissaList(-GridRadiusFm, GridRadiusFm, Samples);

			FireballParam param = CreateFireballParam();

			SurfacePlotFunction function = (x, y) =>
			{
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				return EMFNormalization * emf.CalculateElectricFieldPerFm2(FixedTime, x, y, 0).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, gridValues, gridValues, function);

			return dataList;
		}

		private List<List<double>> CreateMagneticFieldStrengthInTransversePlaneDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> gridValues = GetLinearAbscissaList(-GridRadiusFm, GridRadiusFm, Samples);

			FireballParam param = CreateFireballParam();

			SurfacePlotFunction function = (x, y) =>
			{
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				return EMFNormalization * emf.CalculateMagneticFieldPerFm2(FixedTime, x, y, 0).Norm;
			};

			AddSurfacePlotFunctionLists(dataList, gridValues, gridValues, function);

			return dataList;
		}

		private void CreateAverageElectromagneticFieldStrengthPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 750,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Average electric field strength for bb mesons'");
			plotFile.AppendLine("set xlabel '{/Symbol t} (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel 'e<|E|>/m_{/Symbol p}^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale x 10");
			plotFile.AppendLine("set format x '%g'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 0);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 750,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Average magnetic field strength for bb mesons'");
			plotFile.AppendLine("set cblabel 'e<|B|>/m_{/Symbol p}^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 1);

			WritePlotFile(plotFile);
		}

		private List<List<double>> AverageCollisionalElectricFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> properTimeValues = GetLogarithmicAbscissaList(0.1, 10, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam();

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				return EMFNormalization * emf.CalculateAverageElectricFieldStrengthPerFm2(properTime);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

			return dataList;
		}

		private List<List<double>> AverageCollisionalMagneticFieldStrengthDataList()
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> properTimeValues = GetLogarithmicAbscissaList(0.1, 10, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam();

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				return EMFNormalization * emf.CalculateAverageMagneticFieldStrengthPerFm2(properTime);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

			return dataList;
		}

		private void CreateAverageSpinStateOverlapPlotFile()
		{
			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,500 0");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Spin state overlap for N = 1, L = 0'");
			plotFile.AppendLine("set xlabel '{/Symbol t} (fm/c)'");
			plotFile.AppendLine("set ylabel 'b (fm)'");
			plotFile.AppendLine("set cblabel '|<{/Symbol U}^0(1S)(B)|{/Symbol h}_b^0(1S)(0)>|^2'");
			plotFile.AppendLine();
			plotFile.AppendLine("set logscale x 10");
			plotFile.AppendLine("set format x '%g'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 0);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 1000,500 1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Spin state overlap for N = 2, L = 1'");
			plotFile.AppendLine("set cblabel '|<{/Symbol c}_b^0(1P)(B)|h_b^0(1P)(0)>|^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 1);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 1000,500 2");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Spin state overlap for N = 2, L = 0'");
			plotFile.AppendLine("set cblabel '|<{/Symbol U}^0(2S)(B)|{/Symbol h}_b^0(2S)(0)>|^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 2);

			plotFile.AppendLine();
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal windows enhanced size 1000,500 3");
			plotFile.AppendLine();
			plotFile.AppendLine("set title 'Spin state overlap for N = 3, L = 1'");
			plotFile.AppendLine("set cblabel '|<{/Symbol c}_b^0(2P)(B)|h_b^0(2P)(0)>|^2'");
			plotFile.AppendLine();

			AppendSurfacePlotCommands(plotFile, index: 3);

			WritePlotFile(plotFile);
		}

		private List<List<double>> CreateAverageSpinStateOverlapDataList(
			BottomiumState tripletState
			)
		{
			List<List<double>> dataList = new List<List<double>>();

			List<double> properTimeValues = GetLogarithmicAbscissaList(0.1, 10, Samples);
			List<double> impactParamValues = GetLinearAbscissaList(0, 25, Samples);

			FireballParam param = CreateFireballParam();

			SurfacePlotFunction function = (properTime, impactParam) =>
			{
				param.ImpactParameterFm = impactParam;

				return CalculateAverageSpinStateOverlap(tripletState, properTime);
			};

			AddSurfacePlotFunctionLists(dataList, properTimeValues, impactParamValues, function);

			return dataList;
		}
	}
}
