using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
			CbxShapeFunctionA.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ShapeFunction"));
			CbxShapeFunctionB.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ShapeFunction"));
			MsxEMFCalculationMethodSelection.AddItems(JobOrganizer.GetWorkerEnumEntries("EMFCalculationMethod"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["DiffusenessA"] = TbxDiffusenessA.Text;
			nameValuePairs["DiffusenessB"] = TbxDiffusenessB.Text;
			nameValuePairs["EMFCalculationMethod"] = CbxEMFCalculationMethod.Text;
			nameValuePairs["EMFCalculationMethodSelection"] = MsxEMFCalculationMethodSelection.SelectionString;
			nameValuePairs["EffectiveTimeSamples"] = TbxEffectiveTimeSamples.Text;
			nameValuePairs["ImpactParam"] = TbxImpactParam.Text;
			nameValuePairs["LorentzFactor"] = TbxLorentzFactor.Text;
			nameValuePairs["NuclearRadiusA"] = TbxNuclearRadiusA.Text;
			nameValuePairs["NuclearRadiusB"] = TbxNuclearRadiusB.Text;
			nameValuePairs["NucleonNumberA"] = TbxNucleonNumberA.Text;
			nameValuePairs["NucleonNumberB"] = TbxNucleonNumberB.Text;
			nameValuePairs["Outfile"] = TbxOutfile.Text;
			nameValuePairs["ProtonNumberA"] = TbxProtonNumberA.Text;
			nameValuePairs["ProtonNumberB"] = TbxProtonNumberB.Text;
			nameValuePairs["QGPConductivityMeV"] = TbxQGPConductivityMeV.Text;
			nameValuePairs["RadialDistance"] = TbxRadialDistance.Text;
			nameValuePairs["ShapeFunctionA"] = CbxShapeFunctionA.Text;
			nameValuePairs["ShapeFunctionB"] = CbxShapeFunctionB.Text;
			nameValuePairs["StartEffectiveTime"] = TbxStartEffectiveTime.Text;
			nameValuePairs["StopEffectiveTime"] = TbxStopEffectiveTime.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxEMFCalculationMethod.Text = nameValuePairs["EMFCalculationMethod"].ToString();
			CbxShapeFunctionA.Text = nameValuePairs["ShapeFunctionA"].ToString();
			CbxShapeFunctionB.Text = nameValuePairs["ShapeFunctionB"].ToString();
			MsxEMFCalculationMethodSelection.SelectionString = nameValuePairs["EMFCalculationMethodSelection"].ToString();
			TbxDiffusenessA.Text = nameValuePairs["DiffusenessA"].ToString();
			TbxDiffusenessB.Text = nameValuePairs["DiffusenessB"].ToString();
			TbxEffectiveTimeSamples.Text = nameValuePairs["EffectiveTimeSamples"].ToString();
			TbxImpactParam.Text = nameValuePairs["ImpactParam"].ToString();
			TbxLorentzFactor.Text = nameValuePairs["LorentzFactor"].ToString();
			TbxNuclearRadiusA.Text = nameValuePairs["NuclearRadiusA"].ToString();
			TbxNuclearRadiusB.Text = nameValuePairs["NuclearRadiusB"].ToString();
			TbxNucleonNumberA.Text = nameValuePairs["NucleonNumberA"].ToString();
			TbxNucleonNumberB.Text = nameValuePairs["NucleonNumberB"].ToString();
			TbxOutfile.Text = nameValuePairs["Outfile"];
			TbxProtonNumberA.Text = nameValuePairs["ProtonNumberA"].ToString();
			TbxProtonNumberB.Text = nameValuePairs["ProtonNumberB"].ToString();
			TbxQGPConductivityMeV.Text = nameValuePairs["QGPConductivityMeV"].ToString();
			TbxRadialDistance.Text = nameValuePairs["RadialDistance"].ToString();
			TbxStartEffectiveTime.Text = nameValuePairs["StartEffectiveTime"].ToString();
			TbxStopEffectiveTime.Text = nameValuePairs["StopEffectiveTime"].ToString();
		}

		private void InitializeMenuEntry()
		{
			MenuEntry = new MenuItemElectromagnetism();
			MenuEntry.MenuItemPlotPointChargeAzimutalMagneticField.Click += new EventHandler(MenuItemPlotPointChargeAzimutalMagneticField_Click);
			MenuEntry.MenuItemPlotPointChargeLongitudinalElectricField.Click += new EventHandler(MenuItemPlotPointChargeLongitudinalElectricField_Click);
			MenuEntry.MenuItemPlotPointChargeRadialElectricField.Click += new EventHandler(MenuItemPlotPointChargeRadialElectricField_Click);
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
				LblOutfile, TbxOutfile);
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

		private void GbxOutput_Enter(object sender, EventArgs e)
		{

		}

		private void CtrlTextBoxLog_TextChanged(object sender, EventArgs e)
		{

		}

		private void VSplit_SplitterMoved(object sender, SplitterEventArgs e)
		{

		}

		private void LayoutBottom_Paint(object sender, PaintEventArgs e)
		{

		}

		private void GbxGeneralParameters_Enter(object sender, EventArgs e)
		{

		}

		private void LayoutGeneralParameters_Paint(object sender, PaintEventArgs e)
		{

		}

		private void CbxEMFCalculationMethod_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void LblEMFCalculationMethod_Click(object sender, EventArgs e)
		{

		}

		private void LblQGPConductivityMeV_Click(object sender, EventArgs e)
		{

		}

		private void TbxQGPConductivityMeV_TextChanged(object sender, EventArgs e)
		{

		}

		private void GbxSinglePointCharge_Enter(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void MsxEMFCalculationMethodSelection_Load(object sender, EventArgs e)
		{

		}

		private void LblEMFCalculationMethodSelection_Click(object sender, EventArgs e)
		{

		}

		private void LblEffectiveTimeSamples_Click(object sender, EventArgs e)
		{

		}

		private void TbxEffectiveTimeSamples_TextChanged(object sender, EventArgs e)
		{

		}

		private void TbxLorentzFactor_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblRadialDistance_Click(object sender, EventArgs e)
		{

		}

		private void TbxRadialDistance_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblStartEffectiveTime_Click(object sender, EventArgs e)
		{

		}

		private void TbxStartEffectiveTime_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblStopEffectiveTime_Click(object sender, EventArgs e)
		{

		}

		private void TbxStopEffectiveTime_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblLorentzFactor_Click(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void LblOutfile_Click(object sender, EventArgs e)
		{

		}

		private void TbxOutfile_TextChanged(object sender, EventArgs e)
		{

		}

		private void HSplit_SplitterMoved(object sender, SplitterEventArgs e)
		{

		}

		private void GbxGlauber_Enter(object sender, EventArgs e)
		{

		}

		private void CtrlStatusTrackingCtrl_Load(object sender, EventArgs e)
		{

		}

		private void LblShapeFunctionB_Click(object sender, EventArgs e)
		{

		}

		private void LayoutGlauber_Paint(object sender, PaintEventArgs e)
		{

		}

		private void LblShapeFunctionA_Click(object sender, EventArgs e)
		{

		}

		private void CbxShapeFunctionA_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void CbxShapeFunctionB_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void LblDiffusenessA_Click(object sender, EventArgs e)
		{

		}

		private void TbxDiffusenessA_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblDiffusenessB_Click(object sender, EventArgs e)
		{

		}

		private void TbxDiffusenessB_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblNucleonNumberA_Click(object sender, EventArgs e)
		{

		}

		private void TbxNucleonNumberA_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblNucleonNumberB_Click(object sender, EventArgs e)
		{

		}

		private void TbxNucleonNumberB_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblNuclearRadiusA_Click(object sender, EventArgs e)
		{

		}

		private void TbxNuclearRadiusA_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblNuclearRadiusB_Click(object sender, EventArgs e)
		{

		}

		private void TbxNuclearRadiusB_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblImpactParam_Click(object sender, EventArgs e)
		{

		}

		private void TbxImpactParam_TextChanged(object sender, EventArgs e)
		{

		}

		private void LblProtonNumberA_Click(object sender, EventArgs e)
		{

		}

		private void TbxProtonNumberA_TextChanged(object sender, EventArgs e)
		{

		}
	}
}