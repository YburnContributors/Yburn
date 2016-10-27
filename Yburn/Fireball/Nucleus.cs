﻿using System;
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
				nuclearRadiusFm: param.NuclearRadiusAFm,
				diffusenessFm: param.DiffusenessAFm);

			nucleusB = CreateNucleus(
				shape: param.NucleusShapeB,
				nucleonNumber: param.NucleonNumberB,
				protonNumber: param.ProtonNumberB,
				nuclearRadiusFm: param.NuclearRadiusBFm,
				diffusenessFm: param.DiffusenessBFm);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static Nucleus CreateNucleus(
			NucleusShape shape,
			int nucleonNumber,
			int protonNumber,
			double nuclearRadiusFm,
			double diffusenessFm
			)
		{
			Nucleus nucleus;

			switch(shape)
			{
				case NucleusShape.WoodsSaxonPotential:
					nucleus = new WoodsSaxonNucleus(
						nucleonNumber: nucleonNumber,
						protonNumber: protonNumber,
						nuclearRadiusFm: nuclearRadiusFm,
						diffusenessFm: diffusenessFm);
					break;

				case NucleusShape.GaussianDistribution:
					nucleus = new GaussianNucleus(
						nucleonNumber: nucleonNumber,
						protonNumber: protonNumber,
						nuclearRadiusFm: nuclearRadiusFm);
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
			int nucleonNumber,
			int protonNumber,
			double nuclearRadiusFm,
			double normalizingConstantFm3
			)
		{
			NucleonNumber = nucleonNumber;
			ProtonNumber = protonNumber;
			NuclearRadiusFm = nuclearRadiusFm;
			NormalizingConstantFm3 = normalizingConstantFm3;

			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly int NucleonNumber;

		public readonly int ProtonNumber;

		public readonly double NuclearRadiusFm;

		public double GetNucleonNumberDensityPerFm3(
			double radiusFm
			)
		{
			return NucleonNumber * UnnormalizedDensity(radiusFm) / NormalizingConstantFm3;
		}

		public double GetNucleonNumberColumnDensityPerFm3(
			double xFm,
			double yFm
			)
		{
			return NucleonNumber * UnnormalizedColumnDensity(xFm, yFm) / NormalizingConstantFm3;
		}

		public double GetProtonNumberDensityPerFm3(
			double radiusFm
			)
		{
			return ProtonNumber * UnnormalizedDensity(radiusFm) / NormalizingConstantFm3;
		}

		public double GetProtonNumberColumnDensityPerFm3(
			double xFm,
			double yFm
			)
		{
			return ProtonNumber * UnnormalizedColumnDensity(xFm, yFm) / NormalizingConstantFm3;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double NormalizingConstantFm3;

		private void AssertValidMembers()
		{
			if(NucleonNumber <= 0)
			{
				throw new Exception("NucleonNumber <= 0.");
			}
			if(ProtonNumber <= 0)
			{
				throw new Exception("ProtonNumber <= 0.");
			}
			if(NuclearRadiusFm <= 0)
			{
				throw new Exception("NuclearRadius <= 0.");
			}
			if(NormalizingConstantFm3 <= 0)
			{
				throw new Exception("NormalizingConstant <= 0.");
			}
		}

		protected abstract double UnnormalizedDensity(
			double radiusFm
			);

		protected virtual double UnnormalizedColumnDensity(
			double xFm,
			double yFm
			)
		{
			Func<double, double> integrand =
				z => UnnormalizedDensity(Math.Sqrt(xFm * xFm + yFm * yFm + z * z));

			double integral =
				Quadrature.IntegrateOverPositiveAxis(integrand, 2 * NuclearRadiusFm, 64);

			// factor two because integral runs from minus to plus infinity
			return 2 * integral;
		}

		private class GaussianNucleus : Nucleus
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public GaussianNucleus(
				int nucleonNumber,
				int protonNumber,
				double nuclearRadiusFm
				) : base(
					nucleonNumber: nucleonNumber,
					protonNumber: protonNumber,
					nuclearRadiusFm: nuclearRadiusFm,
					normalizingConstantFm3: Functions.GaussianDistributionNormalizingConstant3D(nuclearRadiusFm)
					)
			{
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			protected override double UnnormalizedDensity(
				double radiusFm
				)
			{
				return Functions.GaussianDistributionUnnormalized(radiusFm, NuclearRadiusFm);
			}

			protected override double UnnormalizedColumnDensity(
				double xFm,
				double yFm
				)
			{
				return Math.Sqrt(2 * Math.PI * NuclearRadiusFm * NuclearRadiusFm)
					* UnnormalizedDensity(Math.Sqrt(xFm * xFm + yFm * yFm));
			}
		}

		private class WoodsSaxonNucleus : Nucleus
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public WoodsSaxonNucleus(
				int nucleonNumber,
				int protonNumber,
				double nuclearRadiusFm,
				double diffusenessFm
				) : base(
					nucleonNumber: nucleonNumber,
					protonNumber: protonNumber,
					nuclearRadiusFm: nuclearRadiusFm,
					normalizingConstantFm3: Functions.WoodsSaxonPotentialNormalizingConstant3D(nuclearRadiusFm, diffusenessFm)
					)
			{
				DiffusenessFm = diffusenessFm;

				AssertValidDiffuseness();
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public readonly double DiffusenessFm;

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			protected void AssertValidDiffuseness()
			{
				if(DiffusenessFm <= 0)
				{
					throw new Exception("Diffuseness <= 0.");
				}
			}

			protected override double UnnormalizedDensity(
				double radiusFm
				)
			{
				return Functions.WoodsSaxonPotentialUnnormalized(radiusFm, NuclearRadiusFm, DiffusenessFm);
			}
		}
	}
}