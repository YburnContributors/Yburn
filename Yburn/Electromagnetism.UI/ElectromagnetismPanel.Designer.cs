namespace Yburn.Electromagnetism.UI
{
	partial class ElectromagnetismPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not dify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.VSplit = new System.Windows.Forms.SplitContainer();
			this.LayoutBottom = new System.Windows.Forms.TableLayoutPanel();
			this.GbxGlauber = new System.Windows.Forms.GroupBox();
			this.LayoutGlauber = new System.Windows.Forms.TableLayoutPanel();
			this.TbxProtonNumberB = new System.Windows.Forms.TextBox();
			this.TbxProtonNumberA = new System.Windows.Forms.TextBox();
			this.LblProtonNumberB = new System.Windows.Forms.Label();
			this.LblProtonNumberA = new System.Windows.Forms.Label();
			this.LblDiffusenessA = new System.Windows.Forms.Label();
			this.TbxDiffusenessA = new System.Windows.Forms.TextBox();
			this.LblDiffusenessB = new System.Windows.Forms.Label();
			this.TbxDiffusenessB = new System.Windows.Forms.TextBox();
			this.LblNucleonNumberA = new System.Windows.Forms.Label();
			this.TbxNucleonNumberA = new System.Windows.Forms.TextBox();
			this.LblNucleonNumberB = new System.Windows.Forms.Label();
			this.TbxNucleonNumberB = new System.Windows.Forms.TextBox();
			this.LblNuclearRadiusA = new System.Windows.Forms.Label();
			this.TbxNuclearRadiusA = new System.Windows.Forms.TextBox();
			this.LblNuclearRadiusB = new System.Windows.Forms.Label();
			this.TbxNuclearRadiusB = new System.Windows.Forms.TextBox();
			this.LblImpactParam = new System.Windows.Forms.Label();
			this.TbxImpactParam = new System.Windows.Forms.TextBox();
			this.GbxGeneralParameters = new System.Windows.Forms.GroupBox();
			this.LayoutGeneralParameters = new System.Windows.Forms.TableLayoutPanel();
			this.CbxEMFCalculationMethod = new System.Windows.Forms.ComboBox();
			this.LblEMFCalculationMethod = new System.Windows.Forms.Label();
			this.LblQGPConductivityMeV = new System.Windows.Forms.Label();
			this.TbxQGPConductivityMeV = new System.Windows.Forms.TextBox();
			this.GbxSinglePointCharge = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.MsxEMFCalculationMethodSelection = new Yburn.UI.MultiSelectBox();
			this.LblEMFCalculationMethodSelection = new System.Windows.Forms.Label();
			this.LblEffectiveTimeSamples = new System.Windows.Forms.Label();
			this.TbxEffectiveTimeSamples = new System.Windows.Forms.TextBox();
			this.TbxLorentzFactor = new System.Windows.Forms.TextBox();
			this.LblRadialDistance = new System.Windows.Forms.Label();
			this.TbxRadialDistance = new System.Windows.Forms.TextBox();
			this.LblStartEffectiveTime = new System.Windows.Forms.Label();
			this.TbxStartEffectiveTime = new System.Windows.Forms.TextBox();
			this.LblStopEffectiveTime = new System.Windows.Forms.Label();
			this.TbxStopEffectiveTime = new System.Windows.Forms.TextBox();
			this.LblLorentzFactor = new System.Windows.Forms.Label();
			this.GbxFourierSynthesis = new System.Windows.Forms.GroupBox();
			this.LayoutFourierSynthesis = new System.Windows.Forms.TableLayoutPanel();
			this.LblMinFourierFrequency = new System.Windows.Forms.Label();
			this.TbxMinFourierFrequency = new System.Windows.Forms.TextBox();
			this.LblMaxFourierFrequency = new System.Windows.Forms.Label();
			this.TbxMaxFourierFrequency = new System.Windows.Forms.TextBox();
			this.LblFourierFrequencySteps = new System.Windows.Forms.Label();
			this.TbxFourierFrequencySteps = new System.Windows.Forms.TextBox();
			this.GbxOutput = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.LblOutfile = new System.Windows.Forms.Label();
			this.TbxOutfile = new System.Windows.Forms.TextBox();
			this.HSplit = new System.Windows.Forms.SplitContainer();
			this.CtrlTextBoxLog = new System.Windows.Forms.RichTextBox();
			this.CtrlStatusTrackingCtrl = new Yburn.UI.StatusTrackingCtrl();
			((System.ComponentModel.ISupportInitialize)(this.VSplit)).BeginInit();
			this.VSplit.Panel1.SuspendLayout();
			this.VSplit.Panel2.SuspendLayout();
			this.VSplit.SuspendLayout();
			this.LayoutBottom.SuspendLayout();
			this.GbxGlauber.SuspendLayout();
			this.LayoutGlauber.SuspendLayout();
			this.GbxGeneralParameters.SuspendLayout();
			this.LayoutGeneralParameters.SuspendLayout();
			this.GbxSinglePointCharge.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.GbxFourierSynthesis.SuspendLayout();
			this.LayoutFourierSynthesis.SuspendLayout();
			this.GbxOutput.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.HSplit)).BeginInit();
			this.HSplit.Panel1.SuspendLayout();
			this.HSplit.Panel2.SuspendLayout();
			this.HSplit.SuspendLayout();
			this.SuspendLayout();
			// 
			// VSplit
			// 
			this.VSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.VSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.VSplit.IsSplitterFixed = true;
			this.VSplit.Location = new System.Drawing.Point(0, 0);
			this.VSplit.Name = "VSplit";
			// 
			// VSplit.Panel1
			// 
			this.VSplit.Panel1.Controls.Add(this.LayoutBottom);
			// 
			// VSplit.Panel2
			// 
			this.VSplit.Panel2.Controls.Add(this.HSplit);
			this.VSplit.Size = new System.Drawing.Size(794, 503);
			this.VSplit.SplitterDistance = 500;
			this.VSplit.TabIndex = 0;
			// 
			// LayoutBottom
			// 
			this.LayoutBottom.AutoScroll = true;
			this.LayoutBottom.BackColor = System.Drawing.SystemColors.Control;
			this.LayoutBottom.ColumnCount = 1;
			this.LayoutBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.LayoutBottom.Controls.Add(this.GbxGlauber, 0, 0);
			this.LayoutBottom.Controls.Add(this.GbxGeneralParameters, 0, 1);
			this.LayoutBottom.Controls.Add(this.GbxSinglePointCharge, 0, 3);
			this.LayoutBottom.Controls.Add(this.GbxFourierSynthesis, 0, 2);
			this.LayoutBottom.Controls.Add(this.GbxOutput, 0, 4);
			this.LayoutBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutBottom.Location = new System.Drawing.Point(0, 0);
			this.LayoutBottom.Name = "LayoutBottom";
			this.LayoutBottom.RowCount = 5;
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutBottom.Size = new System.Drawing.Size(500, 503);
			this.LayoutBottom.TabIndex = 0;
			// 
			// GbxGlauber
			// 
			this.GbxGlauber.Controls.Add(this.LayoutGlauber);
			this.GbxGlauber.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxGlauber.Location = new System.Drawing.Point(10, 10);
			this.GbxGlauber.Margin = new System.Windows.Forms.Padding(10);
			this.GbxGlauber.Name = "GbxGlauber";
			this.GbxGlauber.Padding = new System.Windows.Forms.Padding(10);
			this.GbxGlauber.Size = new System.Drawing.Size(459, 269);
			this.GbxGlauber.TabIndex = 4;
			this.GbxGlauber.TabStop = false;
			this.GbxGlauber.Text = "Glauber";
			// 
			// LayoutGlauber
			// 
			this.LayoutGlauber.ColumnCount = 2;
			this.LayoutGlauber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutGlauber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutGlauber.Controls.Add(this.TbxProtonNumberB, 0, 7);
			this.LayoutGlauber.Controls.Add(this.TbxProtonNumberA, 0, 6);
			this.LayoutGlauber.Controls.Add(this.LblProtonNumberB, 0, 7);
			this.LayoutGlauber.Controls.Add(this.LblProtonNumberA, 0, 6);
			this.LayoutGlauber.Controls.Add(this.LblDiffusenessA, 0, 0);
			this.LayoutGlauber.Controls.Add(this.TbxDiffusenessA, 1, 0);
			this.LayoutGlauber.Controls.Add(this.LblDiffusenessB, 0, 1);
			this.LayoutGlauber.Controls.Add(this.TbxDiffusenessB, 1, 1);
			this.LayoutGlauber.Controls.Add(this.LblNucleonNumberA, 0, 2);
			this.LayoutGlauber.Controls.Add(this.TbxNucleonNumberA, 1, 2);
			this.LayoutGlauber.Controls.Add(this.LblNucleonNumberB, 0, 3);
			this.LayoutGlauber.Controls.Add(this.TbxNucleonNumberB, 1, 3);
			this.LayoutGlauber.Controls.Add(this.LblNuclearRadiusA, 0, 4);
			this.LayoutGlauber.Controls.Add(this.TbxNuclearRadiusA, 1, 4);
			this.LayoutGlauber.Controls.Add(this.LblNuclearRadiusB, 0, 5);
			this.LayoutGlauber.Controls.Add(this.TbxNuclearRadiusB, 1, 5);
			this.LayoutGlauber.Controls.Add(this.LblImpactParam, 0, 8);
			this.LayoutGlauber.Controls.Add(this.TbxImpactParam, 1, 8);
			this.LayoutGlauber.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutGlauber.Location = new System.Drawing.Point(10, 34);
			this.LayoutGlauber.Name = "LayoutGlauber";
			this.LayoutGlauber.RowCount = 9;
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutGlauber.Size = new System.Drawing.Size(439, 225);
			this.LayoutGlauber.TabIndex = 0;
			// 
			// TbxProtonNumberB
			// 
			this.TbxProtonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxProtonNumberB.Location = new System.Drawing.Point(244, 178);
			this.TbxProtonNumberB.Name = "TbxProtonNumberB";
			this.TbxProtonNumberB.Size = new System.Drawing.Size(192, 31);
			this.TbxProtonNumberB.TabIndex = 11;
			// 
			// TbxProtonNumberA
			// 
			this.TbxProtonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxProtonNumberA.Location = new System.Drawing.Point(244, 153);
			this.TbxProtonNumberA.Name = "TbxProtonNumberA";
			this.TbxProtonNumberA.Size = new System.Drawing.Size(192, 31);
			this.TbxProtonNumberA.TabIndex = 10;
			// 
			// LblProtonNumberB
			// 
			this.LblProtonNumberB.AutoSize = true;
			this.LblProtonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblProtonNumberB.Location = new System.Drawing.Point(3, 175);
			this.LblProtonNumberB.Name = "LblProtonNumberB";
			this.LblProtonNumberB.Size = new System.Drawing.Size(235, 25);
			this.LblProtonNumberB.TabIndex = 9;
			this.LblProtonNumberB.Text = "ProtonNumberB";
			this.LblProtonNumberB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblProtonNumberA
			// 
			this.LblProtonNumberA.AutoSize = true;
			this.LblProtonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblProtonNumberA.Location = new System.Drawing.Point(3, 150);
			this.LblProtonNumberA.Name = "LblProtonNumberA";
			this.LblProtonNumberA.Size = new System.Drawing.Size(235, 25);
			this.LblProtonNumberA.TabIndex = 8;
			this.LblProtonNumberA.Text = "ProtonNumberA";
			this.LblProtonNumberA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblDiffusenessA
			// 
			this.LblDiffusenessA.AutoSize = true;
			this.LblDiffusenessA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDiffusenessA.Location = new System.Drawing.Point(3, 0);
			this.LblDiffusenessA.Name = "LblDiffusenessA";
			this.LblDiffusenessA.Size = new System.Drawing.Size(235, 25);
			this.LblDiffusenessA.TabIndex = 0;
			this.LblDiffusenessA.Text = "DiffusenessA (fm)";
			this.LblDiffusenessA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDiffusenessA
			// 
			this.TbxDiffusenessA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDiffusenessA.Location = new System.Drawing.Point(244, 3);
			this.TbxDiffusenessA.Name = "TbxDiffusenessA";
			this.TbxDiffusenessA.Size = new System.Drawing.Size(192, 31);
			this.TbxDiffusenessA.TabIndex = 1;
			// 
			// LblDiffusenessB
			// 
			this.LblDiffusenessB.AutoSize = true;
			this.LblDiffusenessB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDiffusenessB.Location = new System.Drawing.Point(3, 25);
			this.LblDiffusenessB.Name = "LblDiffusenessB";
			this.LblDiffusenessB.Size = new System.Drawing.Size(235, 25);
			this.LblDiffusenessB.TabIndex = 0;
			this.LblDiffusenessB.Text = "DiffusenessB (fm)";
			this.LblDiffusenessB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDiffusenessB
			// 
			this.TbxDiffusenessB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDiffusenessB.Location = new System.Drawing.Point(244, 28);
			this.TbxDiffusenessB.Name = "TbxDiffusenessB";
			this.TbxDiffusenessB.Size = new System.Drawing.Size(192, 31);
			this.TbxDiffusenessB.TabIndex = 2;
			// 
			// LblNucleonNumberA
			// 
			this.LblNucleonNumberA.AutoSize = true;
			this.LblNucleonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNucleonNumberA.Location = new System.Drawing.Point(3, 50);
			this.LblNucleonNumberA.Name = "LblNucleonNumberA";
			this.LblNucleonNumberA.Size = new System.Drawing.Size(235, 25);
			this.LblNucleonNumberA.TabIndex = 0;
			this.LblNucleonNumberA.Text = "NucleonNumberA";
			this.LblNucleonNumberA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNucleonNumberA
			// 
			this.TbxNucleonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNucleonNumberA.Location = new System.Drawing.Point(244, 53);
			this.TbxNucleonNumberA.Name = "TbxNucleonNumberA";
			this.TbxNucleonNumberA.Size = new System.Drawing.Size(192, 31);
			this.TbxNucleonNumberA.TabIndex = 3;
			// 
			// LblNucleonNumberB
			// 
			this.LblNucleonNumberB.AutoSize = true;
			this.LblNucleonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNucleonNumberB.Location = new System.Drawing.Point(3, 75);
			this.LblNucleonNumberB.Name = "LblNucleonNumberB";
			this.LblNucleonNumberB.Size = new System.Drawing.Size(235, 25);
			this.LblNucleonNumberB.TabIndex = 0;
			this.LblNucleonNumberB.Text = "NucleonNumberB";
			this.LblNucleonNumberB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNucleonNumberB
			// 
			this.TbxNucleonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNucleonNumberB.Location = new System.Drawing.Point(244, 78);
			this.TbxNucleonNumberB.Name = "TbxNucleonNumberB";
			this.TbxNucleonNumberB.Size = new System.Drawing.Size(192, 31);
			this.TbxNucleonNumberB.TabIndex = 4;
			// 
			// LblNuclearRadiusA
			// 
			this.LblNuclearRadiusA.AutoSize = true;
			this.LblNuclearRadiusA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNuclearRadiusA.Location = new System.Drawing.Point(3, 100);
			this.LblNuclearRadiusA.Name = "LblNuclearRadiusA";
			this.LblNuclearRadiusA.Size = new System.Drawing.Size(235, 25);
			this.LblNuclearRadiusA.TabIndex = 0;
			this.LblNuclearRadiusA.Text = "NuclearRadiusA (fm)";
			this.LblNuclearRadiusA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNuclearRadiusA
			// 
			this.TbxNuclearRadiusA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNuclearRadiusA.Location = new System.Drawing.Point(244, 103);
			this.TbxNuclearRadiusA.Name = "TbxNuclearRadiusA";
			this.TbxNuclearRadiusA.Size = new System.Drawing.Size(192, 31);
			this.TbxNuclearRadiusA.TabIndex = 5;
			// 
			// LblNuclearRadiusB
			// 
			this.LblNuclearRadiusB.AutoSize = true;
			this.LblNuclearRadiusB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNuclearRadiusB.Location = new System.Drawing.Point(3, 125);
			this.LblNuclearRadiusB.Name = "LblNuclearRadiusB";
			this.LblNuclearRadiusB.Size = new System.Drawing.Size(235, 25);
			this.LblNuclearRadiusB.TabIndex = 0;
			this.LblNuclearRadiusB.Text = "NuclearRadiusB (fm)";
			this.LblNuclearRadiusB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNuclearRadiusB
			// 
			this.TbxNuclearRadiusB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNuclearRadiusB.Location = new System.Drawing.Point(244, 128);
			this.TbxNuclearRadiusB.Name = "TbxNuclearRadiusB";
			this.TbxNuclearRadiusB.Size = new System.Drawing.Size(192, 31);
			this.TbxNuclearRadiusB.TabIndex = 6;
			// 
			// LblImpactParam
			// 
			this.LblImpactParam.AutoSize = true;
			this.LblImpactParam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblImpactParam.Location = new System.Drawing.Point(3, 200);
			this.LblImpactParam.Name = "LblImpactParam";
			this.LblImpactParam.Size = new System.Drawing.Size(235, 25);
			this.LblImpactParam.TabIndex = 0;
			this.LblImpactParam.Text = "ImpactParam (fm)";
			this.LblImpactParam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxImpactParam
			// 
			this.TbxImpactParam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxImpactParam.Location = new System.Drawing.Point(244, 203);
			this.TbxImpactParam.Name = "TbxImpactParam";
			this.TbxImpactParam.Size = new System.Drawing.Size(192, 31);
			this.TbxImpactParam.TabIndex = 7;
			// 
			// GbxGeneralParameters
			// 
			this.GbxGeneralParameters.Controls.Add(this.LayoutGeneralParameters);
			this.GbxGeneralParameters.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxGeneralParameters.Location = new System.Drawing.Point(10, 299);
			this.GbxGeneralParameters.Margin = new System.Windows.Forms.Padding(10);
			this.GbxGeneralParameters.Name = "GbxGeneralParameters";
			this.GbxGeneralParameters.Padding = new System.Windows.Forms.Padding(10);
			this.GbxGeneralParameters.Size = new System.Drawing.Size(459, 94);
			this.GbxGeneralParameters.TabIndex = 3;
			this.GbxGeneralParameters.TabStop = false;
			this.GbxGeneralParameters.Text = "General Parameters";
			// 
			// LayoutGeneralParameters
			// 
			this.LayoutGeneralParameters.ColumnCount = 2;
			this.LayoutGeneralParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutGeneralParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutGeneralParameters.Controls.Add(this.CbxEMFCalculationMethod, 1, 1);
			this.LayoutGeneralParameters.Controls.Add(this.LblEMFCalculationMethod, 0, 1);
			this.LayoutGeneralParameters.Controls.Add(this.LblQGPConductivityMeV, 0, 0);
			this.LayoutGeneralParameters.Controls.Add(this.TbxQGPConductivityMeV, 1, 0);
			this.LayoutGeneralParameters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutGeneralParameters.Location = new System.Drawing.Point(10, 34);
			this.LayoutGeneralParameters.Name = "LayoutGeneralParameters";
			this.LayoutGeneralParameters.Padding = new System.Windows.Forms.Padding(5);
			this.LayoutGeneralParameters.RowCount = 2;
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.Size = new System.Drawing.Size(439, 50);
			this.LayoutGeneralParameters.TabIndex = 0;
			// 
			// CbxEMFCalculationMethod
			// 
			this.CbxEMFCalculationMethod.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxEMFCalculationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxEMFCalculationMethod.Location = new System.Drawing.Point(243, 33);
			this.CbxEMFCalculationMethod.Name = "CbxEMFCalculationMethod";
			this.CbxEMFCalculationMethod.Size = new System.Drawing.Size(188, 33);
			this.CbxEMFCalculationMethod.TabIndex = 7;
			// 
			// LblEMFCalculationMethod
			// 
			this.LblEMFCalculationMethod.AutoSize = true;
			this.LblEMFCalculationMethod.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblEMFCalculationMethod.Location = new System.Drawing.Point(8, 30);
			this.LblEMFCalculationMethod.Name = "LblEMFCalculationMethod";
			this.LblEMFCalculationMethod.Size = new System.Drawing.Size(229, 25);
			this.LblEMFCalculationMethod.TabIndex = 1;
			this.LblEMFCalculationMethod.Text = "EMFCalculationMethod";
			this.LblEMFCalculationMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblQGPConductivityMeV
			// 
			this.LblQGPConductivityMeV.AutoSize = true;
			this.LblQGPConductivityMeV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblQGPConductivityMeV.Location = new System.Drawing.Point(8, 5);
			this.LblQGPConductivityMeV.Name = "LblQGPConductivityMeV";
			this.LblQGPConductivityMeV.Size = new System.Drawing.Size(229, 25);
			this.LblQGPConductivityMeV.TabIndex = 0;
			this.LblQGPConductivityMeV.Text = "QGPConductivity (MeV)";
			this.LblQGPConductivityMeV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxQGPConductivityMeV
			// 
			this.TbxQGPConductivityMeV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxQGPConductivityMeV.Location = new System.Drawing.Point(243, 8);
			this.TbxQGPConductivityMeV.Name = "TbxQGPConductivityMeV";
			this.TbxQGPConductivityMeV.Size = new System.Drawing.Size(188, 31);
			this.TbxQGPConductivityMeV.TabIndex = 0;
			// 
			// GbxSinglePointCharge
			// 
			this.GbxSinglePointCharge.Controls.Add(this.tableLayoutPanel2);
			this.GbxSinglePointCharge.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxSinglePointCharge.Location = new System.Drawing.Point(10, 552);
			this.GbxSinglePointCharge.Margin = new System.Windows.Forms.Padding(10);
			this.GbxSinglePointCharge.Name = "GbxSinglePointCharge";
			this.GbxSinglePointCharge.Padding = new System.Windows.Forms.Padding(10);
			this.GbxSinglePointCharge.Size = new System.Drawing.Size(459, 244);
			this.GbxSinglePointCharge.TabIndex = 2;
			this.GbxSinglePointCharge.TabStop = false;
			this.GbxSinglePointCharge.Text = "Single Point Charge";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel2.Controls.Add(this.MsxEMFCalculationMethodSelection, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.LblEMFCalculationMethodSelection, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.LblEffectiveTimeSamples, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.TbxEffectiveTimeSamples, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.TbxLorentzFactor, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.LblRadialDistance, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.TbxRadialDistance, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.LblStartEffectiveTime, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.TbxStartEffectiveTime, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.LblStopEffectiveTime, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.TbxStopEffectiveTime, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.LblLorentzFactor, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 34);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel2.RowCount = 6;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(439, 200);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// MsxEMFCalculationMethodSelection
			// 
			this.MsxEMFCalculationMethodSelection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxEMFCalculationMethodSelection.Location = new System.Drawing.Point(243, 134);
			this.MsxEMFCalculationMethodSelection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxEMFCalculationMethodSelection.Name = "MsxEMFCalculationMethodSelection";
			this.MsxEMFCalculationMethodSelection.SelectionString = "";
			this.MsxEMFCalculationMethodSelection.Size = new System.Drawing.Size(188, 68);
			this.MsxEMFCalculationMethodSelection.TabIndex = 22;
			// 
			// LblEMFCalculationMethodSelection
			// 
			this.LblEMFCalculationMethodSelection.AutoSize = true;
			this.LblEMFCalculationMethodSelection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblEMFCalculationMethodSelection.Location = new System.Drawing.Point(8, 130);
			this.LblEMFCalculationMethodSelection.Name = "LblEMFCalculationMethodSelection";
			this.LblEMFCalculationMethodSelection.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LblEMFCalculationMethodSelection.Size = new System.Drawing.Size(229, 75);
			this.LblEMFCalculationMethodSelection.TabIndex = 11;
			this.LblEMFCalculationMethodSelection.Text = "EMFCalculationMethodSelection";
			// 
			// LblEffectiveTimeSamples
			// 
			this.LblEffectiveTimeSamples.AutoSize = true;
			this.LblEffectiveTimeSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblEffectiveTimeSamples.Location = new System.Drawing.Point(8, 105);
			this.LblEffectiveTimeSamples.Name = "LblEffectiveTimeSamples";
			this.LblEffectiveTimeSamples.Size = new System.Drawing.Size(229, 25);
			this.LblEffectiveTimeSamples.TabIndex = 10;
			this.LblEffectiveTimeSamples.Text = "EffectiveTimeSamples";
			this.LblEffectiveTimeSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxEffectiveTimeSamples
			// 
			this.TbxEffectiveTimeSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxEffectiveTimeSamples.Location = new System.Drawing.Point(243, 108);
			this.TbxEffectiveTimeSamples.Name = "TbxEffectiveTimeSamples";
			this.TbxEffectiveTimeSamples.Size = new System.Drawing.Size(188, 31);
			this.TbxEffectiveTimeSamples.TabIndex = 9;
			// 
			// TbxLorentzFactor
			// 
			this.TbxLorentzFactor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxLorentzFactor.Location = new System.Drawing.Point(243, 8);
			this.TbxLorentzFactor.Name = "TbxLorentzFactor";
			this.TbxLorentzFactor.Size = new System.Drawing.Size(188, 31);
			this.TbxLorentzFactor.TabIndex = 8;
			// 
			// LblRadialDistance
			// 
			this.LblRadialDistance.AutoSize = true;
			this.LblRadialDistance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblRadialDistance.Location = new System.Drawing.Point(8, 30);
			this.LblRadialDistance.Name = "LblRadialDistance";
			this.LblRadialDistance.Size = new System.Drawing.Size(229, 25);
			this.LblRadialDistance.TabIndex = 0;
			this.LblRadialDistance.Text = "RadialDistance (fm)";
			this.LblRadialDistance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxRadialDistance
			// 
			this.TbxRadialDistance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxRadialDistance.Location = new System.Drawing.Point(243, 33);
			this.TbxRadialDistance.Name = "TbxRadialDistance";
			this.TbxRadialDistance.Size = new System.Drawing.Size(188, 31);
			this.TbxRadialDistance.TabIndex = 0;
			// 
			// LblStartEffectiveTime
			// 
			this.LblStartEffectiveTime.AutoSize = true;
			this.LblStartEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblStartEffectiveTime.Location = new System.Drawing.Point(8, 55);
			this.LblStartEffectiveTime.Name = "LblStartEffectiveTime";
			this.LblStartEffectiveTime.Size = new System.Drawing.Size(229, 25);
			this.LblStartEffectiveTime.TabIndex = 0;
			this.LblStartEffectiveTime.Text = "StartEffectiveTime (fm/c)";
			this.LblStartEffectiveTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxStartEffectiveTime
			// 
			this.TbxStartEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxStartEffectiveTime.Location = new System.Drawing.Point(243, 58);
			this.TbxStartEffectiveTime.Name = "TbxStartEffectiveTime";
			this.TbxStartEffectiveTime.Size = new System.Drawing.Size(188, 31);
			this.TbxStartEffectiveTime.TabIndex = 1;
			// 
			// LblStopEffectiveTime
			// 
			this.LblStopEffectiveTime.AutoSize = true;
			this.LblStopEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblStopEffectiveTime.Location = new System.Drawing.Point(8, 80);
			this.LblStopEffectiveTime.Name = "LblStopEffectiveTime";
			this.LblStopEffectiveTime.Size = new System.Drawing.Size(229, 25);
			this.LblStopEffectiveTime.TabIndex = 7;
			this.LblStopEffectiveTime.Text = "StopEffectiveTime (fm/c)";
			this.LblStopEffectiveTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxStopEffectiveTime
			// 
			this.TbxStopEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxStopEffectiveTime.Location = new System.Drawing.Point(243, 83);
			this.TbxStopEffectiveTime.Name = "TbxStopEffectiveTime";
			this.TbxStopEffectiveTime.Size = new System.Drawing.Size(188, 31);
			this.TbxStopEffectiveTime.TabIndex = 2;
			// 
			// LblLorentzFactor
			// 
			this.LblLorentzFactor.AutoSize = true;
			this.LblLorentzFactor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblLorentzFactor.Location = new System.Drawing.Point(8, 5);
			this.LblLorentzFactor.Name = "LblLorentzFactor";
			this.LblLorentzFactor.Size = new System.Drawing.Size(229, 25);
			this.LblLorentzFactor.TabIndex = 0;
			this.LblLorentzFactor.Text = "LorentzFactor";
			this.LblLorentzFactor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GbxFourierSynthesis
			// 
			this.GbxFourierSynthesis.Controls.Add(this.LayoutFourierSynthesis);
			this.GbxFourierSynthesis.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxFourierSynthesis.Location = new System.Drawing.Point(10, 413);
			this.GbxFourierSynthesis.Margin = new System.Windows.Forms.Padding(10);
			this.GbxFourierSynthesis.Name = "GbxFourierSynthesis";
			this.GbxFourierSynthesis.Padding = new System.Windows.Forms.Padding(10);
			this.GbxFourierSynthesis.Size = new System.Drawing.Size(459, 119);
			this.GbxFourierSynthesis.TabIndex = 0;
			this.GbxFourierSynthesis.TabStop = false;
			this.GbxFourierSynthesis.Text = "Fourier Synthesis";
			// 
			// LayoutFourierSynthesis
			// 
			this.LayoutFourierSynthesis.ColumnCount = 2;
			this.LayoutFourierSynthesis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutFourierSynthesis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutFourierSynthesis.Controls.Add(this.LblMinFourierFrequency, 0, 0);
			this.LayoutFourierSynthesis.Controls.Add(this.TbxMinFourierFrequency, 1, 0);
			this.LayoutFourierSynthesis.Controls.Add(this.LblMaxFourierFrequency, 0, 1);
			this.LayoutFourierSynthesis.Controls.Add(this.TbxMaxFourierFrequency, 1, 1);
			this.LayoutFourierSynthesis.Controls.Add(this.LblFourierFrequencySteps, 0, 2);
			this.LayoutFourierSynthesis.Controls.Add(this.TbxFourierFrequencySteps, 1, 2);
			this.LayoutFourierSynthesis.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutFourierSynthesis.Location = new System.Drawing.Point(10, 34);
			this.LayoutFourierSynthesis.Name = "LayoutFourierSynthesis";
			this.LayoutFourierSynthesis.Padding = new System.Windows.Forms.Padding(5);
			this.LayoutFourierSynthesis.RowCount = 3;
			this.LayoutFourierSynthesis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutFourierSynthesis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutFourierSynthesis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutFourierSynthesis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutFourierSynthesis.Size = new System.Drawing.Size(439, 75);
			this.LayoutFourierSynthesis.TabIndex = 0;
			// 
			// LblMinFourierFrequency
			// 
			this.LblMinFourierFrequency.AutoSize = true;
			this.LblMinFourierFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMinFourierFrequency.Location = new System.Drawing.Point(8, 5);
			this.LblMinFourierFrequency.Name = "LblMinFourierFrequency";
			this.LblMinFourierFrequency.Size = new System.Drawing.Size(229, 25);
			this.LblMinFourierFrequency.TabIndex = 0;
			this.LblMinFourierFrequency.Text = "MinFourierFrequency (1/fm)";
			this.LblMinFourierFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMinFourierFrequency
			// 
			this.TbxMinFourierFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMinFourierFrequency.Location = new System.Drawing.Point(243, 8);
			this.TbxMinFourierFrequency.Name = "TbxMinFourierFrequency";
			this.TbxMinFourierFrequency.Size = new System.Drawing.Size(188, 31);
			this.TbxMinFourierFrequency.TabIndex = 0;
			// 
			// LblMaxFourierFrequency
			// 
			this.LblMaxFourierFrequency.AutoSize = true;
			this.LblMaxFourierFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMaxFourierFrequency.Location = new System.Drawing.Point(8, 30);
			this.LblMaxFourierFrequency.Name = "LblMaxFourierFrequency";
			this.LblMaxFourierFrequency.Size = new System.Drawing.Size(229, 25);
			this.LblMaxFourierFrequency.TabIndex = 0;
			this.LblMaxFourierFrequency.Text = "MaxFourierFrequency (1/fm)";
			this.LblMaxFourierFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMaxFourierFrequency
			// 
			this.TbxMaxFourierFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMaxFourierFrequency.Location = new System.Drawing.Point(243, 33);
			this.TbxMaxFourierFrequency.Name = "TbxMaxFourierFrequency";
			this.TbxMaxFourierFrequency.Size = new System.Drawing.Size(188, 31);
			this.TbxMaxFourierFrequency.TabIndex = 1;
			// 
			// LblFourierFrequencySteps
			// 
			this.LblFourierFrequencySteps.AutoSize = true;
			this.LblFourierFrequencySteps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblFourierFrequencySteps.Location = new System.Drawing.Point(8, 55);
			this.LblFourierFrequencySteps.Name = "LblFourierFrequencySteps";
			this.LblFourierFrequencySteps.Size = new System.Drawing.Size(229, 25);
			this.LblFourierFrequencySteps.TabIndex = 7;
			this.LblFourierFrequencySteps.Text = "FourierFrequencySteps";
			this.LblFourierFrequencySteps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxFourierFrequencySteps
			// 
			this.TbxFourierFrequencySteps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxFourierFrequencySteps.Location = new System.Drawing.Point(243, 58);
			this.TbxFourierFrequencySteps.Name = "TbxFourierFrequencySteps";
			this.TbxFourierFrequencySteps.Size = new System.Drawing.Size(188, 31);
			this.TbxFourierFrequencySteps.TabIndex = 2;
			// 
			// GbxOutput
			// 
			this.GbxOutput.Controls.Add(this.tableLayoutPanel1);
			this.GbxOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxOutput.Location = new System.Drawing.Point(10, 816);
			this.GbxOutput.Margin = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Name = "GbxOutput";
			this.GbxOutput.Padding = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Size = new System.Drawing.Size(459, 69);
			this.GbxOutput.TabIndex = 1;
			this.GbxOutput.TabStop = false;
			this.GbxOutput.Text = "Output";
			this.GbxOutput.Enter += new System.EventHandler(this.GbxOutput_Enter);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel1.Controls.Add(this.LblOutfile, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.TbxOutfile, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 34);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 25);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// LblOutfile
			// 
			this.LblOutfile.AutoSize = true;
			this.LblOutfile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblOutfile.Location = new System.Drawing.Point(8, 5);
			this.LblOutfile.Name = "LblOutfile";
			this.LblOutfile.Size = new System.Drawing.Size(229, 25);
			this.LblOutfile.TabIndex = 0;
			this.LblOutfile.Text = "Outfile";
			this.LblOutfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxOutfile
			// 
			this.TbxOutfile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxOutfile.Location = new System.Drawing.Point(243, 8);
			this.TbxOutfile.Name = "TbxOutfile";
			this.TbxOutfile.Size = new System.Drawing.Size(188, 31);
			this.TbxOutfile.TabIndex = 0;
			// 
			// HSplit
			// 
			this.HSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.HSplit.Location = new System.Drawing.Point(0, 0);
			this.HSplit.Name = "HSplit";
			this.HSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// HSplit.Panel1
			// 
			this.HSplit.Panel1.Controls.Add(this.CtrlTextBoxLog);
			// 
			// HSplit.Panel2
			// 
			this.HSplit.Panel2.Controls.Add(this.CtrlStatusTrackingCtrl);
			this.HSplit.Size = new System.Drawing.Size(290, 503);
			this.HSplit.SplitterDistance = 439;
			this.HSplit.TabIndex = 0;
			// 
			// CtrlTextBoxLog
			// 
			this.CtrlTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CtrlTextBoxLog.Location = new System.Drawing.Point(0, 0);
			this.CtrlTextBoxLog.Name = "CtrlTextBoxLog";
			this.CtrlTextBoxLog.Size = new System.Drawing.Size(290, 439);
			this.CtrlTextBoxLog.TabIndex = 9;
			this.CtrlTextBoxLog.Text = "";
			this.CtrlTextBoxLog.WordWrap = false;
			// 
			// CtrlStatusTrackingCtrl
			// 
			this.CtrlStatusTrackingCtrl.Location = new System.Drawing.Point(3, 3);
			this.CtrlStatusTrackingCtrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CtrlStatusTrackingCtrl.Name = "CtrlStatusTrackingCtrl";
			this.CtrlStatusTrackingCtrl.Size = new System.Drawing.Size(900, 50);
			this.CtrlStatusTrackingCtrl.TabIndex = 0;
			// 
			// ElectromagnetismPanel
			// 
			this.Controls.Add(this.VSplit);
			this.Name = "ElectromagnetismPanel";
			this.Size = new System.Drawing.Size(794, 503);
			this.VSplit.Panel1.ResumeLayout(false);
			this.VSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.VSplit)).EndInit();
			this.VSplit.ResumeLayout(false);
			this.LayoutBottom.ResumeLayout(false);
			this.GbxGlauber.ResumeLayout(false);
			this.LayoutGlauber.ResumeLayout(false);
			this.LayoutGlauber.PerformLayout();
			this.GbxGeneralParameters.ResumeLayout(false);
			this.LayoutGeneralParameters.ResumeLayout(false);
			this.LayoutGeneralParameters.PerformLayout();
			this.GbxSinglePointCharge.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.GbxFourierSynthesis.ResumeLayout(false);
			this.LayoutFourierSynthesis.ResumeLayout(false);
			this.LayoutFourierSynthesis.PerformLayout();
			this.GbxOutput.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.HSplit.Panel1.ResumeLayout(false);
			this.HSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.HSplit)).EndInit();
			this.HSplit.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer VSplit;
		private System.Windows.Forms.SplitContainer HSplit;
		private System.Windows.Forms.TableLayoutPanel LayoutBottom;
		private System.Windows.Forms.RichTextBox CtrlTextBoxLog;
		private Yburn.UI.StatusTrackingCtrl CtrlStatusTrackingCtrl;
		private System.Windows.Forms.GroupBox GbxFourierSynthesis;
		private System.Windows.Forms.TableLayoutPanel LayoutFourierSynthesis;
		private System.Windows.Forms.Label LblMaxFourierFrequency;
		private System.Windows.Forms.Label LblMinFourierFrequency;
		private System.Windows.Forms.TextBox TbxMaxFourierFrequency;
		private System.Windows.Forms.TextBox TbxMinFourierFrequency;
		private System.Windows.Forms.TextBox TbxFourierFrequencySteps;
		private System.Windows.Forms.Label LblFourierFrequencySteps;
		private System.Windows.Forms.GroupBox GbxOutput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label LblOutfile;
		private System.Windows.Forms.TextBox TbxOutfile;
		private System.Windows.Forms.GroupBox GbxSinglePointCharge;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label LblRadialDistance;
		private System.Windows.Forms.TextBox TbxRadialDistance;
		private System.Windows.Forms.Label LblStartEffectiveTime;
		private System.Windows.Forms.TextBox TbxStartEffectiveTime;
		private System.Windows.Forms.Label LblStopEffectiveTime;
		private System.Windows.Forms.TextBox TbxStopEffectiveTime;
		private System.Windows.Forms.Label LblLorentzFactor;
		private System.Windows.Forms.TextBox TbxLorentzFactor;
		private System.Windows.Forms.Label LblEffectiveTimeSamples;
		private System.Windows.Forms.TextBox TbxEffectiveTimeSamples;
		private System.Windows.Forms.Label LblEMFCalculationMethodSelection;
		private Yburn.UI.MultiSelectBox MsxEMFCalculationMethodSelection;
		private System.Windows.Forms.GroupBox GbxGeneralParameters;
		private System.Windows.Forms.TableLayoutPanel LayoutGeneralParameters;
		private System.Windows.Forms.Label LblQGPConductivityMeV;
		private System.Windows.Forms.TextBox TbxQGPConductivityMeV;
		private System.Windows.Forms.ComboBox CbxEMFCalculationMethod;
		private System.Windows.Forms.Label LblEMFCalculationMethod;
		private System.Windows.Forms.GroupBox GbxGlauber;
		private System.Windows.Forms.TableLayoutPanel LayoutGlauber;
		private System.Windows.Forms.TextBox TbxProtonNumberB;
		private System.Windows.Forms.TextBox TbxProtonNumberA;
		private System.Windows.Forms.Label LblProtonNumberB;
		private System.Windows.Forms.Label LblProtonNumberA;
		private System.Windows.Forms.Label LblDiffusenessA;
		private System.Windows.Forms.TextBox TbxDiffusenessA;
		private System.Windows.Forms.Label LblDiffusenessB;
		private System.Windows.Forms.TextBox TbxDiffusenessB;
		private System.Windows.Forms.Label LblNucleonNumberA;
		private System.Windows.Forms.TextBox TbxNucleonNumberA;
		private System.Windows.Forms.Label LblNucleonNumberB;
		private System.Windows.Forms.TextBox TbxNucleonNumberB;
		private System.Windows.Forms.Label LblNuclearRadiusA;
		private System.Windows.Forms.TextBox TbxNuclearRadiusA;
		private System.Windows.Forms.Label LblNuclearRadiusB;
		private System.Windows.Forms.TextBox TbxNuclearRadiusB;
		private System.Windows.Forms.Label LblImpactParam;
		private System.Windows.Forms.TextBox TbxImpactParam;
	}
}
