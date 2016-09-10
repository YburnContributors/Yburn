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
			NuclearDensityFunction protonDensityFunction
			)
		{
			PointChargeEMF = PointChargeElectromagneticField.Create(
				emfCalculationMethod, qgpConductivityMeV, nucleusRapidity);

			ProtonDensityFunction = protonDensityFunction;

			NucleusVelocity = Math.Tanh(nucleusRapidity);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double CalculateAzimutalMagneticField(
			double effectiveTime,
			double radialDistance,
			int quadratureOrder
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return ProtonDensityFunction.GetColumnDensity(x, y)
					* PointChargeEMF.CalculateAzimutalMagneticField(
						effectiveTime,
						pointChargePosition.Norm)
					* pointChargePosition.Direction.X;
			};

			double integral = Quadrature.IntegrateOverRealPlane(
				integrand,
				2 * ProtonDensityFunction.NuclearRadius,
				quadratureOrder);

			return integral;
		}

		public double CalculateLongitudinalElectricField(
			double effectiveTime,
			double radialDistance,
			int quadratureOrder
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return ProtonDensityFunction.GetColumnDensity(x, y)
					* PointChargeEMF.CalculateLongitudinalElectricField(
						effectiveTime,
						pointChargePosition.Norm)
					* pointChargePosition.Direction.X;
			};

			double integral = Quadrature.IntegrateOverRealPlane(
				integrand,
				2 * ProtonDensityFunction.NuclearRadius,
				quadratureOrder);

			return integral;
		}

		public double CalculateRadialElectricField(
			double effectiveTime,
			double radialDistance,
			int quadratureOrder
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return ProtonDensityFunction.GetColumnDensity(x, y)
					* PointChargeEMF.CalculateRadialElectricField(
						effectiveTime,
						pointChargePosition.Norm)
					* pointChargePosition.Direction.X;
			};

			double integral = Quadrature.IntegrateOverRealPlane(
				integrand,
				2 * ProtonDensityFunction.NuclearRadius,
				quadratureOrder);

			return integral;
		}

		public double CalculateAzimutalMagneticField_LCF(
			double effectiveTime,
			double radialDistance,
			double observerRapidity,
			int quadratureOrder
			)
		{
			Func<double, double, double> integrand = (x, y) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance - x, -y, 0);

				return ProtonDensityFunction.GetColumnDensity(x, y)
					* PointChargeEMF.CalculateAzimutalMagneticField_LCF(
						effectiveTime,
						pointChargePosition.Norm,
						observerRapidity)
					* pointChargePosition.Direction.X;
			};

			double integral = Quadrature.IntegrateOverRealPlane(
				integrand,
				2 * ProtonDensityFunction.NuclearRadius,
				quadratureOrder);

			return integral;
		}

		public SpatialVector CalculateMagneticField(
			double t,
			double x,
			double y,
			double z,
			int quadratureOrder
			)
		{
			double effectiveTime = CalculateEffectiveTime(t, z);
			double radialDistance = CalculateRadialDistance(x, y);

			double azimutalField = CalculateAzimutalMagneticField(
				effectiveTime, radialDistance, quadratureOrder);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, 0, azimutalField, 0);
		}

		public SpatialVector CalculateMagneticField_LCF(
			double properTime,
			double x,
			double y,
			double rapidity,
			int quadratureOrder
			)
		{
			double effectiveTime = CalculateEffectiveTime_LCF(properTime, rapidity);
			double radialDistance = CalculateRadialDistance(x, y);

			double azimutalField = CalculateAzimutalMagneticField_LCF(
				effectiveTime, radialDistance, rapidity, quadratureOrder);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x, y, 0, azimutalField, 0);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private PointChargeElectromagneticField PointChargeEMF;

		private NuclearDensityFunction ProtonDensityFunction;

		private double NucleusVelocity;

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