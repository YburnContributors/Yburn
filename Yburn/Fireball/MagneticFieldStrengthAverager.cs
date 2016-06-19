using Yburn.PhysUtil;

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

		public double CalculateAverageMagneticFieldStrengthInLabFrame(
			QuadraturePrecision precision
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			double[] x = Param.GenerateDiscreteXAxis();
			double[] y = Param.GenerateDiscreteYAxis();
			double formationTimeFm = Param.FormationTimesFm[0];
			double mediumExpanseFm = Param.ParticleVelocity * formationTimeFm;

			double[,] magneticFieldColumnDensityValues = new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					IntegrandIn1D integrand = z => emf.CalculateMagneticField(
						formationTimeFm,
						new EuclideanVector3D(x[i], y[j], z),
						precision).Norm;

					magneticFieldColumnDensityValues[i, j] = Quadrature.UseGaussLegendre(
							integrand, -mediumExpanseFm, mediumExpanseFm, precision);

					magneticFieldColumnDensityValues[i, j] *= glauber.NcollField[i, j];
				}
			}

			SimpleFireballField magneticFieldColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, magneticFieldColumnDensityValues);

			return magneticFieldColumnDensity.TrapezoidalRuleSummedValues()
				/ (2 * mediumExpanseFm * glauber.NcollField.TrapezoidalRuleSummedValues());
		}

		public double CalculateAverageMagneticFieldStrengthInLCF(
			QuadraturePrecision precision
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			double[] x = Param.GenerateDiscreteXAxis();
			double[] y = Param.GenerateDiscreteYAxis();
			double formationTimeFm = Param.FormationTimesFm[0];

			double[,] magneticFieldColumnDensityValues = new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					IntegrandIn1D integrand = rapidity => emf.CalculateMagneticField_LCF(
							formationTimeFm,
							new EuclideanVector2D(x[i], y[j]),
							rapidity,
							precision).Norm
						* Functions.NormalDistributionProbabilityDensity(
							rapidity, 0, RapidityDistributionWidth);

					magneticFieldColumnDensityValues[i, j] =
						Quadrature.UseGaussLegendre_RealAxis(
							integrand, Param.ParticleVelocity, precision)
						* glauber.NcollField[i, j];
				}
			}

			SimpleFireballField magneticFieldColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, magneticFieldColumnDensityValues);

			return magneticFieldColumnDensity.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double RapidityDistributionWidth = 2.7;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}