using Meta.Numerics.Functions;
using System;

namespace Yburn.Fireball
{
	public abstract class ElectromagneticField
	{
		public static ElectromagneticField Create(
			FireballParam param
			)
		{
			switch(param.EMFCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					return new ElectromagneticField_URLimitFourierSynthesis(param);

				case EMFCalculationMethod.DiffusionApproximation:
					return new ElectromagneticField_DiffusionApproximation(param);

				case EMFCalculationMethod.FreeSpace:
					return new ElectromagneticField_FreeSpace(param);

				default:
					throw new Exception("Invalid Calculation Method.");
			}
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ElectromagneticField(
			FireballParam param
			)
		{
			EMFCalculationMethod = param.EMFCalculationMethod;
			QGPConductivityMeV = param.QGPConductivityMeV;
			MinFourierFrequency = param.MinFourierFrequency;
			MaxFourierFrequency = param.MaxFourierFrequency;
			FourierFrequencySteps = param.FourierFrequencySteps;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// for a point charge (with charge e) moving along the beam axis
		public abstract double CalculatePointChargeAzimutalMagneticField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
			);

		// for a point charge (with charge e) moving along the beam axis
		public abstract double CalculatePointChargeLongitudinalElectricField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
			);

		// for a point charge (with charge e) moving along the beam axis
		public abstract double CalculatePointChargeRadialElectricField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
			);

		public EuclideanVector3D CalculateSingleNucleusMagneticField(
			double effectiveTime,
			EuclideanVector2D coordinatesInReactionPlane,
			double lorentzFactor
			)
		{
			return null;
		}

		public EuclideanVector3D CalculateMagneticField(
			double t,
			EuclideanVector3D coordinates,
			double nucleiVelocity,
			double impactParameter
			)
		{
			double lorentzFactor = CalculateLorentzFactor(nucleiVelocity);
			EuclideanVector2D nucleusPosition = new EuclideanVector2D(impactParameter / 2, 0);
			EuclideanVector2D coordinatesInReactionPlane = new EuclideanVector2D(coordinates.X, coordinates.Y);

			// Nucleus A is located at negative x and moves in positive z direction
			EuclideanVector3D fieldNucleusA = CalculateSingleNucleusMagneticField(
				t - coordinates.Z / nucleiVelocity,
				coordinatesInReactionPlane + nucleusPosition,
				lorentzFactor);

			// Nucleus B is located at positive x and moves in negative z direction
			EuclideanVector3D fieldNucleusB = CalculateSingleNucleusMagneticField(
				t + coordinates.Z / nucleiVelocity,
				coordinatesInReactionPlane - nucleusPosition,
				lorentzFactor);

			return fieldNucleusA + fieldNucleusB;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EMFCalculationMethod EMFCalculationMethod;

		private double QGPConductivity;

		private double QGPConductivityMeV
		{
			get
			{
				return QGPConductivity * PhysConst.HBARC;
			}
			set
			{
				QGPConductivity = value / PhysConst.HBARC;
			}
		}

		private double MinFourierFrequency;

		private double MaxFourierFrequency;

		private int FourierFrequencySteps;

		private double[] FourierFrequency;

		private double CalculateLorentzFactor(
			double velocity
			)
		{
			return 1 / Math.Sqrt(1 - velocity * velocity);
		}

		private class ElectromagneticField_URLimitFourierSynthesis : ElectromagneticField
		{
			/********************************************************************************************
			 * Constructors
			 ********************************************************************************************/

			public ElectromagneticField_URLimitFourierSynthesis(FireballParam param)
				: base(param)
			{
				FourierFrequency = GetFourierFrequencyValueArray();
			}

			/********************************************************************************************
			 * Public members, functions and properties
			 ********************************************************************************************/

			public override double CalculatePointChargeAzimutalMagneticField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double integral = 0;
				for(int j = 1; j < FourierFrequencySteps; j++)
				{
					integral += PointChargeAzimutalMagneticFieldIntegrand(
							effectiveTime, radialDistance, lorentzFactor, j)
						* (FourierFrequency[j + 1] - FourierFrequency[j - 1]);
				}

				// first step vanishes because BesselJ(1, 0) = 0
				integral += PointChargeAzimutalMagneticFieldIntegrand(
						effectiveTime, radialDistance, lorentzFactor, FourierFrequencySteps)
					* (FourierFrequency[FourierFrequencySteps]
						- FourierFrequency[FourierFrequencySteps - 1]);

				integral *= 0.5;

				return PhysConst.ElementaryCharge / (2 * PhysConst.PI * QGPConductivity) * integral;
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double integral = 0;
				for(int j = 1; j < FourierFrequencySteps; j++)
				{
					integral += PointChargeLongitudinalElectricFieldIntegrand(
							effectiveTime, radialDistance, lorentzFactor, j)
						* (FourierFrequency[j + 1] - FourierFrequency[j - 1]);
				}

				// first and last steps
				integral += PointChargeLongitudinalElectricFieldIntegrand(
						effectiveTime, radialDistance, lorentzFactor, 0)
					* (FourierFrequency[1] - FourierFrequency[0]);

				integral += PointChargeLongitudinalElectricFieldIntegrand(
						effectiveTime, radialDistance, lorentzFactor, FourierFrequencySteps)
					* (FourierFrequency[FourierFrequencySteps]
						- FourierFrequency[FourierFrequencySteps - 1]);

				integral *= 0.5;

				return PhysConst.ElementaryCharge / (4 * PhysConst.PI) * integral;
			}

			public override double CalculatePointChargeRadialElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				// identical with azimutal magnetic field component in this limit
				return CalculatePointChargeAzimutalMagneticField(
					effectiveTime, radialDistance, lorentzFactor);
			}

