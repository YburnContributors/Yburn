using System.Windows.Forms;

namespace Yburn.Electromagnetism.UI
{
	public class MenuItemElectromagnetism : ToolStripMenuItem
	{
		public MenuItemElectromagnetism()
			: base()
		{
			InitializeComponent();
		}

		public ToolStripMenuItem MenuItemPlotPointChargeAzimuthalMagneticField;
		public ToolStripMenuItem MenuItemPlotPointChargeLongitudinalElectricField;
		public ToolStripMenuItem MenuItemPlotPointChargeRadialElectricField;
		public ToolStripMenuItem MenuItemPlotPointChargeAndNucleusFieldComponents;
		public ToolStripMenuItem MenuItemPlotNucleusEMFStrengthInLCF;
		public ToolStripMenuItem MenuItemPlotCollisionalEMFStrengthVersusTime;
		public ToolStripMenuItem MenuItemPlotCollisionalEMFStrengthVersusImpactParam;
		public ToolStripMenuItem MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam;
		public ToolStripMenuItem MenuItemPlotEMFStrengthInTransversePlane;
		public ToolStripMenuItem MenuItemPlotAverageCollisionalEMFStrength;
		public ToolStripMenuItem MenuItemPlotAverageSpinStateOverlap;
		public ToolStripMenuItem MenuItemShowEMFNormalizationFactor;

		private void InitializeComponent()
		{
			this.MenuItemPlotPointChargeAzimuthalMagneticField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeLongitudinalElectricField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeRadialElectricField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeAndNucleusFieldComponents = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotNucleusEMFStrengthInLCF = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotCollisionalEMFStrengthVersusTime = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotCollisionalEMFStrengthVersusImpactParam = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotEMFStrengthInTransversePlane = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotAverageCollisionalEMFStrength = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotAverageSpinStateOverlap = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemShowEMFNormalizationFactor = new System.Windows.Forms.ToolStripMenuItem();
			//
			// MenuItemPlotPointChargeAzimuthalMagneticField
			//
			this.MenuItemPlotPointChargeAzimuthalMagneticField.Name = "MenuItemPlotPointChargeAzimuthalMagneticField";
			this.MenuItemPlotPointChargeAzimuthalMagneticField.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotPointChargeAzimuthalMagneticField.Text = "Plot point charge &azimuthal magnetic field";
			//
			// MenuItemPlotPointChargeLongitudinalElectricField
			//
			this.MenuItemPlotPointChargeLongitudinalElectricField.Name = "MenuItemPlotPointChargeLongitudinalElectricField";
			this.MenuItemPlotPointChargeLongitudinalElectricField.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotPointChargeLongitudinalElectricField.Text = "Plot point charge &longitudinal electric field";
			//
			// MenuItemPlotPointChargeRadialElectricField
			//
			this.MenuItemPlotPointChargeRadialElectricField.Name = "MenuItemPlotPointChargeRadialElectricField";
			this.MenuItemPlotPointChargeRadialElectricField.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotPointChargeRadialElectricField.Text = "Plot point charge &radial electric field";
			//
			// MenuItemPlotPointChargeAndNucleusFieldComponents
			//
			this.MenuItemPlotPointChargeAndNucleusFieldComponents.Name = "MenuItemPlotPointChargeAndNucleusFieldComponents";
			this.MenuItemPlotPointChargeAndNucleusFieldComponents.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotPointChargeAndNucleusFieldComponents.Text = "Plot point charge and nucleus field &components";
			//
			// MenuItemPlotNucleusEMFStrengthInLCF
			//
			this.MenuItemPlotNucleusEMFStrengthInLCF.Name = "MenuItemPlotNucleusEMFStrengthInLCF";
			this.MenuItemPlotNucleusEMFStrengthInLCF.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotNucleusEMFStrengthInLCF.Text = "Plot nucleus electromagnetic field strength in LC&F";
			//
			// MenuItemPlotCollisionalEMFStrengthVersusTime
			//
			this.MenuItemPlotCollisionalEMFStrengthVersusTime.Name = "MenuItemPlotCollisionalEMFStrengthVersusTime";
			this.MenuItemPlotCollisionalEMFStrengthVersusTime.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotCollisionalEMFStrengthVersusTime.Text = "Plot collisional electromagnetic field strength versus &time";
			//
			// MenuItemPlotCollisionalEMFStrengthVersusImpactParam
			//
			this.MenuItemPlotCollisionalEMFStrengthVersusImpactParam.Name = "MenuItemPlotCollisionalEMFStrengthVersusImpactParam";
			this.MenuItemPlotCollisionalEMFStrengthVersusImpactParam.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotCollisionalEMFStrengthVersusImpactParam.Text = "Plot collisional electromagnetic field strength versus &impact parameter";
			//
			// MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam
			//
			this.MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam.Name = "MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam";
			this.MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam.Text = "Plot collisional electromagnetic field strength versus time a&nd impact parameter";
			//
			// MenuItemPlotEMFStrengthInTransversePlane
			//
			this.MenuItemPlotEMFStrengthInTransversePlane.Name = "MenuItemPlotEMFStrengthInTransversePlane";
			this.MenuItemPlotEMFStrengthInTransversePlane.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotEMFStrengthInTransversePlane.Text = "Plot collisional electromagnetic field strength in transverse &plane";
			//
			// MenuItemPlotAverageCollisionalEMFStrength
			//
			this.MenuItemPlotAverageCollisionalEMFStrength.Name = "MenuItemPlotAverageCollisionalEMFStrength";
			this.MenuItemPlotAverageCollisionalEMFStrength.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotAverageCollisionalEMFStrength.Text = "Plot average collisional electromagnetic field strength for b&b̅ mesons";
			//
			// MenuItemPlotAverageSpinStateOverlap
			//
			this.MenuItemPlotAverageSpinStateOverlap.Name = "MenuItemPlotAverageSpinStateOverlap";
			this.MenuItemPlotAverageSpinStateOverlap.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotAverageSpinStateOverlap.Text = "Plot &overlap between magnetically shifted triplet and unshifted singlet spin state";
			//
			// MenuItemShowEMFNormalizationFactor
			//
			this.MenuItemShowEMFNormalizationFactor.Name = "MenuItemShowEMFNormalizationFactor";
			this.MenuItemShowEMFNormalizationFactor.Size = new System.Drawing.Size(361, 24);
			this.MenuItemShowEMFNormalizationFactor.Text = "&Show EMF normalization factor";
			//
			// MenuItemElectromagnetism
			//
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.MenuItemPlotPointChargeAzimuthalMagneticField,
				this.MenuItemPlotPointChargeLongitudinalElectricField,
				this.MenuItemPlotPointChargeRadialElectricField,
				this.MenuItemPlotPointChargeAndNucleusFieldComponents,
				this.MenuItemPlotNucleusEMFStrengthInLCF,
				this.MenuItemPlotCollisionalEMFStrengthVersusTime,
				this.MenuItemPlotCollisionalEMFStrengthVersusImpactParam,
				this.MenuItemPlotCollisionalEMFStrengthVersusTimeAndImpactParam,
				this.MenuItemPlotEMFStrengthInTransversePlane,
				this.MenuItemPlotAverageCollisionalEMFStrength,
				this.MenuItemPlotAverageSpinStateOverlap,
				this.MenuItemShowEMFNormalizationFactor});
			this.Size = new System.Drawing.Size(84, 24);
			this.Text = "&Electromagnetism";
		}
	}
}
