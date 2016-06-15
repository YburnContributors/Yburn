using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.Tests.Util;

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

			double result = averager.CalculateAverageMagneticFieldStrength(
				QuadraturePrecision.Use8Points);

			AssertHelper.AssertRoundedEqual(0.41503035843345487, result);
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
			param.ImpactParameterFm = 7.0;
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
