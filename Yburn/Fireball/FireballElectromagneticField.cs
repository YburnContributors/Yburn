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
			Param = param.Clone();

			NuclearDensityFunction.CreateProtonDensityFunctionPair(
				param, out ProtonDensityFunctionA, out ProtonDensityFunctionB);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public EuclideanVector3D CalculateMagneticFieldInCMS(
			double time,
			EuclideanVector3D position,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			EuclideanVector3D nucleusOffset = new EuclideanVector3D(
				0.5 * Param.ImpactParameterFm, 0, 0);

			// Nucleus A is located at negative x and moves in positive z direction
			EuclideanVector3D fieldNucleusA = CalculateSingleNucleusMagneticFieldInCMS(
				time,
				position + nucleusOffset,
				Param.ParticleVelocity,
				ProtonDensityFunctionA,
				precision);

			// Nucleus B is located at positive x and moves in negative z direction
			EuclideanVector3D fieldNucleusB = CalculateSingleNucleusMagneticFieldInCMS(
				time,
				position - nucleusOffset,
				-Param.ParticleVelocity,
				ProtonDensityFunctionA,
				precision);

			return fieldNucleusA + fieldNucleusB;
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticFieldInCMS(
			double time,
			EuclideanVector3D position,
			double nucleusVelocity,
			NuclearDensityFunction protonDensityFunction,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				Param.EMFCalculationMethod, Param.QGPConductivityMeV, nucleusVelocity);

			IntegrandIn2D<EuclideanVector3D> integrand = (x, y) =>
				protonDensityFunction.GetColumnDensity(x, y)
				* pcEMF.CalculatePointChargeMagneticField(
					time,
					position.X - x,
					position.Y - y,
					position.Z);

			EuclideanVector3D integral = Quadrature.UseGaussLegendre_RealPlane(
				integrand,
				protonDensityFunction.NuclearRadius,
				protonDensityFunction.NuclearRadius,
				precision);

			return integral;
		}

		public EuclideanVector3D CalculateMagneticFieldInLCF(
			double properTime,
			EuclideanVector2D position,
			double rapidity,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			EuclideanVector2D nucleusOffset = new EuclideanVector2D(
				0.5 * Param.ImpactParameterFm, 0);

			// Nucleus A is located at negative x and moves in positive z direction
			EuclideanVector3D fieldNucleusA = CalculateSingleNucleusMagneticFieldInLCF(
				properTime,
				position + nucleusOffset,
				rapidity,
				Param.ParticleVelocity,
				ProtonDensityFunctionA,
				precision);

			// Nucleus B is located at positive x and moves in negative z direction
			EuclideanVector3D fieldNucleusB = CalculateSingleNucleusMagneticFieldInLCF(
				properTime,
				position - nucleusOffset,
				rapidity,
				-Param.ParticleVelocity,
				ProtonDensityFunctionA,
				precision);

			return fieldNucleusA + fieldNucleusB;
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticFieldInLCF(
			double properTime,
			EuclideanVector2D position,
			double rapidity,
			double nucleusVelocity,
			NuclearDensityFunction protonDensityFunction,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			PointChargeElectromagneticField pcEMF = PointChargeElectromagneticField.Create(
				Param.EMFCalculationMethod, Param.QGPConductivityMeV, nucleusVelocity);

			IntegrandIn2D<EuclideanVector3D> integrand = (x, y) =>
				protonDensityFunction.GetColumnDensity(x, y)
				* pcEMF.CalculatePointChargeMagneticField_LCF(
					properTime,
					position.X - x,
					position.Y - y,
					rapidity);

			EuclideanVector3D integral = Quadrature.UseGaussLegendre_RealPlane(
				integrand,
				protonDensityFunction.NuclearRadius,
				protonDensityFunction.NuclearRadius,
				precision);

			return integral;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;

		private NuclearDensityFunction ProtonDensityFunctionA;

		private NuclearDensityFunction ProtonDensityFunctionB;
	}
}
