﻿using System;

namespace Yburn.Fireball
{
	public abstract class DensityFunction
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected DensityFunction(
			double nuclearRadius,
			int nucleonNumber
			)
		{
			NuclearRadius = nuclearRadius;
			NucleonNumber = nucleonNumber;
			NormalizationConstant = 1;

			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void CreatePair(
			FireballParam param,
			out DensityFunction densityA,
			out DensityFunction densityB
			)
		{
			switch(param.ShapeFunctionA)
			{
				case ShapeFunction.WoodsSaxonPotential:
					densityA = new WoodsSaxonPotential(
						param.NuclearRadiusAFm, param.DiffusenessAFm, param.NucleonNumberA);
					break;

				case ShapeFunction.GaussianDistribution:
					densityA = new GaussianDistribution(
						param.NuclearRadiusAFm, param.NucleonNumberA);
					break;

				default:
					throw new Exception("Invalid ShapeFunctionA.");
			}

			switch(param.ShapeFunctionB)
			{
				case ShapeFunction.WoodsSaxonPotential:
					densityB = new WoodsSaxonPotential(
						param.NuclearRadiusBFm, param.DiffusenessBFm, param.NucleonNumberB);
					break;

				case ShapeFunction.GaussianDistribution:
					densityB = new GaussianDistribution(
						param.NuclearRadiusBFm, param.NucleonNumberB);
					break;

				default:
					throw new Exception("Invalid ShapeFunctionB.");
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

		public int NucleonNumber
		{
			get; protected set;
		}

		public void NormalizeTo(
			double normalizationValue
			)
		{
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
			OneVariableIntegrand integrand = z => Value(Math.Sqrt(x * x + y * y + z * z));
			double integral = Quadrature.UseGaussLegendreOverPositiveAxis(integrand, NuclearRadius);

			// factor two because integral runs from minus to plus infinity
			return 2 * integral;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// in fm^-3
		protected double NormalizationConstant;

		protected void AssertValidMembers()
		{
			if(NucleonNumber <= 0)
			{
				throw new Exception("NucleonNumber <= 0.");
			}

			if(NuclearRadius <= 0)
			{
				throw new Exception("NuclearRadius <= 0.");
			}
		}

		// in fm^3
		protected virtual double CalculateVolumeIntegral()
		{
			OneVariableIntegrand integrand = r => r * r * UnnormalizedDensity(r);
			double integral = Quadrature.UseGaussLegendreOverPositiveAxis(integrand, NuclearRadius);

			return 4 * Math.PI * integral;
		}

		protected abstract double UnnormalizedDensity(
			double radius
			);
	}
}