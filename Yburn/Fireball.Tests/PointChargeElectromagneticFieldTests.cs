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

			AssertCorrectPointChargeFields_URLimitFourierSynthesis(fieldValuesPositiveRapidity);
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

			AssertCorrectPointChargeFields_DiffusionApproximation(fieldValuesPositiveRapidity);
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

		private static FireballParam CreateFireballParam(
			EMFCalculationMethod emfCalculationMethod
			)
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = emfCalculationMethod;
			param.QGPConductivityMeV = 5.8;

			return param;
		}

		private static readonly double QGPConductivityMeV = 5.8;

		private static readonly double PointChargeRapidity = 5.3;

		private static readonly double[] EffectiveTimes = new double[] { 0.1, 0.4, 1.0, 4.0, 10.0 };

		private static readonly double RadialDistance = 7.4;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double[,] CalculatePointChargeFields_PositiveRapidity(EMFCalculationMethod method)
		{
			PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
				method, QGPConductivityMeV, PointChargeRapidity);

			double[,] fieldValues = new double[3, EffectiveTimes.Length];
			for(int i = 0; i < EffectiveTimes.Length; i++)
			{
				fieldValues[0, i] = emf.CalculateAzimutalMagneticField(
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
				method, QGPConductivityMeV, -PointChargeRapidity);

			double[,] fieldValues = new double[3, EffectiveTimes.Length];
			for(int i = 0; i < EffectiveTimes.Length; i++)
			{
				fieldValues[0, i] = emf.CalculateAzimutalMagneticField(
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
			double[,] switchedFieldValues =
				new double[fieldValues.GetLength(0), fieldValues.GetLength(1)];

			for(int i = 0; i < fieldValues.GetLength(1); i++)
			{
				switchedFieldValues[0, i] = -fieldValues[0, i];
				switchedFieldValues[1, i] = -fieldValues[1, i];
				switchedFieldValues[2, i] = fieldValues[2, i];
			}

			return switchedFieldValues;
		}

		private void AssertCorrectPointChargeFields_URLimitFourierSynthesis(double[,] fieldValues)
		{
			AssertHelper.AssertApproximatelyEqual(0.0049467209997090845, fieldValues[0, 0]);
			AssertHelper.AssertApproximatelyEqual(0.0059387002261759025, fieldValues[0, 1]);
			AssertHelper.AssertApproximatelyEqual(0.0017557690290176549, fieldValues[0, 2]);
			AssertHelper.AssertApproximatelyEqual(0.00014832126189119873, fieldValues[0, 3]);
			AssertHelper.AssertApproximatelyEqual(2.51893192097701E-05, fieldValues[0, 4]);

			AssertHelper.AssertApproximatelyEqual(1.1963757605413558E-05, fieldValues[1, 0]);
			AssertHelper.AssertApproximatelyEqual(7.5823493934334626E-08, fieldValues[1, 1]);
			AssertHelper.AssertApproximatelyEqual(-9.5159750512638544E-07, fieldValues[1, 2]);
			AssertHelper.AssertApproximatelyEqual(-1.2201623967417586E-07, fieldValues[1, 3]);
			AssertHelper.AssertApproximatelyEqual(-2.2138424860791441E-08, fieldValues[1, 4]);

			AssertHelper.AssertApproximatelyEqual(0.0049467209997090845, fieldValues[2, 0]);
			AssertHelper.AssertApproximatelyEqual(0.0059387002261759025, fieldValues[2, 1]);
			AssertHelper.AssertApproximatelyEqual(0.0017557690290176549, fieldValues[2, 2]);
			AssertHelper.AssertApproximatelyEqual(0.00014832126189119873, fieldValues[2, 3]);
			AssertHelper.AssertApproximatelyEqual(2.51893192097701E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_DiffusionApproximation(double[,] fieldValues)
		{
			AssertHelper.AssertApproximatelyEqual(0.0046867553901049569, fieldValues[0, 0]);
			AssertHelper.AssertApproximatelyEqual(0.0059898207639691742, fieldValues[0, 1]);
			AssertHelper.AssertApproximatelyEqual(0.0017525326354007119, fieldValues[0, 2]);
			AssertHelper.AssertApproximatelyEqual(0.00014811951578589806, fieldValues[0, 3]);
			AssertHelper.AssertApproximatelyEqual(2.5173609831507704E-05, fieldValues[0, 4]);

			AssertHelper.AssertApproximatelyEqual(1.2987045736925823E-05, fieldValues[1, 0]);
			AssertHelper.AssertApproximatelyEqual(3.2768311031226838E-08, fieldValues[1, 1]);
			AssertHelper.AssertApproximatelyEqual(-9.5975204229983674E-07, fieldValues[1, 2]);
			AssertHelper.AssertApproximatelyEqual(-1.2207878863917365E-07, fieldValues[1, 3]);
			AssertHelper.AssertApproximatelyEqual(-2.2140237645729471E-08, fieldValues[1, 4]);

			AssertHelper.AssertApproximatelyEqual(0.0046867553901049569, fieldValues[2, 0]);
			AssertHelper.AssertApproximatelyEqual(0.0059898207639691742, fieldValues[2, 1]);
			AssertHelper.AssertApproximatelyEqual(0.0017525326354007119, fieldValues[2, 2]);
			AssertHelper.AssertApproximatelyEqual(0.00014811951578589806, fieldValues[2, 3]);
			AssertHelper.AssertApproximatelyEqual(2.5173609831507704E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_FreeSpace(double[,] fieldValues)
		{
			AssertHelper.AssertApproximatelyEqual(0.0092479378354883117, fieldValues[0, 0]);
			AssertHelper.AssertApproximatelyEqual(0.00026408176571688155, fieldValues[0, 1]);
			AssertHelper.AssertApproximatelyEqual(1.7628840881285309E-05, fieldValues[0, 2]);
			AssertHelper.AssertApproximatelyEqual(2.7756673928392448E-07, fieldValues[0, 3]);
			AssertHelper.AssertApproximatelyEqual(1.77719066637953E-08, fieldValues[0, 4]);

			AssertHelper.AssertApproximatelyEqual(-0.00012497213291200423, fieldValues[1, 0]);
			AssertHelper.AssertApproximatelyEqual(-1.4274690038750356E-05, fieldValues[1, 1]);
			AssertHelper.AssertApproximatelyEqual(-2.3822757947682848E-06, fieldValues[1, 2]);
			AssertHelper.AssertApproximatelyEqual(-1.500360752886078E-07, fieldValues[1, 3]);
			AssertHelper.AssertApproximatelyEqual(-2.4016090086209862E-08, fieldValues[1, 4]);

			AssertHelper.AssertApproximatelyEqual(0.0092483986903891917, fieldValues[2, 0]);
			AssertHelper.AssertApproximatelyEqual(0.00026409492577246675, fieldValues[2, 1]);
			AssertHelper.AssertApproximatelyEqual(1.7629719383916E-05, fieldValues[2, 2]);
			AssertHelper.AssertApproximatelyEqual(2.7758057133971843E-07, fieldValues[2, 3]);
			AssertHelper.AssertApproximatelyEqual(1.7772792295860485E-08, fieldValues[2, 4]);
		}
	}
}