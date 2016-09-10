using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.TestUtil;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class QQonFireTests : QQonFire
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CalculateInitialQQPopulations_CMS2012()
		{
			ProtonProtonBaseline = ProtonProtonBaseline.CMS2012;
			FeedDown3P = 0.06;

			double[] initialQQPopulations = BottomiumCascade.GetInitialQQPopulations(
				ProtonProtonBaseline, FeedDown3P);
			AssertCorrectInitialQQPopulations_CMS2012(initialQQPopulations);
		}

		[TestMethod]
		public void CalculateQGPSuppressionFactorsBinWise_AveragedTemperature()
		{
			double[][][][] suppressionFactors = CalculateQGPSuppressionFactorsInQQonFire(
				DecayWidthEvaluationType.AveragedTemperature);
			AssertCorrectQGPSuppression_AveragedTemperature(suppressionFactors);
		}

		[TestMethod]
		public void CalculateQGPSuppressionFactorsBinWise_UnshiftedTemperature()
		{
			double[][][][] suppressionFactors = CalculateQGPSuppressionFactorsInQQonFire(
				DecayWidthEvaluationType.UnshiftedTemperature);
			AssertCorrectQGPSuppression_UnshiftedTemperature(suppressionFactors);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertCorrectInitialQQPopulations_CMS2012(
			double[] initialQQPopulations
			)
		{
			AssertHelper.AssertRoundedEqual(13.811371002243353, initialQQPopulations[0]);
			AssertHelper.AssertRoundedEqual(43.69470939802315, initialQQPopulations[1]);
			AssertHelper.AssertRoundedEqual(17.730737923019689, initialQQPopulations[2]);
			AssertHelper.AssertRoundedEqual(45.626563577477171, initialQQPopulations[3]);
			AssertHelper.AssertRoundedEqual(10.893164282464253, initialQQPopulations[4]);
			AssertHelper.AssertRoundedEqual(7.6588791939455149E+99, initialQQPopulations[5]);
		}

		private static Dictionary<string, string> GetQQonFireVariables(
			DecayWidthEvaluationType type
			)
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("ExpansionMode", "Transverse");
			nameValuePairs.Add("DecayWidthType", "GammaTot");
			nameValuePairs.Add("TemperatureProfile", "NmixPHOBOS13");
			nameValuePairs.Add("DiffusenessA", "0.546");
			nameValuePairs.Add("DiffusenessB", "0.546");
			nameValuePairs.Add("NucleonNumberA", "208");
			nameValuePairs.Add("NucleonNumberB", "208");
			nameValuePairs.Add("PotentialTypes", "Complex");
			nameValuePairs.Add("TransverseMomenta", "6");
			nameValuePairs.Add("NuclearRadiusA", "6.62");
			nameValuePairs.Add("NuclearRadiusB", "6.62");
			nameValuePairs.Add("CentralityBinBoundaries", "0,5,10,20,30,40,50,60,100;0,100");
			nameValuePairs.Add("ImpactParamsAtBinBoundaries", "0,3,4,6,8,9,10,11,21;0,21");
			nameValuePairs.Add("InitialMaximumTemperature", "550");
			nameValuePairs.Add("BreakupTemperature", "160");
			nameValuePairs.Add("FormationTimes", "0.3,0.3,0.3,0.3,0.3,0.3");
			nameValuePairs.Add("ThermalTime", "0.1");
			nameValuePairs.Add("GridCellSize", "1");
			nameValuePairs.Add("GridRadius", "10");
			nameValuePairs.Add("BeamRapidity", "7.99");
			nameValuePairs.Add("DecayWidthEvaluationType", type.ToString());
			nameValuePairs.Add("ShapeFunctionTypeA", "WoodsSaxonPotential");
			nameValuePairs.Add("ShapeFunctionTypeB", "WoodsSaxonPotential");

			return nameValuePairs;
		}

		private static void AssertCorrectQGPSuppression_AveragedTemperature(
			double[][][][] suppressionFactors
			)
		{
			AssertHelper.AssertRoundedEqual(0.69416771573446012, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.15205702539100854, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.1925487874165395, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.045685627208333864, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.058639432515965262, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.024374912037685454, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.7068406460261224, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.16586279374979654, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.2067194941170501, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.049546344415701077, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.06037037290741569, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.023721076140757389, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.73231051917256051, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.18530592690099557, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.2382472839797343, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.056250490459696156, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.074785460383488767, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.032496729493837466, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.78123989907445823, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.26172698830274355, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.31596526910627376, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.073154353788067658, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.10480586241272083, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.042526567575468972, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.8314197908001425, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.3550831986132551, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.41210906335951991, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.099559911718889074, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.15825423650345089, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.069365171119633, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.869769981154432, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.51764833524279175, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.49663280996990122, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.15183687555488643, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.18974418239006868, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.092313206637775, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.91007161054040009, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.65573253929611308, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.60921969041969481, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.26208668542739672, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.30015274651972929, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.12608900855433627, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.77665332129383091, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.29548547851508994, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.33195372929064076, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.10699701663510344, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.1380113071616991, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.066064899163649743, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
		}

		private static void AssertCorrectQGPSuppression_UnshiftedTemperature(
			double[][][][] suppressionFactors
			)
		{
			AssertHelper.AssertRoundedEqual(0.66092580176498772, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.13913447150789768, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.16802372331062193, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.040154582889409748, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.054876477742192273, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.022614486384883316, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.67454570650543433, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.1374496092986692, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.18101735415045733, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.042776442357004216, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.053267006398543634, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.023659541329827748, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.70179952115982258, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.16205667636734336, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.20677373026537796, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.050583550921727352, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.0643723133858434, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.029472486819586842, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.75415366512782844, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.21763539647860428, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.27241326105640878, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.0712239231336328, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.090954808250399241, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.03840313223863763, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.80786253568655675, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.32141783212834019, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.37184343709066797, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.097026070350358429, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.13552985414005603, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.060771994077653817, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.849064292198348, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.40082765620227367, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.46135790786203712, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.14715260217861975, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.18193217760364011, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.075128622481612561, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.8926925737728979, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.57235224890988246, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.57010245236169665, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.17701301919539064, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.26786355031390874, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.12580576890400605, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.749523445600885, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.25693623437384239, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.29750673099966313, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.095822370981678381, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.12240356672188657, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.059246679685398637, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double[][][][] CalculateQGPSuppressionFactorsInQQonFire(
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