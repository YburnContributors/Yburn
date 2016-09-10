using Meta.Numerics.Functions;
using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum EMFCalculationMethod
	{
		URLimitFourierSynthesis,
		DiffusionApproximation,
		FreeSpace
	}

	public abstract class PointChargeElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected PointChargeElectromagneticField(
			EMFCalculationMethod emfCalculationMethod,
			double qgpConductivityMeV,
			double pointChargeRapidity
			)
		{
			EMFCalculationMethod = emfCalculationMethod;
			QGPConductivityPerFm = qgpConductivityMeV / Constants.HbarCMeVFm;
			PointChargeRapidity = pointChargeRapidity;
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static PointChargeElectromagneticField Create(
			EMFCalculationMethod emfCalculationMethod,
			double qgpConductivityMeV,
			double pointChargeRapidity
			)
		{
			switch(emfCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					return new PointChargeElectromagneticField_URLimitFourierSynthesis(
						emfCalculationMethod,
						qgpConductivityMeV,
						pointChargeRapidity);

				case EMFCalculationMethod.DiffusionApproximation:
					return new PointChargeElectromagneticField_DiffusionApproximation(
						emfCalculationMethod,
						qgpConductivityMeV,
						pointChargeRapidity);

				case EMFCalculationMethod.FreeSpace:
					return new PointChargeElectromagneticField_FreeSpace(
						emfCalculationMethod,
						qgpConductivityMeV,
						pointChargeRapidity);

				default:
					throw new Exception("Invalid Calculation Method.");
			}
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public abstract double CalculateAzimutalMagneticField(
			double effectiveTime,
			double radialDistance
			);

		public abstract double CalculateLongitudinalElectricField(
			double effectiveTime,
			double radialDistance
			);

		public abstract double CalculateRadialElectricField(
			double effectiveTime,
			double radialDistance
			);

		public virtual double CalculateAzimutalMagneticField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			return Math.Cosh(observerRapidity) * CalculateAzimutalMagneticField(
					effectiveTime, radialDistance)
				- Math.Sinh(observerRapidity) * CalculateRadialElectricField(
					effectiveTime, radialDistance);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EMFCalculationMethod EMFCalculationMethod;

		private double QGPConductivityPerFm;

		private double PointChargeRapidity;

		private class PointChargeElectromagneticField_URLimitFourierSynthesis
			: PointChargeElectromagneticField
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public PointChargeElectromagneticField_URLimitFourierSynthesis(
				EMFCalculationMethod emfCalculationMethod,
				double qgpConductivityMeV,
				double pointChargeRapidity
				) : base(emfCalculationMethod, qgpConductivityMeV, pointChargeRapidity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override double CalculateAzimutalMagneticField(
				double effectiveTime,
				double radialDistance
				)
			{
				// up to sign identical with radial electric field component in this limit
				return Math.Sign(PointChargeRapidity) * CalculateRadialElectricField(
					effectiveTime, radialDistance);
			}

			public override double CalculateLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					Func<double, double> integrand = k =>
					PointChargeLongitudinalElectricFieldIntegrand(k, effectiveTime, radialDistance);

					double integral = Quadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return Math.Sign(PointChargeRapidity) * Constants.ElementaryCharge / (4 * Math.PI)
						* integral;
				}
				else
				{
					return 0;
				}
			}

			public override double CalculateRadialElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					Func<double, double> integrand = k =>
					 PointChargeRadialElectricFieldIntegrand(k, effectiveTime, radialDistance);

					double integral = Quadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return Constants.ElementaryCharge / (2 * Math.PI * QGPConductivityPerFm)
						* integral;
				}
				else
				{
					return 0;
				}
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double PointChargeLongitudinalElectricFieldIntegrand(
				double fourierFrequency,
				double effectiveTime,
				double radialDistance
				)
			{
				double x;
				double exponentialPart;
				CalculateShorthands(
					fourierFrequency, effectiveTime, out x, out exponentialPart);

				return AdvancedMath.BesselJ(0, fourierFrequency * radialDistance)
						* fourierFrequency * (1 - x) / x * exponentialPart;
			}

			private double PointChargeRadialElectricFieldIntegrand(
				double fourierFrequency,
				double effectiveTime,
				double radialDistance
				)
			{
				double x;
				double exponentialPart;
				CalculateShorthands(
					fourierFrequency, effectiveTime, out x, out exponentialPart);

				return AdvancedMath.BesselJ(1, fourierFrequency * radialDistance)
						* fourierFrequency * fourierFrequency / x * exponentialPart;
			}

			private void CalculateShorthands(
				double fourierFrequency,
				double effectiveTime,
				out double x,
				out double exponentialPart
				)
			{
				double lorentzFactor = Math.Cosh(PointChargeRapidity);

				x = Math.Sqrt(1 + 4 * Math.Pow(
					fourierFrequency / (lorentzFactor * QGPConductivityPerFm), 2));

				exponentialPart = Math.Exp(0.5 * QGPConductivityPerFm
					* lorentzFactor * lorentzFactor * effectiveTime * (1 - x));
			}
		}

		private class PointChargeElectromagneticField_DiffusionApproximation
			: PointChargeElectromagneticField
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public PointChargeElectromagneticField_DiffusionApproximation(
				EMFCalculationMethod emfCalculationMethod,
				double qgpConductivityMeV,
				double pointChargeRapidity
				) : base(emfCalculationMethod, qgpConductivityMeV, pointChargeRapidity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override double CalculateAzimutalMagneticField(
				double effectiveTime,
				double radialDistance
				)
			{
				// up to sign identical with radial electric field component in this limit
				return Math.Sign(PointChargeRapidity) * CalculateRadialElectricField(
					effectiveTime, radialDistance);
			}

			public override double CalculateLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

					return Math.Sign(PointChargeRapidity) * Constants.ElementaryCharge
						* (0.25 * radialDistance * radialDistance * QGPConductivityPerFm
							- effectiveTime)
						/ (4 * Math.PI * effectiveTime * effectiveTime * effectiveTime
							* Math.Cosh(PointChargeRapidity) * Math.Cosh(PointChargeRapidity))
						* exponentialPart;
				}
				else
				{
					return 0;
				}
			}

			public override double CalculateRadialElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

					return Constants.ElementaryCharge * (radialDistance * QGPConductivityPerFm)
						/ (8 * Math.PI * effectiveTime * effectiveTime)
						* exponentialPart;
				}
				else
				{
					return 0;
				}
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double CalculateExponentialPart(
				double effectiveTime,
				double radialDistance
				)
			{
				return Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivityPerFm
					/ effectiveTime);
			}
		}

		private class PointChargeElectromagneticField_FreeSpace
			: PointChargeElectromagneticField
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public PointChargeElectromagneticField_FreeSpace(
				EMFCalculationMethod emfCalculationMethod,
				double qgpConductivityMeV,
				double pointChargeRapidity
				) : base(emfCalculationMethod, qgpConductivityMeV, pointChargeRapidity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override double CalculateAzimutalMagneticField(
				double effectiveTime,
				double radialDistance
				)
			{
				double denominator = CalculateDenominator(effectiveTime, radialDistance);

				return Constants.ElementaryCharge * Math.Sinh(PointChargeRapidity) * radialDistance
					/ (4 * Math.PI * denominator);
			}

			public override double CalculateLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				double denominator = CalculateDenominator(effectiveTime, radialDistance);

				return -Constants.ElementaryCharge * Math.Sinh(PointChargeRapidity) * effectiveTime
					/ (4 * Math.PI * denominator);
			}

			public override double CalculateRadialElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				double denominator = CalculateDenominator(effectiveTime, radialDistance);

				return Constants.ElementaryCharge * Math.Cosh(PointChargeRapidity)
					* radialDistance / (4 * Math.PI * denominator);
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double CalculateDenominator(
				double effectiveTime,
				double radialDistance
				)
			{
				return Math.Pow(radialDistance * radialDistance + Math.Pow(
					Math.Sinh(PointChargeRapidity) * effectiveTime, 2), 1.5);
			}
		}
	}
}