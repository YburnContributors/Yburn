﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballEvolutionTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void FireballEvolution()
		{
			SetFireball();
			CalculateFireballEvolution();

			double[] qgpSuppressionFactors = GetSuppressionFactors();
			AssertHelper.AssertApproximatelyEqual(0.5708251868116111, qgpSuppressionFactors[(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.19042039607106531, qgpSuppressionFactors[(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.19347049076166911, qgpSuppressionFactors[(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.072893356385165961, qgpSuppressionFactors[(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.068729442928737755, qgpSuppressionFactors[(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.027508390401150242, qgpSuppressionFactors[(int)BottomiumState.x3P]);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double BreakupTemperature = 160;

		private static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

		private static FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.BreakupTemperatureMeV = BreakupTemperature;
			param.DecayWidthRetrievalFunction = DummyDecayWidthProvider.GetDummyDecayWidth;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.ExpansionMode = ExpansionMode.Transverse;
			param.FormationTimesFm = new List<double> { 0.4, 0.4, 0.4, 0.4, 0.4, 0.4 };
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
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;
			param.ThermalTimeFm = 0.1;
			param.TransverseMomentaGeV = new List<double> { 6 };

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/
		private Fireball Fireball;

		private void SetFireball()
		{
			Fireball = new Fireball(CreateFireballParam());
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