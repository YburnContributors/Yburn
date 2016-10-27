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
			this.LblProtonNumberB = new System.Windows.Forms.Label();
			this.TbxProtonNumberA = new System.Windows.Forms.TextBox();
			this.LblProtonNumberA = new System.Windows.Forms.Label();
			this.LblNucleusShapeA = new System.Windows.Forms.Label();
			this.CbxNucleusShapeA = new System.Windows.Forms.ComboBox();
			this.LblNucleusShapeB = new System.Windows.Forms.Label();
			this.CbxNucleusShapeB = new System.Windows.Forms.ComboBox();
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
			this.LblImpactParameter = new System.Windows.Forms.Label();
			this.TbxImpactParameter = new System.Windows.Forms.TextBox();
			this.GbxGeneralParameters = new System.Windows.Forms.GroupBox();
			this.LayoutGeneralParameters = new System.Windows.Forms.TableLayoutPanel();
			this.TbxSamples = new System.Windows.Forms.TextBox();
			this.LblSamples = new System.Windows.Forms.Label();
			this.TbxQuadratureOrder = new System.Windows.Forms.TextBox();
			this.LblQuadratureOrder = new System.Windows.Forms.Label();
			this.LblGridCellSize = new System.Windows.Forms.Label();
			this.TbxGridCellSize = new System.Windows.Forms.TextBox();
			this.LblGridRadius = new System.Windows.Forms.Label();
			this.TbxGridRadius = new System.Windows.Forms.TextBox();
			this.CbxEMFCalculationMethod = new System.Windows.Forms.ComboBox();
			this.LblEMFCalculationMethod = new System.Windows.Forms.Label();
			this.LblQGPConductivityMeV = new System.Windows.Forms.Label();
			this.TbxQGPConductivityMeV = new System.Windows.Forms.TextBox();
			this.GbxSinglePointCharge = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.MsxEMFCalculationMethodSelection = new Yburn.UI.MultiSelectBox();
			this.LblEMFCalculationMethodSelection = new System.Windows.Forms.Label();
			this.TbxPointChargeRapidity = new System.Windows.Forms.TextBox();
			this.LblRadialDistance = new System.Windows.Forms.Label();
			this.TbxRadialDistance = new System.Windows.Forms.TextBox();
			this.LblStartEffectiveTime = new System.Windows.Forms.Label();
			this.TbxStartEffectiveTime = new System.Windows.Forms.TextBox();
			this.LblStopEffectiveTime = new System.Windows.Forms.Label();
			this.TbxStopEffectiveTime = new System.Windows.Forms.TextBox();
			this.LblPointChargeRapidity = new System.Windows.Forms.Label();
			this.GbxOutput = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.LblDataFileName = new System.Windows.Forms.Label();
			this.TbxDataFileName = new System.Windows.Forms.TextBox();
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
			this.LayoutBottom.Controls.Add(this.GbxSinglePointCharge, 0, 2);
			this.LayoutBottom.Controls.Add(this.GbxOutput, 0, 3);
			this.LayoutBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutBottom.Location = new System.Drawing.Point(0, 0);
			this.LayoutBottom.Name = "LayoutBottom";
			this.LayoutBottom.RowCount = 4;
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
			this.GbxGlauber.Size = new System.Drawing.Size(459, 310);
			this.GbxGlauber.TabIndex = 0;
			this.GbxGlauber.TabStop = false;
			this.GbxGlauber.Text = "Glauber";
			// 
			// LayoutGlauber
			// 
			this.LayoutGlauber.ColumnCount = 2;
			this.LayoutGlauber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutGlauber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutGlauber.Controls.Add(this.TbxProtonNumberB, 1, 7);
			this.LayoutGlauber.Controls.Add(this.LblProtonNumberB, 0, 7);
			this.LayoutGlauber.Controls.Add(this.TbxProtonNumberA, 1, 2);
			this.LayoutGlauber.Controls.Add(this.LblProtonNumberA, 0, 2);
			this.LayoutGlauber.Controls.Add(this.LblNucleusShapeA, 0, 0);
			this.LayoutGlauber.Controls.Add(this.CbxNucleusShapeA, 1, 0);
			this.LayoutGlauber.Controls.Add(this.LblNucleusShapeB, 0, 5);
			this.LayoutGlauber.Controls.Add(this.CbxNucleusShapeB, 1, 5);
			this.LayoutGlauber.Controls.Add(this.LblDiffusenessA, 0, 4);
			this.LayoutGlauber.Controls.Add(this.TbxDiffusenessA, 1, 4);
			this.LayoutGlauber.Controls.Add(this.LblDiffusenessB, 0, 9);
			this.LayoutGlauber.Controls.Add(this.TbxDiffusenessB, 1, 9);
			this.LayoutGlauber.Controls.Add(this.LblNucleonNumberA, 0, 1);
			this.LayoutGlauber.Controls.Add(this.TbxNucleonNumberA, 1, 1);
			this.LayoutGlauber.Controls.Add(this.LblNucleonNumberB, 0, 6);
			this.LayoutGlauber.Controls.Add(this.TbxNucleonNumberB, 1, 6);
			this.LayoutGlauber.Controls.Add(this.LblNuclearRadiusA, 0, 3);
			this.LayoutGlauber.Controls.Add(this.TbxNuclearRadiusA, 1, 3);
			this.LayoutGlauber.Controls.Add(this.LblNuclearRadiusB, 0, 8);
			this.LayoutGlauber.Controls.Add(this.TbxNuclearRadiusB, 1, 8);
			this.LayoutGlauber.Controls.Add(this.LblImpactParameter, 0, 10);
			this.LayoutGlauber.Controls.Add(this.TbxImpactParameter, 1, 10);
			this.LayoutGlauber.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutGlauber.Location = new System.Drawing.Point(10, 34);
			this.LayoutGlauber.Name = "LayoutGlauber";
			this.LayoutGlauber.RowCount = 11;
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGlauber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
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
			this.LayoutGlauber.Size = new System.Drawing.Size(439, 266);
			this.LayoutGlauber.TabIndex = 0;
			// 
			// TbxProtonNumberB
			// 
			this.TbxProtonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxProtonNumberB.Location = new System.Drawing.Point(244, 178);
			this.TbxProtonNumberB.Name = "TbxProtonNumberB";
			this.TbxProtonNumberB.Size = new System.Drawing.Size(192, 31);
			this.TbxProtonNumberB.TabIndex = 0;
			// 
			// LblProtonNumberB
			// 
			this.LblProtonNumberB.AutoSize = true;
			this.LblProtonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblProtonNumberB.Location = new System.Drawing.Point(3, 175);
			this.LblProtonNumberB.Name = "LblProtonNumberB";
			this.LblProtonNumberB.Size = new System.Drawing.Size(235, 25);
			this.LblProtonNumberB.TabIndex = 0;
			this.LblProtonNumberB.Text = "ProtonNumberB";
			this.LblProtonNumberB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxProtonNumberA
			// 
			this.TbxProtonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxProtonNumberA.Location = new System.Drawing.Point(244, 53);
			this.TbxProtonNumberA.Name = "TbxProtonNumberA";
			this.TbxProtonNumberA.Size = new System.Drawing.Size(192, 31);
			this.TbxProtonNumberA.TabIndex = 0;
			// 
			// LblProtonNumberA
			// 
			this.LblProtonNumberA.AutoSize = true;
			this.LblProtonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblProtonNumberA.Location = new System.Drawing.Point(3, 50);
			this.LblProtonNumberA.Name = "LblProtonNumberA";
			this.LblProtonNumberA.Size = new System.Drawing.Size(235, 25);
			this.LblProtonNumberA.TabIndex = 0;
			this.LblProtonNumberA.Text = "ProtonNumberA";
			this.LblProtonNumberA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblNucleusShapeA
			// 
			this.LblNucleusShapeA.AutoSize = true;
			this.LblNucleusShapeA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNucleusShapeA.Location = new System.Drawing.Point(3, 0);
			this.LblNucleusShapeA.Name = "LblNucleusShapeA";
			this.LblNucleusShapeA.Size = new System.Drawing.Size(235, 25);
			this.LblNucleusShapeA.TabIndex = 0;
			this.LblNucleusShapeA.Text = "NucleusShapeA";
			this.LblNucleusShapeA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CbxNucleusShapeA
			// 
			this.CbxNucleusShapeA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxNucleusShapeA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxNucleusShapeA.Location = new System.Drawing.Point(244, 3);
			this.CbxNucleusShapeA.Name = "CbxNucleusShapeA";
			this.CbxNucleusShapeA.Size = new System.Drawing.Size(192, 33);
			this.CbxNucleusShapeA.TabIndex = 0;
			// 
			// LblNucleusShapeB
			// 
			this.LblNucleusShapeB.AutoSize = true;
			this.LblNucleusShapeB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNucleusShapeB.Location = new System.Drawing.Point(3, 125);
			this.LblNucleusShapeB.Name = "LblNucleusShapeB";
			this.LblNucleusShapeB.Size = new System.Drawing.Size(235, 25);
			this.LblNucleusShapeB.TabIndex = 0;
			this.LblNucleusShapeB.Text = "NucleusShapeB";
			this.LblNucleusShapeB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CbxNucleusShapeB
			// 
			this.CbxNucleusShapeB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxNucleusShapeB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxNucleusShapeB.Location = new System.Drawing.Point(244, 128);
			this.CbxNucleusShapeB.Name = "CbxNucleusShapeB";
			this.CbxNucleusShapeB.Size = new System.Drawing.Size(192, 33);
			this.CbxNucleusShapeB.TabIndex = 0;
			// 
			// LblDiffusenessA
			// 
			this.LblDiffusenessA.AutoSize = true;
			this.LblDiffusenessA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDiffusenessA.Location = new System.Drawing.Point(3, 100);
			this.LblDiffusenessA.Name = "LblDiffusenessA";
			this.LblDiffusenessA.Size = new System.Drawing.Size(235, 25);
			this.LblDiffusenessA.TabIndex = 0;
			this.LblDiffusenessA.Text = "DiffusenessA (fm)";
			this.LblDiffusenessA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDiffusenessA
			// 
			this.TbxDiffusenessA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDiffusenessA.Location = new System.Drawing.Point(244, 103);
			this.TbxDiffusenessA.Name = "TbxDiffusenessA";
			this.TbxDiffusenessA.Size = new System.Drawing.Size(192, 31);
			this.TbxDiffusenessA.TabIndex = 0;
			// 
			// LblDiffusenessB
			// 
			this.LblDiffusenessB.AutoSize = true;
			this.LblDiffusenessB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDiffusenessB.Location = new System.Drawing.Point(3, 225);
			this.LblDiffusenessB.Name = "LblDiffusenessB";
			this.LblDiffusenessB.Size = new System.Drawing.Size(235, 25);
			this.LblDiffusenessB.TabIndex = 0;
			this.LblDiffusenessB.Text = "DiffusenessB (fm)";
			this.LblDiffusenessB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDiffusenessB
			// 
			this.TbxDiffusenessB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDiffusenessB.Location = new System.Drawing.Point(244, 228);
			this.TbxDiffusenessB.Name = "TbxDiffusenessB";
			this.TbxDiffusenessB.Size = new System.Drawing.Size(192, 31);
			this.TbxDiffusenessB.TabIndex = 0;
			// 
			// LblNucleonNumberA
			// 
			this.LblNucleonNumberA.AutoSize = true;
			this.LblNucleonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNucleonNumberA.Location = new System.Drawing.Point(3, 25);
			this.LblNucleonNumberA.Name = "LblNucleonNumberA";
			this.LblNucleonNumberA.Size = new System.Drawing.Size(235, 25);
			this.LblNucleonNumberA.TabIndex = 0;
			this.LblNucleonNumberA.Text = "NucleonNumberA";
			this.LblNucleonNumberA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNucleonNumberA
			// 
			this.TbxNucleonNumberA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNucleonNumberA.Location = new System.Drawing.Point(244, 28);
			this.TbxNucleonNumberA.Name = "TbxNucleonNumberA";
			this.TbxNucleonNumberA.Size = new System.Drawing.Size(192, 31);
			this.TbxNucleonNumberA.TabIndex = 0;
			// 
			// LblNucleonNumberB
			// 
			this.LblNucleonNumberB.AutoSize = true;
			this.LblNucleonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNucleonNumberB.Location = new System.Drawing.Point(3, 150);
			this.LblNucleonNumberB.Name = "LblNucleonNumberB";
			this.LblNucleonNumberB.Size = new System.Drawing.Size(235, 25);
			this.LblNucleonNumberB.TabIndex = 0;
			this.LblNucleonNumberB.Text = "NucleonNumberB";
			this.LblNucleonNumberB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNucleonNumberB
			// 
			this.TbxNucleonNumberB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNucleonNumberB.Location = new System.Drawing.Point(244, 153);
			this.TbxNucleonNumberB.Name = "TbxNucleonNumberB";
			this.TbxNucleonNumberB.Size = new System.Drawing.Size(192, 31);
			this.TbxNucleonNumberB.TabIndex = 0;
			// 
			// LblNuclearRadiusA
			// 
			this.LblNuclearRadiusA.AutoSize = true;
			this.LblNuclearRadiusA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNuclearRadiusA.Location = new System.Drawing.Point(3, 75);
			this.LblNuclearRadiusA.Name = "LblNuclearRadiusA";
			this.LblNuclearRadiusA.Size = new System.Drawing.Size(235, 25);
			this.LblNuclearRadiusA.TabIndex = 0;
			this.LblNuclearRadiusA.Text = "NuclearRadiusA (fm)";
			this.LblNuclearRadiusA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNuclearRadiusA
			// 
			this.TbxNuclearRadiusA.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNuclearRadiusA.Location = new System.Drawing.Point(244, 78);
			this.TbxNuclearRadiusA.Name = "TbxNuclearRadiusA";
			this.TbxNuclearRadiusA.Size = new System.Drawing.Size(192, 31);
			this.TbxNuclearRadiusA.TabIndex = 0;
			// 
			// LblNuclearRadiusB
			// 
			this.LblNuclearRadiusB.AutoSize = true;
			this.LblNuclearRadiusB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNuclearRadiusB.Location = new System.Drawing.Point(3, 200);
			this.LblNuclearRadiusB.Name = "LblNuclearRadiusB";
			this.LblNuclearRadiusB.Size = new System.Drawing.Size(235, 25);
			this.LblNuclearRadiusB.TabIndex = 0;
			this.LblNuclearRadiusB.Text = "NuclearRadiusB (fm)";
			this.LblNuclearRadiusB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNuclearRadiusB
			// 
			this.TbxNuclearRadiusB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNuclearRadiusB.Location = new System.Drawing.Point(244, 203);
			this.TbxNuclearRadiusB.Name = "TbxNuclearRadiusB";
			this.TbxNuclearRadiusB.Size = new System.Drawing.Size(192, 31);
			this.TbxNuclearRadiusB.TabIndex = 0;
			// 
			// LblImpactParameter
			// 
			this.LblImpactParameter.AutoSize = true;
			this.LblImpactParameter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblImpactParameter.Location = new System.Drawing.Point(3, 250);
			this.LblImpactParameter.Name = "LblImpactParameter";
			this.LblImpactParameter.Size = new System.Drawing.Size(235, 25);
			this.LblImpactParameter.TabIndex = 0;
			this.LblImpactParameter.Text = "ImpactParameter (fm)";
			this.LblImpactParameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxImpactParameter
			// 
			this.TbxImpactParameter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxImpactParameter.Location = new System.Drawing.Point(244, 253);
			this.TbxImpactParameter.Name = "TbxImpactParameter";
			this.TbxImpactParameter.Size = new System.Drawing.Size(192, 31);
			this.TbxImpactParameter.TabIndex = 0;
			// 
			// GbxGeneralParameters
			// 
			this.GbxGeneralParameters.Controls.Add(this.LayoutGeneralParameters);
			this.GbxGeneralParameters.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxGeneralParameters.Location = new System.Drawing.Point(10, 340);
			this.GbxGeneralParameters.Margin = new System.Windows.Forms.Padding(10);
			this.GbxGeneralParameters.Name = "GbxGeneralParameters";
			this.GbxGeneralParameters.Padding = new System.Windows.Forms.Padding(10);
			this.GbxGeneralParameters.Size = new System.Drawing.Size(459, 185);
			this.GbxGeneralParameters.TabIndex = 0;
			this.GbxGeneralParameters.TabStop = false;
			this.GbxGeneralParameters.Text = "General Parameters";
			// 
			// LayoutGeneralParameters
			// 
			this.LayoutGeneralParameters.ColumnCount = 2;
			this.LayoutGeneralParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutGeneralParameters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutGeneralParameters.Controls.Add(this.TbxSamples, 1, 5);
			this.LayoutGeneralParameters.Controls.Add(this.LblSamples, 0, 5);
			this.LayoutGeneralParameters.Controls.Add(this.TbxQuadratureOrder, 1, 4);
			this.LayoutGeneralParameters.Controls.Add(this.LblQuadratureOrder, 0, 4);
			this.LayoutGeneralParameters.Controls.Add(this.LblGridCellSize, 0, 2);
			this.LayoutGeneralParameters.Controls.Add(this.TbxGridCellSize, 1, 3);
			this.LayoutGeneralParameters.Controls.Add(this.LblGridRadius, 0, 3);
			this.LayoutGeneralParameters.Controls.Add(this.TbxGridRadius, 1, 2);
			this.LayoutGeneralParameters.Controls.Add(this.CbxEMFCalculationMethod, 1, 1);
			this.LayoutGeneralParameters.Controls.Add(this.LblEMFCalculationMethod, 0, 1);
			this.LayoutGeneralParameters.Controls.Add(this.LblQGPConductivityMeV, 0, 0);
			this.LayoutGeneralParameters.Controls.Add(this.TbxQGPConductivityMeV, 1, 0);
			this.LayoutGeneralParameters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutGeneralParameters.Location = new System.Drawing.Point(10, 34);
			this.LayoutGeneralParameters.Name = "LayoutGeneralParameters";
			this.LayoutGeneralParameters.RowCount = 6;
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutGeneralParameters.Size = new System.Drawing.Size(439, 141);
			this.LayoutGeneralParameters.TabIndex = 0;
			// 
			// TbxSamples
			// 
			this.TbxSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxSamples.Location = new System.Drawing.Point(244, 128);
			this.TbxSamples.Name = "TbxSamples";
			this.TbxSamples.Size = new System.Drawing.Size(192, 31);
			this.TbxSamples.TabIndex = 0;
			// 
			// LblSamples
			// 
			this.LblSamples.AutoSize = true;
			this.LblSamples.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblSamples.Location = new System.Drawing.Point(3, 125);
			this.LblSamples.Name = "LblSamples";
			this.LblSamples.Size = new System.Drawing.Size(235, 25);
			this.LblSamples.TabIndex = 0;
			this.LblSamples.Text = "Samples";
			this.LblSamples.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxQuadratureOrder
			// 
			this.TbxQuadratureOrder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxQuadratureOrder.Location = new System.Drawing.Point(244, 103);
			this.TbxQuadratureOrder.Name = "TbxQuadratureOrder";
			this.TbxQuadratureOrder.Size = new System.Drawing.Size(192, 31);
			this.TbxQuadratureOrder.TabIndex = 0;
			// 
			// LblQuadratureOrder
			// 
			this.LblQuadratureOrder.AutoSize = true;
			this.LblQuadratureOrder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblQuadratureOrder.Location = new System.Drawing.Point(3, 100);
			this.LblQuadratureOrder.Name = "LblQuadratureOrder";
			this.LblQuadratureOrder.Size = new System.Drawing.Size(235, 25);
			this.LblQuadratureOrder.TabIndex = 0;
			this.LblQuadratureOrder.Text = "QuadratureOrder";
			this.LblQuadratureOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblGridCellSize
			// 
			this.LblGridCellSize.AutoSize = true;
			this.LblGridCellSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblGridCellSize.Location = new System.Drawing.Point(3, 50);
			this.LblGridCellSize.Name = "LblGridCellSize";
			this.LblGridCellSize.Size = new System.Drawing.Size(235, 25);
			this.LblGridCellSize.TabIndex = 0;
			this.LblGridCellSize.Text = "GridCellSize (fm)";
			this.LblGridCellSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxGridCellSize
			// 
			this.TbxGridCellSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxGridCellSize.Location = new System.Drawing.Point(244, 78);
			this.TbxGridCellSize.Name = "TbxGridCellSize";
			this.TbxGridCellSize.Size = new System.Drawing.Size(192, 31);
			this.TbxGridCellSize.TabIndex = 0;
			// 
			// LblGridRadius
			// 
			this.LblGridRadius.AutoSize = true;
			this.LblGridRadius.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblGridRadius.Location = new System.Drawing.Point(3, 75);
			this.LblGridRadius.Name = "LblGridRadius";
			this.LblGridRadius.Size = new System.Drawing.Size(235, 25);
			this.LblGridRadius.TabIndex = 0;
			this.LblGridRadius.Text = "GridRadius (fm)";
			this.LblGridRadius.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxGridRadius
			// 
			this.TbxGridRadius.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxGridRadius.Location = new System.Drawing.Point(244, 53);
			this.TbxGridRadius.Name = "TbxGridRadius";
			this.TbxGridRadius.Size = new System.Drawing.Size(192, 31);
			this.TbxGridRadius.TabIndex = 0;
			// 
			// CbxEMFCalculationMethod
			// 
			this.CbxEMFCalculationMethod.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxEMFCalculationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxEMFCalculationMethod.Location = new System.Drawing.Point(244, 28);
			this.CbxEMFCalculationMethod.Name = "CbxEMFCalculationMethod";
			this.CbxEMFCalculationMethod.Size = new System.Drawing.Size(192, 33);
			this.CbxEMFCalculationMethod.TabIndex = 0;
			// 
			// LblEMFCalculationMethod
			// 
			this.LblEMFCalculationMethod.AutoSize = true;
			this.LblEMFCalculationMethod.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblEMFCalculationMethod.Location = new System.Drawing.Point(3, 25);
			this.LblEMFCalculationMethod.Name = "LblEMFCalculationMethod";
			this.LblEMFCalculationMethod.Size = new System.Drawing.Size(235, 25);
			this.LblEMFCalculationMethod.TabIndex = 0;
			this.LblEMFCalculationMethod.Text = "EMFCalculationMethod";
			this.LblEMFCalculationMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblQGPConductivityMeV
			// 
			this.LblQGPConductivityMeV.AutoSize = true;
			this.LblQGPConductivityMeV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblQGPConductivityMeV.Location = new System.Drawing.Point(3, 0);
			this.LblQGPConductivityMeV.Name = "LblQGPConductivityMeV";
			this.LblQGPConductivityMeV.Size = new System.Drawing.Size(235, 25);
			this.LblQGPConductivityMeV.TabIndex = 0;
			this.LblQGPConductivityMeV.Text = "QGPConductivity (MeV)";
			this.LblQGPConductivityMeV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxQGPConductivityMeV
			// 
			this.TbxQGPConductivityMeV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxQGPConductivityMeV.Location = new System.Drawing.Point(244, 3);
			this.TbxQGPConductivityMeV.Name = "TbxQGPConductivityMeV";
			this.TbxQGPConductivityMeV.Size = new System.Drawing.Size(192, 31);
			this.TbxQGPConductivityMeV.TabIndex = 0;
			// 
			// GbxSinglePointCharge
			// 
			this.GbxSinglePointCharge.Controls.Add(this.tableLayoutPanel2);
			this.GbxSinglePointCharge.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxSinglePointCharge.Location = new System.Drawing.Point(10, 545);
			this.GbxSinglePointCharge.Margin = new System.Windows.Forms.Padding(10);
			this.GbxSinglePointCharge.Name = "GbxSinglePointCharge";
			this.GbxSinglePointCharge.Padding = new System.Windows.Forms.Padding(10);
			this.GbxSinglePointCharge.Size = new System.Drawing.Size(459, 210);
			this.GbxSinglePointCharge.TabIndex = 0;
			this.GbxSinglePointCharge.TabStop = false;
			this.GbxSinglePointCharge.Text = "Single Point Charge";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel2.Controls.Add(this.MsxEMFCalculationMethodSelection, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.LblEMFCalculationMethodSelection, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.TbxPointChargeRapidity, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.LblRadialDistance, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.TbxRadialDistance, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.LblStartEffectiveTime, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.TbxStartEffectiveTime, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.LblStopEffectiveTime, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.TbxStopEffectiveTime, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.LblPointChargeRapidity, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 34);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 5;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(439, 166);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// MsxEMFCalculationMethodSelection
			// 
			this.MsxEMFCalculationMethodSelection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxEMFCalculationMethodSelection.Location = new System.Drawing.Point(244, 104);
			this.MsxEMFCalculationMethodSelection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxEMFCalculationMethodSelection.Name = "MsxEMFCalculationMethodSelection";
			this.MsxEMFCalculationMethodSelection.SelectionString = "";
			this.MsxEMFCalculationMethodSelection.Size = new System.Drawing.Size(192, 68);
			this.MsxEMFCalculationMethodSelection.TabIndex = 0;
			// 
			// LblEMFCalculationMethodSelection
			// 
			this.LblEMFCalculationMethodSelection.AutoSize = true;
			this.LblEMFCalculationMethodSelection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblEMFCalculationMethodSelection.Location = new System.Drawing.Point(3, 100);
			this.LblEMFCalculationMethodSelection.Name = "LblEMFCalculationMethodSelection";
			this.LblEMFCalculationMethodSelection.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LblEMFCalculationMethodSelection.Size = new System.Drawing.Size(235, 75);
			this.LblEMFCalculationMethodSelection.TabIndex = 0;
			this.LblEMFCalculationMethodSelection.Text = "EMFCalculationMethodSelection";
			// 
			// TbxPointChargeRapidity
			// 
			this.TbxPointChargeRapidity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxPointChargeRapidity.Location = new System.Drawing.Point(244, 3);
			this.TbxPointChargeRapidity.Name = "TbxPointChargeRapidity";
			this.TbxPointChargeRapidity.Size = new System.Drawing.Size(192, 31);
			this.TbxPointChargeRapidity.TabIndex = 0;
			// 
			// LblRadialDistance
			// 
			this.LblRadialDistance.AutoSize = true;
			this.LblRadialDistance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblRadialDistance.Location = new System.Drawing.Point(3, 25);
			this.LblRadialDistance.Name = "LblRadialDistance";
			this.LblRadialDistance.Size = new System.Drawing.Size(235, 25);
			this.LblRadialDistance.TabIndex = 0;
			this.LblRadialDistance.Text = "RadialDistance (fm)";
			this.LblRadialDistance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxRadialDistance
			// 
			this.TbxRadialDistance.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxRadialDistance.Location = new System.Drawing.Point(244, 28);
			this.TbxRadialDistance.Name = "TbxRadialDistance";
			this.TbxRadialDistance.Size = new System.Drawing.Size(192, 31);
			this.TbxRadialDistance.TabIndex = 0;
			// 
			// LblStartEffectiveTime
			// 
			this.LblStartEffectiveTime.AutoSize = true;
			this.LblStartEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblStartEffectiveTime.Location = new System.Drawing.Point(3, 50);
			this.LblStartEffectiveTime.Name = "LblStartEffectiveTime";
			this.LblStartEffectiveTime.Size = new System.Drawing.Size(235, 25);
			this.LblStartEffectiveTime.TabIndex = 0;
			this.LblStartEffectiveTime.Text = "StartEffectiveTime (fm/c)";
			this.LblStartEffectiveTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxStartEffectiveTime
			// 
			this.TbxStartEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxStartEffectiveTime.Location = new System.Drawing.Point(244, 53);
			this.TbxStartEffectiveTime.Name = "TbxStartEffectiveTime";
			this.TbxStartEffectiveTime.Size = new System.Drawing.Size(192, 31);
			this.TbxStartEffectiveTime.TabIndex = 0;
			// 
			// LblStopEffectiveTime
			// 
			this.LblStopEffectiveTime.AutoSize = true;
			this.LblStopEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblStopEffectiveTime.Location = new System.Drawing.Point(3, 75);
			this.LblStopEffectiveTime.Name = "LblStopEffectiveTime";
			this.LblStopEffectiveTime.Size = new System.Drawing.Size(235, 25);
			this.LblStopEffectiveTime.TabIndex = 0;
			this.LblStopEffectiveTime.Text = "StopEffectiveTime (fm/c)";
			this.LblStopEffectiveTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxStopEffectiveTime
			// 
			this.TbxStopEffectiveTime.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxStopEffectiveTime.Location = new System.Drawing.Point(244, 78);
			this.TbxStopEffectiveTime.Name = "TbxStopEffectiveTime";
			this.TbxStopEffectiveTime.Size = new System.Drawing.Size(192, 31);
			this.TbxStopEffectiveTime.TabIndex = 0;
			// 
			// LblPointChargeRapidity
			// 
			this.LblPointChargeRapidity.AutoSize = true;
			this.LblPointChargeRapidity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblPointChargeRapidity.Location = new System.Drawing.Point(3, 0);
			this.LblPointChargeRapidity.Name = "LblPointChargeRapidity";
			this.LblPointChargeRapidity.Size = new System.Drawing.Size(235, 25);
			this.LblPointChargeRapidity.TabIndex = 0;
			this.LblPointChargeRapidity.Text = "PointChargeRapidity";
			this.LblPointChargeRapidity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GbxOutput
			// 
			this.GbxOutput.Controls.Add(this.tableLayoutPanel1);
			this.GbxOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxOutput.Location = new System.Drawing.Point(10, 775);
			this.GbxOutput.Margin = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Name = "GbxOutput";
			this.GbxOutput.Padding = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Size = new System.Drawing.Size(459, 60);
			this.GbxOutput.TabIndex = 0;
			this.GbxOutput.TabStop = false;
			this.GbxOutput.Text = "Output";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel1.Controls.Add(this.LblDataFileName, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.TbxDataFileName, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 34);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 16);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// LblDataFileName
			// 
			this.LblDataFileName.AutoSize = true;
			this.LblDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDataFileName.Location = new System.Drawing.Point(3, 0);
			this.LblDataFileName.Name = "LblDataFileName";
			this.LblDataFileName.Size = new System.Drawing.Size(235, 25);
			this.LblDataFileName.TabIndex = 0;
			this.LblDataFileName.Text = "DataFileName";
			this.LblDataFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDataFileName
			// 
			this.TbxDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDataFileName.Location = new System.Drawing.Point(244, 3);
			this.TbxDataFileName.Name = "TbxDataFileName";
			this.TbxDataFileName.Size = new System.Drawing.Size(192, 31);
			this.TbxDataFileName.TabIndex = 0;
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
			this.CtrlTextBoxLog.TabIndex = 0;
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
		private System.Windows.Forms.GroupBox GbxOutput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label LblDataFileName;
		private System.Windows.Forms.TextBox TbxDataFileName;
		private System.Windows.Forms.GroupBox GbxSinglePointCharge;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label LblRadialDistance;
		private System.Windows.Forms.TextBox TbxRadialDistance;
		private System.Windows.Forms.Label LblStartEffectiveTime;
		private System.Windows.Forms.TextBox TbxStartEffectiveTime;
		private System.Windows.Forms.Label LblStopEffectiveTime;
		private System.Windows.Forms.TextBox TbxStopEffectiveTime;
		private System.Windows.Forms.Label LblPointChargeRapidity;
		private System.Windows.Forms.TextBox TbxPointChargeRapidity;
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
		private System.Windows.Forms.Label LblNucleusShapeA;
		private System.Windows.Forms.ComboBox CbxNucleusShapeA;
		private System.Windows.Forms.Label LblNucleusShapeB;
		private System.Windows.Forms.ComboBox CbxNucleusShapeB;
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
		private System.Windows.Forms.Label LblImpactParameter;
		private System.Windows.Forms.TextBox TbxImpactParameter;
		private System.Windows.Forms.Label LblProtonNumberB;
		private System.Windows.Forms.TextBox TbxProtonNumberA;
		private System.Windows.Forms.Label LblProtonNumberA;
		private System.Windows.Forms.TextBox TbxProtonNumberB;
		private System.Windows.Forms.Label LblGridCellSize;
		private System.Windows.Forms.TextBox TbxGridCellSize;
		private System.Windows.Forms.Label LblGridRadius;
		private System.Windows.Forms.TextBox TbxGridRadius;
		private System.Windows.Forms.Label LblQuadratureOrder;
		private System.Windows.Forms.TextBox TbxQuadratureOrder;
		private System.Windows.Forms.TextBox TbxSamples;
		private System.Windows.Forms.Label LblSamples;
	}
}
