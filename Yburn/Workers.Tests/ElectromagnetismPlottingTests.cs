using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Yburn.TestUtil;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class ElectromagnetismPlottingTests
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ElectromagnetismPlottingTests()
		{
			FileCleaner = new FileCleaner();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestCleanup]
		public void CleanTestFiles()
		{
			FileCleaner.Clean();
		}

		[TestMethod]
		public void PlotPointChargeAzimutalMagneticField()
		{
			Electromagnetism electromagnetism = new Electromagnetism();
			electromagnetism.VariableNameValuePairs = GetPointChargeFieldPlotParams();

			WaitForGnuplotThenKillIt(electromagnetism.PlotPointChargeAzimutalMagneticField());
		}

		[TestMethod]
		public void PlotPointChargeLongitudinalElectricField()
		{
			Electromagnetism electromagnetism = new Electromagnetism();
			electromagnetism.VariableNameValuePairs = GetPointChargeFieldPlotParams();

			WaitForGnuplotThenKillIt(electromagnetism.PlotPointChargeLongitudinalElectricField());
		}

		[TestMethod]
		public void PlotPointChargeRadialElectricField()
		{
			Electromagnetism electromagnetism = new Electromagnetism();
			electromagnetism.VariableNameValuePairs = GetPointChargeFieldPlotParams();

			WaitForGnuplotThenKillIt(electromagnetism.PlotPointChargeRadialElectricField());
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void WaitForGnuplotThenKillIt(
			Process process
			)
		{
			Thread.Sleep(350);

			process.Kill();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FileCleaner FileCleaner;

		private void MarkFilesForDelete(
			Dictionary<string, string> nameValuePairs
			)
		{
			string dataPathFile = YburnConfigFile.OutputPath + nameValuePairs["DataFileName"];
			string dataPlotPathFile = dataPathFile + ".plt";

			FileCleaner.MarkForDelete(dataPathFile);
			FileCleaner.MarkForDelete(dataPlotPathFile);
		}

		private Dictionary<string, string> GetPointChargeFieldPlotParams()
		{
			Dictionary<string, string> paramList = new Dictionary<string, string>();
			paramList["PointChargeRapidity"] = "5.3";
			paramList["RadialDistance"] = "7.4";
			paramList["StartEffectiveTime"] = "0.0";
			paramList["StopEffectiveTime"] = "10.0";
			paramList["EffectiveTimeSamples"] = "1000";
			paramList["EMFCalculationMethodSelection"] = "URLimitFourierSynthesis,DiffusionApproximation,FreeSpace";
			paramList["DataFileName"] = "PlotPointChargeFieldTest.txt";

			MarkFilesForDelete(paramList);

			return paramList;
		}
	}
}