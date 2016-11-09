using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public delegate double LCFFieldFunction(double x, double y, double rapidity);

	public class LCFFieldAverager
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double AverageByBottomiumDistribution(
			LCFFieldFunction function,
			FireballParam param,
			int quadratureOrder
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(param);

			List<double> x = param.GenerateDiscreteXAxis();
			List<double> y = param.GenerateDiscreteYAxis();

			double[,] functionColumnDensityValues = new double[x.Count, y.Count];
			for(int i = 0; i < x.Count; i++)
			{
				for(int j = 0; j < y.Count; j++)
				{
					Func<double, double> integrand = rapidity => function(x[i], y[j], rapidity)
						* Functions.GaussianDistributionNormalized1D(
							rapidity, RapidityDistributionWidth);

					functionColumnDensityValues[i, j] =
						Quadrature.IntegrateOverRealAxis(
							integrand, 2 * RapidityDistributionWidth, quadratureOrder)
						* glauber.NcollField[i, j];
				}
			}

			SimpleFireballField functionColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, functionColumnDensityValues);

			return functionColumnDensity.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double RapidityDistributionWidth = 2.7;

		private static readonly double BottomQuarkMagneton_Fm = 0.5 * Constants.ChargeBottomQuark
			* Constants.HbarCMeVFm / Constants.RestMassBottomQuarkMeV;

		//private static readonly double TeslaFm2 = 5.017029326E-15;

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public LCFFieldAverager(
			FireballParam param
			)
		{
			Param = param.Clone();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double CalculateAverageElectricFieldStrengthPerFm2(
			double properTimeFm,
			int quadratureOrder
			)
		{
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);
			LCFFieldFunction function = (x, y, rapidity) =>
				emf.CalculateElectricFieldPerFm2_LCF(properTimeFm, x, y, rapidity, quadratureOrder).Norm;

			return AverageByBottomiumDistribution(function, Param, quadratureOrder);
		}

		public double CalculateAverageMagneticFieldStrengthPerFm2(
			double properTimeFm,
			int quadratureOrder
			)
		{
			FireballElectromagneticField emf = new FireballElectromagneticField(Param);
			LCFFieldFunction function = (x, y, rapidity) =>
				emf.CalculateMagneticFieldPerFm2_LCF(properTimeFm, x, y, rapidity, quadratureOrder).Norm;

			return AverageByBottomiumDistribution(function, Param, quadratureOrder);
		}

		public double CalculateAverageSpinStateOverlap(
			double properTimeFm,
			int quadratureOrder
			)
		{
			double B_PerFmSquared =
				CalculateAverageMagneticFieldStrengthPerFm2(properTimeFm, quadratureOrder);

			double HyperfineEnergySplitting_MeV = Constants.RestMassY2SMeV - Constants.RestMassEtab2SMeV;

			double x = 4 * BottomQuarkMagneton_Fm * B_PerFmSquared * Constants.HbarCMeVFm / HyperfineEnergySplitting_MeV;
			double y = x / (1 + Math.Sqrt(1 + x * x));
			double mixingCoefficient = y / Math.Sqrt(1 + y * y);

			return mixingCoefficient * mixingCoefficient;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}