namespace Yburn.QQonFire.UI
{
   partial class QQonFireMainWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QQonFireMainWindow));
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
			this.LayoutBottom = new System.Windows.Forms.TableLayoutPanel();
			this.YburnConfigDataBox = new Yburn.UI.YburnConfigDataBox();
			this.QQonFirePanel = new Yburn.QQonFire.UI.QQonFirePanel();
			this.MenuStrip.SuspendLayout();
			this.LayoutBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile});
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
			// LayoutBottom
			// 
			this.LayoutBottom.ColumnCount = 1;
			this.LayoutBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.LayoutBottom.Controls.Add(this.YburnConfigDataBox, 0, 0);
			this.LayoutBottom.Controls.Add(this.QQonFirePanel, 0, 1);
			this.LayoutBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutBottom.Location = new System.Drawing.Point(0, 42);
			this.LayoutBottom.Name = "LayoutBottom";
			this.LayoutBottom.RowCount = 2;
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.Size = new System.Drawing.Size(1182, 607);
			this.LayoutBottom.TabIndex = 2;
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
			// QQonFirePanel
			// 
			this.QQonFirePanel.BackColor = System.Drawing.SystemColors.Control;
			this.QQonFirePanel.ControlsValues = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("QQonFirePanel.ControlsValues")));
			this.QQonFirePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.QQonFirePanel.Location = new System.Drawing.Point(3, 103);
			this.QQonFirePanel.Name = "QQonFirePanel";
			this.QQonFirePanel.Size = new System.Drawing.Size(1176, 584);
			this.QQonFirePanel.TabIndex = 0;
			// 
			// QQonFireMainWindow
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
			this.Name = "QQonFireMainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainWindow";
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.LayoutBottom.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip MenuStrip;
      private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
      private System.Windows.Forms.ToolStripMenuItem MenuItemAbortProcess;
      private System.Windows.Forms.ToolStripMenuItem MenuItemClearScreen;
      private System.Windows.Forms.ToolStripMenuItem MenuItemLoadParaFile;
      private System.Windows.Forms.ToolStripMenuItem MenuItemOpenReadMe;
      private System.Windows.Forms.ToolStripMenuItem MenuItemQuit;
      private System.Windows.Forms.ToolStripMenuItem MenuItemSelectOutputPath;
      private System.Windows.Forms.ToolStripMenuItem MenuItemLoadBatchFile;
      private QQonFirePanel QQonFirePanel;
      private System.Windows.Forms.TableLayoutPanel LayoutBottom;
      private Yburn.UI.YburnConfigDataBox YburnConfigDataBox;
      private System.Windows.Forms.ToolStripMenuItem MenuItemSelectQQDataFile;
      private System.Windows.Forms.ToolStripMenuItem MenutItemSaveValuesAsParameterFile;
   }
}

