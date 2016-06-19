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

		public static double NormalDistributionProbabilityDensity(
			double x,
			double expectationValue,
			double standardDeviation
			)
		{
			double normalization = Math.Sqrt(2 * Math.PI) * standardDeviation;
			double exponent = -0.5 * Math.Pow((x - expectationValue) / standardDeviation, 2);

			return Math.Exp(exponent) / normalization;
		}
	}
}
