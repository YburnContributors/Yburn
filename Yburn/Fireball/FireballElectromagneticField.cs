using Yburn.PhysUtil;

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

			Nucleus nucleusA;
			Nucleus nucleusB;
			Nucleus.CreateNucleusPair(
				param, out nucleusA, out nucleusB);

			NucleusEMFA = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				param.BeamRapidity,
				nucleusA);

			NucleusEMFB = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				-param.BeamRapidity,
				nucleusB);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SpatialVector CalculateElectricFieldPerFm2(
			double t,
			double x,
			double y,
			double z,
			int quadratureOrder
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricFieldPerFm2(
				t,
				x + 0.5 * ImpactParameterFm,
				y,
				z,
				quadratureOrder);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricFieldPerFm2(
				t,
				x - 0.5 * ImpactParameterFm,
				y,
				z,
				quadratureOrder);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateElectricFieldPerFm2_LCF(
			double properTime,
			double x,
			double y,
			double rapidity,
			int quadratureOrder
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateElectricFieldPerFm2_LCF(
				properTime,
				x + 0.5 * ImpactParameterFm,
				y,
				rapidity,
				quadratureOrder);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateElectricFieldPerFm2_LCF(
				properTime,
				x - 0.5 * ImpactParameterFm,
				y,
				rapidity,
				quadratureOrder);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticFieldPerFm2(
			double t,
			double x,
			double y,
			double z,
			int quadratureOrder
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticFieldPerFm2(
				t,
				x + 0.5 * ImpactParameterFm,
				y,
				z,
				quadratureOrder);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticFieldPerFm2(
				t,
				x - 0.5 * ImpactParameterFm,
				y,
				z,
				quadratureOrder);

			return fieldNucleusA + fieldNucleusB;
		}

		public SpatialVector CalculateMagneticFieldPerFm2_LCF(
			double properTime,
			double x,
			double y,
			double rapidity,
			int quadratureOrder
			)
		{
			// Nucleus A is located at negative x and moves in positive z direction
			SpatialVector fieldNucleusA = NucleusEMFA.CalculateMagneticFieldPerFm2_LCF(
				properTime,
				x + 0.5 * ImpactParameterFm,
				y,
				rapidity,
				quadratureOrder);

			// Nucleus B is located at positive x and moves in negative z direction
			SpatialVector fieldNucleusB = NucleusEMFB.CalculateMagneticFieldPerFm2_LCF(
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