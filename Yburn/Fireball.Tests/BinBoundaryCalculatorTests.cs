using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
		public void CalculateMinBiasBin()
		{
			BinBoundaryCalculator calculator = new BinBoundaryCalculator(
				CreateFireballParam(), CancellationToken);
			calculator.Calculate(CentralityBinsInPercent);

			AssertCorrectImpactParamsAtBinBoundaries(calculator);
			AssertCorrectMeanParticipantsInBin(calculator);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly List<List<int>> CentralityBinsInPercent
			= new List<List<int>> { new List<int> { 0, 5, 10, 20, 30, 40, 50, 100 } };

		private static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

		private static double GridCellSize = 0.4;

		private static int GridRadiusFm = 10;

		private static void AssertCorrectImpactParamsAtBinBoundaries(BinBoundaryCalculator calculator)
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

		private static void AssertCorrectMeanParticipantsInBin(BinBoundaryCalculator calculator)
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

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.BreakupTemperatureMeV = 160;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.FormationTimesFm = new List<double> { 0.3, 0.3, 0.3, 0.3, 0.3, 0.3 };
			param.GridCellSizeFm = GridCellSize;
			param.GridRadiusFm = GridRadiusFm;
			param.InitialMaximumTemperatureMeV = 550;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.ProtonProtonBaseline = ProtonProtonBaseline.CMS2012;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;

			return param;
		}
	}
}