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

			Workers.SingleQQ singleQQ = new Workers.SingleQQ
			{
				VariableNameValuePairs = PlotterUITool.ControlsValues
			};

			AssertHelper.AssertFirstContainsSecond(
				singleQQ.VariableNameValuePairs, GetConvertedAlphaNameValuePairs());
		}

		[TestMethod]
		public void TakePionGDFParamsFromSingleQQPanel()
		{
			PlotterUITool.CreatePionGDFPlotParamForm(GetSingleQQPanelPionGDFParams());

			Workers.SingleQQ singleQQ = new Workers.SingleQQ
			{
				VariableNameValuePairs = PlotterUITool.ControlsValues
			};

			AssertHelper.AssertFirstContainsSecond(
				singleQQ.VariableNameValuePairs, GetConvertedPionGDFNameValuePairs());
		}

		[TestMethod]
		public void TakePotentialParamsFromSingleQQPanel()
		{
			PlotterUITool.CreatePotentialPlotParamForm(GetSingleQQPanelPotentialParams());

			Workers.SingleQQ singleQQ = new Workers.SingleQQ
			{
				VariableNameValuePairs = PlotterUITool.ControlsValues
			};

			AssertHelper.AssertFirstContainsSecond(
				singleQQ.VariableNameValuePairs, GetConvertedPotentialNameValuePairs());
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static Dictionary<string, string> GetSingleQQPanelAlphaParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				{ "DataFileName", "stdout.txt" },
				{ "EnergySteps", "1000" },
				{ "Energy_MeV", "-440" },
				{ "MaxEnergy_MeV", "10000" },
				{ "RunningCouplingType", "LOperturbative_Cutoff3" }
			};

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedAlphaNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				{ "AlphaSoft", "0" },
				{ "ColorState", "Singlet" },
				{ "DataFileName", "stdout.txt" },
				{ "DebyeMass_MeV", "0" },
				{ "EnergyScale_MeV", "-440" },
				{ "MaxEnergy_MeV", "10000" },
				{ "MaxRadius_fm", "0" },
				{ "MinEnergy_MeV", "-440" },
				{ "MinRadius_fm", "0" },
				{ "PotentialType", "0" },
				{ "RunningCouplingTypeSelection", "LOperturbative_Cutoff3" },
				{ "Samples", "1000" },
				{ "Sigma_MeV2", "0" },
				{ "SpinCouplingRange_fm", "0" },
				{ "SpinCouplingStrength_MeV", "0" },
				{ "SpinState", "Singlet" },
				{ "Temperature_MeV", "0" }
			};

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetSingleQQPanelPionGDFParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				{ "DataFileName", "stdout.txt" },
				{ "EnergySteps", "1000" },
				{ "Energy_MeV", "900" }
			};

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedPionGDFNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				{ "ColorState", "Singlet" },
				{ "DataFileName", "stdout.txt" },
				{ "DebyeMass_MeV", "0" },
				{ "EnergyScale_MeV", "900" },
				{ "MaxEnergy_MeV", "0" },
				{ "MaxRadius_fm", "0" },
				{ "MinEnergy_MeV", "900" },
				{ "MinRadius_fm", "0" },
				{ "PotentialType", "0" },
				{ "RunningCouplingTypeSelection", "" },
				{ "Samples", "1000" },
				{ "Sigma_MeV2", "0" },
				{ "SpinCouplingRange_fm", "0" },
				{ "SpinCouplingStrength_MeV", "0" },
				{ "SpinState", "Singlet" },
				{ "Temperature_MeV", "0" }
			};

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetSingleQQPanelPotentialParams()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				{ "AlphaSoft", "0.5" },
				{ "ColorState", "Octet" },
				{ "DataFileName", "stdout.txt" },
				{ "DebyeMass_MeV", "550" },
				{ "EnergySteps", "1000" },
				{ "MaxRadius_fm", "10" },
				{ "MinRadius_fm", "0" },
				{ "PotentialType", "Complex" },
				{ "Sigma_MeV2", "192000" },
				{ "SpinCouplingRange_fm", "0.1" },
				{ "SpinCouplingStrength_MeV", "15" },
				{ "SpinState", "Triplet" },
				{ "Temperature_MeV", "3333" }
			};

			return nameValuePairs;
		}

		private static Dictionary<string, string> GetConvertedPotentialNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				{ "AlphaSoft", "0.5" },
				{ "ColorState", "Octet" },
				{ "DataFileName", "stdout.txt" },
				{ "DebyeMass_MeV", "550" },
				{ "EnergyScale_MeV", "0" },
				{ "MaxEnergy_MeV", "0" },
				{ "MaxRadius_fm", "10" },
				{ "MinEnergy_MeV", "0" },
				{ "MinRadius_fm", "0" },
				{ "PotentialType", "Complex" },
				{ "RunningCouplingTypeSelection", "" },
				{ "Samples", "1000" },
				{ "Sigma_MeV2", "1.92E+05" },
				{ "SpinCouplingRange_fm", "0.1" },
				{ "SpinCouplingStrength_MeV", "15" },
				{ "SpinState", "Triplet" },
				{ "Temperature_MeV", "3333" }
			};

			return nameValuePairs;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private PlotterUITool PlotterUITool;

		private BackgroundService BackgroundService;
	}
}
