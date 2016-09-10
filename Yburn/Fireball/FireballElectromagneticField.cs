﻿using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class FireballElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballElectromagneticField(
			FireballParam param
			)
		{
			ImpactParameterFm = param.ImpactParameterFm;

			NuclearDensityFunction densityA;
			NuclearDensityFunction densityB;
			NuclearDensityFunction.CreateProtonDensityFunctionPair(
				param, out densityA, out densityB);

			NucleusEMFA = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				param.BeamRapidity,
				densityA);

			NucleusEMFB = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				-param.BeamRapidity,
				densityB);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SpatialVector CalculateMagneticField(
			double t,
			double x,
			double y,
			double z,
			int quadratureOrder
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticField(
				t,
				x + 0.5 * ImpactParameterFm,
				y,
				z,
				quadratureOrder);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticField(
				t,
				x - 0.5 * ImpactParameterFm,
				y,
				z,
				quadratureOrder);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticField_LCF(
			double properTime,
			double x,
			double y,
			double rapidity,
			int quadratureOrder
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticField_LCF(
				properTime,
				x + 0.5 * ImpactParameterFm,
				y,
				rapidity,
				quadratureOrder);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticField_LCF(
				properTime,
				x - 0.5 * ImpactParameterFm,
				y,
				rapidity,
				quadratureOrder);

			return fieldNucleusA + fieldNucleusB;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double ImpactParameterFm;

		private NucleusElectromagneticField NucleusEMFA;

		private NucleusElectromagneticField NucleusEMFB;
	}
}