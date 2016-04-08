using System;

namespace Yburn.Fireball
{
	public class WoodsSaxonPotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public WoodsSaxonPotential(
			double nuclearRadius,
			double diffuseness,
			int nucleonNumber
			)
		{
			NuclearRadius = nuclearRadius;
			Diffuseness = diffuseness;
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
			return NormalizationConstant * UnnormalizedPotential(radius);
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
			double h = 0.1 * Diffuseness;
			double temp = 1;
			double z = h;
			double r2 = x * x + y * y;
			double integral = 0.5 * Value(Math.Sqrt(r2));
			while(temp >= 1e-12)
			{
				temp = Value(Math.Sqrt(r2 + z * z));
				integral += temp;
				z += h;
			}

			// factor two because integral runs from minus to plus infinity
			return 2 * integral * h;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// in fm
		private double NuclearRadius;

		// in fm
		private double Diffuseness;

		private int NucleonNumber;

		// in fm^-3
		private double NormalizationConstant;

		private void AssertValidMembers()
		{
			if(NucleonNumber <= 0)
			{
				throw new Exception("NucleonNumber <= 0.");
			}

			if(Diffuseness <= 0)
			{
				throw new Exception("Diffuseness <= 0.");
			}

			if(NuclearRadius <= 0)
			{
				throw new Exception("NuclearRadius <= 0.");
			}
		}

		// in fm^3
		private double CalculateVolumeIntegral()
		{
			double h = 0.1 * Diffuseness;
			double temp = 1;
			double integral = 0;
			double r = h;
			while(temp >= 1e-12)
			{
				temp = r * r * UnnormalizedPotential(r);
				integral += temp;
				r += h;
			}

			return integral * h * 4 * PhysConst.PI;
		}

		private double UnnormalizedPotential(
			double radius
			)
		{
			return 1.0 / (Math.Exp((radius - NuclearRadius) / Diffuseness) + 1.0);
		}
	}
}