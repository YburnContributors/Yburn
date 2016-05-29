using System;
using Yburn.Util;

namespace Yburn.Fireball
{
	public delegate double OneVariableIntegrand(double x);

	public delegate double TwoVariableIntegrand(double x, double y);

	public delegate TVector TwoVariableIntegrandVectorValued<TVector>(double x, double y)
		where TVector : EuclideanVector<TVector>, new();

	public static class Quadrature
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		static Quadrature()
		{
			InitializeGaussLegendre(out GaussLegendreGridPoints, out GaussLegendreWeights);
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double UseSummedTrapezoidalRule(
			OneVariableIntegrand integrand,
			double[] gridPoints
			)
		{
			int steps = gridPoints.Length - 1;
			double integral = integrand(gridPoints[0]) * (gridPoints[1] - gridPoints[0]);

			for(int i = 1; i < steps; i++)
			{
				integral += integrand(gridPoints[i]) * (gridPoints[i + 1] - gridPoints[i - 1]);
			}

			integral += integrand(gridPoints[steps]) * (gridPoints[steps] - gridPoints[steps - 1]);

			return integral *= 0.5;
		}

		public static double UseUniformSummedTrapezoidalRule(
			OneVariableIntegrand integrand,
			double lowerLimit,
			double upperLimit,
			int steps
			)
		{
			double h = (upperLimit - lowerLimit) / steps;

			double integral = 0.5 * (integrand(lowerLimit) + integrand(upperLimit));
			for(int i = 1; i < steps; i++)
			{
				integral += integrand(lowerLimit + i * h);
			}

			return h * integral;
		}

		public static double UseUniformSummedTrapezoidalRule(
			TwoVariableIntegrand integrand,
			double lowerLimitX,
			double upperLimitX,
			double lowerLimitY,
			double upperLimitY,
			int steps
			)
		{
			Func<OneVariableIntegrand, double> quadratureFormulaX =
				i => UseUniformSummedTrapezoidalRule(i, lowerLimitX, upperLimitX, steps);

			Func<OneVariableIntegrand, double> quadratureFormulaY =
				i => UseUniformSummedTrapezoidalRule(i, lowerLimitY, upperLimitY, steps);

			return ApplyToIntegrand(quadratureFormulaX, quadratureFormulaY, integrand);
		}

		public static double UseExponentialSummedTrapezoidalRule(
			OneVariableIntegrand integrand,
			double lowerLimit,
			double upperLimit,
			int steps
			)
		{
			double[] gridPoints = GetExponentialGridPoints(lowerLimit, upperLimit, steps);

			return UseSummedTrapezoidalRule(integrand, gridPoints);
		}

		public static double UseGaussLegendre(
			OneVariableIntegrand integrand,
			double lowerLimit,
			double upperLimit
			)
		{
			double jacobian = 0.5 * (upperLimit - lowerLimit);
			double offset = 0.5 * (upperLimit + lowerLimit);

			double integral = 0;
			for(int i = 0; i < 64; i++)
			{
				integral += GaussLegendreWeights[i]
					* integrand(jacobian * GaussLegendreGridPoints[i] + offset);
			}

			return jacobian * integral;
		}

		public static double UseGaussLegendre(
			TwoVariableIntegrand integrand,
			double lowerLimitX,
			double upperLimitX,
			double lowerLimitY,
			double upperLimitY
			)
		{
			Func<OneVariableIntegrand, double> quadratureFormulaX =
				i => UseGaussLegendre(i, lowerLimitX, upperLimitX);

			Func<OneVariableIntegrand, double> quadratureFormulaY =
				i => UseGaussLegendre(i, lowerLimitY, upperLimitY);

			return ApplyToIntegrand(quadratureFormulaX, quadratureFormulaY, integrand);
		}

		public static TVector UseGaussLegendre<TVector>(
			TwoVariableIntegrandVectorValued<TVector> integrand,
			double lowerLimitX,
			double upperLimitX,
			double lowerLimitY,
			double upperLimitY
			) where TVector : EuclideanVector<TVector>, new()
		{
			Func<TwoVariableIntegrand, double> quadratureFormula =
				i => UseGaussLegendre(i, lowerLimitX, upperLimitX, lowerLimitY, upperLimitY);

			return ApplyToVectorValuedIntegrand(quadratureFormula, integrand);
		}

		public static double UseGaussLegendreOverPositiveAxis(
			OneVariableIntegrand integrand,
			double characteristicScale
			)
		{
			OneVariableIntegrand transformedIntegrand = TransformIntegrandDomainToUnitInterval(
				integrand, characteristicScale);

			return UseGaussLegendre(transformedIntegrand, 0, 1);
		}

		public static double UseGaussLegendreOverNegativeAxis(
			OneVariableIntegrand integrand,
			double characteristicScale
			)
		{
			OneVariableIntegrand transformedIntegrand = TransformIntegrandDomainToUnitInterval(
				integrand, characteristicScale);

			return UseGaussLegendre(transformedIntegrand, -1, 0);
		}

		public static double UseGaussLegendreOverFirstQuadrant(
			TwoVariableIntegrand integrand,
			double characteristicScale
			)
		{
			Func<OneVariableIntegrand, double> quadratureFormula =
				i => UseGaussLegendreOverPositiveAxis(i, characteristicScale);

			return ApplyToIntegrand(quadratureFormula, quadratureFormula, integrand);
		}

		public static double UseGaussLegendreOverSecondQuadrant(
			TwoVariableIntegrand integrand,
			double characteristicScale
			)
		{
			Func<OneVariableIntegrand, double> quadratureFormulaX =
				i => UseGaussLegendreOverNegativeAxis(i, characteristicScale);

			Func<OneVariableIntegrand, double> quadratureFormulaY =
				i => UseGaussLegendreOverPositiveAxis(i, characteristicScale);

			return ApplyToIntegrand(quadratureFormulaX, quadratureFormulaY, integrand);
		}

		public static double UseGaussLegendreOverAllQuadrants(
			TwoVariableIntegrand integrand,
			double characteristicScale
			)
		{
			Func<OneVariableIntegrand, double> quadratureFormulaPositive =
				i => UseGaussLegendreOverPositiveAxis(i, characteristicScale);

			Func<OneVariableIntegrand, double> quadratureFormulaNegative =
				i => UseGaussLegendreOverNegativeAxis(i, characteristicScale);

			return ApplyToIntegrand(quadratureFormulaPositive, quadratureFormulaPositive, integrand)
				+ ApplyToIntegrand(quadratureFormulaNegative, quadratureFormulaPositive, integrand)
				+ ApplyToIntegrand(quadratureFormulaPositive, quadratureFormulaNegative, integrand)
				+ ApplyToIntegrand(quadratureFormulaNegative, quadratureFormulaNegative, integrand);
		}

		public static TVector UseGaussLegendreOverAllQuadrants<TVector>(
			TwoVariableIntegrandVectorValued<TVector> integrand,
			double characteristicScale
			) where TVector : EuclideanVector<TVector>, new()
		{
			Func<TwoVariableIntegrand, double> quadratureFormula =
				i => UseGaussLegendreOverAllQuadrants(i, characteristicScale);

			return ApplyToVectorValuedIntegrand(quadratureFormula, integrand);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double[] GaussLegendreGridPoints;

		private static readonly double[] GaussLegendreWeights;

		private static void InitializeGaussLegendre(
			out double[] GaussLegendreGridPoints,
			out double[] GaussLegendreWeights
			)
		{
			double[][] glTable;
			TableFileReader.Read("..\\..\\GaussLegendre_64GridPoints.txt", out glTable);
			GaussLegendreGridPoints = glTable[0];
			GaussLegendreWeights = glTable[1];
		}

		public static double ApplyToIntegrand(
			Func<OneVariableIntegrand, double> quadratureFormulaX,
			Func<OneVariableIntegrand, double> quadratureFormulaY,
			TwoVariableIntegrand integrand
			)
		{
			OneVariableIntegrand integrandWithXIntegrationPerformed =
				y => quadratureFormulaX(x => integrand(x, y));

			return quadratureFormulaY(integrandWithXIntegrationPerformed);
		}

		private static TVector ApplyToVectorValuedIntegrand<TVector>(
			Func<TwoVariableIntegrand, double> quadratureFormula,
			TwoVariableIntegrandVectorValued<TVector> integrand
			) where TVector : EuclideanVector<TVector>, new()
		{
			TVector integral = new TVector();

			for(int i = 0; i < integral.Dimension; i++)
			{
				integral[i] = quadratureFormula((x, y) => integrand(x, y)[i]);
			}

			return integral;
		}

		private static double[] GetExponentialGridPoints(
			double lowerLimit,
			double upperLimit,
			int steps
			)
		{
			double[] gridPoints = new double[steps + 1];

			// dx = (upperLimit / lowerLimit) ^ (1 / steps)
			// But use formula below since Math.Pow precision is insufficient.
			double dx = Math.Exp(Math.Log(upperLimit / lowerLimit) / steps);
			gridPoints[0] = lowerLimit;

			for(int n = 1; n < gridPoints.Length; n++)
			{
				gridPoints[n] = gridPoints[n - 1] * dx;
			}

			return gridPoints;
		}

		private static OneVariableIntegrand TransformIntegrandDomainToUnitInterval(
			OneVariableIntegrand integrand,
			double characteristicScale
			)
		{
			OneVariableIntegrand transformedIntegrand =
				x => integrand(StretchUnitIntervalToFullAxis(x, characteristicScale))
					* StretchUnitIntervalToFullAxisJacobian(x, characteristicScale);

			return transformedIntegrand;
		}

		private static double StretchUnitIntervalToFullAxis(
				double x,
				double characteristicScale
				)
		{
			return -characteristicScale * Math.Sign(x) * Math.Log(1 - Math.Abs(x));
		}

		private static double StretchUnitIntervalToFullAxisJacobian(
				double x,
				double characteristicScale
				)
		{
			return characteristicScale / (1 - Math.Abs(x));
		}
	}
}