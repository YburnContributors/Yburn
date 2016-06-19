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
		public void CalculateSingleNucleusMagneticField()
		{
			EuclideanVector3D[] fieldValues = CalculateSingleNucleusMagneticFieldValues();
			AssertCorrectSingleNucleusMagneticFieldValues(fieldValues);
		}

		[TestMethod]
		public void CalculateMagneticField()
		{
			EuclideanVector3D[] fieldValues = CalculateMagneticFieldValues();
			AssertCorrectMagneticFieldValues(fieldValues);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double Time = 0.4;

		private static readonly EuclideanVector3D[] Positions =
			new EuclideanVector3D[] {
				new EuclideanVector3D(0.0, 0.0, 0.0),
				new EuclideanVector3D(1.0, 0.0, 0.0),
				new EuclideanVector3D(1.0, 2.0, 0.0),
				new EuclideanVector3D(1.0, 2.0, 3.0) };

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

		private EuclideanVector3D[] CalculateSingleNucleusMagneticFieldValues()
		{
			FireballParam param = CreateFireballParam();
			FireballElectromagneticField emf = new FireballElectromagneticField(param);
			PrivateObject privateEMF = new PrivateObject(emf);
			DensityFunction density = (DensityFunction)privateEMF.GetField("ProtonDensityFunctionA");

			EuclideanVector3D[] fieldValues = new EuclideanVector3D[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateSingleNucleusMagneticField(
						Time,
						Positions[i],
						param.ParticleVelocity,
						density);
			}

			return fieldValues;
		}

		private EuclideanVector3D[] CalculateMagneticFieldValues()
		{
			FireballElectromagneticField emf =
				new FireballElectromagneticField(CreateFireballParam());

			EuclideanVector3D[] fieldValues = new EuclideanVector3D[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateMagneticField(Time, Positions[i]);
			}

			return fieldValues;
		}

		private void AssertCorrectSingleNucleusMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Y);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X);
			AssertHelper.AssertRoundedEqual(0.088800919190011027, fieldValues[1].Y);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z);

			AssertHelper.AssertRoundedEqual(-0.16906670076021077, fieldValues[2].X);
			AssertHelper.AssertRoundedEqual(0.084533350380106786, fieldValues[2].Y);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z);
		}

		private void AssertCorrectMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			uint roundedDigits = 14;

			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.54102943400050552, fieldValues[0].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.52302485032356349, fieldValues[1].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0.025290863243184331, fieldValues[2].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.49768392486013513, fieldValues[2].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(-0.0058621636408420471, fieldValues[3].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.028971135436686207, fieldValues[3].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[3].Z, roundedDigits);
		}
	}
}