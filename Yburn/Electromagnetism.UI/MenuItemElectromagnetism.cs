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

		public ToolStripMenuItem MenuItemPlotPointChargeAzimutalMagneticField;
		public ToolStripMenuItem MenuItemPlotPointChargeLongitudinalElectricField;
		public ToolStripMenuItem MenuItemPlotPointChargeRadialElectricField;
		public ToolStripMenuItem MenuItemPlotSingleNucleusMagneticFieldStrength;
		public ToolStripMenuItem MenuItemPlotCentralMagneticFieldStrength;
		public ToolStripMenuItem MenuItemPlotAverageMagneticFieldStrength;
		public ToolStripMenuItem MenuItemPlotOrthoParaStateOverlap;

		private void InitializeComponent()
		{
			this.MenuItemPlotPointChargeAzimutalMagneticField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeLongitudinalElectricField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeRadialElectricField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotSingleNucleusMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotCentralMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotAverageMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotOrthoParaStateOverlap = new System.Windows.Forms.ToolStripMenuItem();
			//
			// MenuItemPlotPointChargeAzimutalMagneticField
			//
			this.MenuItemPlotPointChargeAzimutalMagneticField.Name = "MenuItemPlotPointChargeAzimutalMagneticField";
			this.MenuItemPlotPointChargeAzimutalMagneticField.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotPointChargeAzimutalMagneticField.Text = "Plot point charge &azimutal magnetic field";
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
			// MenuItemPlotSingleNucleusMagneticFieldStrength
			//
			this.MenuItemPlotSingleNucleusMagneticFieldStrength.Name = "MenuItemPlotSingleNucleusMagneticFieldStrength";
			this.MenuItemPlotSingleNucleusMagneticFieldStrength.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotSingleNucleusMagneticFieldStrength.Text = "Plot single &nucleus magnetic field strength";
			//
			// MenuItemPlotCentralMagneticFieldStrength
			//
			this.MenuItemPlotCentralMagneticFieldStrength.Name = "MenuItemPlotCentralMagneticFieldStrength";
			this.MenuItemPlotCentralMagneticFieldStrength.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotCentralMagneticFieldStrength.Text = "Plot &central magnetic field strength";
			//
			// MenuItemPlotAverageMagneticFieldStrength
			//
			this.MenuItemPlotAverageMagneticFieldStrength.Name = "MenuItemPlotAverageMagneticFieldStrength";
			this.MenuItemPlotAverageMagneticFieldStrength.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotAverageMagneticFieldStrength.Text = "Plot &average magnetic field strength";
			//
			// MenuItemPlotOrthoParaStateOverlap
			//
			this.MenuItemPlotOrthoParaStateOverlap.Name = "MenuItemPlotOrthoParaStateOverlap";
			this.MenuItemPlotOrthoParaStateOverlap.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotOrthoParaStateOverlap.Text = "Plot ortho/para state &overlap";
			//
			// MenuItemElectromagnetism
			//
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.MenuItemPlotPointChargeAzimutalMagneticField,
				this.MenuItemPlotPointChargeLongitudinalElectricField,
				this.MenuItemPlotPointChargeRadialElectricField,
				this.MenuItemPlotSingleNucleusMagneticFieldStrength,
				this.MenuItemPlotCentralMagneticFieldStrength,
				this.MenuItemPlotAverageMagneticFieldStrength,
			this.MenuItemPlotOrthoParaStateOverlap});
			this.Size = new System.Drawing.Size(84, 24);
			this.Text = "&Electromagnetism";
		}
	}
}