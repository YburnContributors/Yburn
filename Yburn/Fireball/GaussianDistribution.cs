using System;

namespace Yburn.Fireball
{
	public class GaussianDistribution
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public GaussianDistribution(
			double rmsRadius,
			int nucleonNumber
			)
		{
			RmsRadius = rmsRadius;
			NucleonNumber = nucleonNumber;
			NormalizationConstant = 1;

			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// in fm^-3
		public double Value(
			double radius
			)
		{
			return NormalizationConstant * UnnormalizedGaussian(radius);
		}

		public void NormalizeTo(
			double normalizationValue
			)
		{
			NormalizationConstant = normalizationValue / CalculateVolumeIntegral();
		}

		public double GetColumnDensity(
			double x,
			double y
			)
		{
			double r = Math.Sqrt(x * x + y * y);
			return Math.Sqrt(2 * Math.PI * Math.Pow(RmsRadius, 2)) * Value(r);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// in fm
		private double RmsRadius;

		private int NucleonNumber;

		// in fm^-3
		private double NormalizationConstant;

		private void AssertValidMembers()
		{
			if(NucleonNumber <= 0)
			{
				throw new Exception("NucleonNumber <= 0.");
			}

			if(RmsRadius <= 0)
			{
				throw new Exception("RmsRadius <= 0.");
			}
		}

		// in fm^3
		private double CalculateVolumeIntegral()
		{
			return Math.Pow(2 * Math.PI * Math.Pow(RmsRadius, 2), 1.5);
		}

		private double UnnormalizedGaussian(
			double radius
			)
		{
			return Math.Exp(-Math.Pow(radius / RmsRadius, 2) / 2);
		}
	}
}
