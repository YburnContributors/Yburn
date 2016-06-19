using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yburn.SingleQQ.UI;
using Yburn.TestUtil;

namespace Yburn.UI.Tests
{
	[TestClass]
	public class PlotterUIToolTests
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public PlotterUIToolTests()
		{
			BackgroundService = new BackgroundService();
			BackgroundService.SetWorker(new Workers.SingleQQ());

			PlotterUITool = new PlotterUITool();
			PlotterUITool.SetJobOrganizer(BackgroundService);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void TakeAlphaParamsFromSingleQQPanel()
		{
			PlotterUITool.CreateAlphaPlotParamForm(GetSingleQQPanelAlphaParams());

			Workers.SingleQQ singleQQ = new Workers.SingleQQ();
			singleQQ.VariableNameValuePairs = PlotterUITool.ControlsValues;

			AssertHelper.AssertFirstContainsSecond(
				singleQQ.VariableNameValuePairs, GetConvertedAlphaNameValuePairs());
		}

		[TestMethod]
		public void TakePionGDFParamsFromSingleQQPanel()
		{
			PlotterUITool.CreatePionGDFPlotParamForm(GetSingleQQPanelPionGDFParams());

			Workers.SingleQQ singleQQ = new Workers.SingleQQ();
			singleQQ.VariableNameValuePairs = PlotterUITool.ControlsValues;

			AssertHelper.AssertFirstContainsSecond(
				singleQQ.VariableNameValuePairs, GetConvertedPionGDFNameValuePairs());
		}

		[TestMethod]
		public void TakePotentialParamsFromSingleQQPanel()
		{
			PlotterUITool.CreatePotentialPlotParamForm(GetSingleQQPanelPotentialParams());

			Workers.SingleQQ singleQQ = new Workers.SingleQQ();
			singleQQ.VariableNameValuePairs = PlotterUITool.ControlsValues;

			AssertHelper.AssertFirstContainsSecond(
				singleQQ.VariableNameValuePairs, GetConvertedPotentialNameValuePairs());
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static Dictionary<string, string> GetSingleQQPanelAlphaParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("MaxEnergy", "10000");
			nameValuePairs.Add("Energy", "-440");
			nameValuePairs.Add("EnergySteps", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");
			nameValuePairs.Add("RunningCouplingType", "LOperturbative_Cutoff3");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedAlphaNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			nameValuePairs.Add("MinEnergy", "-440");
			nameValuePairs.Add("MaxEnergy", "1E+04");
			nameValuePairs.Add("Samples", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");
			nameValuePairs.Add("RunningCouplingTypeSelection", "LOperturbative_Cutoff3");
			nameValuePairs.Add("EnergyScale", "-440");

			nameValuePairs.Add("PotentialType", "0");
			nameValuePairs.Add("AlphaSoft", "0");
			nameValuePairs.Add("Sigma", "0");
			nameValuePairs.Add("ColorState", "Singlet");
			nameValuePairs.Add("Temperature", "0");
			nameValuePairs.Add("DebyeMass", "0");
			nameValuePairs.Add("SpinState", "Singlet");
			nameValuePairs.Add("SpinCouplingRange", "0");
			nameValuePairs.Add("SpinCouplingStrength", "0");
			nameValuePairs.Add("MinRadius", "0");
			nameValuePairs.Add("MaxRadius", "0");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetSingleQQPanelPionGDFParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("Energy", "900");
			nameValuePairs.Add("EnergySteps", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedPionGDFNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("MinEnergy", "900");
			nameValuePairs.Add("MaxEnergy", "0");
			nameValuePairs.Add("Samples", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");
			nameValuePairs.Add("EnergyScale", "900");

			nameValuePairs.Add("PotentialType", "0");
			nameValuePairs.Add("AlphaSoft", "0");
			nameValuePairs.Add("Sigma", "0");
			nameValuePairs.Add("ColorState", "Singlet");
			nameValuePairs.Add("Temperature", "0");
			nameValuePairs.Add("DebyeMass", "0");
			nameValuePairs.Add("RunningCouplingTypeSelection", "");
			nameValuePairs.Add("SpinState", "Singlet");
			nameValuePairs.Add("SpinCouplingRange", "0");
			nameValuePairs.Add("SpinCouplingStrength", "0");
			nameValuePairs.Add("MinRadius", "0");
			nameValuePairs.Add("MaxRadius", "0");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetSingleQQPanelPotentialParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("PotentialType", "Complex");
			nameValuePairs.Add("AlphaSoft", "0.5");
			nameValuePairs.Add("Sigma", "192000");
			nameValuePairs.Add("ColorState", "Octet");
			nameValuePairs.Add("Temperature", "3333");
			nameValuePairs.Add("DebyeMass", "550");
			nameValuePairs.Add("SpinState", "Triplet");
			nameValuePairs.Add("SpinCouplingRange", "0.1");
			nameValuePairs.Add("SpinCouplingStrength", "15");
			nameValuePairs.Add("MinRadius", "0");
			nameValuePairs.Add("MaxRadius", "10");
			nameValuePairs.Add("EnergySteps", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedPotentialNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("PotentialType", "Complex");
			nameValuePairs.Add("AlphaSoft", "0.5");
			nameValuePairs.Add("Sigma", "1.92E+05");
			nameValuePairs.Add("ColorState", "Octet");
			nameValuePairs.Add("Temperature", "3333");
			nameValuePairs.Add("DebyeMass", "550");
			nameValuePairs.Add("SpinState", "Triplet");
			nameValuePairs.Add("SpinCouplingRange", "0.1");
			nameValuePairs.Add("SpinCouplingStrength", "15");
			nameValuePairs.Add("MinRadius", "0");
			nameValuePairs.Add("MaxRadius", "10");
			nameValuePairs.Add("Samples", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");

			nameValuePairs.Add("MinEnergy", "0");
			nameValuePairs.Add("MaxEnergy", "0");
			nameValuePairs.Add("RunningCouplingTypeSelection", "");
			nameValuePairs.Add("EnergyScale", "0");

			return nameValuePairs;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private PlotterUITool PlotterUITool;

		private BackgroundService BackgroundService;
	}
}