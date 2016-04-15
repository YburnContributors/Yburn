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

			return nameValuePairs;
		}

		private static void AssertCorrectQGPSuppression_AveragedTemperature(
			double[][][][] suppressionFactors
			)
		{
			Assert.AreEqual(0.68086962014632735, suppressionFactors[0][0][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.14316306431160888, suppressionFactors[0][0][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.18128642440959014, suppressionFactors[0][0][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.043013431100560634, suppressionFactors[0][0][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.055209555926692321, suppressionFactors[0][0][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.022949200077261551, suppressionFactors[0][0][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.692577005969072, suppressionFactors[0][1][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.15527974936968483, suppressionFactors[0][1][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.19352954640638462, suppressionFactors[0][1][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.046384989470286041, suppressionFactors[0][1][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.056518379807095565, suppressionFactors[0][1][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.022207528727187342, suppressionFactors[0][1][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.71632855861501921, suppressionFactors[0][2][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.17155472114698531, suppressionFactors[0][2][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.2205673993061486, suppressionFactors[0][2][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.052076246911228329, suppressionFactors[0][2][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.069235771434250332, suppressionFactors[0][2][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.030085208057886892, suppressionFactors[0][2][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.76225785729577922, suppressionFactors[0][3][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.2366583606019653, suppressionFactors[0][3][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.28570161253481574, suppressionFactors[0][3][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.066147513280696679, suppressionFactors[0][3][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.094767390009490082, suppressionFactors[0][3][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.038453305205380484, suppressionFactors[0][3][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.80994888548035426, suppressionFactors[0][4][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.3118370445707252, suppressionFactors[0][4][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.37363245395161121, suppressionFactors[0][4][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.08743434987315464, suppressionFactors[0][4][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.13898019841288758, suppressionFactors[0][4][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.060917075314357125, suppressionFactors[0][4][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.847136008290759, suppressionFactors[0][5][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.44205589982694382, suppressionFactors[0][5][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.44611297681011847, suppressionFactors[0][5][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.12966406354363527, suppressionFactors[0][5][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.16203574812626245, suppressionFactors[0][5][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.078832664640274872, suppressionFactors[0][5][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.88756320447105375, suppressionFactors[0][6][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.59250761351502523, suppressionFactors[0][6][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.54493880481260482, suppressionFactors[0][6][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.21574903327848002, suppressionFactors[0][6][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.24708490930707822, suppressionFactors[0][6][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.10379612247358185, suppressionFactors[0][6][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.76312975577621667, suppressionFactors[1][0][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.27758105200750893, suppressionFactors[1][0][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.31187378072889271, suppressionFactors[1][0][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.098790631966432854, suppressionFactors[1][0][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.1284206335439522, suppressionFactors[1][0][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.06024302245421876, suppressionFactors[1][0][0][(int)BottomiumState.x3P], 1e-14);
		}

		private static void AssertCorrectQGPSuppression_UnshiftedTemperature(
			double[][][][] suppressionFactors
			)
		{
			Assert.AreEqual(0.64504470583267826, suppressionFactors[0][0][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.13099636298486359, suppressionFactors[0][0][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.1581958547896371, suppressionFactors[0][0][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.037805902863083128, suppressionFactors[0][0][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.051666699982967849, suppressionFactors[0][0][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.021291743407713348, suppressionFactors[0][0][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.65742458981930174, suppressionFactors[0][1][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.1286794970684276, suppressionFactors[0][1][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.16946735763371593, suppressionFactors[0][1][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.040047047904081548, suppressionFactors[0][1][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.049868250828717174, suppressionFactors[0][1][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.022149920207538511, suppressionFactors[0][1][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.68250250097376686, suppressionFactors[0][2][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.15003075394753368, suppressionFactors[0][2][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.19142943905254989, suppressionFactors[0][2][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.0468298403428202, suppressionFactors[0][2][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.059595364571460106, suppressionFactors[0][2][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.027285388768641988, suppressionFactors[0][2][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.73091257746693872, suppressionFactors[0][3][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.19678993164606615, suppressionFactors[0][3][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.24632108515359974, suppressionFactors[0][3][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.0644019823491814, suppressionFactors[0][3][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.082243011871106886, suppressionFactors[0][3][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.034724819071799647, suppressionFactors[0][3][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.7811002988219532, suppressionFactors[0][4][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.28227183723495597, suppressionFactors[0][4][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.32655602666659855, suppressionFactors[0][4][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.085209109122750287, suppressionFactors[0][4][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.11902345513889848, suppressionFactors[0][4][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.053370475131223616, suppressionFactors[0][4][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.8206507505735231, suppressionFactors[0][5][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.3422946007239146, suppressionFactors[0][5][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.40897117538427269, suppressionFactors[0][5][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.12566383686406413, suppressionFactors[0][5][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.15536453415968757, suppressionFactors[0][5][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.064157553578111412, suppressionFactors[0][5][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.864312172915215, suppressionFactors[0][6][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.47115878555061386, suppressionFactors[0][6][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.50112716740252372, suppressionFactors[0][6][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.14571662694065002, suppressionFactors[0][6][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.22050453245128895, suppressionFactors[0][6][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.10356296037829871, suppressionFactors[0][6][0][(int)BottomiumState.x3P], 1e-14);

			Assert.AreEqual(0.73231901342343508, suppressionFactors[1][0][0][(int)BottomiumState.Y1S], 1e-14);
			Assert.AreEqual(0.23902319522368137, suppressionFactors[1][0][0][(int)BottomiumState.x1P], 1e-14);
			Assert.AreEqual(0.27754945106862167, suppressionFactors[1][0][0][(int)BottomiumState.Y2S], 1e-14);
			Assert.AreEqual(0.088539233545740281, suppressionFactors[1][0][0][(int)BottomiumState.x2P], 1e-14);
			Assert.AreEqual(0.11240887845296035, suppressionFactors[1][0][0][(int)BottomiumState.Y3S], 1e-14);
			Assert.AreEqual(0.054117016875894854, suppressionFactors[1][0][0][(int)BottomiumState.x3P], 1e-14);
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