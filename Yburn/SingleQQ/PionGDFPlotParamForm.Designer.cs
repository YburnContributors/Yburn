namespace Yburn.SingleQQ
{
	partial class PionGDFPlotParamForm
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
			this.TbxEnergyScale = new System.Windows.Forms.TextBox();
			this.LblDataFileName = new System.Windows.Forms.Label();
			this.LblSamples = new System.Windows.Forms.Label();
			this.LblEnergyScale = new System.Windows.Forms.Label();
			this.GbxPlotParams = new System.Windows.Forms.GroupBox();
			this.LayoutPanel.SuspendLayout();
			this.GbxPlotParams.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnPlot
			// 
			this.BtnPlot.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnPlot.Location = new System.Drawing.Point(102, 168);
			this.BtnPlot.Name = "BtnPlot";
			this.BtnPlot.Size = new System.Drawing.Size(143, 35);
			this.BtnPlot.TabIndex = 6;
			this.BtnPlot.Text = "&Plot";
			this.BtnPlot.UseVisualStyleBackColor = true;
			this.BtnPlot.Click += new System.EventHandler(this.BtnPlot_Click);
			// 
			// BtnLeave
			// 
			this.BtnLeave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.BtnLeave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnLeave.Location = new System.Drawing.Point(251, 168);
			this.BtnLeave.Name = "BtnLeave";
			this.BtnLeave.Size = new System.Drawing.Size(143, 35);
			this.BtnLeave.TabIndex = 7;
			this.BtnLeave.Text = "&Leave";
			this.BtnLeave.UseVisualStyleBackColor = true;
			// 
			// LayoutPanel
			// 
			this.LayoutPanel.ColumnCount = 2;
			this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.LayoutPanel.Controls.Add(this.TbxDataFileName, 1, 2);
			this.LayoutPanel.Controls.Add(this.TbxSamples, 1, 1);
			this.LayoutPanel.Controls.Add(this.TbxEnergyScale, 1, 0);
			this.LayoutPanel.Controls.Add(this.LblDataFileName, 0, 2);
			this.LayoutPanel.Controls.Add(this.LblSamples, 0, 1);
			this.LayoutPanel.Controls.Add(this.LblEnergyScale, 0, 0);
			this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutPanel.Location = new System.Drawing.Point(3, 18);
			this.LayoutPanel.Name = "LayoutPanel";
			this.LayoutPanel.Padding = new System.Windows.Forms.Padding(5);
			this.LayoutPanel.RowCount = 3;
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutPanel.Size = new System.Drawing.Size(450, 100);
			this.LayoutPanel.TabIndex = 3;
			// 
			// TbxDataFileName
			// 
			this.TbxDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDataFileName.Location = new System.Drawing.Point(228, 68);
			this.TbxDataFileName.Name = "TbxDataFileName";
			this.TbxDataFileName.Size = new System.Drawing.Size(214, 22);
			this.TbxDataFileName.TabIndex = 4;
			// 
			// TbxSamples
			// 
			this.TbxSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxSamples.Location = new System.Drawing.Point(228, 38);
			this.TbxSamples.Name = "TbxSamples";
			this.TbxSamples.Size = new System.Drawing.Size(214, 22);
			this.TbxSamples.TabIndex = 3;
			// 
			// TbxEnergyScale
			// 
			this.TbxEnergyScale.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxEnergyScale.Location = new System.Drawing.Point(228, 8);
			this.TbxEnergyScale.Name = "TbxEnergyScale";
			this.TbxEnergyScale.Size = new System.Drawing.Size(214, 22);
			this.TbxEnergyScale.TabIndex = 2;
			// 
			// LblDataFileName
			// 
			this.LblDataFileName.AutoSize = true;
			this.LblDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDataFileName.Location = new System.Drawing.Point(8, 65);
			this.LblDataFileName.Name = "LblDataFileName";
			this.LblDataFileName.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblDataFileName.Size = new System.Drawing.Size(214, 30);
			this.LblDataFileName.TabIndex = 10;
			this.LblDataFileName.Text = "DataFileName";
			this.LblDataFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblSamples
			// 
			this.LblSamples.AutoSize = true;
			this.LblSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSamples.Location = new System.Drawing.Point(8, 35);
			this.LblSamples.Name = "LblSamples";
			this.LblSamples.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblSamples.Size = new System.Drawing.Size(214, 30);
			this.LblSamples.TabIndex = 8;
			this.LblSamples.Text = "Samples";
			this.LblSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblEnergyScale
			// 
			this.LblEnergyScale.AutoSize = true;
			this.LblEnergyScale.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblEnergyScale.Location = new System.Drawing.Point(8, 5);
			this.LblEnergyScale.Name = "LblEnergyScale";
			this.LblEnergyScale.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblEnergyScale.Size = new System.Drawing.Size(214, 30);
			this.LblEnergyScale.TabIndex = 5;
			this.LblEnergyScale.Text = "EnergyScale (MeV)";
			this.LblEnergyScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GbxPlotParams
			// 
			this.GbxPlotParams.Controls.Add(this.LayoutPanel);
			this.GbxPlotParams.Dock = System.Windows.Forms.DockStyle.Top;
			this.GbxPlotParams.Location = new System.Drawing.Point(20, 20);
			this.GbxPlotParams.Margin = new System.Windows.Forms.Padding(0);
			this.GbxPlotParams.Name = "GbxPlotParams";
			this.GbxPlotParams.Size = new System.Drawing.Size(456, 121);
			this.GbxPlotParams.TabIndex = 4;
			this.GbxPlotParams.TabStop = false;
			this.GbxPlotParams.Text = "Plot parameters";
			// 
			// PionGDFPlotParamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 234);
			this.Controls.Add(this.GbxPlotParams);
			this.Controls.Add(this.BtnLeave);
			this.Controls.Add(this.BtnPlot);
			this.Name = "PionGDFPlotParamForm";
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
		private System.Windows.Forms.TextBox TbxSamples;
		private System.Windows.Forms.Label LblSamples;
		private System.Windows.Forms.TextBox TbxEnergyScale;
		private System.Windows.Forms.Label LblEnergyScale;
		private System.Windows.Forms.TextBox TbxDataFileName;
		private System.Windows.Forms.Label LblDataFileName;
		private System.Windows.Forms.GroupBox GbxPlotParams;
	}
}