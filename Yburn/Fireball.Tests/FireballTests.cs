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
			Assert.AreEqual(0.685250831226219, qgpSuppressionFactors[(int)BottomiumState.Y1S], 1e-4);
			Assert.AreEqual(0.145548188917557, qgpSuppressionFactors[(int)BottomiumState.x1P], 1e-4);
			Assert.AreEqual(0.181337174147212, qgpSuppressionFactors[(int)BottomiumState.Y2S], 1e-4);
			Assert.AreEqual(0.0429757611907636, qgpSuppressionFactors[(int)BottomiumState.x2P], 1e-4);
			Assert.AreEqual(0.0553731580470921, qgpSuppressionFactors[(int)BottomiumState.Y3S], 1e-4);
			Assert.AreEqual(0.0224418133599137, qgpSuppressionFactors[(int)BottomiumState.x3P], 1e-4);
		}

		[TestMethod]
		public void FireballEvolution_MaximallyBlueshifted()
		{
			SetFireball(DecayWidthEvaluationType.MaximallyBlueshifted);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			Assert.AreEqual(0.269354021666524, qgpSuppressionFactors[(int)BottomiumState.Y1S], 4e-4);
			Assert.AreEqual(0.0208609142470617, qgpSuppressionFactors[(int)BottomiumState.x1P], 4e-4);
			Assert.AreEqual(0.028876456026112, qgpSuppressionFactors[(int)BottomiumState.Y2S], 4e-4);
			Assert.AreEqual(0.00848629615599608, qgpSuppressionFactors[(int)BottomiumState.x2P], 4e-4);
			Assert.AreEqual(0.0107574789962652, qgpSuppressionFactors[(int)BottomiumState.Y3S], 4e-4);
			Assert.AreEqual(0.00486554796139194, qgpSuppressionFactors[(int)BottomiumState.x3P], 4e-4);
		}

		[TestMethod]
		public void FireballEvolution_SameAsTemperature()
		{
			SetFireball(DecayWidthEvaluationType.UnshiftedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			Assert.AreEqual(0.65139796775724945, qgpSuppressionFactors[(int)BottomiumState.Y1S], 1e-15);
			Assert.AreEqual(0.12047654947393906, qgpSuppressionFactors[(int)BottomiumState.x1P], 1e-15);
			Assert.AreEqual(0.15676062081686307, qgpSuppressionFactors[(int)BottomiumState.Y2S], 1e-15);
			Assert.AreEqual(0.037209757981401831, qgpSuppressionFactors[(int)BottomiumState.x2P], 1e-15);
			Assert.AreEqual(0.0468273894779688, qgpSuppressionFactors[(int)BottomiumState.Y3S], 1e-15);
			Assert.AreEqual(0.020321416763603287, qgpSuppressionFactors[(int)BottomiumState.x3P], 1e-15);
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
			param.DiffusenessFmA = 0.546;
			param.DiffusenessFmB = 0.546;
			param.DecayWidthEvaluationType = DecayWidthEvaluationType.AveragedTemperature;
			param.ExpansionMode = ExpansionMode.Transverse;
			param.ImpactParamFm = 1.5;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;
			param.TransverseMomentaGeV = new double[] { 6 };
			param.NuclearRadiusFmA = 6.62;
			param.NuclearRadiusFmB = 6.62;
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