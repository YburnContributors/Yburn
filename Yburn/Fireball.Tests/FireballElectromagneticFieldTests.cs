using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Threading;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballElectromagneticFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

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

		private static readonly double EffectiveTime = 0.4;

		private static readonly double LorentzFactor = 100.0;

		private static readonly double NucleiVelocity = 0.9;

		private static readonly EuclideanVector3D[] Positions =
			new EuclideanVector3D[] {
				new EuclideanVector3D(0.0, 0.0, 0.0),
				new EuclideanVector3D(1.0, 0.0, 0.0),
				new EuclideanVector3D(1.0, 2.0, 0.0),
				new EuclideanVector3D(1.0, 2.0, 3.0) };

		private static readonly EuclideanVector2D[] PositionsInReactionPlane =
			new EuclideanVector2D[] {
				new EuclideanVector2D(0.0, 0.0),
				new EuclideanVector2D(1.0, 0.0),
				new EuclideanVector2D(1.0, 2.0) };

		private static readonly double Time = 4.0;

		private static FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.QGPConductivityMeV = 5.8;

			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.ImpactParamFm = 2.0;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EuclideanVector3D[] CalculateSingleNucleusMagneticFieldValues()
		{
			FireballElectromagneticField emf =
				new FireballElectromagneticField(CreateFireballParam());
			PrivateObject privateEMF = new PrivateObject(emf);
			DensityFunction density = (DensityFunction)privateEMF.GetField("ChargeNumberDensityA");

			EuclideanVector3D[] fieldValues = new EuclideanVector3D[PositionsInReactionPlane.Length];
			for(int i = 0; i < PositionsInReactionPlane.Length; i++)
			{
				fieldValues[i] = emf.CalculateSingleNucleusMagneticField(
						EffectiveTime,
						PositionsInReactionPlane[i],
						LorentzFactor,
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
				fieldValues[i] = emf.CalculateMagneticField(
						Time,
						Positions[i],
						NucleiVelocity);
			}

			return fieldValues;
		}

		private void AssertCorrectSingleNucleusMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			Assert.AreEqual(0, fieldValues[0].X);
			Assert.AreEqual(0, fieldValues[0].Y);
			Assert.AreEqual(0, fieldValues[0].Z);

			Assert.AreEqual(0, fieldValues[1].X);
			Assert.AreEqual(0.08880091919, fieldValues[1].Y, 1e-11);
			Assert.AreEqual(0, fieldValues[1].Z);

			Assert.AreEqual(-0.16906670076, fieldValues[2].X, 1e-11);
			Assert.AreEqual(0.08453335038, fieldValues[2].Y, 1e-11);
			Assert.AreEqual(0, fieldValues[2].Z);
		}

		private void AssertCorrectMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			Assert.AreEqual(0, fieldValues[0].X, 1e-18);
			Assert.AreEqual(0, fieldValues[0].Y, 1e-18);
			Assert.AreEqual(0, fieldValues[0].Z);

			Assert.AreEqual(0, fieldValues[1].X, 1e-18);
			Assert.AreEqual(0.00334717796, fieldValues[1].Y, 1e-11);
			Assert.AreEqual(0, fieldValues[1].Z);

			Assert.AreEqual(-0.00667074897, fieldValues[2].X, 1e-11);
			Assert.AreEqual(0.00332357101, fieldValues[2].Y, 1e-11);
			Assert.AreEqual(0, fieldValues[2].Z);

			Assert.AreEqual(-0.08019360443, fieldValues[3].X, 1e-11);
			Assert.AreEqual(0.07916052193, fieldValues[3].Y, 1e-11);
			Assert.AreEqual(0, fieldValues[3].Z);
		}
	}
}