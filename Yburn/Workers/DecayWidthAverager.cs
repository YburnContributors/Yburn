using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.PhysUtil;
using Yburn.QQState;

namespace Yburn.Workers
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

		private static void SetInterpolation(
			List<QQDataSet> dataSetList,
			DecayWidthType decayWidthType,
			out LinearInterpolation1D interpolatedBindingEnergies,
			out LinearInterpolation1D interpolatedDecayWidths
			)
		{
			int listSize = dataSetList.Count;

			double[] temperatureList = new double[listSize];
			double[] energyList = new double[listSize];
			double[] gammaList = new double[listSize];

			if(listSize > 0)
			{
				for(int j = 0; j < listSize; j++)
				{
					temperatureList[j] = dataSetList[j].Temperature;
					energyList[j] = dataSetList[j].Energy;
					gammaList[j] = dataSetList[j].GetGamma(decayWidthType);
				}
			}
			else
			{
				temperatureList = new double[] { 0 };
				energyList = new double[] { 0 };
				gammaList = new double[] { double.PositiveInfinity };
			}

			interpolatedBindingEnergies = new LinearInterpolation1D(temperatureList, energyList);
			interpolatedDecayWidths = new LinearInterpolation1D(temperatureList, gammaList);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public DecayWidthAverager(
			List<QQDataSet> dataSetList,
			DecayWidthType decayWidthType,
			double qgpFormationTemperature,
			int numberAveragingAngles
			)
		{
			SetInterpolation(dataSetList, decayWidthType,
				out InterpolatedBindingEnergies, out InterpolatedDecayWidths);
			QGPFormationTemperature = qgpFormationTemperature;
			NumberAveragingAngles = numberAveragingAngles;
			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double GetEffectiveDecayWidth(
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
						return GetDecayWidthEvaluatedAtEffectiveTemperature(qgpTemperature);

					case DecayWidthEvaluationType.MaximallyBlueshifted:
						return GetDecayWidthEvaluatedAtEffectiveTemperature(
							GetDopplerShiftedTemperature(qgpTemperature, velocity, 1));

					case DecayWidthEvaluationType.AveragedTemperature:
						return GetDecayWidthEvaluatedAtEffectiveTemperature(
							GetAveragedTemperature(qgpTemperature, velocity));

					case DecayWidthEvaluationType.AveragedDecayWidth:
						return GetAveragedDecayWidth(qgpTemperature, velocity);

					default:
						throw new Exception("Invalid DecayWidthEvaluationType.");
				}
			}
		}

		public double GetDecayWidthEvaluatedAtDopplerShiftedTemperature(
			double qgpTemperature,
			double velocity,
			double cosine
			)
		{
			return GetInterpolatedDecayWidth(GetDopplerShiftedTemperature(qgpTemperature, velocity, cosine));
		}

		public readonly double QGPFormationTemperature;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly int NumberAveragingAngles;

		private readonly LinearInterpolation1D InterpolatedBindingEnergies;

		private readonly LinearInterpolation1D InterpolatedDecayWidths;

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

		private double GetDecayWidthEvaluatedAtEffectiveTemperature(
			double effectiveTemperature
			)
		{
			if(GetInterpolatedBindingEnergy(effectiveTemperature) < 0)
			{
				return GetInterpolatedDecayWidth(effectiveTemperature);
			}
			else
			{
				return double.PositiveInfinity;
			}
		}

		private double GetAveragedDecayWidth(
			double qgpTemperature,
			double velocity
			)
		{
			if(GetInterpolatedBindingEnergy(GetDopplerShiftedTemperature(qgpTemperature, velocity, 1)) > 0)
			{
				return double.PositiveInfinity;
			}
			else
			{
				Func<double, double> integrand = cosine =>
					GetDecayWidthEvaluatedAtDopplerShiftedTemperature(qgpTemperature, velocity, cosine);

				return 0.5 * Quadrature.IntegrateOverInterval(integrand, -1, 1, NumberAveragingAngles);
			}
		}

		private double GetInterpolatedBindingEnergy(
			double temperature
			)
		{
			if(temperature < InterpolatedBindingEnergies.Xmin)
			{
				return double.NegativeInfinity;
			}

			if(temperature > InterpolatedBindingEnergies.Xmax)
			{
				return double.PositiveInfinity;
			}

			return InterpolatedBindingEnergies.GetValue(temperature);
		}

		private double GetInterpolatedDecayWidth(
			double temperature
			)
		{
			if(temperature < InterpolatedDecayWidths.Xmin)
			{
				return 0;
			}

			if(temperature > InterpolatedDecayWidths.Xmax)
			{
				return double.PositiveInfinity;
			}

			return InterpolatedDecayWidths.GetValue(temperature);
		}
	}
}