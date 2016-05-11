using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		[TestCleanup]
		public void TestCleanup()
		{
			if(Fireball != null)
			{
				Fireball.Dispose();
			}
		}

		[TestMethod]
		public void FireballEvolution_AveragedTemperature()
		{
			SetFireball(DecayWidthEvaluationType.AveragedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			Assert.AreEqual(0.68735662450554291, qgpSuppressionFactors[(int)BottomiumState.Y1S], 1e-4);
			Assert.AreEqual(0.14694835743748225, qgpSuppressionFactors[(int)BottomiumState.x1P], 1e-4);
			Assert.AreEqual(0.18302631971728495, qgpSuppressionFactors[(int)BottomiumState.Y2S], 1e-4);
			Assert.AreEqual(0.043377555420717834, qgpSuppressionFactors[(int)BottomiumState.x2P], 1e-4);
			Assert.AreEqual(0.055872843005052776, qgpSuppressionFactors[(int)BottomiumState.Y3S], 1e-4);
			Assert.AreEqual(0.022662140834388896, qgpSuppressionFactors[(int)BottomiumState.x3P], 1e-4);
		}

		[TestMethod]
		public void FireballEvolution_MaximallyBlueshifted()
		{
			SetFireball(DecayWidthEvaluationType.MaximallyBlueshifted);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			Assert.AreEqual(0.27229850669580641, qgpSuppressionFactors[(int)BottomiumState.Y1S], 4e-4);
			Assert.AreEqual(0.021115017239003504, qgpSuppressionFactors[(int)BottomiumState.x1P], 4e-4);
			Assert.AreEqual(0.029253999798176698, qgpSuppressionFactors[(int)BottomiumState.Y2S], 4e-4);
			Assert.AreEqual(0.0085912564232263084, qgpSuppressionFactors[(int)BottomiumState.x2P], 4e-4);
			Assert.AreEqual(0.010894507768678827, qgpSuppressionFactors[(int)BottomiumState.Y3S], 4e-4);
			Assert.AreEqual(0.0049171614280425, qgpSuppressionFactors[(int)BottomiumState.x3P], 4e-4);
		}

		[TestMethod]
		public void FireballEvolution_SameAsTemperature()
		{
			SetFireball(DecayWidthEvaluationType.UnshiftedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			Assert.AreEqual(0.65398639785152268, qgpSuppressionFactors[(int)BottomiumState.Y1S], 1e-15);
			Assert.AreEqual(0.12166054713738836, qgpSuppressionFactors[(int)BottomiumState.x1P], 1e-15);
			Assert.AreEqual(0.15830120452031807, qgpSuppressionFactors[(int)BottomiumState.Y2S], 1e-15);
			Assert.AreEqual(0.037575441317286427, qgpSuppressionFactors[(int)BottomiumState.x2P], 1e-15);
			Assert.AreEqual(0.047287591234820583, qgpSuppressionFactors[(int)BottomiumState.Y3S], 1e-15);
			Assert.AreEqual(0.020521127911300879, qgpSuppressionFactors[(int)BottomiumState.x3P], 1e-15);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double MinimalCentralTemperatureMeV = 160;

		private static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

		private static FireballParam CreateFireballParam(
			DecayWidthEvaluationType decayWidthEvaluationType
			)
		{
			FireballParam param = new FireballParam();
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.DecayWidthEvaluationType = DecayWidthEvaluationType.AveragedTemperature;
			param.ExpansionMode = ExpansionMode.Transverse;
			param.ImpactParamFm = 1.5;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;
			param.TransverseMomentaGeV = new double[] { 6 };
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.DecayWidthEvaluationType = decayWidthEvaluationType;
			param.InitialCentralTemperatureMeV = 550;
			param.MinimalCentralTemperatureMeV = MinimalCentralTemperatureMeV;
			param.FormationTimesFm = new double[] { 0.3, 0.3, 0.3, 0.3, 0.3, 0.3 };
			param.ThermalTimeFm = 0.1;
			param.GridCellSizeFm = 0.4;
			param.NumberGridCells = 26;
			param.BeamRapidity = 7.99;
			param.TemperatureDecayWidthList = TemperatureDecayWidthList.GetList(
					"..\\..\\bbdata-Pert1LoopCut3.txt", DecayWidthType.GammaTot, new string[] { "Complex" });
            param.CollisionType = CollisionType.WoodsSaxonAWoodsSaxonB;

            if (param.CollisionType == CollisionType.WoodsSaxonAWoodsSaxonB)
            {
                param.NumberGridCellsInX = param.NumberGridCells;
                param.NumberGridCellsInY = param.NumberGridCells;
            }
            else if (param.CollisionType == CollisionType.WoodsSaxonAGaussianB)
            {
                param.NumberGridCellsInX = 2 * param.NumberGridCells - 1;
                param.NumberGridCellsInY = param.NumberGridCells;
            }
            else
            {
                throw new Exception("Invalid CollisionType.");
            }

            return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/
		private Fireball Fireball;

		private void SetFireball(
			DecayWidthEvaluationType decayWidthEvaluationType
			)
		{
			Fireball = new Fireball(CreateFireballParam(decayWidthEvaluationType));
		}

		private void CalculateFireballEvolution()
		{
			while(Fireball.CentralTemperature > MinimalCentralTemperatureMeV)
			{
				Fireball.Advance(5);
			}
		}

		private double[] GetSuppressionFactors()
		{
			double[] qgpSuppressionFactors = new double[NumberBottomiumStates];
			for(int l = 0; l < NumberBottomiumStates; l++)
			{
				qgpSuppressionFactors[l] = Fireball.IntegrateFireballField(
					"UnscaledSuppression", (BottomiumState)l) /
					Fireball.IntegrateFireballField("Overlap");
			}

			return qgpSuppressionFactors;
		}
	}
}