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
		public void FireballEvolution_AveragedDecayWidth()
		{
			SetFireball(DecayWidthEvaluationType.AveragedDecayWidth);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertApproximatelyEqual(0.64017034045961385, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.025271952777538038, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.040457173167155434, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.016922276995776139, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.016374854512085597, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.016374854512085597, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		[TestMethod]
		public void FireballEvolution_AveragedTemperature()
		{
			SetFireball(DecayWidthEvaluationType.AveragedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertApproximatelyEqual(0.68252447820678641, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.14523997466000008, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.18004535030644905, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.04249598145607774, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.054605639154239473, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.02236133123563689, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		[TestMethod]
		public void FireballEvolution_MaximallyBlueshifted()
		{
			SetFireball(DecayWidthEvaluationType.MaximallyBlueshifted);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertApproximatelyEqual(0.29322392236161327, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.024133455852001055, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.035597085276149021, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.016922276995776139, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.016374854512085597, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.016374854512085597, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		[TestMethod]
		public void FireballEvolution_UnshiftedTemperature()
		{
			SetFireball(DecayWidthEvaluationType.UnshiftedTemperature);
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertApproximatelyEqual(0.65398456350529932, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.1216628615554517, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.15831465807174472, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.037575775582552727, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.047288313626309621, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.020521140343921745, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double BreakupTemperature = 160;

		private static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

		private static FireballParam CreateFireballParam(
			DecayWidthEvaluationType decayWidthEvaluationType
			)
		{
			FireballParam param = new FireballParam();
			param.BeamRapidity = 7.99;
			param.BreakupTemperatureMeV = BreakupTemperature;
			param.DecayWidthEvaluationType = DecayWidthEvaluationType.AveragedTemperature;
			param.DecayWidthEvaluationType = decayWidthEvaluationType;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.ExpansionMode = ExpansionMode.Transverse;
			param.FormationTimesFm = new double[] { 0.3, 0.3, 0.3, 0.3, 0.3, 0.3 };
			param.GridCellSizeFm = 0.4;
			param.GridRadiusFm = 10;
			param.ImpactParameterFm = 1.5;
			param.InitialMaximumTemperatureMeV = 550;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.NumberAveragingAngles = 20;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.QGPFormationTemperatureMeV = 160;
			param.TemperatureDecayWidthList = TemperatureDecayWidthListHelper.GetList(
				"..\\..\\bbdata-Pert1LoopCut3.txt", DecayWidthType.GammaTot, new string[] { "Complex" });
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;
			param.ThermalTimeFm = 0.1;
			param.TransverseMomentaGeV = new double[] { 6 };

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
			while(Fireball.MaximumTemperature > BreakupTemperature)
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