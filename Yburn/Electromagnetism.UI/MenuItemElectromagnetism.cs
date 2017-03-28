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
		public ToolStripMenuItem MenuItemPlotPointChargeAndNucleusEMF;
		public ToolStripMenuItem MenuItemPlotNucleusMagneticFieldStrengthInLCF;
		public ToolStripMenuItem MenuItemPlotCentralMagneticFieldStrength;
		public ToolStripMenuItem MenuItemPlotEMFStrengthInTransversePlane;
		public ToolStripMenuItem MenuItemPlotAverageElectricFieldStrength;
		public ToolStripMenuItem MenuItemPlotAverageMagneticFieldStrength;
		public ToolStripMenuItem MenuItemPlotAverageSpinStateOverlap;

		private void InitializeComponent()
		{
			this.MenuItemPlotPointChargeAzimuthalMagneticField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeLongitudinalElectricField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeRadialElectricField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeAndNucleusEMF = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotNucleusMagneticFieldStrengthInLCF = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotCentralMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotEMFStrengthInTransversePlane = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotAverageElectricFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotAverageMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotAverageSpinStateOverlap = new System.Windows.Forms.ToolStripMenuItem();
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
			// MenuItemPlotPointChargeAndNucleusEMF
			//
			this.MenuItemPlotPointChargeAndNucleusEMF.Name = "MenuItemPlotPointChargeAndNucleusEMF";
			this.MenuItemPlotPointChargeAndNucleusEMF.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotPointChargeAndNucleusEMF.Text = "Plot point charge and nucleus &field components";
			//
			// MenuItemPlotNucleusMagneticFieldStrengthInLCF
			//
			this.MenuItemPlotNucleusMagneticFieldStrengthInLCF.Name = "MenuItemPlotNucleusMagneticFieldStrengthInLCF";
			this.MenuItemPlotNucleusMagneticFieldStrengthInLCF.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotNucleusMagneticFieldStrengthInLCF.Text = "Plot &nucleus magnetic field strength in LCF";
			//
			// MenuItemPlotCentralMagneticFieldStrength
			//
			this.MenuItemPlotCentralMagneticFieldStrength.Name = "MenuItemPlotCentralMagneticFieldStrength";
			this.MenuItemPlotCentralMagneticFieldStrength.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotCentralMagneticFieldStrength.Text = "Plot &central magnetic field strength";
			//
			// MenuItemPlotEMFStrengthInTransversePlane
			//
			this.MenuItemPlotEMFStrengthInTransversePlane.Name = "MenuItemPlotEMFStrengthInTransversePlane";
			this.MenuItemPlotEMFStrengthInTransversePlane.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotEMFStrengthInTransversePlane.Text = "Plot electromagnetic field strength in &transverse plane";
			//
			// MenuItemPlotAverageElectricFieldStrength
			//
			this.MenuItemPlotAverageElectricFieldStrength.Name = "MenuItemPlotAverageElectricFieldStrength";
			this.MenuItemPlotAverageElectricFieldStrength.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotAverageElectricFieldStrength.Text = "Plot average &electric field strength for bb mesons";
			//
			// MenuItemPlotAverageMagneticFieldStrength
			//
			this.MenuItemPlotAverageMagneticFieldStrength.Name = "MenuItemPlotAverageMagneticFieldStrength";
			this.MenuItemPlotAverageMagneticFieldStrength.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotAverageMagneticFieldStrength.Text = "Plot average &magnetic field strength for bb mesons";
			//
			// MenuItemPlotAverageSpinStateOverlap
			//
			this.MenuItemPlotAverageSpinStateOverlap.Name = "MenuItemPlotAverageSpinStateOverlap";
			this.MenuItemPlotAverageSpinStateOverlap.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotAverageSpinStateOverlap.Text = "Plot &overlap between magnetically shifted triplet and unshifted singlet spin state";
			//
			// MenuItemElectromagnetism
			//
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.MenuItemPlotPointChargeAzimuthalMagneticField,
				this.MenuItemPlotPointChargeLongitudinalElectricField,
				this.MenuItemPlotPointChargeRadialElectricField,
				this.MenuItemPlotPointChargeAndNucleusEMF,
				this.MenuItemPlotNucleusMagneticFieldStrengthInLCF,
				this.MenuItemPlotCentralMagneticFieldStrength,
				this.MenuItemPlotEMFStrengthInTransversePlane,
				this.MenuItemPlotAverageElectricFieldStrength,
				this.MenuItemPlotAverageMagneticFieldStrength,
				this.MenuItemPlotAverageSpinStateOverlap});
			this.Size = new System.Drawing.Size(84, 24);
			this.Text = "&Electromagnetism";
		}
	}
}