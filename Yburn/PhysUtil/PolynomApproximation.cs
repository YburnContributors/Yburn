using MathNet.Numerics;
using System;
using System.Collections.Generic;

namespace Yburn.PhysUtil
{
	public class PolynomApproximation
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public PolynomApproximation(
			double[] x,
			double[] y,
			int order
			)
		{
			GetValue = Fit.PolynomialFunc(x, y, order);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly Func<double, double> GetValue;
	}

	public class PolynomApproximationThroughOrigin
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public PolynomApproximationThroughOrigin(
			double[] x,
			double[] y,
			int order
			)
		{
			Func<double, double>[] functions = new Func<double, double>[order];
			for(int i = 0; i < functions.Length; i++)
			{
				int power = i + 1;
				functions[i] = value => Math.Pow(value, power);
			}

			GetValue = Fit.LinearCombinationFunc(x, y, functions);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly Func<double, double> GetValue;
	}
}
