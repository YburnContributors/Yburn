using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Yburn.Fireball;
using Yburn.Tests.Util;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class QQonFireTests : QQonFire
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
		public void CalculateInitialPopulations_CMS2012()
		{
			double[] ppYields = GetProtonProtonYieldsInQQonFire(ProtonProtonBaseline.CMS2012);
			double[] initialPopulations = BottomiumCascade.GetInitialPopulations(ppYields);
			AssertCorrectInitialPopulations_CMS2012(initialPopulations);
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

		private static void AssertCorrectInitialPopulations_CMS2012(
			double[] initialPopulations
			)
		{
			AssertHelper.AssertRoundedEqual(13.811371002243353, initialPopulations[0]);
			AssertHelper.AssertRoundedEqual(43.69470939802315, initialPopulations[1]);
			AssertHelper.AssertRoundedEqual(17.730737923019689, initialPopulations[2]);
			AssertHelper.AssertRoundedEqual(45.626563577477171, initialPopulations[3]);
			AssertHelper.AssertRoundedEqual(10.893164282464253, initialPopulations[4]);
			AssertHelper.AssertRoundedEqual(7.6588791939455149E+99, initialPopulations[5]);
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
			nameValuePairs.Add("InitialCentralTemperature", "550");
			nameValuePairs.Add("MinimalCentralTemperature", "160");
			nameValuePairs.Add("FormationTimes", "0.3,0.3,0.3,0.3,0.3,0.3");
			nameValuePairs.Add("ThermalTime", "0.1");
			nameValuePairs.Add("GridCellSize", "1");
			nameValuePairs.Add("GridRadius", "11");
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
			AssertHelper.AssertRoundedEqual(0.69416771573504366, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.15205702539109442, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.19254878741681819, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.04568562720830853, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.05863943251594736, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.024374912037664256, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.706840646026698, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.16586279374989857, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.20671949411734458, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.049546344415670879, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.06037037290739445, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.023721076140733734, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.73231051917315837, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.18530592690107875, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.23824728398009587, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.056250490459661573, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.0747854603834698, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.032496729493807851, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.78123989907509617, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.26172698830314295, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.31596526910694783, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.073154353788071336, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.104805862412777, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.042526567575460909, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.83141979080072514, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.35508319861384291, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.41210906336044223, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.09955991171893791, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.1582542365036714, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.069365171119677355, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.86976998115487236, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.51764833524367637, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.49663280997076248, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.15183687555496933, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.18974418239020935, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.092313206637799544, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.91007161054070751, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.65573253929699715, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.60921969042046142, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.26208668542752617, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.30015274651992385, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.12608900855433455, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.77665332129435494, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.29548547851534979, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.33195372929111455, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.10699701663508131, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.13801130716172277, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.066064899163620447, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
		}

		private static void AssertCorrectQGPSuppression_UnshiftedTemperature(
			double[][][][] suppressionFactors
			)
		{
			AssertHelper.AssertRoundedEqual(0.66092580176562676, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.13913447150797736, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.16802372331082671, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.040154582889386344, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.05487647774217623, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.022614486384863897, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.67454570650605994, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.13744960929871825, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.18101735415067727, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.042776442356975829, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.053267006398519362, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.023659541329804305, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.70179952116046673, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.16205667636741211, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.20677373026564111, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.050583550921695114, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.06437231338582064, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.02947248681956004, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.754153665128512, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.21763539647890548, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.2724132610569186, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.0712239231336395, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.0909548082504475, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.038403132238608174, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.80786253568719613, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.32141783212888442, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.37184343709148676, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.097026070350414828, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.13552985414024427, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.060771994077699246, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.84906429219884494, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.40082765620281707, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.46135790786288405, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.1471526021787109, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.18193217760379016, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.0751286224816427, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.8926925737732575, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.57235224891061132, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.57010245236248236, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.17701301919542423, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.26786355031407744, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.12580576890400502, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.749523445601456, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.25693623437404312, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.29750673100004915, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.095822370981658064, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.12240356672190413, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.059246679685368578, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double[] GetProtonProtonYieldsInQQonFire(
			ProtonProtonBaseline baseline
			)
		{
			FeedDown3P = 0.06;
			ProtonProtonBaseline = ProtonProtonBaseline.CMS2012;

			return GetProtonProtonYields();
		}

		private double[][][][] CalculateQGPSuppressionFactorsInQQonFire(
			DecayWidthEvaluationType type
			)
		{
			StatusValues = new string[3];
			VariableNameValuePairs = GetQQonFireVariables(type);
			NumberCentralityBins = new int[] { 7, 1 };

			return CalculateQGPSuppressionFactors();
		}
	}
}