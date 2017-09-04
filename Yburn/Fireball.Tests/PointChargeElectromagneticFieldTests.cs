using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class PointChargeElectromagneticFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculatePointChargeFields_URLimitFourierSynthesis()
		{
			double[,] fieldValuesPositiveRapidity = CalculatePointChargeFields_PositiveRapidity(
				EMFCalculationMethod.URLimitFourierSynthesis);
			double[,] fieldValuesNegativeRapidity = CalculatePointChargeFields_NegativeRapidity(
				EMFCalculationMethod.URLimitFourierSynthesis);

			AssertCorrectPointChargeFields_URLimitFourierSynthesis(
				fieldValuesPositiveRapidity);
			AssertCorrectPointChargeFields_URLimitFourierSynthesis(
				SwitchRapidityDependentSigns(fieldValuesNegativeRapidity));
		}

		[TestMethod]
		public void CalculatePointChargeFields_DiffusionApproximation()
		{
			double[,] fieldValuesPositiveRapidity = CalculatePointChargeFields_PositiveRapidity(
				EMFCalculationMethod.DiffusionApproximation);
			double[,] fieldValuesNegativeRapidity = CalculatePointChargeFields_NegativeRapidity(
				EMFCalculationMethod.DiffusionApproximation);

			AssertCorrectPointChargeFields_DiffusionApproximation(
				fieldValuesPositiveRapidity);
			AssertCorrectPointChargeFields_DiffusionApproximation(
				SwitchRapidityDependentSigns(fieldValuesNegativeRapidity));
		}

		[TestMethod]
		public void CalculatePointChargeFields_FreeSpace()
		{
			double[,] fieldValuesPositiveRapidity = CalculatePointChargeFields_PositiveRapidity(
				EMFCalculationMethod.FreeSpace);
			double[,] fieldValuesNegativeRapidity = CalculatePointChargeFields_NegativeRapidity(
				EMFCalculationMethod.FreeSpace);

			AssertCorrectPointChargeFields_FreeSpace(fieldValuesPositiveRapidity);
			AssertCorrectPointChargeFields_FreeSpace(
				SwitchRapidityDependentSigns(fieldValuesNegativeRapidity));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double QGPConductivity = 5.8;

		private static readonly double PointChargeRapidity = 5.3;

		private static readonly double[] EffectiveTimes = new double[] { 0.1, 0.4, 1.0, 4.0, 10.0 };

		private static readonly double RadialDistance = 7.4;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double[,] CalculatePointChargeFields_PositiveRapidity(EMFCalculationMethod method)
		{
			PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
				method, QGPConductivity, PointChargeRapidity);

			double[,] fieldValues = new double[3, EffectiveTimes.Length];
			for(int i = 0; i < EffectiveTimes.Length; i++)
			{
				fieldValues[0, i] = emf.CalculateAzimuthalMagneticField(
					EffectiveTimes[i], RadialDistance);
				fieldValues[1, i] = emf.CalculateLongitudinalElectricField(
					EffectiveTimes[i], RadialDistance);
				fieldValues[2, i] = emf.CalculateRadialElectricField(
					EffectiveTimes[i], RadialDistance);
			}

			return fieldValues;
		}

		private double[,] CalculatePointChargeFields_NegativeRapidity(EMFCalculationMethod method)
		{
			PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
				method, QGPConductivity, -PointChargeRapidity);

			double[,] fieldValues = new double[3, EffectiveTimes.Length];
			for(int i = 0; i < EffectiveTimes.Length; i++)
			{
				fieldValues[0, i] = emf.CalculateAzimuthalMagneticField(
					EffectiveTimes[i], RadialDistance);
				fieldValues[1, i] = emf.CalculateLongitudinalElectricField(
					EffectiveTimes[i], RadialDistance);
				fieldValues[2, i] = emf.CalculateRadialElectricField(
					EffectiveTimes[i], RadialDistance);
			}

			return fieldValues;
		}

		private double[,] SwitchRapidityDependentSigns(
			double[,] fieldValues
			)
		{
			double[,] switchedFieldValues
				= new double[fieldValues.GetLength(0), fieldValues.GetLength(1)];

			for(int i = 0; i < fieldValues.GetLength(1); i++)
			{
				switchedFieldValues[0, i] = -fieldValues[0, i];
				switchedFieldValues[1, i] = -fieldValues[1, i];
				switchedFieldValues[2, i] = fieldValues[2, i];
			}

			return switchedFieldValues;
		}

		private void AssertCorrectPointChargeFields_URLimitFourierSynthesis(
			double[,] fieldValues
			)
		{
			AssertHelper.AssertApproximatelyEqual(0.0049465, fieldValues[0, 0], 5);
			AssertHelper.AssertApproximatelyEqual(0.0059384, fieldValues[0, 1], 5);
			AssertHelper.AssertApproximatelyEqual(0.0017557, fieldValues[0, 2], 5);
			AssertHelper.AssertApproximatelyEqual(0.00014831, fieldValues[0, 3], 5);
			AssertHelper.AssertApproximatelyEqual(2.5188E-05, fieldValues[0, 4], 5);

			AssertHelper.AssertApproximatelyEqual(1.1963E-05, fieldValues[1, 0], 5);
			AssertHelper.AssertApproximatelyEqual(7.5820E-08, fieldValues[1, 1], 5);
			AssertHelper.AssertApproximatelyEqual(-9.5155E-07, fieldValues[1, 2], 5);
			AssertHelper.AssertApproximatelyEqual(-1.2201E-07, fieldValues[1, 3], 5);
			AssertHelper.AssertApproximatelyEqual(-2.2137E-08, fieldValues[1, 4], 5);

			AssertHelper.AssertApproximatelyEqual(0.0049467, fieldValues[2, 0], 5);
			AssertHelper.AssertApproximatelyEqual(0.0059387, fieldValues[2, 1], 5);
			AssertHelper.AssertApproximatelyEqual(0.0017558, fieldValues[2, 2], 5);
			AssertHelper.AssertApproximatelyEqual(0.00014832, fieldValues[2, 3], 5);
			AssertHelper.AssertApproximatelyEqual(2.5189E-05, fieldValues[2, 4], 5);
		}

		private void AssertCorrectPointChargeFields_DiffusionApproximation(
			double[,] fieldValues
			)
		{
			AssertHelper.AssertApproximatelyEqual(0.0046865, fieldValues[0, 0], 5);
			AssertHelper.AssertApproximatelyEqual(0.0059895, fieldValues[0, 1], 5);
			AssertHelper.AssertApproximatelyEqual(0.0017524, fieldValues[0, 2], 5);
			AssertHelper.AssertApproximatelyEqual(0.00014811, fieldValues[0, 3], 5);
			AssertHelper.AssertApproximatelyEqual(2.5172E-05, fieldValues[0, 4], 5);

			AssertHelper.AssertApproximatelyEqual(1.2986E-05, fieldValues[1, 0], 5);
			AssertHelper.AssertApproximatelyEqual(3.2767E-08, fieldValues[1, 1], 5);
			AssertHelper.AssertApproximatelyEqual(-9.5970E-07, fieldValues[1, 2], 5);
			AssertHelper.AssertApproximatelyEqual(-1.2207E-07, fieldValues[1, 3], 5);
			AssertHelper.AssertApproximatelyEqual(-2.2139E-08, fieldValues[1, 4], 5);

			AssertHelper.AssertApproximatelyEqual(0.0046868, fieldValues[2, 0], 5);
			AssertHelper.AssertApproximatelyEqual(0.0059898, fieldValues[2, 1], 5);
			AssertHelper.AssertApproximatelyEqual(0.0017525, fieldValues[2, 2], 5);
			AssertHelper.AssertApproximatelyEqual(0.00014812, fieldValues[2, 3], 5);
			AssertHelper.AssertApproximatelyEqual(2.5174E-05, fieldValues[2, 4], 5);
		}

		private void AssertCorrectPointChargeFields_FreeSpace(
			double[,] fieldValues
			)
		{
			AssertHelper.AssertApproximatelyEqual(0.0092479, fieldValues[0, 0], 5);
			AssertHelper.AssertApproximatelyEqual(0.00026408, fieldValues[0, 1], 5);
			AssertHelper.AssertApproximatelyEqual(1.7629E-05, fieldValues[0, 2], 5);
			AssertHelper.AssertApproximatelyEqual(2.7757E-07, fieldValues[0, 3], 5);
			AssertHelper.AssertApproximatelyEqual(1.7772E-08, fieldValues[0, 4], 5);

			AssertHelper.AssertApproximatelyEqual(-0.00012497, fieldValues[1, 0], 5);
			AssertHelper.AssertApproximatelyEqual(-1.4275E-05, fieldValues[1, 1], 5);
			AssertHelper.AssertApproximatelyEqual(-2.3823E-06, fieldValues[1, 2], 5);
			AssertHelper.AssertApproximatelyEqual(-1.5004E-07, fieldValues[1, 3], 5);
			AssertHelper.AssertApproximatelyEqual(-2.4016E-08, fieldValues[1, 4], 5);

			AssertHelper.AssertApproximatelyEqual(0.0092484, fieldValues[2, 0], 5);
			AssertHelper.AssertApproximatelyEqual(0.00026409, fieldValues[2, 1], 5);
			AssertHelper.AssertApproximatelyEqual(1.7630E-05, fieldValues[2, 2], 5);
			AssertHelper.AssertApproximatelyEqual(2.7758E-07, fieldValues[2, 3], 5);
			AssertHelper.AssertApproximatelyEqual(1.7773E-08, fieldValues[2, 4], 5);
		}
	}
}
