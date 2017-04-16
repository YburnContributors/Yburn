using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class CollisionalElectromagneticField
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double CalculateNucleusPeakRapidity(
			double beamRapidity,
			uint nucleonNumber
			)
		{
			return 1 / (1 + 0.2) * (beamRapidity - Math.Log(nucleonNumber) / 6) - 0.2;
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public CollisionalElectromagneticField(
			FireballParam param
			)
		{
			NucleusPositionA = param.NucleusPositionA;
			NucleusPositionB = param.NucleusPositionB;

			Nucleus nucleusA;
			Nucleus nucleusB;
			Nucleus.CreateNucleusPair(
				param, out nucleusA, out nucleusB);

			NucleusEMFA = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				CalculateNucleusPeakRapidity(param.BeamRapidity, param.NucleonNumberB),
				nucleusA,
				param.EMFQuadratureOrder);

			NucleusEMFB = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				-CalculateNucleusPeakRapidity(param.BeamRapidity, param.NucleonNumberA),
				nucleusB,
				param.EMFQuadratureOrder);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SpatialVector CalculateElectricFieldPerFm2(
			double t,
			double x,
			double y,
			double z
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricFieldPerFm2(
				t, x - NucleusPositionA, y, z);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricFieldPerFm2(
				t, x - NucleusPositionB, y, z);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateElectricFieldPerFm2_LCF(
			double properTime,
			double x,
			double y,
			double rapidity
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricFieldPerFm2_LCF(
				properTime, x - NucleusPositionA, y, rapidity);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricFieldPerFm2_LCF(
				properTime, x - NucleusPositionB, y, rapidity);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticFieldPerFm2(
			double t,
			double x,
			double y,
			double z
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticFieldPerFm2(
				t, x - NucleusPositionA, y, z);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticFieldPerFm2(
				t, x - NucleusPositionB, y, z);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticFieldPerFm2_LCF(
			double properTime,
			double x,
			double y,
			double rapidity
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticFieldPerFm2_LCF(
				properTime, x - NucleusPositionA, y, rapidity);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticFieldPerFm2_LCF(
				properTime, x - NucleusPositionB, y, rapidity);

			return fieldNucleusA + fieldNucleusB;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double NucleusPositionA;

		private readonly double NucleusPositionB;

		private readonly NucleusElectromagneticField NucleusEMFA;

		private readonly NucleusElectromagneticField NucleusEMFB;
	}
}