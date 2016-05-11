using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

		private static readonly int[][] CentralityBinsInPercent = new int[][] {
			new int[] { 0, 5, 10, 20, 30, 40, 50, 100 } };

		private static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

		private static double GridCellSize = 0.4;

		private static int NumberGridCells = 26;

		private List<KeyValuePair<double, double>>[] GetTemperatureDecayWidthList()
		{
			List<KeyValuePair<double, double>>[] list
				= new List<KeyValuePair<double, double>>[NumberBottomiumStates];
			for(int i = 0; i < list.Length; i++)
			{
				list[i] = new List<KeyValuePair<double, double>>();
			}

			return list;
		}

		private static void AssertCorrectImpactParamsAtBinBoundaries(BinBoundaryCalculator calculator)
		{
			double[] impactParams = calculator.ImpactParamsAtBinBoundaries[0];
			Assert.AreEqual(8, impactParams.Length);
			Assert.AreEqual(0, impactParams[0], 1e-14);
			Assert.AreEqual(3.2, impactParams[1], 1e-14);
			Assert.AreEqual(4.8, impactParams[2], 1e-14);
			Assert.AreEqual(6.8, impactParams[3], 1e-14);
			Assert.AreEqual(8.4, impactParams[4], 1e-14);
			Assert.AreEqual(9.6, impactParams[5], 1e-14);
			Assert.AreEqual(10.8, impactParams[6], 1e-14);
			Assert.AreEqual(21.2, impactParams[7], 1e-14);
		}

		private static void AssertCorrectMeanParticipantsInBin(BinBoundaryCalculator calculator)
		{
			double[] nparts = calculator.MeanParticipantsInBin[0];
			Assert.AreEqual(7, nparts.Length);
			Assert.AreEqual(386.319541643199, nparts[0], 1e-12);
			Assert.AreEqual(334.785097729082, nparts[1], 1e-12);
			Assert.AreEqual(263.95471617815, nparts[2], 1e-12);
			Assert.AreEqual(189.589583965075, nparts[3], 1e-12);
			Assert.AreEqual(133.929607266372, nparts[4], 1e-12);
			Assert.AreEqual(90.956150729068, nparts[5], 1e-12);
			Assert.AreEqual(20.1293079185253, nparts[6], 1e-12);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();
			param.InitialCentralTemperatureMeV = 550;
			param.MinimalCentralTemperatureMeV = 160;
			param.FormationTimesFm = new double[] { 0.3, 0.3, 0.3, 0.3, 0.3, 0.3 };

			param.GridCellSizeFm = GridCellSize;
			param.NumberGridCells = NumberGridCells;

			param.NucleonNumberA = 208;
			param.NuclearRadiusFmA = 6.62;
			param.DiffusenessFmA = 0.546;

			param.NucleonNumberB = 208;
			param.NuclearRadiusFmB = 6.62;
			param.DiffusenessFmB = 0.546;

			param.TemperatureDecayWidthList = GetTemperatureDecayWidthList();
            param.CollisionType = CollisionType.WoodsSaxonAWoodsSaxonB;

            if (param.CollisionType == CollisionType.WoodsSaxonAWoodsSaxonB)
            {
                param.NumberGridCellsInX = NumberGridCells;
                param.NumberGridCellsInY = NumberGridCells;
            }
            else if (param.CollisionType == CollisionType.WoodsSaxonAGaussianB)
            {
                param.NumberGridCellsInX = 2 * NumberGridCells - 1;
                param.NumberGridCellsInY = NumberGridCells;
            }
            else
            {
                throw new Exception("Invalid CollisionType.");
            }

            return param;
		}
	}
}