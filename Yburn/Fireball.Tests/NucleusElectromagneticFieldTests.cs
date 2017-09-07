using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.PhysUtil;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class NucleusElectromagneticFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateMagneticField()
		{
			SpatialVector[] fieldValues = CalculateMagneticFieldValues();
			AssertCorrectMagneticFieldValues(fieldValues);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double Time = 0.4;

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

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private SpatialVector[] CalculateMagneticFieldValues()
		{
			FireballParam param = CreateFireballParam();

			Nucleus.CreateNucleusPair(param, out Nucleus nucleusA, out Nucleus nucleusB);

			NucleusElectromagneticField emf = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivity_MeV,
				param.BeamRapidity,
				nucleusA,
				param.EMFQuadratureOrder);

			SpatialVector[] fieldValues = new SpatialVector[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateMagneticField_per_fm2(
						Time, Positions[i].X, Positions[i].Y, Positions[i].Z);
			}

			return fieldValues;
		}

		private void AssertCorrectMagneticFieldValues(SpatialVector[] fieldValues)
		{
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].X, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[0].Z, 5);

			AssertHelper.AssertApproximatelyEqual(0, fieldValues[1].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.088801, fieldValues[1].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[1].Z, 5);

			AssertHelper.AssertApproximatelyEqual(-0.16907, fieldValues[2].X, 5);
			AssertHelper.AssertApproximatelyEqual(0.084533, fieldValues[2].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[2].Z, 5);

			AssertHelper.AssertApproximatelyEqual(0, fieldValues[3].X, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[3].Y, 5);
			AssertHelper.AssertApproximatelyEqual(0, fieldValues[3].Z, 5);
		}
	}
}
