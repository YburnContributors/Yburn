using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum ShapeFunctionType
	{
		WoodsSaxonPotential,
		GaussianDistribution
	};

	public abstract class NuclearDensityFunction
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected NuclearDensityFunction(
			double nuclearRadius
			)
		{
			NormalizingFactor = 1;

			NuclearRadius = nuclearRadius;
			AssertValidNuclearRadius();
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void CreateNucleonDensityFunctionPair(
			FireballParam param,
			out NuclearDensityFunction densityA,
			out NuclearDensityFunction densityB
			)
		{
			densityA = CreateDensityFunction(
				param.ShapeFunctionTypeA,
				param.NucleonNumberA,
				param.NuclearRadiusAFm,
				param.DiffusenessAFm);

			densityB = CreateDensityFunction(
				param.ShapeFunctionTypeB,
				param.NucleonNumberB,
				param.NuclearRadiusBFm,
				param.DiffusenessBFm);
		}

		public static void CreateProtonDensityFunctionPair(
			FireballParam param,
			out NuclearDensityFunction densityA,
			out NuclearDensityFunction densityB
			)
		{
			densityA = CreateDensityFunction(
				param.ShapeFunctionTypeA,
				param.ProtonNumberA,
				param.NuclearRadiusAFm,
				param.DiffusenessAFm);

			densityB = CreateDensityFunction(
				param.ShapeFunctionTypeB,
				param.ProtonNumberB,
				param.NuclearRadiusBFm,
				param.DiffusenessBFm);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static NuclearDensityFunction CreateDensityFunction(
			ShapeFunctionType shapeFunctionType,
			double normalization,
			double nuclearRadiusFm,
			double diffusenessFm
			)
		{
			NuclearDensityFunction density;

			switch(shapeFunctionType)
			{
				case ShapeFunctionType.WoodsSaxonPotential:
					density = new WoodsSaxonPotential(nuclearRadiusFm, diffusenessFm);
					break;

				case ShapeFunctionType.GaussianDistribution:
					density = new GaussianDistribution(nuclearRadiusFm);
					break;

				default:
					throw new Exception("Invalid ShapeFunctionType.");
			}

			density.NormalizeTo(normalization);

			return density;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double Normalization
		{
			get; protected set;
		}

		// in fm
		public double NuclearRadius
		{
			get; protected set;
		}

		public void NormalizeTo(
			double normalization
			)
		{
			if(normalization <= 0)
			{
				throw new Exception("Normalization <= 0.");
			}

			Normalization = normalization;
			NormalizingFactor = normalization / CalculateVolumeIntegral();
		}

		// in fm^-3
		public double Value(
			double radius
			)
		{
			return NormalizingFactor * UnnormalizedDensity(radius);
		}

		public virtual double GetColumnDensity(
			double x,
			double y
			)
		{
			Func<double, double> integrand = z => Value(Math.Sqrt(x * x + y * y + z * z));
			double integral = Quadrature.IntegrateOverPositiveAxis(integrand, 2 * NuclearRadius, 64);

			// factor two because integral runs from minus to plus infinity
			return 2 * integral;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// in fm^-3
		protected double NormalizingFactor;

		protected void AssertValidNuclearRadius()
		{
			if(NuclearRadius <= 0)
			{
				throw new Exception("NuclearRadius <= 0.");
			}
		}

		// in fm^3
		protected virtual double CalculateVolumeIntegral()
		{
			Func<double, double> integrand = r => r * r * UnnormalizedDensity(r);
			double integral = Quadrature.IntegrateOverPositiveAxis(integrand, 2 * NuclearRadius, 64);

			return 4 * Math.PI * integral;
		}

		protected abstract double UnnormalizedDensity(
			double radius
			);

		private class GaussianDistribution : NuclearDensityFunction
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public GaussianDistribution(
				double nuclearRadius
				) : base(nuclearRadius)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override double GetColumnDensity(
				double x,
				double y
				)
			{
				double r = Math.Sqrt(x * x + y * y);
				return Math.Sqrt(2 * Math.PI * NuclearRadius * NuclearRadius) * Value(r);
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			// in fm^3
			protected override double CalculateVolumeIntegral()
			{
				return Functions.GaussianDistributionNormalizingConstant3D(NuclearRadius);
			}

			protected override double UnnormalizedDensity(
				double radius
				)
			{
				return Functions.GaussianDistributionUnnormalized(radius, NuclearRadius);
			}
		}

		private class WoodsSaxonPotential : NuclearDensityFunction
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public WoodsSaxonPotential(
				double nuclearRadius,
				double diffuseness
				) : base(nuclearRadius)
			{
				Diffuseness = diffuseness;

				AssertValidDiffuseness();
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			// in fm
			public double Diffuseness
			{
				get; protected set;
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			protected void AssertValidDiffuseness()
			{
				if(Diffuseness <= 0)
				{
					throw new Exception("Diffuseness <= 0.");
				}
			}

			// in fm^3
			protected override double CalculateVolumeIntegral()
			{
				return Functions.WoodsSaxonPotentialNormalizingConstant3D(NuclearRadius, Diffuseness);
			}

			protected override double UnnormalizedDensity(
				double radius
				)
			{
				return Functions.WoodsSaxonPotentialUnnormalized(radius, NuclearRadius, Diffuseness);
			}
		}
	}
}