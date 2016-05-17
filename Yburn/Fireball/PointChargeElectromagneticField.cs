using Meta.Numerics.Functions;
using System;

namespace Yburn.Fireball
{
	public abstract class PointChargeElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public PointChargeElectromagneticField(FireballParam param)
		{
			EMFCalculationMethod = param.EMFCalculationMethod;
			QGPConductivityMeV = param.QGPConductivityMeV;
			MinFourierFrequency = param.MinFourierFrequency;
			MaxFourierFrequency = param.MaxFourierFrequency;
			FourierFrequencySteps = param.FourierFrequencySteps;
		}

		public static PointChargeElectromagneticField Create(
			FireballParam param
			)
		{
			switch(param.EMFCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					return new PointChargeElectromagneticField_URLimitFourierSynthesis(param);

				case EMFCalculationMethod.DiffusionApproximation:
					return new PointChargeElectromagneticField_DiffusionApproximation(param);

				case EMFCalculationMethod.FreeSpace:
					return new PointChargeElectromagneticField_FreeSpace(param);

				default:
					throw new Exception("Invalid Calculation Method.");
			}
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

		public EuclideanVector3D CalculatePointChargeMagneticField(
			double effectiveTime,
			EuclideanVector2D positionInReactionPlane,
			double lorentzFactor
			)
		{
			EuclideanVector2D azimutalPart =
				CalculatePointChargeAzimutalMagneticField(
					effectiveTime, positionInReactionPlane.Norm, lorentzFactor)
				* EuclideanVector2D.CreateAzimutalUnitVectorAtPosition(positionInReactionPlane);

			return new EuclideanVector3D(azimutalPart, 0);
		}

		public EuclideanVector3D CalculatePointChargeElectricField(
			double effectiveTime,
			EuclideanVector2D positionInReactionPlane,
			double lorentzFactor
			)
		{
			EuclideanVector2D radialPart =
				CalculatePointChargeRadialElectricField(
					effectiveTime, positionInReactionPlane.Norm, lorentzFactor)
				* EuclideanVector2D.CreateRadialUnitVectorAtPosition(positionInReactionPlane);

			double longitudinalPart =
				CalculatePointChargeLongitudinalElectricField(
					effectiveTime, positionInReactionPlane.Norm, lorentzFactor);

			return new EuclideanVector3D(radialPart, longitudinalPart);
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

		private class PointChargeElectromagneticField_URLimitFourierSynthesis : PointChargeElectromagneticField
		{
			/********************************************************************************************
			 * Constructors
			 ********************************************************************************************/

			public PointChargeElectromagneticField_URLimitFourierSynthesis(FireballParam param)
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

				return PhysConst.ElementaryCharge / (2 * Math.PI * QGPConductivity) * integral;
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

				return PhysConst.ElementaryCharge / (4 * Math.PI) * integral;
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

		private class PointChargeElectromagneticField_DiffusionApproximation : PointChargeElectromagneticField
		{
			/********************************************************************************************
			 * Constructors
			 ********************************************************************************************/

			public PointChargeElectromagneticField_DiffusionApproximation(FireballParam param)
				: base(param)
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
				return 0.125 * PhysConst.ElementaryCharge / Math.PI
					* (radialDistance * QGPConductivity) / (effectiveTime * effectiveTime)
					* Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivity / effectiveTime);
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				return -0.25 * PhysConst.ElementaryCharge / Math.PI
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
		}

		private class PointChargeElectromagneticField_FreeSpace : PointChargeElectromagneticField
		{
			/********************************************************************************************
			 * Constructors
			 ********************************************************************************************/

			public PointChargeElectromagneticField_FreeSpace(FireballParam param)
				: base(param)
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

				return PhysConst.ElementaryCharge * lorentzFactor / (4 * Math.PI)
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

				return PhysConst.ElementaryCharge * lorentzFactor / (4 * Math.PI)
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

				return PhysConst.ElementaryCharge * lorentzFactor / (4 * Math.PI)
					* radialDistance / denominator;
			}
		}
	}
}