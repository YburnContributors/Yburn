using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class MagneticFieldStrengthAveragerTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateAverageMagneticFieldStrength()
		{
			MagneticFieldStrengthAverager averager =
				new MagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageMagneticFieldStrength(8);

			AssertHelper.AssertApproximatelyEqual(0.74916774299354139, result, 14);
		}

		[TestMethod]
		public void CalculateAverageMagneticFieldStrength_LCF()
		{
			MagneticFieldStrengthAverager averager =
				new MagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageMagneticFieldStrength_LCF(8);

			AssertHelper.AssertApproximatelyEqual(0.23624004632176207, result);
		}

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.BeamRapidity = 7.99;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.FormationTimesFm = new double[] { 0.4, 0.4, 0.4, 0.4, 0.4, 0.4 };
			param.GridCellSizeFm = 1;
			param.GridRadiusFm = 10;
			param.ImpactParameterFm = 7;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.QGPConductivityMeV = 5.8;
			param.NucleusShapeA = NucleusShape.WoodsSaxonPotential;
			param.NucleusShapeB = NucleusShape.WoodsSaxonPotential;

			return param;
		}
	}
}
