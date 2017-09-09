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
			double pointChargeRapidity
			)
		{
			EMFCalculationMethod = emfCalculationMethod;

			PointChargeVelocity = Math.Tanh(pointChargeRapidity);
			PointChargeLorentzFactor = Math.Cosh(pointChargeRapidity);
			PointChargeLorentzFactorTimesVelocity = Math.Sinh(pointChargeRapidity);
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static PointChargeElectromagneticField Create(
			EMFCalculationMethod emfCalculationMethod,
			double pointChargeRapidity
			)
		{
			switch(emfCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					return new PointChargeElectromagneticField_URLimitFourierSynthesis(
						emfCalculationMethod, pointChargeRapidity);

				case EMFCalculationMethod.DiffusionApproximation:
					return new PointChargeElectromagneticField_DiffusionApproximation(
						emfCalculationMethod, pointChargeRapidity);

				case EMFCalculationMethod.FreeSpace:
					return new PointChargeElectromagneticField_FreeSpace(
						emfCalculationMethod, pointChargeRapidity);

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
			double effectiveTime_fm,
			double radialDistance_fm,
			double conductivity_MeV
			)
		{
			CalculateElectromagneticField(
				effectiveTime_fm, radialDistance_fm, conductivity_MeV,
				out double azimuthalMagneticComponent_per_fm2,
				out double longitudinalElectricComponent_per_fm2,
				out double radialElectricComponent_per_fm2);

			switch(component)
			{
				case EMFComponent.AzimuthalMagneticComponent:
					return azimuthalMagneticComponent_per_fm2;

				case EMFComponent.LongitudinalElectricComponent:
					return longitudinalElectricComponent_per_fm2;

				case EMFComponent.RadialElectricComponent:
					return radialElectricComponent_per_fm2;

				default:
					throw new Exception("Invalid EMFComponent.");
			}
		}

		public abstract void CalculateElectromagneticField(
			double effectiveTime_fm,
			double radialDistance_fm,
			double conductivity_MeV,
			out double azimuthalMagneticComponent_per_fm2,
			out double longitudinalElectricComponent_per_fm2,
			out double radialElectricComponent_per_fm2
			);

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EMFCalculationMethod EMFCalculationMethod;

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
				double pointChargeRapidity
				) : base(emfCalculationMethod, pointChargeRapidity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/


			public override void CalculateElectromagneticField(
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV,
				out double azimuthalMagneticComponent_per_fm2,
				out double longitudinalElectricComponent_per_fm2,
				out double radialElectricComponent_per_fm2
				)
			{
				longitudinalElectricComponent_per_fm2
					= CalculateLongitudinalElectricComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);

				radialElectricComponent_per_fm2
					= CalculateRadialElectricComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);

				azimuthalMagneticComponent_per_fm2 = PointChargeVelocity * radialElectricComponent_per_fm2;
			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double CalculateLongitudinalElectricComponent(
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV
				)
			{
				if(effectiveTime_fm > 0)
				{
					Func<double, double> integrand = k => PointChargeLongitudinalElectricComponentIntegrand(
						k, effectiveTime_fm, radialDistance_fm, conductivity_MeV);

					double integral = ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return Coupling * PointChargeVelocity * integral;
				}
				else
				{
					return 0;
				}
			}

			private double PointChargeLongitudinalElectricComponentIntegrand(
				double fourierFrequency_per_fm,
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV
				)
			{
				CalculateShorthands(fourierFrequency_per_fm, effectiveTime_fm, conductivity_MeV,
					out double x, out double exponentialPart);

				return AdvancedMath.BesselJ(0, fourierFrequency_per_fm * radialDistance_fm)
						* fourierFrequency_per_fm * (1 - x) / x * exponentialPart;
			}

			private double CalculateRadialElectricComponent(
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV
				)
			{
				if(effectiveTime_fm > 0)
				{
					Func<double, double> integrand = k => PointChargeRadialElectricComponentIntegrand(
						k, effectiveTime_fm, radialDistance_fm, conductivity_MeV);

					double integral = ImproperQuadrature.IntegrateOverPositiveAxis(integrand, 1, 64);

					return 2 * Coupling / conductivity_MeV * Constants.HbarC_MeV_fm * integral;
				}
				else
				{
					return 0;
				}
			}

			private double PointChargeRadialElectricComponentIntegrand(
				double fourierFrequency_per_fm,
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV
				)
			{
				CalculateShorthands(fourierFrequency_per_fm, effectiveTime_fm, conductivity_MeV,
					out double x, out double exponentialPart);

				return AdvancedMath.BesselJ(1, fourierFrequency_per_fm * radialDistance_fm)
						* fourierFrequency_per_fm * fourierFrequency_per_fm / x * exponentialPart;
			}

			private void CalculateShorthands(
				double fourierFrequency_per_fm,
				double effectiveTime_fm,
				double conductivity_MeV,
				out double x,
				out double exponentialPart
				)
			{
				x = Math.Sqrt(1 + 4 * Math.Pow(
					Constants.HbarC_MeV_fm * fourierFrequency_per_fm / (PointChargeLorentzFactor * conductivity_MeV), 2));

				exponentialPart = Math.Exp(0.5 * conductivity_MeV / Constants.HbarC_MeV_fm
					* PointChargeLorentzFactor * PointChargeLorentzFactor * effectiveTime_fm * (1 - x));
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
				double pointChargeRapidity
				) : base(emfCalculationMethod, pointChargeRapidity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override void CalculateElectromagneticField(
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV,
				out double azimuthalMagneticComponent_per_fm2,
				out double longitudinalElectricComponent_per_fm2,
				out double radialElectricComponent_per_fm2
				)
			{
				if(effectiveTime_fm > 0)
				{
					double exponentialPart = CalculateExponentialPart(
						effectiveTime_fm, radialDistance_fm, conductivity_MeV);

					longitudinalElectricComponent_per_fm2 = Coupling * PointChargeVelocity * exponentialPart
						* (0.25 * radialDistance_fm * radialDistance_fm * conductivity_MeV / Constants.HbarC_MeV_fm - effectiveTime_fm)
						/ (PointChargeLorentzFactor * PointChargeLorentzFactor * effectiveTime_fm * effectiveTime_fm * effectiveTime_fm);

					radialElectricComponent_per_fm2 = Coupling * exponentialPart
						* (radialDistance_fm * conductivity_MeV / Constants.HbarC_MeV_fm)
						/ (2 * effectiveTime_fm * effectiveTime_fm);

					azimuthalMagneticComponent_per_fm2 = PointChargeVelocity * radialElectricComponent_per_fm2;
				}
				else
				{
					azimuthalMagneticComponent_per_fm2 = 0;
					longitudinalElectricComponent_per_fm2 = 0;
					radialElectricComponent_per_fm2 = 0;
				}

			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double CalculateExponentialPart(
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV
				)
			{
				return Math.Exp(-0.25 * conductivity_MeV / Constants.HbarC_MeV_fm
					* radialDistance_fm * radialDistance_fm / effectiveTime_fm);
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
				double pointChargeRapidity
				) : base(emfCalculationMethod, pointChargeRapidity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override void CalculateElectromagneticField(
				double effectiveTime_fm,
				double radialDistance_fm,
				double conductivity_MeV,
				out double azimuthalMagneticComponent_per_fm2,
				out double longitudinalElectricComponent_per_fm2,
				out double radialElectricComponent_per_fm2
				)
			{
				if(effectiveTime_fm > 0)
				{
					double denominator = CalculateDenominator(effectiveTime_fm, radialDistance_fm);

					longitudinalElectricComponent_per_fm2 = -Coupling * PointChargeLorentzFactorTimesVelocity * effectiveTime_fm / denominator;

					radialElectricComponent_per_fm2 = Coupling * PointChargeLorentzFactor * radialDistance_fm / denominator;

					azimuthalMagneticComponent_per_fm2 = PointChargeVelocity * radialElectricComponent_per_fm2;
				}
				else
				{
					azimuthalMagneticComponent_per_fm2 = 0;
					longitudinalElectricComponent_per_fm2 = 0;
					radialElectricComponent_per_fm2 = 0;
				}

			}

			/****************************************************************************************
			 * Private/protected members, functions and properties
			 ****************************************************************************************/

			private double CalculateDenominator(
				double effectiveTime_fm,
				double radialDistance_fm
				)
			{
				return Math.Pow(radialDistance_fm * radialDistance_fm
					+ PointChargeLorentzFactorTimesVelocity * PointChargeLorentzFactorTimesVelocity
					* effectiveTime_fm * effectiveTime_fm, 1.5);
			}
		}
	}
}
