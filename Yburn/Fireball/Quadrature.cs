using System;
using Yburn.Util;

namespace Yburn.Fireball
{
	public delegate double IntegrandIn1D(double x);

	public delegate double IntegrandIn2D(double x, double y);

	public delegate TVector IntegrandIn1D<TVector>(double x)
		where TVector : EuclideanVectorBase<TVector>, new();

	public delegate TVector IntegrandIn2D<TVector>(double x, double y)
		where TVector : EuclideanVectorBase<TVector>, new();

	public enum QuadraturePrecision
	{
		Use8Points,
		Use16Points,
		Use32Points,
		Use64Points
	}

	/// <summary>
	/// The length scale L for unbounded integration should be chosen such that the interval [-L, L]
	/// contains the predominant part of the unbounded integral.
	/// I.e. for integrating a particle shape function choose L ~ 'particle radius'.
	/// </summary>
	public static class Quadrature
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		static Quadrature()
		{
			InitializeGaussLegendre();
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double UseSummedTrapezoidalRule(
			IntegrandIn1D integrand,
			double[] abscissae
			)
		{
			double[] weights = new double[abscissae.Length];

			weights[0] = abscissae[1] - abscissae[0];
			for(int i = 1; i < weights.Length - 1; i++)
			{
				weights[i] = abscissae[i + 1] - abscissae[i - 1];
			}
			weights[weights.Length - 1] =
				abscissae[weights.Length - 1] - abscissae[weights.Length - 2];

			return 0.5 * PerformSum(integrand, abscissae, weights);
		}

		public static double UseUniformSummedTrapezoidalRule(
			IntegrandIn1D integrand,
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
			IntegrandIn2D integrand,
			double lowerLimitX,
			double upperLimitX,
			double lowerLimitY,
			double upperLimitY,
			int steps
			)
		{
			Func<IntegrandIn1D, double> formulaX =
				i => UseUniformSummedTrapezoidalRule(i, lowerLimitX, upperLimitX, steps);

			Func<IntegrandIn1D, double> formulaY =
				i => UseUniformSummedTrapezoidalRule(i, lowerLimitY, upperLimitY, steps);

			return Apply1DFormulaeToIntegrandIn2D(integrand, formulaX, formulaY);
		}

		public static double UseExponentialSummedTrapezoidalRule(
			IntegrandIn1D integrand,
			double lowerLimit,
			double upperLimit,
			int steps
			)
		{
			double[] gridPoints = GetExponentialGridPoints(lowerLimit, upperLimit, steps);

			return UseSummedTrapezoidalRule(integrand, gridPoints);
		}

		public static double UseGaussLegendre(
			IntegrandIn1D integrand,
			double lowerLimit,
			double upperLimit,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			double[] abscissae;
			double[] weights;
			GetGaussLegendreAbscissaeAndWeights(precision, out abscissae, out weights);

			double jacobian = 0.5 * (upperLimit - lowerLimit);
			double offset = 0.5 * (upperLimit + lowerLimit);

			double[] transformedAbscissae = new double[abscissae.Length];
			for(int i = 0; i < transformedAbscissae.Length; i++)
			{
				transformedAbscissae[i] = jacobian * abscissae[i] + offset;
			}

			return jacobian * PerformSum(integrand, transformedAbscissae, weights);
		}

		public static TVector UseGaussLegendre<TVector>(
			IntegrandIn1D<TVector> integrand,
			double lowerLimit,
			double upperLimit,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			) where TVector : EuclideanVectorBase<TVector>, new()
		{
			double[] abscissae;
			double[] weights;
			GetGaussLegendreAbscissaeAndWeights(precision, out abscissae, out weights);

			double jacobian = 0.5 * (upperLimit - lowerLimit);
			double offset = 0.5 * (upperLimit + lowerLimit);

			double[] transformedAbscissae = new double[abscissae.Length];
			for(int i = 0; i < transformedAbscissae.Length; i++)
			{
				transformedAbscissae[i] = jacobian * abscissae[i] + offset;
			}

			return jacobian * PerformSum(integrand, transformedAbscissae, weights);
		}

		public static double UseGaussLegendre(
			IntegrandIn2D integrand,
			double lowerLimitX,
			double upperLimitX,
			double lowerLimitY,
			double upperLimitY,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			Func<IntegrandIn1D, double> formulaX =
				i => UseGaussLegendre(i, lowerLimitX, upperLimitX, precision);

			Func<IntegrandIn1D, double> formulaY =
				i => UseGaussLegendre(i, lowerLimitY, upperLimitY, precision);

			return Apply1DFormulaeToIntegrandIn2D(integrand, formulaX, formulaY);
		}

		public static TVector UseGaussLegendre<TVector>(
			IntegrandIn2D<TVector> integrand,
			double lowerLimitX,
			double upperLimitX,
			double lowerLimitY,
			double upperLimitY,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			) where TVector : EuclideanVectorBase<TVector>, new()
		{
			Func<IntegrandIn1D<TVector>, TVector> formulaX =
				i => UseGaussLegendre(i, lowerLimitX, upperLimitX, precision);

			Func<IntegrandIn1D<TVector>, TVector> formulaY =
				i => UseGaussLegendre(i, lowerLimitY, upperLimitY, precision);

			return Apply1DFormulaeToIntegrandIn2D(integrand, formulaX, formulaY);
		}

		public static double UseGaussLegendre_RealPlane(
			IntegrandIn2D integrand,
			double lengthScaleX,
			double lengthScaleY,
			QuadraturePrecision precisionPerQuadrant = QuadraturePrecision.Use64Points
			)
		{
			Func<IntegrandIn1D, double> formulaPositive =
				i => UseGaussLegendre_PositiveAxis(i, lengthScaleX, precisionPerQuadrant);

			Func<IntegrandIn1D, double> formulaNegative =
				i => UseGaussLegendre_NegativeAxis(i, lengthScaleY, precisionPerQuadrant);

			return Apply1DFormulaeToIntegrandIn2D(integrand, formulaPositive, formulaPositive)
				+ Apply1DFormulaeToIntegrandIn2D(integrand, formulaNegative, formulaPositive)
				+ Apply1DFormulaeToIntegrandIn2D(integrand, formulaNegative, formulaNegative)
				+ Apply1DFormulaeToIntegrandIn2D(integrand, formulaPositive, formulaNegative);
		}

		public static TVector UseGaussLegendre_RealPlane<TVector>(
			IntegrandIn2D<TVector> integrand,
			double lengthScaleX,
			double lengthScaleY,
			QuadraturePrecision precisionPerQuadrant = QuadraturePrecision.Use64Points
			) where TVector : EuclideanVectorBase<TVector>, new()
		{
			Func<IntegrandIn1D<TVector>, TVector> formulaPositive =
				i => UseGaussLegendre_PositiveAxis(i, lengthScaleX, precisionPerQuadrant);

			Func<IntegrandIn1D<TVector>, TVector> formulaNegative =
				i => UseGaussLegendre_NegativeAxis(i, lengthScaleY, precisionPerQuadrant);

			return Apply1DFormulaeToIntegrandIn2D(integrand, formulaPositive, formulaPositive)
				+ Apply1DFormulaeToIntegrandIn2D(integrand, formulaNegative, formulaPositive)
				+ Apply1DFormulaeToIntegrandIn2D(integrand, formulaNegative, formulaNegative)
				+ Apply1DFormulaeToIntegrandIn2D(integrand, formulaPositive, formulaNegative);
		}

		public static double UseGaussLegendre_PositiveAxis(
			IntegrandIn1D integrand,
			double lengthScale,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			double[] abscissae;
			double[] weights;
			GetGaussLegendreAbscissaeAndWeights(precision, out abscissae, out weights);

			double[] transformedAbscissae;
			double[] transformedWeights;
			TransformToPositiveAxis(
				lengthScale,
				abscissae,
				weights,
				out transformedAbscissae,
				out transformedWeights);

			return PerformSum(integrand, transformedAbscissae, transformedWeights);
		}

		public static TVector UseGaussLegendre_PositiveAxis<TVector>(
			IntegrandIn1D<TVector> integrand,
			double lengthScale,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			) where TVector : EuclideanVectorBase<TVector>, new()
		{
			double[] abscissae;
			double[] weights;
			GetGaussLegendreAbscissaeAndWeights(precision, out abscissae, out weights);

			double[] transformedAbscissae;
			double[] transformedWeights;
			TransformToPositiveAxis(
				lengthScale,
				abscissae,
				weights,
				out transformedAbscissae,
				out transformedWeights);

			return PerformSum(integrand, transformedAbscissae, transformedWeights);
		}

		public static double UseGaussLegendre_NegativeAxis(
			IntegrandIn1D integrand,
			double lengthScale,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			)
		{
			double[] abscissae;
			double[] weights;
			GetGaussLegendreAbscissaeAndWeights(precision, out abscissae, out weights);

			double[] transformedAbscissae;
			double[] transformedWeights;
			TransformToNegativeAxis(
				lengthScale,
				abscissae,
				weights,
				out transformedAbscissae,
				out transformedWeights);

			return PerformSum(integrand, transformedAbscissae, transformedWeights);
		}

		public static TVector UseGaussLegendre_NegativeAxis<TVector>(
			IntegrandIn1D<TVector> integrand,
			double lengthScale,
			QuadraturePrecision precision = QuadraturePrecision.Use64Points
			) where TVector : EuclideanVectorBase<TVector>, new()
		{
			double[] abscissae;
			double[] weights;
			GetGaussLegendreAbscissaeAndWeights(precision, out abscissae, out weights);

			double[] transformedAbscissae;
			double[] transformedWeights;
			TransformToNegativeAxis(
				lengthScale,
				abscissae,
				weights,
				out transformedAbscissae,
				out transformedWeights);

			return PerformSum(integrand, transformedAbscissae, transformedWeights);
		}

		public static double UseGaussLegendre_FirstQuadrant(
			IntegrandIn2D integrand,
			double lengthScaleX,
			double lengthScaleY,
			QuadraturePrecision precisionPerAxis = QuadraturePrecision.Use64Points
			)
		{
			Func<IntegrandIn1D, double> formulaX =
				i => UseGaussLegendre_PositiveAxis(i, lengthScaleX, precisionPerAxis);

			Func<IntegrandIn1D, double> formulaY =
				i => UseGaussLegendre_PositiveAxis(i, lengthScaleY, precisionPerAxis);

			return Apply1DFormulaeToIntegrandIn2D(integrand, formulaX, formulaY);
		}

		public static double UseGaussLegendre_SecondQuadrant(
			IntegrandIn2D integrand,
			double lengthScaleX,
			double lengthScaleY,
			QuadraturePrecision precisionPerAxis = QuadraturePrecision.Use64Points
			)
		{
			Func<IntegrandIn1D, double> formulaX =
				i => UseGaussLegendre_NegativeAxis(i, lengthScaleX, precisionPerAxis);

			Func<IntegrandIn1D, double> formulaY =
				i => UseGaussLegendre_PositiveAxis(i, lengthScaleY, precisionPerAxis);

			return Apply1DFormulaeToIntegrandIn2D(integrand, formulaX, formulaY);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double PerformSum(
			IntegrandIn1D integrand,
			double[] abscissae,
			double[] weights
			)
		{
			double integral = 0;
			for(int i = 0; i < abscissae.Length; i++)
			{
				integral += weights[i] * integrand(abscissae[i]);
			}

			return integral;
		}

		private static TVector PerformSum<TVector>(
			IntegrandIn1D<TVector> integrand,
			double[] abscissae,
			double[] weights
			) where TVector : EuclideanVectorBase<TVector>, new()
		{
			TVector integral = new TVector();
			for(int i = 0; i < abscissae.Length; i++)
			{
				integral += weights[i] * integrand(abscissae[i]);
			}

			return integral;
		}

		private static double[] GaussLegendre8Abscissae;

		private static double[] GaussLegendre8Weights;

		private static double[] GaussLegendre16Abscissae;

		private static double[] GaussLegendre16Weights;

		private static double[] GaussLegendre32Abscissae;

		private static double[] GaussLegendre32Weights;

		private static double[] GaussLegendre64Abscissae;

		private static double[] GaussLegendre64Weights;

		private static void InitializeGaussLegendre()
		{
			string dir = "..\\..\\";
			GetAbscissaeAndWeightsFromFile(dir + "GaussLegendre8.txt",
				out GaussLegendre8Abscissae, out GaussLegendre8Weights);

			GetAbscissaeAndWeightsFromFile(dir + "GaussLegendre16.txt",
				out GaussLegendre16Abscissae, out GaussLegendre16Weights);

			GetAbscissaeAndWeightsFromFile(dir + "GaussLegendre32.txt",
				out GaussLegendre32Abscissae, out GaussLegendre32Weights);

			GetAbscissaeAndWeightsFromFile(dir + "GaussLegendre64.txt",
				out GaussLegendre64Abscissae, out GaussLegendre64Weights);
		}

		private static void GetAbscissaeAndWeightsFromFile(
			string pathFile,
			out double[] abscissae,
			out double[] weights
			)
		{
			double[][] glTable;
			TableFileReader.Read(pathFile, out glTable);
			abscissae = glTable[0];
			weights = glTable[1];
		}

		public static double Apply1DFormulaeToIntegrandIn2D(
			IntegrandIn2D integrand,
			Func<IntegrandIn1D, double> formulaX,
			Func<IntegrandIn1D, double> formulaY
			)
		{
			IntegrandIn1D integrandWithXIntegrationPerformed =
				y => formulaX(x => integrand(x, y));

			return formulaY(integrandWithXIntegrationPerformed);
		}

		public static TVector Apply1DFormulaeToIntegrandIn2D<TVector>(
			IntegrandIn2D<TVector> integrand,
			Func<IntegrandIn1D<TVector>, TVector> formulaX,
			Func<IntegrandIn1D<TVector>, TVector> formulaY
			) where TVector : EuclideanVectorBase<TVector>, new()
		{
			IntegrandIn1D<TVector> integrandWithXIntegrationPerformed =
				y => formulaX(x => integrand(x, y));

			return formulaY(integrandWithXIntegrationPerformed);
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

		private static void GetGaussLegendreAbscissaeAndWeights(
			QuadraturePrecision precision,
			out double[] abscissae,
			out double[] weights
			)
		{
			switch(precision)
			{
				case QuadraturePrecision.Use8Points:
					abscissae = GaussLegendre8Abscissae;
					weights = GaussLegendre8Weights;
					break;

				case QuadraturePrecision.Use16Points:
					abscissae = GaussLegendre16Abscissae;
					weights = GaussLegendre16Weights;
					break;

				case QuadraturePrecision.Use32Points:
					abscissae = GaussLegendre32Abscissae;
					weights = GaussLegendre32Weights;
					break;

				case QuadraturePrecision.Use64Points:
					abscissae = GaussLegendre64Abscissae;
					weights = GaussLegendre64Weights;
					break;

				default:
					throw new Exception("Invalid Precision.");
			}
		}

		private static void TransformToPositiveAxis(
			double lengthScale,
			double[] abscissae,
			double[] weights,
			out double[] transformedAbscissae,
			out double[] transformedWeights
			)
		{
			int length = abscissae.Length;
			transformedAbscissae = new double[length];
			transformedWeights = new double[length];

			for(int i = 0; i < length; i++)
			{
				double x = 0.5 * abscissae[i] + 0.5;
				transformedAbscissae[i] = MapUnitIntervalToRealAxis(x, lengthScale);
				transformedWeights[i] = 0.5 * weights[i]
					* MapUnitIntervalToRealAxis_Jacobian(x, lengthScale);
			}
		}

		private static void TransformToNegativeAxis(
			double lengthScale,
			double[] abscissae,
			double[] weights,
			out double[] transformedAbscissae,
			out double[] transformedWeights
			)
		{
			int length = abscissae.Length;
			transformedAbscissae = new double[length];
			transformedWeights = new double[length];

			for(int i = 0; i < length; i++)
			{
				double x = 0.5 * abscissae[i] - 0.5;
				transformedAbscissae[i] = MapUnitIntervalToRealAxis(x, lengthScale);
				transformedWeights[i] = 0.5 * weights[i]
					* MapUnitIntervalToRealAxis_Jacobian(x, lengthScale);
			}
		}

		private static double MapUnitIntervalToRealAxis(
				double x,
				double lengthScale
				)
		{
			return -lengthScale * Math.Sign(x) * Math.Log(1 - Math.Abs(x));
		}

		private static double MapUnitIntervalToRealAxis_Jacobian(
				double x,
				double lengthScale
				)
		{
			return lengthScale / (1 - Math.Abs(x));
		}
	}
}