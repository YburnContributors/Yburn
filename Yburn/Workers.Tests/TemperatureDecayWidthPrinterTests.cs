using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.QQState;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class TemperatureDecayWidthPrinterTests : TemperatureDecayWidthPrinter
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public TemperatureDecayWidthPrinterTests()
			: base(null, null, null, 0, 0, 0)
		{
		}

		public TemperatureDecayWidthPrinterTests(
			List<BottomiumState> bottomiumStates
			)
			: base(
				  "..\\..\\bbdata-Test.txt",
				  bottomiumStates,
				  new List<PotentialType>() { PotentialType.Complex },
				  DecayWidthType.GammaTot,
				  160,
				  20
				  )
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void GivenOneState_PrintList()
		{
			Printer = new TemperatureDecayWidthPrinterTests(
				GetBottomiumStatesList(BottomiumState.Y1S));

			AssertReturnsList(
				  "#UnshiftedTemperature" + Environment.NewLine
				+ "#MediumTemperature  MediumVelocity      DecayWidth(Y1S)     " + Environment.NewLine
				+ "#(MeV)              (c)                 (MeV)               " + Environment.NewLine
				+ "#" + Environment.NewLine
				+ "0                   0                   0                   " + Environment.NewLine
				+ "120                 0                   0                   " + Environment.NewLine
				+ "240                 0                   360                 " + Environment.NewLine
				+ "360                 0                   540                 " + Environment.NewLine
				+ "480                 0                   720                 " + Environment.NewLine
				+ "600                 0                   Infinity            " + Environment.NewLine
				+ Environment.NewLine + Environment.NewLine);
		}

		[TestMethod]
		public void GivenManyStates_PrintList()
		{
			Printer = new TemperatureDecayWidthPrinterTests(
				GetBottomiumStatesList(BottomiumState.Y1S, BottomiumState.Y2S, BottomiumState.Y3S));

			AssertReturnsList(
				  "#UnshiftedTemperature" + Environment.NewLine
				+ "#MediumTemperature  MediumVelocity      DecayWidth(Y1S)     DecayWidth(Y2S)     DecayWidth(Y3S)     " + Environment.NewLine
				+ "#(MeV)              (c)                 (MeV)               (MeV)               (MeV)               " + Environment.NewLine
				+ "#" + Environment.NewLine
				+ "0                   0                   0                   0                   0                   " + Environment.NewLine
				+ "120                 0                   0                   0                   0                   " + Environment.NewLine
				+ "240                 0                   360                 720                 1080                " + Environment.NewLine
				+ "360                 0                   540                 1080                1620                " + Environment.NewLine
				+ "480                 0                   720                 1440                2160                " + Environment.NewLine
				+ "600                 0                   Infinity            Infinity            Infinity            " + Environment.NewLine
				+ Environment.NewLine + Environment.NewLine);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static List<BottomiumState> GetBottomiumStatesList(params BottomiumState[] states)
		{
			return new List<BottomiumState>(states);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private TemperatureDecayWidthPrinter Printer;

		private void AssertReturnsList(
			string expectedList
			)
		{
			Assert.AreEqual(expectedList, Printer.GetList(
				new List<DopplerShiftEvaluationType> { DopplerShiftEvaluationType.UnshiftedTemperature },
				ElectricDipoleAlignment.Random,
				new List<double> { 0, 120, 240, 360, 480, 600 }, new List<double> { 0 },
				0, 0));
		}
	}
}
