using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class QGPSuppressionTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			CancellationTokenSource = new CancellationTokenSource();
			CancellationToken = CancellationTokenSource.Token;
		}

		[TestMethod]
		public void ConvertRaggedToFlatImpactParamsArray()
		{
			QGPSuppression suppression = new QGPSuppression(
				new FireballParam(), NumberCentralityBins, ImpactParamsAtBinBoundaries, CancellationToken);

			List<double> flatImpactParams = suppression.FlatImpactParamsAtBinBoundaries;

			AssertCorrectFlatImpactParamsArray(flatImpactParams);
		}

		[TestMethod]
		public void CalculateQGPSuppressionFactors_PbPb()
		{
			QGPSuppression suppression = new QGPSuppression(
				CreateFireballParam_PbPb(),
				new List<int> { 8 },
				new List<List<double>> { new List<double> { 0, 3.2, 4.4, 6.8, 8.4, 9.6, 10.8, 12, 21.2 } },
				CancellationToken);

			BottomiumVector[][][] suppressionFactors = suppression.CalculateQGPSuppressionFactors();

			AssertCorrectQGPSuppressionFactors_PbPb(suppressionFactors);
		}

		[TestMethod]
		public void CalculateQGPSuppressionFactors_pPb()
		{
			QGPSuppression suppression = new QGPSuppression(
				CreateFireballParam_pPb(),
				new List<int> { 7 },
				new List<List<double>> { new List<double> { 0, 1.6, 2.4, 3.6, 4.4, 5.0, 5.6, 14.0 } },
				CancellationToken);

			BottomiumVector[][][] suppressionFactors = suppression.CalculateQGPSuppressionFactors();

			AssertCorrectQGPSuppressionFactors_pPb(suppressionFactors);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly List<int> NumberCentralityBins = new List<int> { 5, 6 };

		private static readonly List<List<double>> ImpactParamsAtBinBoundaries
			= new List<List<double>> {
				new List<double> { 3.2, 4.4, 8.4, 9.6, 10.8, 21.2 },
				new List<double> { 0, 4.4, 6.8, 8.4, 9.6, 10.8, 12 }};

		private static void AssertCorrectFlatImpactParamsArray(
			List<double> flatImpactParams
			)
		{
			AssertHelper.AssertApproximatelyEqual(0, flatImpactParams[0]);
			AssertHelper.AssertApproximatelyEqual(3.2, flatImpactParams[1]);
			AssertHelper.AssertApproximatelyEqual(4.4, flatImpactParams[2]);
			AssertHelper.AssertApproximatelyEqual(6.8, flatImpactParams[3]);
			AssertHelper.AssertApproximatelyEqual(8.4, flatImpactParams[4]);
			AssertHelper.AssertApproximatelyEqual(9.6, flatImpactParams[5]);
			AssertHelper.AssertApproximatelyEqual(10.8, flatImpactParams[6]);
			AssertHelper.AssertApproximatelyEqual(12, flatImpactParams[7]);
			AssertHelper.AssertApproximatelyEqual(21.2, flatImpactParams[8]);
		}

		private static Dictionary<BottomiumState, double> FormationTimes
		{
			get
			{
				Dictionary<BottomiumState, double> formationTimes
					= new Dictionary<BottomiumState, double>();

				foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
				{
					formationTimes.Add(state, 0.4);
				}

				return formationTimes;
			}
		}

		private static FireballParam CreateFireballParam_PbPb()
		{
			FireballParam param = new FireballParam();

			param.BreakupTemperatureMeV = 160;
			param.DecayWidthRetrievalFunction = DummyDecayWidthProvider.GetDummyDecayWidth;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.ExpansionMode = ExpansionMode.Transverse;
			param.FormationTimesFm = FormationTimes;
			param.GridCellSizeFm = 1;
			param.GridRadiusFm = 10;
			param.InelasticppCrossSectionFm = 6.4;
			param.InitialMaximumTemperatureMeV = 550;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;
			param.ThermalTimeFm = 0.1;
			param.TransverseMomentaGeV = new List<double> { 6 };
			param.UseElectromagneticFields = false;

			return param;
		}

		private static FireballParam CreateFireballParam_pPb()
		{
			FireballParam param = CreateFireballParam_PbPb();

			param.DiffusenessBFm = 0;
			param.GridCellSizeFm = 0.5;
			param.GridRadiusFm = 5;
			param.InelasticppCrossSectionFm = 6.8;
			param.NuclearRadiusBFm = 0.8775;
			param.NucleonNumberB = 1;
			param.NucleusShapeB = NucleusShape.GaussianDistribution;
			param.ProtonNumberB = 1;
			param.TemperatureProfile = TemperatureProfile.NmixALICE13;

			return param;
		}

		private static void AssertCorrectQGPSuppressionFactors_PbPb(
			BottomiumVector[][][] suppressionFactors
			)
		{
			double delta = 1e-15;

			Assert.AreEqual(0.586626174521954, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.204151275047942, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.211263852989795, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.076005447291281, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.075856419798820, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.032699584075028, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.613388678135476, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.227368852789128, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.234643100860244, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.088424354111899, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.088153601561521, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.045800187761444, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.652970461046764, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.275831167270236, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.281226564855334, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.107771816024975, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.107266638934064, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.042789497081523, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.726275064280610, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.374313291658318, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.379417189949000, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.152932686622931, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.152529350961211, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.069632159510619, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.801390031120177, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.492586640953676, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.497741308757423, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.265631710111936, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.264550152394591, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.103652184224175, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.856808215847959, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.602033600785376, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.606858548537993, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.356732501947880, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.356323009722294, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.144812498236678, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.915048776996274, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.741526641242698, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.745091114081688, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.566675293834111, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.564323400718734, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.289146990984308, suppressionFactors[0][6][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.980191693809096, suppressionFactors[0][7][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.932299761227210, suppressionFactors[0][7][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.933034859326331, suppressionFactors[0][7][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.882625506326560, suppressionFactors[0][7][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.881633742794585, suppressionFactors[0][7][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.606532238535875, suppressionFactors[0][7][0][BottomiumState.x3P], delta);
		}

		private static void AssertCorrectQGPSuppressionFactors_pPb(
			BottomiumVector[][][] suppressionFactors
			)
		{
			double delta = 1e-15;

			Assert.AreEqual(0.870959255403926, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.629071345116346, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.631319997953238, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.390430589067853, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.389174258284470, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.212606879499683, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.875132495839671, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.637186632666858, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.640192026469614, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.395268934760168, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.394378385525501, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.222612084074620, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.885178387969945, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.662593020945238, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.665245099438922, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.413556448412305, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.412628112619902, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.251422324633851, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.898903631322919, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.700957838965086, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.703522646627089, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.457002504044941, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.456189544654840, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.262981036565631, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.908720901637514, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.725026269883419, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.727761452532946, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.468378280815664, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.467604848668772, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.258530558924321, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.928613379383993, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.776128555001233, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.778661249187993, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.555353923522511, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.554132873567121, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.324448555979025, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.975278397817057, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.916207058397954, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.917102712662986, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.839872591490728, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.838921624613152, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.618396390851657, suppressionFactors[0][6][0][BottomiumState.x3P], delta);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;
	}
}
