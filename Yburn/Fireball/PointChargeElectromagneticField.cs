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
		AzimuthalMagneticComponent,
		LongitudinalElectricComponent,
		RadialElectricComponent
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

			PointChargeVelocity = Math.Tanh(pointChargeRapidity);
			PointChargeLorentzFactor = Math.Cosh(pointChargeRapidity);
			PointChargeLorentzFactorTimesVelocity = Math.Sinh(pointChargeRapidity);
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
			double effectiveTime,
			double radialDistance,
			EMFComponent component
			)
		{
			CalculateElectromagneticField(
				effectiveTime, radialDistance,
				out double azimuthalMagneticComponent,
				out double longitudinalElectricComponent,
				out double radialElectricComponent);

			switch(component)
			{
				case EMFComponent.AzimuthalMagneticComponent:
					return azimuthalMagneticComponent;

				case EMFComponent.LongitudinalElectricComponent:
					return longitudinalElectricComponent;

				case EMFComponent.RadialElectricComponent:
					return radialElectricComponent;

				default:
					throw new Exception("Invalid EMFComponent.");
			}
		}

		public abstract void CalculateElectromagneticField(
			double effectiveTime,
			double radialDistance,
			out double azimuthalMagneticComponent,
			out double longitudinalElectricComponent,
			out double radialElectricComponent
			);

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EMFCalculationMethod EMFCalculationMethod;

		private double QGPConductivityPerFm;

		private double PointChargeVelocity;

		private double PointChargeLorentzFactor;

		private double PointChargeLorentzFactorTimesVelocity;

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


			public override void CalculateElectromagneticField(
				double effectiveTime,
				double radialDistance,
				out double azimuthalMagneticComponent,
				out double longitudinalElectricComponent,
				out double radialElectricComponent
				)
			{
				longitudinalElectricComponent
					= CalculateLongitudinalElectricComponent(effectiveTime, radialDistance);

				radialElectricComponent
					= CalculateRadialElectricComponent(effectiveTime, radialDistance);

				azimuthalMagneticComponent = PointChargeVelocity * radialElectricComponent;
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double CalculateLongitudinalElectricComponent(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					Func<double, double> integrand = k =>
					PointChargeLongitudinalElectricComponentIntegrand(k, effectiveTime, radialDistance);

					double integral = ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return Coupling * PointChargeVelocity * integral;
				}
				else
				{
					return 0;
				}
			}

			private double PointChargeLongitudinalElectricComponentIntegrand(
				double fourierFrequency,
				double effectiveTime,
				double radialDistance
				)
			{
				CalculateShorthands(
					fourierFrequency, effectiveTime, out double x, out double exponentialPart);

				return AdvancedMath.BesselJ(0, fourierFrequency * radialDistance)
						* fourierFrequency * (1 - x) / x * exponentialPart;
			}

			private double CalculateRadialElectricComponent(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime > 0)
				{
					Func<double, double> integrand = k =>
					 PointChargeRadialElectricComponentIntegrand(k, effectiveTime, radialDistance);

					double integral = ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return 2 * Coupling / QGPConductivityPerFm * integral;
				}
				else
				{
					return 0;
				}
			}

			private double PointChargeRadialElectricComponentIntegrand(
				double fourierFrequency,
				double effectiveTime,
				double radialDistance
				)
			{
				CalculateShorthands(
					fourierFrequency, effectiveTime, out double x, out double exponentialPart);

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
				x = Math.Sqrt(1 + 4 * Math.Pow(
					fourierFrequency / (PointChargeLorentzFactor * QGPConductivityPerFm), 2));

				exponentialPart = Math.Exp(0.5 * QGPConductivityPerFm
					* PointChargeLorentzFactor * PointChargeLorentzFactor * effectiveTime * (1 - x));
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

			public override void CalculateElectromagneticField(
				double effectiveTime,
				double radialDistance,
				out double azimuthalMagneticComponent,
				out double longitudinalElectricComponent,
				out double radialElectricComponent
				)
			{
				if(effectiveTime > 0)
				{
					double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

					longitudinalElectricComponent = Coupling * PointChargeVelocity * exponentialPart
						* (0.25 * radialDistance * radialDistance * QGPConductivityPerFm - effectiveTime)
						/ (PointChargeLorentzFactor * PointChargeLorentzFactor
							* effectiveTime * effectiveTime * effectiveTime);

					radialElectricComponent = Coupling * exponentialPart
						* (radialDistance * QGPConductivityPerFm) / (2 * effectiveTime * effectiveTime);

					azimuthalMagneticComponent = PointChargeVelocity * radialElectricComponent;
				}
				else
				{
					azimuthalMagneticComponent = 0;
					longitudinalElectricComponent = 0;
					radialElectricComponent = 0;
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

			public override void CalculateElectromagneticField(
				double effectiveTime,
				double radialDistance,
				out double azimuthalMagneticComponent,
				out double longitudinalElectricComponent,
				out double radialElectricComponent
				)
			{
				if(effectiveTime > 0)
				{
					double denominator = CalculateDenominator(effectiveTime, radialDistance);

					longitudinalElectricComponent = -Coupling * PointChargeLorentzFactorTimesVelocity * effectiveTime / denominator;

					radialElectricComponent = Coupling * PointChargeLorentzFactor * radialDistance / denominator;

					azimuthalMagneticComponent = PointChargeVelocity * radialElectricComponent;
				}
				else
				{
					azimuthalMagneticComponent = 0;
					longitudinalElectricComponent = 0;
					radialElectricComponent = 0;
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
					+ PointChargeLorentzFactorTimesVelocity * PointChargeLorentzFactorTimesVelocity
					* effectiveTime * effectiveTime, 1.5);
			}
		}
	}
}
