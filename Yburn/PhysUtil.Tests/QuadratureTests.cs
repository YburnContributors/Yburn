using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yburn.TestUtil;

namespace Yburn.PhysUtil.Tests
{
    [TestClass]
    public class QuadratureTests
    {
        /********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

        [TestMethod]
        public void IntegrateOverInterval()
        {
            Func<double, double> integrand = x => Math.Exp(x);
            double result = Quadrature.IntegrateOverInterval(integrand, 0, 1, 8);

            AssertHelper.AssertRoundedEqual(Math.E - 1, result);
        }

        [TestMethod]
        public void IntegrateOverPositiveAxis()
        {
            double diffuseness = 1;
            double nuclearRadius = 5;
            Func<double, double> integrand = r => 4 * Math.PI * r * r
                * Functions.WoodsSaxonPotentialNormalized3D(r, nuclearRadius, diffuseness);

            double result = Quadrature.IntegrateOverPositiveAxis(integrand, 2 * nuclearRadius, 64);

            AssertHelper.AssertRoundedEqual(1, result);
        }

        [TestMethod]
        public void IntegrateOverRealAxis()
        {
            Func<double, double> integrand = x => Functions.GaussianDistributionNormalized1D(x, 1);

            double result = Quadrature.IntegrateOverRealAxis(integrand, 2.7, 64);

            AssertHelper.AssertRoundedEqual(1, result);
        }

        [TestMethod]
        public void IntegrateOverInterval2D()
        {
            Func<double, double, double> integrand = (x, y) => Math.Exp(x + 0.5 * y);
            double result = Quadrature.IntegrateOverInterval(integrand, 0, 1, 0, 2, 8);

            AssertHelper.AssertRoundedEqual(2 * Math.Pow(Math.E - 1, 2), result);
        }

        [TestMethod]
        public void IntegrateOverRealPlane()
        {
            Func<double, double, double> integrand = (x, y) => Math.Exp(-x * x - y * y) / Math.PI;
            double result = Quadrature.IntegrateOverRealPlane(integrand, 1, 32);

            AssertHelper.AssertRoundedEqual(1, result, 4);
        }
    }
}