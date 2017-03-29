using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public delegate double LCFFieldFunction(double x, double y, double rapidity);

	public class LCFFieldAverager
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double AverageRapidityDependence(
			Func<double, double> function,
			int quadratureOrder
			)
		{
			Func<double, double> integrand = rapidity => function(rapidity)
				* Functions.GaussianDistributionNormalized1D(rapidity, RapidityDistributionWidth);

			return ImproperQuadrature.IntegrateOverRealAxis(
				integrand, 2 * RapidityDistributionWidth, quadratureOrder);
		}

		public static double AverageByBottomiumDistribution(
			LCFFieldFunction function,
			FireballParam param
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(param);

			double[] x = param.XAxis;
			double[] y = param.YAxis;

			double[,] functionColumnDensityValues = new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					Func<double, double> integrand = rapidity => function(x[i], y[j], rapidity);

					functionColumnDensityValues[i, j]
						= AverageRapidityDependence(integrand, param.EMFQuadratureOrder)
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

		//private static readonly double TeslaFm2 = 5.017029326E-15;

		private static double CalculateSpinStateOverlap(
			BottomiumState tripletState,
			double magneticFieldStrength
			)
		{
			double[] HyperfineEnergySplitting_MeV = GetHyperfineEnergySplitting(tripletState);
			int Jmax = HyperfineEnergySplitting_MeV.Length - 1;

			double result = 0;

			for(int J = 0; J <= Jmax; J++)
			{
				double helper1 = 4 * Constants.MagnetonBottomQuarkFm * magneticFieldStrength
					* Constants.HbarCMeVFm / HyperfineEnergySplitting_MeV[J];
				double helper2 = helper1 / (1 + Math.Sqrt(1 + helper1 * helper1));

				double coefficient = helper2 * helper2 / (1 + helper2 * helper2);

				result += (2 * J + 1) * coefficient;
			}

			return result / (Jmax + 1) / (Jmax + 1);
		}

		private static double[] GetHyperfineEnergySplitting(
			BottomiumState tripletState
			)
		{
			switch(tripletState)
			{
				case BottomiumState.Y1S:
					return new double[] { Constants.RestMassY1SMeV - Constants.RestMassEta1SMeV };

				case BottomiumState.x1P:
					return new double[] {
						Constants.RestMassX1P0MeV - Constants.RestMassH1PMeV,
						Constants.RestMassX1P1MeV - Constants.RestMassH1PMeV,
						Constants.RestMassX1P2MeV - Constants.RestMassH1PMeV
					};

				case BottomiumState.Y2S:
					return new double[] { Constants.RestMassY2SMeV - Constants.RestMassEta2SMeV };

				case BottomiumState.x2P:
					return new double[] {
						Constants.RestMassX2P0MeV - Constants.RestMassH2PMeV,
						Constants.RestMassX2P1MeV - Constants.RestMassH2PMeV,
						Constants.RestMassX2P2MeV - Constants.RestMassH2PMeV
					};

				case BottomiumState.Y3S:
				case BottomiumState.x3P:
				default:
					throw new Exception("Invalid BottomiumState.");
			}
		}

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
			double properTimeFm
			)
		{
			CollisionalElectromagneticField emf = new CollisionalElectromagneticField(Param);
			LCFFieldFunction function = (x, y, rapidity) =>
				emf.CalculateElectricFieldPerFm2_LCF(properTimeFm, x, y, rapidity).Norm;

			return AverageByBottomiumDistribution(function, Param);
		}

		public double CalculateAverageMagneticFieldStrengthPerFm2(
			double properTimeFm
			)
		{
			CollisionalElectromagneticField emf = new CollisionalElectromagneticField(Param);
			LCFFieldFunction function = (x, y, rapidity) =>
				emf.CalculateMagneticFieldPerFm2_LCF(properTimeFm, x, y, rapidity).Norm;

			return AverageByBottomiumDistribution(function, Param);
		}

		public double CalculateAverageSpinStateOverlap(
			BottomiumState tripletState,
			double properTimeFm
			)
		{
			LCFFieldFunction mixingCoefficientSquared = (x, y, rapidity) =>
			{
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(Param);
				double B_PerFm2 = emf.CalculateMagneticFieldPerFm2_LCF(
					properTimeFm, x, y, rapidity).Norm;

				return CalculateSpinStateOverlap(tripletState, B_PerFm2);
			};

			return AverageByBottomiumDistribution(mixingCoefficientSquared, Param);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}