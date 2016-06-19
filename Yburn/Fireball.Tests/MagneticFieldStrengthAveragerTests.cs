using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.PhysUtil;
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
		public void CalculateAverageMagneticFieldStrengthInLabFrame()
		{
			MagneticFieldStrengthAverager averager =
				new MagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageMagneticFieldStrengthInLabFrame(
				QuadraturePrecision.Use8Points);

			AssertHelper.AssertRoundedEqual(0.70708322492671616, result);
		}

		[TestMethod]
		public void CalculateAverageMagneticFieldStrengthInLCF()
		{
			MagneticFieldStrengthAverager averager =
				new MagneticFieldStrengthAverager(CreateFireballParam());

			double result = averager.CalculateAverageMagneticFieldStrengthInLCF(
				QuadraturePrecision.Use8Points);

			AssertHelper.AssertRoundedEqual(0.22539532179428509, result);
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
			param.ShapeFunctionTypeA = ShapeFunctionType.WoodsSaxonPotential;
			param.ShapeFunctionTypeB = ShapeFunctionType.WoodsSaxonPotential;

			return param;
		}
	}
}
