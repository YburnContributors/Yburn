using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Yburn.Tests.Util;

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
			DecayWidthAverager averager = new DecayWidthAverager(TemperatureDecayWidthList);

			AssertHelper.AssertRoundedEqual(0, averager.GetDecayWidth(60, 0));
		}

		[TestMethod]
		public void GivenTemperatureAboveBoundary_ReturnInfinity()
		{
			DecayWidthAverager averager = new DecayWidthAverager(TemperatureDecayWidthList);

			AssertHelper.AssertRoundedEqual(double.PositiveInfinity, averager.GetDecayWidth(60000, 0));
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayEmptyException))]
		public void ThrowIf_EmptyTemperatureDecayWidthList()
		{
			DecayWidthAverager averager = new DecayWidthAverager(
				new List<KeyValuePair<double, double>>());
		}

		[TestMethod]
		public void GivenSingleValue_ReturnOnlyOneValue()
		{
			List<KeyValuePair<double, double>> singleEntryList
				= new List<KeyValuePair<double, double>>();
			singleEntryList.Add(new KeyValuePair<double, double>(500, 650));
			DecayWidthAverager averager = new DecayWidthAverager(singleEntryList);

			AssertHelper.AssertRoundedEqual(0, averager.GetDecayWidth(100, 0.2));
			AssertHelper.AssertRoundedEqual(650, averager.GetDecayWidth(500, 0.2));
			AssertHelper.AssertRoundedEqual(double.PositiveInfinity, averager.GetDecayWidth(999, 0.2));
		}

		[TestMethod]
		public void GivenNoAngle_SimpleInterpolation()
		{
			DecayWidthAverager averager = new DecayWidthAverager(TemperatureDecayWidthList);

			AssertHelper.AssertRoundedEqual(InterpolatedDecayWidth.GetValue(160), averager.GetDecayWidth(160, 0.2));
			AssertHelper.AssertRoundedEqual(InterpolatedDecayWidth.GetValue(160), averager.GetDecayWidth(160, 0.8));
		}

		[TestMethod]
		public void GivenZeroVelocity_SimpleInterpolation()
		{
			DecayWidthAverager averager = new DecayWidthAverager(TemperatureDecayWidthList,
				new double[] { 0, Math.PI / 3, 2 * Math.PI / 3, Math.PI });

			AssertHelper.AssertRoundedEqual(InterpolatedDecayWidth.GetValue(160), averager.GetDecayWidth(160, 0));
		}

		[TestMethod]
		[ExpectedException(typeof(AveragingAnglesDisorderedException))]
		public void ThrowIfAnglesDisordered()
		{
			DecayWidthAverager averager = new DecayWidthAverager(TemperatureDecayWidthList,
				new double[] { 1, 3, 2 });
		}

		[TestMethod]
		public void GivenForwardAngle_MaximumBlueshift()
		{
			// effective temperature: T * sqrt(1-v*v)/(1-v)
			AssertHelper.AssertRoundedEqual(InterpolatedDecayWidth.GetValue(160), GetDecayWidth(160, 0, 0));
			AssertHelper.AssertRoundedEqual(InterpolatedDecayWidth.GetValue(244.404037064311), GetDecayWidth(160, 0.4, 0));
			AssertHelper.AssertRoundedEqual(double.PositiveInfinity, GetDecayWidth(160, 0.9, 0));
		}

		[TestMethod]
		public void GivenOneAngle_AngleDependentShift()
		{
			// effective temperature: T * sqrt(1-v*v)/(1-v*cos(theta))
			AssertHelper.AssertRoundedEqual(double.PositiveInfinity, GetDecayWidth(250, 0.9, 0));
			AssertHelper.AssertRoundedEqual(InterpolatedDecayWidth.GetValue(198.13177016094),
				GetDecayWidth(250, 0.9, 60));
			AssertHelper.AssertRoundedEqual(0, GetDecayWidth(250, 0.9, 180));
		}

		[TestMethod]
		public void GivenMultipleAngles_PerformSolidAngleAverage()
		{
			double[] angles = new double[] { 0, 60, 120, 180 };

			double decayWidth1 = GetDecayWidth(200, 0.5, angles[0]);
			double decayWidth2 = GetDecayWidth(200, 0.5, angles[1]);
			double decayWidth3 = GetDecayWidth(200, 0.5, angles[2]);
			double decayWidth4 = GetDecayWidth(200, 0.5, angles[3]);
			double averagedDecayWidth
				= 0.25 * (0.5 * decayWidth1 + 1.5 * decayWidth2 + 1.5 * decayWidth3 + 0.5 * decayWidth4);

			DecayWidthAverager averager = new DecayWidthAverager(TemperatureDecayWidthList, angles);

			AssertHelper.AssertRoundedEqual(averagedDecayWidth, averager.GetDecayWidth(200, 0.5));

			AssertHelper.AssertRoundedEqual(double.PositiveInfinity, averager.GetDecayWidth(250, 0.9));

			AssertHelper.AssertRoundedEqual(0.125 * GetDecayWidth(100, 0.9, angles[0]),
				averager.GetDecayWidth(100, 0.9));
		}

		[TestMethod]
		public void CalculateDecayWidthFromAveragedTemperature()
		{
			// averaged temperature: T * sqrt(1-v*v) * artanh(v)/v
			AssertHelper.AssertRoundedEqual(141.571763304332,
				DecayWidthAverager.GetAveragedTemperature(160, 0.7));

			DecayWidthAverager averager = new DecayWidthAverager(TemperatureDecayWidthList);

			AssertHelper.AssertRoundedEqual(241.571763304332,
				averager.GetDecayWidthUsingAveragedTemperature(160, 0.7));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static List<KeyValuePair<double, double>> TemperatureDecayWidthList;

		private static double[] Temperatures;

		private static double[] DecayWidths;

		private static LinearInterpolation1D InterpolatedDecayWidth;

		private static double GetDecayWidth(
			double temperature,
			double velocity,
			double angle
			)
		{
			DecayWidthAverager averager = new DecayWidthAverager(
				TemperatureDecayWidthList, new double[] { angle });
			return averager.GetDecayWidth(temperature, velocity);
		}
	}
}