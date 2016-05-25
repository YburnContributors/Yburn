using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;

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
			OneVariableIntegrand integrand = x => x;
			double[] gridPoints = { 0, 0.2, 0.3, 0.7, 0.8, 1 };
			double result = Quadrature.UseSummedTrapezoidalRule(integrand, gridPoints);

			Assert.AreEqual(0.5, result, eps);
		}

		[TestMethod]
		public void UseUniformSummedTrapezoidalRule()
		{
			OneVariableIntegrand integrand = x => x;
			double result = Quadrature.UseUniformSummedTrapezoidalRule(integrand, 0, 1, 5);

			Assert.AreEqual(0.5, result, eps);
		}

		[TestMethod]
		public void UseUniformSummedTrapezoidalRuleIn2Dimensions()
		{
			TwoVariableIntegrand integrand = (x, y) => x;
			double result = Quadrature.UseUniformSummedTrapezoidalRule(integrand, 0, 1, 0, 2, 5);

			Assert.AreEqual(1, result, eps);
		}

		[TestMethod]
		public void UseExponentialSummedTrapezoidalRule()
		{
			OneVariableIntegrand integrand = x => 1 / (x * x);
			double result = Quadrature.UseExponentialSummedTrapezoidalRule(integrand, 1, 100, 2000);

			Assert.AreEqual(0.99, result, 1e-5);
		}

		[TestMethod]
		public void UseGaussLegendre()
		{
			OneVariableIntegrand integrand = x => Math.Exp(x);
			double result = Quadrature.UseGaussLegendre(integrand, 0, 1);

			Assert.AreEqual(Math.E - 1, result, eps);
		}

		[TestMethod]
		public void UseGaussLegendreIn2Dimensions()
		{
			TwoVariableIntegrand integrand = (x, y) => Math.Exp(x + 0.5 * y);
			double result = Quadrature.UseGaussLegendre(integrand, 0, 1, 0, 2);

			Assert.AreEqual(2 * Math.Pow(Math.E - 1, 2), result, eps);
		}

		[TestMethod]
		public void UseGaussLegendreIn2DimensionsVectorValued()
		{
			TwoVariableIntegrandVectorValued<EuclideanVector2D> integrand =
				(x, y) => new EuclideanVector2D(Math.Exp(x + 0.5 * y), x * y * y * y);
			EuclideanVector2D result =
				Quadrature.UseGaussLegendre<EuclideanVector2D>(integrand, 0, 1, 0, 2);

			Assert.AreEqual(2 * Math.Pow(Math.E - 1, 2), result.X, eps);
			Assert.AreEqual(2, result.Y, eps);
		}

		[TestMethod]
		public void UseGaussLegendreOverPositiveAxis()
		{
			OneVariableIntegrand integrand = x => Math.Exp(-x);
			double result = Quadrature.UseGaussLegendreOverPositiveAxis(integrand, 1);

			Assert.AreEqual(1, result, eps);
		}

		[TestMethod]
		public void UseGaussLegendreOverNegativeAxis()
		{
			OneVariableIntegrand integrand = x => Math.Exp(x);
			double result = Quadrature.UseGaussLegendreOverNegativeAxis(integrand, 1);

			Assert.AreEqual(1, result, eps);
		}

		[TestMethod]
		public void UseGaussLegendreOverFirstQuadrant()
		{
			TwoVariableIntegrand integrand = (x, y) => Math.Exp(-x - y);
			double result = Quadrature.UseGaussLegendreOverFirstQuadrant(integrand, 1);

			Assert.AreEqual(1, result, eps);
		}

		[TestMethod]
		public void UseGaussLegendreOverSecondQuadrant()
		{
			TwoVariableIntegrand integrand = (x, y) => Math.Exp(x - y);
			double result = Quadrature.UseGaussLegendreOverSecondQuadrant(integrand, 1);

			Assert.AreEqual(1, result, eps);
		}

		[TestMethod]
		public void UseGaussLegendreOverAllQuadrants()
		{
			TwoVariableIntegrand integrand = (x, y) => Math.Exp(-x * x - y * y) / Math.PI;
			double result = Quadrature.UseGaussLegendreOverAllQuadrants(integrand, 1);

			Assert.AreEqual(1, result, eps);
		}

		[TestMethod]
		public void UseGaussLegendreOverAllQuadrantsVectorValued()
		{
			TwoVariableIntegrandVectorValued<EuclideanVector2D> integrand = (x, y) =>
				new EuclideanVector2D(
					Math.Exp(-x * x - y * y) / Math.PI,
					Math.Exp(-Math.Abs(x) - Math.Abs(y)));

			EuclideanVector2D result = Quadrature.UseGaussLegendreOverAllQuadrants(integrand, 1);

			Assert.AreEqual(1, result.X, eps);
			Assert.AreEqual(4, result.Y, eps);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double eps = 1e-15;
	}
}