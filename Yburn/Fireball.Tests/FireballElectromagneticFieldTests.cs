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
			NuclearDensityFunction density = (NuclearDensityFunction)privateEMF.GetField("ProtonDensityFunctionA");

			EuclideanVector3D[] fieldValues = new EuclideanVector3D[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateSingleNucleusMagneticFieldInCMS(
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
				fieldValues[i] = emf.CalculateMagneticFieldInCMS(Time, Positions[i]);
			}

			return fieldValues;
		}

		private void AssertCorrectSingleNucleusMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			uint roundedDigits = 14;

			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.088835376061350557, fieldValues[1].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(-0.16913152546087232, fieldValues[2].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.084565866489620917, fieldValues[2].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(-0.0097934633586487085, fieldValues[3].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.00489673165924602, fieldValues[3].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[3].Z, roundedDigits);
		}

		private void AssertCorrectMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			uint roundedDigits = 15;

			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.54123199912271935, fieldValues[0].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.52321932562404094, fieldValues[1].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(0.025301883103653389, fieldValues[2].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.49786733766273539, fieldValues[2].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z, roundedDigits);

			AssertHelper.AssertRoundedEqual(-0.0058636488341155353, fieldValues[3].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.028979123920509407, fieldValues[3].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[3].Z, roundedDigits);
		}
	}
}