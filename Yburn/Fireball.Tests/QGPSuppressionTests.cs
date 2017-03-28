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
			param.CenterOfMassEnergyTeV = 2.76;
			param.DecayWidthRetrievalFunction = DummyDecayWidthProvider.GetDummyDecayWidth;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.ExpansionMode = ExpansionMode.Transverse;
			param.FormationTimesFm = FormationTimes;
			param.GridCellSizeFm = 1;
			param.GridRadiusFm = 10;
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
			param.UseElectricField = false;
			param.UseMagneticField = false;

			return param;
		}

		private static FireballParam CreateFireballParam_pPb()
		{
			FireballParam param = CreateFireballParam_PbPb();

			param.CenterOfMassEnergyTeV = 5.02;
			param.DiffusenessBFm = 0;
			param.GridCellSizeFm = 0.5;
			param.GridRadiusFm = 5;
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
			double delta = 1e-5;

			Assert.AreEqual(0.58622, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.20376, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.21091, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.07592, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.07572, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.03186, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.61286, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.22699, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.23438, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.08834, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.08796, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.04579, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.65272, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.27555, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.28085, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.10763, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.10716, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.04278, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.72584, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.37376, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.37883, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.15272, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.15238, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.06963, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.80121, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.49217, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.49739, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.26519, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.26443, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.10365, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.85666, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.60173, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.60648, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.35660, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.35595, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.14480, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.91494, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.74089, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.74446, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.56565, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.56410, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.28914, suppressionFactors[0][6][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.98008, suppressionFactors[0][7][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.93179, suppressionFactors[0][7][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.93279, suppressionFactors[0][7][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.88177, suppressionFactors[0][7][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.88078, suppressionFactors[0][7][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.60604, suppressionFactors[0][7][0][BottomiumState.x3P], delta);
		}

		private static void AssertCorrectQGPSuppressionFactors_pPb(
			BottomiumVector[][][] suppressionFactors
			)
		{
			double delta = 1e-5;

			Assert.AreEqual(0.87096, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.62907, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.63132, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.39043, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.38917, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.21260, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.87513, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.63726, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.64020, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.39527, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.39438, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.22261, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.88520, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.66266, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.66529, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.41356, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.41263, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.25142, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.89892, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.70113, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.70356, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.45702, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.45635, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.26298, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.90874, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.72517, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.72782, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.46840, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.46787, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.25853, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.92864, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.77639, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.77879, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.55540, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.55443, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.32465, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.97531, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.91642, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.91720, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.84017, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.83931, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.61840, suppressionFactors[0][6][0][BottomiumState.x3P], delta);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;
	}
}
