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
			FireballParam param = new FireballParam
			{
				BreakupTemperature_MeV = 160,
				CenterOfMassEnergy_TeV = 2.76,
				DecayWidthRetrievalFunction = DummyDecayWidthProvider.GetDummyDecayWidth,
				DiffusenessA_fm = 0.546,
				DiffusenessB_fm = 0.546,
				ExpansionMode = ExpansionMode.Transverse,
				FormationTimes_fm = FormationTimes,
				GridCellSize_fm = 1,
				GridRadius_fm = 10,
				InitialMaximumTemperature_MeV = 550,
				NuclearRadiusA_fm = 6.62,
				NuclearRadiusB_fm = 6.62,
				NucleonNumberA = 208,
				NucleonNumberB = 208,
				NucleusShapeA = NucleusShape.WoodsSaxonPotential,
				NucleusShapeB = NucleusShape.WoodsSaxonPotential,
				ProtonNumberA = 82,
				ProtonNumberB = 82,
				TemperatureProfile = TemperatureProfile.NmixPHOBOS13,
				ThermalTime_fm = 0.1,
				TransverseMomenta_GeV = new List<double> { 6 },
				UseElectricField = false,
				UseMagneticField = false
			};

			return param;
		}

		private static FireballParam CreateFireballParam_pPb()
		{
			FireballParam param = CreateFireballParam_PbPb();

			param.CenterOfMassEnergy_TeV = 5.02;
			param.DiffusenessA_fm = 0;
			param.GridCellSize_fm = 0.5;
			param.GridRadius_fm = 5;
			param.NuclearRadiusA_fm = 0.8775;
			param.NucleonNumberA = 1;
			param.NucleusShapeA = NucleusShape.GaussianDistribution;
			param.ProtonNumberA = 1;
			param.TemperatureProfile = TemperatureProfile.NmixALICE13;

			return param;
		}

		private static void AssertCorrectQGPSuppressionFactors_PbPb(
			BottomiumVector[][][] suppressionFactors
			)
		{
			double delta = 1e-5;

			Assert.AreEqual(0.57786, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.19621, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.20244, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.07380, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.07357, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.03155, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.60474, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.21882, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.22606, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.08596, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.08566, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.04003, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.64518, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.26647, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.27148, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.10488, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.10451, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.04260, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.71978, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.36396, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.36927, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.14951, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.14933, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.06915, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.79651, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.48379, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.48819, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.24818, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.24746, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.10287, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.85361, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.59466, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.59955, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.35144, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.35102, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.14453, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.91339, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.73759, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.74097, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.55947, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.55890, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.28768, suppressionFactors[0][6][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.97970, suppressionFactors[0][7][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.93052, suppressionFactors[0][7][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.93169, suppressionFactors[0][7][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.88070, suppressionFactors[0][7][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.88044, suppressionFactors[0][7][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.60604, suppressionFactors[0][7][0][BottomiumState.x3P], delta);
		}

		private static void AssertCorrectQGPSuppressionFactors_pPb(
			BottomiumVector[][][] suppressionFactors
			)
		{
			double delta = 1e-5;

			Assert.AreEqual(0.85880, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.60237, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.60536, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.37327, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.36559, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.19903, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.86364, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.61187, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.61474, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.37871, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.37802, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.20890, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.87461, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.63850, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.64148, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.39844, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.39733, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.23330, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.88971, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.67920, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.68164, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.44076, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.43980, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.25830, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.90086, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.70471, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.70723, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.45433, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.45353, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.25619, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.92250, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.75985, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.76173, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.53303, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.53212, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.32135, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.97362, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.91093, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.91196, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.83170, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.83055, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.62332, suppressionFactors[0][6][0][BottomiumState.x3P], delta);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;
	}
}
