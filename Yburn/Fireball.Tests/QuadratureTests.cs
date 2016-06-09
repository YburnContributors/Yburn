using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;
using Yburn.Tests.Util;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class QuadratureTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		[TestMethod]
		public void UseSummedTrapezoidalRule()
		{
			IntegrandIn1D integrand = x => x;
			double[] gridPoints = { 0, 0.2, 0.3, 0.7, 0.8, 1 };
			double result = Quadrature.UseSummedTrapezoidalRule(integrand, gridPoints);

			AssertHelper.AssertRoundedEqual(0.5, result);
		}

		[TestMethod]
		public void UseUniformSummedTrapezoidalRule()
		{
			IntegrandIn1D integrand = x => x;
			double result = Quadrature.UseUniformSummedTrapezoidalRule(integrand, 0, 1, 500);

			AssertHelper.AssertRoundedEqual(0.5, result);
		}

		[TestMethod]
		public void UseUniformSummedTrapezoidalRuleIn2Dimensions()
		{
			IntegrandIn2D integrand = (x, y) => x;
			double result = Quadrature.UseUniformSummedTrapezoidalRule(integrand, 0, 1, 0, 2, 500);

			AssertHelper.AssertRoundedEqual(1, result);
		}

		[TestMethod]
		public void UseExponentialSummedTrapezoidalRule()
		{
			IntegrandIn1D integrand = x => 1 / (x * x);
			double result = Quadrature.UseExponentialSummedTrapezoidalRule(integrand, 1, 100, 2000);

			AssertHelper.AssertRoundedEqual(0.99, result, 5);
		}

		[TestMethod]
		public void UseGaussLegendre()
		{
			IntegrandIn1D integrand = x => Math.Exp(x);
			double result = Quadrature.UseGaussLegendre(integrand, 0, 1);

			AssertHelper.AssertRoundedEqual(Math.E - 1, result);
		}

		[TestMethod]
		public void UseGaussLegendreIn2Dimensions()
		{
			IntegrandIn2D integrand = (x, y) => Math.Exp(x + 0.5 * y);
			double result = Quadrature.UseGaussLegendre(integrand, 0, 1, 0, 2);

			AssertHelper.AssertRoundedEqual(2 * Math.Pow(Math.E - 1, 2), result);
		}

		[TestMethod]
		public void UseGaussLegendreIn2DimensionsVectorValued()
		{
			IntegrandIn2D<EuclideanVector2D> integrand =
				(x, y) => new EuclideanVector2D(Math.Exp(x + 0.5 * y), x * y * y * y);
			EuclideanVector2D result = Quadrature.UseGaussLegendre(integrand, 0, 1, 0, 2);

			AssertHelper.AssertRoundedEqual(2 * Math.Pow(Math.E - 1, 2), result.X);
			AssertHelper.AssertRoundedEqual(2, result.Y);
		}

		[TestMethod]
		public void UseGaussLegendre_PositiveAxis()
		{
			IntegrandIn1D integrand = x => Math.Exp(-x);
			double result = Quadrature.UseGaussLegendre_PositiveAxis(integrand, 1);

			AssertHelper.AssertRoundedEqual(1, result);
		}

		[TestMethod]
		public void UseGaussLegendre_NegativeAxis()
		{
			IntegrandIn1D integrand = x => Math.Exp(x);
			double result = Quadrature.UseGaussLegendre_NegativeAxis(integrand, 1);

			AssertHelper.AssertRoundedEqual(1, result);
		}

		[TestMethod]
		public void UseGaussLegendre_FirstQuadrant()
		{
			IntegrandIn2D integrand = (x, y) => Math.Exp(-x - y);
			double result = Quadrature.UseGaussLegendre_FirstQuadrant(integrand, 1, 1);

			AssertHelper.AssertRoundedEqual(1, result);
		}

		[TestMethod]
		public void UseGaussLegendre_SecondQuadrant()
		{
			IntegrandIn2D integrand = (x, y) => Math.Exp(x - y);
			double result = Quadrature.UseGaussLegendre_SecondQuadrant(integrand, 1, 1);

			AssertHelper.AssertRoundedEqual(1, result);
		}

		[TestMethod]
		public void UseGaussLegendre_RealPlane()
		{
			IntegrandIn2D integrand = (x, y) => Math.Exp(-x * x - y * y) / Math.PI;
			double result = Quadrature.UseGaussLegendre_RealPlane(integrand, 1, 1);

			AssertHelper.AssertRoundedEqual(1, result);
		}

		[TestMethod]
		public void UseGaussLegendre_RealPlaneVectorValued()
		{
			IntegrandIn2D<EuclideanVector2D> integrand = (x, y) =>
				new EuclideanVector2D(
					Math.Exp(-x * x - y * y) / Math.PI,
					Math.Exp(-Math.Abs(x) - Math.Abs(y)));

			EuclideanVector2D result = Quadrature.UseGaussLegendre_RealPlane(integrand, 1, 1);

			AssertHelper.AssertRoundedEqual(1, result.X);
			AssertHelper.AssertRoundedEqual(4, result.Y);
		}
	}
}