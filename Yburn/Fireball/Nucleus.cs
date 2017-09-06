using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum NucleusShape
	{
		WoodsSaxonPotential,
		GaussianDistribution
	};

	public abstract class Nucleus
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void CreateNucleusPair(
			FireballParam param,
			out Nucleus nucleusA,
			out Nucleus nucleusB
			)
		{
			nucleusA = CreateNucleus(
				shape: param.NucleusShapeA,
				nucleonNumber: param.NucleonNumberA,
				protonNumber: param.ProtonNumberA,
				nuclearRadius_fm: param.NuclearRadiusA_fm,
				diffuseness_fm: param.DiffusenessA_fm);

			nucleusB = CreateNucleus(
				shape: param.NucleusShapeB,
				nucleonNumber: param.NucleonNumberB,
				protonNumber: param.ProtonNumberB,
				nuclearRadius_fm: param.NuclearRadiusB_fm,
				diffuseness_fm: param.DiffusenessB_fm);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static Nucleus CreateNucleus(
			NucleusShape shape,
			uint nucleonNumber,
			uint protonNumber,
			double nuclearRadius_fm,
			double diffuseness_fm
			)
		{
			Nucleus nucleus;

			switch(shape)
			{
				case NucleusShape.WoodsSaxonPotential:
					nucleus = new WoodsSaxonNucleus(
						nucleonNumber: nucleonNumber,
						protonNumber: protonNumber,
						nuclearRadius_fm: nuclearRadius_fm,
						diffuseness_fm: diffuseness_fm);
					break;

				case NucleusShape.GaussianDistribution:
					nucleus = new GaussianNucleus(
						nucleonNumber: nucleonNumber,
						protonNumber: protonNumber,
						nuclearRadius_fm: nuclearRadius_fm);
					break;

				default:
					throw new Exception("Invalid NucleusShape.");
			}

			return nucleus;
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected Nucleus(
			uint nucleonNumber,
			uint protonNumber,
			double nuclearRadius_fm,
			double normalizingConstant_fm3
			)
		{
			NucleonNumber = nucleonNumber;
			ProtonNumber = protonNumber;
			NuclearRadius_fm = nuclearRadius_fm;
			NormalizingConstant_fm3 = normalizingConstant_fm3;

			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly uint NucleonNumber;

		public readonly uint ProtonNumber;

		public readonly double NuclearRadius_fm;

		public double GetNucleonNumberDensity_per_fm3(
			double radius_fm
			)
		{
			return NucleonNumber * UnnormalizedDensity(radius_fm) / NormalizingConstant_fm3;
		}

		public double GetNucleonNumberColumnDensity_per_fm3(
			double x_fm,
			double y_fm
			)
		{
			return NucleonNumber * UnnormalizedColumnDensity(x_fm, y_fm) / NormalizingConstant_fm3;
		}

		public double GetProtonNumberDensity_per_fm3(
			double radius_fm
			)
		{
			return ProtonNumber * UnnormalizedDensity(radius_fm) / NormalizingConstant_fm3;
		}

		public double GetProtonNumberColumnDensity_per_fm3(
			double x_fm,
			double y_fm
			)
		{
			return ProtonNumber * UnnormalizedColumnDensity(x_fm, y_fm) / NormalizingConstant_fm3;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double NormalizingConstant_fm3;

		private void AssertValidMembers()
		{
			if(NuclearRadius_fm <= 0)
			{
				throw new Exception("NuclearRadius <= 0.");
			}
			if(NormalizingConstant_fm3 <= 0)
			{
				throw new Exception("NormalizingConstant <= 0.");
			}
		}

		protected abstract double UnnormalizedDensity(
			double radius_fm
			);

		protected virtual double UnnormalizedColumnDensity(
			double x_fm,
			double y_fm
			)
		{
			Func<double, double> integrand
				= z => UnnormalizedDensity(Math.Sqrt(x_fm * x_fm + y_fm * y_fm + z * z));

			double integral
				= ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 2 * NuclearRadius_fm, 64);

			// factor two because integral runs from minus to plus infinity
			return 2 * integral;
		}

		private class GaussianNucleus : Nucleus
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public GaussianNucleus(
				uint nucleonNumber,
				uint protonNumber,
				double nuclearRadius_fm
				) : base(
					nucleonNumber: nucleonNumber,
					protonNumber: protonNumber,
					nuclearRadius_fm: nuclearRadius_fm,
					normalizingConstant_fm3: Functions.GaussianDistributionNormalizingConstant3D(nuclearRadius_fm)
					)
			{
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			protected override double UnnormalizedDensity(
				double radius_fm
				)
			{
				return Functions.GaussianDistributionUnnormalized(radius_fm, NuclearRadius_fm);
			}

			protected override double UnnormalizedColumnDensity(
				double x_fm,
				double y_fm
				)
			{
				return Math.Sqrt(2 * Math.PI * NuclearRadius_fm * NuclearRadius_fm)
					* UnnormalizedDensity(Math.Sqrt(x_fm * x_fm + y_fm * y_fm));
			}
		}

		private class WoodsSaxonNucleus : Nucleus
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public WoodsSaxonNucleus(
				uint nucleonNumber,
				uint protonNumber,
				double nuclearRadius_fm,
				double diffuseness_fm
				) : base(
					nucleonNumber: nucleonNumber,
					protonNumber: protonNumber,
					nuclearRadius_fm: nuclearRadius_fm,
					normalizingConstant_fm3: Functions.WoodsSaxonPotentialNormalizingConstant3D(nuclearRadius_fm, diffuseness_fm)
					)
			{
				Diffuseness_fm = diffuseness_fm;

				AssertValidDiffuseness();
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public readonly double Diffuseness_fm;

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			protected void AssertValidDiffuseness()
			{
				if(Diffuseness_fm <= 0)
				{
					throw new Exception("Diffuseness <= 0.");
				}
			}

			protected override double UnnormalizedDensity(
				double radius_fm
				)
			{
				return Functions.WoodsSaxonPotentialUnnormalized(radius_fm, NuclearRadius_fm, Diffuseness_fm);
			}
		}
	}
}
