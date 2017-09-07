using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Yburn.Interfaces;
using Yburn.UI;

namespace Yburn.QQonFire.UI
{
	public partial class QQonFirePanel : UserControl
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQonFirePanel()
			: base()
		{
			InitializeComponent();
			InitializeMenuEntry();

			this.Name = "QQonFire";
			LayoutBottom.AutoScrollMinSize = new Size(0, 917);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Initialize(
			JobOrganizer jobOrganizer,
			ToolTipMaker toolTipMaker
			)
		{
			Initialize(jobOrganizer);
			MakeToolTips(toolTipMaker);
		}

		public void Initialize(
			JobOrganizer jobOrganizer
			)
		{
			SetJobOrganizer(jobOrganizer);
			InitializeComboBoxes();
		}

		public RichTextBox TextBoxLog
		{
			get
			{
				return CtrlTextBoxLog;
			}
		}

		public MenuItemQQonFire MenuEntry
		{
			get;
			private set;
		}

		public StatusTrackingCtrl StatusTrackingCtrl
		{
			get
			{
				return CtrlStatusTrackingCtrl;
			}
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

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private JobOrganizer JobOrganizer;

		private void SetJobOrganizer(
			JobOrganizer jobOrganizer
			)
		{
			JobOrganizer = jobOrganizer;
		}

		private void InitializeComboBoxes()
		{
			CbxDecayWidthType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("DecayWidthType"));
			CbxDopplerShiftEvaluationType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("DopplerShiftEvaluationType"));
			CbxEMFCalculationMethod.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("EMFCalculationMethod"));
			CbxElectricDipoleAlignment.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ElectricDipoleAlignment"));
			CbxExpansionMode.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ExpansionMode"));
			CbxNucleusShapeA.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("NucleusShape"));
			CbxNucleusShapeB.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("NucleusShape"));
			CbxTemperatureProfile.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("TemperatureProfile"));
			MsxBottomiumStates.AddItems(JobOrganizer.GetWorkerEnumEntries("BottomiumState"));
			MsxFireballFieldTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("FireballFieldType"));
			MsxPotentialTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("PotentialType"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				["BjorkenLifeTime_fm"] = TbxBjorkenLifeTime.Text,
				["BottomiumStates"] = MsxBottomiumStates.SelectionString,
				["BreakupTemperature_MeV"] = TbxBreakupTemperature.Text,
				["CenterOfMassEnergy_TeV"] = TbxCenterOfMassEnergy.Text,
				["CentralityBinBoundaries_percent"] = TbxCentralityBinBoundaries.Text,
				["DataFileName"] = TbxDataFileName.Text,
				["DecayWidthType"] = CbxDecayWidthType.Text,
				["DiffusenessA_fm"] = TbxDiffusenessA.Text,
				["DiffusenessB_fm"] = TbxDiffusenessB.Text,
				["DimuonDecaysFrompp"] = TbxDimuonDecaysFrompp.Text,
				["DopplerShiftEvaluationType"] = CbxDopplerShiftEvaluationType.Text,
				["EMFCalculationMethod"] = CbxEMFCalculationMethod.Text,
				["EMFQuadratureOrder"] = TbxEMFQuadratureOrder.Text,
				["EMFUpdateInterval_fm"] = TbxEMFUpdateInterval.Text,
				["ElectricDipoleAlignment"] = CbxElectricDipoleAlignment.Text,
				["ExpansionMode"] = CbxExpansionMode.Text,
				["FireballFieldTypes"] = MsxFireballFieldTypes.SelectionString,
				["FormationTimes_fm"] = TbxFormationTimes.Text,
				["GridCellSize_fm"] = TbxGridCellSize.Text,
				["GridRadius_fm"] = TbxGridRadius.Text,
				["ImpactParameter_fm"] = TbxImpactParameter.Text,
				["ImpactParamsAtBinBoundaries_fm"] = TbxImpactParamsAtBinBoundaries.Text,
				["InitialMaximumTemperature_MeV"] = TbxInitialMaximumTemperature.Text,
				["LifeTime_fm"] = TbxLifeTime.Text,
				["MeanParticipantsInBin"] = TbxMeanParticipantsInBin.Text,
				["NuclearRadiusA_fm"] = TbxNuclearRadiusA.Text,
				["NuclearRadiusB_fm"] = TbxNuclearRadiusB.Text,
				["NucleonNumberA"] = TbxNucleonNumberA.Text,
				["NucleonNumberB"] = TbxNucleonNumberB.Text,
				["NucleusShapeA"] = CbxNucleusShapeA.Text,
				["NucleusShapeB"] = CbxNucleusShapeB.Text,
				["NumberAveragingAngles"] = TbxNumberAveragingAngles.Text,
				["ParticipantsAtBinBoundaries"] = TbxParticipantsAtBinBoundaries.Text,
				["PotentialTypes"] = MsxPotentialTypes.SelectionString,
				["ProtonNumberA"] = TbxProtonNumberA.Text,
				["ProtonNumberB"] = TbxProtonNumberB.Text,
				["QGPConductivity_MeV"] = TbxQGPConductivity.Text,
				["QGPFormationTemperature_MeV"] = TbxQGPFormationTemperature.Text,
				["SnapRate_per_fm"] = TbxSnapRate.Text,
				["TemperatureProfile"] = CbxTemperatureProfile.Text,
				["ThermalTime_fm"] = TbxThermalTime.Text,
				["TransverseMomenta_GeV"] = TbxTransverseMomenta.Text,
				["UseElectricField"] = ChkUseElectricField.Checked.ToString(),
				["UseMagneticField"] = ChkUseMagneticField.Checked.ToString()
			};

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxDecayWidthType.Text = nameValuePairs["DecayWidthType"];
			CbxDopplerShiftEvaluationType.Text = nameValuePairs["DopplerShiftEvaluationType"];
			CbxEMFCalculationMethod.Text = nameValuePairs["EMFCalculationMethod"];
			CbxElectricDipoleAlignment.Text = nameValuePairs["ElectricDipoleAlignment"];
			CbxExpansionMode.Text = nameValuePairs["ExpansionMode"];
			CbxNucleusShapeA.Text = nameValuePairs["NucleusShapeA"];
			CbxNucleusShapeB.Text = nameValuePairs["NucleusShapeB"];
			CbxTemperatureProfile.Text = nameValuePairs["TemperatureProfile"];
			ChkUseElectricField.Checked = bool.Parse(nameValuePairs["UseElectricField"]);
			ChkUseMagneticField.Checked = bool.Parse(nameValuePairs["UseMagneticField"]);
			MsxBottomiumStates.SelectionString = nameValuePairs["BottomiumStates"];
			MsxFireballFieldTypes.SelectionString = nameValuePairs["FireballFieldTypes"];
			MsxPotentialTypes.SelectionString = nameValuePairs["PotentialTypes"];
			TbxBjorkenLifeTime.Text = nameValuePairs["BjorkenLifeTime_fm"];
			TbxBreakupTemperature.Text = nameValuePairs["BreakupTemperature_MeV"];
			TbxCenterOfMassEnergy.Text = nameValuePairs["CenterOfMassEnergy_TeV"];
			TbxCentralityBinBoundaries.Text = nameValuePairs["CentralityBinBoundaries_percent"];
			TbxDataFileName.Text = nameValuePairs["DataFileName"];
			TbxDiffusenessA.Text = nameValuePairs["DiffusenessA_fm"];
			TbxDiffusenessB.Text = nameValuePairs["DiffusenessB_fm"];
			TbxDimuonDecaysFrompp.Text = nameValuePairs["DimuonDecaysFrompp"];
			TbxEMFQuadratureOrder.Text = nameValuePairs["EMFQuadratureOrder"];
			TbxEMFUpdateInterval.Text = nameValuePairs["EMFUpdateInterval_fm"];
			TbxFormationTimes.Text = nameValuePairs["FormationTimes_fm"];
			TbxGridCellSize.Text = nameValuePairs["GridCellSize_fm"];
			TbxGridRadius.Text = nameValuePairs["GridRadius_fm"];
			TbxImpactParameter.Text = nameValuePairs["ImpactParameter_fm"];
			TbxImpactParamsAtBinBoundaries.Text = nameValuePairs["ImpactParamsAtBinBoundaries_fm"];
			TbxInitialMaximumTemperature.Text = nameValuePairs["InitialMaximumTemperature_MeV"];
			TbxLifeTime.Text = nameValuePairs["LifeTime_fm"];
			TbxMeanParticipantsInBin.Text = nameValuePairs["MeanParticipantsInBin"];
			TbxNuclearRadiusA.Text = nameValuePairs["NuclearRadiusA_fm"];
			TbxNuclearRadiusB.Text = nameValuePairs["NuclearRadiusB_fm"];
			TbxNucleonNumberA.Text = nameValuePairs["NucleonNumberA"];
			TbxNucleonNumberB.Text = nameValuePairs["NucleonNumberB"];
			TbxNumberAveragingAngles.Text = nameValuePairs["NumberAveragingAngles"];
			TbxParticipantsAtBinBoundaries.Text = nameValuePairs["ParticipantsAtBinBoundaries"];
			TbxProtonNumberA.Text = nameValuePairs["ProtonNumberA"];
			TbxProtonNumberB.Text = nameValuePairs["ProtonNumberB"];
			TbxQGPConductivity.Text = nameValuePairs["QGPConductivity_MeV"];
			TbxQGPFormationTemperature.Text = nameValuePairs["QGPFormationTemperature_MeV"];
			TbxSnapRate.Text = nameValuePairs["SnapRate_per_fm"];
			TbxThermalTime.Text = nameValuePairs["ThermalTime_fm"];
			TbxTransverseMomenta.Text = nameValuePairs["TransverseMomenta_GeV"];
		}

		private void InitializeMenuEntry()
		{
			MenuEntry = new MenuItemQQonFire();
			MenuEntry.MenuItemBinBounds.Click += new EventHandler(MenuItemBinBounds_Click);
			MenuEntry.MenuItemDirectPionDecayWidths.Click += new EventHandler(MenuItemDirectPionDecayWidths_Click);
			MenuEntry.MenuItemMakeSnapshots.Click += new EventHandler(MenuItemMakeSnapshots_Click);
			MenuEntry.MenuItemShowBranchingRatio.Click += new EventHandler(MenuItemShowBranchingRatio_Click);
			MenuEntry.MenuItemShowCumulativeMatrix.Click += new EventHandler(MenuItemShowCumulativeMatrix_Click);
			MenuEntry.MenuItemShowFeedDown.Click += new EventHandler(MenuItemShowY1SFeedDownFractions_Click);
			MenuEntry.MenuItemShowGamma.Click += new EventHandler(MenuItemShowGamma_Click);
			MenuEntry.MenuItemShowInitialQQPopulations.Click += new EventHandler(MenuItemShowInitialQQPopulations_Click);
			MenuEntry.MenuItemShowInverseCumulativeMatrix.Click += new EventHandler(MenuItemShowInverseCumulativeMatrix_Click);
			MenuEntry.MenuItemShowProtonProtonDimuonDecays.Click += new EventHandler(MenuItemShowProtonProtonDimuonDecays_Click);
			MenuEntry.MenuItemShowSnapsX.Click += new EventHandler(MenuItemShowSnapsX_Click);
			MenuEntry.MenuItemShowSnapsXY.Click += new EventHandler(MenuItemShowSnapsXY_Click);
			MenuEntry.MenuItemShowSnapsY.Click += new EventHandler(MenuItemShowSnapsY_Click);
			MenuEntry.MenuItemSuppression.Click += new EventHandler(MenuItemSuppression_Click);
		}

		private void MakeToolTips(
			ToolTipMaker toolTipMaker
			)
		{
			toolTipMaker.Add(
				"Choose which decay widths are used by specifying the potential types used in SingleQQ."
				+ " The choice should be unique for every temperature.",
				LblPotentialTypes, MsxPotentialTypes);
		}

		private void MenuItemSuppression_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateSuppression", ControlsValues);
		}

		private void MenuItemBinBounds_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateBinBoundaries", ControlsValues);
		}

		private void MenuItemDirectPionDecayWidths_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateDirectPionDecayWidths", ControlsValues);
		}

		private void MenuItemShowGamma_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowDecayWidthInput", ControlsValues);
		}

		private void MenuItemShowBranchingRatio_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowBranchingRatio", ControlsValues);
		}

		private void MenuItemShowCumulativeMatrix_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowCumulativeMatrix", ControlsValues);
		}

		private void MenuItemShowInverseCumulativeMatrix_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowInverseCumulativeMatrix", ControlsValues);
		}

		private void MenuItemShowInitialQQPopulations_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowInitialQQPopulations", ControlsValues);
		}

		private void MenuItemShowProtonProtonDimuonDecays_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowProtonProtonDimuonDecays", ControlsValues);
		}

		private void MenuItemShowY1SFeedDownFractions_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowY1SFeedDownFractions", ControlsValues);
		}

		private void MenuItemMakeSnapshots_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("MakeSnapshots", ControlsValues);
		}

		private void MenuItemShowSnapsX_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowSnapsX", ControlsValues);
		}

		private void MenuItemShowSnapsY_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowSnapsY", ControlsValues);
		}

		private void MenuItemShowSnapsXY_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowSnapsXY", ControlsValues);
		}
	}
}
