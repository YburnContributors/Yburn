using MathNet.Numerics.Integration;
using System;
using Yburn.Fireball;
using Yburn.PhysUtil;

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

		private static double GetAbsoluteMagneticPotentialEnergy(
			double magneticFieldStrength
			)
		{
			return Math.Abs(2 * Constants.MagnetonBottomQuark_fm
				* magneticFieldStrength * Constants.HbarC_MeV_fm);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public DecayWidthAverager(
			LinearInterpolation1D interpolatedDecayWidths,
			LinearInterpolation1D interpolatedEnergies,
			LinearInterpolation1D interpolatedDisplacementRMS,
			DopplerShiftEvaluationType dopplerShiftEvaluationType,
			ElectricDipoleAlignment electricDipoleAlignment,
			double qgpFormationTemperature,
			int numberAveragingAngles
			)
		{
			InterpolatedDecayWidths = interpolatedDecayWidths;
			InterpolatedEnergies = interpolatedEnergies;
			InterpolatedDisplacementRMS = interpolatedDisplacementRMS;

			DopplerShiftEvaluationType = dopplerShiftEvaluationType;
			ElectricDipoleAlignment = electricDipoleAlignment;

			QGPFormationTemperature = qgpFormationTemperature;
			NumberAveragingAngles = numberAveragingAngles;

			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double GetInMediumDecayWidth(
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
				switch(DopplerShiftEvaluationType)
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
			return InterpolatedDecayWidths.GetValue(temperature, 0, double.PositiveInfinity);
		}

		public double GetEnergy(
			double temperature
			)
		{
			return InterpolatedEnergies.GetValue(
				temperature, double.NegativeInfinity, double.PositiveInfinity);
		}

		public double GetDisplacementRMS(
			double temperature
			)
		{
			return InterpolatedDisplacementRMS.GetValue(temperature, 0, 0);
		}

		public double GetExistenceProbability(
			double temperature,
			double electricFieldStrength,
			double magneticFieldStrength
			)
		{
			if(electricFieldStrength == 0 && magneticFieldStrength == 0)
			{
				return Functions.HeavisideStepFunction(-GetEnergy(temperature));
			}
			else
			{
				double A = -GetEnergy(temperature);
				double B = 0;
				double C = -GetAbsoluteMagneticPotentialEnergy(magneticFieldStrength);

				switch(ElectricDipoleAlignment)
				{
					case ElectricDipoleAlignment.WeakenBinding:
						A -= GetAbsoluteElectricPotentialEnergy(temperature, electricFieldStrength);
						break;

					case ElectricDipoleAlignment.StrengthenBinding:
						A += GetAbsoluteElectricPotentialEnergy(temperature, electricFieldStrength);
						break;

					case ElectricDipoleAlignment.Random:
						B = GetAbsoluteElectricPotentialEnergy(temperature, electricFieldStrength);
						break;

					default:
						throw new Exception("Invalid ElectricDipoleAlignment.");
				}

				return (2 * Functions.AveragedHeavisideStepFunctionWithLinearArgument(A, B)
					+ Functions.AveragedHeavisideStepFunctionWithLinearArgument(A + C, B)) / 3;
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double QGPFormationTemperature;

		private readonly int NumberAveragingAngles;

		private readonly DopplerShiftEvaluationType DopplerShiftEvaluationType;

		private readonly ElectricDipoleAlignment ElectricDipoleAlignment;

		private readonly LinearInterpolation1D InterpolatedDecayWidths;

		private readonly LinearInterpolation1D InterpolatedEnergies;

		private readonly LinearInterpolation1D InterpolatedDisplacementRMS;

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

		private double GetAbsoluteElectricPotentialEnergy(
			double temperature,
			double electricFieldStrength
			)
		{
			return Math.Abs(Constants.ChargeBottomQuark * GetDisplacementRMS(temperature)
				* electricFieldStrength * Constants.HbarC_MeV_fm);
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
