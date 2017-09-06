/**************************************************************************************************
 * Manages controls for the plotting dialogs.
 **************************************************************************************************/

using System.Collections.Generic;
using Yburn.Interfaces;
using Yburn.UI;

namespace Yburn.SingleQQ.UI
{
	public class PlotterUITool
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public PlotterUITool()
		{
			DataFileName = string.Empty;
			RunningCouplingTypeSelection = string.Empty;
			MaxEnergy = string.Empty;
			MinEnergy = string.Empty;
			Samples = string.Empty;
			EnergyScale = string.Empty;
			BottomiumStates = string.Empty;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void SetJobOrganizer(
			JobOrganizer jobOrganizer
			)
		{
			JobOrganizer = jobOrganizer;

			ColorStates = JobOrganizer.GetWorkerEnumEntries("ColorState");
			PotentialTypes = JobOrganizer.GetWorkerEnumEntries("PotentialType");
			RunningCouplingTypes = JobOrganizer.GetWorkerEnumEntries("RunningCouplingType");
			SpinStates = JobOrganizer.GetWorkerEnumEntries("SpinState");
		}

		public Dictionary<string, string> ControlsValues
		{
			get
			{
				return GetControlsValues();
			}
			set
			{
				SetControlsValues(value);
			}
		}

		public AlphaPlotParamForm CreateAlphaPlotParamForm(
			Dictionary<string, string> singleQQParams
			)
		{
			SetControlsValuesFromSingleQQParams(singleQQParams);
			return CreateAlphaPlotParamForm();
		}

		public PionGDFPlotParamForm CreatePionGDFPlotParamForm(
			Dictionary<string, string> singleQQParams
			)
		{
			SetControlsValuesFromSingleQQParams(singleQQParams);
			return CreatePionGDFPlotParamForm();
		}

		public PotentialPlotParamForm CreatePotentialPlotParamForm(
			Dictionary<string, string> singleQQParams
			)
		{
			SetControlsValuesFromSingleQQParams(singleQQParams);
			return CreatePotentialPlotParamForm();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string TryGetString(
			Dictionary<string, string> nameValuePairs,
			string key,
			string defaultIfNull
			)
		{
			string value;
			nameValuePairs.TryGetValue(key, out value);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private JobOrganizer JobOrganizer;

		private string DataFileName;

		private string RunningCouplingTypeSelection;

		private string MaxEnergy;

		private string MinEnergy;

		private string Samples;

		private string EnergyScale;

		private string MaxRadius;

		private string MinRadius;

		private string DebyeMass;

		private string PotentialType;

		private string SpinState;

		private string Sigma;

		private string SpinCouplingRange;

		private string SpinCouplingStrength;

		private string Temperature;

		private string AlphaSoft;

		private string ColorState;

		private string[] ColorStates;

		private string[] PotentialTypes;

		private string[] RunningCouplingTypes;

		private string[] SpinStates;

		private string BottomiumStates;

		private string DecayWidthAveragingAngles;

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["AlphaSoft"] = AlphaSoft;
			nameValuePairs["BottomiumStates"] = BottomiumStates;
			nameValuePairs["ColorState"] = ColorState;
			nameValuePairs["DataFileName"] = DataFileName;
			nameValuePairs["DebyeMass_MeV"] = DebyeMass;
			nameValuePairs["DecayWidthAveragingAngles"] = DecayWidthAveragingAngles;
			nameValuePairs["EnergyScale_MeV"] = EnergyScale;
			nameValuePairs["MaxEnergy_MeV"] = MaxEnergy;
			nameValuePairs["MaxRadius_fm"] = MaxRadius;
			nameValuePairs["MinEnergy_MeV"] = MinEnergy;
			nameValuePairs["MinRadius_fm"] = MinRadius;
			nameValuePairs["PotentialType"] = PotentialType;
			nameValuePairs["RunningCouplingTypeSelection"] = RunningCouplingTypeSelection;
			nameValuePairs["Samples"] = Samples;
			nameValuePairs["Sigma_MeV2"] = Sigma;
			nameValuePairs["SpinCouplingRange_fm"] = SpinCouplingRange;
			nameValuePairs["SpinCouplingStrength_MeV"] = SpinCouplingStrength;
			nameValuePairs["SpinState"] = SpinState;
			nameValuePairs["Temperature_MeV"] = Temperature;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			AlphaSoft = nameValuePairs["AlphaSoft"];
			BottomiumStates = nameValuePairs["BottomiumStates"];
			ColorState = nameValuePairs["ColorState"];
			DataFileName = nameValuePairs["DataFileName"];
			DebyeMass = nameValuePairs["DebyeMass_MeV"];
			DecayWidthAveragingAngles = nameValuePairs["DecayWidthAveragingAngles"];
			EnergyScale = nameValuePairs["EnergyScale_MeV"];
			MaxEnergy = nameValuePairs["MaxEnergy_MeV"];
			MaxRadius = nameValuePairs["MaxRadius_fm"];
			MinEnergy = nameValuePairs["MinEnergy_MeV"];
			MinRadius = nameValuePairs["MinRadius_fm"];
			PotentialType = nameValuePairs["PotentialType"];
			RunningCouplingTypeSelection = nameValuePairs["RunningCouplingTypeSelection"];
			Samples = nameValuePairs["Samples"];
			Sigma = nameValuePairs["Sigma_MeV2"];
			SpinCouplingRange = nameValuePairs["SpinCouplingRange_fm"];
			SpinCouplingStrength = nameValuePairs["SpinCouplingStrength_MeV"];
			SpinState = nameValuePairs["SpinState"];
			Temperature = nameValuePairs["Temperature_MeV"];
		}

		private void SetControlsValuesFromSingleQQParams(
			Dictionary<string, string> param
			)
		{
			Dictionary<string, string> standardParams = GetControlsValues();
			standardParams["AlphaSoft"] = TryGetString(param, "AlphaSoft", AlphaSoft);
			standardParams["ColorState"] = TryGetString(param, "ColorState", ColorState);
			standardParams["DataFileName"] = TryGetString(param, "DataFileName", DataFileName);
			standardParams["DebyeMass_MeV"] = TryGetString(param, "DebyeMass_MeV", DebyeMass);
			standardParams["EnergyScale_MeV"] = TryGetString(param, "Energy_MeV", EnergyScale);
			standardParams["RunningCouplingTypeSelection"] = TryGetString(param, "RunningCouplingType", RunningCouplingTypeSelection);
			standardParams["MaxEnergy_MeV"] = TryGetString(param, "MaxEnergy_MeV", MaxEnergy);
			standardParams["MaxRadius_fm"] = TryGetString(param, "MaxRadius_fm", MaxRadius);
			standardParams["MinEnergy_MeV"] = TryGetString(param, "Energy_MeV", MinEnergy);
			standardParams["MinRadius_fm"] = TryGetString(param, "MinRadius_fm", MinRadius);
			standardParams["PotentialType"] = TryGetString(param, "PotentialType", PotentialType);
			standardParams["Samples"] = TryGetString(param, "EnergySteps", Samples);
			standardParams["Sigma_MeV2"] = TryGetString(param, "Sigma_MeV2", Sigma);
			standardParams["SpinCouplingRange_fm"] = TryGetString(param, "SpinCouplingRange_fm", SpinCouplingRange);
			standardParams["SpinCouplingStrength_MeV"] = TryGetString(param, "SpinCouplingStrength_MeV", SpinCouplingStrength);
			standardParams["SpinState"] = TryGetString(param, "SpinState", SpinState);
			standardParams["Temperature_MeV"] = TryGetString(param, "Temperature_MeV", Temperature);

			SetControlsValues(standardParams);
		}

		private AlphaPlotParamForm CreateAlphaPlotParamForm()
		{
			AlphaPlotParamForm plotForm = new AlphaPlotParamForm("Plot Alpha", RunningCouplingTypes,
				RunningCouplingTypeSelection, MinEnergy, MaxEnergy, Samples, DataFileName);
			plotForm.PlotRequest += OnPlotRequest;

			return plotForm;
		}

		private PionGDFPlotParamForm CreatePionGDFPlotParamForm()
		{
			PionGDFPlotParamForm plotForm = new PionGDFPlotParamForm(
				"Plot Pion Gluon Distribution Function", EnergyScale, Samples, DataFileName);
			plotForm.PlotRequest += OnPlotRequest;

			return plotForm;
		}

		private PotentialPlotParamForm CreatePotentialPlotParamForm()
		{
			PotentialPlotParamForm plotForm = new PotentialPlotParamForm(
				"Plot QQ-Interaction Potential", PotentialTypes, PotentialType, AlphaSoft, Sigma,
				ColorStates, ColorState, Temperature, DebyeMass, SpinStates, SpinState,
				SpinCouplingRange, SpinCouplingStrength, MinRadius, MaxRadius, Samples, DataFileName);
			plotForm.PlotRequest += OnPlotRequest;

			return plotForm;
		}

		private void OnPlotRequest(
			object sender,
			PlotRequestEventArgs args
			)
		{
			DataFileName = args.DataFileName;
			Samples = args.Samples;

			if(args is AlphaPlotRequestEventArgs)
			{
				AlphaPlotRequestEventArgs alphaArgs = args as AlphaPlotRequestEventArgs;
				MaxEnergy = alphaArgs.MaxEnergy;
				MinEnergy = alphaArgs.MinEnergy;
				RunningCouplingTypeSelection = alphaArgs.RunningCouplingTypeSelection;
			}
			else if(args is PionGDFPlotRequestEventArgs)
			{
				PionGDFPlotRequestEventArgs pionArgs = args as PionGDFPlotRequestEventArgs;
				EnergyScale = pionArgs.EnergyScale;
			}
			else if(args is PotentialPlotRequestEventArgs)
			{
				PotentialPlotRequestEventArgs potentialArgs = args as PotentialPlotRequestEventArgs;
				AlphaSoft = potentialArgs.AlphaSoft;
				ColorState = potentialArgs.ColorState;
				DataFileName = potentialArgs.DataFileName;
				DebyeMass = potentialArgs.DebyeMass;
				MinRadius = potentialArgs.MinRadius;
				MaxRadius = potentialArgs.MaxRadius;
				PotentialType = potentialArgs.PotentialType;
				Samples = potentialArgs.Samples;
				Sigma = potentialArgs.Sigma;
				SpinState = potentialArgs.SpinState;
				SpinCouplingRange = potentialArgs.SpinCouplingRange;
				SpinCouplingStrength = potentialArgs.SpinCouplingStrength;
				Temperature = potentialArgs.Temperature;
			}
		}
	}
}
