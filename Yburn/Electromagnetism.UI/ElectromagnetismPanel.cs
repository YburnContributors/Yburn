using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Yburn.Interfaces;
using Yburn.UI;

namespace Yburn.Electromagnetism.UI
{
	public partial class ElectromagnetismPanel : UserControl
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ElectromagnetismPanel()
			: base()
		{
			InitializeComponent();
			InitializeMenuEntry();

			this.Name = "Electromagnetism";
			LayoutBottom.AutoScrollMinSize = new Size(0, 530);
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

		public MenuItemElectromagnetism MenuEntry
		{
			get;
			private set;
		}

		public StatusTrackingCtrl StatusTrackingCtrl
		{
			get
			{
				return null;
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
			CbxEMFCalculationMethod.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("EMFCalculationMethod"));
			CbxNucleusShapeA.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("NucleusShape"));
			CbxNucleusShapeB.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("NucleusShape"));
			MsxEMFCalculationMethodSelection.AddItems(JobOrganizer.GetWorkerEnumEntries("EMFCalculationMethod"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			nameValuePairs["DiffusenessA"] = TbxDiffusenessA.Text;
			nameValuePairs["DiffusenessB"] = TbxDiffusenessB.Text;
			nameValuePairs["EMFCalculationMethod"] = CbxEMFCalculationMethod.Text;
			nameValuePairs["EMFCalculationMethodSelection"] = MsxEMFCalculationMethodSelection.SelectionString;
			nameValuePairs["EMFQuadratureOrder"] = TbxEMFQuadratureOrder.Text;
			nameValuePairs["GridCellSize"] = TbxGridCellSize.Text;
			nameValuePairs["GridRadius"] = TbxGridRadius.Text;
			nameValuePairs["ImpactParameter"] = TbxImpactParameter.Text;
			nameValuePairs["PointChargeRapidity"] = TbxPointChargeRapidity.Text;
			nameValuePairs["NuclearRadiusA"] = TbxNuclearRadiusA.Text;
			nameValuePairs["NuclearRadiusB"] = TbxNuclearRadiusB.Text;
			nameValuePairs["NucleonNumberA"] = TbxNucleonNumberA.Text;
			nameValuePairs["NucleonNumberB"] = TbxNucleonNumberB.Text;
			nameValuePairs["DataFileName"] = TbxDataFileName.Text;
			nameValuePairs["ProtonNumberA"] = TbxProtonNumberA.Text;
			nameValuePairs["ProtonNumberB"] = TbxProtonNumberB.Text;
			nameValuePairs["QGPConductivity"] = TbxQGPConductivity.Text;
			nameValuePairs["RadialDistance"] = TbxRadialDistance.Text;
			nameValuePairs["NucleusShapeA"] = CbxNucleusShapeA.Text;
			nameValuePairs["NucleusShapeB"] = CbxNucleusShapeB.Text;
			nameValuePairs["Samples"] = TbxSamples.Text;
			nameValuePairs["StartEffectiveTime"] = TbxStartEffectiveTime.Text;
			nameValuePairs["StopEffectiveTime"] = TbxStopEffectiveTime.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxEMFCalculationMethod.Text = nameValuePairs["EMFCalculationMethod"];
			CbxNucleusShapeA.Text = nameValuePairs["NucleusShapeA"];
			CbxNucleusShapeB.Text = nameValuePairs["NucleusShapeB"];
			MsxEMFCalculationMethodSelection.SelectionString = nameValuePairs["EMFCalculationMethodSelection"];
			TbxDiffusenessA.Text = nameValuePairs["DiffusenessA"];
			TbxDiffusenessB.Text = nameValuePairs["DiffusenessB"];
			TbxEMFQuadratureOrder.Text = nameValuePairs["EMFQuadratureOrder"];
			TbxGridCellSize.Text = nameValuePairs["GridCellSize"];
			TbxGridRadius.Text = nameValuePairs["GridRadius"];
			TbxImpactParameter.Text = nameValuePairs["ImpactParameter"];
			TbxPointChargeRapidity.Text = nameValuePairs["PointChargeRapidity"];
			TbxNuclearRadiusA.Text = nameValuePairs["NuclearRadiusA"];
			TbxNuclearRadiusB.Text = nameValuePairs["NuclearRadiusB"];
			TbxNucleonNumberA.Text = nameValuePairs["NucleonNumberA"];
			TbxNucleonNumberB.Text = nameValuePairs["NucleonNumberB"];
			TbxDataFileName.Text = nameValuePairs["DataFileName"];
			TbxProtonNumberA.Text = nameValuePairs["ProtonNumberA"];
			TbxProtonNumberB.Text = nameValuePairs["ProtonNumberB"];
			TbxQGPConductivity.Text = nameValuePairs["QGPConductivity"];
			TbxRadialDistance.Text = nameValuePairs["RadialDistance"];
			TbxSamples.Text = nameValuePairs["Samples"];
			TbxStartEffectiveTime.Text = nameValuePairs["StartEffectiveTime"];
			TbxStopEffectiveTime.Text = nameValuePairs["StopEffectiveTime"];
		}

		private void InitializeMenuEntry()
		{
			MenuEntry = new MenuItemElectromagnetism();
			MenuEntry.MenuItemPlotPointChargeAzimutalMagneticField.Click += new EventHandler(MenuItemPlotPointChargeAzimutalMagneticField_Click);
			MenuEntry.MenuItemPlotPointChargeLongitudinalElectricField.Click += new EventHandler(MenuItemPlotPointChargeLongitudinalElectricField_Click);
			MenuEntry.MenuItemPlotPointChargeRadialElectricField.Click += new EventHandler(MenuItemPlotPointChargeRadialElectricField_Click);
			MenuEntry.MenuItemPlotPointChargeAndNucleusFieldComponents.Click += new EventHandler(MenuItemPlotPointChargeAndNucleusFieldComponents_Click);
			MenuEntry.MenuItemPlotNucleusMagneticFieldStrengthInLCF.Click += new EventHandler(MenuItemPlotNucleusMagneticFieldStrengthInLCF_Click);
			MenuEntry.MenuItemPlotCentralMagneticFieldStrength.Click += new EventHandler(MenuItemPlotCentralMagneticFieldStrength_Click);
			MenuEntry.MenuItemPlotAverageMagneticFieldStrength.Click += new EventHandler(MenuItemPlotAverageMagneticFieldStrength_Click);
			MenuEntry.MenuItemPlotAverageSpinStateOverlap.Click += new EventHandler(MenuItemPlotAverageSpinStateOverlap_Click);
		}

		private void MakeToolTips(
			ToolTipMaker toolTipMaker
			)
		{
			toolTipMaker.Add(
				"Chosen approximation for the calculation of the electromagnetic field.",
				LblEMFCalculationMethod, CbxEMFCalculationMethod);
			toolTipMaker.Add(
				"Name of the output file. The standard output path can be set\r\n"
				+ "in the menu \"File\" using \"Set output path\".",
				LblDataFileName, TbxDataFileName);
		}

		private void MenuItemPlotPointChargeAzimutalMagneticField_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotPointChargeAzimutalMagneticField", ControlsValues);
		}

		private void MenuItemPlotPointChargeLongitudinalElectricField_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotPointChargeLongitudinalElectricField", ControlsValues);
		}

		private void MenuItemPlotPointChargeRadialElectricField_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotPointChargeRadialElectricField", ControlsValues);
		}

		private void MenuItemPlotPointChargeAndNucleusFieldComponents_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotPointChargeAndNucleusFieldComponents", ControlsValues);
		}

		private void MenuItemPlotNucleusMagneticFieldStrengthInLCF_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotNucleusMagneticFieldStrengthInLCF", ControlsValues);
		}

		private void MenuItemPlotCentralMagneticFieldStrength_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotCentralMagneticFieldStrength", ControlsValues);
		}

		private void MenuItemPlotAverageMagneticFieldStrength_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotAverageMagneticFieldStrength", ControlsValues);
		}

		private void MenuItemPlotAverageSpinStateOverlap_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotAverageSpinStateOverlap", ControlsValues);
		}
	}
}