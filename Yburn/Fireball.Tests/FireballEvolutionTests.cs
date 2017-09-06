﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballEvolutionTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void FireballEvolution_PbPb()
		{
			Fireball = new Fireball(CreateFireballParam_PbPb());
			CalculateFireballEvolution();
			double[] qgpSuppressionFactors = GetSuppressionFactors();

			double delta = 1e-5;
			Assert.AreEqual(0.56163, qgpSuppressionFactors[(int)BottomiumState.Y1S], delta);
			Assert.AreEqual(0.18183, qgpSuppressionFactors[(int)BottomiumState.x1P], delta);
			Assert.AreEqual(0.18518, qgpSuppressionFactors[(int)BottomiumState.Y2S], delta);
			Assert.AreEqual(0.06975, qgpSuppressionFactors[(int)BottomiumState.x2P], delta);
			Assert.AreEqual(0.06666, qgpSuppressionFactors[(int)BottomiumState.Y3S], delta);
			Assert.AreEqual(0.02729, qgpSuppressionFactors[(int)BottomiumState.x3P], delta);
		}

		[TestMethod]
		public void FireballEvolution_pPb()
		{
			Fireball = new Fireball(CreateFireballParam_pPb());
			CalculateFireballEvolution();
			double[] qgpSuppressionFactors = GetSuppressionFactors();

			double delta = 1e-5;
			Assert.AreEqual(0.85531, qgpSuppressionFactors[(int)BottomiumState.Y1S], delta);
			Assert.AreEqual(0.58711, qgpSuppressionFactors[(int)BottomiumState.x1P], delta);
			Assert.AreEqual(0.59037, qgpSuppressionFactors[(int)BottomiumState.Y2S], delta);
			Assert.AreEqual(0.34551, qgpSuppressionFactors[(int)BottomiumState.x2P], delta);
			Assert.AreEqual(0.33990, qgpSuppressionFactors[(int)BottomiumState.Y3S], delta);
			Assert.AreEqual(0.19744, qgpSuppressionFactors[(int)BottomiumState.x3P], delta);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double BreakupTemperature = 160;

		private static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

		private static Dictionary<BottomiumState, double> FormationTimes
		{
			get
			{
				Dictionary<BottomiumState, double> formationTimes
					= new Dictionary<BottomiumState, double>();

				foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
				{
					formationTimes.Add(state, 0.4);
				}

				return formationTimes;
			}
		}

		private static FireballParam CreateFireballParam_PbPb()
		{
			FireballParam param = new FireballParam();

			param.BreakupTemperature_MeV = BreakupTemperature;
			param.CenterOfMassEnergyTeV = 2.76;
			param.DecayWidthRetrievalFunction = DummyDecayWidthProvider.GetDummyDecayWidth;
			param.DiffusenessA_fm = 0.546;
			param.DiffusenessB_fm = 0.546;
			param.ExpansionMode = ExpansionMode.Transverse;
			param.FormationTimes_fm = FormationTimes;
			param.GridCellSize_fm = 0.4;
			param.GridRadius_fm = 10;
			param.ImpactParameter_fm = 1.5;
			param.InitialMaximumTemperature_MeV = 550;
			param.NuclearRadiusA_fm = 6.62;
			param.NuclearRadiusB_fm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;
			param.ThermalTime_fm = 0.1;
			param.TransverseMomentaGeV = new List<double> { 6 };
			param.UseElectricField = false;
			param.UseMagneticField = false;

			return param;
		}

		private static FireballParam CreateFireballParam_pPb()
		{
			FireballParam param = CreateFireballParam_PbPb();

			param.CenterOfMassEnergyTeV = 5.02;
			param.DiffusenessA_fm = 0;
			param.GridCellSize_fm = 0.2;
			param.GridRadius_fm = 5;
			param.NuclearRadiusA_fm = 0.8775;
			param.NucleonNumberA = 1;
			param.NucleusShapeA = NucleusShape.GaussianDistribution;
			param.ProtonNumberA = 1;
			param.TemperatureProfile = TemperatureProfile.NmixALICE13;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/
		private Fireball Fireball;

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
