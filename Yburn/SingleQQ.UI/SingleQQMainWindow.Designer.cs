namespace Yburn.SingleQQ.UI
{
	public partial class SingleQQMainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleQQMainWindow));
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemAbortProcess = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemClearScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemLoadParaFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenutItemSaveValuesAsParameterFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemLoadBatchFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSelectQQDataFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSelectOutputPath = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemOpenReadMe = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemCalculate = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemCalculateBoundWave = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemCalculateFreeWave = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemCalculateGammaDiss = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemCalculateQuarkMass = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlot = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotWave = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotCrossSection = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotAlpha = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPionGDF = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemPlotPotential = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemArchive = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemArchiveQQData = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemShowArchivedQQData = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSelectArchiveDataFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemCompareResultsWithArchivedData = new System.Windows.Forms.ToolStripMenuItem();
			this.LayoutBottom = new System.Windows.Forms.TableLayoutPanel();
			this.YburnConfigDataBox = new Yburn.UI.YburnConfigDataBox();
			this.SingleQQPanel = new Yburn.SingleQQ.UI.SingleQQPanel();
			this.MenuStrip.SuspendLayout();
			this.LayoutBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemCalculate,
            this.MenuItemPlot,
            this.MenuItemArchive});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(1182, 42);
			this.MenuStrip.TabIndex = 0;
			this.MenuStrip.Text = "MenuStrip";
			// 
			// MenuItemFile
			// 
			this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAbortProcess,
            this.MenuItemClearScreen,
            this.MenuItemLoadParaFile,
            this.MenutItemSaveValuesAsParameterFile,
            this.MenuItemLoadBatchFile,
            this.MenuItemSelectQQDataFile,
            this.MenuItemSelectOutputPath,
            this.MenuItemOpenReadMe,
            this.MenuItemQuit});
			this.MenuItemFile.Name = "MenuItemFile";
			this.MenuItemFile.ShortcutKeyDisplayString = "Shift+Alt+S";
			this.MenuItemFile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.MenuItemFile.Size = new System.Drawing.Size(64, 38);
			this.MenuItemFile.Text = "&File";
			// 
			// MenuItemAbortProcess
			// 
			this.MenuItemAbortProcess.Name = "MenuItemAbortProcess";
			this.MenuItemAbortProcess.ShortcutKeyDisplayString = "Shift+Alt+Del";
			this.MenuItemAbortProcess.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Delete)));
			this.MenuItemAbortProcess.Size = new System.Drawing.Size(513, 38);
			this.MenuItemAbortProcess.Text = "&Abort running process";
			this.MenuItemAbortProcess.Click += new System.EventHandler(this.MenuItemAbortProcess_Click);
			// 
			// MenuItemClearScreen
			// 
			this.MenuItemClearScreen.Name = "MenuItemClearScreen";
			this.MenuItemClearScreen.ShortcutKeyDisplayString = "Alt+Shift+L";
			this.MenuItemClearScreen.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
			this.MenuItemClearScreen.Size = new System.Drawing.Size(513, 38);
			this.MenuItemClearScreen.Text = "&Clear screen";
			this.MenuItemClearScreen.Click += new System.EventHandler(this.MenuItemClearScreen_Click);
			// 
			// MenuItemLoadParaFile
			// 
			this.MenuItemLoadParaFile.Name = "MenuItemLoadParaFile";
			this.MenuItemLoadParaFile.ShortcutKeyDisplayString = "Ctrl+O";
			this.MenuItemLoadParaFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.MenuItemLoadParaFile.Size = new System.Drawing.Size(513, 38);
			this.MenuItemLoadParaFile.Text = "Load &parameter file";
			this.MenuItemLoadParaFile.Click += new System.EventHandler(this.MenuItemLoadParaFile_Click);
			// 
			// MenutItemSaveValuesAsParameterFile
			// 
			this.MenutItemSaveValuesAsParameterFile.Name = "MenutItemSaveValuesAsParameterFile";
			this.MenutItemSaveValuesAsParameterFile.Size = new System.Drawing.Size(513, 38);
			this.MenutItemSaveValuesAsParameterFile.Text = "&Save current values as parameter file";
			this.MenutItemSaveValuesAsParameterFile.Click += new System.EventHandler(this.MenutItemSaveValuesAsParameterFile_Click);
			// 
			// MenuItemLoadBatchFile
			// 
			this.MenuItemLoadBatchFile.Name = "MenuItemLoadBatchFile";
			this.MenuItemLoadBatchFile.ShortcutKeyDisplayString = "Ctrl+Shift+O";
			this.MenuItemLoadBatchFile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
			this.MenuItemLoadBatchFile.Size = new System.Drawing.Size(513, 38);
			this.MenuItemLoadBatchFile.Text = "Load &batch file";
			this.MenuItemLoadBatchFile.Click += new System.EventHandler(this.MenuItemLoadBatchFile_Click);
			// 
			// MenuItemSelectQQDataFile
			// 
			this.MenuItemSelectQQDataFile.Name = "MenuItemSelectQQDataFile";
			this.MenuItemSelectQQDataFile.Size = new System.Drawing.Size(513, 38);
			this.MenuItemSelectQQDataFile.Text = "Select &QQDataFile";
			this.MenuItemSelectQQDataFile.Click += new System.EventHandler(this.MenuItemSelectQQDataFile_Click);
			// 
			// MenuItemSelectOutputPath
			// 
			this.MenuItemSelectOutputPath.Name = "MenuItemSelectOutputPath";
			this.MenuItemSelectOutputPath.Size = new System.Drawing.Size(513, 38);
			this.MenuItemSelectOutputPath.Text = "Select &OutputPath";
			this.MenuItemSelectOutputPath.Click += new System.EventHandler(this.MenuItemSelectOutputPath_Click);
			// 
			// MenuItemOpenReadMe
			// 
			this.MenuItemOpenReadMe.Name = "MenuItemOpenReadMe";
			this.MenuItemOpenReadMe.ShortcutKeyDisplayString = "F1";
			this.MenuItemOpenReadMe.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.MenuItemOpenReadMe.Size = new System.Drawing.Size(513, 38);
			this.MenuItemOpenReadMe.Text = "Open &ReadMe";
			this.MenuItemOpenReadMe.Click += new System.EventHandler(this.MenuItemOpenReadMe_Click);
			// 
			// MenuItemQuit
			// 
			this.MenuItemQuit.Name = "MenuItemQuit";
			this.MenuItemQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.MenuItemQuit.Size = new System.Drawing.Size(513, 38);
			this.MenuItemQuit.Text = "&Quit";
			this.MenuItemQuit.Click += new System.EventHandler(this.MenuItemQuit_Click);
			// 
			// MenuItemCalculate
			// 
			this.MenuItemCalculate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemCalculateBoundWave,
            this.MenuItemCalculateFreeWave,
            this.MenuItemCalculateGammaDiss,
            this.MenuItemCalculateQuarkMass});
			this.MenuItemCalculate.Name = "MenuItemCalculate";
			this.MenuItemCalculate.ShortcutKeyDisplayString = "Ctrl+W";
			this.MenuItemCalculate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.MenuItemCalculate.Size = new System.Drawing.Size(124, 38);
			this.MenuItemCalculate.Text = "&Calculate";
			// 
			// MenuItemCalculateBoundWave
			// 
			this.MenuItemCalculateBoundWave.Name = "MenuItemCalculateBoundWave";
			this.MenuItemCalculateBoundWave.ShortcutKeyDisplayString = "Ctrl+W";
			this.MenuItemCalculateBoundWave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.MenuItemCalculateBoundWave.Size = new System.Drawing.Size(529, 38);
			this.MenuItemCalculateBoundWave.Text = "Calculate bound &wavefunction";
			this.MenuItemCalculateBoundWave.Click += new System.EventHandler(this.MenuItemCalculateBoundWave_Click);
			// 
			// MenuItemCalculateFreeWave
			// 
			this.MenuItemCalculateFreeWave.Name = "MenuItemCalculateFreeWave";
			this.MenuItemCalculateFreeWave.ShortcutKeyDisplayString = "Ctrl+F";
			this.MenuItemCalculateFreeWave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.MenuItemCalculateFreeWave.Size = new System.Drawing.Size(529, 38);
			this.MenuItemCalculateFreeWave.Text = "Calculate &free wavefunction";
			this.MenuItemCalculateFreeWave.Click += new System.EventHandler(this.MenuItemCalculateFreeWave_Click);
			// 
			// MenuItemCalculateGammaDiss
			// 
			this.MenuItemCalculateGammaDiss.Name = "MenuItemCalculateGammaDiss";
			this.MenuItemCalculateGammaDiss.ShortcutKeyDisplayString = "Ctrl+G";
			this.MenuItemCalculateGammaDiss.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.MenuItemCalculateGammaDiss.Size = new System.Drawing.Size(529, 38);
			this.MenuItemCalculateGammaDiss.Text = "Calculate &GammaDiss";
			this.MenuItemCalculateGammaDiss.Click += new System.EventHandler(this.MenuItemCalculateGammaDiss_Click);
			// 
			// MenuItemCalculateQuarkMass
			// 
			this.MenuItemCalculateQuarkMass.Name = "MenuItemCalculateQuarkMass";
			this.MenuItemCalculateQuarkMass.ShortcutKeyDisplayString = "Ctrl+M";
			this.MenuItemCalculateQuarkMass.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.MenuItemCalculateQuarkMass.Size = new System.Drawing.Size(529, 38);
			this.MenuItemCalculateQuarkMass.Text = "Calculate &QuarkMass";
			this.MenuItemCalculateQuarkMass.Click += new System.EventHandler(this.MenuItemCalculateQuarkMass_Click);
			// 
			// MenuItemPlot
			// 
			this.MenuItemPlot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemPlotWave,
            this.MenuItemPlotCrossSection,
            this.MenuItemPlotAlpha,
            this.MenuItemPlotPionGDF,
            this.MenuItemPlotPotential});
			this.MenuItemPlot.Name = "MenuItemPlot";
			this.MenuItemPlot.ShortcutKeyDisplayString = "";
			this.MenuItemPlot.Size = new System.Drawing.Size(68, 38);
			this.MenuItemPlot.Text = "&Plot";
			// 
			// MenuItemPlotWave
			// 
			this.MenuItemPlotWave.Name = "MenuItemPlotWave";
			this.MenuItemPlotWave.ShortcutKeyDisplayString = "Ctrl+P";
			this.MenuItemPlotWave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.MenuItemPlotWave.Size = new System.Drawing.Size(530, 38);
			this.MenuItemPlotWave.Text = "Plot current &wavefunction";
			this.MenuItemPlotWave.Click += new System.EventHandler(this.MenuItemPlotWave_Click);
			// 
			// MenuItemPlotCrossSection
			// 
			this.MenuItemPlotCrossSection.Name = "MenuItemPlotCrossSection";
			this.MenuItemPlotCrossSection.ShortcutKeyDisplayString = "Ctrl+Shift+C";
			this.MenuItemPlotCrossSection.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
			this.MenuItemPlotCrossSection.Size = new System.Drawing.Size(530, 38);
			this.MenuItemPlotCrossSection.Text = "Plot current &cross section";
			this.MenuItemPlotCrossSection.Click += new System.EventHandler(this.MenuItemPlotCrossSection_Click);
			// 
			// MenuItemPlotAlpha
			// 
			this.MenuItemPlotAlpha.Name = "MenuItemPlotAlpha";
			this.MenuItemPlotAlpha.Size = new System.Drawing.Size(530, 38);
			this.MenuItemPlotAlpha.Text = "Plot &Alpha";
			this.MenuItemPlotAlpha.Click += new System.EventHandler(this.MenuItemPlotAlpha_Click);
			// 
			// MenuItemPlotPionGDF
			// 
			this.MenuItemPlotPionGDF.Name = "MenuItemPlotPionGDF";
			this.MenuItemPlotPionGDF.Size = new System.Drawing.Size(530, 38);
			this.MenuItemPlotPionGDF.Text = "Plot Pion&GDF";
			this.MenuItemPlotPionGDF.Click += new System.EventHandler(this.MenuItemPlotPionGDF_Click);
			// 
			// MenuItemPlotPotential
			// 
			this.MenuItemPlotPotential.Name = "MenuItemPlotPotential";
			this.MenuItemPlotPotential.ShortcutKeyDisplayString = "";
			this.MenuItemPlotPotential.Size = new System.Drawing.Size(530, 38);
			this.MenuItemPlotPotential.Text = "Plot &Potential";
			this.MenuItemPlotPotential.Click += new System.EventHandler(this.MenuItemPlotPotential_Click);
			// 
			// MenuItemArchive
			// 
			this.MenuItemArchive.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemArchiveQQData,
            this.MenuItemShowArchivedQQData,
            this.MenuItemSelectArchiveDataFile,
            this.MenuItemCompareResultsWithArchivedData});
			this.MenuItemArchive.Name = "MenuItemArchive";
			this.MenuItemArchive.Size = new System.Drawing.Size(106, 38);
			this.MenuItemArchive.Text = "&Archive";
			// 
			// MenuItemArchiveQQData
			// 
			this.MenuItemArchiveQQData.Name = "MenuItemArchiveQQData";
			this.MenuItemArchiveQQData.ShortcutKeyDisplayString = "Ctrl+S";
			this.MenuItemArchiveQQData.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.MenuItemArchiveQQData.Size = new System.Drawing.Size(490, 38);
			this.MenuItemArchiveQQData.Text = "&Archive data";
			this.MenuItemArchiveQQData.Click += new System.EventHandler(this.MenuItemArchiveQQData_Click);
			// 
			// MenuItemShowArchivedQQData
			// 
			this.MenuItemShowArchivedQQData.Name = "MenuItemShowArchivedQQData";
			this.MenuItemShowArchivedQQData.Size = new System.Drawing.Size(490, 38);
			this.MenuItemShowArchivedQQData.Text = "&Show archived data";
			this.MenuItemShowArchivedQQData.Click += new System.EventHandler(this.MenuItemShowArchivedQQData_Click);
			// 
			// MenuItemSelectArchiveDataFile
			// 
			this.MenuItemSelectArchiveDataFile.Name = "MenuItemSelectArchiveDataFile";
			this.MenuItemSelectArchiveDataFile.Size = new System.Drawing.Size(490, 38);
			this.MenuItemSelectArchiveDataFile.Text = "S&elect archive data file";
			this.MenuItemSelectArchiveDataFile.Click += new System.EventHandler(this.MenuItemSelectQQDataFile_Click);
			// 
			// MenuItemCompareResultsWithArchivedData
			// 
			this.MenuItemCompareResultsWithArchivedData.Name = "MenuItemCompareResultsWithArchivedData";
			this.MenuItemCompareResultsWithArchivedData.Size = new System.Drawing.Size(490, 38);
			this.MenuItemCompareResultsWithArchivedData.Text = "&Compare results with archived data";
			this.MenuItemCompareResultsWithArchivedData.Click += new System.EventHandler(this.MenuItemCompareResultsWithArchivedData_Click);
			// 
			// LayoutBottom
			// 
			this.LayoutBottom.ColumnCount = 1;
			this.LayoutBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.LayoutBottom.Controls.Add(this.YburnConfigDataBox, 0, 0);
			this.LayoutBottom.Controls.Add(this.SingleQQPanel, 0, 1);
			this.LayoutBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutBottom.Location = new System.Drawing.Point(0, 42);
			this.LayoutBottom.Name = "LayoutBottom";
			this.LayoutBottom.RowCount = 2;
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.Size = new System.Drawing.Size(1182, 607);
			this.LayoutBottom.TabIndex = 3;
			// 
			// YburnConfigDataBox
			// 
			this.YburnConfigDataBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.YburnConfigDataBox.Location = new System.Drawing.Point(4, 5);
			this.YburnConfigDataBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.YburnConfigDataBox.Name = "YburnConfigDataBox";
			this.YburnConfigDataBox.OutputPath = "";
			this.YburnConfigDataBox.QQDataPathFile = "";
			this.YburnConfigDataBox.Size = new System.Drawing.Size(600, 90);
			this.YburnConfigDataBox.TabIndex = 1;
			// 
			// SingleQQPanel
			// 
			this.SingleQQPanel.ControlsValues = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("SingleQQPanel.ControlsValues")));
			this.SingleQQPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SingleQQPanel.Location = new System.Drawing.Point(3, 103);
			this.SingleQQPanel.Name = "SingleQQPanel";
			this.SingleQQPanel.Size = new System.Drawing.Size(1176, 525);
			this.SingleQQPanel.TabIndex = 2;
			// 
			// SingleQQMainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1182, 649);
			this.Controls.Add(this.LayoutBottom);
			this.Controls.Add(this.MenuStrip);
			this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuStrip;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "SingleQQMainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainWindow";
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.LayoutBottom.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
		private System.Windows.Forms.ToolStripMenuItem MenuItemAbortProcess;
		private System.Windows.Forms.ToolStripMenuItem MenuItemClearScreen;
		private System.Windows.Forms.ToolStripMenuItem MenuItemLoadParaFile;
		private System.Windows.Forms.ToolStripMenuItem MenuItemOpenReadMe;
		private System.Windows.Forms.ToolStripMenuItem MenuItemQuit;
		private System.Windows.Forms.ToolStripMenuItem MenuItemSelectOutputPath;
		private System.Windows.Forms.ToolStripMenuItem MenuItemLoadBatchFile;
      private System.Windows.Forms.ToolStripMenuItem MenuItemSelectQQDataFile;
      private System.Windows.Forms.ToolStripMenuItem MenuItemCalculateBoundWave;
      private System.Windows.Forms.ToolStripMenuItem MenuItemCalculateFreeWave;
      private System.Windows.Forms.ToolStripMenuItem MenuItemCalculateGammaDiss;
      private System.Windows.Forms.ToolStripMenuItem MenuItemCalculateQuarkMass;
      private System.Windows.Forms.ToolStripMenuItem MenuItemPlot;
      private System.Windows.Forms.ToolStripMenuItem MenuItemPlotWave;
      private System.Windows.Forms.ToolStripMenuItem MenuItemPlotPotential;
      private System.Windows.Forms.ToolStripMenuItem MenuItemPlotCrossSection;
      private System.Windows.Forms.ToolStripMenuItem MenuItemArchiveQQData;
      private System.Windows.Forms.ToolStripMenuItem MenuItemShowArchivedQQData;
      private System.Windows.Forms.ToolStripMenuItem MenuItemPlotPionGDF;
      private System.Windows.Forms.ToolStripMenuItem MenuItemSelectArchiveDataFile;
      private System.Windows.Forms.ToolStripMenuItem MenuItemCompareResultsWithArchivedData;
      private System.Windows.Forms.ToolStripMenuItem MenuItemPlotAlpha;
      private System.Windows.Forms.TableLayoutPanel LayoutBottom;
      private Yburn.UI.YburnConfigDataBox YburnConfigDataBox;
      private SingleQQPanel SingleQQPanel;
      private System.Windows.Forms.ToolStripMenuItem MenutItemSaveValuesAsParameterFile;
      private System.Windows.Forms.ToolStripMenuItem MenuItemCalculate;
      private System.Windows.Forms.ToolStripMenuItem MenuItemArchive;
   }
}

