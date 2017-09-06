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
			nameValuePairs.Add("MaxEnergy_MeV", "10000");
			nameValuePairs.Add("Energy_MeV", "-440");
			nameValuePairs.Add("EnergySteps", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");
			nameValuePairs.Add("RunningCouplingType", "LOperturbative_Cutoff3");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedAlphaNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			nameValuePairs.Add("MinEnergy_MeV", "-440");
			nameValuePairs.Add("MaxEnergy_MeV", "10000");
			nameValuePairs.Add("Samples", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");
			nameValuePairs.Add("RunningCouplingTypeSelection", "LOperturbative_Cutoff3");
			nameValuePairs.Add("EnergyScale_MeV", "-440");

			nameValuePairs.Add("PotentialType", "0");
			nameValuePairs.Add("AlphaSoft", "0");
			nameValuePairs.Add("Sigma_MeV2", "0");
			nameValuePairs.Add("ColorState", "Singlet");
			nameValuePairs.Add("Temperature_MeV", "0");
			nameValuePairs.Add("DebyeMass_MeV", "0");
			nameValuePairs.Add("SpinState", "Singlet");
			nameValuePairs.Add("SpinCouplingRange_fm", "0");
			nameValuePairs.Add("SpinCouplingStrength_MeV", "0");
			nameValuePairs.Add("MinRadius_fm", "0");
			nameValuePairs.Add("MaxRadius_fm", "0");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetSingleQQPanelPionGDFParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("Energy_MeV", "900");
			nameValuePairs.Add("EnergySteps", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedPionGDFNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("MinEnergy_MeV", "900");
			nameValuePairs.Add("MaxEnergy_MeV", "0");
			nameValuePairs.Add("Samples", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");
			nameValuePairs.Add("EnergyScale_MeV", "900");

			nameValuePairs.Add("PotentialType", "0");
			nameValuePairs.Add("AlphaSoft", "0");
			nameValuePairs.Add("Sigma_MeV2", "0");
			nameValuePairs.Add("ColorState", "Singlet");
			nameValuePairs.Add("Temperature_MeV", "0");
			nameValuePairs.Add("DebyeMass_MeV", "0");
			nameValuePairs.Add("RunningCouplingTypeSelection", "");
			nameValuePairs.Add("SpinState", "Singlet");
			nameValuePairs.Add("SpinCouplingRange_fm", "0");
			nameValuePairs.Add("SpinCouplingStrength_MeV", "0");
			nameValuePairs.Add("MinRadius_fm", "0");
			nameValuePairs.Add("MaxRadius_fm", "0");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetSingleQQPanelPotentialParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("PotentialType", "Complex");
			nameValuePairs.Add("AlphaSoft", "0.5");
			nameValuePairs.Add("Sigma_MeV2", "192000");
			nameValuePairs.Add("ColorState", "Octet");
			nameValuePairs.Add("Temperature_MeV", "3333");
			nameValuePairs.Add("DebyeMass_MeV", "550");
			nameValuePairs.Add("SpinState", "Triplet");
			nameValuePairs.Add("SpinCouplingRange_fm", "0.1");
			nameValuePairs.Add("SpinCouplingStrength_MeV", "15");
			nameValuePairs.Add("MinRadius_fm", "0");
			nameValuePairs.Add("MaxRadius_fm", "10");
			nameValuePairs.Add("EnergySteps", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedPotentialNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("PotentialType", "Complex");
			nameValuePairs.Add("AlphaSoft", "0.5");
			nameValuePairs.Add("Sigma_MeV2", "1.92E+05");
			nameValuePairs.Add("ColorState", "Octet");
			nameValuePairs.Add("Temperature_MeV", "3333");
			nameValuePairs.Add("DebyeMass_MeV", "550");
			nameValuePairs.Add("SpinState", "Triplet");
			nameValuePairs.Add("SpinCouplingRange_fm", "0.1");
			nameValuePairs.Add("SpinCouplingStrength_MeV", "15");
			nameValuePairs.Add("MinRadius_fm", "0");
			nameValuePairs.Add("MaxRadius_fm", "10");
			nameValuePairs.Add("Samples", "1000");
			nameValuePairs.Add("DataFileName", "stdout.txt");

			nameValuePairs.Add("MinEnergy_MeV", "0");
			nameValuePairs.Add("MaxEnergy_MeV", "0");
			nameValuePairs.Add("RunningCouplingTypeSelection", "");
			nameValuePairs.Add("EnergyScale_MeV", "0");

			return nameValuePairs;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private PlotterUITool PlotterUITool;

		private BackgroundService BackgroundService;
	}
}
