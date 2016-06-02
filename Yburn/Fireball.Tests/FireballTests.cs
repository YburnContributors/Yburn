using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;
using Yburn.Tests.Util;

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
			AssertHelper.AssertRoundedEqual(0.68735662437240208, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.14694835740192158, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.18302631965053007, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.043377555418212643, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.055872842998975665, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.022662140834380177, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		[TestMethod]
		public void FireballEvolution_MaximallyBlueshifted()
		{
			SetFireball(DecayWidthEvaluationType.MaximallyBlueshifted);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertRoundedEqual(0.27229850657076948, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.02111501723733869, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.029253999794417004, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.008591256423154045, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.010894507768336408, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.0049171614281235529, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		[TestMethod]
		public void FireballEvolution_SameAsTemperature()
		{
			SetFireball(DecayWidthEvaluationType.UnshiftedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertRoundedEqual(0.65398639771067846, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.12166054711179025, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.15830120446823065, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.037575441315405834, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.047287591230614393, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.020521127911320304, qgpSuppressionFactors[(int)BottomiumState.x3P]);
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
			param.ImpactParameterFm = 1.5;
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
			param.NumberGridPoints = 26;
			param.BeamRapidity = 7.99;
			param.TemperatureDecayWidthList = TemperatureDecayWidthList.GetList(
					"..\\..\\bbdata-Pert1LoopCut3.txt", DecayWidthType.GammaTot, new string[] { "Complex" });
			param.ShapeFunctionTypeA = ShapeFunctionType.WoodsSaxonPotential;
			param.ShapeFunctionTypeB = ShapeFunctionType.WoodsSaxonPotential;

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