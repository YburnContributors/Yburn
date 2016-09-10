using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.PhysUtil;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballElectromagneticFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateNucleusMagneticField()
		{
			SpatialVector[] fieldValues = CalculateNucleusMagneticFieldValues();
			AssertCorrectNucleusMagneticFieldValues(fieldValues);
		}

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

		private static readonly SpatialVector[] Positions =
			new SpatialVector[] {
				new SpatialVector(0.0, 0.0, 0.0),
				new SpatialVector(1.0, 0.0, 0.0),
				new SpatialVector(1.0, 2.0, 0.0),
				new SpatialVector(1.0, 2.0, 3.0) };

		private static FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.QGPConductivityMeV = 5.8;

			param.BeamRapidity = 7.99;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.ImpactParameterFm = 7.0;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private SpatialVector[] CalculateNucleusMagneticFieldValues()
		{
			FireballParam param = CreateFireballParam();

			NuclearDensityFunction densityA;
			NuclearDensityFunction densityB;
			NuclearDensityFunction.CreateProtonDensityFunctionPair(param, out densityA, out densityB);

			NucleusElectromagneticField emf = new NucleusElectromagneticField(
				param.EMFCalculationMethod,
				param.QGPConductivityMeV,
				param.BeamRapidity,
				densityA);

			SpatialVector[] fieldValues = new SpatialVector[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateMagneticField(
						Time, Positions[i].X, Positions[i].Y, Positions[i].Z, 64);
			}

			return fieldValues;
		}

		private SpatialVector[] CalculateMagneticFieldValues()
		{
			FireballElectromagneticField emf =
				new FireballElectromagneticField(CreateFireballParam());

			SpatialVector[] fieldValues = new SpatialVector[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateMagneticField(
					Time, Positions[i].X, Positions[i].Y, Positions[i].Z, 64);
			}

			return fieldValues;
		}

		private void AssertCorrectNucleusMagneticFieldValues(SpatialVector[] fieldValues)
		{
			int roundedDigits = 14;

			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.088800919210167348, fieldValues[1].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(-0.16906670099454876, fieldValues[2].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.084533350497274382, fieldValues[2].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0, fieldValues[3].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[3].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[3].Z, roundedDigits);
		}

		private void AssertCorrectMagneticFieldValues(SpatialVector[] fieldValues)
		{
			int roundedDigits = 15;

			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.541029435898956, fieldValues[0].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.52302485253884012, fieldValues[1].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0.025290862764515948, fieldValues[2].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.49768392755697766, fieldValues[2].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0.0045089336487558534, fieldValues[3].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.005636167060944817, fieldValues[3].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[3].Z, roundedDigits);
		}
	}
}