using System;
using System.Collections.Generic;

namespace Yburn.Fireball
{
	public class DecayWidthAverager
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double GetAveragedTemperature(
			double temperature,
			double velocity
			)
		{
			return velocity > 0 ?
				GetAveragedTemperature_NonZeroVelocity(temperature, velocity)
				: temperature;
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public DecayWidthAverager(
			List<KeyValuePair<double, double>> temperatureDecayWidthList
			)
			: this(temperatureDecayWidthList, null)
		{
		}

		public DecayWidthAverager(
			List<KeyValuePair<double, double>> temperatureDecayWidthList,
			double[] averagingAngles
			)
		{
			SetDecayWidthInterpolation(temperatureDecayWidthList);
			SetAveragingAngleCosines(averagingAngles);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double GetDecayWidth(
			double temperature,
			double velocity
			)
		{
			if(AreAveragingAnglesSet && velocity > 0)
			{
				return GetAveragedDecayWidth(temperature, velocity);
			}
			else
			{
				return GetDecayWidth(temperature);
			}
		}

		public double GetDecayWidthUsingAveragedTemperature(
			double temperature,
			double velocity
			)
		{
			return GetDecayWidth(GetAveragedTemperature(temperature, velocity));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private void AssertAnglesOrdered(
			double[] averagingAngles
			)
		{
			for(int i = 1; i < averagingAngles.Length; i++)
			{
				if(averagingAngles[i - 1] >= averagingAngles[i])
				{
					throw new AveragingAnglesDisorderedException();
				}
			}
		}

		private static double GetAveragedTemperature(
			double temperature,
			double velocity,
			double cosine
			)
		{
			return temperature * Math.Sqrt(1.0 - velocity * velocity) / (1.0 - velocity * cosine);
		}

		private static double GetAveragedTemperature_NonZeroVelocity(
			double temperature,
			double velocity
			)
		{
			return temperature * Math.Sqrt(1.0 - velocity * velocity) * Artanh(velocity) / velocity;
		}

		private static double Artanh(
			double x
			)
		{
			return 0.5 * Math.Log((1.0 + x) / (1.0 - x));
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private LinearInterpolation1D InterpolatedDecayWidths;

		private double[] Cosines;

		private void SetAveragingAngleCosines(
			double[] averagingAngles
			)
		{
			AreAveragingAnglesSet = false;

			if(averagingAngles != null
				&& averagingAngles.Length > 0)
			{
				AssertAnglesOrdered(averagingAngles);

				Cosines = new double[averagingAngles.Length];
				for(int i = 0; i < Cosines.Length; i++)
				{
					Cosines[i] = Math.Cos(averagingAngles[Cosines.Length - 1 - i] * Math.PI / 180);
				}

				AreAveragingAnglesSet = true;
			}
		}

		private bool AreAveragingAnglesSet;

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

		private double GetAveragedDecayWidth(
			double temperature,
			double velocity
			)
		{
			if(Cosines.Length == 1)
			{
				return GetDecayWidth(GetAveragedTemperature(temperature, velocity, Cosines[0]));
			}
			else
			{
				double averagedDecayWidth = 0;
				double averagedTemperature;
				for(int i = 1; i < Cosines.Length - 1; i++)
				{
					averagedTemperature = GetAveragedTemperature(
						temperature, velocity, Cosines[i]);
					averagedDecayWidth += GetDecayWidth(
						averagedTemperature) * (Cosines[i + 1] - Cosines[i - 1]);
				}

				averagedTemperature = GetAveragedTemperature(
					temperature, velocity, Cosines[0]);
				averagedDecayWidth += GetDecayWidth(
					averagedTemperature) * (Cosines[1] - Cosines[0]);

				averagedTemperature = GetAveragedTemperature(
					temperature, velocity, Cosines[Cosines.Length - 1]);
				averagedDecayWidth += GetDecayWidth(averagedTemperature)
					* (Cosines[Cosines.Length - 1] - Cosines[Cosines.Length - 2]);

				return averagedDecayWidth * 0.25;
			}
		}

		private double GetDecayWidth(
			double averagedTemperature
			)
		{
			if(averagedTemperature < InterpolatedDecayWidths.Xmin)
			{
				return 0;
			}

			if(averagedTemperature > InterpolatedDecayWidths.Xmax)
			{
				return double.PositiveInfinity;
			}

			return InterpolatedDecayWidths.GetValue(averagedTemperature);
		}
	}

	public class AveragingAnglesDisorderedException : Exception
	{
		public AveragingAnglesDisorderedException()
			: base("AveragingAngles are disordered.")
		{
		}
	}
}