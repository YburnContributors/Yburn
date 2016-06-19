using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void FireballEvolution_AveragedTemperature()
		{
			SetFireball(DecayWidthEvaluationType.AveragedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertRoundedEqual(0.68735662437240219, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.14694835740192164, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.18302631965053015, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.04337755541821265, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.055872842998975672, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.022662140834380173, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		[TestMethod]
		public void FireballEvolution_MaximallyBlueshifted()
		{
			SetFireball(DecayWidthEvaluationType.MaximallyBlueshifted);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertRoundedEqual(0.27229850657076959, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.02111501723733869, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.029253999794417004, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.0085912564231540433, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.01089450776833641, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.0049171614281235511, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		[TestMethod]
		public void FireballEvolution_SameAsTemperature()
		{
			SetFireball(DecayWidthEvaluationType.UnshiftedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertRoundedEqual(0.65398639771067857, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.12166054711179025, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.15830120446823068, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.037575441315405828, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.047287591230614406, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
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
			param.GridRadiusFm = 10;
			param.BeamRapidity = 7.99;
			param.TemperatureDecayWidthList = TemperatureDecayWidthListHelper.GetList(
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