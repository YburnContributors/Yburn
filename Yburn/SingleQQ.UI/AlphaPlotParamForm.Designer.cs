namespace Yburn.SingleQQ.UI
{
	partial class AlphaPlotParamForm
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
			this.BtnPlot = new System.Windows.Forms.Button();
			this.BtnLeave = new System.Windows.Forms.Button();
			this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.TbxDataFileName = new System.Windows.Forms.TextBox();
			this.TbxSamples = new System.Windows.Forms.TextBox();
			this.TbxMaxEnergy = new System.Windows.Forms.TextBox();
			this.TbxMinEnergy = new System.Windows.Forms.TextBox();
			this.LblDataFileName = new System.Windows.Forms.Label();
			this.LblSamples = new System.Windows.Forms.Label();
			this.LblMaxEnergy = new System.Windows.Forms.Label();
			this.LblMinEnergy = new System.Windows.Forms.Label();
			this.LblRunningCouplingType = new System.Windows.Forms.Label();
			this.MsxRunningCouplingType = new Yburn.UI.MultiSelectBox();
			this.GbxPlotParams = new System.Windows.Forms.GroupBox();
			this.LayoutPanel.SuspendLayout();
			this.GbxPlotParams.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnPlot
			// 
			this.BtnPlot.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnPlot.Location = new System.Drawing.Point(102, 300);
			this.BtnPlot.Name = "BtnPlot";
			this.BtnPlot.Size = new System.Drawing.Size(143, 35);
			this.BtnPlot.TabIndex = 0;
			this.BtnPlot.Text = "&Plot";
			this.BtnPlot.UseVisualStyleBackColor = true;
			this.BtnPlot.Click += new System.EventHandler(this.BtnPlot_Click);
			// 
			// BtnLeave
			// 
			this.BtnLeave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.BtnLeave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnLeave.Location = new System.Drawing.Point(251, 300);
			this.BtnLeave.Name = "BtnLeave";
			this.BtnLeave.Size = new System.Drawing.Size(143, 35);
			this.BtnLeave.TabIndex = 0;
			this.BtnLeave.Text = "&Leave";
			this.BtnLeave.UseVisualStyleBackColor = true;
			// 
			// LayoutPanel
			// 
			this.LayoutPanel.ColumnCount = 2;
			this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel.Controls.Add(this.TbxDataFileName, 1, 4);
			this.LayoutPanel.Controls.Add(this.TbxSamples, 1, 3);
			this.LayoutPanel.Controls.Add(this.TbxMaxEnergy, 1, 2);
			this.LayoutPanel.Controls.Add(this.TbxMinEnergy, 1, 1);
			this.LayoutPanel.Controls.Add(this.LblDataFileName, 0, 4);
			this.LayoutPanel.Controls.Add(this.LblSamples, 0, 3);
			this.LayoutPanel.Controls.Add(this.LblMaxEnergy, 0, 2);
			this.LayoutPanel.Controls.Add(this.LblMinEnergy, 0, 1);
			this.LayoutPanel.Controls.Add(this.LblRunningCouplingType, 0, 0);
			this.LayoutPanel.Controls.Add(this.MsxRunningCouplingType, 1, 0);
			this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutPanel.Location = new System.Drawing.Point(3, 18);
			this.LayoutPanel.Name = "LayoutPanel";
			this.LayoutPanel.Padding = new System.Windows.Forms.Padding(5);
			this.LayoutPanel.RowCount = 5;
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.Size = new System.Drawing.Size(450, 232);
			this.LayoutPanel.TabIndex = 0;
			// 
			// TbxDataFileName
			// 
			this.TbxDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDataFileName.Location = new System.Drawing.Point(228, 203);
			this.TbxDataFileName.Name = "TbxDataFileName";
			this.TbxDataFileName.Size = new System.Drawing.Size(214, 22);
			this.TbxDataFileName.TabIndex = 0;
			// 
			// TbxSamples
			// 
			this.TbxSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxSamples.Location = new System.Drawing.Point(228, 173);
			this.TbxSamples.Name = "TbxSamples";
			this.TbxSamples.Size = new System.Drawing.Size(214, 22);
			this.TbxSamples.TabIndex = 0;
			// 
			// TbxMaxEnergy
			// 
			this.TbxMaxEnergy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMaxEnergy.Location = new System.Drawing.Point(228, 143);
			this.TbxMaxEnergy.Name = "TbxMaxEnergy";
			this.TbxMaxEnergy.Size = new System.Drawing.Size(214, 22);
			this.TbxMaxEnergy.TabIndex = 0;
			// 
			// TbxMinEnergy
			// 
			this.TbxMinEnergy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMinEnergy.Location = new System.Drawing.Point(228, 113);
			this.TbxMinEnergy.Name = "TbxMinEnergy";
			this.TbxMinEnergy.Size = new System.Drawing.Size(214, 22);
			this.TbxMinEnergy.TabIndex = 0;
			// 
			// LblDataFileName
			// 
			this.LblDataFileName.AutoSize = true;
			this.LblDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDataFileName.Location = new System.Drawing.Point(8, 200);
			this.LblDataFileName.Name = "LblDataFileName";
			this.LblDataFileName.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblDataFileName.Size = new System.Drawing.Size(214, 30);
			this.LblDataFileName.TabIndex = 0;
			this.LblDataFileName.Text = "DataFileName";
			this.LblDataFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblSamples
			// 
			this.LblSamples.AutoSize = true;
			this.LblSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSamples.Location = new System.Drawing.Point(8, 170);
			this.LblSamples.Name = "LblSamples";
			this.LblSamples.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblSamples.Size = new System.Drawing.Size(214, 30);
			this.LblSamples.TabIndex = 0;
			this.LblSamples.Text = "Samples";
			this.LblSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblMaxEnergy
			// 
			this.LblMaxEnergy.AutoSize = true;
			this.LblMaxEnergy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMaxEnergy.Location = new System.Drawing.Point(8, 140);
			this.LblMaxEnergy.Name = "LblMaxEnergy";
			this.LblMaxEnergy.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblMaxEnergy.Size = new System.Drawing.Size(214, 30);
			this.LblMaxEnergy.TabIndex = 0;
			this.LblMaxEnergy.Text = "MaxEnergy (MeV)";
			this.LblMaxEnergy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblMinEnergy
			// 
			this.LblMinEnergy.AutoSize = true;
			this.LblMinEnergy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMinEnergy.Location = new System.Drawing.Point(8, 110);
			this.LblMinEnergy.Name = "LblMinEnergy";
			this.LblMinEnergy.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblMinEnergy.Size = new System.Drawing.Size(214, 30);
			this.LblMinEnergy.TabIndex = 0;
			this.LblMinEnergy.Text = "MinEnergy (MeV)";
			this.LblMinEnergy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblRunningCouplingType
			// 
			this.LblRunningCouplingType.AutoSize = true;
			this.LblRunningCouplingType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblRunningCouplingType.Location = new System.Drawing.Point(8, 5);
			this.LblRunningCouplingType.Name = "LblRunningCouplingType";
			this.LblRunningCouplingType.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.LblRunningCouplingType.Size = new System.Drawing.Size(214, 105);
			this.LblRunningCouplingType.TabIndex = 0;
			this.LblRunningCouplingType.Text = "RunningCouplingType";
			// 
			// MsxRunningCouplingType
			// 
			this.MsxRunningCouplingType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxRunningCouplingType.Location = new System.Drawing.Point(228, 8);
			this.MsxRunningCouplingType.Name = "MsxRunningCouplingType";
			this.MsxRunningCouplingType.SelectionString = "";
			this.MsxRunningCouplingType.Size = new System.Drawing.Size(214, 99);
			this.MsxRunningCouplingType.TabIndex = 0;
			// 
			// GbxPlotParams
			// 
			this.GbxPlotParams.Controls.Add(this.LayoutPanel);
			this.GbxPlotParams.Dock = System.Windows.Forms.DockStyle.Top;
			this.GbxPlotParams.Location = new System.Drawing.Point(20, 20);
			this.GbxPlotParams.Margin = new System.Windows.Forms.Padding(0);
			this.GbxPlotParams.Name = "GbxPlotParams";
			this.GbxPlotParams.Size = new System.Drawing.Size(456, 253);
			this.GbxPlotParams.TabIndex = 0;
			this.GbxPlotParams.TabStop = false;
			this.GbxPlotParams.Text = "Plot parameters";
			// 
			// AlphaPlotParamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 366);
			this.Controls.Add(this.GbxPlotParams);
			this.Controls.Add(this.BtnLeave);
			this.Controls.Add(this.BtnPlot);
			this.Name = "AlphaPlotParamForm";
			this.Padding = new System.Windows.Forms.Padding(20);
			this.Text = "PlotParamForm";
			this.LayoutPanel.ResumeLayout(false);
			this.LayoutPanel.PerformLayout();
			this.GbxPlotParams.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnPlot;
		private System.Windows.Forms.Button BtnLeave;
		private System.Windows.Forms.TableLayoutPanel LayoutPanel;
		private System.Windows.Forms.Label LblMinEnergy;
		private System.Windows.Forms.TextBox TbxSamples;
		private System.Windows.Forms.Label LblSamples;
		private System.Windows.Forms.TextBox TbxMaxEnergy;
		private System.Windows.Forms.Label LblMaxEnergy;
		private System.Windows.Forms.TextBox TbxMinEnergy;
		private System.Windows.Forms.TextBox TbxDataFileName;
		private System.Windows.Forms.Label LblDataFileName;
		private System.Windows.Forms.Label LblRunningCouplingType;
		private System.Windows.Forms.GroupBox GbxPlotParams;
		private Yburn.UI.MultiSelectBox MsxRunningCouplingType;
	}
}
