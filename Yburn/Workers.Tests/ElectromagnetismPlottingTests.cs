﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
		public void PlotPointChargeAzimuthalMagneticField()
		{
			Electromagnetism electromagnetism = new Electromagnetism();
			electromagnetism.VariableNameValuePairs = GetPointChargeFieldPlotParams();

			WaitForGnuplotThenKillIt(electromagnetism.PlotPointChargeAzimuthalMagneticField());
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
			paramList["ParticleRapidity"] = "5.3";
			paramList["RadialDistance"] = "7.4";
			paramList["Samples"] = "1000";
			paramList["StartTime"] = "0.0";
			paramList["StopTime"] = "10.0";
			paramList["DataFileName"] = "PlotPointChargeFieldTest.txt";

			MarkFilesForDelete(paramList);

			return paramList;
		}
	}
}