namespace Yburn.Fireball
{
	public class MagneticFieldStrengthAverager
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public MagneticFieldStrengthAverager(
			FireballParam param
			)
		{
			Param = param.Clone();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double CalculateAverageMagneticFieldStrength(
			QuadraturePrecision precision
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			double[,] nCollFieldValues = glauber.NcollField.GetDiscreteValues();
			double[] x = Param.GenerateDiscreteXAxis();
			double[] y = Param.GenerateDiscreteYAxis();
			double formationTimeFm = Param.FormationTimesFm[0];
			double mediumExpanseFm = Param.ParticleVelocity * formationTimeFm;

			double[,] magneticFieldValues =
				new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					IntegrandIn1D integrand = z => emf.CalculateMagneticField(
						formationTimeFm,
						new EuclideanVector3D(x[i], y[j], z),
						precision).Norm;

					magneticFieldValues[i, j] = Quadrature.UseGaussLegendre(
						integrand, -mediumExpanseFm, mediumExpanseFm, precision);
				}
			}

			SimpleFireballField magneticField = new SimpleFireballField(
				FireballFieldType.Ncoll, magneticFieldValues);

			double integral = 2 * Param.GridCellSizeFm * Param.GridCellSizeFm
				* magneticField.TrapezoidalRuleSummedValues();

			if(Param.AreParticlesABIdentical)
			{
				integral *= 2;
			}
			integral /= mediumExpanseFm * glauber.GetTotalNumberCollisions();

			return integral;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}