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

		private void InitializeComponent()
		{
			this.MenuItemPlotPointChargeAzimutalMagneticField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeLongitudinalElectricField = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPointChargeRadialElectricField = new System.Windows.Forms.ToolStripMenuItem();
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
			// MenuItemElectromagnetism
			//
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.MenuItemPlotPointChargeAzimutalMagneticField,
				this.MenuItemPlotPointChargeLongitudinalElectricField,
				this.MenuItemPlotPointChargeRadialElectricField});
			this.Size = new System.Drawing.Size(84, 24);
			this.Text = "Electro&magnetism";
		}
	}
}