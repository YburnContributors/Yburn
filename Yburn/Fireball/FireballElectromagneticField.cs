using System;

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
			ImpactParameter = param.ImpactParameterFm;

			PointChargeEMF = PointChargeElectromagneticField.Create(param);

			DensityFunction.CreateProtonDensityFunctionPair(
				param, out ProtonDensityFunctionA, out ProtonDensityFunctionB);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public EuclideanVector3D CalculateMagneticField(
			double time,
			EuclideanVector3D position,
			double nucleiVelocity
			)
		{
			double lorentzFactor = CalculateLorentzFactor(nucleiVelocity);
			EuclideanVector2D nucleusPosition = new EuclideanVector2D(ImpactParameter / 2, 0);
			EuclideanVector2D positionInReactionPlane = new EuclideanVector2D(position.X, position.Y);

			// Nucleus A is located at negative x and moves in positive z direction
			EuclideanVector3D fieldNucleusA = CalculateSingleNucleusMagneticField(
				time - position.Z / nucleiVelocity,
				positionInReactionPlane + nucleusPosition,
				lorentzFactor,
				ProtonDensityFunctionA);

			// Nucleus B is located at positive x and moves in negative z direction
			EuclideanVector3D fieldNucleusB = CalculateSingleNucleusMagneticField(
				time + position.Z / nucleiVelocity,
				positionInReactionPlane - nucleusPosition,
				lorentzFactor,
				ProtonDensityFunctionB);

			return fieldNucleusA + fieldNucleusB;
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticField(
			double effectiveTime,
			EuclideanVector2D positionInReactionPlane,
			double lorentzFactor,
			DensityFunction protonDensityFunction
			)
		{
			TwoVariableIntegrandVectorValued<EuclideanVector3D> integrand = (x, y) =>
				protonDensityFunction.GetColumnDensity(x, y)
				* PointChargeEMF.CalculatePointChargeMagneticField(
					effectiveTime,
					positionInReactionPlane - new EuclideanVector2D(x, y),
					lorentzFactor);

			EuclideanVector3D integral = Quadrature.UseGaussLegendreOverAllQuadrants(
				integrand, protonDensityFunction.NuclearRadius);

			return integral;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private DensityFunction ProtonDensityFunctionA;

		private DensityFunction ProtonDensityFunctionB;

		private PointChargeElectromagneticField PointChargeEMF;

		private double ImpactParameter;

		private double CalculateLorentzFactor(
			double velocity
			)
		{
			return 1 / Math.Sqrt(1 - velocity * velocity);
		}
	}
}
