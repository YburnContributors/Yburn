namespace Yburn.UI
{
	partial class MainWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemAbortProcess = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemClearScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemLoadParaFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemLoadBatchFile = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemSetOutputPath = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemOpenReadMe = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.SingleQQPanel = new Yburn.UI.SingleQQPanel();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.InMediumDecayWidthPanel = new Yburn.UI.InMediumDecayWidthPanel();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.QQonFirePanel = new Yburn.UI.QQonFirePanel();
			this.MenuStrip.SuspendLayout();
			this.TabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip
			// 
			this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItemFile});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(1182, 40);
			this.MenuStrip.TabIndex = 0;
			this.MenuStrip.Text = "MenuStrip";
			// 
			// MenuItemFile
			// 
			this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.MenuItemAbortProcess,
			this.MenuItemClearScreen,
			this.MenuItemLoadParaFile,
			this.MenuItemLoadBatchFile,
			this.MenuItemSetOutputPath,
			this.MenuItemOpenReadMe,
			this.MenuItemQuit});
			this.MenuItemFile.Name = "MenuItemFile";
			this.MenuItemFile.ShortcutKeyDisplayString = "Shift+Alt+S";
			this.MenuItemFile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
			| System.Windows.Forms.Keys.S)));
			this.MenuItemFile.Size = new System.Drawing.Size(64, 36);
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
			// MenuItemSetOutputPath
			// 
			this.MenuItemSetOutputPath.Name = "MenuItemSetOutputPath";
			this.MenuItemSetOutputPath.Size = new System.Drawing.Size(513, 38);
			this.MenuItemSetOutputPath.Text = "S&et output path";
			this.MenuItemSetOutputPath.Click += new System.EventHandler(this.MenuItemSetOutputPath_Click);
			// 
			// MenuItemOpenReadMe
			// 
			this.MenuItemOpenReadMe.Name = "MenuItemOpenReadMe";
			this.MenuItemOpenReadMe.ShortcutKeyDisplayString = "F1";
			this.MenuItemOpenReadMe.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.MenuItemOpenReadMe.Size = new System.Drawing.Size(513, 38);
			this.MenuItemOpenReadMe.Text = "&Open ReadMe";
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
			// TabControl
			// 
			this.TabControl.Controls.Add(this.tabPage1);
			this.TabControl.Controls.Add(this.tabPage2);
			this.TabControl.Controls.Add(this.tabPage3);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 40);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(1182, 609);
			this.TabControl.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.SingleQQPanel);
			this.tabPage1.Location = new System.Drawing.Point(8, 42);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1166, 559);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "SingleQQ";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage1.Enter += new System.EventHandler(this.SingleQQPanel_Enter);
			// 
			// SingleQQPanel
			// 
			this.SingleQQPanel.AutoScroll = true;
			this.SingleQQPanel.BackColor = System.Drawing.SystemColors.Control;
			this.SingleQQPanel.ControlsValues = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("SingleQQPanel.ControlsValues")));
			this.SingleQQPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SingleQQPanel.Location = new System.Drawing.Point(3, 3);
			this.SingleQQPanel.Name = "SingleQQPanel";
			this.SingleQQPanel.Size = new System.Drawing.Size(1160, 553);
			this.SingleQQPanel.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.InMediumDecayWidthPanel);
			this.tabPage2.Location = new System.Drawing.Point(8, 42);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1166, 559);
			this.tabPage2.TabIndex = 2;
			this.tabPage2.Text = "InMediumDecayWidth";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.tabPage2.Enter += new System.EventHandler(this.InMediumDecayWidthPanel_Enter);
			// 
			// InMediumDecayWidthPanel
			// 
			this.InMediumDecayWidthPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InMediumDecayWidthPanel.Location = new System.Drawing.Point(3, 3);
			this.InMediumDecayWidthPanel.Name = "InMediumDecayWidthPanel";
			this.InMediumDecayWidthPanel.Size = new System.Drawing.Size(1160, 553);
			this.InMediumDecayWidthPanel.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.QQonFirePanel);
			this.tabPage3.Location = new System.Drawing.Point(8, 42);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(1166, 559);
			this.tabPage3.TabIndex = 1;
			this.tabPage3.Text = "QQonFire";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.tabPage3.Enter += new System.EventHandler(this.QQonFirePanel_Enter);
			// 
			// QQonFirePanel
			// 
			this.QQonFirePanel.BackColor = System.Drawing.SystemColors.Control;
			this.QQonFirePanel.ControlsValues = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("QQonFirePanel.ControlsValues")));
			this.QQonFirePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.QQonFirePanel.Location = new System.Drawing.Point(3, 3);
			this.QQonFirePanel.Name = "QQonFirePanel";
			this.QQonFirePanel.Size = new System.Drawing.Size(1160, 553);
			this.QQonFirePanel.TabIndex = 0;
			this.QQonFirePanel.Load += new System.EventHandler(this.QQonFirePanel_Load);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 28F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1182, 649);
			this.Controls.Add(this.TabControl);
			this.Controls.Add(this.MenuStrip);
			this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuStrip;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainWindow";
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.TabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
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
		private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private SingleQQPanel SingleQQPanel;
		private System.Windows.Forms.TabPage tabPage3;
		private QQonFirePanel QQonFirePanel;
		private System.Windows.Forms.ToolStripMenuItem MenuItemSetOutputPath;
		private System.Windows.Forms.ToolStripMenuItem MenuItemLoadBatchFile;
		private System.Windows.Forms.TabPage tabPage2;
		private InMediumDecayWidthPanel InMediumDecayWidthPanel;

	}
}

