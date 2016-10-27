using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yburn.Fireball;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class TemperatureDecayWidthPrinterTests : TemperatureDecayWidthPrinter
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public TemperatureDecayWidthPrinterTests()
			: base("", GetBottomiumStateArray(), 0, null, 1, 160)
		{
		}

		public TemperatureDecayWidthPrinterTests(
			string dataPathFile,
			BottomiumState[] bottomiumStates,
			DecayWidthType decayWidthType,
			string[] potentialTypes,
			int numberAveragingAngles,
			double qgpFormationTemperature
			)
			: base(dataPathFile, bottomiumStates, decayWidthType, potentialTypes,
				  numberAveragingAngles, qgpFormationTemperature)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void GivenOneState_PrintList()
		{
			Printer = new TemperatureDecayWidthPrinterTests("",
				GetBottomiumStateArray(BottomiumState.Y1S),
				DecayWidthType.None, potentialTypes: null,
				numberAveragingAngles: 1,
				qgpFormationTemperature: 160);

			AssertReturnsList(
				  "#UnshiftedTemperature\r\n"
				+ "#MediumTemperature  MediumVelocity      DecayWidth(Y1S)     \r\n"
				+ "#(MeV)              (c)                 (MeV)               \r\n"
				+ "#\r\n"
				+ "80                  0                   0                   \r\n"
				+ "120                 0                   0                   \r\n"
				+ "160                 0                   350                 \r\n"
				+ "200                 0                   550                 \r\n"
				+ "240                 0                   Infinity            \r\n\r\n");
		}

		[TestMethod]
		public void GivenManyStates_PrintList()
		{
			Printer = new TemperatureDecayWidthPrinterTests("",
				GetBottomiumStateArray(BottomiumState.Y1S, BottomiumState.x2P, BottomiumState.Y3S),
				DecayWidthType.None, potentialTypes: null,
				numberAveragingAngles: 1,
				qgpFormationTemperature: 160);

			AssertReturnsList(
				  "#UnshiftedTemperature\r\n"
				+ "#MediumTemperature  MediumVelocity      DecayWidth(Y1S)     DecayWidth(x2P)     DecayWidth(Y3S)     \r\n"
				+ "#(MeV)              (c)                 (MeV)               (MeV)               (MeV)               \r\n"
				+ "#\r\n"
				+ "80                  0                   0                   0                   0                   \r\n"
				+ "120                 0                   0                   0                   0                   \r\n"
				+ "160                 0                   350                 700                 1050                \r\n"
				+ "200                 0                   550                 1100                1650                \r\n"
				+ "240                 0                   Infinity            Infinity            Infinity            \r\n\r\n");
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static BottomiumState[] GetBottomiumStateArray(params BottomiumState[] states)
		{
			return states;
		}

		private static List<KeyValuePair<double, double>> GetTemperatureDecayWidthValues(
			BottomiumState state
			)
		{
			List<KeyValuePair<double, double>> list = new List<KeyValuePair<double, double>>();
			switch(state)
			{
				case BottomiumState.Y1S:
					list.Add(new KeyValuePair<double, double>(100, 200));
					list.Add(new KeyValuePair<double, double>(150, 300));
					list.Add(new KeyValuePair<double, double>(220, 650));
					break;

				case BottomiumState.x2P:
					list.Add(new KeyValuePair<double, double>(100, 400));
					list.Add(new KeyValuePair<double, double>(150, 600));
					list.Add(new KeyValuePair<double, double>(220, 1300));
					break;

				case BottomiumState.Y3S:
					list.Add(new KeyValuePair<double, double>(100, 600));
					list.Add(new KeyValuePair<double, double>(150, 900));
					list.Add(new KeyValuePair<double, double>(220, 1950));
					break;

				default:
					break;
			}

			return list;
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
				new double[] { 80, 120, 160, 200, 240 },
				new double[] { 0 },
				new DecayWidthEvaluationType[] { DecayWidthEvaluationType.UnshiftedTemperature }));
		}

		protected override DecayWidthAverager CreateDecayWidthAverager(
			BottomiumState state
			)
		{
			return new DecayWidthAverager(GetTemperatureDecayWidthValues(state), 1, 160);
		}
	}
}