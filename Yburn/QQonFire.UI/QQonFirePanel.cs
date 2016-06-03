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
			CbxDecayWidthEvaluationType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("DecayWidthEvaluationType"));
			CbxExpansionMode.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ExpansionMode"));
			CbxDecayWidthType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("DecayWidthType"));
			CbxTemperatureProfile.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("TemperatureProfile"));
			CbxProtonProtonBaseline.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ProtonProtonBaseline"));
			CbxShapeFunctionTypeA.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ShapeFunction"));
			CbxShapeFunctionTypeB.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ShapeFunction"));
			MsxFireballFieldTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("FireballFieldType"));
			MsxPotentialTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("PotentialType"));
			MsxBottomiumStates.AddItems(JobOrganizer.GetWorkerEnumEntries("BottomiumState"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["DiffusenessA"] = TbxDiffusenessA.Text;
			nameValuePairs["DiffusenessB"] = TbxDiffusenessB.Text;
			nameValuePairs["NucleonNumberA"] = TbxNucleonNumberA.Text;
			nameValuePairs["NucleonNumberB"] = TbxNucleonNumberB.Text;
			nameValuePairs["NuclearRadiusA"] = TbxNuclearRadiusA.Text;
			nameValuePairs["NuclearRadiusB"] = TbxNuclearRadiusB.Text;
			nameValuePairs["ImpactParam"] = TbxImpactParam.Text;
			nameValuePairs["ShapeFunctionTypeA"] = CbxShapeFunctionTypeA.Text;
			nameValuePairs["ShapeFunctionTypeB"] = CbxShapeFunctionTypeB.Text;
			nameValuePairs["ExpansionMode"] = CbxExpansionMode.Text;
			nameValuePairs["TemperatureProfile"] = CbxTemperatureProfile.Text;
			nameValuePairs["ProtonProtonBaseline"] = CbxProtonProtonBaseline.Text;
			nameValuePairs["InitialCentralTemperature"] = TbxInitialCentralTemperature.Text;
			nameValuePairs["MinimalCentralTemperature"] = TbxMinimalCentralTemperature.Text;
			nameValuePairs["ThermalTime"] = TbxThermalTime.Text;
			nameValuePairs["GridCellSize"] = TbxGridCellSize.Text;
			nameValuePairs["GridRadius"] = TbxGridRadius.Text;
			nameValuePairs["BeamRapidity"] = TbxBeamRapidity.Text;
			nameValuePairs["DecayWidthType"] = CbxDecayWidthType.Text;
			nameValuePairs["DecayWidthAveragingAngles"] = TbxDecayWidthAveragingAngles.Text;
			nameValuePairs["PotentialTypes"] = MsxPotentialTypes.SelectionString;
			nameValuePairs["FeedDown3P"] = TbxFeedDown3P.Text;
			nameValuePairs["FormationTimes"] = TbxFormationTimes.Text;
			nameValuePairs["TransverseMomenta"] = TbxTransverseMomenta.Text;
			nameValuePairs["DecayWidthEvaluationType"] = CbxDecayWidthEvaluationType.Text;
			nameValuePairs["BottomiumStates"] = MsxBottomiumStates.SelectionString;
			nameValuePairs["FireballFieldTypes"] = MsxFireballFieldTypes.SelectionString;
			nameValuePairs["SnapRate"] = TbxSnapRate.Text;
			nameValuePairs["CentralityBinBoundaries"] = TbxCentralityBinBoundaries.Text;
			nameValuePairs["ImpactParamsAtBinBoundaries"] = TbxImpactParamsAtBinBoundaries.Text;
			nameValuePairs["ParticipantsAtBinBoundaries"] = TbxParticipantsAtBinBoundaries.Text;
			nameValuePairs["MeanParticipantsInBin"] = TbxMeanParticipantsInBin.Text;
			nameValuePairs["Outfile"] = TbxOutfile.Text;
			nameValuePairs["BjorkenLifeTime"] = TbxBjorkenLifeTime.Text;
			nameValuePairs["LifeTime"] = TbxLifeTime.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			TbxDiffusenessA.Text = nameValuePairs["DiffusenessA"].ToString();
			TbxDiffusenessB.Text = nameValuePairs["DiffusenessB"].ToString();
			TbxNucleonNumberA.Text = nameValuePairs["NucleonNumberA"].ToString();
			TbxNucleonNumberB.Text = nameValuePairs["NucleonNumberB"].ToString();
			TbxNuclearRadiusA.Text = nameValuePairs["NuclearRadiusA"].ToString();
			TbxNuclearRadiusB.Text = nameValuePairs["NuclearRadiusB"].ToString();
			TbxImpactParam.Text = nameValuePairs["ImpactParam"].ToString();
			CbxShapeFunctionTypeA.Text = nameValuePairs["ShapeFunctionTypeA"].ToString();
			CbxShapeFunctionTypeB.Text = nameValuePairs["ShapeFunctionTypeB"].ToString();
			CbxExpansionMode.Text = nameValuePairs["ExpansionMode"].ToString();
			CbxTemperatureProfile.Text = nameValuePairs["TemperatureProfile"].ToString();
			CbxProtonProtonBaseline.Text = nameValuePairs["ProtonProtonBaseline"].ToString();
			TbxInitialCentralTemperature.Text = nameValuePairs["InitialCentralTemperature"].ToString();
			TbxMinimalCentralTemperature.Text = nameValuePairs["MinimalCentralTemperature"].ToString();
			TbxThermalTime.Text = nameValuePairs["ThermalTime"].ToString();
			TbxGridCellSize.Text = nameValuePairs["GridCellSize"].ToString();
			TbxGridRadius.Text = nameValuePairs["GridRadius"].ToString();
			TbxBeamRapidity.Text = nameValuePairs["BeamRapidity"].ToString();
			CbxDecayWidthType.Text = nameValuePairs["DecayWidthType"].ToString();
			TbxDecayWidthAveragingAngles.Text = nameValuePairs["DecayWidthAveragingAngles"].ToString();
			MsxPotentialTypes.SelectionString = nameValuePairs["PotentialTypes"].ToString();
			TbxFeedDown3P.Text = nameValuePairs["FeedDown3P"].ToString();
			TbxFormationTimes.Text = nameValuePairs["FormationTimes"].ToString();
			TbxTransverseMomenta.Text = nameValuePairs["TransverseMomenta"].ToString();
			CbxDecayWidthEvaluationType.Text = nameValuePairs["DecayWidthEvaluationType"].ToString();
			MsxBottomiumStates.SelectionString = nameValuePairs["BottomiumStates"].ToString();
			MsxFireballFieldTypes.SelectionString = nameValuePairs["FireballFieldTypes"].ToString();
			TbxSnapRate.Text = nameValuePairs["SnapRate"].ToString();
			TbxCentralityBinBoundaries.Text = nameValuePairs["CentralityBinBoundaries"].ToString();
			TbxImpactParamsAtBinBoundaries.Text = nameValuePairs["ImpactParamsAtBinBoundaries"].ToString();
			TbxParticipantsAtBinBoundaries.Text = nameValuePairs["ParticipantsAtBinBoundaries"].ToString();
			TbxMeanParticipantsInBin.Text = nameValuePairs["MeanParticipantsInBin"].ToString();
			TbxOutfile.Text = nameValuePairs["Outfile"].ToString();
			TbxBjorkenLifeTime.Text = nameValuePairs["BjorkenLifeTime"].ToString();
			TbxLifeTime.Text = nameValuePairs["LifeTime"].ToString();
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
			MenuEntry.MenuItemShowInitialPopulations.Click += new EventHandler(MenuItemShowInitialPopulations_Click);
			MenuEntry.MenuItemShowProtonProtonYields.Click += new EventHandler(MenuItemShowProtonProtonYields_Click);
			MenuEntry.MenuItemShowFeedDown.Click += new EventHandler(MenuItemShowFeedDown_Click);
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

		private void MenuItemShowInitialPopulations_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowInitialPopulations", ControlsValues);
		}

		private void MenuItemShowProtonProtonYields_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowProtonProtonYields", ControlsValues);
		}

		private void MenuItemShowFeedDown_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowY1SFeedDown", ControlsValues);
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