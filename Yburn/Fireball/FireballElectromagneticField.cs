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

			DensityFunction.CreateProtonDensityFunctionPair(
				param, out ProtonDensityFunctionA, out ProtonDensityFunctionB);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public EuclideanVector3D CalculateMagneticField(
			double time,
			EuclideanVector3D position,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			EuclideanVector3D nucleusOffset = new EuclideanVector3D(
				0.5 * Param.ImpactParameterFm, 0, 0);

			// Nucleus A is located at negative x and moves in positive z direction
			EuclideanVector3D fieldNucleusA = CalculateSingleNucleusMagneticField(
				time,
				position + nucleusOffset,
				Param.ParticleVelocity,
				ProtonDensityFunctionA,
				precision);

			// Nucleus B is located at positive x and moves in negative z direction
			EuclideanVector3D fieldNucleusB = CalculateSingleNucleusMagneticField(
				time,
				position - nucleusOffset,
				-Param.ParticleVelocity,
				ProtonDensityFunctionA,
				precision);

			return fieldNucleusA + fieldNucleusB;
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticField(
			double time,
			EuclideanVector3D position,
			double nucleusVelocity,
			DensityFunction protonDensityFunction,
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

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;

		private DensityFunction ProtonDensityFunctionA;

		private DensityFunction ProtonDensityFunctionB;
	}
}
