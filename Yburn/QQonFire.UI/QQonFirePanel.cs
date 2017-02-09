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
			CbxElectricDipoleAlignmentType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("EMFDipoleAlignmentType"));
			CbxExpansionMode.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ExpansionMode"));
			CbxMagneticDipoleAlignmentType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("EMFDipoleAlignmentType"));
			CbxNucleusShapeA.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("NucleusShape"));
			CbxNucleusShapeB.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("NucleusShape"));
			CbxTemperatureProfile.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("TemperatureProfile"));
			MsxBottomiumStates.AddItems(JobOrganizer.GetWorkerEnumEntries("BottomiumState"));
			MsxFireballFieldTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("FireballFieldType"));
			MsxPotentialTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("PotentialType"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			nameValuePairs["BeamRapidity"] = TbxBeamRapidity.Text;
			nameValuePairs["BjorkenLifeTime"] = TbxBjorkenLifeTime.Text;
			nameValuePairs["BottomiumStates"] = MsxBottomiumStates.SelectionString;
			nameValuePairs["BreakupTemperature"] = TbxBreakupTemperature.Text;
			nameValuePairs["CentralityBinBoundaries"] = TbxCentralityBinBoundaries.Text;
			nameValuePairs["DataFileName"] = TbxDataFileName.Text;
			nameValuePairs["DecayWidthType"] = CbxDecayWidthType.Text;
			nameValuePairs["DiffusenessA"] = TbxDiffusenessA.Text;
			nameValuePairs["DiffusenessB"] = TbxDiffusenessB.Text;
			nameValuePairs["DimuonDecaysFrompp"] = TbxDimuonDecaysFrompp.Text;
			nameValuePairs["DopplerShiftEvaluationType"] = CbxDopplerShiftEvaluationType.Text;
			nameValuePairs["EMFCalculationMethod"] = CbxEMFCalculationMethod.Text;
			nameValuePairs["EMFQuadratureOrder"] = TbxEMFQuadratureOrder.Text;
			nameValuePairs["EMFUpdateInterval"] = TbxEMFUpdateInterval.Text;
			nameValuePairs["ElectricDipoleAlignmentType"] = CbxElectricDipoleAlignmentType.Text;
			nameValuePairs["ExpansionMode"] = CbxExpansionMode.Text;
			nameValuePairs["FireballFieldTypes"] = MsxFireballFieldTypes.SelectionString;
			nameValuePairs["FormationTimes"] = TbxFormationTimes.Text;
			nameValuePairs["GridCellSize"] = TbxGridCellSize.Text;
			nameValuePairs["GridRadius"] = TbxGridRadius.Text;
			nameValuePairs["ImpactParameter"] = TbxImpactParameter.Text;
			nameValuePairs["ImpactParamsAtBinBoundaries"] = TbxImpactParamsAtBinBoundaries.Text;
			nameValuePairs["InelasticppCrossSection"] = TbxInelasticppCrossSection.Text;
			nameValuePairs["InitialMaximumTemperature"] = TbxInitialMaximumTemperature.Text;
			nameValuePairs["LifeTime"] = TbxLifeTime.Text;
			nameValuePairs["MagneticDipoleAlignmentType"] = CbxMagneticDipoleAlignmentType.Text;
			nameValuePairs["MeanParticipantsInBin"] = TbxMeanParticipantsInBin.Text;
			nameValuePairs["NuclearRadiusA"] = TbxNuclearRadiusA.Text;
			nameValuePairs["NuclearRadiusB"] = TbxNuclearRadiusB.Text;
			nameValuePairs["NucleonNumberA"] = TbxNucleonNumberA.Text;
			nameValuePairs["NucleonNumberB"] = TbxNucleonNumberB.Text;
			nameValuePairs["NucleusShapeA"] = CbxNucleusShapeA.Text;
			nameValuePairs["NucleusShapeB"] = CbxNucleusShapeB.Text;
			nameValuePairs["NumberAveragingAngles"] = TbxNumberAveragingAngles.Text;
			nameValuePairs["ParticipantsAtBinBoundaries"] = TbxParticipantsAtBinBoundaries.Text;
			nameValuePairs["PotentialTypes"] = MsxPotentialTypes.SelectionString;
			nameValuePairs["ProtonNumberA"] = TbxProtonNumberA.Text;
			nameValuePairs["ProtonNumberB"] = TbxProtonNumberB.Text;
			nameValuePairs["QGPConductivity"] = TbxQGPConductivity.Text;
			nameValuePairs["QGPFormationTemperature"] = TbxQGPFormationTemperature.Text;
			nameValuePairs["SnapRate"] = TbxSnapRate.Text;
			nameValuePairs["TemperatureProfile"] = CbxTemperatureProfile.Text;
			nameValuePairs["ThermalTime"] = TbxThermalTime.Text;
			nameValuePairs["TransverseMomenta"] = TbxTransverseMomenta.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxDecayWidthType.Text = nameValuePairs["DecayWidthType"];
			CbxDopplerShiftEvaluationType.Text = nameValuePairs["DopplerShiftEvaluationType"];
			CbxEMFCalculationMethod.Text = nameValuePairs["EMFCalculationMethod"];
			CbxElectricDipoleAlignmentType.Text = nameValuePairs["ElectricDipoleAlignmentType"];
			CbxExpansionMode.Text = nameValuePairs["ExpansionMode"];
			CbxMagneticDipoleAlignmentType.Text = nameValuePairs["MagneticDipoleAlignmentType"];
			CbxNucleusShapeA.Text = nameValuePairs["NucleusShapeA"];
			CbxNucleusShapeB.Text = nameValuePairs["NucleusShapeB"];
			CbxTemperatureProfile.Text = nameValuePairs["TemperatureProfile"];
			MsxBottomiumStates.SelectionString = nameValuePairs["BottomiumStates"];
			MsxFireballFieldTypes.SelectionString = nameValuePairs["FireballFieldTypes"];
			MsxPotentialTypes.SelectionString = nameValuePairs["PotentialTypes"];
			TbxBeamRapidity.Text = nameValuePairs["BeamRapidity"];
			TbxBjorkenLifeTime.Text = nameValuePairs["BjorkenLifeTime"];
			TbxBreakupTemperature.Text = nameValuePairs["BreakupTemperature"];
			TbxCentralityBinBoundaries.Text = nameValuePairs["CentralityBinBoundaries"];
			TbxDataFileName.Text = nameValuePairs["DataFileName"];
			TbxDiffusenessA.Text = nameValuePairs["DiffusenessA"];
			TbxDiffusenessB.Text = nameValuePairs["DiffusenessB"];
			TbxDimuonDecaysFrompp.Text = nameValuePairs["DimuonDecaysFrompp"];
			TbxEMFQuadratureOrder.Text = nameValuePairs["EMFQuadratureOrder"];
			TbxEMFUpdateInterval.Text = nameValuePairs["EMFUpdateInterval"];
			TbxFormationTimes.Text = nameValuePairs["FormationTimes"];
			TbxGridCellSize.Text = nameValuePairs["GridCellSize"];
			TbxGridRadius.Text = nameValuePairs["GridRadius"];
			TbxImpactParameter.Text = nameValuePairs["ImpactParameter"];
			TbxImpactParamsAtBinBoundaries.Text = nameValuePairs["ImpactParamsAtBinBoundaries"];
			TbxInelasticppCrossSection.Text = nameValuePairs["InelasticppCrossSection"];
			TbxInitialMaximumTemperature.Text = nameValuePairs["InitialMaximumTemperature"];
			TbxLifeTime.Text = nameValuePairs["LifeTime"];
			TbxMeanParticipantsInBin.Text = nameValuePairs["MeanParticipantsInBin"];
			TbxNuclearRadiusA.Text = nameValuePairs["NuclearRadiusA"];
			TbxNuclearRadiusB.Text = nameValuePairs["NuclearRadiusB"];
			TbxNucleonNumberA.Text = nameValuePairs["NucleonNumberA"];
			TbxNucleonNumberB.Text = nameValuePairs["NucleonNumberB"];
			TbxNumberAveragingAngles.Text = nameValuePairs["NumberAveragingAngles"];
			TbxParticipantsAtBinBoundaries.Text = nameValuePairs["ParticipantsAtBinBoundaries"];
			TbxProtonNumberA.Text = nameValuePairs["ProtonNumberA"];
			TbxProtonNumberB.Text = nameValuePairs["ProtonNumberB"];
			TbxQGPConductivity.Text = nameValuePairs["QGPConductivity"];
			TbxQGPFormationTemperature.Text = nameValuePairs["QGPFormationTemperature"];
			TbxSnapRate.Text = nameValuePairs["SnapRate"];
			TbxThermalTime.Text = nameValuePairs["ThermalTime"];
			TbxTransverseMomenta.Text = nameValuePairs["TransverseMomenta"];
		}

		private void InitializeMenuEntry()
		{
			MenuEntry = new MenuItemQQonFire();
			MenuEntry.MenuItemBinBounds.Click += new EventHandler(MenuItemBinBounds_Click);
			MenuEntry.MenuItemDirectPionDecayWidths.Click += new EventHandler(MenuItemDirectPionDecayWidths_Click);
			MenuEntry.MenuItemSuppression.Click += new EventHandler(MenuItemSuppression_Click);
			MenuEntry.MenuItemMakeSnapshots.Click += new EventHandler(MenuItemMakeSnapshots_Click);
			MenuEntry.MenuItemShowSnapsX.Click += new EventHandler(MenuItemShowSnapsX_Click);
			MenuEntry.MenuItemShowSnapsY.Click += new EventHandler(MenuItemShowSnapsY_Click);
			MenuEntry.MenuItemShowSnapsXY.Click += new EventHandler(MenuItemShowSnapsXY_Click);
			MenuEntry.MenuItemShowGamma.Click += new EventHandler(MenuItemShowGamma_Click);
			MenuEntry.MenuItemShowBranchingRatio.Click += new EventHandler(MenuItemShowBranchingRatio_Click);
			MenuEntry.MenuItemShowCumulativeMatrix.Click += new EventHandler(MenuItemShowCumulativeMatrix_Click);
			MenuEntry.MenuItemShowInverseCumulativeMatrix.Click += new EventHandler(MenuItemShowInverseCumulativeMatrix_Click);
			MenuEntry.MenuItemShowInitialQQPopulations.Click += new EventHandler(MenuItemShowInitialQQPopulations_Click);
			MenuEntry.MenuItemShowProtonProtonDimuonDecays.Click += new EventHandler(MenuItemShowProtonProtonDimuonDecays_Click);
			MenuEntry.MenuItemShowFeedDown.Click += new EventHandler(MenuItemShowY1SFeedDownFractions_Click);
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