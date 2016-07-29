using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            : base("", GetArray(), 0, null, 0, 0, 1, 0, EmptyAngleArray)
        {
        }

        public TemperatureDecayWidthPrinterTests(
            string dataPathFile,
            BottomiumState[] bottomiumStates,
            DecayWidthType decayWidthType,
            string[] potentialTypes,
            double minTemperature,
            double maxTemperature,
            double stepSize,
            double mediumVelocity,
            double[] averagingAngles
            )
            : base(dataPathFile, bottomiumStates, decayWidthType, potentialTypes,
                minTemperature, maxTemperature, stepSize, mediumVelocity, averagingAngles)
        {
        }

        /********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowIf_ZeroOrNegativeStepSize()
        {
            Printer = new TemperatureDecayWidthPrinter("", GetArray(BottomiumState.Y1S),
                DecayWidthType.None, potentialTypes: null,
                minTemperature: 0, maxTemperature: 10, stepSize: 0,
                mediumVelocity: 0,
                averagingAngles: EmptyAngleArray);
        }

        [TestMethod]
        public void GivenOneState_PrintList()
        {
            Printer = new TemperatureDecayWidthPrinterTests("",
                GetArray(BottomiumState.Y1S),
                DecayWidthType.None, potentialTypes: null,
                minTemperature: 80,
                maxTemperature: 240,
                stepSize: 40,
                mediumVelocity: 0,
                averagingAngles: EmptyAngleArray);

            AssertReturnsList(
                  "#Temperature        DecayWidth(Y1S)     \r\n"
                + "#(MeV)              (MeV)               \r\n"
                + "#\r\n"
                + "80                  0                   \r\n"
                + "120                 240                 \r\n"
                + "160                 350                 \r\n"
                + "200                 550                 \r\n"
                + "240                 Infinity            \r\n\r\n");
        }

        [TestMethod]
        public void GivenManyStates_PrintList()
        {
            Printer = new TemperatureDecayWidthPrinterTests("",
                GetArray(BottomiumState.Y1S, BottomiumState.x2P, BottomiumState.Y3S),
                DecayWidthType.None, potentialTypes: null,
                minTemperature: 80,
                maxTemperature: 240,
                stepSize: 40,
                mediumVelocity: 0,
                averagingAngles: EmptyAngleArray);

            AssertReturnsList(
                  "#Temperature        DecayWidth(Y1S)     DecayWidth(x2P)     DecayWidth(Y3S)     \r\n"
                + "#(MeV)              (MeV)               (MeV)               (MeV)               \r\n"
                + "#\r\n"
                + "80                  0                   0                   0                   \r\n"
                + "120                 240                 480                 720                 \r\n"
                + "160                 350                 700                 1050                \r\n"
                + "200                 550                 1100                1650                \r\n"
                + "240                 Infinity            Infinity            Infinity            \r\n\r\n");
        }

        [TestMethod]
        public void GivenManyStates_UsingAveragedTemperature_PrintList()
        {
            Printer = new TemperatureDecayWidthPrinterTests("",
                GetArray(BottomiumState.Y1S, BottomiumState.x2P, BottomiumState.Y3S),
                DecayWidthType.None, potentialTypes: null,
                minTemperature: 80,
                maxTemperature: 240,
                stepSize: 40,
                mediumVelocity: 0,
                averagingAngles: EmptyAngleArray);

            AssertReturnsListUsingAveragedTemperature(
                  "#Temperature        DecayWidth(Y1S)     DecayWidth(x2P)     DecayWidth(Y3S)     \r\n"
                + "#(MeV)              (MeV)               (MeV)               (MeV)               \r\n"
                + "#\r\n"
                + "80                  0                   0                   0                   \r\n"
                + "120                 240                 480                 720                 \r\n"
                + "160                 350                 700                 1050                \r\n"
                + "200                 550                 1100                1650                \r\n"
                + "240                 Infinity            Infinity            Infinity            \r\n\r\n\r\n"
                + "#Eff. Temperature   DecayWidth(Y1S)     DecayWidth(x2P)     DecayWidth(Y3S)     \r\n"
                + "#(MeV)              (MeV)               (MeV)               (MeV)               \r\n"
                + "#\r\n"
                + "80                  0                   0                   0                   \r\n"
                + "120                 240                 480                 720                 \r\n"
                + "160                 350                 700                 1050                \r\n"
                + "200                 550                 1100                1650                \r\n"
                + "240                 Infinity            Infinity            Infinity            \r\n\r\n");
        }

        /********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

        private static BottomiumState[] GetArray(params BottomiumState[] states)
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

        private static readonly double[] EmptyAngleArray = new double[] { };

        /********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

        private TemperatureDecayWidthPrinter Printer;

        private void AssertReturnsList(
            string expectedList
            )
        {
            Assert.AreEqual(expectedList, Printer.GetList());
        }

        private void AssertReturnsListUsingAveragedTemperature(
            string expectedList
            )
        {
            Assert.AreEqual(expectedList, Printer.GetListUsingAveragedTemperature());
        }

        protected override DecayWidthAverager CreateDecayWidthAverager(
            BottomiumState state
            )
        {
            return new DecayWidthAverager(GetTemperatureDecayWidthValues(state));
        }
    }
}