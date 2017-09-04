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
		public void IntegrateOverPositiveAxis()
		{
			double diffuseness = 1;
			double nuclearRadius = 5;
			Func<double, double> integrand = r => 4 * Math.PI * r * r
				* Functions.WoodsSaxonPotentialNormalized3D(r, nuclearRadius, diffuseness);

			double result = ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 2 * nuclearRadius, 64);

			AssertHelper.AssertApproximatelyEqual(1, result);
		}

		[TestMethod]
		public void IntegrateOverRealAxis()
		{
			Func<double, double> integrand = x => Functions.GaussianDistributionNormalized1D(x, 1);

			double result = ImproperQuadrature.IntegrateOverRealAxis(integrand, 2.7, 64);

			AssertHelper.AssertApproximatelyEqual(1, result);
		}

		[TestMethod]
		public void IntegrateOverRealPlane()
		{
			Func<double, double, double> integrand = (x, y) => Math.Exp(-x * x - y * y) / Math.PI;
			double result = ImproperQuadrature.IntegrateOverRealPlane(integrand, 1, 32);

			AssertHelper.AssertApproximatelyEqual(1, result, 4);
		}
	}
}
