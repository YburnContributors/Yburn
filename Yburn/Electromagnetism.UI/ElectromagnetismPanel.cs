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
			MsxEMFCalculationMethodSelection.AddItems(JobOrganizer.GetWorkerEnumEntries("EMFCalculationMethod"));
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
			nameValuePairs["ProtonNumberA"] = TbxProtonNumberA.Text;
			nameValuePairs["ProtonNumberB"] = TbxProtonNumberB.Text;
			nameValuePairs["ImpactParam"] = TbxImpactParam.Text;
			nameValuePairs["QGPConductivityMeV"] = TbxQGPConductivityMeV.Text;
			nameValuePairs["EMFCalculationMethod"] = CbxEMFCalculationMethod.Text;
			nameValuePairs["MinFourierFrequency"] = TbxMinFourierFrequency.Text;
			nameValuePairs["MaxFourierFrequency"] = TbxMaxFourierFrequency.Text;
			nameValuePairs["FourierFrequencySteps"] = TbxFourierFrequencySteps.Text;
			nameValuePairs["LorentzFactor"] = TbxLorentzFactor.Text;
			nameValuePairs["RadialDistance"] = TbxRadialDistance.Text;
			nameValuePairs["StartEffectiveTime"] = TbxStartEffectiveTime.Text;
			nameValuePairs["StopEffectiveTime"] = TbxStopEffectiveTime.Text;
			nameValuePairs["EffectiveTimeSamples"] = TbxEffectiveTimeSamples.Text;
			nameValuePairs["EMFCalculationMethodSelection"] = MsxEMFCalculationMethodSelection.SelectionString;
			nameValuePairs["Outfile"] = TbxOutfile.Text;

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
			TbxProtonNumberA.Text = nameValuePairs["ProtonNumberA"].ToString();
			TbxProtonNumberB.Text = nameValuePairs["ProtonNumberB"].ToString();
			TbxImpactParam.Text = nameValuePairs["ImpactParam"].ToString();
			TbxQGPConductivityMeV.Text = nameValuePairs["QGPConductivityMeV"].ToString();
			CbxEMFCalculationMethod.Text = nameValuePairs["EMFCalculationMethod"].ToString();
			TbxMinFourierFrequency.Text = nameValuePairs["MinFourierFrequency"].ToString();
			TbxMaxFourierFrequency.Text = nameValuePairs["MaxFourierFrequency"].ToString();
			TbxFourierFrequencySteps.Text = nameValuePairs["FourierFrequencySteps"].ToString();
			TbxLorentzFactor.Text = nameValuePairs["LorentzFactor"].ToString();
			TbxRadialDistance.Text = nameValuePairs["RadialDistance"].ToString();
			TbxStartEffectiveTime.Text = nameValuePairs["StartEffectiveTime"].ToString();
			TbxStopEffectiveTime.Text = nameValuePairs["StopEffectiveTime"].ToString();
			TbxEffectiveTimeSamples.Text = nameValuePairs["EffectiveTimeSamples"].ToString();
			MsxEMFCalculationMethodSelection.SelectionString = nameValuePairs["EMFCalculationMethodSelection"].ToString();
			TbxOutfile.Text = nameValuePairs["Outfile"];
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
				"The electromagnetic fields are calculated via Fourier synthesis from their\r\n"
				+ "counterparts in Fourier space. To perform this numerically, one has to choose\r\n"
				+ "cutoffs and a discretization of the unbounded Fourier integral.",
				GbxFourierSynthesis);
			toolTipMaker.Add(
				"Chosen approximation for the calculation of the electromagnetic field.",
				LblEMFCalculationMethod, CbxEMFCalculationMethod);
			toolTipMaker.Add(
				"Minimum Fourier frequency in 1/fm to be considered in the Fourier synthesis.",
				LblMinFourierFrequency, TbxMinFourierFrequency);
			toolTipMaker.Add(
				"Maximum Fourier frequency in 1/fm to be considered in the Fourier synthesis.",
				LblMaxFourierFrequency, TbxMaxFourierFrequency);
			toolTipMaker.Add(
				"Number of Fourier frequency steps to be considered in the Fourier synthesis.",
				LblFourierFrequencySteps, TbxFourierFrequencySteps);
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
	}
}