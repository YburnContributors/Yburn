using System;
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

		public double CalculateAverageMagneticFieldStrengthInCMS(
			QuadraturePrecision precision
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			double[] x = Param.GenerateDiscreteXAxis();
			double[] y = Param.GenerateDiscreteYAxis();
			double formationTimeFm = Param.FormationTimesFm[0];
			double mediumExpanseFm = Param.ParticleVelocity * formationTimeFm;

			double[,] magneticFieldStrengthColumnDensityValues = new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					IntegrandIn1D integrand = z => emf.CalculateMagneticFieldInCMS(
						formationTimeFm,
						new EuclideanVector3D(x[i], y[j], z),
						precision).Norm;

					magneticFieldStrengthColumnDensityValues[i, j] = Quadrature.UseGaussLegendre(
							integrand, -mediumExpanseFm, mediumExpanseFm, precision);

					magneticFieldStrengthColumnDensityValues[i, j] *= glauber.NcollField[i, j];
				}
			}

			SimpleFireballField magneticFieldStrengthColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, magneticFieldStrengthColumnDensityValues);

			return magneticFieldStrengthColumnDensity.TrapezoidalRuleSummedValues()
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

			double[,] magneticFieldStrengthColumnDensityValues = new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					IntegrandIn1D integrand = rapidity => emf.CalculateMagneticFieldInLCF(
							formationTimeFm,
							new EuclideanVector2D(x[i], y[j]),
							rapidity,
							precision).Norm
						* Functions.NormalDistribution(
							rapidity, 0, RapidityDistributionWidth);

					magneticFieldStrengthColumnDensityValues[i, j] =
						Quadrature.UseGaussLegendre_RealAxis(
							integrand, 0.9 * RapidityDistributionWidth, precision)
						* glauber.NcollField[i, j];
				}
			}

			SimpleFireballField magneticFieldStrengthColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, magneticFieldStrengthColumnDensityValues);

			return magneticFieldStrengthColumnDensity.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		public double CalculateOverlapBetweenParaAndShiftedOrthoState(
			QuadraturePrecision precision
			)
		{
			double B = CalculateAverageMagneticFieldStrengthInLCF(precision);

			double x = 4 * BottomiumMagnetonFm * B / (BottomiumHyperfineEnergyMeV / PhysConst.HBARC);
			double y = x / (1 + Math.Sqrt(1 + x * x));
			double sin = y / Math.Sqrt(1 + y * y);

			return sin * sin;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double RapidityDistributionWidth = 2.7;

		private static readonly double ProtonMassMeV = 938.272;

		private static readonly double NuclearMagnetonFm = 0.5 * PhysConst.ElementaryCharge / ProtonMassMeV * PhysConst.HBARC;

		private static readonly double BottomiumMagnetonFm = -0.066 * NuclearMagnetonFm;

		private static readonly double BottomiumHyperfineEnergyMeV = 71.4;

		//private static readonly double TeslaFmFm = 5.017029326E-15;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}