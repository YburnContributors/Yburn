using Meta.Numerics.Functions;
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

        public static double GaussianDistributionUnnormalized(
            double x,
            double standardDeviation
            )
        {
            return Math.Exp(-0.5 * (x / standardDeviation) * (x / standardDeviation));
        }

        public static double GaussianDistributionNormalizingConstant1D(
            double standardDeviation
            )
        {
            return Math.Sqrt(2.0 * Math.PI) * standardDeviation;
        }

        public static double GaussianDistributionNormalized1D(
            double x,
            double standardDeviation
            )
        {
            return GaussianDistributionUnnormalized(x, standardDeviation)
                / GaussianDistributionNormalizingConstant1D(standardDeviation);
        }

        public static double GaussianDistributionNormalizingConstant3D(
            double standardDeviation
            )
        {
            return Math.Pow(2.0 * Math.PI * standardDeviation * standardDeviation, 1.5);
        }

        public static double GaussianDistributionNormalized3D(
            double r,
            double standardDeviation
            )
        {
            return GaussianDistributionUnnormalized(r, standardDeviation)
                / GaussianDistributionNormalizingConstant3D(standardDeviation);
        }

        public static double WoodsSaxonPotentialUnnormalized(
            double r,
            double nuclearRadius,
            double diffuseness
            )
        {
            return 1.0 / (1.0 + Math.Exp((r - nuclearRadius) / diffuseness));
        }

        public static double WoodsSaxonPotentialNormalizingConstant3D(
            double nuclearRadius,
            double diffuseness
            )
        {
            return -8.0 * Math.PI * diffuseness * diffuseness * diffuseness
                * AdvancedMath.PolyLog(3, -Math.Exp(nuclearRadius / diffuseness));
        }

        public static double WoodsSaxonPotentialNormalized3D(
            double r,
            double nuclearRadius,
            double diffuseness
            )
        {
            return WoodsSaxonPotentialUnnormalized(r, nuclearRadius, diffuseness)
                / WoodsSaxonPotentialNormalizingConstant3D(nuclearRadius, diffuseness);
        }
    }
}
