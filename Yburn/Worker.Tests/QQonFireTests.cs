using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Yburn.Fireball;

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
			Assert.AreEqual(13.811371002243353, initialPopulations[0]);
			Assert.AreEqual(43.69470939802315, initialPopulations[1]);
			Assert.AreEqual(17.730737923019689, initialPopulations[2]);
			Assert.AreEqual(45.626563577477171, initialPopulations[3]);
			Assert.AreEqual(10.893164282464253, initialPopulations[4]);
			Assert.AreEqual(7.6588791939455149E+99, initialPopulations[5]);
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
			nameValuePairs.Add("NumberGridCells", "11");
			nameValuePairs.Add("BeamRapidity", "7.99");
			nameValuePairs.Add("DecayWidthEvaluationType", type.ToString());
            nameValuePairs.Add("CollisionType", "LeadAOnLeadB");

			return nameValuePairs;
		}

		private static void AssertCorrectQGPSuppression_AveragedTemperature(
			double[][][][] suppressionFactors
			)
		{
			Assert.AreEqual(0.69416771586322834, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.15205702542475405, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.19254878748342438, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.045685627210575834, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.058639432521239335, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.024374912037580371, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.70684064615367359, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.16586279379014279, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.20671949419161531, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.049546344418366278, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.060370372913078646, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.023721076140436208, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.73231051928963864, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.18530592694125161, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.23824728406295617, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.056250490461516721, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.0747854603903557, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.032496729493154554, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.78123989917546721, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.2617269883636909, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.31596526921418422, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.073154353789435536, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.10480586242202333, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.042526567573757189, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.8314197908818638, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.35508319869077254, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.41210906348588144, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.099559911719745944, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.15825423651978518, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.069365171117771934, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.86976998121916349, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.51764833537207133, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.49663281009467181, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.15183687555627595, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.18974418240128541, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.092313206632822664, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.91007161058611441, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.65573253942717469, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.60921969053231018, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.26208668543832531, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.30015274654109236, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.12608900854419244, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.77665332139113219, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.29548547856649726, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.33195372937608258, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.10699701662802373, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.13801130716323654, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.06606489915160374, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
		}

		private static void AssertCorrectQGPSuppression_UnshiftedTemperature(
			double[][][][] suppressionFactors
			)
		{
			Assert.AreEqual(0.66092580190175465, suppressionFactors[0][0][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.13913447153928038, suppressionFactors[0][0][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.16802372336401564, suppressionFactors[0][0][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.040154582891192447, suppressionFactors[0][0][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.054876477747244114, suppressionFactors[0][0][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.022614486384820134, suppressionFactors[0][0][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.674545706641169, suppressionFactors[0][1][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.1374496093271432, suppressionFactors[0][1][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.18101735421044518, suppressionFactors[0][1][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.042776442358912141, suppressionFactors[0][1][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.053267006402661236, suppressionFactors[0][1][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.023659541329529726, suppressionFactors[0][1][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.7017995212853847, suppressionFactors[0][2][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.16205667640077726, suppressionFactors[0][2][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.20677373033111629, suppressionFactors[0][2][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.050583550923102127, suppressionFactors[0][2][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.06437231339063211, suppressionFactors[0][2][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.029472486818906, suppressionFactors[0][2][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.75415366523757288, suppressionFactors[0][3][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.21763539652270267, suppressionFactors[0][3][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.27241326114042447, suppressionFactors[0][3][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.071223923135563375, suppressionFactors[0][3][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.09095480825706917, suppressionFactors[0][3][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.038403132236777208, suppressionFactors[0][3][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.80786253577719158, suppressionFactors[0][4][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.32141783220042325, suppressionFactors[0][4][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.37184343720338942, suppressionFactors[0][4][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.097026070352058721, suppressionFactors[0][4][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.13552985415227781, suppressionFactors[0][4][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.060771994075717005, suppressionFactors[0][4][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.849064292271673, suppressionFactors[0][5][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.40082765627819456, suppressionFactors[0][5][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.461357907984821, suppressionFactors[0][5][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.147152602181344, suppressionFactors[0][5][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.18193217761623379, suppressionFactors[0][5][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.0751286224756989, suppressionFactors[0][5][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.892692573826445, suppressionFactors[0][6][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.5723522490166546, suppressionFactors[0][6][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.57010245247663116, suppressionFactors[0][6][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.17701301919083615, suppressionFactors[0][6][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.26786355033021775, suppressionFactors[0][6][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.12580576889396544, suppressionFactors[0][6][0][(int)BottomiumState.x3P]);

			Assert.AreEqual(0.74952344570614438, suppressionFactors[1][0][0][(int)BottomiumState.Y1S]);
			Assert.AreEqual(0.25693623441263669, suppressionFactors[1][0][0][(int)BottomiumState.x1P]);
			Assert.AreEqual(0.29750673107032882, suppressionFactors[1][0][0][(int)BottomiumState.Y2S]);
			Assert.AreEqual(0.095822370973747933, suppressionFactors[1][0][0][(int)BottomiumState.x2P]);
			Assert.AreEqual(0.12240356672081884, suppressionFactors[1][0][0][(int)BottomiumState.Y3S]);
			Assert.AreEqual(0.059246679673248995, suppressionFactors[1][0][0][(int)BottomiumState.x3P]);
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
			VariableNameValueList = GetQQonFireVariables(type);
			NumberCentralityBins = new int[] { 7, 1 };

			return CalculateQGPSuppressionFactors();
		}
	}
}