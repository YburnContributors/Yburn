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

		public double CalculateAzimutalMagneticField(
			double effectiveTime,
			double radialDistance
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return Nucleus.GetProtonNumberColumnDensityPerFm3(x, y)
					* PointChargeEMF.CalculateAzimutalMagneticField(
						effectiveTime,
						pointChargePosition.Norm)
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
					* PointChargeEMF.CalculateLongitudinalElectricField(
						effectiveTime,
						pointChargePosition.Norm)
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
					* PointChargeEMF.CalculateRadialElectricField(
						effectiveTime,
						pointChargePosition.Norm)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadiusFm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateAzimutalMagneticField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return Nucleus.GetProtonNumberColumnDensityPerFm3(x, y)
					* PointChargeEMF.CalculateAzimutalMagneticField_LCF(
						effectiveTime,
						pointChargePosition.Norm,
						observerRapidity)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadiusFm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateLongitudinalElectricField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return Nucleus.GetProtonNumberColumnDensityPerFm3(x, y)
					* PointChargeEMF.CalculateLongitudinalElectricField_LCF(
						effectiveTime,
						pointChargePosition.Norm,
						observerRapidity)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadiusFm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateRadialElectricField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return Nucleus.GetProtonNumberColumnDensityPerFm3(x, y)
					* PointChargeEMF.CalculateRadialElectricField_LCF(
						effectiveTime,
						pointChargePosition.Norm,
						observerRapidity)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadiusFm,
				QuadratureOrder);

			return integral;
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

			double azimutalField
				= CalculateAzimutalMagneticField(effectiveTime, radialDistance);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, 0, azimutalField, 0);
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

			double azimutalField
				= CalculateAzimutalMagneticField_LCF(effectiveTime, radialDistance, rapidity);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, 0, azimutalField, 0);
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