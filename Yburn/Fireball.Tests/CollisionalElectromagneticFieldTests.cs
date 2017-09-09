using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.PhysUtil;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class CollisionalElectromagneticFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateCollisionalElectricField()
		{
			SpatialVector[] fieldValues = CalculateElectricFieldValues();
			AssertCorrectElectricFieldValues(fieldValues);
		}

		[TestMethod]
		public void CalculateCollisionalMagneticField()
		{
			SpatialVector[] fieldValues = CalculateMagneticFieldValues();
			AssertCorrectMagneticFieldValues(fieldValues);
		}

		[TestMethod]
		public void CalculateAverageElectricFieldStrength()
		{
			FireballParam param = CreateFireballParamForAverageFieldStrengths();
			CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

			double result = emf.CalculateAverageElectricFieldStrength(0.4, QGPConductivity);

			AssertHelper.AssertApproximatelyEqual(0.12384, result, 5);
		}

		[TestMethod]
		public void CalculateAverageMagneticFieldStrength()
		{
			FireballParam param = CreateFireballParamForAverageFieldStrengths();
			CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

			double result = emf.CalculateAverageMagneticFieldStrength(0.4, QGPConductivity);

			AssertHelper.AssertApproximatelyEqual(0.16239, result, 5);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double Time = 0.4;

		private double QGPConductivity = 5.8;

		private static readonly SpatialVector[] Positions = new SpatialVector[]
		{
			new SpatialVector(0.0, 0.0, 0.0),
			new SpatialVector(1.0, 0.0, 0.0),
			new SpatialVector(1.0, 2.0, 0.0),
			new SpatialVector(1.0, 2.0, 3.0)
		};

		private static FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam
			{
				CenterOfMassEnergy_TeV = 2.76,
				DiffusenessA_fm = 0.546,
				DiffusenessB_fm = 0.546,
				EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation,
				EMFQuadratureOrder = 64,
				ImpactParameter_fm = 7,
				NuclearRadiusA_fm = 6.62,
				NuclearRadiusB_fm = 6.62,
				NucleonNumberA = 208,
				NucleonNumberB = 208,
				NucleusShapeA = NucleusShape.WoodsSaxonPotential,
				NucleusShapeB = NucleusShape.WoodsSaxonPotential,
				ProtonNumberA = 82,
				ProtonNumberB = 82,
				QGPConductivity_MeV = 5.8
			};

			return param;
		}

		private static FireballParam CreateFireballParamForAverageFieldStrengths()
		{
			FireballParam param = CreateFireballParam();

			param.EMFQuadratureOrder = 8;
			param.GridCellSize_fm = 1;
			param.GridRadius_fm = 10;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private SpatialVector[] CalculateElectricFieldValues()
		{
			CollisionalElectromagneticField emf
				= new CollisionalElectromagneticField(CreateFireballParam());

			SpatialVector[] fieldValues = new SpatialVector[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateElectricField(
					Time, Positions[i].X, Positions[i].Y, Positions[i].Z, QGPConductivity);
			}

			return fieldValues;
		}

		private void AssertCorrectElectricFieldValues(SpatialVector[] fieldValues)
		{
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].X, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].Z, 5);

			AssertHelper.AssertApproximatelyEqual(0.10682, fieldValues[1].X, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[1].Y, 5);
			AssertHelper.AssertApproximatelyEqual(-1.7582E-05, fieldValues[1].Z, 5);

			AssertHelper.AssertApproximatelyEqual(0.10155, fieldValues[2].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.29162, fieldValues[2].Y, 5);
			AssertHelper.AssertApproximatelyEqual(-7.9711E-06, fieldValues[2].Z, 5);

			AssertHelper.AssertApproximatelyEqual(-0.0056360, fieldValues[3].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.0045088, fieldValues[3].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[3].Z, 5);
		}

		private SpatialVector[] CalculateMagneticFieldValues()
		{
			CollisionalElectromagneticField emf
				= new CollisionalElectromagneticField(CreateFireballParam());

			SpatialVector[] fieldValues = new SpatialVector[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateMagneticField(
					Time, Positions[i].X, Positions[i].Y, Positions[i].Z, QGPConductivity);
			}

			return fieldValues;
		}

		private void AssertCorrectMagneticFieldValues(SpatialVector[] fieldValues)
		{
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.54102, fieldValues[0].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].Z, 5);

			AssertHelper.AssertApproximatelyEqual(0, fieldValues[1].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.52301, fieldValues[1].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[1].Z, 5);

			AssertHelper.AssertApproximatelyEqual(0.025290, fieldValues[2].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.49767, fieldValues[2].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[2].Z, 5);

			AssertHelper.AssertApproximatelyEqual(0.0045087, fieldValues[3].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.0056358, fieldValues[3].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[3].Z, 5);
		}
	}
}
