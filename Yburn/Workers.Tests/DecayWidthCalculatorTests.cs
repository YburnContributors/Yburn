using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.QQState;
using Yburn.TestUtil;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class DecayWidthCalculatorTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void GetBoundStateDataSets()
		{
			List<QQDataSet> dataSets = DecayWidthProvider.GetBoundStateDataSets(
				DataPathFile, PotentialTypes, BottomiumState.Y1S);

			Assert.AreEqual(4, dataSets.Count);
			Assert.AreEqual(9300, dataSets[0].BoundMass);
			Assert.AreEqual(ColorState.Singlet, dataSets[0].ColorState);
			Assert.AreEqual(400, dataSets[0].DebyeMass);
			Assert.AreEqual(-500, dataSets[0].Energy);
			Assert.AreEqual(10, dataSets[0].GammaDamp);
			Assert.AreEqual(0.2, dataSets[0].GammaDiss);
			Assert.AreEqual(200, dataSets[0].GammaTot);
			Assert.AreEqual(0, dataSets[0].L);
			Assert.AreEqual(1, dataSets[0].N);
			Assert.AreEqual(PotentialType.Complex, dataSets[0].PotentialType);
			Assert.AreEqual(0.23, dataSets[0].RMS);
			Assert.AreEqual(1400, dataSets[0].SoftScale);
			Assert.AreEqual(100, dataSets[0].Temperature);
			Assert.AreEqual(900, dataSets[0].UltraSoftScale);
		}

		[TestMethod]
		public void GivenTemperatureBelowQGPFormationTemperature_ReturnZero()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.UnshiftedTemperature);

			Assert.AreEqual(0, Calculator.GetDecayWidth(BottomiumState.Y1S, 150, 0));
		}

		[TestMethod]
		public void GivenTemperatureBelowBoundary_ReturnZero()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.UnshiftedTemperature);

			Assert.AreEqual(0, Calculator.GetDecayWidth(BottomiumState.x1P, 180, 0));
		}

		[TestMethod]
		public void GivenTemperatureAboveBoundary_ReturnInfinity()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.UnshiftedTemperature);

			Assert.AreEqual(double.PositiveInfinity, Calculator.GetDecayWidth(BottomiumState.Y1S, 800, 0));
		}

		[TestMethod]
		public void NoEntriesInDataFile_ReturnZeroOrInfinity()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.UnshiftedTemperature);

			Assert.AreEqual(0, Calculator.GetDecayWidth(BottomiumState.x3P, 150, 0));
			Assert.AreEqual(double.PositiveInfinity, Calculator.GetDecayWidth(BottomiumState.x3P, 180, 0));
		}

		[TestMethod]
		public void UsingUnshiftedTemperature_SimpleInterpolation()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.UnshiftedTemperature);

			Assert.AreEqual(450, Calculator.GetDecayWidth(BottomiumState.Y1S, 300, 0));
		}

		[TestMethod]
		public void UsingMaximallyBlueshifted_SimpleInterpolationWithMaximallyDopplerShiftedTemperature()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.MaximallyBlueshifted);

			// maximally blueshifted temperature: T * sqrt(1-v*v)/(1-v)
			// v = 0.6  =>  sqrt(1-v*v)/(1-v) = 2
			Assert.AreEqual(600, Calculator.GetDecayWidth(BottomiumState.Y1S, 200, 0.6));

		}

		[TestMethod]
		public void UsingAveragedTemperature_SimpleInterpolationWithAverageDopplerShiftedTemperature()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.AveragedTemperature);

			// averaged temperature: T * sqrt(1-v*v) * artanh(v)/v
			// v = 0.874348...  =>  sqrt(1-v*v) * artanh(v)/v = 0.75
			AssertHelper.AssertApproximatelyEqual(450, Calculator.GetDecayWidth(BottomiumState.Y1S, 400, 0.874348), 6);
		}

		[TestMethod]
		public void UsingAveragedTemperature_AverageDecayWidthsEvaluatedAtDopplerShiftedTemperatures()
		{
			CreateDecayWidthCalculator(DecayWidthEvaluationType.AveragedDecayWidth);

			AssertHelper.AssertApproximatelyEqual(303.343, Calculator.GetDecayWidth(BottomiumState.Y1S, 200, 0.2), 6);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly string DataPathFile = "..\\..\\bbdata-Test.txt";

		private static readonly List<PotentialType> PotentialTypes = new List<PotentialType> { PotentialType.Complex };

		private static readonly DecayWidthType DecayWidthType = DecayWidthType.GammaTot;

		private static double QGPFormationTemperature = 160;

		private static int NumberAveragingAngles = 20;

		private static DecayWidthProvider Calculator;

		private static void CreateDecayWidthCalculator(
			DecayWidthEvaluationType evaluationType
			)
		{
			Calculator = new DecayWidthProvider(
				DataPathFile,
				PotentialTypes,
				evaluationType,
				DecayWidthType,
				QGPFormationTemperature,
				NumberAveragingAngles);
		}
	}
}
