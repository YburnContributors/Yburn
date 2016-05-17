using Microsoft.VisualStudio.TestTools.UnitTesting;

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

		private static readonly double EffectiveTime = 0.4;

		private static readonly double ImpactParameter = 2.0;

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
			param.FourierFrequencySteps = 1000;
			param.MaxFourierFrequency = 1e3;
			param.MinFourierFrequency = 1e-3;
			param.QGPConductivityMeV = 5.8;

			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EuclideanVector3D[] CalculateSingleNucleusMagneticFieldValues()
		{
			FireballElectromagneticField emf =
				new FireballElectromagneticField(CreateFireballParam());

			EuclideanVector3D[] fieldValues = new EuclideanVector3D[PositionsInReactionPlane.Length];
			for(int i = 0; i < PositionsInReactionPlane.Length; i++)
			{
				fieldValues[i] = emf.CalculateSingleNucleusMagneticField(
						EffectiveTime,
						PositionsInReactionPlane[i],
						LorentzFactor,
						emf.ProtonDistributionNucleonA);
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
						NucleiVelocity,
						ImpactParameter);
			}

			return fieldValues;
		}

		private void AssertCorrectSingleNucleusMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			Assert.AreEqual(-8.1598459753170567E-19, fieldValues[0].X);
			Assert.AreEqual(6.5541761151600369E-18, fieldValues[0].Y);
			Assert.AreEqual(0, fieldValues[0].Z);

			Assert.AreEqual(-1.523789143681731E-18, fieldValues[1].X);
			Assert.AreEqual(0.0888009191906366, fieldValues[1].Y);
			Assert.AreEqual(0, fieldValues[1].Z);

			Assert.AreEqual(-0.16906670076139624, fieldValues[2].X);
			Assert.AreEqual(0.084533350380698646, fieldValues[2].Y);
			Assert.AreEqual(0, fieldValues[2].Z);
		}

		private void AssertCorrectMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			Assert.AreEqual(-5.3364238191853149E-20, fieldValues[0].X);
			Assert.AreEqual(9.86623976961809E-17, fieldValues[0].Y);
			Assert.AreEqual(0, fieldValues[0].Z);

			Assert.AreEqual(2.2769755079442495E-19, fieldValues[1].X);
			Assert.AreEqual(0.003347177956242457, fieldValues[1].Y);
			Assert.AreEqual(0, fieldValues[1].Z);

			Assert.AreEqual(-0.0066707489691250708, fieldValues[2].X);
			Assert.AreEqual(0.0033235710128827959, fieldValues[2].Y);
			Assert.AreEqual(0, fieldValues[2].Z);

			Assert.AreEqual(-0.080193604428246038, fieldValues[3].X);
			Assert.AreEqual(0.079160521928345773, fieldValues[3].Y);
			Assert.AreEqual(0, fieldValues[3].Z);
		}
	}
}