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
			double nucleusRapidity,
			Nucleus nucleus,
			int quadratureOrder
			)
		{
			PointChargeEMF = PointChargeElectromagneticField.Create(
				emfCalculationMethod, nucleusRapidity);

			Nucleus = nucleus;
			NucleusVelocity = Math.Tanh(nucleusRapidity);
			QuadratureOrder = quadratureOrder;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double CalculateElectromagneticField(
			double effectiveTime_fm,
			double radialDistance_fm,
			EMFComponent component
			)
		{
			CalculateElectromagneticField(
				effectiveTime_fm, radialDistance_fm,
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

		public void CalculateElectromagneticField(
			double effectiveTime_fm,
			double radialDistance_fm,
			out double azimuthalMagneticComponent_per_fm2,
			out double longitudinalElectricComponent_per_fm2,
			out double radialElectricComponent_per_fm2
			)
		{
			throw new NotImplementedException();
		}

		public double CalculateAzimuthalMagneticComponent(
			double effectiveTime_fm,
			double radialDistance_fm,
			double conductivity_MeV
			)
		{
			Func<double, double, double> integrand = (x_fm, y_fm) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance_fm - x_fm, -y_fm, 0);

				return Nucleus.GetProtonNumberColumnDensity_per_fm3(x_fm, y_fm)
					* PointChargeEMF.CalculateElectromagneticField(
						EMFComponent.AzimuthalMagneticComponent,
						effectiveTime_fm,
						pointChargePosition.Norm,
						conductivity_MeV)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadius_fm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateLongitudinalElectricComponent(
			double effectiveTime_fm,
			double radialDistance_fm,
			double conductivity_MeV
			)
		{
			Func<double, double, double> integrand = (x_fm, y_fm) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance_fm - x_fm, -y_fm, 0);

				return Nucleus.GetProtonNumberColumnDensity_per_fm3(x_fm, y_fm)
					* PointChargeEMF.CalculateElectromagneticField(
						EMFComponent.LongitudinalElectricComponent,
						effectiveTime_fm,
						pointChargePosition.Norm,
						conductivity_MeV)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadius_fm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateRadialElectricComponent(
			double effectiveTime_fm,
			double radialDistance_fm,
			double conductivity_MeV
			)
		{
			Func<double, double, double> integrand = (x_fm, y_fm) =>
			{
				SpatialVector pointChargePosition = new SpatialVector(radialDistance_fm - x_fm, -y_fm, 0);

				return Nucleus.GetProtonNumberColumnDensity_per_fm3(x_fm, y_fm)
					* PointChargeEMF.CalculateElectromagneticField(
						EMFComponent.RadialElectricComponent,
						effectiveTime_fm,
						pointChargePosition.Norm,
						conductivity_MeV)
					* pointChargePosition.Direction.X;
			};

			double integral = ImproperQuadrature.IntegrateOverRealPlane(
				integrand,
				2 * Nucleus.NuclearRadius_fm,
				QuadratureOrder);

			return integral;
		}

		public double CalculateAzimuthalMagneticComponentInLCF(
			double effectiveTime_fm,
			double radialDistance_fm,
			double observerRapidity,
			double conductivity_MeV
			)
		{
			return (Math.Cosh(observerRapidity) * NucleusVelocity - Math.Sinh(observerRapidity))
				* CalculateRadialElectricComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);
		}

		public double CalculateLongitudinalElectricComponentInLCF(
			double effectiveTime_fm,
			double radialDistance_fm,
			double observerRapidity,
			double conductivity_MeV
			)
		{
			return CalculateLongitudinalElectricComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);
		}

		public double CalculateRadialElectricComponentInLCF(
			double effectiveTime_fm,
			double radialDistance_fm,
			double observerRapidity,
			double conductivity_MeV
			)
		{
			return (Math.Cosh(observerRapidity) - Math.Sinh(observerRapidity) * NucleusVelocity)
				* CalculateRadialElectricComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);
		}

		public SpatialVector CalculateElectricField(
			double t_fm,
			double x_fm,
			double y_fm,
			double z_fm,
			double conductivity_MeV
			)
		{
			double effectiveTime_fm = CalculateEffectiveTime(t_fm, z_fm);
			double radialDistance_fm = CalculateRadialDistance(x_fm, y_fm);

			double longitudinalField
				= CalculateLongitudinalElectricComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);

			double radialField
				= CalculateRadialElectricComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x_fm, y_fm, radialField, 0, longitudinalField);
		}

		public SpatialVector CalculateElectricFieldInLCF(
			double properTime_fm,
			double x_fm,
			double y_fm,
			double rapidity,
			double conductivity_MeV
			)
		{
			double effectiveTime_fm = CalculateEffectiveTimeFromLCFCoordinates(properTime_fm, rapidity);
			double radialDistance_fm = CalculateRadialDistance(x_fm, y_fm);

			double longitudinalField
				= CalculateLongitudinalElectricComponentInLCF(effectiveTime_fm, radialDistance_fm, rapidity, conductivity_MeV);

			double radialField
				= CalculateRadialElectricComponentInLCF(effectiveTime_fm, radialDistance_fm, rapidity, conductivity_MeV);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x_fm, y_fm, radialField, 0, longitudinalField);
		}

		public SpatialVector CalculateMagneticField(
			double t_fm,
			double x_fm,
			double y_fm,
			double z_fm,
			double conductivity_MeV
			)
		{
			double effectiveTime_fm = CalculateEffectiveTime(t_fm, z_fm);
			double radialDistance_fm = CalculateRadialDistance(x_fm, y_fm);

			double azimuthalField
				= CalculateAzimuthalMagneticComponent(effectiveTime_fm, radialDistance_fm, conductivity_MeV);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x_fm, y_fm, 0, azimuthalField, 0);
		}

		public SpatialVector CalculateMagneticFieldInLCF(
			double properTime_fm,
			double x_fm,
			double y_fm,
			double rapidity,
			double conductivity_MeV
			)
		{
			double effectiveTime_fm = CalculateEffectiveTimeFromLCFCoordinates(properTime_fm, rapidity);
			double radialDistance_fm = CalculateRadialDistance(x_fm, y_fm);

			double azimuthalField
				= CalculateAzimuthalMagneticComponentInLCF(effectiveTime_fm, radialDistance_fm, rapidity, conductivity_MeV);

			return SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				x_fm, y_fm, 0, azimuthalField, 0);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private PointChargeElectromagneticField PointChargeEMF;

		private Nucleus Nucleus;

		private double NucleusVelocity;

		private int QuadratureOrder;

		private double CalculateEffectiveTime(
			double t_fm,
			double z_fm
			)
		{
			return t_fm - z_fm / NucleusVelocity;
		}

		private double CalculateEffectiveTimeFromLCFCoordinates(
			double properTime_fm,
			double rapidity
			)
		{
			return properTime_fm * (Math.Cosh(rapidity) - Math.Sinh(rapidity) / NucleusVelocity);
		}

		private double CalculateRadialDistance(
			double x_fm,
			double y_fm
			)
		{
			return Math.Sqrt(x_fm * x_fm + y_fm * y_fm);
		}
	}
}
