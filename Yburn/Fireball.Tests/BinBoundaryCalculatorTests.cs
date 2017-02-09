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
			AssertHelper.AssertApproximatelyEqual(383.63733145261438, nparts[0]);
			AssertHelper.AssertApproximatelyEqual(340.22338305849638, nparts[1]);
			AssertHelper.AssertApproximatelyEqual(268.43057326077735, nparts[2]);
			AssertHelper.AssertApproximatelyEqual(187.36409577711942, nparts[3]);
			AssertHelper.AssertApproximatelyEqual(131.9447405820836, nparts[4]);
			AssertHelper.AssertApproximatelyEqual(89.250454155387914, nparts[5]);
			AssertHelper.AssertApproximatelyEqual(19.648149650443639, nparts[6]);
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
			AssertHelper.AssertApproximatelyEqual(14.974233725674532, nparts[0]);
			AssertHelper.AssertApproximatelyEqual(14.487499546553337, nparts[1]);
			AssertHelper.AssertApproximatelyEqual(13.489194925233843, nparts[2]);
			AssertHelper.AssertApproximatelyEqual(12.008818387583021, nparts[3]);
			AssertHelper.AssertApproximatelyEqual(10.510321287054687, nparts[4]);
			AssertHelper.AssertApproximatelyEqual(8.8777714843503954, nparts[5]);
			AssertHelper.AssertApproximatelyEqual(3.459490632960875, nparts[6]);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;

		private static FireballParam CreateFireballParam_PbPb()
		{
			FireballParam param = new FireballParam();

			param.BreakupTemperatureMeV = 160;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.GridCellSizeFm = 0.4;
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

			return param;
		}

		private static FireballParam CreateFireballParam_pPb()
		{
			FireballParam param = CreateFireballParam_PbPb();

			param.DiffusenessBFm = 0;
			param.GridCellSizeFm = 0.2;
			param.GridRadiusFm = 5;
			param.InelasticppCrossSectionFm = 6.8;
			param.NuclearRadiusBFm = 0.8775;
			param.NucleonNumberB = 1;
			param.NucleusShapeB = NucleusShape.GaussianDistribution;
			param.ProtonNumberB = 1;
			param.TemperatureProfile = TemperatureProfile.NmixALICE13;

			return param;
		}
	}
}
