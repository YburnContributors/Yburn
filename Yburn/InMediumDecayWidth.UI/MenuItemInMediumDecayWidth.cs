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
		public ToolStripMenuItem MenuItemPlotInMediumDecayWidthsVersusMediumTemperature;
		public ToolStripMenuItem MenuItemPlotInMediumDecayWidthsVersusMediumVelocity;

		private void InitializeComponent()
		{
			this.MenuItemCalculateInMediumDecayWidths = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity = new System.Windows.Forms.ToolStripMenuItem();
			//
			// MenuItemCalculateInMediumDecayWidths
			//
			this.MenuItemCalculateInMediumDecayWidths.Name = "MenuItemCalculateInMediumDecayWidths";
			this.MenuItemCalculateInMediumDecayWidths.Size = new System.Drawing.Size(361, 24);
			this.MenuItemCalculateInMediumDecayWidths.Text = "Ca&lculate in medium decay widths";
			//
			// MenuItemPlotInMediumDecayWidthsVersusMediumTemperature
			//
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature.Name = "MenuItemPlotInMediumDecayWidthsVersusMediumTemperature";
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature.Text = "Plot in medium decay widths versus medium &temperature";
			//
			// MenuItemPlotInMediumDecayWidthsVersusMediumVelocity
			//
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity.Name = "MenuItemPlotInMediumDecayWidthsVersusMediumVelocity";
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity.Text = "Plot in medium decay widths versus medium &velocity";
			//
			// MenuItemAverageDecayWidth
			//
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.MenuItemCalculateInMediumDecayWidths,
				this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature,
				this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity});
			this.Size = new System.Drawing.Size(84, 24);
			this.Text = "InMedium&DecayWidth";
		}
	}
}