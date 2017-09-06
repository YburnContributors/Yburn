using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Yburn.TestUtil;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class SingleQQPlottingTests
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public SingleQQPlottingTests()
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
		public void PlotAlpha()
		{
			SingleQQ singleQQ = new SingleQQ();
			singleQQ.VariableNameValuePairs = GetAlphaPlotParams();

			WaitForGnuplotThenKillIt(singleQQ.PlotAlpha());
		}

		[TestMethod]
		public void PlotPionGDF()
		{
			SingleQQ singleQQ = new SingleQQ();
			singleQQ.VariableNameValuePairs = GetPionGDFPlotParams();

			WaitForGnuplotThenKillIt(singleQQ.PlotPionGDF());
		}

		[TestMethod]
		public void PlotComplexPotential()
		{
			SingleQQ singleQQ = new SingleQQ();
			singleQQ.VariableNameValuePairs = GetComplexPotentialPlotParams();

			WaitForGnuplotThenKillIt(singleQQ.PlotQQPotential());
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

		private Dictionary<string, string> GetAlphaPlotParams()
		{
			Dictionary<string, string> paramList = new Dictionary<string, string>();
			paramList["RunningCouplingTypeSelection"]
				= "LOperturbative_Cutoff1 NonPerturbative_ITP";
			paramList["MinEnergy_MeV"] = "1";
			paramList["MaxEnergy_MeV"] = "30000";
			paramList["Samples"] = "500";
			paramList["DataFileName"] = "PlotAlphaTest.txt";

			MarkFilesForDelete(paramList);

			return paramList;
		}

		private Dictionary<string, string> GetPionGDFPlotParams()
		{
			Dictionary<string, string> paramList = new Dictionary<string, string>();
			paramList["Samples"] = "500";
			paramList["EnergyScale_MeV"] = "1000";
			paramList["DataFileName"] = "PlotPionGDFTest.txt";

			MarkFilesForDelete(paramList);

			return paramList;
		}

		private Dictionary<string, string> GetComplexPotentialPlotParams()
		{
			Dictionary<string, string> paramList = new Dictionary<string, string>();
			paramList["Samples"] = "500";
			paramList["DataFileName"] = "PlotPotentialTest.txt";
			paramList["PotentialType"] = "Complex";
			paramList["MinRadius_fm"] = "0";
			paramList["MaxRadius_fm"] = "10";
			paramList["Sigma_MeV2"] = "192000";
			paramList["AlphaSoft"] = "0.5";
			paramList["Temperature_MeV"] = "200";
			paramList["DebyeMass_MeV"] = "250";

			MarkFilesForDelete(paramList);

			return paramList;
		}
	}
}
