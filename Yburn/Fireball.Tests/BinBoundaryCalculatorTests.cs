using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class BinBoundaryCalculatorTests
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
		public void CalculateBins_PbPb()
		{
			BinBoundaryCalculator calculator = new BinBoundaryCalculator(
				CreateFireballParam_PbPb(), CancellationToken);
			calculator.Calculate(CentralityBinsInPercent);

			AssertCorrectImpactParamsAtBinBoundaries_PbPb(calculator);
			AssertCorrectMeanParticipantsInBin_PbPb(calculator);
		}

		[TestMethod]
		public void CalculateBins_pPb()
		{
			BinBoundaryCalculator calculator = new BinBoundaryCalculator(
				CreateFireballParam_pPb(), CancellationToken);
			calculator.Calculate(CentralityBinsInPercent);

			AssertCorrectImpactParamsAtBinBoundaries_pPb(calculator);
			AssertCorrectMeanParticipantsInBin_pPb(calculator);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly List<List<int>> CentralityBinsInPercent
			= new List<List<int>> { new List<int> { 0, 5, 10, 20, 30, 40, 50, 100 } };

		private static void AssertCorrectImpactParamsAtBinBoundaries_PbPb(BinBoundaryCalculator calculator)
		{
			List<double> impactParams = calculator.ImpactParamsAtBinBoundaries[0];
			Assert.AreEqual(8, impactParams.Count);
			AssertHelper.AssertApproximatelyEqual(0, impactParams[0]);
			AssertHelper.AssertApproximatelyEqual(3.2, impactParams[1]);
			AssertHelper.AssertApproximatelyEqual(4.4, impactParams[2]);
			AssertHelper.AssertApproximatelyEqual(6.8, impactParams[3]);
			AssertHelper.AssertApproximatelyEqual(8.4, impactParams[4]);
			AssertHelper.AssertApproximatelyEqual(9.6, impactParams[5]);
			AssertHelper.AssertApproximatelyEqual(10.8, impactParams[6]);
			AssertHelper.AssertApproximatelyEqual(21.2, impactParams[7]);
		}

		private static void AssertCorrectMeanParticipantsInBin_PbPb(BinBoundaryCalculator calculator)
		{
			List<double> nparts = calculator.MeanParticipantsInBin[0];
			Assert.AreEqual(7, nparts.Count);
			AssertHelper.AssertApproximatelyEqual(383.37, nparts[0], 5);
			AssertHelper.AssertApproximatelyEqual(339.85, nparts[1], 5);
			AssertHelper.AssertApproximatelyEqual(268.00, nparts[2], 5);
			AssertHelper.AssertApproximatelyEqual(186.95, nparts[3], 5);
			AssertHelper.AssertApproximatelyEqual(131.58, nparts[4], 5);
			AssertHelper.AssertApproximatelyEqual(88.942, nparts[5], 5);
			AssertHelper.AssertApproximatelyEqual(19.579, nparts[6], 5);
		}

		private static void AssertCorrectImpactParamsAtBinBoundaries_pPb(BinBoundaryCalculator calculator)
		{
			List<double> impactParams = calculator.ImpactParamsAtBinBoundaries[0];
			Assert.AreEqual(8, impactParams.Count);
			AssertHelper.AssertApproximatelyEqual(0, impactParams[0]);
			AssertHelper.AssertApproximatelyEqual(1.6, impactParams[1]);
			AssertHelper.AssertApproximatelyEqual(2.4, impactParams[2]);
			AssertHelper.AssertApproximatelyEqual(3.6, impactParams[3]);
			AssertHelper.AssertApproximatelyEqual(4.4, impactParams[4]);
			AssertHelper.AssertApproximatelyEqual(5.0, impactParams[5]);
			AssertHelper.AssertApproximatelyEqual(5.6, impactParams[6]);
			AssertHelper.AssertApproximatelyEqual(14.0, impactParams[7]);
		}

		private static void AssertCorrectMeanParticipantsInBin_pPb(BinBoundaryCalculator calculator)
		{
			List<double> nparts = calculator.MeanParticipantsInBin[0];
			Assert.AreEqual(7, nparts.Count);
			AssertHelper.AssertApproximatelyEqual(15.257, nparts[0], 5);
			AssertHelper.AssertApproximatelyEqual(14.760, nparts[1], 5);
			AssertHelper.AssertApproximatelyEqual(13.742, nparts[2], 5);
			AssertHelper.AssertApproximatelyEqual(12.231, nparts[3], 5);
			AssertHelper.AssertApproximatelyEqual(10.703, nparts[4], 5);
			AssertHelper.AssertApproximatelyEqual(9.0377, nparts[5], 5);
			AssertHelper.AssertApproximatelyEqual(3.5060, nparts[6], 5);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;

		private static FireballParam CreateFireballParam_PbPb()
		{
			FireballParam param = new FireballParam();

			param.BreakupTemperature_MeV = 160;
			param.CenterOfMassEnergyTeV = 2.76;
			param.DiffusenessA_fm = 0.546;
			param.DiffusenessB_fm = 0.546;
			param.GridCellSize_fm = 0.4;
			param.GridRadius_fm = 10;
			param.InitialMaximumTemperature_MeV = 550;
			param.NuclearRadiusA_fm = 6.62;
			param.NuclearRadiusB_fm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;

			return param;
		}

		private static FireballParam CreateFireballParam_pPb()
		{
			FireballParam param = CreateFireballParam_PbPb();

			param.CenterOfMassEnergyTeV = 5.02;
			param.DiffusenessA_fm = 0;
			param.GridCellSize_fm = 0.2;
			param.GridRadius_fm = 5;
			param.NuclearRadiusA_fm = 0.8775;
			param.NucleonNumberA = 1;
			param.NucleusShapeA = NucleusShape.GaussianDistribution;
			param.ProtonNumberA = 1;
			param.TemperatureProfile = TemperatureProfile.NmixALICE13;

			return param;
		}
	}
}
