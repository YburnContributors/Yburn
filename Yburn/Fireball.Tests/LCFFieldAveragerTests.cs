﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class LCFFieldAveragerTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateAverageElectricFieldStrength()
		{
			LCFFieldAverager averager = new LCFFieldAverager(CreateFireballParam());

			double result = averager.CalculateAverageElectricFieldStrengthPerFm2(0.4);

			AssertHelper.AssertApproximatelyEqual(0.17670, result, 5);
		}

		[TestMethod]
		public void CalculateAverageMagneticFieldStrength()
		{
			LCFFieldAverager averager = new LCFFieldAverager(CreateFireballParam());

			double result = averager.CalculateAverageMagneticFieldStrengthPerFm2(0.4);

			AssertHelper.AssertApproximatelyEqual(0.23606, result, 5);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.CenterOfMassEnergyTeV = 2.76;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.EMFQuadratureOrder = 8;
			param.GridCellSizeFm = 1;
			param.GridRadiusFm = 10;
			param.ImpactParameterFm = 7;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.QGPConductivityMeV = 5.8;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;

			return param;
		}
	}
}
