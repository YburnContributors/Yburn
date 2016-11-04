using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class ElectromagneticFieldStrengthAverager
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ElectromagneticFieldStrengthAverager(
			FireballParam param
			)
		{
			Param = param.Clone();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double CalculateAverageElectricFieldStrengthPerFm2(
			double timeFm,
			int quadratureOrder
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			List<double> x = Param.GenerateDiscreteXAxis();
			List<double> y = Param.GenerateDiscreteYAxis();
			double mediumExpanseFm = Math.Tanh(Param.BeamRapidity.Value) * timeFm;

			double[,] fieldStrengthColumnDensityValuesPerFm2 = new double[x.Count, y.Count];
			for(int i = 0; i < x.Count; i++)
			{
				for(int j = 0; j < y.Count; j++)
				{
					Func<double, double> integrand = z => emf.CalculateElectricFieldPerFm2(
						timeFm, x[i], y[j], z, quadratureOrder).Norm;

					fieldStrengthColumnDensityValuesPerFm2[i, j] =
						Quadrature.IntegrateOverInterval(
							integrand, -mediumExpanseFm, mediumExpanseFm, quadratureOrder)
							/ (2 * mediumExpanseFm);

					fieldStrengthColumnDensityValuesPerFm2[i, j] *= glauber.NcollField[i, j];
				}
			}

			SimpleFireballField fieldStrengthColumnDensityPerFm2 = new SimpleFireballField(
				FireballFieldType.Ncoll, fieldStrengthColumnDensityValuesPerFm2);

			return fieldStrengthColumnDensityPerFm2.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		public double CalculateAverageElectricFieldStrengthPerFm2_LCF(
			double properTimeFm,
			int quadratureOrder
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			List<double> x = Param.GenerateDiscreteXAxis();
			List<double> y = Param.GenerateDiscreteYAxis();

			double[,] fieldStrengthColumnDensityValuesPerFm2 = new double[x.Count, y.Count];
			for(int i = 0; i < x.Count; i++)
			{
				for(int j = 0; j < y.Count; j++)
				{
					Func<double, double> integrand = rapidity => emf.CalculateElectricFieldPerFm2_LCF(
							properTimeFm, x[i], y[j], rapidity, quadratureOrder).Norm
						* Functions.GaussianDistributionNormalized1D(
							rapidity, RapidityDistributionWidth);

					fieldStrengthColumnDensityValuesPerFm2[i, j] =
						Quadrature.IntegrateOverRealAxis(
							integrand, 2 * RapidityDistributionWidth, quadratureOrder)
						* glauber.NcollField[i, j];
				}
			}

			SimpleFireballField fieldStrengthColumnDensityPerFm2 = new SimpleFireballField(
				FireballFieldType.Ncoll, fieldStrengthColumnDensityValuesPerFm2);

			return fieldStrengthColumnDensityPerFm2.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		public double CalculateAverageMagneticFieldStrengthPerFm2(
			double timeFm,
			int quadratureOrder
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			List<double> x = Param.GenerateDiscreteXAxis();
			List<double> y = Param.GenerateDiscreteYAxis();
			double mediumExpanseFm = Math.Tanh(Param.BeamRapidity.Value) * timeFm;

			double[,] fieldStrengthColumnDensityValuesPerFm2 = new double[x.Count, y.Count];
			for(int i = 0; i < x.Count; i++)
			{
				for(int j = 0; j < y.Count; j++)
				{
					Func<double, double> integrand = z => emf.CalculateMagneticFieldPerFm2(
						timeFm, x[i], y[j], z, quadratureOrder).Norm;

					fieldStrengthColumnDensityValuesPerFm2[i, j] =
						Quadrature.IntegrateOverInterval(
							integrand, -mediumExpanseFm, mediumExpanseFm, quadratureOrder)
							/ (2 * mediumExpanseFm);

					fieldStrengthColumnDensityValuesPerFm2[i, j] *= glauber.NcollField[i, j];
				}
			}

			SimpleFireballField fieldStrengthColumnDensityPerFm2 = new SimpleFireballField(
				FireballFieldType.Ncoll, fieldStrengthColumnDensityValuesPerFm2);

			return fieldStrengthColumnDensityPerFm2.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		public double CalculateAverageMagneticFieldStrengthPerFm2_LCF(
			double properTimeFm,
			int quadratureOrder
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);

			List<double> x = Param.GenerateDiscreteXAxis();
			List<double> y = Param.GenerateDiscreteYAxis();

			double[,] fieldStrengthColumnDensityValuesPerFm2 = new double[x.Count, y.Count];
			for(int i = 0; i < x.Count; i++)
			{
				for(int j = 0; j < y.Count; j++)
				{
					Func<double, double> integrand = rapidity => emf.CalculateMagneticFieldPerFm2_LCF(
							properTimeFm, x[i], y[j], rapidity, quadratureOrder).Norm
						* Functions.GaussianDistributionNormalized1D(
							rapidity, RapidityDistributionWidth);

					fieldStrengthColumnDensityValuesPerFm2[i, j] =
						Quadrature.IntegrateOverRealAxis(
							integrand, 2 * RapidityDistributionWidth, quadratureOrder)
						* glauber.NcollField[i, j];
				}
			}

			SimpleFireballField fieldStrengthColumnDensityPerFm2 = new SimpleFireballField(
				FireballFieldType.Ncoll, fieldStrengthColumnDensityValuesPerFm2);

			return fieldStrengthColumnDensityPerFm2.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		public double CalculateSpinStateOverlap(
			double properTimeFm,
			int quadratureOrder
			)
		{
			double B_PerFmSquared =
				CalculateAverageMagneticFieldStrengthPerFm2_LCF(properTimeFm, quadratureOrder);

			double HyperfineEnergySplitting_MeV = Constants.RestMassY2SMeV - Constants.RestMassEtab2SMeV;

			double x = 4 * BottomQuarkMagneton_Fm * B_PerFmSquared * Constants.HbarCMeVFm / HyperfineEnergySplitting_MeV;
			double y = x / (1 + Math.Sqrt(1 + x * x));
			double mixingCoefficient = y / Math.Sqrt(1 + y * y);

			return mixingCoefficient * mixingCoefficient;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double RapidityDistributionWidth = 2.7;

		private static readonly double BottomQuarkMagneton_Fm = 0.5 * Constants.ChargeBottomQuark
			* Constants.HbarCMeVFm / Constants.RestMassBottomQuarkMeV;

		//private static readonly double TeslaFmFm = 5.017029326E-15;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}