using System.Windows.Forms;

namespace Yburn.InMediumDecayWidth.UI
{
    public class MenuItemInMediumDecayWidth : ToolStripMenuItem
    {
        public MenuItemInMediumDecayWidth()
            : base()
        {
            InitializeComponent();
        }

        public ToolStripMenuItem MenuItemCalculateInMediumDecayWidths;
        public ToolStripMenuItem MenuItemPlotInMediumDecayWidths;

        private void InitializeComponent()
        {
            this.MenuItemCalculateInMediumDecayWidths = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPlotInMediumDecayWidths = new System.Windows.Forms.ToolStripMenuItem();
            //
            // MenuItemCalculateInMediumDecayWidths
            //
            this.MenuItemCalculateInMediumDecayWidths.Name = "MenuItemCalculateInMediumDecayWidths";
            this.MenuItemCalculateInMediumDecayWidths.Size = new System.Drawing.Size(361, 24);
            this.MenuItemCalculateInMediumDecayWidths.Text = "Ca&lculate in medium decay widths";
            //
            // MenuItemPlotInMediumDecayWidths
            //
            this.MenuItemPlotInMediumDecayWidths.Name = "MenuItemPlotInMediumDecayWidths";
            this.MenuItemPlotInMediumDecayWidths.Size = new System.Drawing.Size(361, 24);
            this.MenuItemPlotInMediumDecayWidths.Text = "&Plot in medium decay widths";
            //
            // MenuItemAverageDecayWidth
            //
            this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.MenuItemCalculateInMediumDecayWidths,
                this.MenuItemPlotInMediumDecayWidths});
            this.Size = new System.Drawing.Size(84, 24);
            this.Text = "InMedium&DecayWidth";
        }
    }
}