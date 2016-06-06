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
			BeamRapidity = param.BeamRapidity;

			PointChargeEMF = PointChargeElectromagneticField.Create(param);

			DensityFunction.CreateProtonDensityFunctionPair(
				param, out ProtonDensityFunctionA, out ProtonDensityFunctionB);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public EuclideanVector3D CalculateMagneticField(
			double time,
			EuclideanVector3D position
			)
		{
			EuclideanVector2D nucleusPosition = new EuclideanVector2D(ImpactParameter / 2, 0);
			EuclideanVector2D positionInReactionPlane = new EuclideanVector2D(position.X, position.Y);

			// Nucleus A is located at negative x and moves in positive z direction
			EuclideanVector3D fieldNucleusA = CalculateSingleNucleusMagneticField(
				time - position.Z / ParticleVelocity,
				positionInReactionPlane + nucleusPosition,
				ProtonDensityFunctionA);

			// Nucleus B is located at positive x and moves in negative z direction
			EuclideanVector3D fieldNucleusB = CalculateSingleNucleusMagneticField(
				time + position.Z / ParticleVelocity,
				positionInReactionPlane - nucleusPosition,
				ProtonDensityFunctionB);

			return fieldNucleusA + fieldNucleusB;
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticField(
			double effectiveTime,
			EuclideanVector2D positionInReactionPlane,
			DensityFunction protonDensityFunction
			)
		{
			TwoVariableIntegrandVectorValued<EuclideanVector2D> integrand = (x, y) =>
				protonDensityFunction.GetColumnDensity(x, y)
				* PointChargeEMF.CalculatePointChargeMagneticField(
					effectiveTime,
					positionInReactionPlane - new EuclideanVector2D(x, y),
					ParticleLorentzFactor);

			EuclideanVector2D integral = Quadrature.UseGaussLegendreOverAllQuadrants(
				integrand, protonDensityFunction.NuclearRadius);

			return new EuclideanVector3D(integral, 0);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly DensityFunction ProtonDensityFunctionA;

		private readonly DensityFunction ProtonDensityFunctionB;

		private readonly PointChargeElectromagneticField PointChargeEMF;

		private readonly double ImpactParameter;

		private readonly double BeamRapidity;

		private double ParticleVelocity
		{
			get
			{
				return Math.Tanh(BeamRapidity);
			}
		}

		private double ParticleLorentzFactor
		{
			get
			{
				return 1 / Math.Sqrt(1 - ParticleVelocity * ParticleVelocity);
			}
		}
	}
}
