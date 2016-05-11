using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class ElectromagneticFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculatePointChargeFields_URLimitFourierSynthesis()
		{
			AssertCorrectPointChargeFields_URLimitFourierSynthesis(
				CalculatePointChargeFields(EMFCalculationMethod.URLimitFourierSynthesis));
		}

		[TestMethod]
		public void CalculatePointChargeFields_DiffusionApproximation()
		{
			AssertCorrectPointChargeFields_DiffusionApproximation(
				CalculatePointChargeFields(EMFCalculationMethod.DiffusionApproximation));
		}

		[TestMethod]
		public void CalculatePointChargeFields_FreeSpace()
		{
			AssertCorrectPointChargeFields_FreeSpace(
				CalculatePointChargeFields(EMFCalculationMethod.FreeSpace));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static FireballParam CreateFireballParam(
			EMFCalculationMethod emfCalculationMethod
			)
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = emfCalculationMethod;
			param.FourierFrequencySteps = 1000;
			param.MaxFourierFrequency = 1e3;
			param.MinFourierFrequency = 1e-3;
			param.QGPConductivityMeV = 5.8;

			return param;
		}

		private static readonly double[] EffectiveTimes = new double[] { 0.1, 0.4, 1.0, 4.0, 10.0 };

		private static readonly double LorentzFactor = 100.0;

		private static readonly double RadialDistance = 7.4;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double[,] CalculatePointChargeFields(EMFCalculationMethod method)
		{
			ElectromagneticField emf = ElectromagneticField.Create(CreateFireballParam(method));

			double[,] fieldValues = new double[3, EffectiveTimes.Length];
			for(int i = 0; i < EffectiveTimes.Length; i++)
			{
				fieldValues[0, i] = emf.CalculatePointChargeAzimutalMagneticField(
					EffectiveTimes[i], RadialDistance, LorentzFactor);
				fieldValues[1, i] = emf.CalculatePointChargeLongitudinalElectricField(
					EffectiveTimes[i], RadialDistance, LorentzFactor);
				fieldValues[2, i] = emf.CalculatePointChargeRadialElectricField(
					EffectiveTimes[i], RadialDistance, LorentzFactor);
			}

			return fieldValues;
		}

		private void AssertCorrectPointChargeFields_URLimitFourierSynthesis(double[,] fieldValues)
		{
			Assert.AreEqual(0.0049475635602519033, fieldValues[0, 0]);
			Assert.AreEqual(0.00593871520575941, fieldValues[0, 1]);
			Assert.AreEqual(0.0017558357972176566, fieldValues[0, 2]);
			Assert.AreEqual(0.00014832666848880948, fieldValues[0, 3]);
			Assert.AreEqual(2.5190172703916751E-05, fieldValues[0, 4]);

			Assert.AreEqual(1.2001898703703947E-05, fieldValues[1, 0]);
			Assert.AreEqual(7.62244322281048E-08, fieldValues[1, 1]);
			Assert.AreEqual(-9.54855435541566E-07, fieldValues[1, 2]);
			Assert.AreEqual(-1.2243732827321582E-07, fieldValues[1, 3]);
			Assert.AreEqual(-2.2214858057250161E-08, fieldValues[1, 4]);

			Assert.AreEqual(0.0049475635602519033, fieldValues[2, 0]);
			Assert.AreEqual(0.00593871520575941, fieldValues[2, 1]);
			Assert.AreEqual(0.0017558357972176566, fieldValues[2, 2]);
			Assert.AreEqual(0.00014832666848880948, fieldValues[2, 3]);
			Assert.AreEqual(2.5190172703916751E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_DiffusionApproximation(double[,] fieldValues)
		{
			Assert.AreEqual(0.0046867553901049561, fieldValues[0, 0]);
			Assert.AreEqual(0.0059898207639691725, fieldValues[0, 1]);
			Assert.AreEqual(0.0017525326354007117, fieldValues[0, 2]);
			Assert.AreEqual(0.00014811951578589804, fieldValues[0, 3]);
			Assert.AreEqual(2.5173609831507704E-05, fieldValues[0, 4]);

			Assert.AreEqual(1.303147359434814E-05, fieldValues[1, 0]);
			Assert.AreEqual(3.2880409339029525E-08, fieldValues[1, 1]);
			Assert.AreEqual(-9.63035292991319E-07, fieldValues[1, 2]);
			Assert.AreEqual(-1.2249641241026189E-07, fieldValues[1, 3]);
			Assert.AreEqual(-2.2215977990481151E-08, fieldValues[1, 4]);

			Assert.AreEqual(0.0046867553901049561, fieldValues[2, 0]);
			Assert.AreEqual(0.0059898207639691725, fieldValues[2, 1]);
			Assert.AreEqual(0.0017525326354007117, fieldValues[2, 2]);
			Assert.AreEqual(0.00014811951578589804, fieldValues[2, 3]);
			Assert.AreEqual(2.5173609831507704E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_FreeSpace(double[,] fieldValues)
		{
			Assert.AreEqual(0.0092627885485511378, fieldValues[0, 0]);
			Assert.AreEqual(0.00026494041156239204, fieldValues[0, 1]);
			Assert.AreEqual(1.7688661294938162E-05, fieldValues[0, 2]);
			Assert.AreEqual(2.7851588500794759E-07, fieldValues[0, 3]);
			Assert.AreEqual(1.7832704311815843E-08, fieldValues[0, 4]);

			Assert.AreEqual(-0.000125172818223664, fieldValues[1, 0]);
			Assert.AreEqual(-1.4321103327696867E-05, fieldValues[1, 1]);
			Assert.AreEqual(-2.390359634451103E-06, fieldValues[1, 2]);
			Assert.AreEqual(-1.5054912703132303E-07, fieldValues[1, 3]);
			Assert.AreEqual(-2.4098249070021406E-08, fieldValues[1, 4]);

			Assert.AreEqual(0.0092632517227169177, fieldValues[2, 0]);
			Assert.AreEqual(0.00026495365957657951, fieldValues[2, 1]);
			Assert.AreEqual(1.7689545794340919E-05, fieldValues[2, 2]);
			Assert.AreEqual(2.7852981184671959E-07, fieldValues[2, 3]);
			Assert.AreEqual(1.7833596013909647E-08, fieldValues[2, 4]);
		}
	}
}