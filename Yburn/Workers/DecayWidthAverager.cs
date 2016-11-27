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
				return qgpTemperature * Math.Sqrt(1.0 - qgpVelocity * qgpVelocity)
					* Functions.Artanh(qgpVelocity) / qgpVelocity;
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

		public static double GetMaximallyBlueshiftedTemperature(
			double qgpTemperature,
			double qgpVelocity
			)
		{
			return GetDopplerShiftedTemperature(qgpTemperature, qgpVelocity, 1);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void SetInterpolation(
			List<QQDataSet> dataSetList,
			DecayWidthType decayWidthType,
			out LinearInterpolation1D interpolatedDecayWidths,
			out LinearInterpolation1D interpolatedEnergies
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
					gammaList[j] = dataSetList[j].GetGamma(decayWidthType);
					energyList[j] = dataSetList[j].Energy;
				}
			}
			else
			{
				temperatureList = new double[] { 0 };
				gammaList = new double[] { double.PositiveInfinity };
				energyList = new double[] { 0 };
			}

			interpolatedDecayWidths = new LinearInterpolation1D(temperatureList, gammaList);
			interpolatedEnergies = new LinearInterpolation1D(temperatureList, energyList);
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
				out InterpolatedDecayWidths, out InterpolatedEnergies);
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
			DopplerShiftEvaluationType evaluationType
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
					case DopplerShiftEvaluationType.UnshiftedTemperature:
						return GetEffectiveDecayWidth(qgpTemperature);

					case DopplerShiftEvaluationType.MaximallyBlueshifted:
						return GetEffectiveDecayWidth(
							GetMaximallyBlueshiftedTemperature(qgpTemperature, qgpVelocity));

					case DopplerShiftEvaluationType.AveragedTemperature:
						return GetEffectiveDecayWidth(
							GetAveragedTemperature(qgpTemperature, qgpVelocity));

					case DopplerShiftEvaluationType.AveragedDecayWidth:
						return GetAveragedDecayWidth(qgpTemperature, qgpVelocity);

					case DopplerShiftEvaluationType.AveragedLifeTime:
						return GetInverselyAveragedDecayWidth(qgpTemperature, qgpVelocity);

					default:
						throw new Exception("Invalid DopplerShiftEvaluationType.");
				}
			}
		}

		public double GetDecayWidth(
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

		public double GetEnergy(
			double temperature
			)
		{
			if(temperature < InterpolatedEnergies.Xmin)
			{
				return double.NegativeInfinity;
			}

			if(temperature > InterpolatedEnergies.Xmax)
			{
				return double.PositiveInfinity;
			}

			return InterpolatedEnergies.GetValue(temperature);
		}

		public double GetExistenceProbability(
			double temperature
			)
		{
			return Functions.HeavisideStepFunction(-GetEnergy(temperature));
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly int NumberAveragingAngles;

		private readonly LinearInterpolation1D InterpolatedDecayWidths;

		private readonly LinearInterpolation1D InterpolatedEnergies;

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

		private double GetEffectiveDecayWidth(
			double temperature
			)
		{
			return GetDecayWidth(temperature)
				/ GetExistenceProbability(temperature);
		}

		private double GetAveragedDecayWidth(
			double qgpTemperature,
			double qgpVelocity
			)
		{
			return CalculateAverageDopplerShift(GetDecayWidth, qgpTemperature, qgpVelocity)
				/ GetExistenceProbability(
					GetMaximallyBlueshiftedTemperature(qgpTemperature, qgpVelocity));
		}

		private double GetInverselyAveragedDecayWidth(
			double qgpTemperature,
			double qgpVelocity
			)
		{
			TemperatureDependentFunction effectiveLifeTime
				= temperature => 1 / GetEffectiveDecayWidth(temperature);

			return 1 / CalculateAverageDopplerShift(effectiveLifeTime, qgpTemperature, qgpVelocity);
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
	}
}
