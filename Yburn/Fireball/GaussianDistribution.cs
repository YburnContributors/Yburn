using System;

namespace Yburn.Fireball
{
	public class GaussianDistribution : DensityFunction
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public GaussianDistribution(
			double nuclearRadius,
			int nucleonNumber
			) : base(nuclearRadius, nucleonNumber)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override double GetColumnDensity(
			double x,
			double y
			)
		{
			double r = Math.Sqrt(x * x + y * y);
			return Math.Sqrt(2 * Math.PI * Math.Pow(NuclearRadius, 2)) * Value(r);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// in fm^3
		protected override double CalculateVolumeIntegral()
		{
			return Math.Pow(2 * Math.PI * NuclearRadius * NuclearRadius, 1.5);
		}

		protected override double UnnormalizedDensity(
			double radius
			)
		{
			return Math.Exp(-0.5 * Math.Pow(radius / NuclearRadius, 2));
		}
	}
}
