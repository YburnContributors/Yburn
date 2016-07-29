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
        public ToolStripMenuItem MenuItemPlotPointChargeAndNucleusFieldComponents;
        public ToolStripMenuItem MenuItemPlotNucleusMagneticFieldStrength;
        public ToolStripMenuItem MenuItemPlotCentralMagneticFieldStrength;
        public ToolStripMenuItem MenuItemPlotAverageMagneticFieldStrength;
        public ToolStripMenuItem MenuItemPlotSpinStateOverlap;

        private void InitializeComponent()
        {
            this.MenuItemPlotPointChargeAzimutalMagneticField = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotPointChargeLongitudinalElectricField = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotPointChargeRadialElectricField = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotPointChargeAndNucleusFieldComponents = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotNucleusMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotCentralMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotAverageMagneticFieldStrength = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotSpinStateOverlap = new System.Windows.Forms.ToolStripMenuItem();
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
            // MenuItemPlotPointChargeAndNucleusFieldComponents
            //
            this.MenuItemPlotPointChargeAndNucleusFieldComponents.Name = "MenuItemPlotPointChargeAndNucleusFieldComponents";
            this.MenuItemPlotPointChargeAndNucleusFieldComponents.Size = new System.Drawing.Size(361, 24);
            this.MenuItemPlotPointChargeAndNucleusFieldComponents.Text = "Plot point charge and nucleus &field components";
            //
            // MenuItemPlotNucleusMagneticFieldStrength
            //
            this.MenuItemPlotNucleusMagneticFieldStrength.Name = "MenuItemPlotNucleusMagneticFieldStrength";
            this.MenuItemPlotNucleusMagneticFieldStrength.Size = new System.Drawing.Size(361, 24);
            this.MenuItemPlotNucleusMagneticFieldStrength.Text = "Plot &nucleus magnetic field strength";
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
            this.MenuItemPlotAverageMagneticFieldStrength.Text = "Plot &average magnetic field strength for bb mesons";
            //
            // MenuItemPlotSpinStateOverlap
            //
            this.MenuItemPlotSpinStateOverlap.Name = "MenuItemPlotSpinStateOverlap";
            this.MenuItemPlotSpinStateOverlap.Size = new System.Drawing.Size(361, 24);
            this.MenuItemPlotSpinStateOverlap.Text = "Plot &overlap between shifted \u03A5 and unshifted \u03B7b spin states";
            //
            // MenuItemElectromagnetism
            //
            this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.MenuItemPlotPointChargeAzimutalMagneticField,
                this.MenuItemPlotPointChargeLongitudinalElectricField,
                this.MenuItemPlotPointChargeRadialElectricField,
                this.MenuItemPlotPointChargeAndNucleusFieldComponents,
                this.MenuItemPlotNucleusMagneticFieldStrength,
                this.MenuItemPlotCentralMagneticFieldStrength,
                this.MenuItemPlotAverageMagneticFieldStrength,
            this.MenuItemPlotSpinStateOverlap});
            this.Size = new System.Drawing.Size(84, 24);
            this.Text = "&Electromagnetism";
        }
    }
}