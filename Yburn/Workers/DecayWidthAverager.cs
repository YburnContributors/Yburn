using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.PhysUtil;
using Yburn.QQState;

namespace Yburn.Workers
{

	public delegate double TemperatureDependentFunction(double temperature);

	public class DecayWidthAverager
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double GetAveragedTemperature(
			double qgpTemperature,
			double qgpVelocity
			)
		{
			if(qgpVelocity > 0)
			{
				return qgpTemperature
					* Math.Sqrt(1.0 - qgpVelocity * qgpVelocity) * Functions.Artanh(qgpVelocity) / qgpVelocity;
			}
			else
			{
				return qgpTemperature;
			}
		}

		public static double GetDopplerShiftedTemperature(
			double qgpTemperature,
			double qgpVelocity,
			double cosine
			)
		{
			return qgpTemperature
				* Math.Sqrt(1.0 - qgpVelocity * qgpVelocity) / (1.0 - qgpVelocity * cosine);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

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

		public readonly double QGPFormationTemperature;

		public double GetInMediumDecayWidth(
			double qgpTemperature,
			double qgpVelocity,
			DecayWidthEvaluationType evaluationType
			)
		{
			if(!ExistsMedium(qgpTemperature))
			{
				return 0;
			}
			else
			{
				switch(evaluationType)
				{
					case DecayWidthEvaluationType.UnshiftedTemperature:
						return GetScaledDecayWidth(qgpTemperature);

					case DecayWidthEvaluationType.MaximallyBlueshifted:
						return GetScaledDecayWidth(
							GetDopplerShiftedTemperature(qgpTemperature, qgpVelocity, 1));

					case DecayWidthEvaluationType.AveragedTemperature:
						return GetScaledDecayWidth(
							GetAveragedTemperature(qgpTemperature, qgpVelocity));

					case DecayWidthEvaluationType.AveragedDecayWidth:
						return GetScaledAveragedDecayWidth(qgpTemperature, qgpVelocity);

					default:
						throw new Exception("Invalid DecayWidthEvaluationType.");
				}
			}
		}

		public double GetDecayWidth(
			double effectiveTemperature
			)
		{
			return InterpolateDecayWidth(effectiveTemperature);
		}

		public double GetExistenceProbability(
			double effectiveTemperature
			)
		{
			return Functions.HeavisideStepFunction(-InterpolateBindingEnergy(effectiveTemperature));
		}

		public double GetAveragedDecayWidth(
		double qgpTemperature,
		double qgpVelocity
		)
		{
			return CalculateAverageDopplerShift(GetDecayWidth, qgpTemperature, qgpVelocity);
		}

		public double GetAveragedExistenceProbability(
			double qgpTemperature,
			double qgpVelocity
			)
		{
			return CalculateAverageDopplerShift(GetExistenceProbability, qgpTemperature, qgpVelocity);
		}

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

		private bool ExistsMedium(
			double qgpTemperature
			)
		{
			return qgpTemperature >= QGPFormationTemperature;
		}

		private double GetScaledDecayWidth(
			double effectiveTemperature
			)
		{
			return GetDecayWidth(effectiveTemperature)
				/ GetExistenceProbability(effectiveTemperature);
		}

		private double GetScaledAveragedDecayWidth(
			double qgpTemperature,
			double qgpVelocity
			)
		{
			return GetAveragedDecayWidth(qgpTemperature, qgpVelocity)
				/ GetAveragedExistenceProbability(qgpTemperature, qgpVelocity);
		}

		private double CalculateAverageDopplerShift(
			TemperatureDependentFunction function,
			double qgpTemperature,
			double qgpVelocity
			)
		{
			Func<double, double> integrand = cosine => function(
				GetDopplerShiftedTemperature(qgpTemperature, qgpVelocity, cosine));

			return 0.5 * Quadrature.IntegrateOverInterval(integrand, -1, 1, NumberAveragingAngles);
		}

		private double InterpolateBindingEnergy(
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

		private double InterpolateDecayWidth(
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
