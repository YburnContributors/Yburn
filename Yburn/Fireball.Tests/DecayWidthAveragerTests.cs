using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yburn.PhysUtil;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class DecayWidthAveragerTests
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		[ClassInitialize]
		public static void ClassInitialize(
			TestContext context
			)
		{
			TemperatureDecayWidthList = new List<KeyValuePair<double, double>>();
			TemperatureDecayWidthList.Add(new KeyValuePair<double, double>(100, 200));
			TemperatureDecayWidthList.Add(new KeyValuePair<double, double>(200, 300));
			TemperatureDecayWidthList.Add(new KeyValuePair<double, double>(500, 650));

			Temperatures = new double[] { 100, 200, 500 };
			DecayWidths = new double[] { 200, 300, 650 };
			InterpolatedDecayWidth = new LinearInterpolation1D(Temperatures, DecayWidths);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void GivenTemperatureBelowBoundary_ReturnZero()
		{
			DecayWidthAverager averager =
				new DecayWidthAverager(TemperatureDecayWidthList, 1, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(0, averager.GetDecayWidth(60, 0, DecayWidthEvaluationType.UnshiftedTemperature));
		}

		[TestMethod]
		public void GivenTemperatureAboveBoundary_ReturnInfinity()
		{
			DecayWidthAverager averager =
				new DecayWidthAverager(TemperatureDecayWidthList, 1, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(double.PositiveInfinity, averager.GetDecayWidth(60000, 0, DecayWidthEvaluationType.UnshiftedTemperature));
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayEmptyException))]
		public void ThrowIf_EmptyTemperatureDecayWidthList()
		{
			DecayWidthAverager averager = new DecayWidthAverager(
				new List<KeyValuePair<double, double>>(), 1, QGPFormationTemperature);
		}

		[TestMethod]
		public void GivenSingleValue_ReturnOnlyOneValue()
		{
			List<KeyValuePair<double, double>> singleEntryList
				= new List<KeyValuePair<double, double>>();
			singleEntryList.Add(new KeyValuePair<double, double>(500, 650));
			DecayWidthAverager averager
				= new DecayWidthAverager(singleEntryList, 1, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(0, averager.GetDecayWidth(100, 0.2, DecayWidthEvaluationType.UnshiftedTemperature));
			AssertHelper.AssertApproximatelyEqual(650, averager.GetDecayWidth(500, 0.2, DecayWidthEvaluationType.UnshiftedTemperature));
			AssertHelper.AssertApproximatelyEqual(double.PositiveInfinity, averager.GetDecayWidth(999, 0.2, DecayWidthEvaluationType.UnshiftedTemperature));
		}

		[TestMethod]
		public void GivenZeroVelocity_SimpleInterpolation()
		{
			DecayWidthAverager averager =
				new DecayWidthAverager(TemperatureDecayWidthList, 1, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(InterpolatedDecayWidth.GetValue(160), averager.GetDecayWidth(160, 0, DecayWidthEvaluationType.AveragedTemperature));
		}

		[TestMethod]
		public void GetDecayWidth_UnshiftedTemperature()
		{
			DecayWidthAverager averager =
				new DecayWidthAverager(TemperatureDecayWidthList, 1, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(0,
				averager.GetDecayWidth(150, 0.5, DecayWidthEvaluationType.UnshiftedTemperature));
			AssertHelper.AssertApproximatelyEqual(InterpolatedDecayWidth.GetValue(160),
				averager.GetDecayWidth(160, 0.2, DecayWidthEvaluationType.UnshiftedTemperature));
			AssertHelper.AssertApproximatelyEqual(InterpolatedDecayWidth.GetValue(160),
				averager.GetDecayWidth(160, 0.8, DecayWidthEvaluationType.UnshiftedTemperature));
		}

		[TestMethod]
		public void GetDecayWidth_MaximallyBlueshifted()
		{
			// maximally blueshifted temperature: T * sqrt(1-v*v)/(1-v)
			DecayWidthAverager averager =
				new DecayWidthAverager(TemperatureDecayWidthList, 1, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(0,
				averager.GetDecayWidth(150, 0.5, DecayWidthEvaluationType.MaximallyBlueshifted));
			AssertHelper.AssertApproximatelyEqual(511.022213331555,
				averager.GetDecayWidth(160, 0.7, DecayWidthEvaluationType.MaximallyBlueshifted));
			AssertHelper.AssertApproximatelyEqual(double.PositiveInfinity,
				averager.GetDecayWidth(250, 0.9, DecayWidthEvaluationType.MaximallyBlueshifted));
		}

		[TestMethod]
		public void GetDecayWidth_AveragedTemperature()
		{
			// averaged temperature: T * sqrt(1-v*v) * artanh(v)/v
			AssertHelper.AssertApproximatelyEqual(141.571763304332,
				DecayWidthAverager.GetAveragedTemperature(160, 0.7));

			DecayWidthAverager averager =
				new DecayWidthAverager(TemperatureDecayWidthList, 1, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(0,
				averager.GetDecayWidth(150, 0.5, DecayWidthEvaluationType.AveragedTemperature));
			AssertHelper.AssertApproximatelyEqual(241.571763304332,
				averager.GetDecayWidth(160, 0.7, DecayWidthEvaluationType.AveragedTemperature));
		}

		[TestMethod]
		public void GetDecayWidth_AveragedDecayWidth()
		{
			DecayWidthAverager averager =
				new DecayWidthAverager(TemperatureDecayWidthList, 5, QGPFormationTemperature);

			AssertHelper.AssertApproximatelyEqual(0,
				averager.GetDecayWidth(150, 0.5, DecayWidthEvaluationType.AveragedDecayWidth));
			AssertHelper.AssertApproximatelyEqual(179.847779045952,
				averager.GetDecayWidth(160, 0.7, DecayWidthEvaluationType.AveragedDecayWidth));
			AssertHelper.AssertApproximatelyEqual(double.PositiveInfinity,
				averager.GetDecayWidth(250, 0.9, DecayWidthEvaluationType.AveragedDecayWidth));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static List<KeyValuePair<double, double>> TemperatureDecayWidthList;

		private static double[] Temperatures;

		private static double[] DecayWidths;

		private static LinearInterpolation1D InterpolatedDecayWidth;

		private static readonly double QGPFormationTemperature = 160;
	}
}