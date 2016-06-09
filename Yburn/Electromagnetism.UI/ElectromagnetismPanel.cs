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
			CbxShapeFunctionTypeA.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ShapeFunction"));
			CbxShapeFunctionTypeB.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ShapeFunction"));
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
			nameValuePairs["GridCellSize"] = TbxGridCellSize.Text;
			nameValuePairs["GridRadius"] = TbxGridRadius.Text;
			nameValuePairs["ImpactParam"] = TbxImpactParam.Text;
			nameValuePairs["PointChargeVelocity"] = TbxPointChargeVelocity.Text;
			nameValuePairs["NuclearRadiusA"] = TbxNuclearRadiusA.Text;
			nameValuePairs["NuclearRadiusB"] = TbxNuclearRadiusB.Text;
			nameValuePairs["NucleonNumberA"] = TbxNucleonNumberA.Text;
			nameValuePairs["NucleonNumberB"] = TbxNucleonNumberB.Text;
			nameValuePairs["Outfile"] = TbxOutfile.Text;
			nameValuePairs["ProtonNumberA"] = TbxProtonNumberA.Text;
			nameValuePairs["ProtonNumberB"] = TbxProtonNumberB.Text;
			nameValuePairs["QGPConductivityMeV"] = TbxQGPConductivityMeV.Text;
			nameValuePairs["RadialDistance"] = TbxRadialDistance.Text;
			nameValuePairs["ShapeFunctionTypeA"] = CbxShapeFunctionTypeA.Text;
			nameValuePairs["ShapeFunctionTypeB"] = CbxShapeFunctionTypeB.Text;
			nameValuePairs["StartEffectiveTime"] = TbxStartEffectiveTime.Text;
			nameValuePairs["StopEffectiveTime"] = TbxStopEffectiveTime.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxEMFCalculationMethod.Text = nameValuePairs["EMFCalculationMethod"].ToString();
			CbxShapeFunctionTypeA.Text = nameValuePairs["ShapeFunctionTypeA"].ToString();
			CbxShapeFunctionTypeB.Text = nameValuePairs["ShapeFunctionTypeB"].ToString();
			MsxEMFCalculationMethodSelection.SelectionString = nameValuePairs["EMFCalculationMethodSelection"].ToString();
			TbxDiffusenessA.Text = nameValuePairs["DiffusenessA"].ToString();
			TbxDiffusenessB.Text = nameValuePairs["DiffusenessB"].ToString();
			TbxEffectiveTimeSamples.Text = nameValuePairs["EffectiveTimeSamples"].ToString();
			TbxGridCellSize.Text = nameValuePairs["GridCellSize"].ToString();
			TbxGridRadius.Text = nameValuePairs["GridRadius"].ToString();
			TbxImpactParam.Text = nameValuePairs["ImpactParam"].ToString();
			TbxPointChargeVelocity.Text = nameValuePairs["PointChargeVelocity"].ToString();
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
			MenuEntry.MenuItemPlotCentralMagneticFieldStrength.Click += new EventHandler(MenuItemPlotCentralMagneticFieldStrength_Click);
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

		private void MenuItemPlotCentralMagneticFieldStrength_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotCentralMagneticFieldStrength", ControlsValues);
		}
	}
}