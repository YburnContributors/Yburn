using Microsoft.VisualStudio.TestTools.UnitTesting;
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
				  "#UnshiftedTemperature\r\n"
				+ "#MediumTemperature  MediumVelocity      DecayWidth(Y1S)     \r\n"
				+ "#(MeV)              (c)                 (MeV)               \r\n"
				+ "#\r\n"
				+ "0                   0                   0                   \r\n"
				+ "120                 0                   0                   \r\n"
				+ "240                 0                   360                 \r\n"
				+ "360                 0                   540                 \r\n"
				+ "480                 0                   720                 \r\n"
				+ "600                 0                   Infinity            \r\n\r\n\r\n");
		}

		[TestMethod]
		public void GivenManyStates_PrintList()
		{
			Printer = new TemperatureDecayWidthPrinterTests(
				GetBottomiumStatesList(BottomiumState.Y1S, BottomiumState.Y2S, BottomiumState.Y3S));

			AssertReturnsList(
				  "#UnshiftedTemperature\r\n"
				+ "#MediumTemperature  MediumVelocity      DecayWidth(Y1S)     DecayWidth(Y2S)     DecayWidth(Y3S)     \r\n"
				+ "#(MeV)              (c)                 (MeV)               (MeV)               (MeV)               \r\n"
				+ "#\r\n"
				+ "0                   0                   0                   0                   0                   \r\n"
				+ "120                 0                   0                   0                   0                   \r\n"
				+ "240                 0                   360                 720                 1080                \r\n"
				+ "360                 0                   540                 1080                1620                \r\n"
				+ "480                 0                   720                 1440                2160                \r\n"
				+ "600                 0                   Infinity            Infinity            Infinity            \r\n\r\n\r\n");
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
				new List<double> { 0, 120, 240, 360, 480, 600 },
				new List<double> { 0 },
				new List<DecayWidthEvaluationType> { DecayWidthEvaluationType.UnshiftedTemperature }));
		}
	}
}