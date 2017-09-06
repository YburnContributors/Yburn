﻿using System;
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
		}

		private Dictionary<string, string> GetControlsValues()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			nameValuePairs["DiffusenessA_fm"] = TbxDiffusenessA.Text;
			nameValuePairs["DiffusenessB_fm"] = TbxDiffusenessB.Text;
			nameValuePairs["EMFCalculationMethod"] = CbxEMFCalculationMethod.Text;
			nameValuePairs["EMFQuadratureOrder"] = TbxEMFQuadratureOrder.Text;
			nameValuePairs["GridCellSize_fm"] = TbxGridCellSize.Text;
			nameValuePairs["GridRadius_fm"] = TbxGridRadius.Text;
			nameValuePairs["ImpactParameter_fm"] = TbxImpactParameter.Text;
			nameValuePairs["ParticleRapidity"] = TbxParticleRapidity.Text;
			nameValuePairs["NuclearRadiusA_fm"] = TbxNuclearRadiusA.Text;
			nameValuePairs["NuclearRadiusB_fm"] = TbxNuclearRadiusB.Text;
			nameValuePairs["NucleonNumberA"] = TbxNucleonNumberA.Text;
			nameValuePairs["NucleonNumberB"] = TbxNucleonNumberB.Text;
			nameValuePairs["DataFileName"] = TbxDataFileName.Text;
			nameValuePairs["ProtonNumberA"] = TbxProtonNumberA.Text;
			nameValuePairs["ProtonNumberB"] = TbxProtonNumberB.Text;
			nameValuePairs["QGPConductivity_MeV"] = TbxQGPConductivity.Text;
			nameValuePairs["RadialDistance_fm"] = TbxRadialDistance.Text;
			nameValuePairs["NucleusShapeA"] = CbxNucleusShapeA.Text;
			nameValuePairs["NucleusShapeB"] = CbxNucleusShapeB.Text;
			nameValuePairs["Samples"] = TbxSamples.Text;
			nameValuePairs["StartTime_fm"] = TbxStartTime.Text;
			nameValuePairs["StopTime_fm"] = TbxStopTime.Text;

			return nameValuePairs;
		}

		private void SetControlsValues(
			Dictionary<string, string> nameValuePairs
			)
		{
			CbxEMFCalculationMethod.Text = nameValuePairs["EMFCalculationMethod"];
			CbxNucleusShapeA.Text = nameValuePairs["NucleusShapeA"];
			CbxNucleusShapeB.Text = nameValuePairs["NucleusShapeB"];
			TbxDiffusenessA.Text = nameValuePairs["DiffusenessA_fm"];
			TbxDiffusenessB.Text = nameValuePairs["DiffusenessB_fm"];
			TbxEMFQuadratureOrder.Text = nameValuePairs["EMFQuadratureOrder"];
			TbxGridCellSize.Text = nameValuePairs["GridCellSize_fm"];
			TbxGridRadius.Text = nameValuePairs["GridRadius_fm"];
			TbxImpactParameter.Text = nameValuePairs["ImpactParameter_fm"];
			TbxParticleRapidity.Text = nameValuePairs["ParticleRapidity"];
			TbxNuclearRadiusA.Text = nameValuePairs["NuclearRadiusA_fm"];
			TbxNuclearRadiusB.Text = nameValuePairs["NuclearRadiusB_fm"];
			TbxNucleonNumberA.Text = nameValuePairs["NucleonNumberA"];
			TbxNucleonNumberB.Text = nameValuePairs["NucleonNumberB"];
			TbxDataFileName.Text = nameValuePairs["DataFileName"];
			TbxProtonNumberA.Text = nameValuePairs["ProtonNumberA"];
			TbxProtonNumberB.Text = nameValuePairs["ProtonNumberB"];
			TbxQGPConductivity.Text = nameValuePairs["QGPConductivity_MeV"];
			TbxRadialDistance.Text = nameValuePairs["RadialDistance_fm"];
			TbxSamples.Text = nameValuePairs["Samples"];
			TbxStartTime.Text = nameValuePairs["StartTime_fm"];
			TbxStopTime.Text = nameValuePairs["StopTime_fm"];
		}

		private void InitializeMenuEntry()
		{
			MenuEntry = new MenuItemElectromagnetism();
			MenuEntry.MenuItemPlotPointChargeAzimuthalMagneticField.Click += new EventHandler(MenuItemPlotPointChargeAzimuthalMagneticField_Click);
			MenuEntry.MenuItemPlotPointChargeLongitudinalElectricField.Click += new EventHandler(MenuItemPlotPointChargeLongitudinalElectricField_Click);
			MenuEntry.MenuItemPlotPointChargeRadialElectricField.Click += new EventHandler(MenuItemPlotPointChargeRadialElectricField_Click);
			MenuEntry.MenuItemPlotPointChargeAndNucleusFieldComponents.Click += new EventHandler(MenuItemPlotPointChargeAndNucleusFieldComponents_Click);
			MenuEntry.MenuItemPlotNucleusEMFStrengthInLCF.Click += new EventHandler(MenuItemPlotNucleusEMFStrengthInLCF_Click);
			MenuEntry.MenuItemPlotCollisionalEMFStrengthVersusTime.Click += new EventHandler(MenuItemPlotCollisionalEMFStrengthVersusTime_Click);
			MenuEntry.MenuItemPlotCollisionalEMFStrengthVersusImpactParam.Click += new EventHandler(MenuItemPlotCollisionalEMFStrengthVersusImpactParam_Click);
			MenuEntry.MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam.Click += new EventHandler(MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam_Click);
			MenuEntry.MenuItemPlotEMFStrengthInTransversePlane.Click += new EventHandler(MenuItemPlotEMFStrengthInTransversePlane_Click);
			MenuEntry.MenuItemPlotAverageCollisionalEMFStrength.Click += new EventHandler(MenuItemPlotAverageCollisionalEMFStrength_Click);
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

		private void MenuItemPlotPointChargeAzimuthalMagneticField_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotPointChargeAzimuthalMagneticField", ControlsValues);
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

		private void MenuItemPlotNucleusEMFStrengthInLCF_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotNucleusEMFStrengthInLCF", ControlsValues);
		}

		private void MenuItemPlotCollisionalEMFStrengthVersusTime_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotCollisionalEMFStrengthVersusTime", ControlsValues);
		}

		private void MenuItemPlotCollisionalEMFStrengthVersusImpactParam_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotCollisionalEMFStrengthVersusImpactParam", ControlsValues);
		}

		private void MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotCollisionalEMFStrengthVersusTimeAndImpactParam", ControlsValues);
		}

		private void MenuItemPlotEMFStrengthInTransversePlane_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotEMFStrengthInTransversePlane", ControlsValues);
		}

		private void MenuItemPlotAverageCollisionalEMFStrength_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotAverageCollisionalEMFStrength", ControlsValues);
		}

		private void MenuItemPlotAverageSpinStateOverlap_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotAverageSpinStateOverlap", ControlsValues);
		}
	}
}
