using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Threading;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class PointChargeElectromagneticFieldTests
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
		public void CalculatePointChargeFields_URLimitFourierSynthesis()
		{
			double[,] fieldValues =
				CalculatePointChargeFields(EMFCalculationMethod.URLimitFourierSynthesis);
			AssertCorrectPointChargeFields_URLimitFourierSynthesis(fieldValues);
		}

		[TestMethod]
		public void CalculatePointChargeFields_DiffusionApproximation()
		{
			double[,] fieldValues =
				CalculatePointChargeFields(EMFCalculationMethod.DiffusionApproximation);
			AssertCorrectPointChargeFields_DiffusionApproximation(fieldValues);
		}

		[TestMethod]
		public void CalculatePointChargeFields_FreeSpace()
		{
			double[,] fieldValues =
				CalculatePointChargeFields(EMFCalculationMethod.FreeSpace);
			AssertCorrectPointChargeFields_FreeSpace(fieldValues);
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
			PointChargeElectromagneticField emf =
				PointChargeElectromagneticField.Create(CreateFireballParam(method));

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
			Assert.AreEqual(0.00494740617644823, fieldValues[0, 0]);
			Assert.AreEqual(0.0059385262927476, fieldValues[0, 1]);
			Assert.AreEqual(0.0017557799444454323, fieldValues[0, 2]);
			Assert.AreEqual(0.00014832195167545627, fieldValues[0, 3]);
			Assert.AreEqual(2.518937294604581E-05, fieldValues[0, 4]);

			Assert.AreEqual(1.2001516913746505E-05, fieldValues[1, 0]);
			Assert.AreEqual(7.6222006082363156E-08, fieldValues[1, 1]);
			Assert.AreEqual(-9.5482506233525629E-07, fieldValues[1, 2]);
			Assert.AreEqual(-1.2243343485310766E-07, fieldValues[1, 3]);
			Assert.AreEqual(-2.2214152781911365E-08, fieldValues[1, 4]);

			Assert.AreEqual(0.00494740617644823, fieldValues[2, 0]);
			Assert.AreEqual(0.0059385262927476, fieldValues[2, 1]);
			Assert.AreEqual(0.0017557799444454323, fieldValues[2, 2]);
			Assert.AreEqual(0.00014832195167545627, fieldValues[2, 3]);
			Assert.AreEqual(2.518937294604581E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_DiffusionApproximation(double[,] fieldValues)
		{
			Assert.AreEqual(0.0046867553901049569, fieldValues[0, 0]);
			Assert.AreEqual(0.0059898207639691742, fieldValues[0, 1]);
			Assert.AreEqual(0.0017525326354007119, fieldValues[0, 2]);
			Assert.AreEqual(0.00014811951578589806, fieldValues[0, 3]);
			Assert.AreEqual(2.5173609831507704E-05, fieldValues[0, 4]);

			Assert.AreEqual(1.3031473594348137E-05, fieldValues[1, 0]);
			Assert.AreEqual(3.2880409339029518E-08, fieldValues[1, 1]);
			Assert.AreEqual(-9.63035292991319E-07, fieldValues[1, 2]);
			Assert.AreEqual(-1.2249641241026189E-07, fieldValues[1, 3]);
			Assert.AreEqual(-2.2215977990481151E-08, fieldValues[1, 4]);

			Assert.AreEqual(0.0046867553901049569, fieldValues[2, 0]);
			Assert.AreEqual(0.0059898207639691742, fieldValues[2, 1]);
			Assert.AreEqual(0.0017525326354007119, fieldValues[2, 2]);
			Assert.AreEqual(0.00014811951578589806, fieldValues[2, 3]);
			Assert.AreEqual(2.5173609831507704E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_FreeSpace(double[,] fieldValues)
		{
			Assert.AreEqual(0.0092627885485511326, fieldValues[0, 0]);
			Assert.AreEqual(0.00026494041156239194, fieldValues[0, 1]);
			Assert.AreEqual(1.7688661294938162E-05, fieldValues[0, 2]);
			Assert.AreEqual(2.7851588500794764E-07, fieldValues[0, 3]);
			Assert.AreEqual(1.783270431181584E-08, fieldValues[0, 4]);

			Assert.AreEqual(-0.00012517281822366395, fieldValues[1, 0]);
			Assert.AreEqual(-1.4321103327696862E-05, fieldValues[1, 1]);
			Assert.AreEqual(-2.3903596344511026E-06, fieldValues[1, 2]);
			Assert.AreEqual(-1.5054912703132303E-07, fieldValues[1, 3]);
			Assert.AreEqual(-2.4098249070021406E-08, fieldValues[1, 4]);

			Assert.AreEqual(0.00926325172271691, fieldValues[2, 0]);
			Assert.AreEqual(0.00026495365957657934, fieldValues[2, 1]);
			Assert.AreEqual(1.7689545794340915E-05, fieldValues[2, 2]);
			Assert.AreEqual(2.7852981184671959E-07, fieldValues[2, 3]);
			Assert.AreEqual(1.7833596013909644E-08, fieldValues[2, 4]);
		}
	}
}