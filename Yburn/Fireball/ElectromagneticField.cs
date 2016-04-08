using System;
using Meta.Numerics.Functions;

namespace Yburn.Fireball
{
	public class ElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ElectromagneticField(
			EMFCalculationMethod emfCalculationMethod,
			double minSpatialFrequency,
			double maxSpatialFrequency,
			int spatialFrequencySteps
			)
		{
			EMFCalculationMethod = emfCalculationMethod;
			MinSpatialFrequency = minSpatialFrequency;
			MaxSpatialFrequency = maxSpatialFrequency;
			SpatialFrequencySteps = spatialFrequencySteps;
			SpatialFrequency = GetSpatialFrequencyValueArray();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// for a point charge (with charge e) moving along the beam axis
		public double GetPointChargeAzimutalMagneticField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			switch(EMFCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					return PointChargeAzimutalMagneticField(
						effectiveTime, radialDistance, lorentzFactor);

				case EMFCalculationMethod.DiffusionApproximation:
					return PointChargeAzimutalMagneticField_DiffusionApproximation(
						effectiveTime, radialDistance, lorentzFactor);

				case EMFCalculationMethod.FreeSpace:
					return PointChargeAzimutalMagneticField_FreeSpace(
						effectiveTime, radialDistance, lorentzFactor);

				default:
					throw new Exception("Invalid Calculation Method.");
			}
		}

