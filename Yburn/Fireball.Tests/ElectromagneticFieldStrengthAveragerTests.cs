using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class ElectromagneticFieldStrengthAveragerTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateAverageElectricFieldStrength()
		{
			ElectromagneticFieldStrengthAverager averager =
				new ElectromagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageElectricFieldStrengthPerFm2(0.4, 8);

			AssertHelper.AssertApproximatelyEqual(0.64257321196335648, result);
		}

		[TestMethod]
		public void CalculateAverageElectricFieldStrength_LCF()
		{
			ElectromagneticFieldStrengthAverager averager =
				new ElectromagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageElectricFieldStrengthPerFm2_LCF(0.4, 8);

			AssertHelper.AssertApproximatelyEqual(8.68669889202293, result);
		}

		[TestMethod]
		public void CalculateAverageMagneticFieldStrength()
		{
			ElectromagneticFieldStrengthAverager averager =
				new ElectromagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageMagneticFieldStrengthPerFm2(0.4, 8);

			AssertHelper.AssertApproximatelyEqual(0.74916774299354139, result, 14);
		}

		[TestMethod]
		public void CalculateAverageMagneticFieldStrength_LCF()
		{
			ElectromagneticFieldStrengthAverager averager =
				new ElectromagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageMagneticFieldStrengthPerFm2_LCF(0.4, 8);

			AssertHelper.AssertApproximatelyEqual(0.23624004632176207, result);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.BeamRapidity = 7.99;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.GridCellSizeFm = 1;
			param.GridRadiusFm = 10;
			param.ImpactParameterFm = 7;
			param.InelasticppCrossSectionFm = 6.4;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.QGPConductivityMeV = 5.8;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;

			return param;
		}
	}
}
