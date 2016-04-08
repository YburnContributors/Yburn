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
			MsxPotentialTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("PotentialType"));
			MsxBottomiumStates.AddItems(JobOrganizer.GetWorkerEnumEntries("BottomiumState"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["MinTemperature"] = TbxMinTemperature.Text;
			nameValuePairs["MaxTemperature"] = TbxMaxTemperature.Text;
			nameValuePairs["DecayWidthAveragingAngles"] = TbxDecayWidthAveragingAngles.Text;
			nameValuePairs["MediumVelocity"] = TbxMediumVelocity.Text;
			nameValuePairs["TemperatureStepSize"] = TbxTemperatureStepSize.Text;
			nameValuePairs["DecayWidthType"] = CbxDecayWidthType.Text;
			nameValuePairs["PotentialTypes"] = MsxPotentialTypes.SelectionString;
			nameValuePairs["BottomiumStates"] = MsxBottomiumStates.SelectionString;
			nameValuePairs["UseAveragedTemperature"] = ChkUseAveragedTemperature.Checked.ToString();
			nameValuePairs["Outfile"] = TbxOutfile.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			TbxMinTemperature.Text = nameValuePairs["MinTemperature"].ToString();
			TbxMaxTemperature.Text = nameValuePairs["MaxTemperature"].ToString();
			TbxMediumVelocity.Text = nameValuePairs["MediumVelocity"].ToString();
			TbxDecayWidthAveragingAngles.Text = nameValuePairs["DecayWidthAveragingAngles"].ToString();
			TbxTemperatureStepSize.Text = nameValuePairs["TemperatureStepSize"].ToString();
			CbxDecayWidthType.Text = nameValuePairs["DecayWidthType"].ToString();
			MsxPotentialTypes.SelectionString = nameValuePairs["PotentialTypes"];
			MsxBottomiumStates.SelectionString = nameValuePairs["BottomiumStates"];
			ChkUseAveragedTemperature.Checked = bool.Parse(nameValuePairs["UseAveragedTemperature"]);
			TbxOutfile.Text = nameValuePairs["Outfile"];
		}

		private void InitializeMenuEntry()
		{
			MenuEntry = new MenuItemInMediumDecayWidth();
			MenuEntry.MenuItemCalculateInMediumDecayWidths.Click += new EventHandler(MenuItemCalculateInMediumDecayWidths_Click);
			MenuEntry.MenuItemPlotInMediumDecayWidths.Click += new EventHandler(MenuItemPlotInMediumDecayWidths_Click);
		}

		private void MakeToolTips(
			ToolTipMaker toolTipMaker
			)
		{
			toolTipMaker.Add(
				"In medium bottomium decay widths depend on temperature, velocity and angle\r\n"
				+ "relative to the velocity vector. The decay width as a function of temperature and\r\n"
				+ "velocity is calculated as an average over the solid angle. The average is calculated\r\n"
				+ "from samples of dedicated azimuthal angles given by the user.",
				GbxAverageParams);
			toolTipMaker.Add(
				"Minimum temperature in MeV to be considered in the calculation.",
				LblMinTemperature, TbxMinTemperature);
			toolTipMaker.Add(
				"Maximum temperature in MeV to be considered in the calculation.",
				LblMaxTemperature, TbxMaxTemperature);
			toolTipMaker.Add(
				"Size of temperature steps between samples in MeV to be considered in the calculation.",
				LblTemperatureStepSize, TbxTemperatureStepSize);
			toolTipMaker.Add(
				"Relative velocity between the bottomium state and the QGP medium in units of c.",
				LblMediumVelocity, TbxMediumVelocity);
			toolTipMaker.Add(
				"Angles relative to the velocity vector from which the angular average is calculated.\r\n"
				+ "The angles are given by a comma separated list with values in degree.",
				LblDecayWidthAveragingAngles, TbxDecayWidthAveragingAngles);
			toolTipMaker.Add(
				"Instead of averaging the decay width, calculate the (exact) angular average of\r\n"
				+ "temperature and evaluate the decay width at that effective temperature.",
				LblUseAveragedTemperature, ChkUseAveragedTemperature);
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
				LblOutfile, TbxOutfile);
		}

		private void MenuItemCalculateInMediumDecayWidths_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateInMediumDecayWidth", ControlsValues);
		}

		private void MenuItemPlotInMediumDecayWidths_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotInMediumDecayWidth", ControlsValues);
		}
	}
}