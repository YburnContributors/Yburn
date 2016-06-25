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
			nameValuePairs.Add("InitialCentralTemperature", "550");
			nameValuePairs.Add("MinimalCentralTemperature", "160");
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
			AssertHelper.AssertRoundedEqual(0.69416771573504343, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.15205702539109445, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.19254878741681819, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.045685627208308516, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.058639432515947346, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.024374912037664245, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.70684064602669794, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.16586279374989857, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.20671949411734461, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.049546344415670879, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.060370372907394429, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.023721076140733731, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.73231051917315848, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.18530592690107881, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.2382472839800959, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.056250490459661566, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.07478546038346981, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.032496729493807837, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.78123989907509639, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.26172698830314295, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.31596526910694794, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.07315435378807135, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.10480586241277703, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.042526567575460916, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.83141979080072492, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.35508319861384291, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.41210906336044228, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.099559911718937924, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.1582542365036714, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.069365171119677355, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.86976998115487236, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.51764833524367637, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.49663280997076253, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.1518368755549693, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.18974418239020932, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.092313206637799489, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.91007161054070762, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.65573253929699737, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.60921969042046187, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.26208668542752628, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.30015274651992391, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.12608900855433461, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.77665332129435516, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.2954854785153499, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.33195372929111466, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.10699701663508132, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.1380113071617228, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.066064899163620447, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
		}

		private static void AssertCorrectQGPSuppression_UnshiftedTemperature(
			double[][][][] suppressionFactors
			)
		{
			AssertHelper.AssertRoundedEqual(0.66092580176562676, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.13913447150797736, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.16802372331082674, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.04015458288938633, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.054876477742176223, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.022614486384863894, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.67454570650605994, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.13744960929871822, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.1810173541506773, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.042776442356975822, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.053267006398519362, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.023659541329804305, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.70179952116046673, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.16205667636741208, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.20677373026564119, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.050583550921695114, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.06437231338582064, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.029472486819560037, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.754153665128512, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.21763539647890545, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.27241326105691865, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.071223923133639511, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.090954808250447508, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.038403132238608181, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.80786253568719613, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.32141783212888442, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.37184343709148687, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.097026070350414842, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.13552985414024427, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.060771994077699246, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.84906429219884494, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.400827656202817, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.46135790786288416, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.14715260217871087, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.18193217760379019, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.07512862248164269, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.89269257377325784, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.57235224891061143, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.57010245236248269, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.17701301919542428, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.26786355031407749, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.12580576890400505, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertRoundedEqual(0.74952344560145612, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertRoundedEqual(0.25693623437404317, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertRoundedEqual(0.29750673100004921, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertRoundedEqual(0.0958223709816581, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertRoundedEqual(0.12240356672190415, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertRoundedEqual(0.059246679685368592, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
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