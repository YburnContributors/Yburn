using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

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
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
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
			Assert.AreEqual(4.4, impactParams[2], 1e-14);
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
			Assert.AreEqual(383.63733145093579, nparts[0]);
			Assert.AreEqual(340.22338305697463, nparts[1]);
			Assert.AreEqual(268.43057325955959, nparts[2]);
			Assert.AreEqual(187.36409577622058, nparts[3]);
			Assert.AreEqual(131.9447405813884, nparts[4]);
			Assert.AreEqual(89.250454154885873, nparts[5]);
			Assert.AreEqual(19.648149650345417, nparts[6]);
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
			param.NuclearRadiusAFm = 6.62;
			param.DiffusenessAFm = 0.546;

			param.NucleonNumberB = 208;
			param.NuclearRadiusBFm = 6.62;
			param.DiffusenessBFm = 0.546;

			param.TemperatureDecayWidthList = GetTemperatureDecayWidthList();
			param.ShapeFunctionA = ShapeFunction.WoodsSaxonPotential;
			param.ShapeFunctionB = ShapeFunction.WoodsSaxonPotential;

			if(param.ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				param.NumberGridCellsInX = NumberGridCells;
				param.NumberGridCellsInY = NumberGridCells;
			}
			else if(param.ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				param.NumberGridCellsInX = 2 * NumberGridCells - 1;
				param.NumberGridCellsInY = NumberGridCells;
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}

			return param;
		}
	}
}