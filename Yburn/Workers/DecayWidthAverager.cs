using MathNet.Numerics.Integration;
using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.PhysUtil;
using Yburn.QQState;

namespace Yburn.Workers
{

	public delegate double QQDataFunction(double temperature);

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
			out LinearInterpolation1D interpolatedRadiusRMS,
			out LinearInterpolation1D interpolatedEnergies,
			out LinearInterpolation1D interpolatedDecayWidths
			)
		{
			int listSize = dataSetList.Count;

			double[] temperatureList = new double[listSize];
			double[] radiusList = new double[listSize];
			double[] energyList = new double[listSize];
			double[] gammaList = new double[listSize];

			if(listSize > 0)
			{
				for(int j = 0; j < listSize; j++)
				{
					temperatureList[j] = dataSetList[j].Temperature;
					radiusList[j] = dataSetList[j].RadiusRMS;
					gammaList[j] = dataSetList[j].GetGamma(decayWidthType);
					energyList[j] = dataSetList[j].Energy;
				}
			}
			else
			{
				temperatureList = new double[] { 0 };
				radiusList = new double[] { 0 };
				gammaList = new double[] { double.PositiveInfinity };
				energyList = new double[] { 0 };
			}

			interpolatedRadiusRMS = new LinearInterpolation1D(temperatureList, radiusList);
			interpolatedEnergies = new LinearInterpolation1D(temperatureList, energyList);
			interpolatedDecayWidths = new LinearInterpolation1D(temperatureList, gammaList);
		}

		private static double GetMagneticDipoleMoment()
		{
			return 2 * Constants.MagnetonBottomQuarkFm / 3.0;
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
				out InterpolatedRadiusRMS, out InterpolatedEnergies, out InterpolatedDecayWidths);
			QGPFormationTemperature = qgpFormationTemperature;
			NumberAveragingAngles = numberAveragingAngles;
			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly double QGPFormationTemperature;

		public double GetInMediumDecayWidth(
			DopplerShiftEvaluationType evaluationType,
			double qgpTemperature,
			double qgpVelocity,
			double electricFieldStrength,
			double magneticFieldStrength
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
						return GetEffectiveDecayWidth(
							qgpTemperature, electricFieldStrength, magneticFieldStrength);

					case DopplerShiftEvaluationType.MaximallyBlueshifted:
						return GetEffectiveDecayWidth(
							GetMaximallyBlueshiftedTemperature(qgpTemperature, qgpVelocity),
							electricFieldStrength, magneticFieldStrength);

					case DopplerShiftEvaluationType.AveragedTemperature:
						return GetEffectiveDecayWidth(
							GetAveragedTemperature(qgpTemperature, qgpVelocity),
							electricFieldStrength, magneticFieldStrength);

					case DopplerShiftEvaluationType.AveragedDecayWidth:
						return GetAveragedDecayWidth(
							qgpTemperature, qgpVelocity,
							electricFieldStrength, magneticFieldStrength);

					case DopplerShiftEvaluationType.AveragedLifeTime:
						return GetInverselyAveragedDecayWidth(
							qgpTemperature, qgpVelocity,
							electricFieldStrength, magneticFieldStrength);

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

		public double GetRadiusRMS(
			double temperature
			)
		{
			if(temperature < InterpolatedRadiusRMS.Xmin)
			{
				return 0;
			}

			if(temperature > InterpolatedRadiusRMS.Xmax)
			{
				return 0;
			}

			return InterpolatedRadiusRMS.GetValue(temperature);
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

		public double GetEnergyInElectromagneticField(
			double temperature,
			double electricFieldStrength,
			double magneticFieldStrength
			)
		{
			return GetEnergy(temperature)
				- GetElectricDipoleMoment(temperature) * electricFieldStrength * Constants.HbarCMeVFm
				- GetMagneticDipoleMoment() * magneticFieldStrength * Constants.HbarCMeVFm;
		}

		public double GetExistenceProbability(
			double temperature,
			double electricFieldStrength,
			double magneticFieldStrength
			)
		{
			double energy = GetEnergyInElectromagneticField(
				temperature, electricFieldStrength, magneticFieldStrength);

			return Functions.HeavisideStepFunction(-energy);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly int NumberAveragingAngles;

		private readonly LinearInterpolation1D InterpolatedDecayWidths;

		private readonly LinearInterpolation1D InterpolatedRadiusRMS;

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

		private double GetElectricDipoleMoment(
			double temperature
			)
		{
			return Math.Abs(Constants.ChargeBottomQuark) * 2 * GetRadiusRMS(temperature);
		}

		private double GetEffectiveDecayWidth(
			double temperature,
			double electricFieldStrength,
			double magneticFieldStrength
			)
		{
			return GetDecayWidth(temperature)
				/ GetExistenceProbability(temperature, electricFieldStrength, magneticFieldStrength);
		}

		private double GetAveragedDecayWidth(
			double qgpTemperature,
			double qgpVelocity,
			double electricFieldStrength,
			double magneticFieldStrength
			)
		{
			QQDataFunction decayWidth = temperature => GetEffectiveDecayWidth(
				temperature, electricFieldStrength, magneticFieldStrength);

			return CalculateAverageDopplerShift(decayWidth, qgpTemperature, qgpVelocity);
		}

		private double GetInverselyAveragedDecayWidth(
			double qgpTemperature,
			double qgpVelocity,
			double electricFieldStrength,
			double magneticFieldStrength
			)
		{
			QQDataFunction lifeTime = temperature => 1 / GetEffectiveDecayWidth(
				temperature, electricFieldStrength, magneticFieldStrength);

			return 1 / CalculateAverageDopplerShift(lifeTime, qgpTemperature, qgpVelocity);
		}

		private double CalculateAverageDopplerShift(
			QQDataFunction function,
			double qgpTemperature,
			double qgpVelocity
			)
		{
			Func<double, double> integrand = cosine => function(
				GetDopplerShiftedTemperature(qgpTemperature, qgpVelocity, cosine));

			return 0.5 * NewtonCotesTrapeziumRule.IntegrateComposite(
				integrand, -1, 1, NumberAveragingAngles);
		}
	}
}
