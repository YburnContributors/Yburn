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
			NormalizationConstant = 1;

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
			CreateDensityFunctionPair(param, out densityA, out densityB);

			densityA.NormalizeTo(param.NucleonNumberA);
			densityB.NormalizeTo(param.NucleonNumberB);
		}

		public static void CreateProtonDensityFunctionPair(
			FireballParam param,
			out NuclearDensityFunction densityA,
			out NuclearDensityFunction densityB
			)
		{
			CreateDensityFunctionPair(param, out densityA, out densityB);

			densityA.NormalizeTo(param.ProtonNumberA);
			densityB.NormalizeTo(param.ProtonNumberB);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void CreateDensityFunctionPair(
			FireballParam param,
			out NuclearDensityFunction densityA,
			out NuclearDensityFunction densityB
			)
		{
			densityA = CreateDensityFunction(
				param.ShapeFunctionTypeA,
				param.NuclearRadiusAFm,
				param.DiffusenessAFm);

			densityB = CreateDensityFunction(
				param.ShapeFunctionTypeB,
				param.NuclearRadiusBFm,
				param.DiffusenessBFm);
		}

		private static NuclearDensityFunction CreateDensityFunction(
			ShapeFunctionType ShapeFunctionType,
			double NuclearRadiusFm,
			double DiffusenessFm
			)
		{
			switch(ShapeFunctionType)
			{
				case ShapeFunctionType.WoodsSaxonPotential:
					return new WoodsSaxonPotential(
						NuclearRadiusFm, DiffusenessFm);

				case ShapeFunctionType.GaussianDistribution:
					return new GaussianDistribution(
						NuclearRadiusFm);

				default:
					throw new Exception("Invalid ShapeFunctionType.");
			}
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// in fm
		public double NuclearRadius
		{
			get; protected set;
		}

		public void NormalizeTo(
			double normalizationValue
			)
		{
			if(normalizationValue <= 0)
			{
				throw new Exception("Normalization <= 0.");
			}

			NormalizationConstant = normalizationValue / CalculateVolumeIntegral();
		}

		// in fm^-3
		public double Value(
			double radius
			)
		{
			return NormalizationConstant * UnnormalizedDensity(radius);
		}

		public virtual double GetColumnDensity(
			double x,
			double y
			)
		{
			IntegrandIn1D integrand = z => Value(Math.Sqrt(x * x + y * y + z * z));
			double integral = Quadrature.UseGaussLegendre_PositiveAxis(integrand, NuclearRadius);

			// factor two because integral runs from minus to plus infinity
			return 2 * integral;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// in fm^-3
		protected double NormalizationConstant;

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
			IntegrandIn1D integrand = r => r * r * UnnormalizedDensity(r);
			double integral = Quadrature.UseGaussLegendre_PositiveAxis(integrand, NuclearRadius);

			return 4 * Math.PI * integral;
		}

		protected abstract double UnnormalizedDensity(
			double radius
			);

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

			protected override double UnnormalizedDensity(
				double radius
				)
			{
				return 1.0 / (Math.Exp((radius - NuclearRadius) / Diffuseness) + 1.0);
			}
		}

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
				return Math.Pow(2 * Math.PI * NuclearRadius * NuclearRadius, 1.5);
			}

			protected override double UnnormalizedDensity(
				double radius
				)
			{
				return Functions.UnnormalizedGaussianDistribution(radius, 0, NuclearRadius);
			}
		}
	}
}