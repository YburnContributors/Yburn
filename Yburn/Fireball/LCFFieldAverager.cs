using MathNet.Numerics.Integration;
using System;

namespace Yburn.Fireball
{
	public delegate double LCFFieldFunction(double x, double y, double rapidity);

	public class LCFFieldAverager
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public LCFFieldAverager(
			FireballParam param
			)
		{
			Param = param.Clone();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double AverageRapidityDependence(
			Func<double, double> function
			)
		{
			double rapidityCap = Param.PartonPeakRapidity;

			return GaussLegendreRule.Integrate(function,
				-rapidityCap, +rapidityCap, Param.EMFQuadratureOrder) / (2 * rapidityCap);
		}

		public double AverageByBottomiumDistribution(
			LCFFieldFunction function
			)
		{
			GlauberCalculation glauber = new GlauberCalculation(Param);

			double[] x = Param.XAxis;
			double[] y = Param.YAxis;

			double[,] functionColumnDensityValues = new double[x.Length, y.Length];
			for(int i = 0; i < x.Length; i++)
			{
				for(int j = 0; j < y.Length; j++)
				{
					Func<double, double> integrand = rapidity => function(x[i], y[j], rapidity);

					functionColumnDensityValues[i, j]
						= AverageRapidityDependence(integrand)
						* glauber.NcollField[i, j];
				}
			}

			SimpleFireballField functionColumnDensity = new SimpleFireballField(
				FireballFieldType.Ncoll, functionColumnDensityValues);

			return functionColumnDensity.TrapezoidalRuleSummedValues()
				/ glauber.NcollField.TrapezoidalRuleSummedValues();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}