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
			FireballParam param = new FireballParam();

			param.CenterOfMassEnergyTeV = 2.76;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.EMFQuadratureOrder = 64;
			param.ImpactParameterFm = 7;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.QGPConductivityMeV = 5.8;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private SpatialVector[] CalculateMagneticFieldValues()
		{
			FireballParam param = CreateFireballParam();

			Nucleus nucleusA;
			Nucleus nucleusB;
			Nucleus.CreateNucleusPair(param, out nucleusA, out nucleusB);

			NucleusElectromagneticField emf = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				param.BeamRapidity,
				nucleusA,
				param.EMFQuadratureOrder);

			SpatialVector[] fieldValues = new SpatialVector[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateMagneticFieldPerFm2(
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