			/********************************************************************************************
			 * Private/protected static members, functions and properties
			 ********************************************************************************************/

			/********************************************************************************************
			 * Private/protected members, functions and properties
			 ********************************************************************************************/

			private double[] GetFourierFrequencyValueArray()
			{
				double[] fourierFrequency = new double[FourierFrequencySteps + 1];

				double dk = Math.Exp(Math.Log(MaxFourierFrequency / MinFourierFrequency) / FourierFrequencySteps);
				fourierFrequency[0] = MinFourierFrequency;

				for(int n = 0; n < FourierFrequencySteps; n++)
				{
					fourierFrequency[n + 1] = fourierFrequency[n] * dk;
				}

				return fourierFrequency;
			}

			private double PointChargeAzimutalMagneticFieldIntegrand(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor,
				int fourierFrequencyIndex
				)
			{
				double x = Math.Sqrt(1 + 4 * FourierFrequency[fourierFrequencyIndex] * FourierFrequency[fourierFrequencyIndex]
					/ (lorentzFactor * lorentzFactor * QGPConductivity * QGPConductivity));

				return AdvancedMath.BesselJ(1, FourierFrequency[fourierFrequencyIndex] * radialDistance)
						* FourierFrequency[fourierFrequencyIndex] * FourierFrequency[fourierFrequencyIndex] / x
						* Math.Exp(0.5 * QGPConductivity * lorentzFactor * lorentzFactor * effectiveTime * (1 - x));
			}

			private double PointChargeLongitudinalElectricFieldIntegrand(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor,
				int fourierFrequencyIndex
				)
			{
				double x = Math.Sqrt(1 + 4 * FourierFrequency[fourierFrequencyIndex] * FourierFrequency[fourierFrequencyIndex]
					/ (lorentzFactor * lorentzFactor * QGPConductivity * QGPConductivity));

				return AdvancedMath.BesselJ(0, FourierFrequency[fourierFrequencyIndex] * radialDistance)
						* FourierFrequency[fourierFrequencyIndex] * (1 - x) / x
						* Math.Exp(0.5 * QGPConductivity * lorentzFactor * lorentzFactor * effectiveTime * (1 - x));
			}
		}

		private class ElectromagneticField_DiffusionApproximation : ElectromagneticField
		{
			/********************************************************************************************
			 * Constructors
			 ********************************************************************************************/

			public ElectromagneticField_DiffusionApproximation(FireballParam param) : base(param)
			{
			}

			/********************************************************************************************
			 * Public members, functions and properties
			 ********************************************************************************************/

			public override double CalculatePointChargeAzimutalMagneticField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				return 0.125 * PhysConst.ElementaryCharge / PhysConst.PI
					* (radialDistance * QGPConductivity) / (effectiveTime * effectiveTime)
					* Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivity / effectiveTime);
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				return -0.25 * PhysConst.ElementaryCharge / PhysConst.PI
					* (effectiveTime - 0.25 * radialDistance * radialDistance * QGPConductivity)
					/ (lorentzFactor * lorentzFactor * effectiveTime * effectiveTime * effectiveTime)
					* Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivity / effectiveTime);
			}

			public override double CalculatePointChargeRadialElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				// identical with azimutal magnetic field component in this limit
				return CalculatePointChargeAzimutalMagneticField(
					effectiveTime, radialDistance, lorentzFactor);
			}

			/********************************************************************************************
			 * Private/protected static members, functions and properties
			 ********************************************************************************************/

			/********************************************************************************************
			 * Private/protected members, functions and properties
			 ********************************************************************************************/
		}

		private class ElectromagneticField_FreeSpace : ElectromagneticField
		{
			/********************************************************************************************
			 * Constructors
			 ********************************************************************************************/

			public ElectromagneticField_FreeSpace(FireballParam param) : base(param)
			{
			}

			/********************************************************************************************
			 * Public members, functions and properties
			 ********************************************************************************************/

			public override double CalculatePointChargeAzimutalMagneticField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double velocity = Math.Sqrt(1 - 1 / (lorentzFactor * lorentzFactor));
				double denominator = Math.Pow(
					radialDistance * radialDistance + lorentzFactor * lorentzFactor
						* velocity * velocity * effectiveTime * effectiveTime,
					1.5);

				return PhysConst.ElementaryCharge * lorentzFactor / (4 * PhysConst.PI)
					* velocity * radialDistance / denominator;
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double velocity = Math.Sqrt(1 - 1 / (lorentzFactor * lorentzFactor));
				double denominator = Math.Pow(
					radialDistance * radialDistance + lorentzFactor * lorentzFactor
						* velocity * velocity * effectiveTime * effectiveTime,
					1.5);

				return PhysConst.ElementaryCharge * lorentzFactor / (4 * PhysConst.PI)
					* (-velocity * effectiveTime) / denominator;
			}

			public override double CalculatePointChargeRadialElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double velocity = Math.Sqrt(1 - 1 / (lorentzFactor * lorentzFactor));
				double denominator = Math.Pow(
					radialDistance * radialDistance + lorentzFactor * lorentzFactor
						* velocity * velocity * effectiveTime * effectiveTime,
					1.5);

				return PhysConst.ElementaryCharge * lorentzFactor / (4 * PhysConst.PI)
					* radialDistance / denominator;
			}

			/********************************************************************************************
			 * Private/protected static members, functions and properties
			 ********************************************************************************************/

			/********************************************************************************************
			 * Private/protected members, functions and properties
			 ********************************************************************************************/
		}
	}
}