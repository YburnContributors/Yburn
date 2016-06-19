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

		private static readonly double QGPConductivityMeV = 5.8;

		private static readonly double PointChargeVelocity = 0.99995;

		private static readonly double[] EffectiveTimes = new double[] { 0.1, 0.4, 1.0, 4.0, 10.0 };

		private static readonly double RadialDistance = 7.4;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double[,] CalculatePointChargeFields(EMFCalculationMethod method)
		{
			PointChargeElectromagneticField emf = PointChargeElectromagneticField.Create(
				method, QGPConductivityMeV, PointChargeVelocity);

			double[,] fieldValues = new double[3, EffectiveTimes.Length];
			for(int i = 0; i < EffectiveTimes.Length; i++)
			{
				fieldValues[0, i] = emf.CalculatePointChargeAzimutalMagneticField(
					EffectiveTimes[i], RadialDistance);
				fieldValues[1, i] = emf.CalculatePointChargeLongitudinalElectricField(
					EffectiveTimes[i], RadialDistance);
				fieldValues[2, i] = emf.CalculatePointChargeRadialElectricField(
					EffectiveTimes[i], RadialDistance);
			}

			return fieldValues;
		}

		private void AssertCorrectPointChargeFields_URLimitFourierSynthesis(double[,] fieldValues)
		{
			AssertHelper.AssertRoundedEqual(0.0049474011562835631, fieldValues[0, 0]);
			AssertHelper.AssertRoundedEqual(0.0059385275681660592, fieldValues[0, 1]);
			AssertHelper.AssertRoundedEqual(0.0017557798644070881, fieldValues[0, 2]);
			AssertHelper.AssertRoundedEqual(0.00014832194661731433, fieldValues[0, 3]);
			AssertHelper.AssertRoundedEqual(2.5189372551999923E-05, fieldValues[0, 4]);

			AssertHelper.AssertRoundedEqual(1.2001240098041735E-05, fieldValues[1, 0]);
			AssertHelper.AssertRoundedEqual(7.6219080569357483E-08, fieldValues[1, 1]);
			AssertHelper.AssertRoundedEqual(-9.5480139553502267E-07, fieldValues[1, 2]);
			AssertHelper.AssertRoundedEqual(-1.2243037559096488E-07, fieldValues[1, 3]);
			AssertHelper.AssertRoundedEqual(-2.2213597473717023E-08, fieldValues[1, 4]);

			AssertHelper.AssertRoundedEqual(0.0049474011562835631, fieldValues[2, 0]);
			AssertHelper.AssertRoundedEqual(0.0059385275681660592, fieldValues[2, 1]);
			AssertHelper.AssertRoundedEqual(0.0017557798644070881, fieldValues[2, 2]);
			AssertHelper.AssertRoundedEqual(0.00014832194661731433, fieldValues[2, 3]);
			AssertHelper.AssertRoundedEqual(2.5189372551999923E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_DiffusionApproximation(double[,] fieldValues)
		{
			AssertHelper.AssertRoundedEqual(0.0046867553901049569, fieldValues[0, 0]);
			AssertHelper.AssertRoundedEqual(0.0059898207639691742, fieldValues[0, 1]);
			AssertHelper.AssertRoundedEqual(0.0017525326354007119, fieldValues[0, 2]);
			AssertHelper.AssertRoundedEqual(0.00014811951578589806, fieldValues[0, 3]);
			AssertHelper.AssertRoundedEqual(2.5173609831507704E-05, fieldValues[0, 4]);

			AssertHelper.AssertRoundedEqual(1.3031147807508825E-05, fieldValues[1, 0]);
			AssertHelper.AssertRoundedEqual(3.2879587328797416E-08, fieldValues[1, 1]);
			AssertHelper.AssertRoundedEqual(-9.6301121710903459E-07, fieldValues[1, 2]);
			AssertHelper.AssertRoundedEqual(-1.2249334999995673E-07, fieldValues[1, 3]);
			AssertHelper.AssertRoundedEqual(-2.221542259103232E-08, fieldValues[1, 4]);

			AssertHelper.AssertRoundedEqual(0.0046867553901049569, fieldValues[2, 0]);
			AssertHelper.AssertRoundedEqual(0.0059898207639691742, fieldValues[2, 1]);
			AssertHelper.AssertRoundedEqual(0.0017525326354007119, fieldValues[2, 2]);
			AssertHelper.AssertRoundedEqual(0.00014811951578589806, fieldValues[2, 3]);
			AssertHelper.AssertRoundedEqual(2.5173609831507704E-05, fieldValues[2, 4]);
		}

		private void AssertCorrectPointChargeFields_FreeSpace(double[,] fieldValues)
		{
			AssertHelper.AssertRoundedEqual(0.009262679881516021, fieldValues[0, 0]);
			AssertHelper.AssertRoundedEqual(0.000264934116228407, fieldValues[0, 1]);
			AssertHelper.AssertRoundedEqual(1.7688222647396556E-05, fieldValues[0, 2]);
			AssertHelper.AssertRoundedEqual(2.7850892498847166E-07, fieldValues[0, 3]);
			AssertHelper.AssertRoundedEqual(1.7832258486247548E-08, fieldValues[0, 4]);

			AssertHelper.AssertRoundedEqual(-0.00012517134975021649, fieldValues[1, 0]);
			AssertHelper.AssertRoundedEqual(-1.4320763039373351E-05, fieldValues[1, 1]);
			AssertHelper.AssertRoundedEqual(-2.3903003577562913E-06, fieldValues[1, 2]);
			AssertHelper.AssertRoundedEqual(-1.5054536485863333E-07, fieldValues[1, 3]);
			AssertHelper.AssertRoundedEqual(-2.4097646603037224E-08, fieldValues[1, 4]);

			AssertHelper.AssertRoundedEqual(0.009263143038667954, fieldValues[2, 0]);
			AssertHelper.AssertRoundedEqual(0.00026494736359658684, fieldValues[2, 1]);
			AssertHelper.AssertRoundedEqual(1.7689107102751692E-05, fieldValues[2, 2]);
			AssertHelper.AssertRoundedEqual(2.7852285113102822E-07, fieldValues[2, 3]);
			AssertHelper.AssertRoundedEqual(1.7833150143754736E-08, fieldValues[2, 4]);
		}
	}
}