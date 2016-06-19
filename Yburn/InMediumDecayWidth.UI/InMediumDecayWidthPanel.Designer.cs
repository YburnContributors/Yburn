namespace Yburn.InMediumDecayWidth.UI
{
	partial class InMediumDecayWidthPanel
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
			this.GbxAverageParams = new System.Windows.Forms.GroupBox();
			this.LayoutAverageParams = new System.Windows.Forms.TableLayoutPanel();
			this.LblMinTemperature = new System.Windows.Forms.Label();
			this.TbxMinTemperature = new System.Windows.Forms.TextBox();
			this.LblMaxTemperature = new System.Windows.Forms.Label();
			this.TbxMaxTemperature = new System.Windows.Forms.TextBox();
			this.LblTemperatureStepSize = new System.Windows.Forms.Label();
			this.TbxTemperatureStepSize = new System.Windows.Forms.TextBox();
			this.LblMediumVelocity = new System.Windows.Forms.Label();
			this.TbxMediumVelocity = new System.Windows.Forms.TextBox();
			this.LblDecayWidthAveragingAngles = new System.Windows.Forms.Label();
			this.TbxDecayWidthAveragingAngles = new System.Windows.Forms.TextBox();
			this.LblUseAveragedTemperature = new System.Windows.Forms.Label();
			this.ChkUseAveragedTemperature = new System.Windows.Forms.CheckBox();
			this.LblDecayWidthType = new System.Windows.Forms.Label();
			this.CbxDecayWidthType = new System.Windows.Forms.ComboBox();
			this.LblPotentialTypes = new System.Windows.Forms.Label();
			this.MsxPotentialTypes = new Yburn.UI.MultiSelectBox();
			this.LblBottomiumStates = new System.Windows.Forms.Label();
			this.MsxBottomiumStates = new Yburn.UI.MultiSelectBox();
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
			this.GbxAverageParams.SuspendLayout();
			this.LayoutAverageParams.SuspendLayout();
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
			this.LayoutBottom.Controls.Add(this.GbxAverageParams, 0, 0);
			this.LayoutBottom.Controls.Add(this.GbxOutput, 0, 1);
			this.LayoutBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutBottom.Location = new System.Drawing.Point(0, 0);
			this.LayoutBottom.Name = "LayoutBottom";
			this.LayoutBottom.RowCount = 2;
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.Size = new System.Drawing.Size(500, 503);
			this.LayoutBottom.TabIndex = 0;
			// 
			// GbxAverageParams
			// 
			this.GbxAverageParams.Controls.Add(this.LayoutAverageParams);
			this.GbxAverageParams.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxAverageParams.Location = new System.Drawing.Point(10, 10);
			this.GbxAverageParams.Margin = new System.Windows.Forms.Padding(10);
			this.GbxAverageParams.Name = "GbxAverageParams";
			this.GbxAverageParams.Padding = new System.Windows.Forms.Padding(10);
			this.GbxAverageParams.Size = new System.Drawing.Size(459, 368);
			this.GbxAverageParams.TabIndex = 0;
			this.GbxAverageParams.TabStop = false;
			this.GbxAverageParams.Text = "Averaging Parameters";
			// 
			// LayoutAverageParams
			// 
			this.LayoutAverageParams.ColumnCount = 2;
			this.LayoutAverageParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutAverageParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutAverageParams.Controls.Add(this.LblMinTemperature, 0, 0);
			this.LayoutAverageParams.Controls.Add(this.TbxMinTemperature, 1, 0);
			this.LayoutAverageParams.Controls.Add(this.LblMaxTemperature, 0, 1);
			this.LayoutAverageParams.Controls.Add(this.TbxMaxTemperature, 1, 1);
			this.LayoutAverageParams.Controls.Add(this.LblTemperatureStepSize, 0, 2);
			this.LayoutAverageParams.Controls.Add(this.TbxTemperatureStepSize, 1, 2);
			this.LayoutAverageParams.Controls.Add(this.LblMediumVelocity, 0, 3);
			this.LayoutAverageParams.Controls.Add(this.TbxMediumVelocity, 1, 3);
			this.LayoutAverageParams.Controls.Add(this.LblDecayWidthAveragingAngles, 0, 4);
			this.LayoutAverageParams.Controls.Add(this.TbxDecayWidthAveragingAngles, 1, 4);
			this.LayoutAverageParams.Controls.Add(this.LblUseAveragedTemperature, 0, 5);
			this.LayoutAverageParams.Controls.Add(this.ChkUseAveragedTemperature, 1, 5);
			this.LayoutAverageParams.Controls.Add(this.LblDecayWidthType, 0, 6);
			this.LayoutAverageParams.Controls.Add(this.CbxDecayWidthType, 1, 6);
			this.LayoutAverageParams.Controls.Add(this.LblPotentialTypes, 0, 7);
			this.LayoutAverageParams.Controls.Add(this.MsxPotentialTypes, 1, 7);
			this.LayoutAverageParams.Controls.Add(this.LblBottomiumStates, 0, 8);
			this.LayoutAverageParams.Controls.Add(this.MsxBottomiumStates, 1, 8);
			this.LayoutAverageParams.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutAverageParams.Location = new System.Drawing.Point(10, 25);
			this.LayoutAverageParams.Name = "LayoutAverageParams";
			this.LayoutAverageParams.Padding = new System.Windows.Forms.Padding(5);
			this.LayoutAverageParams.RowCount = 10;
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutAverageParams.Size = new System.Drawing.Size(439, 333);
			this.LayoutAverageParams.TabIndex = 0;
			// 
			// LblMinTemperature
			// 
			this.LblMinTemperature.AutoSize = true;
			this.LblMinTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMinTemperature.Location = new System.Drawing.Point(8, 5);
			this.LblMinTemperature.Name = "LblMinTemperature";
			this.LblMinTemperature.Size = new System.Drawing.Size(229, 25);
			this.LblMinTemperature.TabIndex = 0;
			this.LblMinTemperature.Text = "MinTemperature (MeV)";
			this.LblMinTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMinTemperature
			// 
			this.TbxMinTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMinTemperature.Location = new System.Drawing.Point(243, 8);
			this.TbxMinTemperature.Name = "TbxMinTemperature";
			this.TbxMinTemperature.Size = new System.Drawing.Size(188, 22);
			this.TbxMinTemperature.TabIndex = 0;
			// 
			// LblMaxTemperature
			// 
			this.LblMaxTemperature.AutoSize = true;
			this.LblMaxTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMaxTemperature.Location = new System.Drawing.Point(8, 30);
			this.LblMaxTemperature.Name = "LblMaxTemperature";
			this.LblMaxTemperature.Size = new System.Drawing.Size(229, 25);
			this.LblMaxTemperature.TabIndex = 0;
			this.LblMaxTemperature.Text = "MaxTemperature (MeV)";
			this.LblMaxTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMaxTemperature
			// 
			this.TbxMaxTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMaxTemperature.Location = new System.Drawing.Point(243, 33);
			this.TbxMaxTemperature.Name = "TbxMaxTemperature";
			this.TbxMaxTemperature.Size = new System.Drawing.Size(188, 22);
			this.TbxMaxTemperature.TabIndex = 1;
			// 
			// LblTemperatureStepSize
			// 
			this.LblTemperatureStepSize.AutoSize = true;
			this.LblTemperatureStepSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblTemperatureStepSize.Location = new System.Drawing.Point(8, 55);
			this.LblTemperatureStepSize.Name = "LblTemperatureStepSize";
			this.LblTemperatureStepSize.Size = new System.Drawing.Size(229, 25);
			this.LblTemperatureStepSize.TabIndex = 7;
			this.LblTemperatureStepSize.Text = "TemperatureStepSize (MeV)";
			this.LblTemperatureStepSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxTemperatureStepSize
			// 
			this.TbxTemperatureStepSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxTemperatureStepSize.Location = new System.Drawing.Point(243, 58);
			this.TbxTemperatureStepSize.Name = "TbxTemperatureStepSize";
			this.TbxTemperatureStepSize.Size = new System.Drawing.Size(188, 22);
			this.TbxTemperatureStepSize.TabIndex = 2;
			// 
			// LblMediumVelocity
			// 
			this.LblMediumVelocity.AutoSize = true;
			this.LblMediumVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMediumVelocity.Location = new System.Drawing.Point(8, 80);
			this.LblMediumVelocity.Name = "LblMediumVelocity";
			this.LblMediumVelocity.Size = new System.Drawing.Size(229, 25);
			this.LblMediumVelocity.TabIndex = 0;
			this.LblMediumVelocity.Text = "MediumVelocity";
			this.LblMediumVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMediumVelocity
			// 
			this.TbxMediumVelocity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMediumVelocity.Location = new System.Drawing.Point(243, 83);
			this.TbxMediumVelocity.Name = "TbxMediumVelocity";
			this.TbxMediumVelocity.Size = new System.Drawing.Size(188, 22);
			this.TbxMediumVelocity.TabIndex = 3;
			// 
			// LblDecayWidthAveragingAngles
			// 
			this.LblDecayWidthAveragingAngles.AutoSize = true;
			this.LblDecayWidthAveragingAngles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDecayWidthAveragingAngles.Location = new System.Drawing.Point(8, 105);
			this.LblDecayWidthAveragingAngles.Name = "LblDecayWidthAveragingAngles";
			this.LblDecayWidthAveragingAngles.Size = new System.Drawing.Size(229, 25);
			this.LblDecayWidthAveragingAngles.TabIndex = 0;
			this.LblDecayWidthAveragingAngles.Text = "DecayWidthAveragingAngles (deg)";
			this.LblDecayWidthAveragingAngles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDecayWidthAveragingAngles
			// 
			this.TbxDecayWidthAveragingAngles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDecayWidthAveragingAngles.Location = new System.Drawing.Point(243, 108);
			this.TbxDecayWidthAveragingAngles.Name = "TbxDecayWidthAveragingAngles";
			this.TbxDecayWidthAveragingAngles.Size = new System.Drawing.Size(188, 22);
			this.TbxDecayWidthAveragingAngles.TabIndex = 4;
			// 
			// LblUseAveragedTemperature
			// 
			this.LblUseAveragedTemperature.AutoSize = true;
			this.LblUseAveragedTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblUseAveragedTemperature.Location = new System.Drawing.Point(8, 130);
			this.LblUseAveragedTemperature.Name = "LblUseAveragedTemperature";
			this.LblUseAveragedTemperature.Size = new System.Drawing.Size(229, 25);
			this.LblUseAveragedTemperature.TabIndex = 9;
			this.LblUseAveragedTemperature.Text = "UseAveragedTemperature";
			this.LblUseAveragedTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ChkUseAveragedTemperature
			// 
			this.ChkUseAveragedTemperature.AutoSize = true;
			this.ChkUseAveragedTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChkUseAveragedTemperature.Location = new System.Drawing.Point(243, 133);
			this.ChkUseAveragedTemperature.Name = "ChkUseAveragedTemperature";
			this.ChkUseAveragedTemperature.Size = new System.Drawing.Size(188, 19);
			this.ChkUseAveragedTemperature.TabIndex = 5;
			// 
			// LblDecayWidthType
			// 
			this.LblDecayWidthType.AutoSize = true;
			this.LblDecayWidthType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDecayWidthType.Location = new System.Drawing.Point(8, 155);
			this.LblDecayWidthType.Name = "LblDecayWidthType";
			this.LblDecayWidthType.Size = new System.Drawing.Size(229, 25);
			this.LblDecayWidthType.TabIndex = 0;
			this.LblDecayWidthType.Text = "DecayWidthType";
			this.LblDecayWidthType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CbxDecayWidthType
			// 
			this.CbxDecayWidthType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxDecayWidthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxDecayWidthType.Location = new System.Drawing.Point(243, 158);
			this.CbxDecayWidthType.Name = "CbxDecayWidthType";
			this.CbxDecayWidthType.Size = new System.Drawing.Size(188, 24);
			this.CbxDecayWidthType.TabIndex = 6;
			// 
			// LblPotentialTypes
			// 
			this.LblPotentialTypes.AutoSize = true;
			this.LblPotentialTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblPotentialTypes.Location = new System.Drawing.Point(8, 180);
			this.LblPotentialTypes.Name = "LblPotentialTypes";
			this.LblPotentialTypes.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LblPotentialTypes.Size = new System.Drawing.Size(229, 79);
			this.LblPotentialTypes.TabIndex = 0;
			this.LblPotentialTypes.Text = "PotentialTypes";
			// 
			// MsxPotentialTypes
			// 
			this.MsxPotentialTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxPotentialTypes.Location = new System.Drawing.Point(243, 184);
			this.MsxPotentialTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxPotentialTypes.Name = "MsxPotentialTypes";
			this.MsxPotentialTypes.SelectionString = "";
			this.MsxPotentialTypes.Size = new System.Drawing.Size(188, 72);
			this.MsxPotentialTypes.TabIndex = 7;
			// 
			// LblBottomiumStates
			// 
			this.LblBottomiumStates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblBottomiumStates.Location = new System.Drawing.Point(8, 259);
			this.LblBottomiumStates.Name = "LblBottomiumStates";
			this.LblBottomiumStates.Size = new System.Drawing.Size(229, 79);
			this.LblBottomiumStates.TabIndex = 0;
			this.LblBottomiumStates.Text = "BottomiumStates";
			// 
			// MsxBottomiumStates
			// 
			this.MsxBottomiumStates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxBottomiumStates.Location = new System.Drawing.Point(244, 264);
			this.MsxBottomiumStates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MsxBottomiumStates.Name = "MsxBottomiumStates";
			this.MsxBottomiumStates.SelectionString = "";
			this.MsxBottomiumStates.Size = new System.Drawing.Size(186, 69);
			this.MsxBottomiumStates.TabIndex = 8;
			// 
			// GbxOutput
			// 
			this.GbxOutput.Controls.Add(this.tableLayoutPanel1);
			this.GbxOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxOutput.Location = new System.Drawing.Point(10, 398);
			this.GbxOutput.Margin = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Name = "GbxOutput";
			this.GbxOutput.Padding = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Size = new System.Drawing.Size(459, 65);
			this.GbxOutput.TabIndex = 1;
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
			this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 30);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// LblDataFileName
			// 
			this.LblDataFileName.AutoSize = true;
			this.LblDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDataFileName.Location = new System.Drawing.Point(8, 5);
			this.LblDataFileName.Name = "LblDataFileName";
			this.LblDataFileName.Size = new System.Drawing.Size(229, 25);
			this.LblDataFileName.TabIndex = 0;
			this.LblDataFileName.Text = "DataFileName";
			this.LblDataFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxDataFileName
			// 
			this.TbxDataFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxDataFileName.Location = new System.Drawing.Point(243, 8);
			this.TbxDataFileName.Name = "TbxDataFileName";
			this.TbxDataFileName.Size = new System.Drawing.Size(188, 22);
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
			// InMediumDecayWidthPanel
			// 
			this.Controls.Add(this.VSplit);
			this.Name = "InMediumDecayWidthPanel";
			this.Size = new System.Drawing.Size(794, 503);
			this.VSplit.Panel1.ResumeLayout(false);
			this.VSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.VSplit)).EndInit();
			this.VSplit.ResumeLayout(false);
			this.LayoutBottom.ResumeLayout(false);
			this.GbxAverageParams.ResumeLayout(false);
			this.LayoutAverageParams.ResumeLayout(false);
			this.LayoutAverageParams.PerformLayout();
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
		private System.Windows.Forms.GroupBox GbxAverageParams;
		private System.Windows.Forms.TableLayoutPanel LayoutAverageParams;
		private System.Windows.Forms.Label LblMediumVelocity;
		private System.Windows.Forms.TextBox TbxMediumVelocity;
		private System.Windows.Forms.Label LblBottomiumStates;
		private Yburn.UI.MultiSelectBox MsxBottomiumStates;
		private System.Windows.Forms.TextBox TbxDecayWidthAveragingAngles;
		private System.Windows.Forms.Label LblDecayWidthAveragingAngles;
		private System.Windows.Forms.Label LblPotentialTypes;
		private Yburn.UI.MultiSelectBox MsxPotentialTypes;
		private System.Windows.Forms.Label LblDecayWidthType;
		private System.Windows.Forms.ComboBox CbxDecayWidthType;
		private System.Windows.Forms.Label LblMaxTemperature;
		private System.Windows.Forms.Label LblMinTemperature;
		private System.Windows.Forms.TextBox TbxMaxTemperature;
		private System.Windows.Forms.TextBox TbxMinTemperature;
		private System.Windows.Forms.TextBox TbxTemperatureStepSize;
		private System.Windows.Forms.Label LblTemperatureStepSize;
		private System.Windows.Forms.Label LblUseAveragedTemperature;
		private System.Windows.Forms.CheckBox ChkUseAveragedTemperature;
		private System.Windows.Forms.GroupBox GbxOutput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label LblDataFileName;
		private System.Windows.Forms.TextBox TbxDataFileName;
	}
}
