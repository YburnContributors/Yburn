using System.Drawing;
using System.Windows.Forms;

namespace Yburn.SingleQQ
{
   public class MenuItemSingleQQ : ToolStripMenuItem
   {
      public MenuItemSingleQQ()
         : base()
      {
         InitializeComponent();
      }

      public ToolStripMenuItem MenuItemCalculateBoundWave;
      public ToolStripMenuItem MenuItemCalculateFreeWave;
      public ToolStripMenuItem MenuItemCalculateGammaDiss;
      public ToolStripMenuItem MenuItemCalculateQuarkMass;
      public ToolStripMenuItem MenuItemPlot;
      public ToolStripMenuItem MenuItemPlotWave;
      public ToolStripMenuItem MenuItemPlotPotential;
      public ToolStripMenuItem MenuItemPlotCrossSection;
      public ToolStripMenuItem MenuItemArchiveQQData;
      public ToolStripMenuItem MenuItemShowArchivedQQData;
      public ToolStripMenuItem MenuItemPlotPionGDF;
      public ToolStripSeparator toolStripSeparator1;
      public ToolStripMenuItem MenuItemSelectArchiveDataFile;
      public ToolStripMenuItem MenuItemCompareResultsWithArchivedData;
      public ToolStripMenuItem MenuItemPlotAlpha;