		// for a point charge (with charge e) moving along the beam axis
		public double GetPointChargeLongitudinalElectricField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			switch(EMFCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					return PointChargeLongitudinalElectricField(
						effectiveTime, radialDistance, lorentzFactor);

				case EMFCalculationMethod.DiffusionApproximation:
					return PointChargeLongitudinalElectricField_DiffusionApproximation(
						effectiveTime, radialDistance, lorentzFactor);

				case EMFCalculationMethod.FreeSpace:
					return PointChargeLongitudinalElectricField_FreeSpace(
						effectiveTime, radialDistance, lorentzFactor);

				default:
					throw new Exception("Invalid Calculation Method.");
			}
		}

		// for a point charge (with charge e) moving along the beam axis
		public double GetPointChargeRadialElectricField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			switch(EMFCalculationMethod)
			{
				case EMFCalculationMethod.URLimitFourierSynthesis:
					// identical with azimutal magnetic field component in this limit
					return PointChargeAzimutalMagneticField(
						effectiveTime, radialDistance, lorentzFactor);

				case EMFCalculationMethod.DiffusionApproximation:
					// identical with azimutal magnetic field component in this limit
					return PointChargeAzimutalMagneticField_DiffusionApproximation(
						effectiveTime, radialDistance, lorentzFactor);

				case EMFCalculationMethod.FreeSpace:
					return PointChargeRadialElectricField_FreeSpace(
						effectiveTime, radialDistance, lorentzFactor);

				default:
					throw new Exception("Invalid Calculation Method.");
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		// in fm^-1
		private static readonly double QGPConductivity = 5.8 / PhysConst.HBARC;
		//private static readonly double QGPConductivity = 0.01 / PhysConst.HBARC;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EMFCalculationMethod EMFCalculationMethod;

		private double MinSpatialFrequency;

		private double MaxSpatialFrequency;

		private int SpatialFrequencySteps;

		private double[] SpatialFrequency;

		private double[] GetSpatialFrequencyValueArray()
		{
			double[] spatialFrequency = new double[SpatialFrequencySteps + 1];

			double dk = Math.Exp(Math.Log(MaxSpatialFrequency / MinSpatialFrequency) / SpatialFrequencySteps);
			spatialFrequency[0] = MinSpatialFrequency;

			for(int n = 0; n < SpatialFrequencySteps; n++)
			{
				spatialFrequency[n + 1] = spatialFrequency[n] * dk;
			}

			return spatialFrequency;
		}

		private double PointChargeAzimutalMagneticField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			double integral = 0;
			for(int j = 1; j < SpatialFrequencySteps; j++)
			{
				integral += PointChargeAzimutalMagneticFieldIntegrand(
						effectiveTime, radialDistance, lorentzFactor, j)
					* (SpatialFrequency[j + 1] - SpatialFrequency[j - 1]);
			}

			// first step vanishes because BesselJ(1, 0) = 0
			integral += PointChargeAzimutalMagneticFieldIntegrand(
					effectiveTime, radialDistance, lorentzFactor, SpatialFrequencySteps)
				* (SpatialFrequency[SpatialFrequencySteps] - SpatialFrequency[SpatialFrequencySteps - 1]);

			integral *= 0.5;

			return PhysConst.ElementaryCharge / (2 * PhysConst.PI * QGPConductivity) * integral;
		}

		private double PointChargeAzimutalMagneticFieldIntegrand(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor,
			int spatialFrequencyIndex
			)
		{
			double x = Math.Sqrt(1 + 4 * SpatialFrequency[spatialFrequencyIndex] * SpatialFrequency[spatialFrequencyIndex]
				/ (lorentzFactor * lorentzFactor * QGPConductivity * QGPConductivity));

			return AdvancedMath.BesselJ(1, SpatialFrequency[spatialFrequencyIndex] * radialDistance)
					* SpatialFrequency[spatialFrequencyIndex] * SpatialFrequency[spatialFrequencyIndex] / x
					* Math.Exp(0.5 * QGPConductivity * lorentzFactor * lorentzFactor * effectiveTime * (1 - x));
		}

		private double PointChargeAzimutalMagneticField_DiffusionApproximation(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			return 0.125 * PhysConst.ElementaryCharge / PhysConst.PI
				* (radialDistance * QGPConductivity) / (effectiveTime * effectiveTime)
				* Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivity / effectiveTime);
		}

		private double PointChargeAzimutalMagneticField_FreeSpace(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			double velocity = Math.Sqrt(1 - 1 / (lorentzFactor * lorentzFactor));
			double denominator = Math.Pow(
				radialDistance * radialDistance + lorentzFactor * lorentzFactor
					* velocity * velocity * effectiveTime * effectiveTime,
				1.5);

			return PhysConst.ElementaryCharge * lorentzFactor / (4 * PhysConst.PI)
				* velocity * radialDistance / denominator;
		}

		private double PointChargeLongitudinalElectricField(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			double integral = 0;
			for(int j = 1; j < SpatialFrequencySteps; j++)
			{
				integral += PointChargeLongitudinalElectricFieldIntegrand(
						effectiveTime, radialDistance, lorentzFactor, j)
					* (SpatialFrequency[j + 1] - SpatialFrequency[j - 1]);
			}

			// first and last steps
			integral += PointChargeLongitudinalElectricFieldIntegrand(
					effectiveTime, radialDistance, lorentzFactor, 0)
				* (SpatialFrequency[1] - SpatialFrequency[0]);

			integral += PointChargeLongitudinalElectricFieldIntegrand(
					effectiveTime, radialDistance, lorentzFactor, SpatialFrequencySteps)
				* (SpatialFrequency[SpatialFrequencySteps] - SpatialFrequency[SpatialFrequencySteps - 1]);

			integral *= 0.5;

			return PhysConst.ElementaryCharge / (4 * PhysConst.PI) * integral;
		}

		private double PointChargeLongitudinalElectricFieldIntegrand(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor,
			int spatialFrequencyIndex
			)
		{
			double x = Math.Sqrt(1 + 4 * SpatialFrequency[spatialFrequencyIndex] * SpatialFrequency[spatialFrequencyIndex]
				/ (lorentzFactor * lorentzFactor * QGPConductivity * QGPConductivity));

			return AdvancedMath.BesselJ(0, SpatialFrequency[spatialFrequencyIndex] * radialDistance)
					* SpatialFrequency[spatialFrequencyIndex] * (1 - x) / x
					* Math.Exp(0.5 * QGPConductivity * lorentzFactor * lorentzFactor * effectiveTime * (1 - x));
		}

		private double PointChargeLongitudinalElectricField_DiffusionApproximation(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			return -0.25 * PhysConst.ElementaryCharge / PhysConst.PI
				* (effectiveTime - 0.25 * radialDistance * radialDistance * QGPConductivity)
				/ (lorentzFactor * lorentzFactor * effectiveTime * effectiveTime * effectiveTime)
				* Math.Exp(-0.25 * radialDistance * radialDistance * QGPConductivity / effectiveTime);
		}

		private double PointChargeLongitudinalElectricField_FreeSpace(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			double velocity = Math.Sqrt(1 - 1 / (lorentzFactor * lorentzFactor));
			double denominator = Math.Pow(
				radialDistance * radialDistance + lorentzFactor * lorentzFactor
					* velocity * velocity * effectiveTime * effectiveTime,
				1.5);

			return PhysConst.ElementaryCharge * lorentzFactor / (4 * PhysConst.PI)
				* (-velocity * effectiveTime) / denominator;
		}

		private double PointChargeRadialElectricField_FreeSpace(
			double effectiveTime,
			double radialDistance,
			double lorentzFactor
		)
		{
			double velocity = Math.Sqrt(1 - 1 / (lorentzFactor * lorentzFactor));
			double denominator = Math.Pow(
				radialDistance * radialDistance + lorentzFactor * lorentzFactor
					* velocity * velocity * effectiveTime * effectiveTime,
				1.5);

			return PhysConst.ElementaryCharge * lorentzFactor / (4 * PhysConst.PI)
				* radialDistance / denominator;
		}
	}
}
