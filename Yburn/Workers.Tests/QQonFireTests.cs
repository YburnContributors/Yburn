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
			ProtonProtonBaseline ppBaseline = ProtonProtonBaseline.CMS2012;
			double feedDown3P = 0.06;

			double[] initialQQPopulations = BottomiumCascade.GetInitialQQPopulations(
				ppBaseline, feedDown3P);
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
			AssertHelper.AssertApproximatelyEqual(13.831681883072369, initialQQPopulations[0]);
			AssertHelper.AssertApproximatelyEqual(43.69470939802315, initialQQPopulations[1]);
			AssertHelper.AssertApproximatelyEqual(17.730737923019689, initialQQPopulations[2]);
			AssertHelper.AssertApproximatelyEqual(45.626563577477171, initialQQPopulations[3]);
			AssertHelper.AssertApproximatelyEqual(10.893164282464253, initialQQPopulations[4]);
			AssertHelper.AssertApproximatelyEqual(7.6588791939455149E+99, initialQQPopulations[5]);
		}

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
			double[][][][] suppressionFactors
			)
		{
			AssertHelper.AssertApproximatelyEqual(0.689141742678362, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.15022619455483202, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.18908649759186147, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.044797104196318209, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.057319524225271583, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.024002685130619993, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.70189559673253143, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.16376956569956505, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.20319687029597477, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.048718342266889016, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.05915258985658526, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.023661856996550878, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.72735455240534552, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.18292919891040607, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.23366016770795348, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.055190174027759061, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.0729178336874576, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.032094201581999604, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.776146632955757, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.25780648577415727, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.30873786418944649, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.071733607360939172, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.10205425201513091, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.042135611710393685, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.82599378266778112, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.34918832687240736, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.40099861857289371, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.0975539896546211, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.15367516443465792, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.067804408958810666, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.86402294642800825, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.50673812294735943, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.48345153172440841, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.14831794698136355, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.18439888916805033, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.091168263928724361, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.90400326178075741, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.64051602376465633, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.59195185730217992, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.2550878749631848, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.29065903446397745, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.12582091497583, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.77151680528588007, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.29050947155242735, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.32434683175766843, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.10453076821667949, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.13392874993211715, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.0650478684508108, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
		}

		private static void AssertCorrectQGPSuppression_UnshiftedTemperature(
			double[][][][] suppressionFactors
			)
		{
			AssertHelper.AssertApproximatelyEqual(0.66092400851206878, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.13913757251810191, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.16803778763258181, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.040154956103386957, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.054877415833177789, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.022614516663553193, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.67454370076164738, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.13745219574215814, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.1810328796530819, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.042776814375612732, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.0532677396722611, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.023659543787278369, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.70179711960021229, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.16205988073379843, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.20679104226621864, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.05058394251074036, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.064373291513014033, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.029472506645586403, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.75415071875618289, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.21763963519097856, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.27243498235234209, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.071224588255232771, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.090956270159929423, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.038403138907567566, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.80785939677724883, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.32142478964268428, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.37187210567606832, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.097026718871488407, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.13553230925680262, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.060772036786548092, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.84906137900736711, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.40083451298058942, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.46138839218005423, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.14715396457800234, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.18193516535905444, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.075128622481612575, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.89269027614220164, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.57236164791487176, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.57012946866639813, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.1770138353596965, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.26786764742667807, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.12580578021560518, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			AssertHelper.AssertApproximatelyEqual(0.74952101784256642, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.25694061080828384, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.29752677816089318, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.095823060614334918, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.12240536967319705, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(0.059246710043294681, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
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