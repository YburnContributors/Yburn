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
			MsxBottomiumStates.AddItems(JobOrganizer.GetWorkerEnumEntries("BottomiumState"));
			MsxDecayWidthEvaluationTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("DecayWidthEvaluationType"));
			MsxPotentialTypes.AddItems(JobOrganizer.GetWorkerEnumEntries("PotentialType"));
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["BottomiumStates"] = MsxBottomiumStates.SelectionString;
			nameValuePairs["DataFileName"] = TbxDataFileName.Text;
			nameValuePairs["NumberAveragingAngles"] = TbxNumberAveragingAngles.Text;
			nameValuePairs["DecayWidthEvaluationTypes"] = MsxDecayWidthEvaluationTypes.SelectionString;
			nameValuePairs["DecayWidthType"] = CbxDecayWidthType.Text;
			nameValuePairs["MaxTemperature"] = TbxMaxTemperature.Text;
			nameValuePairs["MediumVelocity"] = TbxMediumVelocity.Text;
			nameValuePairs["MinTemperature"] = TbxMinTemperature.Text;
			nameValuePairs["PotentialTypes"] = MsxPotentialTypes.SelectionString;
			nameValuePairs["QGPFormationTemperature"] = TbxQGPFormationTemperature.Text;
			nameValuePairs["TemperatureStepSize"] = TbxTemperatureStepSize.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxDecayWidthType.Text = nameValuePairs["DecayWidthType"];
			MsxBottomiumStates.SelectionString = nameValuePairs["BottomiumStates"];
			MsxDecayWidthEvaluationTypes.SelectionString = nameValuePairs["DecayWidthEvaluationTypes"];
			MsxPotentialTypes.SelectionString = nameValuePairs["PotentialTypes"];
			TbxDataFileName.Text = nameValuePairs["DataFileName"];
			TbxNumberAveragingAngles.Text = nameValuePairs["NumberAveragingAngles"];
			TbxMaxTemperature.Text = nameValuePairs["MaxTemperature"];
			TbxMediumVelocity.Text = nameValuePairs["MediumVelocity"];
			TbxMinTemperature.Text = nameValuePairs["MinTemperature"];
			TbxQGPFormationTemperature.Text = nameValuePairs["QGPFormationTemperature"];
			TbxTemperatureStepSize.Text = nameValuePairs["TemperatureStepSize"];
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
				"Velocity of the QGP medium in the bottomium rest frame in units of c.",
				LblMediumVelocity, TbxMediumVelocity);
			toolTipMaker.Add(
				"Number of angles relative to the velocity vector from which the angular average is calculated.",
				LblNumberAveragingAngles, TbxNumberAveragingAngles);
			toolTipMaker.Add(
				"DecayWidthEvaluationTypes to be considered in the calculation.",
				LblDecayWidthEvaluationTypes, MsxDecayWidthEvaluationTypes);
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

		private void MenuItemPlotInMediumDecayWidths_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotInMediumDecayWidth", ControlsValues);
		}
	}
}