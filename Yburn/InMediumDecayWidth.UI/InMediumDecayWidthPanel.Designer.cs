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
			this.GbxElectromagneticEnergy = new System.Windows.Forms.GroupBox();
			this.LayoutElectromagneticEnergy = new System.Windows.Forms.TableLayoutPanel();
			this.CbxElectricDipoleAlignment = new System.Windows.Forms.ComboBox();
			this.LblMagneticFieldStrength = new System.Windows.Forms.Label();
			this.LblElectricFieldStrength = new System.Windows.Forms.Label();
			this.LblElectricDipoleAlignment = new System.Windows.Forms.Label();
			this.TbxMagneticFieldStrength = new System.Windows.Forms.TextBox();
			this.TbxElectricFieldStrength = new System.Windows.Forms.TextBox();
			this.GbxDopplerShift = new System.Windows.Forms.GroupBox();
			this.LayoutDopplerShift = new System.Windows.Forms.TableLayoutPanel();
			this.TbxNumberAveragingAngles = new System.Windows.Forms.TextBox();
			this.LblNumberAveragingAngles = new System.Windows.Forms.Label();
			this.MsxDopplerShiftEvaluationTypes = new Yburn.UI.MultiSelectBox();
			this.LblDopplerShiftEvaluationTypes = new System.Windows.Forms.Label();
			this.GbxGeneralParams = new System.Windows.Forms.GroupBox();
			this.LayoutAverageParams = new System.Windows.Forms.TableLayoutPanel();
			this.TbxQGPFormationTemperature = new System.Windows.Forms.TextBox();
			this.LblQGPFormationTemperature = new System.Windows.Forms.Label();
			this.LblMediumTemperatures = new System.Windows.Forms.Label();
			this.TbxMediumTemperatures = new System.Windows.Forms.TextBox();
			this.LblMediumVelocities = new System.Windows.Forms.Label();
			this.TbxMediumVelocities = new System.Windows.Forms.TextBox();
			this.LblDecayWidthType = new System.Windows.Forms.Label();
			this.CbxDecayWidthType = new System.Windows.Forms.ComboBox();
			this.LblPotentialTypes = new System.Windows.Forms.Label();
			this.MsxPotentialTypes = new Yburn.UI.MultiSelectBox();
			this.LblBottomiumStates = new System.Windows.Forms.Label();
			this.MsxBottomiumStates = new Yburn.UI.MultiSelectBox();
			this.GbxOutput = new System.Windows.Forms.GroupBox();
			this.LayoutOutput = new System.Windows.Forms.TableLayoutPanel();
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
			this.GbxElectromagneticEnergy.SuspendLayout();
			this.LayoutElectromagneticEnergy.SuspendLayout();
			this.GbxDopplerShift.SuspendLayout();
			this.LayoutDopplerShift.SuspendLayout();
			this.GbxGeneralParams.SuspendLayout();
			this.LayoutAverageParams.SuspendLayout();
			this.GbxOutput.SuspendLayout();
			this.LayoutOutput.SuspendLayout();
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
			this.LayoutBottom.Controls.Add(this.GbxElectromagneticEnergy, 0, 2);
			this.LayoutBottom.Controls.Add(this.GbxDopplerShift, 0, 1);
			this.LayoutBottom.Controls.Add(this.GbxGeneralParams, 0, 0);
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
			// GbxElectromagneticEnergy
			// 
			this.GbxElectromagneticEnergy.Controls.Add(this.LayoutElectromagneticEnergy);
			this.GbxElectromagneticEnergy.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxElectromagneticEnergy.Location = new System.Drawing.Point(10, 470);
			this.GbxElectromagneticEnergy.Margin = new System.Windows.Forms.Padding(10);
			this.GbxElectromagneticEnergy.Name = "GbxElectromagneticEnergy";
			this.GbxElectromagneticEnergy.Padding = new System.Windows.Forms.Padding(10);
			this.GbxElectromagneticEnergy.Size = new System.Drawing.Size(459, 110);
			this.GbxElectromagneticEnergy.TabIndex = 0;
			this.GbxElectromagneticEnergy.TabStop = false;
			this.GbxElectromagneticEnergy.Text = "Electromagnetic Energy";
			// 
			// LayoutElectromagneticEnergy
			// 
			this.LayoutElectromagneticEnergy.ColumnCount = 2;
			this.LayoutElectromagneticEnergy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutElectromagneticEnergy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutElectromagneticEnergy.Controls.Add(this.CbxElectricDipoleAlignment, 1, 0);
			this.LayoutElectromagneticEnergy.Controls.Add(this.LblMagneticFieldStrength, 0, 2);
			this.LayoutElectromagneticEnergy.Controls.Add(this.LblElectricFieldStrength, 0, 1);
			this.LayoutElectromagneticEnergy.Controls.Add(this.LblElectricDipoleAlignment, 0, 0);
			this.LayoutElectromagneticEnergy.Controls.Add(this.TbxMagneticFieldStrength, 1, 2);
			this.LayoutElectromagneticEnergy.Controls.Add(this.TbxElectricFieldStrength, 1, 1);
			this.LayoutElectromagneticEnergy.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutElectromagneticEnergy.Location = new System.Drawing.Point(10, 34);
			this.LayoutElectromagneticEnergy.Name = "LayoutElectromagneticEnergy";
			this.LayoutElectromagneticEnergy.RowCount = 3;
			this.LayoutElectromagneticEnergy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutElectromagneticEnergy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutElectromagneticEnergy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutElectromagneticEnergy.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.LayoutElectromagneticEnergy.Size = new System.Drawing.Size(439, 66);
			this.LayoutElectromagneticEnergy.TabIndex = 0;
			// 
			// CbxElectricDipoleAlignment
			// 
			this.CbxElectricDipoleAlignment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CbxElectricDipoleAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbxElectricDipoleAlignment.Location = new System.Drawing.Point(244, 3);
			this.CbxElectricDipoleAlignment.Name = "CbxElectricDipoleAlignment";
			this.CbxElectricDipoleAlignment.Size = new System.Drawing.Size(192, 33);
			this.CbxElectricDipoleAlignment.TabIndex = 0;
			// 
			// LblMagneticFieldStrength
			// 
			this.LblMagneticFieldStrength.AutoSize = true;
			this.LblMagneticFieldStrength.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMagneticFieldStrength.Location = new System.Drawing.Point(3, 50);
			this.LblMagneticFieldStrength.Name = "LblMagneticFieldStrength";
			this.LblMagneticFieldStrength.Size = new System.Drawing.Size(235, 25);
			this.LblMagneticFieldStrength.TabIndex = 0;
			this.LblMagneticFieldStrength.Text = "MagneticFieldStrength (1/fm²)";
			this.LblMagneticFieldStrength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblElectricFieldStrength
			// 
			this.LblElectricFieldStrength.AutoSize = true;
			this.LblElectricFieldStrength.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblElectricFieldStrength.Location = new System.Drawing.Point(3, 25);
			this.LblElectricFieldStrength.Name = "LblElectricFieldStrength";
			this.LblElectricFieldStrength.Size = new System.Drawing.Size(235, 25);
			this.LblElectricFieldStrength.TabIndex = 0;
			this.LblElectricFieldStrength.Text = "ElectricFieldStrength (1/fm²)";
			this.LblElectricFieldStrength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblElectricDipoleAlignment
			// 
			this.LblElectricDipoleAlignment.AutoSize = true;
			this.LblElectricDipoleAlignment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblElectricDipoleAlignment.Location = new System.Drawing.Point(3, 0);
			this.LblElectricDipoleAlignment.Name = "LblElectricDipoleAlignment";
			this.LblElectricDipoleAlignment.Size = new System.Drawing.Size(235, 25);
			this.LblElectricDipoleAlignment.TabIndex = 0;
			this.LblElectricDipoleAlignment.Text = "ElectricDipoleAlignment";
			this.LblElectricDipoleAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMagneticFieldStrength
			// 
			this.TbxMagneticFieldStrength.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMagneticFieldStrength.Location = new System.Drawing.Point(244, 53);
			this.TbxMagneticFieldStrength.Name = "TbxMagneticFieldStrength";
			this.TbxMagneticFieldStrength.Size = new System.Drawing.Size(192, 31);
			this.TbxMagneticFieldStrength.TabIndex = 0;
			// 
			// TbxElectricFieldStrength
			// 
			this.TbxElectricFieldStrength.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxElectricFieldStrength.Location = new System.Drawing.Point(244, 28);
			this.TbxElectricFieldStrength.Name = "TbxElectricFieldStrength";
			this.TbxElectricFieldStrength.Size = new System.Drawing.Size(192, 31);
			this.TbxElectricFieldStrength.TabIndex = 0;
			// 
			// GbxDopplerShift
			// 
			this.GbxDopplerShift.Controls.Add(this.LayoutDopplerShift);
			this.GbxDopplerShift.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxDopplerShift.Location = new System.Drawing.Point(10, 315);
			this.GbxDopplerShift.Margin = new System.Windows.Forms.Padding(10);
			this.GbxDopplerShift.Name = "GbxDopplerShift";
			this.GbxDopplerShift.Padding = new System.Windows.Forms.Padding(10);
			this.GbxDopplerShift.Size = new System.Drawing.Size(459, 135);
			this.GbxDopplerShift.TabIndex = 0;
			this.GbxDopplerShift.TabStop = false;
			this.GbxDopplerShift.Text = "Doppler Shift";
			// 
			// LayoutDopplerShift
			// 
			this.LayoutDopplerShift.ColumnCount = 2;
			this.LayoutDopplerShift.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutDopplerShift.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutDopplerShift.Controls.Add(this.TbxNumberAveragingAngles, 1, 1);
			this.LayoutDopplerShift.Controls.Add(this.LblNumberAveragingAngles, 0, 1);
			this.LayoutDopplerShift.Controls.Add(this.MsxDopplerShiftEvaluationTypes, 1, 0);
			this.LayoutDopplerShift.Controls.Add(this.LblDopplerShiftEvaluationTypes, 0, 0);
			this.LayoutDopplerShift.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutDopplerShift.Location = new System.Drawing.Point(10, 34);
			this.LayoutDopplerShift.Name = "LayoutDopplerShift";
			this.LayoutDopplerShift.RowCount = 2;
			this.LayoutDopplerShift.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.LayoutDopplerShift.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutDopplerShift.Size = new System.Drawing.Size(439, 91);
			this.LayoutDopplerShift.TabIndex = 0;
			// 
			// TbxNumberAveragingAngles
			// 
			this.TbxNumberAveragingAngles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxNumberAveragingAngles.Location = new System.Drawing.Point(244, 78);
			this.TbxNumberAveragingAngles.Name = "TbxNumberAveragingAngles";
			this.TbxNumberAveragingAngles.Size = new System.Drawing.Size(192, 31);
			this.TbxNumberAveragingAngles.TabIndex = 0;
			// 
			// LblNumberAveragingAngles
			// 
			this.LblNumberAveragingAngles.AutoSize = true;
			this.LblNumberAveragingAngles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblNumberAveragingAngles.Location = new System.Drawing.Point(3, 75);
			this.LblNumberAveragingAngles.Name = "LblNumberAveragingAngles";
			this.LblNumberAveragingAngles.Size = new System.Drawing.Size(235, 25);
			this.LblNumberAveragingAngles.TabIndex = 0;
			this.LblNumberAveragingAngles.Text = "NumberAveragingAngles";
			this.LblNumberAveragingAngles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MsxDopplerShiftEvaluationTypes
			// 
			this.MsxDopplerShiftEvaluationTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxDopplerShiftEvaluationTypes.Location = new System.Drawing.Point(244, 4);
			this.MsxDopplerShiftEvaluationTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxDopplerShiftEvaluationTypes.Name = "MsxDopplerShiftEvaluationTypes";
			this.MsxDopplerShiftEvaluationTypes.SelectionString = "";
			this.MsxDopplerShiftEvaluationTypes.Size = new System.Drawing.Size(192, 68);
			this.MsxDopplerShiftEvaluationTypes.TabIndex = 0;
			// 
			// LblDopplerShiftEvaluationTypes
			// 
			this.LblDopplerShiftEvaluationTypes.AutoSize = true;
			this.LblDopplerShiftEvaluationTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDopplerShiftEvaluationTypes.Location = new System.Drawing.Point(3, 0);
			this.LblDopplerShiftEvaluationTypes.Name = "LblDopplerShiftEvaluationTypes";
			this.LblDopplerShiftEvaluationTypes.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LblDopplerShiftEvaluationTypes.Size = new System.Drawing.Size(235, 75);
			this.LblDopplerShiftEvaluationTypes.TabIndex = 0;
			this.LblDopplerShiftEvaluationTypes.Text = "DopplerShiftEvaluationTypes";
			// 
			// GbxGeneralParams
			// 
			this.GbxGeneralParams.Controls.Add(this.LayoutAverageParams);
			this.GbxGeneralParams.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxGeneralParams.Location = new System.Drawing.Point(10, 10);
			this.GbxGeneralParams.Margin = new System.Windows.Forms.Padding(10);
			this.GbxGeneralParams.Name = "GbxGeneralParams";
			this.GbxGeneralParams.Padding = new System.Windows.Forms.Padding(10);
			this.GbxGeneralParams.Size = new System.Drawing.Size(459, 285);
			this.GbxGeneralParams.TabIndex = 0;
			this.GbxGeneralParams.TabStop = false;
			this.GbxGeneralParams.Text = "General Parameters";
			// 
			// LayoutAverageParams
			// 
			this.LayoutAverageParams.ColumnCount = 2;
			this.LayoutAverageParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutAverageParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutAverageParams.Controls.Add(this.TbxQGPFormationTemperature, 1, 5);
			this.LayoutAverageParams.Controls.Add(this.LblQGPFormationTemperature, 0, 5);
			this.LayoutAverageParams.Controls.Add(this.LblMediumTemperatures, 0, 0);
			this.LayoutAverageParams.Controls.Add(this.TbxMediumTemperatures, 1, 0);
			this.LayoutAverageParams.Controls.Add(this.LblMediumVelocities, 0, 1);
			this.LayoutAverageParams.Controls.Add(this.TbxMediumVelocities, 1, 1);
			this.LayoutAverageParams.Controls.Add(this.LblDecayWidthType, 0, 2);
			this.LayoutAverageParams.Controls.Add(this.CbxDecayWidthType, 1, 2);
			this.LayoutAverageParams.Controls.Add(this.LblPotentialTypes, 0, 3);
			this.LayoutAverageParams.Controls.Add(this.MsxPotentialTypes, 1, 3);
			this.LayoutAverageParams.Controls.Add(this.LblBottomiumStates, 0, 4);
			this.LayoutAverageParams.Controls.Add(this.MsxBottomiumStates, 1, 4);
			this.LayoutAverageParams.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutAverageParams.Location = new System.Drawing.Point(10, 34);
			this.LayoutAverageParams.Name = "LayoutAverageParams";
			this.LayoutAverageParams.RowCount = 6;
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.LayoutAverageParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutAverageParams.Size = new System.Drawing.Size(439, 241);
			this.LayoutAverageParams.TabIndex = 0;
			// 
			// TbxQGPFormationTemperature
			// 
			this.TbxQGPFormationTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxQGPFormationTemperature.Location = new System.Drawing.Point(244, 228);
			this.TbxQGPFormationTemperature.Name = "TbxQGPFormationTemperature";
			this.TbxQGPFormationTemperature.Size = new System.Drawing.Size(192, 31);
			this.TbxQGPFormationTemperature.TabIndex = 0;
			// 
			// LblQGPFormationTemperature
			// 
			this.LblQGPFormationTemperature.AutoSize = true;
			this.LblQGPFormationTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblQGPFormationTemperature.Location = new System.Drawing.Point(3, 225);
			this.LblQGPFormationTemperature.Name = "LblQGPFormationTemperature";
			this.LblQGPFormationTemperature.Size = new System.Drawing.Size(235, 25);
			this.LblQGPFormationTemperature.TabIndex = 0;
			this.LblQGPFormationTemperature.Text = "QGPFormationTemperature (MeV)";
			this.LblQGPFormationTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.LblMediumVelocities.Text = "MediumVelocities (c)";
			this.LblMediumVelocities.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbxMediumVelocities
			// 
			this.TbxMediumVelocities.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxMediumVelocities.Location = new System.Drawing.Point(244, 28);
			this.TbxMediumVelocities.Name = "TbxMediumVelocities";
			this.TbxMediumVelocities.Size = new System.Drawing.Size(192, 31);
			this.TbxMediumVelocities.TabIndex = 0;
			// 
			// LblDecayWidthType
			// 
			this.LblDecayWidthType.AutoSize = true;
			this.LblDecayWidthType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblDecayWidthType.Location = new System.Drawing.Point(3, 50);
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
			this.CbxDecayWidthType.Location = new System.Drawing.Point(244, 53);
			this.CbxDecayWidthType.Name = "CbxDecayWidthType";
			this.CbxDecayWidthType.Size = new System.Drawing.Size(192, 33);
			this.CbxDecayWidthType.TabIndex = 0;
			// 
			// LblPotentialTypes
			// 
			this.LblPotentialTypes.AutoSize = true;
			this.LblPotentialTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblPotentialTypes.Location = new System.Drawing.Point(3, 75);
			this.LblPotentialTypes.Name = "LblPotentialTypes";
			this.LblPotentialTypes.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.LblPotentialTypes.Size = new System.Drawing.Size(235, 75);
			this.LblPotentialTypes.TabIndex = 0;
			this.LblPotentialTypes.Text = "PotentialTypes";
			// 
			// MsxPotentialTypes
			// 
			this.MsxPotentialTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxPotentialTypes.Location = new System.Drawing.Point(244, 79);
			this.MsxPotentialTypes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxPotentialTypes.Name = "MsxPotentialTypes";
			this.MsxPotentialTypes.SelectionString = "";
			this.MsxPotentialTypes.Size = new System.Drawing.Size(192, 68);
			this.MsxPotentialTypes.TabIndex = 0;
			// 
			// LblBottomiumStates
			// 
			this.LblBottomiumStates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblBottomiumStates.Location = new System.Drawing.Point(3, 150);
			this.LblBottomiumStates.Name = "LblBottomiumStates";
			this.LblBottomiumStates.Size = new System.Drawing.Size(235, 75);
			this.LblBottomiumStates.TabIndex = 0;
			this.LblBottomiumStates.Text = "BottomiumStates";
			// 
			// MsxBottomiumStates
			// 
			this.MsxBottomiumStates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MsxBottomiumStates.Location = new System.Drawing.Point(244, 154);
			this.MsxBottomiumStates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.MsxBottomiumStates.Name = "MsxBottomiumStates";
			this.MsxBottomiumStates.SelectionString = "";
			this.MsxBottomiumStates.Size = new System.Drawing.Size(192, 68);
			this.MsxBottomiumStates.TabIndex = 0;
			// 
			// GbxOutput
			// 
			this.GbxOutput.Controls.Add(this.LayoutOutput);
			this.GbxOutput.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxOutput.Location = new System.Drawing.Point(10, 600);
			this.GbxOutput.Margin = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Name = "GbxOutput";
			this.GbxOutput.Padding = new System.Windows.Forms.Padding(10);
			this.GbxOutput.Size = new System.Drawing.Size(459, 60);
			this.GbxOutput.TabIndex = 0;
			this.GbxOutput.TabStop = false;
			this.GbxOutput.Text = "Output";
			// 
			// LayoutOutput
			// 
			this.LayoutOutput.ColumnCount = 2;
			this.LayoutOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.LayoutOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.LayoutOutput.Controls.Add(this.LblDataFileName, 0, 0);
			this.LayoutOutput.Controls.Add(this.TbxDataFileName, 1, 0);
			this.LayoutOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutOutput.Location = new System.Drawing.Point(10, 34);
			this.LayoutOutput.Name = "LayoutOutput";
			this.LayoutOutput.RowCount = 1;
			this.LayoutOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.LayoutOutput.Size = new System.Drawing.Size(439, 16);
			this.LayoutOutput.TabIndex = 0;
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
			this.GbxElectromagneticEnergy.ResumeLayout(false);
			this.LayoutElectromagneticEnergy.ResumeLayout(false);
			this.LayoutElectromagneticEnergy.PerformLayout();
			this.GbxDopplerShift.ResumeLayout(false);
			this.LayoutDopplerShift.ResumeLayout(false);
			this.LayoutDopplerShift.PerformLayout();
			this.GbxGeneralParams.ResumeLayout(false);
			this.LayoutAverageParams.ResumeLayout(false);
			this.LayoutAverageParams.PerformLayout();
			this.GbxOutput.ResumeLayout(false);
			this.LayoutOutput.ResumeLayout(false);
			this.LayoutOutput.PerformLayout();
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
		private System.Windows.Forms.GroupBox GbxGeneralParams;
		private System.Windows.Forms.TableLayoutPanel LayoutAverageParams;
		private System.Windows.Forms.Label LblMediumVelocities;
		private System.Windows.Forms.TextBox TbxMediumVelocities;
		private System.Windows.Forms.Label LblBottomiumStates;
		private Yburn.UI.MultiSelectBox MsxBottomiumStates;
		private System.Windows.Forms.Label LblPotentialTypes;
		private Yburn.UI.MultiSelectBox MsxPotentialTypes;
		private System.Windows.Forms.Label LblDecayWidthType;
		private System.Windows.Forms.ComboBox CbxDecayWidthType;
		private System.Windows.Forms.Label LblMediumTemperatures;
		private System.Windows.Forms.TextBox TbxMediumTemperatures;
		private System.Windows.Forms.GroupBox GbxOutput;
		private System.Windows.Forms.TableLayoutPanel LayoutOutput;
		private System.Windows.Forms.Label LblDataFileName;
		private System.Windows.Forms.TextBox TbxDataFileName;
		private System.Windows.Forms.TextBox TbxQGPFormationTemperature;
		private System.Windows.Forms.Label LblQGPFormationTemperature;
		private System.Windows.Forms.GroupBox GbxElectromagneticEnergy;
		private System.Windows.Forms.TableLayoutPanel LayoutElectromagneticEnergy;
		private System.Windows.Forms.GroupBox GbxDopplerShift;
		private System.Windows.Forms.TableLayoutPanel LayoutDopplerShift;
		private System.Windows.Forms.TextBox TbxNumberAveragingAngles;
		private System.Windows.Forms.Label LblNumberAveragingAngles;
		private Yburn.UI.MultiSelectBox MsxDopplerShiftEvaluationTypes;
		private System.Windows.Forms.Label LblDopplerShiftEvaluationTypes;
		private System.Windows.Forms.Label LblMagneticFieldStrength;
		private System.Windows.Forms.Label LblElectricFieldStrength;
		private System.Windows.Forms.Label LblElectricDipoleAlignment;
		private System.Windows.Forms.TextBox TbxMagneticFieldStrength;
		private System.Windows.Forms.TextBox TbxElectricFieldStrength;
		private System.Windows.Forms.ComboBox CbxElectricDipoleAlignment;
	}
}