      private void InitializeComponent()
      {
         this.MenuItemCalculateBoundWave = new ToolStripMenuItem();
         this.MenuItemCalculateFreeWave = new ToolStripMenuItem();
         this.MenuItemCalculateGammaDiss = new ToolStripMenuItem();
         this.MenuItemCalculateQuarkMass = new ToolStripMenuItem();
         this.MenuItemPlot = new ToolStripMenuItem();
         this.MenuItemPlotWave = new ToolStripMenuItem();
         this.MenuItemPlotCrossSection = new ToolStripMenuItem();
         this.MenuItemPlotAlpha = new ToolStripMenuItem();
         this.MenuItemPlotPionGDF = new ToolStripMenuItem();
         this.MenuItemPlotPotential = new ToolStripMenuItem();
         this.toolStripSeparator1 = new ToolStripSeparator();
         this.MenuItemArchiveQQData = new ToolStripMenuItem();
         this.MenuItemShowArchivedQQData = new ToolStripMenuItem();
         this.MenuItemSelectArchiveDataFile = new ToolStripMenuItem();
         this.MenuItemCompareResultsWithArchivedData = new ToolStripMenuItem();
         //
         // MenuItemSingleQQ
         //
         this.DropDownItems.AddRange(new ToolStripItem[] {
         this.MenuItemCalculateBoundWave,
         this.MenuItemCalculateFreeWave,
         this.MenuItemCalculateGammaDiss,
         this.MenuItemCalculateQuarkMass,
         this.MenuItemPlot,
         this.toolStripSeparator1,
         this.MenuItemArchiveQQData,
         this.MenuItemShowArchivedQQData,
         this.MenuItemSelectArchiveDataFile,
         this.MenuItemCompareResultsWithArchivedData});
         this.Name = "MenuItemSingleQQ";
         this.ShortcutKeyDisplayString = "Ctrl+W";
         this.ShortcutKeys = ((Keys)((Keys.Control | Keys.W)));
         this.Size = new Size(84, 24);
         this.Text = "&SingleQQ";
         //
         // MenuItemCalculateBoundWave
         //
         this.MenuItemCalculateBoundWave.Name = "MenuItemCalculateBoundWave";
         this.MenuItemCalculateBoundWave.ShortcutKeyDisplayString = "Ctrl+W";
         this.MenuItemCalculateBoundWave.ShortcutKeys = ((Keys)((Keys.Control | Keys.W)));
         this.MenuItemCalculateBoundWave.Size = new Size(334, 24);
         this.MenuItemCalculateBoundWave.Text = "Calculate bound &wavefunction";
         //
         // MenuItemCalculateFreeWave
         //
         this.MenuItemCalculateFreeWave.Name = "MenuItemCalculateFreeWave";
         this.MenuItemCalculateFreeWave.ShortcutKeyDisplayString = "Ctrl+F";
         this.MenuItemCalculateFreeWave.ShortcutKeys = ((Keys)((Keys.Control | Keys.F)));
         this.MenuItemCalculateFreeWave.Size = new Size(334, 24);
         this.MenuItemCalculateFreeWave.Text = "Calculate &free wavefunction";
         //
         // MenuItemCalculateGammaDiss
         //
         this.MenuItemCalculateGammaDiss.Name = "MenuItemCalculateGammaDiss";
         this.MenuItemCalculateGammaDiss.ShortcutKeyDisplayString = "Ctrl+G";
         this.MenuItemCalculateGammaDiss.ShortcutKeys = ((Keys)((Keys.Control | Keys.G)));
         this.MenuItemCalculateGammaDiss.Size = new Size(334, 24);
         this.MenuItemCalculateGammaDiss.Text = "Calculate &GammaDiss";
         //
         // MenuItemCalculateQuarkMass
         //
         this.MenuItemCalculateQuarkMass.Name = "MenuItemCalculateQuarkMass";
         this.MenuItemCalculateQuarkMass.ShortcutKeyDisplayString = "Ctrl+M";
         this.MenuItemCalculateQuarkMass.ShortcutKeys = ((Keys)((Keys.Control | Keys.M)));
         this.MenuItemCalculateQuarkMass.Size = new Size(334, 24);
         this.MenuItemCalculateQuarkMass.Text = "Calculate &QuarkMass";
         //
         // MenuItemPlot
         //
         this.MenuItemPlot.DropDownItems.AddRange(new ToolStripItem[] {
         this.MenuItemPlotWave,
         this.MenuItemPlotCrossSection,
         this.MenuItemPlotAlpha,
         this.MenuItemPlotPionGDF,
         this.MenuItemPlotPotential});
         this.MenuItemPlot.Name = "MenuItemPlot";
         this.MenuItemPlot.ShortcutKeyDisplayString = "";
         this.MenuItemPlot.Size = new Size(334, 24);
         this.MenuItemPlot.Text = "&Plot";
         //
         // MenuItemPlotWave
         //
         this.MenuItemPlotWave.Name = "MenuItemPlotWave";
         this.MenuItemPlotWave.ShortcutKeyDisplayString = "Ctrl+P";
         this.MenuItemPlotWave.ShortcutKeys = ((Keys)((Keys.Control | Keys.P)));
         this.MenuItemPlotWave.Size = new Size(333, 24);
         this.MenuItemPlotWave.Text = "Plot current &wavefunction";
         //
         // MenuItemPlotCrossSection
         //
         this.MenuItemPlotCrossSection.Name = "MenuItemPlotCrossSection";
         this.MenuItemPlotCrossSection.ShortcutKeyDisplayString = "Ctrl+Shift+C";
         this.MenuItemPlotCrossSection.ShortcutKeys = ((Keys)(((Keys.Control | Keys.Shift)
            | Keys.C)));
         this.MenuItemPlotCrossSection.Size = new Size(333, 24);
         this.MenuItemPlotCrossSection.Text = "Plot current &cross section";
         //
         // MenuItemPlotAlpha
         //
         this.MenuItemPlotAlpha.Name = "MenuItemPlotAlpha";
         this.MenuItemPlotAlpha.Size = new Size(333, 24);
         this.MenuItemPlotAlpha.Text = "Plot &Alpha";
         //
         // MenuItemPlotPionGDF
         //
         this.MenuItemPlotPionGDF.Name = "MenuItemPlotPionGDF";
         this.MenuItemPlotPionGDF.Size = new Size(333, 24);
         this.MenuItemPlotPionGDF.Text = "Plot Pion&GDF";
         //
         // MenuItemPlotPotential
         //
         this.MenuItemPlotPotential.Name = "MenuItemPlotPotential";
         this.MenuItemPlotPotential.ShortcutKeyDisplayString = "";
         this.MenuItemPlotPotential.Size = new Size(333, 24);
         this.MenuItemPlotPotential.Text = "Plot &Potential";
         //
         // toolStripSeparator1
         //
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new Size(331, 6);
         //
         // MenuItemArchiveQQData
         //
         this.MenuItemArchiveQQData.Name = "MenuItemArchiveQQData";
         this.MenuItemArchiveQQData.ShortcutKeyDisplayString = "Ctrl+S";
         this.MenuItemArchiveQQData.ShortcutKeys = ((Keys)((Keys.Control | Keys.S)));
         this.MenuItemArchiveQQData.Size = new Size(334, 24);
         this.MenuItemArchiveQQData.Text = "&Archive data";
         //
         // MenuItemShowArchivedQQData
         //
         this.MenuItemShowArchivedQQData.Name = "MenuItemShowArchivedQQData";
         this.MenuItemShowArchivedQQData.Size = new Size(334, 24);
         this.MenuItemShowArchivedQQData.Text = "&Show archived data";
         //
         // MenuItemSelectArchiveDataFile
         //
         this.MenuItemSelectArchiveDataFile.Name = "MenuItemSelectArchiveDataFile";
         this.MenuItemSelectArchiveDataFile.Size = new Size(334, 24);
         this.MenuItemSelectArchiveDataFile.Text = "S&elect archive data file";
         //
         // MenuItemCompareResultsWithArchivedData
         //
         this.MenuItemCompareResultsWithArchivedData.Name = "MenuItemCompareResultsWithArchivedData";
         this.MenuItemCompareResultsWithArchivedData.Size = new Size(334, 24);
         this.MenuItemCompareResultsWithArchivedData.Text = "&Compare results with archived data";
      }
   }
}