namespace Yburn.SingleQQ.UI
{
	partial class PotentialPlotParamForm
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
			this.LblPotentialType = new System.Windows.Forms.Label();
			this.CbxPotentialType = new System.Windows.Forms.ComboBox();
			this.LblColorState = new System.Windows.Forms.Label();
			this.CbxColorState = new System.Windows.Forms.ComboBox();
			this.LblSpinState = new System.Windows.Forms.Label();
			this.CbxSpinState = new System.Windows.Forms.ComboBox();
			this.LblAlphaSoft = new System.Windows.Forms.Label();
			this.TbxAlphaSoft = new System.Windows.Forms.TextBox();
			this.LblSigma = new System.Windows.Forms.Label();
			this.TbxSigma = new System.Windows.Forms.TextBox();
			this.LblTemperature = new System.Windows.Forms.Label();
			this.TbxTemperature = new System.Windows.Forms.TextBox();
			this.LblDebyeMass = new System.Windows.Forms.Label();
			this.TbxDebyeMass = new System.Windows.Forms.TextBox();
			this.LblSpinCouplingRange = new System.Windows.Forms.Label();
			this.TbxSpinCouplingRange = new System.Windows.Forms.TextBox();
			this.LblSpinCouplingStrength = new System.Windows.Forms.Label();
			this.TbxSpinCouplingStrength = new System.Windows.Forms.TextBox();
			this.LblMinRadius = new System.Windows.Forms.Label();
			this.TbxMinRadius = new System.Windows.Forms.TextBox();
			this.LblMaxRadius = new System.Windows.Forms.Label();
			this.TbxMaxRadius = new System.Windows.Forms.TextBox();
			this.LblSamples = new System.Windows.Forms.Label();
			this.TbxSamples = new System.Windows.Forms.TextBox();
			this.LblDataFileName = new System.Windows.Forms.Label();
			this.TbxDataFileName = new System.Windows.Forms.TextBox();
			this.GbxPlotParams = new System.Windows.Forms.GroupBox();
			this.LayoutPanel.SuspendLayout();
			this.GbxPlotParams.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnPlot
			// 
			this.BtnPlot.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.BtnPlot.Location = new System.Drawing.Point(102, 468);
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
			this.BtnLeave.Location = new System.Drawing.Point(251, 468);
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
			this.LayoutPanel.Controls.Add(this.LblPotentialType, 0, 0);
			this.LayoutPanel.Controls.Add(this.CbxPotentialType, 1, 0);
			this.LayoutPanel.Controls.Add(this.LblColorState, 0, 1);
			this.LayoutPanel.Controls.Add(this.CbxColorState, 1, 1);
			this.LayoutPanel.Controls.Add(this.LblSpinState, 0, 2);
			this.LayoutPanel.Controls.Add(this.CbxSpinState, 1, 2);
			this.LayoutPanel.Controls.Add(this.LblAlphaSoft, 0, 3);
			this.LayoutPanel.Controls.Add(this.TbxAlphaSoft, 1, 3);
			this.LayoutPanel.Controls.Add(this.LblSigma, 0, 4);
			this.LayoutPanel.Controls.Add(this.TbxSigma, 1, 4);
			this.LayoutPanel.Controls.Add(this.LblTemperature, 0, 5);
			this.LayoutPanel.Controls.Add(this.TbxTemperature, 1, 5);
			this.LayoutPanel.Controls.Add(this.LblDebyeMass, 0, 6);
			this.LayoutPanel.Controls.Add(this.TbxDebyeMass, 1, 6);
			this.LayoutPanel.Controls.Add(this.LblSpinCouplingRange, 0, 7);
			this.LayoutPanel.Controls.Add(this.TbxSpinCouplingRange, 1, 7);
			this.LayoutPanel.Controls.Add(this.LblSpinCouplingStrength, 0, 8);
			this.LayoutPanel.Controls.Add(this.TbxSpinCouplingStrength, 1, 8);
			this.LayoutPanel.Controls.Add(this.LblMinRadius, 0, 9);
			this.LayoutPanel.Controls.Add(this.TbxMinRadius, 1, 9);
			this.LayoutPanel.Controls.Add(this.LblMaxRadius, 0, 10);
			this.LayoutPanel.Controls.Add(this.TbxMaxRadius, 1, 10);
			this.LayoutPanel.Controls.Add(this.LblSamples, 0, 11);
			this.LayoutPanel.Controls.Add(this.TbxSamples, 1, 11);
			this.LayoutPanel.Controls.Add(this.LblDataFileName, 0, 12);
			this.LayoutPanel.Controls.Add(this.TbxDataFileName, 1, 12);
			this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutPanel.Location = new System.Drawing.Point(3, 18);
			this.LayoutPanel.Name = "LayoutPanel";
			this.LayoutPanel.Padding = new System.Windows.Forms.Padding(5);
			this.LayoutPanel.RowCount = 16;
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutPanel.Size = new System.Drawing.Size(450, 400);
			this.LayoutPanel.TabIndex = 3;
			// 
			// LblPotentialType
			// 
			this.LblPotentialType.AutoSize = true;
			this.LblPotentialType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblPotentialType.Location = new System.Drawing.Point(8, 5);
			this.LblPotentialType.Name = "LblPotentialType";
			this.LblPotentialType.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblPotentialType.Size = new System.Drawing.Size(214, 30);
			this.LblPotentialType.TabIndex = 11;
			this.LblPotentialType.Text = "PotentialType";
			this.LblPotentialType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CbxPotentialType
			// 
			this.CbxPotentialType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxPotentialType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxPotentialType.FormattingEnabled = true;
			this.CbxPotentialType.Location = new System.Drawing.Point(228, 8);
			this.CbxPotentialType.Name = "CbxPotentialType";
			this.CbxPotentialType.Size = new System.Drawing.Size(214, 24);
			this.CbxPotentialType.TabIndex = 0;
			// 
			// LblColorState
			// 
			this.LblColorState.AutoSize = true;
			this.LblColorState.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblColorState.Location = new System.Drawing.Point(8, 35);
			this.LblColorState.Name = "LblColorState";
			this.LblColorState.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblColorState.Size = new System.Drawing.Size(214, 30);
			this.LblColorState.TabIndex = 13;
			this.LblColorState.Text = "ColorState";
			this.LblColorState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CbxColorState
			// 
			this.CbxColorState.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxColorState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxColorState.FormattingEnabled = true;
			this.CbxColorState.Location = new System.Drawing.Point(228, 38);
			this.CbxColorState.Name = "CbxColorState";
			this.CbxColorState.Size = new System.Drawing.Size(214, 24);
			this.CbxColorState.TabIndex = 1;
			// 
			// LblSpinState
			// 
			this.LblSpinState.AutoSize = true;
			this.LblSpinState.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSpinState.Location = new System.Drawing.Point(8, 65);
			this.LblSpinState.Name = "LblSpinState";
			this.LblSpinState.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblSpinState.Size = new System.Drawing.Size(214, 30);
			this.LblSpinState.TabIndex = 16;
			this.LblSpinState.Text = "SpinState";
			this.LblSpinState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CbxSpinState
			// 
			this.CbxSpinState.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxSpinState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxSpinState.FormattingEnabled = true;
			this.CbxSpinState.Location = new System.Drawing.Point(228, 68);
			this.CbxSpinState.Name = "CbxSpinState";
			this.CbxSpinState.Size = new System.Drawing.Size(214, 24);
			this.CbxSpinState.TabIndex = 2;
			// 
			// LblAlphaSoft
			// 
			this.LblAlphaSoft.AutoSize = true;
			this.LblAlphaSoft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblAlphaSoft.Location = new System.Drawing.Point(8, 95);
			this.LblAlphaSoft.Name = "LblAlphaSoft";
			this.LblAlphaSoft.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblAlphaSoft.Size = new System.Drawing.Size(214, 30);
			this.LblAlphaSoft.TabIndex = 17;
			this.LblAlphaSoft.Text = "AlphaSoft";
			this.LblAlphaSoft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxAlphaSoft
			// 
			this.TbxAlphaSoft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxAlphaSoft.Location = new System.Drawing.Point(228, 98);
			this.TbxAlphaSoft.Name = "TbxAlphaSoft";
			this.TbxAlphaSoft.Size = new System.Drawing.Size(214, 22);
			this.TbxAlphaSoft.TabIndex = 3;
			// 
			// LblSigma
			// 
			this.LblSigma.AutoSize = true;
			this.LblSigma.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSigma.Location = new System.Drawing.Point(8, 125);
			this.LblSigma.Name = "LblSigma";
			this.LblSigma.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblSigma.Size = new System.Drawing.Size(214, 30);
			this.LblSigma.TabIndex = 12;
			this.LblSigma.Text = "Sigma (MeV)";
			this.LblSigma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxSigma
			// 
			this.TbxSigma.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxSigma.Location = new System.Drawing.Point(228, 128);
			this.TbxSigma.Name = "TbxSigma";
			this.TbxSigma.Size = new System.Drawing.Size(214, 22);
			this.TbxSigma.TabIndex = 4;
			// 
			// LblTemperature
			// 
			this.LblTemperature.AutoSize = true;
			this.LblTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblTemperature.Location = new System.Drawing.Point(8, 155);
			this.LblTemperature.Name = "LblTemperature";
			this.LblTemperature.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblTemperature.Size = new System.Drawing.Size(214, 30);
			this.LblTemperature.TabIndex = 14;
			this.LblTemperature.Text = "Temperature (MeV)";
			this.LblTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxTemperature
			// 
			this.TbxTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxTemperature.Location = new System.Drawing.Point(228, 158);
			this.TbxTemperature.Name = "TbxTemperature";
			this.TbxTemperature.Size = new System.Drawing.Size(214, 22);
			this.TbxTemperature.TabIndex = 5;
			// 
			// LblDebyeMass
			// 
			this.LblDebyeMass.AutoSize = true;
			this.LblDebyeMass.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDebyeMass.Location = new System.Drawing.Point(8, 185);
			this.LblDebyeMass.Name = "LblDebyeMass";
			this.LblDebyeMass.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblDebyeMass.Size = new System.Drawing.Size(214, 30);
			this.LblDebyeMass.TabIndex = 15;
			this.LblDebyeMass.Text = "DebyeMass (MeV)";
			this.LblDebyeMass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDebyeMass
			// 
			this.TbxDebyeMass.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDebyeMass.Location = new System.Drawing.Point(228, 188);
			this.TbxDebyeMass.Name = "TbxDebyeMass";
			this.TbxDebyeMass.Size = new System.Drawing.Size(214, 22);
			this.TbxDebyeMass.TabIndex = 6;
			// 
			// LblSpinCouplingRange
			// 
			this.LblSpinCouplingRange.AutoSize = true;
			this.LblSpinCouplingRange.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSpinCouplingRange.Location = new System.Drawing.Point(8, 215);
			this.LblSpinCouplingRange.Name = "LblSpinCouplingRange";
			this.LblSpinCouplingRange.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblSpinCouplingRange.Size = new System.Drawing.Size(214, 30);
			this.LblSpinCouplingRange.TabIndex = 18;
			this.LblSpinCouplingRange.Text = "SpinCouplingRange (fm)";
			this.LblSpinCouplingRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxSpinCouplingRange
			// 
			this.TbxSpinCouplingRange.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxSpinCouplingRange.Location = new System.Drawing.Point(228, 218);
			this.TbxSpinCouplingRange.Name = "TbxSpinCouplingRange";
			this.TbxSpinCouplingRange.Size = new System.Drawing.Size(214, 22);
			this.TbxSpinCouplingRange.TabIndex = 7;
			// 
			// LblSpinCouplingStrength
			// 
			this.LblSpinCouplingStrength.AutoSize = true;
			this.LblSpinCouplingStrength.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSpinCouplingStrength.Location = new System.Drawing.Point(8, 245);
			this.LblSpinCouplingStrength.Name = "LblSpinCouplingStrength";
			this.LblSpinCouplingStrength.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblSpinCouplingStrength.Size = new System.Drawing.Size(214, 30);
			this.LblSpinCouplingStrength.TabIndex = 19;
			this.LblSpinCouplingStrength.Text = "SpinCouplingStrength (MeV)";
			this.LblSpinCouplingStrength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxSpinCouplingStrength
			// 
			this.TbxSpinCouplingStrength.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxSpinCouplingStrength.Location = new System.Drawing.Point(228, 248);
			this.TbxSpinCouplingStrength.Name = "TbxSpinCouplingStrength";
			this.TbxSpinCouplingStrength.Size = new System.Drawing.Size(214, 22);
			this.TbxSpinCouplingStrength.TabIndex = 8;
			// 
			// LblMinRadius
			// 
			this.LblMinRadius.AutoSize = true;
			this.LblMinRadius.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMinRadius.Location = new System.Drawing.Point(8, 275);
			this.LblMinRadius.Name = "LblMinRadius";
			this.LblMinRadius.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblMinRadius.Size = new System.Drawing.Size(214, 30);
			this.LblMinRadius.TabIndex = 20;
			this.LblMinRadius.Text = "MinRadius (fm)";
			this.LblMinRadius.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMinRadius
			// 
			this.TbxMinRadius.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMinRadius.Location = new System.Drawing.Point(228, 278);
			this.TbxMinRadius.Name = "TbxMinRadius";
			this.TbxMinRadius.Size = new System.Drawing.Size(214, 22);
			this.TbxMinRadius.TabIndex = 9;
			// 
			// LblMaxRadius
			// 
			this.LblMaxRadius.AutoSize = true;
			this.LblMaxRadius.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMaxRadius.Location = new System.Drawing.Point(8, 305);
			this.LblMaxRadius.Name = "LblMaxRadius";
			this.LblMaxRadius.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblMaxRadius.Size = new System.Drawing.Size(214, 30);
			this.LblMaxRadius.TabIndex = 5;
			this.LblMaxRadius.Text = "MaxRadius (fm)";
			this.LblMaxRadius.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMaxRadius
			// 
			this.TbxMaxRadius.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMaxRadius.Location = new System.Drawing.Point(228, 308);
			this.TbxMaxRadius.Name = "TbxMaxRadius";
			this.TbxMaxRadius.Size = new System.Drawing.Size(214, 22);
			this.TbxMaxRadius.TabIndex = 10;
			// 
			// LblSamples
			// 
			this.LblSamples.AutoSize = true;
			this.LblSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSamples.Location = new System.Drawing.Point(8, 335);
			this.LblSamples.Name = "LblSamples";
			this.LblSamples.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblSamples.Size = new System.Drawing.Size(214, 30);
			this.LblSamples.TabIndex = 8;
			this.LblSamples.Text = "Samples";
			this.LblSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxSamples
			// 
			this.TbxSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxSamples.Location = new System.Drawing.Point(228, 338);
			this.TbxSamples.Name = "TbxSamples";
			this.TbxSamples.Size = new System.Drawing.Size(214, 22);
			this.TbxSamples.TabIndex = 11;
			// 
			// LblDataFileName
			// 
			this.LblDataFileName.AutoSize = true;
			this.LblDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDataFileName.Location = new System.Drawing.Point(8, 365);
			this.LblDataFileName.Name = "LblDataFileName";
			this.LblDataFileName.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.LblDataFileName.Size = new System.Drawing.Size(214, 30);
			this.LblDataFileName.TabIndex = 10;
			this.LblDataFileName.Text = "DataFileName";
			this.LblDataFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDataFileName
			// 
			this.TbxDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDataFileName.Location = new System.Drawing.Point(228, 368);
			this.TbxDataFileName.Name = "TbxDataFileName";
			this.TbxDataFileName.Size = new System.Drawing.Size(214, 22);
			this.TbxDataFileName.TabIndex = 12;
			// 
			// GbxPlotParams
			// 
			this.GbxPlotParams.Controls.Add(this.LayoutPanel);
			this.GbxPlotParams.Dock = System.Windows.Forms.DockStyle.Top;
			this.GbxPlotParams.Location = new System.Drawing.Point(20, 20);
			this.GbxPlotParams.Margin = new System.Windows.Forms.Padding(0);
			this.GbxPlotParams.Name = "GbxPlotParams";
			this.GbxPlotParams.Size = new System.Drawing.Size(456, 421);
			this.GbxPlotParams.TabIndex = 4;
			this.GbxPlotParams.TabStop = false;
			this.GbxPlotParams.Text = "Plot parameters";
			// 
			// PotentialPlotParamForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 534);
			this.Controls.Add(this.GbxPlotParams);
			this.Controls.Add(this.BtnLeave);
			this.Controls.Add(this.BtnPlot);
			this.Name = "PotentialPlotParamForm";
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
		private System.Windows.Forms.TextBox TbxAlphaSoft;
		private System.Windows.Forms.Label LblMaxRadius;
		private System.Windows.Forms.TextBox TbxDataFileName;
		private System.Windows.Forms.Label LblDataFileName;
		private System.Windows.Forms.GroupBox GbxPlotParams;
		private System.Windows.Forms.Label LblSpinState;
		private System.Windows.Forms.Label LblDebyeMass;
		private System.Windows.Forms.Label LblTemperature;
		private System.Windows.Forms.Label LblColorState;
		private System.Windows.Forms.Label LblSigma;
		private System.Windows.Forms.Label LblPotentialType;
		private System.Windows.Forms.Label LblSpinCouplingStrength;
		private System.Windows.Forms.Label LblSpinCouplingRange;
		private System.Windows.Forms.Label LblAlphaSoft;
		private System.Windows.Forms.Label LblMinRadius;
		private System.Windows.Forms.TextBox TbxMaxRadius;
		private System.Windows.Forms.TextBox TbxMinRadius;
		private System.Windows.Forms.TextBox TbxSpinCouplingStrength;
		private System.Windows.Forms.TextBox TbxSpinCouplingRange;
		private System.Windows.Forms.TextBox TbxDebyeMass;
		private System.Windows.Forms.TextBox TbxTemperature;
		private System.Windows.Forms.TextBox TbxSigma;
		private System.Windows.Forms.ComboBox CbxPotentialType;
		private System.Windows.Forms.ComboBox CbxSpinState;
		private System.Windows.Forms.ComboBox CbxColorState;
	}
}