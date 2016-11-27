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
			this.TbxQGPFormationTemperature = new System.Windows.Forms.TextBox();
			this.LblQGPFormationTemperature = new System.Windows.Forms.Label();
			this.MsxDopplerShiftEvaluationTypes = new Yburn.UI.MultiSelectBox();
			this.LblMediumTemperatures = new System.Windows.Forms.Label();
			this.TbxMediumTemperatures = new System.Windows.Forms.TextBox();
			this.LblMediumVelocities = new System.Windows.Forms.Label();
			this.TbxMediumVelocities = new System.Windows.Forms.TextBox();
			this.LblNumberAveragingAngles = new System.Windows.Forms.Label();
			this.TbxNumberAveragingAngles = new System.Windows.Forms.TextBox();
			this.LblDopplerShiftEvaluationTypes = new System.Windows.Forms.Label();
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
			this.GbxAverageParams.Size = new System.Drawing.Size(459, 385);
			this.GbxAverageParams.TabIndex = 0;
			this.GbxAverageParams.TabStop = false;
			this.GbxAverageParams.Text = "Averaging Parameters";
			// 
			// LayoutAverageParams
			// 
			this.LayoutAverageParams.ColumnCount = 2;
			this.LayoutAverageParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutAverageParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutAverageParams.Controls.Add(this.TbxQGPFormationTemperature, 1, 7);
			this.LayoutAverageParams.Controls.Add(this.LblQGPFormationTemperature, 0, 7);
			this.LayoutAverageParams.Controls.Add(this.MsxDopplerShiftEvaluationTypes, 1, 3);
			this.LayoutAverageParams.Controls.Add(this.LblMediumTemperatures, 0, 0);
			this.LayoutAverageParams.Controls.Add(this.TbxMediumTemperatures, 1, 0);
			this.LayoutAverageParams.Controls.Add(this.LblMediumVelocities, 0, 1);
			this.LayoutAverageParams.Controls.Add(this.TbxMediumVelocities, 1, 1);
			this.LayoutAverageParams.Controls.Add(this.LblNumberAveragingAngles, 0, 2);
			this.LayoutAverageParams.Controls.Add(this.TbxNumberAveragingAngles, 1, 2);
			this.LayoutAverageParams.Controls.Add(this.LblDopplerShiftEvaluationTypes, 0, 3);
			this.LayoutAverageParams.Controls.Add(this.LblDecayWidthType, 0, 4);
			this.LayoutAverageParams.Controls.Add(this.CbxDecayWidthType, 1, 4);
			this.LayoutAverageParams.Controls.Add(this.LblPotentialTypes, 0, 5);
			this.LayoutAverageParams.Controls.Add(this.MsxPotentialTypes, 1, 5);
			this.LayoutAverageParams.Controls.Add(this.LblBottomiumStates, 0, 6);
			this.LayoutAverageParams.Controls.Add(this.MsxBottomiumStates, 1, 6);
			this.LayoutAverageParams.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutAverageParams.Location = new System.Drawing.Point(10, 34);
			this.LayoutAverageParams.Name = "LayoutAverageParams";
			this.LayoutAverageParams.RowCount = 8;
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.Size = new System.Drawing.Size(439, 341);
			this.LayoutAverageParams.TabIndex = 0;
			// 
			// TbxQGPFormationTemperature
			// 
			this.TbxQGPFormationTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxQGPFormationTemperature.Location = new System.Drawing.Point(244, 328);
			this.TbxQGPFormationTemperature.Name = "TbxQGPFormationTemperature";
			this.TbxQGPFormationTemperature.Size = new System.Drawing.Size(192, 31);
			this.TbxQGPFormationTemperature.TabIndex = 9;
			// 
			// LblQGPFormationTemperature
			// 
			this.LblQGPFormationTemperature.AutoSize = true;
			this.LblQGPFormationTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblQGPFormationTemperature.Location = new System.Drawing.Point(3, 325);
			this.LblQGPFormationTemperature.Name = "LblQGPFormationTemperature";
			this.LblQGPFormationTemperature.Size = new System.Drawing.Size(235, 25);
			this.LblQGPFormationTemperature.TabIndex = 0;
			this.LblQGPFormationTemperature.Text = "QGPFormationTemperature (MeV)";
			this.LblQGPFormationTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MsxDopplerShiftEvaluationTypes
			// 
			this.MsxDopplerShiftEvaluationTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxDopplerShiftEvaluationTypes.Location = new System.Drawing.Point(244, 79);
			this.MsxDopplerShiftEvaluationTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxDopplerShiftEvaluationTypes.Name = "MsxDopplerShiftEvaluationTypes";
			this.MsxDopplerShiftEvaluationTypes.SelectionString = "";
			this.MsxDopplerShiftEvaluationTypes.Size = new System.Drawing.Size(192, 68);
			this.MsxDopplerShiftEvaluationTypes.TabIndex = 5;
			// 
			// LblMediumTemperatures
			// 
			this.LblMediumTemperatures.AutoSize = true;
			this.LblMediumTemperatures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMediumTemperatures.Location = new System.Drawing.Point(3, 0);
			this.LblMediumTemperatures.Name = "LblMediumTemperatures";
			this.LblMediumTemperatures.Size = new System.Drawing.Size(235, 25);
			this.LblMediumTemperatures.TabIndex = 0;
			this.LblMediumTemperatures.Text = "MediumTemperatures (MeV)";
			this.LblMediumTemperatures.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMediumTemperatures
			// 
			this.TbxMediumTemperatures.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMediumTemperatures.Location = new System.Drawing.Point(244, 3);
			this.TbxMediumTemperatures.Name = "TbxMediumTemperatures";
			this.TbxMediumTemperatures.Size = new System.Drawing.Size(192, 31);
			this.TbxMediumTemperatures.TabIndex = 0;
			// 
			// LblMediumVelocities
			// 
			this.LblMediumVelocities.AutoSize = true;
			this.LblMediumVelocities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMediumVelocities.Location = new System.Drawing.Point(3, 25);
			this.LblMediumVelocities.Name = "LblMediumVelocities";
			this.LblMediumVelocities.Size = new System.Drawing.Size(235, 25);
			this.LblMediumVelocities.TabIndex = 0;
			this.LblMediumVelocities.Text = "MediumVelocities";
			this.LblMediumVelocities.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMediumVelocities
			// 
			this.TbxMediumVelocities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMediumVelocities.Location = new System.Drawing.Point(244, 28);
			this.TbxMediumVelocities.Name = "TbxMediumVelocities";
			this.TbxMediumVelocities.Size = new System.Drawing.Size(192, 31);
			this.TbxMediumVelocities.TabIndex = 3;
			// 
			// LblNumberAveragingAngles
			// 
			this.LblNumberAveragingAngles.AutoSize = true;
			this.LblNumberAveragingAngles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNumberAveragingAngles.Location = new System.Drawing.Point(3, 50);
			this.LblNumberAveragingAngles.Name = "LblNumberAveragingAngles";
			this.LblNumberAveragingAngles.Size = new System.Drawing.Size(235, 25);
			this.LblNumberAveragingAngles.TabIndex = 0;
			this.LblNumberAveragingAngles.Text = "NumberAveragingAngles";
			this.LblNumberAveragingAngles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxNumberAveragingAngles
			// 
			this.TbxNumberAveragingAngles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNumberAveragingAngles.Location = new System.Drawing.Point(244, 53);
			this.TbxNumberAveragingAngles.Name = "TbxNumberAveragingAngles";
			this.TbxNumberAveragingAngles.Size = new System.Drawing.Size(192, 31);
			this.TbxNumberAveragingAngles.TabIndex = 4;
			// 
			// LblDopplerShiftEvaluationTypes
			// 
			this.LblDopplerShiftEvaluationTypes.AutoSize = true;
			this.LblDopplerShiftEvaluationTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDopplerShiftEvaluationTypes.Location = new System.Drawing.Point(3, 75);
			this.LblDopplerShiftEvaluationTypes.Name = "LblDopplerShiftEvaluationTypes";
			this.LblDopplerShiftEvaluationTypes.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LblDopplerShiftEvaluationTypes.Size = new System.Drawing.Size(235, 75);
			this.LblDopplerShiftEvaluationTypes.TabIndex = 0;
			this.LblDopplerShiftEvaluationTypes.Text = "DopplerShiftEvaluationTypes";
			// 
			// LblDecayWidthType
			// 
			this.LblDecayWidthType.AutoSize = true;
			this.LblDecayWidthType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDecayWidthType.Location = new System.Drawing.Point(3, 150);
			this.LblDecayWidthType.Name = "LblDecayWidthType";
			this.LblDecayWidthType.Size = new System.Drawing.Size(235, 25);
			this.LblDecayWidthType.TabIndex = 0;
			this.LblDecayWidthType.Text = "DecayWidthType";
			this.LblDecayWidthType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CbxDecayWidthType
			// 
			this.CbxDecayWidthType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxDecayWidthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxDecayWidthType.Location = new System.Drawing.Point(244, 153);
			this.CbxDecayWidthType.Name = "CbxDecayWidthType";
			this.CbxDecayWidthType.Size = new System.Drawing.Size(192, 33);
			this.CbxDecayWidthType.TabIndex = 6;
			// 
			// LblPotentialTypes
			// 
			this.LblPotentialTypes.AutoSize = true;
			this.LblPotentialTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblPotentialTypes.Location = new System.Drawing.Point(3, 175);
			this.LblPotentialTypes.Name = "LblPotentialTypes";
			this.LblPotentialTypes.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LblPotentialTypes.Size = new System.Drawing.Size(235, 75);
			this.LblPotentialTypes.TabIndex = 0;
			this.LblPotentialTypes.Text = "PotentialTypes";
			// 
			// MsxPotentialTypes
			// 
			this.MsxPotentialTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxPotentialTypes.Location = new System.Drawing.Point(244, 179);
			this.MsxPotentialTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxPotentialTypes.Name = "MsxPotentialTypes";
			this.MsxPotentialTypes.SelectionString = "";
			this.MsxPotentialTypes.Size = new System.Drawing.Size(192, 68);
			this.MsxPotentialTypes.TabIndex = 7;
			// 
			// LblBottomiumStates
			// 
			this.LblBottomiumStates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblBottomiumStates.Location = new System.Drawing.Point(3, 250);
			this.LblBottomiumStates.Name = "LblBottomiumStates";
			this.LblBottomiumStates.Size = new System.Drawing.Size(235, 75);
			this.LblBottomiumStates.TabIndex = 0;
			this.LblBottomiumStates.Text = "BottomiumStates";
			// 
			// MsxBottomiumStates
			// 
			this.MsxBottomiumStates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxBottomiumStates.Location = new System.Drawing.Point(244, 254);
			this.MsxBottomiumStates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxBottomiumStates.Name = "MsxBottomiumStates";
			this.MsxBottomiumStates.SelectionString = "";
			this.MsxBottomiumStates.Size = new System.Drawing.Size(192, 68);
			this.MsxBottomiumStates.TabIndex = 8;
			// 
			// GbxOutput
			// 
			this.GbxOutput.Controls.Add(this.tableLayoutPanel1);
			this.GbxOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxOutput.Location = new System.Drawing.Point(10, 415);
			this.GbxOutput.Margin = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Name = "GbxOutput";
			this.GbxOutput.Padding = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Size = new System.Drawing.Size(459, 60);
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
		private System.Windows.Forms.Label LblMediumVelocities;
		private System.Windows.Forms.TextBox TbxMediumVelocities;
		private System.Windows.Forms.Label LblBottomiumStates;
		private Yburn.UI.MultiSelectBox MsxBottomiumStates;
		private System.Windows.Forms.TextBox TbxNumberAveragingAngles;
		private System.Windows.Forms.Label LblNumberAveragingAngles;
		private System.Windows.Forms.Label LblPotentialTypes;
		private Yburn.UI.MultiSelectBox MsxPotentialTypes;
		private System.Windows.Forms.Label LblDecayWidthType;
		private System.Windows.Forms.ComboBox CbxDecayWidthType;
		private System.Windows.Forms.Label LblMediumTemperatures;
		private System.Windows.Forms.TextBox TbxMediumTemperatures;
		private System.Windows.Forms.Label LblDopplerShiftEvaluationTypes;
		private System.Windows.Forms.GroupBox GbxOutput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label LblDataFileName;
		private System.Windows.Forms.TextBox TbxDataFileName;
		private Yburn.UI.MultiSelectBox MsxDopplerShiftEvaluationTypes;
		private System.Windows.Forms.TextBox TbxQGPFormationTemperature;
		private System.Windows.Forms.Label LblQGPFormationTemperature;
	}
}
