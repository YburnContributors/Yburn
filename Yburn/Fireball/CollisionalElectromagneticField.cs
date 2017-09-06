using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class CollisionalElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public CollisionalElectromagneticField(
			FireballParam param
			)
		{
			NucleusPositionA = param.NucleusPositionA;
			NucleusPositionB = param.NucleusPositionB;

			Nucleus.CreateNucleusPair(param, out Nucleus nucleusA, out Nucleus nucleusB);

			NucleusEMFA = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				param.PartonPeakRapidity,
				nucleusA,
				param.EMFQuadratureOrder);

			NucleusEMFB = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				-param.PartonPeakRapidity,
				nucleusB,
				param.EMFQuadratureOrder);

			LCFFieldAverager = new LCFFieldAverager(param);
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

		public double CalculateAverageElectricFieldStrengthPerFm2(
			double properTime,
			double x,
			double y
			)
		{
			Func<double, double> integrand
				= rapidity => CalculateElectricFieldPerFm2_LCF(properTime, x, y, rapidity).Norm;

			return LCFFieldAverager.AverageRapidityDependence(integrand);
		}

		public double CalculateAverageElectricFieldStrengthPerFm2(
			double properTime
			)
		{
			LCFFieldFunction function = (x, y, rapidity) =>
				CalculateElectricFieldPerFm2_LCF(properTime, x, y, rapidity).Norm;

			return LCFFieldAverager.AverageByBottomiumDistribution(function);
		}

		public double CalculateAverageMagneticFieldStrengthPerFm2(
			double properTime,
			double x,
			double y
			)
		{
			Func<double, double> integrand
				= rapidity => CalculateMagneticFieldPerFm2_LCF(properTime, x, y, rapidity).Norm;

			return LCFFieldAverager.AverageRapidityDependence(integrand);
		}

		public double CalculateAverageMagneticFieldStrengthPerFm2(
			double properTime
			)
		{
			LCFFieldFunction function = (x, y, rapidity) =>
				CalculateMagneticFieldPerFm2_LCF(properTime, x, y, rapidity).Norm;

			return LCFFieldAverager.AverageByBottomiumDistribution(function);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double NucleusPositionA;

		private readonly double NucleusPositionB;

		private readonly NucleusElectromagneticField NucleusEMFA;

		private readonly NucleusElectromagneticField NucleusEMFB;

		private readonly LCFFieldAverager LCFFieldAverager;
	}
}
