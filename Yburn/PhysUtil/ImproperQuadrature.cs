using MathNet.Numerics.Integration;
using System;

namespace Yburn.PhysUtil
{
	public static class ImproperQuadrature
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double IntegrateOverPositiveAxis(
			Func<double, double> integrand,
			double approxSupportRadius,
			int order
			)
		{
			Func<double, double> transformedIntegrand
				= MapIntegrandToUnitInterval(integrand, approxSupportRadius);

			return GaussLegendreRule.Integrate(transformedIntegrand, 0, 1, order);
		}

		public static double IntegrateOverRealAxis(
			Func<double, double> integrand,
			double approxSupportRadius,
			int order
			)
		{
			Func<double, double> transformedIntegrand
				= MapIntegrandToUnitInterval(integrand, approxSupportRadius);

			return GaussLegendreRule.Integrate(transformedIntegrand, -1, 1, order);
		}

		public static double IntegrateOverRealPlane(
			Func<double, double, double> integrand,
			double approxSupportRadius,
			int order
			)
		{
			Func<double, double, double> transformedIntegrand
				= MapIntegrandToUnitInterval(integrand, approxSupportRadius);

			return GaussLegendreRule.Integrate(transformedIntegrand, -1, 1, -1, 1, order);
		}

		/********************************************************************************************
         * Private/protected static members, functions and properties
         ********************************************************************************************/

		private static Func<double, double> MapIntegrandToUnitInterval(
			Func<double, double> integrand,
			double approxSupportRadius
			)
		{
			return x => integrand(approxSupportRadius * Functions.Artanh(x))
				* approxSupportRadius / (1 + x) / (1 - x);
		}

		private static Func<double, double, double> MapIntegrandToUnitInterval(
			Func<double, double, double> integrand,
			double approxSupportRadius
			)
		{
			return (x, y) =>
				integrand(
					approxSupportRadius * Functions.Artanh(x),
					approxSupportRadius * Functions.Artanh(y))
				* approxSupportRadius * approxSupportRadius / (1 + x) / (1 + y) / (1 - x) / (1 - y);
		}
	}
}
