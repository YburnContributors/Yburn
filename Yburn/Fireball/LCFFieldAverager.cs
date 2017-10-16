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
			CoordinateSystem system = new CoordinateSystem(Param);
			GlauberCalculation glauber = new GlauberCalculation(Param);

			double[,] functionColumnDensityValues
				= new double[system.XAxis.Count, system.YAxis.Count];

			for(int i = 0; i < system.XAxis.Count; i++)
			{
				for(int j = 0; j < system.YAxis.Count; j++)
				{
					Func<double, double> integrand
						= rapidity => function(system.XAxis[i], system.YAxis[j], rapidity);

					functionColumnDensityValues[i, j]
						= AverageRapidityDependence(integrand)
						* glauber.NumberCollisionsField[i, j];
				}
			}
			SimpleFireballField functionColumnDensity = new SimpleFireballField(
				FireballFieldType.NumberCollisions, system, functionColumnDensityValues);

			return functionColumnDensity.IntegrateValues()
				/ glauber.NumberCollisionsField.IntegrateValues();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;
	}
}
