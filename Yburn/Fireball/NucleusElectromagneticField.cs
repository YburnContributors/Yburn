using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class NucleusElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public NucleusElectromagneticField(
			EMFCalculationMethod emfCalculationMethod,
			double qgpConductivityMeV,
			double nucleusRapidity,
			Nucleus nucleus,
			int quadratureOrder
			)
		{
			PointChargeEMF = PointChargeElectromagneticField.Create(
				emfCalculationMethod, qgpConductivityMeV, nucleusRapidity);

			Nucleus = nucleus;
			NucleusVelocity = Math.Tanh(nucleusRapidity);
			QuadratureOrder = quadratureOrder;
		}

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

		public void CalculateElectromagneticField(
			double effectiveTime,
			double radialDistance,
			out double azimuthalMagneticComponent,
			out double longitudinalElectricComponent,
			out double radialElectricComponent
			)
		{
			throw new NotImplementedException();
		}

		public double CalculateAzimuthalMagneticField(
			double effectiveTime,
			double radialDistance
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return Nucleus.GetProtonNumberColumnDensityPerFm3(x, y)
					* PointChargeEMF.CalculateElectromagneticField(
						effectiveTime,
						pointChargePosition.Norm,
						EMFComponent.AzimuthalMagneticComponent)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadiusFm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateLongitudinalElectricField(
			double effectiveTime,
			double radialDistance
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return Nucleus.GetProtonNumberColumnDensityPerFm3(x, y)
					* PointChargeEMF.CalculateElectromagneticField(
						effectiveTime,
						pointChargePosition.Norm,
						EMFComponent.LongitudinalElectricComponent)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadiusFm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateRadialElectricField(
			double effectiveTime,
			double radialDistance
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return Nucleus.GetProtonNumberColumnDensityPerFm3(x, y)
					* PointChargeEMF.CalculateElectromagneticField(
						effectiveTime,
						pointChargePosition.Norm,
						EMFComponent.RadialElectricComponent)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadiusFm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateAzimuthalMagneticField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			return (Math.Cosh(observerRapidity) * NucleusVelocity - Math.Sinh(observerRapidity))
				* CalculateRadialElectricField(effectiveTime, radialDistance);
		}

		public double CalculateLongitudinalElectricField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			return CalculateLongitudinalElectricField(effectiveTime, radialDistance);
		}

		public double CalculateRadialElectricField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			return (Math.Cosh(observerRapidity) - Math.Sinh(observerRapidity) * NucleusVelocity)
				* CalculateRadialElectricField(effectiveTime, radialDistance);
		}

		public SpatialVector CalculateElectricFieldPerFm2(
			double t,
			double x,
			double y,
			double z
			)
		{
			double effectiveTime = CalculateEffectiveTime(t, z);
			double radialDistance = CalculateRadialDistance(x, y);

			double longitudinalField
				= CalculateLongitudinalElectricField(effectiveTime, radialDistance);

			double radialField
				= CalculateRadialElectricField(effectiveTime, radialDistance);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, radialField, 0, longitudinalField);
		}

		public SpatialVector CalculateElectricFieldPerFm2_LCF(
			double properTime,
			double x,
			double y,
			double rapidity
			)
		{
			double effectiveTime = CalculateEffectiveTime_LCF(properTime, rapidity);
			double radialDistance = CalculateRadialDistance(x, y);

			double longitudinalField
				= CalculateLongitudinalElectricField_LCF(effectiveTime, radialDistance, rapidity);

			double radialField
				= CalculateRadialElectricField_LCF(effectiveTime, radialDistance, rapidity);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, radialField, 0, longitudinalField);
		}

		public SpatialVector CalculateMagneticFieldPerFm2(
			double t,
			double x,
			double y,
			double z
			)
		{
			double effectiveTime = CalculateEffectiveTime(t, z);
			double radialDistance = CalculateRadialDistance(x, y);

			double azimuthalField
				= CalculateAzimuthalMagneticField(effectiveTime, radialDistance);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, 0, azimuthalField, 0);
		}

		public SpatialVector CalculateMagneticFieldPerFm2_LCF(
			double properTime,
			double x,
			double y,
			double rapidity
			)
		{
			double effectiveTime = CalculateEffectiveTime_LCF(properTime, rapidity);
			double radialDistance = CalculateRadialDistance(x, y);

			double azimuthalField
				= CalculateAzimuthalMagneticField_LCF(effectiveTime, radialDistance, rapidity);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, 0, azimuthalField, 0);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private PointChargeElectromagneticField PointChargeEMF;

		private Nucleus Nucleus;

		private double NucleusVelocity;

		private int QuadratureOrder;

		private double CalculateEffectiveTime(
			double t,
			double z
			)
		{
			return t - z / NucleusVelocity;
		}

		private double CalculateEffectiveTime_LCF(
			double properTime,
			double rapidity
			)
		{
			return properTime * (Math.Cosh(rapidity) - Math.Sinh(rapidity) / NucleusVelocity);
		}

		private double CalculateRadialDistance(
			double x,
			double y
			)
		{
			return Math.Sqrt(x * x + y * y);
		}
	}
}
