using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class DecayWidthAverager
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double GetAveragedTemperature(
			double qgpTemperature,
			double velocity
			)
		{
			if(velocity > 0)
			{
				return qgpTemperature
					* Math.Sqrt(1.0 - velocity * velocity) * Functions.Artanh(velocity) / velocity;
			}
			else
			{
				return qgpTemperature;
			}
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public DecayWidthAverager(
			List<KeyValuePair<double, double>> temperatureDecayWidthList,
			int numberAveragingAngles,
			double qgpFormationTemperature
			)
		{
			SetDecayWidthInterpolation(temperatureDecayWidthList);
			NumberAveragingAngles = numberAveragingAngles;
			QGPFormationTemperature = qgpFormationTemperature;
			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double GetDecayWidth(
			double qgpTemperature,
			double velocity,
			DecayWidthEvaluationType evaluationType
			)
		{
			if(qgpTemperature < QGPFormationTemperature)
			{
				return 0;
			}
			else
			{
				switch(evaluationType)
				{
					case DecayWidthEvaluationType.UnshiftedTemperature:
						return GetDecayWidth(qgpTemperature);

					case DecayWidthEvaluationType.MaximallyBlueshifted:
						return GetDecayWidth(
							GetDopplerShiftedTemperature(qgpTemperature, velocity, 1));

					case DecayWidthEvaluationType.AveragedTemperature:
						return GetDecayWidthUsingAveragedTemperature(qgpTemperature, velocity);

					case DecayWidthEvaluationType.AveragedDecayWidth:
						return GetAveragedDecayWidth(qgpTemperature, velocity);

					default:
						throw new Exception("Invalid DecayWidthEvaluationType.");
				}
			}
		}

		public readonly double QGPFormationTemperature;

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double GetDopplerShiftedTemperature(
			double qgpTemperature,
			double velocity,
			double cosine
			)
		{
			return qgpTemperature
				* Math.Sqrt(1.0 - velocity * velocity) / (1.0 - velocity * cosine);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private int NumberAveragingAngles;

		private LinearInterpolation1D InterpolatedDecayWidths;

		private void AssertValidMembers()
		{
			if(NumberAveragingAngles <= 0)
			{
				throw new Exception("NumberAveragingAngles <= 0");
			}
			if(QGPFormationTemperature <= 0)
			{
				throw new Exception("QGPFormationTemperature <= 0");
			}
		}

		private void SetDecayWidthInterpolation(
			List<KeyValuePair<double, double>> temperatureDecayWidthList
			)
		{
			int listSize = temperatureDecayWidthList.Count;
			double[] temperatureList = new double[listSize];
			double[] gammaList = new double[listSize];
			for(int j = 0; j < listSize; j++)
			{
				temperatureList[j] = temperatureDecayWidthList[j].Key;
				gammaList[j] = temperatureDecayWidthList[j].Value;
			}

			InterpolatedDecayWidths = new LinearInterpolation1D(temperatureList, gammaList);
		}

		private double GetDecayWidthUsingAveragedTemperature(
			double qgpTemperature,
			double velocity
			)
		{
			return GetDecayWidth(GetAveragedTemperature(qgpTemperature, velocity));
		}

		private double GetAveragedDecayWidth(
			double qgpTemperature,
			double velocity
			)
		{
			Func<double, double> integrand = cosine => GetDecayWidth(GetDopplerShiftedTemperature(
				qgpTemperature, velocity, cosine));

			return 0.5 * Quadrature.IntegrateOverInterval(integrand, -1, 1, NumberAveragingAngles);
		}

		private double GetDecayWidth(
			double effectiveTemperature
			)
		{
			if(effectiveTemperature < InterpolatedDecayWidths.Xmin)
			{
				return 0;
			}

			if(effectiveTemperature > InterpolatedDecayWidths.Xmax)
			{
				return double.PositiveInfinity;
			}

			return InterpolatedDecayWidths.GetValue(effectiveTemperature);
		}
	}
}