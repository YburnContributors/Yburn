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
		public ToolStripMenuItem MenuItemPlotDecayWidthsFromQQDataFile;
		public ToolStripMenuItem MenuItemPlotEnergiesFromQQDataFile;
		public ToolStripMenuItem MenuItemPlotInMediumDecayWidthsVersusMediumTemperature;
		public ToolStripMenuItem MenuItemPlotInMediumDecayWidthsVersusMediumVelocity;
		public ToolStripMenuItem MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature;

		private void InitializeComponent()
		{
			this.MenuItemCalculateInMediumDecayWidths = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotDecayWidthsFromQQDataFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotEnergiesFromQQDataFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature = new System.Windows.Forms.ToolStripMenuItem();
			//
			// MenuItemCalculateInMediumDecayWidths
			//
			this.MenuItemCalculateInMediumDecayWidths.Name = "MenuItemCalculateInMediumDecayWidths";
			this.MenuItemCalculateInMediumDecayWidths.Size = new System.Drawing.Size(361, 24);
			this.MenuItemCalculateInMediumDecayWidths.Text = "Ca&lculate in-medium decay widths";
			//
			// MenuItemPlotDecayWidthsFromQQDataFile
			//
			this.MenuItemPlotDecayWidthsFromQQDataFile.Name = "MenuItemPlotDecayWidthsFromQQDataFile";
			this.MenuItemPlotDecayWidthsFromQQDataFile.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotDecayWidthsFromQQDataFile.Text = "Plot &decay widths from QQDataFile";
			//
			// MenuItemPlotEnergiesFromQQDataFile
			//
			this.MenuItemPlotEnergiesFromQQDataFile.Name = "MenuItemPlotEnergiesFromQQDataFile";
			this.MenuItemPlotEnergiesFromQQDataFile.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotEnergiesFromQQDataFile.Text = "Plot &energies from QQDataFile";
			//
			// MenuItemPlotInMediumDecayWidthsVersusMediumTemperature
			//
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature.Name = "MenuItemPlotInMediumDecayWidthsVersusMediumTemperature";
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature.Text = "Plot in-medium decay widths versus medium &temperature";
			//
			// MenuItemPlotInMediumDecayWidthsVersusMediumVelocity
			//
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity.Name = "MenuItemPlotInMediumDecayWidthsVersusMediumVelocity";
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity.Text = "Plot in-medium decay widths versus medium &velocity";
			//
			// MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature
			//
			this.MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature.Name = "MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature";
			this.MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature.Size = new System.Drawing.Size(361, 24);
			this.MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature.Text = "Plot in-medium decay widths evaluated at Doppler-&shifted temperature";
			//
			// MenuItemAverageDecayWidth
			//
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.MenuItemCalculateInMediumDecayWidths,
				this.MenuItemPlotDecayWidthsFromQQDataFile,
				this.MenuItemPlotEnergiesFromQQDataFile,
				this.MenuItemPlotInMediumDecayWidthsVersusMediumTemperature,
				this.MenuItemPlotInMediumDecayWidthsVersusMediumVelocity,
				this.MenuItemPlotDecayWidthEvaluatedAtDopplerShiftedTemperature});
			this.Size = new System.Drawing.Size(84, 24);
			this.Text = "InMedium&DecayWidth";
		}
	}
}