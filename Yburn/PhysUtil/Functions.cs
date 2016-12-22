using MathNet.Numerics.Integration;
using Meta.Numerics.Functions;
using System;

namespace Yburn.PhysUtil
{
	public static class Functions
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double Artanh(
			double x
			)
		{
			return 0.5 * Math.Log((1.0 + x) / (1.0 - x));
		}

		/// <summary>
		/// Returns the integral of 0.5 * Θ(A + B*x) over the interval [-1,1].
		/// </summary>
		public static double AveragedHeavisideStepFunctionWithLinearArgument(
			double A,
			double B
			)
		{
			if(B == 0)
			{
				return HeavisideStepFunction(A);
			}
			else if(double.IsInfinity(A))
			{
				return HeavisideStepFunction(A);
			}
			else
			{
				B = Math.Abs(B);

				return HeavisideStepFunction(A - B)
					+ 0.5 * (A / B + 1) * HeavisideStepFunction(B - Math.Abs(A));
			}
		}

		/// <summary>
		/// Returns the integral of 0.25 * Θ(A + B*x + C*y) over the interval [-1,1]×[-1,1].
		/// </summary>
		public static double AveragedHeavisideStepFunctionWithLinearArgument(
			double A,
			double B,
			double C
			)
		{
			if(C == 0)
			{
				return AveragedHeavisideStepFunctionWithLinearArgument(A, B);
			}
			else if(double.IsInfinity(A))
			{
				return HeavisideStepFunction(A);
			}
			else if(double.IsInfinity(B))
			{
				return AveragedHeavisideStepFunctionWithLinearArgument(A, B);
			}
			else if(double.IsInfinity(C))
			{
				return AveragedHeavisideStepFunctionWithLinearArgument(A, C);
			}
			else
			{
				B = Math.Abs(B);
				C = Math.Abs(C);

				Func<double, double> integrand
					= y => ((A + C * y) / B + 1) * HeavisideStepFunction(B - Math.Abs(A + C * y));

				return HeavisideStepFunction(A - B - C)
					+ 0.5 * ((A - B) / C + 1) * HeavisideStepFunction(C - Math.Abs(A - B))
					+ 0.25 * GaussLegendreRule.Integrate(integrand, -1, 1, 10);
			}
		}

		public static double HeavisideStepFunction(
			double x
			)
		{
			return 0.5 * (1 + Math.Sign(x));
		}

		public static double GaussianDistributionUnnormalized(
			double x,
			double standardDeviation
			)
		{
			return Math.Exp(-0.5 * (x / standardDeviation) * (x / standardDeviation));
		}

		public static double GaussianDistributionNormalizingConstant1D(
			double standardDeviation
			)
		{
			return Math.Sqrt(2.0 * Math.PI) * standardDeviation;
		}

		public static double GaussianDistributionNormalized1D(
			double x,
			double standardDeviation
			)
		{
			return GaussianDistributionUnnormalized(x, standardDeviation)
				/ GaussianDistributionNormalizingConstant1D(standardDeviation);
		}

		public static double GaussianDistributionNormalizingConstant3D(
			double standardDeviation
			)
		{
			return Math.Pow(2.0 * Math.PI * standardDeviation * standardDeviation, 1.5);
		}

		public static double GaussianDistributionNormalized3D(
			double r,
			double standardDeviation
			)
		{
			return GaussianDistributionUnnormalized(r, standardDeviation)
				/ GaussianDistributionNormalizingConstant3D(standardDeviation);
		}

		public static double WoodsSaxonPotentialUnnormalized(
			double r,
			double nuclearRadius,
			double diffuseness
			)
		{
			return 1.0 / (1.0 + Math.Exp((r - nuclearRadius) / diffuseness));
		}

		public static double WoodsSaxonPotentialNormalizingConstant3D(
			double nuclearRadius,
			double diffuseness
			)
		{
			return -8.0 * Math.PI * diffuseness * diffuseness * diffuseness
				* AdvancedMath.PolyLog(3, -Math.Exp(nuclearRadius / diffuseness));
		}

		public static double WoodsSaxonPotentialNormalized3D(
			double r,
			double nuclearRadius,
			double diffuseness
			)
		{
			return WoodsSaxonPotentialUnnormalized(r, nuclearRadius, diffuseness)
				/ WoodsSaxonPotentialNormalizingConstant3D(nuclearRadius, diffuseness);
		}
	}
}
