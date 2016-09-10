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

		public double CalculateAverageMagneticFieldStrength(
			int quadratureOrder
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			double[] x = Param.GenerateDiscreteXAxis();
			double[] y = Param.GenerateDiscreteYAxis();
			double formationTimeFm = Param.FormationTimesFm[0];
			double mediumExpanseFm = Math.Tanh(Param.BeamRapidity) * formationTimeFm;

			double[,] magneticFieldStrengthColumnDensityValues = new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					Func<double, double> integrand = z => emf.CalculateMagneticField(
						formationTimeFm, x[i], y[j], z, quadratureOrder).Norm;

					magneticFieldStrengthColumnDensityValues[i, j] =
						Quadrature.IntegrateOverInterval(
							integrand, -mediumExpanseFm, mediumExpanseFm, quadratureOrder);

					magneticFieldStrengthColumnDensityValues[i, j] *= glauber.NcollField[i, j];
				}
			}

			SimpleFireballField magneticFieldStrengthColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, magneticFieldStrengthColumnDensityValues);

			return magneticFieldStrengthColumnDensity.TrapezoidalRuleSummedValues()
				/ (2 * mediumExpanseFm * glauber.NcollField.TrapezoidalRuleSummedValues());
		}

		public double CalculateAverageMagneticFieldStrength_LCF(
			int quadratureOrder
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
					Func<double, double> integrand = rapidity => emf.CalculateMagneticField_LCF(
							formationTimeFm, x[i], y[j], rapidity, quadratureOrder).Norm
						* Functions.GaussianDistributionNormalized1D(
							rapidity, RapidityDistributionWidth);

					magneticFieldStrengthColumnDensityValues[i, j] =
						Quadrature.IntegrateOverRealAxis(
							integrand, 2 * RapidityDistributionWidth, quadratureOrder)
						* glauber.NcollField[i, j];
				}
			}

			SimpleFireballField magneticFieldStrengthColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, magneticFieldStrengthColumnDensityValues);

			return magneticFieldStrengthColumnDensity.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		public double CalculateSpinStateOverlap(
			int quadratureOrder
			)
		{
			double B_PerFmSquared = CalculateAverageMagneticFieldStrength_LCF(quadratureOrder);

			double HyperfineEnergySplitting_MeV = Constants.Y2SMassMeV - Constants.Etab2SMassMeV;

			double x = 4 * BottomQuarkMagneton_Fm * B_PerFmSquared * Constants.HbarCMeVFm / HyperfineEnergySplitting_MeV;
			double y = x / (1 + Math.Sqrt(1 + x * x));
			double mixingCoefficient = y / Math.Sqrt(1 + y * y);

			return mixingCoefficient * mixingCoefficient;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double RapidityDistributionWidth = 2.7;

		private static readonly double BottomQuarkMagneton_Fm = 0.5 * Constants.BottomQuarkCharge
			* Constants.HbarCMeVFm / Constants.BottomQuarkMassMeV;

		//private static readonly double TeslaFmFm = 5.017029326E-15;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}