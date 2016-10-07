using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yburn.Fireball;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class QQonFireTests : QQonFire
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateQGPSuppressionFactorsBinWise_AveragedTemperature()
		{
			BottomiumVector[][][] suppressionFactors = CalculateQGPSuppressionFactorsInQQonFire(
				DecayWidthEvaluationType.AveragedTemperature);
			AssertCorrectQGPSuppression_AveragedTemperature(suppressionFactors);
		}

		[TestMethod]
		public void CalculateQGPSuppressionFactorsBinWise_UnshiftedTemperature()
		{
			BottomiumVector[][][] suppressionFactors = CalculateQGPSuppressionFactorsInQQonFire(
				DecayWidthEvaluationType.UnshiftedTemperature);
			AssertCorrectQGPSuppression_UnshiftedTemperature(suppressionFactors);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static Dictionary<string, string> GetQQonFireVariables(
			DecayWidthEvaluationType type
			)
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("BeamRapidity", "7.99");
			nameValuePairs.Add("BreakupTemperature", "160");
			nameValuePairs.Add("CentralityBinBoundaries", "0,5,10,20,30,40,50,60,100;0,100");
			nameValuePairs.Add("DecayWidthEvaluationType", type.ToString());
			nameValuePairs.Add("DecayWidthType", "GammaTot");
			nameValuePairs.Add("DiffusenessA", "0.546");
			nameValuePairs.Add("DiffusenessB", "0.546");
			nameValuePairs.Add("ExpansionMode", "Transverse");
			nameValuePairs.Add("FormationTimes", "0.3,0.3,0.3,0.3,0.3,0.3");
			nameValuePairs.Add("GridCellSize", "1");
			nameValuePairs.Add("GridRadius", "10");
			nameValuePairs.Add("ImpactParamsAtBinBoundaries", "0,3,4,6,8,9,10,11,21;0,21");
			nameValuePairs.Add("InitialMaximumTemperature", "550");
			nameValuePairs.Add("NuclearRadiusA", "6.62");
			nameValuePairs.Add("NuclearRadiusB", "6.62");
			nameValuePairs.Add("NucleonNumberA", "208");
			nameValuePairs.Add("NucleonNumberB", "208");
			nameValuePairs.Add("NucleusShapeA", "WoodsSaxonPotential");
			nameValuePairs.Add("NucleusShapeB", "WoodsSaxonPotential");
			nameValuePairs.Add("NumberAveragingAngles", "20");
			nameValuePairs.Add("PotentialTypes", "Complex");
			nameValuePairs.Add("ProtonNumberA", "82");
			nameValuePairs.Add("ProtonNumberB", "82");
			nameValuePairs.Add("QGPFormationTemperature", "160");
			nameValuePairs.Add("TemperatureProfile", "NmixPHOBOS13");
			nameValuePairs.Add("ThermalTime", "0.1");
			nameValuePairs.Add("TransverseMomenta", "6");

			return nameValuePairs;
		}

		private static void AssertCorrectQGPSuppression_AveragedTemperature(
			BottomiumVector[][][] suppressionFactors
			)
		{
			double delta = 1e-15;

			Assert.AreEqual(0.689141742678362, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.150226194554832, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.189086497591861, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.044797104196318, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.057319524225271, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.024002685130619, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.701895596732531, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.163769565699565, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.203196870295974, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.048718342266889, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.059152589856585, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.023661856996550, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.727354552405345, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.182929198910406, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.233660167707953, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.055190174027759, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.072917833687457, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.032094201581999, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.776146632955757, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.257806485774157, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.308737864189446, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.071733607360939, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.102054252015130, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.042135611710393, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.825993782667781, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.349188326872407, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.400998618572893, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.097553989654621, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.153675164434657, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.067804408958810, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.864022946428008, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.506738122947359, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.483451531724408, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.148317946981363, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.184398889168050, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.091168263928724, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.904003261780757, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.640516023764656, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.591951857302179, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.255087874963184, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.290659034463977, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.125820914975830, suppressionFactors[0][6][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.771516805285880, suppressionFactors[1][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.290509471552427, suppressionFactors[1][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.324346831757668, suppressionFactors[1][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.104530768216679, suppressionFactors[1][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.133928749932117, suppressionFactors[1][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.065047868450810, suppressionFactors[1][0][0][BottomiumState.x3P], delta);
		}

		private static void AssertCorrectQGPSuppression_UnshiftedTemperature(
			BottomiumVector[][][] suppressionFactors
			)
		{
			double delta = 1e-15;

			Assert.AreEqual(0.660924008512068, suppressionFactors[0][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.139137572518101, suppressionFactors[0][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.168037787632581, suppressionFactors[0][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.040154956103386, suppressionFactors[0][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.054877415833177, suppressionFactors[0][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.022614516663553, suppressionFactors[0][0][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.674543700761647, suppressionFactors[0][1][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.137452195742158, suppressionFactors[0][1][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.181032879653081, suppressionFactors[0][1][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.042776814375612, suppressionFactors[0][1][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.053267739672261, suppressionFactors[0][1][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.023659543787278, suppressionFactors[0][1][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.701797119600212, suppressionFactors[0][2][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.162059880733798, suppressionFactors[0][2][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.206791042266218, suppressionFactors[0][2][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.050583942510740, suppressionFactors[0][2][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.064373291513014, suppressionFactors[0][2][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.029472506645586, suppressionFactors[0][2][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.754150718756182, suppressionFactors[0][3][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.217639635190978, suppressionFactors[0][3][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.272434982352342, suppressionFactors[0][3][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.071224588255232, suppressionFactors[0][3][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.090956270159929, suppressionFactors[0][3][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.038403138907567, suppressionFactors[0][3][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.807859396777248, suppressionFactors[0][4][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.321424789642684, suppressionFactors[0][4][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.371872105676068, suppressionFactors[0][4][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.097026718871488, suppressionFactors[0][4][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.135532309256802, suppressionFactors[0][4][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.060772036786548, suppressionFactors[0][4][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.849061379007367, suppressionFactors[0][5][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.400834512980589, suppressionFactors[0][5][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.461388392180054, suppressionFactors[0][5][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.147153964578002, suppressionFactors[0][5][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.181935165359054, suppressionFactors[0][5][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.075128622481612, suppressionFactors[0][5][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.892690276142201, suppressionFactors[0][6][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.572361647914871, suppressionFactors[0][6][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.570129468666398, suppressionFactors[0][6][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.177013835359696, suppressionFactors[0][6][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.267867647426678, suppressionFactors[0][6][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.125805780215605, suppressionFactors[0][6][0][BottomiumState.x3P], delta);

			Assert.AreEqual(0.749521017842566, suppressionFactors[1][0][0][BottomiumState.Y1S], delta);
			Assert.AreEqual(0.256940610808283, suppressionFactors[1][0][0][BottomiumState.x1P], delta);
			Assert.AreEqual(0.297526778160893, suppressionFactors[1][0][0][BottomiumState.Y2S], delta);
			Assert.AreEqual(0.095823060614334, suppressionFactors[1][0][0][BottomiumState.x2P], delta);
			Assert.AreEqual(0.122405369673197, suppressionFactors[1][0][0][BottomiumState.Y3S], delta);
			Assert.AreEqual(0.059246710043294, suppressionFactors[1][0][0][BottomiumState.x3P], delta);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private BottomiumVector[][][] CalculateQGPSuppressionFactorsInQQonFire(
			DecayWidthEvaluationType type
			)
		{
			StatusValues = new string[3];
			VariableNameValuePairs = GetQQonFireVariables(type);
			int[] numberCentralityBins = new int[] { 7, 1 };

			return CalculateQGPSuppressionFactors(numberCentralityBins);
		}
	}
}