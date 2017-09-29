using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Yburn.Interfaces;
using Yburn.UI;

namespace Yburn.InMediumDecayWidth.UI
{
	public partial class InMediumDecayWidthPanel : UserControl
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public InMediumDecayWidthPanel()
			: base()
		{
			InitializeComponent();
			InitializeMenuEntry();

			this.Name = "InMediumDecayWidth";
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

		public MenuItemInMediumDecayWidth MenuEntry
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
			CbxDecayWidthType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("DecayWidthType"));
			CbxDecayWidthType.Items.Remove("None");
			CbxElectricDipoleAlignment.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ElectricDipoleAlignment"));
			MsxBottomiumStates.AddItems(JobOrganizer.GetWorkerEnumEntries("BottomiumState"));
			MsxDopplerShiftEvaluationTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("DopplerShiftEvaluationType"));
			MsxPotentialTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("PotentialType"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
			{
				["BottomiumStates"] = MsxBottomiumStates.SelectionString,
				["DataFileName"] = TbxDataFileName.Text,
				["DecayWidthType"] = CbxDecayWidthType.Text,
				["DopplerShiftEvaluationTypes"] = MsxDopplerShiftEvaluationTypes.SelectionString,
				["ElectricDipoleAlignment"] = CbxElectricDipoleAlignment.Text,
				["ElectricFieldStrength_per_fm2"] = TbxElectricFieldStrength.Text,
				["MagneticFieldStrength_per_fm2"] = TbxMagneticFieldStrength.Text,
				["MediumTemperatures_MeV"] = TbxMediumTemperatures.Text,
				["MediumVelocities"] = TbxMediumVelocities.Text,
				["NumberAveragingAngles"] = TbxNumberAveragingAngles.Text,
				["PotentialTypes"] = MsxPotentialTypes.SelectionString,
				["QGPFormationTemperature_MeV"] = TbxQGPFormationTemperature.Text
			};

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxDecayWidthType.Text = nameValuePairs["DecayWidthType"];
			CbxElectricDipoleAlignment.Text = nameValuePairs["ElectricDipoleAlignment"];
			MsxBottomiumStates.SelectionString = nameValuePairs["BottomiumStates"];
			MsxDopplerShiftEvaluationTypes.SelectionString = nameValuePairs["DopplerShiftEvaluationTypes"];
			MsxPotentialTypes.SelectionString = nameValuePairs["PotentialTypes"];
			TbxDataFileName.Text = nameValuePairs["DataFileName"];
			TbxElectricFieldStrength.Text = nameValuePairs["ElectricFieldStrength_per_fm2"];
			TbxMagneticFieldStrength.Text = nameValuePairs["MagneticFieldStrength_per_fm2"];
			TbxMediumTemperatures.Text = nameValuePairs["MediumTemperatures_MeV"];
			TbxMediumVelocities.Text = nameValuePairs["MediumVelocities"];
			TbxNumberAveragingAngles.Text = nameValuePairs["NumberAveragingAngles"];
			TbxQGPFormationTemperature.Text = nameValuePairs["QGPFormationTemperature_MeV"];
		}

		private void InitializeMenuEntry()
		{
			MenuEntry = new MenuItemInMediumDecayWidth();
			MenuEntry.MenuItemCalculateInMediumDecayWidths.Click += new EventHandler(MenuItemCalculateInMediumDecayWidths_Click);
			MenuEntry.MenuItemPlotDecayWidthsFromQQDataFile.Click += new EventHandler(MenuItemPlotDecayWidthsFromQQDataFile_Click);
			MenuEntry.MenuItemPlotEnergiesFromQQDataFile.Click += new EventHandler(MenuItemPlotEnergiesFromQQDataFile_Click);
			MenuEntry.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature.Click += new EventHandler(MenuItemPlotInMediumDecayWidthsVersusMediumTemperature_Click);
			MenuEntry.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity.Click += new EventHandler(MenuItemPlotInMediumDecayWidthsVersusMediumVelocity_Click);
			MenuEntry.MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature.Click += new EventHandler(MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature_Click);
			MenuEntry.MenuItemPlotElectromagneticallyShiftedEnergies.Click += new EventHandler(MenuItemPlotElectromagneticallyShiftedEnergies_Click);
		}

		private void MakeToolTips(
			ToolTipMaker toolTipMaker
			)
		{
			toolTipMaker.Add(
				"In-medium bottomium decay widths depend on temperature, velocity and angle\r\n"
				+ "relative to the velocity vector. The decay width as a function of temperature and\r\n"
				+ "velocity is calculated as an average over the solid angle. The average is calculated\r\n"
				+ "from samples of dedicated azimuthal angles given by the user.",
				GbxGeneralParams);
			toolTipMaker.Add(
				"Temperatures in MeV to be considered in the calculation.",
				LblMediumTemperatures, TbxMediumTemperatures);
			toolTipMaker.Add(
				"Velocities of the QGP medium in the bottomium rest frame in units of c.",
				LblMediumVelocities, TbxMediumVelocities);
			toolTipMaker.Add(
				"Number of angles relative to the velocity vector from which the angular average is calculated.",
				LblNumberAveragingAngles, TbxNumberAveragingAngles);
			toolTipMaker.Add(
				"DopplerShiftEvaluationTypes to be considered in the calculation.",
				LblDopplerShiftEvaluationTypes, MsxDopplerShiftEvaluationTypes);
			toolTipMaker.Add(
				"DecayWidthType to be considered in the calculation.",
				LblDecayWidthType, CbxDecayWidthType);
			toolTipMaker.Add(
				"PotentialTypes to be considered in the calculation.",
				LblPotentialTypes, MsxPotentialTypes);
			toolTipMaker.Add(
				"BottomiumStates to be considered in the calculation.",
				LblBottomiumStates, MsxBottomiumStates);
			toolTipMaker.Add(
				"Name of the output file. The standard output path can be set\r\n"
				+ "in the menu \"File\" using \"Set output path\".",
				LblDataFileName, TbxDataFileName);
			toolTipMaker.Add(
				"Critical temperature for the formation of the quark-gluon medium.",
				LblQGPFormationTemperature);
		}

		private void MenuItemCalculateInMediumDecayWidths_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateInMediumDecayWidth", ControlsValues);
		}

		private void MenuItemPlotDecayWidthsFromQQDataFile_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotDecayWidthsFromQQDataFile", ControlsValues);
		}

		private void MenuItemPlotEnergiesFromQQDataFile_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotEnergiesFromQQDataFile", ControlsValues);
		}

		private void MenuItemPlotInMediumDecayWidthsVersusMediumTemperature_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotInMediumDecayWidthsVersusMediumTemperature", ControlsValues);
		}

		private void MenuItemPlotInMediumDecayWidthsVersusMediumVelocity_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotInMediumDecayWidthsVersusMediumVelocity", ControlsValues);
		}

		private void MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotDecayWidthEvaluatedAtDopplerShiftedTemperature", ControlsValues);
		}

		private void MenuItemPlotElectromagneticallyShiftedEnergies_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotElectromagneticallyShiftedEnergies", ControlsValues);
		}
	}
}
