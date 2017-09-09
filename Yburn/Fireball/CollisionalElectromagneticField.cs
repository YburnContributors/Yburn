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
				param.PartonPeakRapidity,
				nucleusA,
				param.EMFQuadratureOrder);

			NucleusEMFB = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				-param.PartonPeakRapidity,
				nucleusB,
				param.EMFQuadratureOrder);

			LCFFieldAverager = new LCFFieldAverager(param);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SpatialVector CalculateElectricField(
			double t_fm,
			double x_fm,
			double y_fm,
			double z_fm,
			double conductivity_MeV
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricField(
				t_fm, x_fm - NucleusPositionA, y_fm, z_fm, conductivity_MeV);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricField(
				t_fm, x_fm - NucleusPositionB, y_fm, z_fm, conductivity_MeV);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateElectricFieldInLCF(
			double properTime_fm,
			double x_fm,
			double y_fm,
			double rapidity,
			double conductivity_MeV
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricFieldInLCF(
				properTime_fm, x_fm - NucleusPositionA, y_fm, rapidity, conductivity_MeV);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricFieldInLCF(
				properTime_fm, x_fm - NucleusPositionB, y_fm, rapidity, conductivity_MeV);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticField(
			double t_fm,
			double x_fm,
			double y_fm,
			double z_fm,
			double conductivity_MeV
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticField(
				t_fm, x_fm - NucleusPositionA, y_fm, z_fm, conductivity_MeV);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticField(
				t_fm, x_fm - NucleusPositionB, y_fm, z_fm, conductivity_MeV);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticFieldInLCF(
			double properTime_fm,
			double x_fm,
			double y_fm,
			double rapidity,
			double conductivity_MeV
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticFieldInLCF(
				properTime_fm, x_fm - NucleusPositionA, y_fm, rapidity, conductivity_MeV);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticFieldInLCF(
				properTime_fm, x_fm - NucleusPositionB, y_fm, rapidity, conductivity_MeV);

			return fieldNucleusA + fieldNucleusB;
		}

		public double CalculateAverageElectricFieldStrength(
			double properTime_fm,
			double x_fm,
			double y_fm,
			double conductivity_MeV
			)
		{
			Func<double, double> integrand = rapidity => CalculateElectricFieldInLCF(
				properTime_fm, x_fm, y_fm, rapidity, conductivity_MeV).Norm;

			return LCFFieldAverager.AverageRapidityDependence(integrand);
		}

		public double CalculateAverageElectricFieldStrength(
			double properTime_fm,
			double conductivity_MeV
			)
		{
			LCFFieldFunction function = (x_fm, y_fm, rapidity) => CalculateElectricFieldInLCF(
				properTime_fm, x_fm, y_fm, rapidity, conductivity_MeV).Norm;

			return LCFFieldAverager.AverageByBottomiumDistribution(function);
		}

		public double CalculateAverageMagneticFieldStrength(
			double properTime_fm,
			double x_fm,
			double y_fm,
			double conductivity_MeV
			)
		{
			Func<double, double> integrand = rapidity => CalculateMagneticFieldInLCF(
				properTime_fm, x_fm, y_fm, rapidity, conductivity_MeV).Norm;

			return LCFFieldAverager.AverageRapidityDependence(integrand);
		}

		public double CalculateAverageMagneticFieldStrength(
			double properTime_fm,
			double conductivity_MeV
			)
		{
			LCFFieldFunction function = (x_fm, y_fm, rapidity) => CalculateMagneticFieldInLCF(
				properTime_fm, x_fm, y_fm, rapidity, conductivity_MeV).Norm;

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
