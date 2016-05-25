using Meta.Numerics.Functions;
using System;

namespace Yburn.Fireball
{
	public abstract class PointChargeElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected PointChargeElectromagneticField(FireballParam param)
		{
			EMFCalculationMethod = param.EMFCalculationMethod;
			QGPConductivityMeV = param.QGPConductivityMeV;
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

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

		private class PointChargeElectromagneticField_URLimitFourierSynthesis : PointChargeElectromagneticField
		{
			/********************************************************************************************
			 * Constructors
			 ********************************************************************************************/

			public PointChargeElectromagneticField_URLimitFourierSynthesis(FireballParam param)
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
				OneVariableIntegrand integrand = k => PointChargeAzimutalMagneticFieldIntegrand(
					k, effectiveTime, radialDistance, lorentzFactor);

				double integral = Quadrature.UseGaussLegendreOverPositiveAxis(integrand, 1);

				return PhysConst.ElementaryCharge / (2 * Math.PI * QGPConductivity) * integral;
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				OneVariableIntegrand integrand = k => PointChargeLongitudinalElectricFieldIntegrand(
					k, effectiveTime, radialDistance, lorentzFactor);

				double integral = Quadrature.UseGaussLegendreOverPositiveAxis(integrand, 1);

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

			private double PointChargeAzimutalMagneticFieldIntegrand(
				double fourierFrequency,
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double x;
				double exponentialPart;
				CalculateShorthands(
					fourierFrequency, effectiveTime, lorentzFactor, out x, out exponentialPart);

				return AdvancedMath.BesselJ(1, fourierFrequency * radialDistance)
						* fourierFrequency * fourierFrequency / x * exponentialPart;
			}

			private double PointChargeLongitudinalElectricFieldIntegrand(
				double fourierFrequency,
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double x;
				double exponentialPart;
				CalculateShorthands(
					fourierFrequency, effectiveTime, lorentzFactor, out x, out exponentialPart);

				return AdvancedMath.BesselJ(0, fourierFrequency * radialDistance)
						* fourierFrequency * (1 - x) / x * exponentialPart;
			}

			private void CalculateShorthands(
				double fourierFrequency,
				double effectiveTime,
				double lorentzFactor,
				out double x,
				out double exponentialPart
				)
			{
				x = Math.Sqrt(
					1 + 4 * Math.Pow(fourierFrequency / (lorentzFactor * QGPConductivity), 2));

				exponentialPart = Math.Exp(
					0.5 * QGPConductivity * lorentzFactor * lorentzFactor * effectiveTime * (1 - x));
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
				double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

				return PhysConst.ElementaryCharge * (radialDistance * QGPConductivity)
					/ (8 * Math.PI * effectiveTime * effectiveTime)
					* exponentialPart;
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

				return PhysConst.ElementaryCharge
					* (0.25 * radialDistance * radialDistance * QGPConductivity - effectiveTime)
					/ (4 * Math.PI * lorentzFactor * lorentzFactor * effectiveTime * effectiveTime * effectiveTime)
					* exponentialPart;
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

			private double CalculateExponentialPart(
				double effectiveTime,
				double radialDistance
				)
			{
				return Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivity / effectiveTime);
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
				double velocity;
				double denominator = CalculateDenominator(
					effectiveTime, radialDistance, lorentzFactor, out velocity);

				return PhysConst.ElementaryCharge * lorentzFactor * velocity * radialDistance
					/ (4 * Math.PI * denominator);
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double velocity;
				double denominator = CalculateDenominator(
					effectiveTime, radialDistance, lorentzFactor, out velocity);

				return PhysConst.ElementaryCharge * lorentzFactor * (-velocity * effectiveTime)
					/ (4 * Math.PI * denominator);
			}

			public override double CalculatePointChargeRadialElectricField(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor
				)
			{
				double velocity;
				double denominator = CalculateDenominator(
					effectiveTime, radialDistance, lorentzFactor, out velocity);

				return PhysConst.ElementaryCharge * lorentzFactor * radialDistance
					/ (4 * Math.PI * denominator);
			}

			/********************************************************************************************
			 * Private/protected members, functions and properties
			 ********************************************************************************************/

			private double CalculateDenominator(
				double effectiveTime,
				double radialDistance,
				double lorentzFactor,
				out double velocity
				)
			{
				velocity = CalculateVelocity(lorentzFactor);

				return Math.Pow(
					radialDistance * radialDistance + Math.Pow(lorentzFactor * velocity * effectiveTime, 2),
					1.5);
			}

			private double CalculateVelocity(
				double lorentzFactor
				)
			{
				return Math.Sqrt(1 - 1 / (lorentzFactor * lorentzFactor));
			}
		}
	}
}