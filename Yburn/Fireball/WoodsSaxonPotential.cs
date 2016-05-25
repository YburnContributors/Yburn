using System;

namespace Yburn.Fireball
{
	public class WoodsSaxonPotential : DensityFunction
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public WoodsSaxonPotential(
			double nuclearRadius,
			double diffuseness,
			int nucleonNumber
			) : base(nuclearRadius, nucleonNumber)
		{
			Diffuseness = diffuseness;

			AssertValidDiffuseness();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// in fm
		public double Diffuseness
		{
			get; protected set;
		}


		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected void AssertValidDiffuseness()
		{
			if(Diffuseness <= 0)
			{
				throw new Exception("Diffuseness <= 0.");
			}
		}

		protected override double UnnormalizedDensity(
			double radius
			)
		{
			return 1.0 / (Math.Exp((radius - NuclearRadius) / Diffuseness) + 1.0);
		}
	}
}