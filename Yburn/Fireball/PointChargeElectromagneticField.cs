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

	public enum EMFComponent
	{
		AzimuthalMagneticField,
		LongitudinalElectricField,
		RadialElectricField
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
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		protected static double Coupling = Constants.ElementaryCharge / (4 * Math.PI);

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double CalculateElectromagneticField(
			EMFComponent component,
			double effectiveTime,
			double radialDistance
			)
		{
			switch(component)
			{
				case EMFComponent.AzimuthalMagneticField:
					return CalculateAzimuthalMagneticField(effectiveTime, radialDistance);

				case EMFComponent.LongitudinalElectricField:
					return CalculateLongitudinalElectricField(effectiveTime, radialDistance);

				case EMFComponent.RadialElectricField:
					return CalculateRadialElectricField(effectiveTime, radialDistance);

				default:
					throw new Exception("Invalid EMFComponent.");
			}
		}

		public double CalculateAzimuthalMagneticField(
			double effectiveTime,
			double radialDistance
			)
		{
			// up to multiplication with velocity identical with radial electric field component
			return Math.Tanh(PointChargeRapidity) * CalculateRadialElectricField(
				effectiveTime, radialDistance);
		}

		public abstract double CalculateLongitudinalElectricField(
			double effectiveTime,
			double radialDistance
			);

		public abstract double CalculateRadialElectricField(
			double effectiveTime,
			double radialDistance
			);

		public virtual double CalculateAzimuthalMagneticField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			return (Math.Cosh(observerRapidity) * Math.Tanh(PointChargeRapidity) - Math.Sinh(observerRapidity))
				* CalculateRadialElectricField(effectiveTime, radialDistance);
		}

		public virtual double CalculateLongitudinalElectricField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			return CalculateLongitudinalElectricField(effectiveTime, radialDistance);
		}

		public virtual double CalculateRadialElectricField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			return (Math.Cosh(observerRapidity) - Math.Sinh(observerRapidity) * Math.Tanh(PointChargeRapidity))
				* CalculateRadialElectricField(effectiveTime, radialDistance);
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

			public override double CalculateLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					Func<double, double> integrand = k =>
					PointChargeLongitudinalElectricFieldIntegrand(k, effectiveTime, radialDistance);

					double integral = ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return Coupling * Math.Tanh(PointChargeRapidity) * integral;
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

					double integral = ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return 2 * Coupling / QGPConductivityPerFm * integral;
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

			public override double CalculateLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

					return Coupling * Math.Tanh(PointChargeRapidity) * exponentialPart
						* (0.25 * radialDistance * radialDistance * QGPConductivityPerFm - effectiveTime)
						/ (Math.Cosh(PointChargeRapidity) * Math.Cosh(PointChargeRapidity)
							* effectiveTime * effectiveTime * effectiveTime);
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

					return Coupling * exponentialPart
						* (radialDistance * QGPConductivityPerFm) / (2 * effectiveTime * effectiveTime);
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
				return Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivityPerFm / effectiveTime);
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

			public override double CalculateLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					double denominator = CalculateDenominator(effectiveTime, radialDistance);

					return -Coupling * Math.Sinh(PointChargeRapidity) * effectiveTime / denominator;
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
					double denominator = CalculateDenominator(effectiveTime, radialDistance);

					return Coupling * Math.Cosh(PointChargeRapidity) * radialDistance / denominator;
				}
				else
				{
					return 0;
				}
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double CalculateDenominator(
				double effectiveTime,
				double radialDistance
				)
			{
				return Math.Pow(radialDistance * radialDistance
					+ Math.Sinh(PointChargeRapidity) * Math.Sinh(PointChargeRapidity)
					* effectiveTime * effectiveTime, 1.5);
			}
		}
	}
}
