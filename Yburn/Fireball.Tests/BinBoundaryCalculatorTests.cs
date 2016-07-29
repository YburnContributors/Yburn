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

        private static readonly int[][] CentralityBinsInPercent = new int[][] {
            new int[] { 0, 5, 10, 20, 30, 40, 50, 100 } };

        private static readonly int NumberBottomiumStates
            = Enum.GetValues(typeof(BottomiumState)).Length;

        private static double GridCellSize = 0.4;

        private static int GridRadiusFm = 10;

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
            AssertHelper.AssertRoundedEqual(0, impactParams[0]);
            AssertHelper.AssertRoundedEqual(3.2, impactParams[1]);
            AssertHelper.AssertRoundedEqual(4.4, impactParams[2]);
            AssertHelper.AssertRoundedEqual(6.8, impactParams[3]);
            AssertHelper.AssertRoundedEqual(8.4, impactParams[4]);
            AssertHelper.AssertRoundedEqual(9.6, impactParams[5]);
            AssertHelper.AssertRoundedEqual(10.8, impactParams[6]);
            AssertHelper.AssertRoundedEqual(21.2, impactParams[7]);
        }

        private static void AssertCorrectMeanParticipantsInBin(BinBoundaryCalculator calculator)
        {
            double[] nparts = calculator.MeanParticipantsInBin[0];
            Assert.AreEqual(7, nparts.Length);
            AssertHelper.AssertRoundedEqual(383.63733145261438, nparts[0]);
            AssertHelper.AssertRoundedEqual(340.22338305849638, nparts[1]);
            AssertHelper.AssertRoundedEqual(268.43057326077735, nparts[2]);
            AssertHelper.AssertRoundedEqual(187.36409577711942, nparts[3]);
            AssertHelper.AssertRoundedEqual(131.9447405820836, nparts[4]);
            AssertHelper.AssertRoundedEqual(89.250454155387914, nparts[5]);
            AssertHelper.AssertRoundedEqual(19.648149650443639, nparts[6]);
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
            param.GridRadiusFm = GridRadiusFm;

            param.NucleonNumberA = 208;
            param.NuclearRadiusAFm = 6.62;
            param.DiffusenessAFm = 0.546;

            param.NucleonNumberB = 208;
            param.NuclearRadiusBFm = 6.62;
            param.DiffusenessBFm = 0.546;

            param.TemperatureDecayWidthList = GetTemperatureDecayWidthList();
            param.ShapeFunctionTypeA = ShapeFunctionType.WoodsSaxonPotential;
            param.ShapeFunctionTypeB = ShapeFunctionType.WoodsSaxonPotential;

            return param;
        }
    }
}