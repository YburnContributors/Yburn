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
				param.QGPConductivity_MeV,
				param.PartonPeakRapidity,
				nucleusA,
				param.EMFQuadratureOrder);

			NucleusEMFB = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivity_MeV,
				-param.PartonPeakRapidity,
				nucleusB,
				param.EMFQuadratureOrder);

			LCFFieldAverager = new LCFFieldAverager(param);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SpatialVector CalculateElectricField_per_fm2(
			double t,
			double x,
			double y,
			double z
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricField_per_fm2(
				t, x - NucleusPositionA, y, z);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricField_per_fm2(
				t, x - NucleusPositionB, y, z);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateElectricFieldInLCF_per_fm2(
			double properTime,
			double x,
			double y,
			double rapidity
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricFieldInLCF_per_fm2(
				properTime, x - NucleusPositionA, y, rapidity);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricFieldInLCF_per_fm2(
				properTime, x - NucleusPositionB, y, rapidity);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticField_per_fm2(
			double t,
			double x,
			double y,
			double z
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticField_per_fm2(
				t, x - NucleusPositionA, y, z);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticField_per_fm2(
				t, x - NucleusPositionB, y, z);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticFieldInLCF_per_fm2(
			double properTime,
			double x,
			double y,
			double rapidity
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticFieldInLCF_per_fm2(
				properTime, x - NucleusPositionA, y, rapidity);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticFieldInLCF_per_fm2(
				properTime, x - NucleusPositionB, y, rapidity);

			return fieldNucleusA + fieldNucleusB;
		}

		public double CalculateAverageElectricFieldStrength_per_fm2(
			double properTime,
			double x,
			double y
			)
		{
			Func<double, double> integrand
				= rapidity => CalculateElectricFieldInLCF_per_fm2(properTime, x, y, rapidity).Norm;

			return LCFFieldAverager.AverageRapidityDependence(integrand);
		}

		public double CalculateAverageElectricFieldStrength_per_fm2(
			double properTime
			)
		{
			LCFFieldFunction function = (x, y, rapidity) =>
				CalculateElectricFieldInLCF_per_fm2(properTime, x, y, rapidity).Norm;

			return LCFFieldAverager.AverageByBottomiumDistribution(function);
		}

		public double CalculateAverageMagneticFieldStrength_per_fm2(
			double properTime,
			double x,
			double y
			)
		{
			Func<double, double> integrand
				= rapidity => CalculateMagneticFieldInLCF_per_fm2(properTime, x, y, rapidity).Norm;

			return LCFFieldAverager.AverageRapidityDependence(integrand);
		}

		public double CalculateAverageMagneticFieldStrength_per_fm2(
			double properTime
			)
		{
			LCFFieldFunction function = (x, y, rapidity) =>
				CalculateMagneticFieldInLCF_per_fm2(properTime, x, y, rapidity).Norm;

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
