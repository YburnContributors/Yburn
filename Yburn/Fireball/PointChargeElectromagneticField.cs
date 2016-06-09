using Meta.Numerics.Functions;
using System;

namespace Yburn.Fireball
{
	public abstract class PointChargeElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected PointChargeElectromagneticField(
			EMFCalculationMethod emfCalculationMethod,
			double qgpConductivityMeV,
			double pointChargeVelocity
			)
		{
			EMFCalculationMethod = emfCalculationMethod;
			QGPConductivityPerFm = qgpConductivityMeV / PhysConst.HBARC;
			PointChargeVelocity = pointChargeVelocity;
			PointChargeLorentzFactor = CalculatePointChargeLorentzFactor();
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static PointChargeElectromagneticField Create(
			EMFCalculationMethod emfCalculationMethod,
			double qgpConductivityMeV,
			double pointChargeVelocity
			)
		{
			switch(emfCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					return new PointChargeElectromagneticField_URLimitFourierSynthesis(
						emfCalculationMethod,
						qgpConductivityMeV,
						pointChargeVelocity);

				case EMFCalculationMethod.DiffusionApproximation:
					return new PointChargeElectromagneticField_DiffusionApproximation(
						emfCalculationMethod,
						qgpConductivityMeV,
						pointChargeVelocity);

				case EMFCalculationMethod.FreeSpace:
					return new PointChargeElectromagneticField_FreeSpace(
						emfCalculationMethod,
						qgpConductivityMeV,
						pointChargeVelocity);

				default:
					throw new Exception("Invalid Calculation Method.");
			}
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public EuclideanVector2D CalculatePointChargeMagneticField(
			double t,
			double x,
			double y,
			double z
			)
		{
			EuclideanVector2D azimutalPart = CalculatePointChargeAzimutalMagneticField(
					CalculateEffectiveTime(t, z), CalculateRadialDistance(x, y))
				* EuclideanVector2D.CreateAzimutalUnitVectorAtPosition(x, y);

			return azimutalPart;
		}

		public EuclideanVector3D CalculatePointChargeElectricField(
			double t,
			double x,
			double y,
			double z
			)
		{
			EuclideanVector2D radialPart = CalculatePointChargeRadialElectricField(
					CalculateEffectiveTime(t, z), CalculateRadialDistance(x, y))
				* EuclideanVector2D.CreateRadialUnitVectorAtPosition(x, y);

			double longitudinalPart = CalculatePointChargeLongitudinalElectricField(
				CalculateEffectiveTime(t, z), CalculateRadialDistance(x, y));

			return new EuclideanVector3D(radialPart, longitudinalPart);
		}

		public abstract double CalculatePointChargeAzimutalMagneticField(
			double effectiveTime,
			double radialDistance
			);

		public abstract double CalculatePointChargeLongitudinalElectricField(
			double effectiveTime,
			double radialDistance
			);

		public abstract double CalculatePointChargeRadialElectricField(
			double effectiveTime,
			double radialDistance
			);

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EMFCalculationMethod EMFCalculationMethod;

		private double QGPConductivityPerFm;

		private double PointChargeVelocity;

		private double PointChargeLorentzFactor;

		private double CalculatePointChargeLorentzFactor()
		{
			return 1 / Math.Sqrt(1 - PointChargeVelocity * PointChargeVelocity);
		}

		private double CalculateEffectiveTime(
			double t,
			double z
			)
		{
			return t - z / PointChargeVelocity;
		}

		private double CalculateRadialDistance(
			double x,
			double y)
		{
			return Math.Sqrt(x * x + y * y);
		}

		private class PointChargeElectromagneticField_URLimitFourierSynthesis
			: PointChargeElectromagneticField
		{
			/****************************************************************************************
			 * Constructors
			 ****************************************************************************************/

			public PointChargeElectromagneticField_URLimitFourierSynthesis(
				EMFCalculationMethod emfCalculationMethod,
				double qgpConductivityMeV,
				double pointChargeVelocity
				) : base(emfCalculationMethod, qgpConductivityMeV, pointChargeVelocity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override double CalculatePointChargeAzimutalMagneticField(
				double effectiveTime,
				double radialDistance
				)
			{
				// up to sign identical with radial electric field component in this limit
				return Math.Sign(PointChargeVelocity) * CalculatePointChargeRadialElectricField(
					effectiveTime, radialDistance);
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				OneVariableIntegrand integrand = k => PointChargeLongitudinalElectricFieldIntegrand(
					k, effectiveTime, radialDistance);

				double integral = Quadrature.UseGaussLegendreOverPositiveAxis(integrand, 1);

				return Math.Sign(PointChargeVelocity) * PhysConst.ElementaryCharge / (4 * Math.PI)
					* integral;
			}

			public override double CalculatePointChargeRadialElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				OneVariableIntegrand integrand = k => PointChargeRadialElectricFieldIntegrand(
					k, effectiveTime, radialDistance);

				double integral = Quadrature.UseGaussLegendreOverPositiveAxis(integrand, 1);

				return PhysConst.ElementaryCharge / (2 * Math.PI * QGPConductivityPerFm) * integral;
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
				double pointChargeVelocity
				) : base(emfCalculationMethod, qgpConductivityMeV, pointChargeVelocity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override double CalculatePointChargeAzimutalMagneticField(
				double effectiveTime,
				double radialDistance
				)
			{
				// up to sign identical with radial electric field component in this limit
				return Math.Sign(PointChargeVelocity) * CalculatePointChargeRadialElectricField(
					effectiveTime, radialDistance);
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime == 0)
				{
					return 0;
				}
				else
				{
					double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

					return Math.Sign(PointChargeVelocity) * PhysConst.ElementaryCharge
						* (0.25 * radialDistance * radialDistance * QGPConductivityPerFm
							- effectiveTime)
						/ (4 * Math.PI * PointChargeLorentzFactor * PointChargeLorentzFactor
							* effectiveTime * effectiveTime * effectiveTime)
						* exponentialPart;
				}
			}

			public override double CalculatePointChargeRadialElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				if(effectiveTime == 0)
				{
					return 0;
				}
				else
				{
					double exponentialPart = CalculateExponentialPart(effectiveTime, radialDistance);

					return PhysConst.ElementaryCharge * (radialDistance * QGPConductivityPerFm)
						/ (8 * Math.PI * effectiveTime * effectiveTime)
						* exponentialPart;
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
				double pointChargeVelocity
				) : base(emfCalculationMethod, qgpConductivityMeV, pointChargeVelocity)
			{
			}

			/****************************************************************************************
			 * Public members, functions and properties
			 ****************************************************************************************/

			public override double CalculatePointChargeAzimutalMagneticField(
				double effectiveTime,
				double radialDistance
				)
			{
				double denominator = CalculateDenominator(effectiveTime, radialDistance);

				return PhysConst.ElementaryCharge * PointChargeLorentzFactor * PointChargeVelocity
					* radialDistance / (4 * Math.PI * denominator);
			}

			public override double CalculatePointChargeLongitudinalElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				double denominator = CalculateDenominator(effectiveTime, radialDistance);

				return -PhysConst.ElementaryCharge * PointChargeLorentzFactor * PointChargeVelocity
					* effectiveTime / (4 * Math.PI * denominator);
			}

			public override double CalculatePointChargeRadialElectricField(
				double effectiveTime,
				double radialDistance
				)
			{
				double denominator = CalculateDenominator(effectiveTime, radialDistance);

				return PhysConst.ElementaryCharge * PointChargeLorentzFactor
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
					PointChargeLorentzFactor * PointChargeVelocity * effectiveTime, 2), 1.5);
			}
		}
	}
}