namespace Yburn.UI
{
   partial class YburnConfigDataBox
   {
      /// <summary> 
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary> 
      /// Verwendete Ressourcen bereinigen.
      /// </summary>
      /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
      protected override void Dispose(bool disposing)
      {
         if(disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Vom Komponenten-Designer generierter Code

      /// <summary> 
      /// Erforderliche Methode für die Designerunterstützung. 
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent()
      {
			this.GbxYburnConfig = new System.Windows.Forms.GroupBox();
			this.LayoutBottom = new System.Windows.Forms.TableLayoutPanel();
			this.BtnQQDataFile = new System.Windows.Forms.Button();
			this.BtnOutputPath = new System.Windows.Forms.Button();
			this.TbxQQDataFile = new System.Windows.Forms.TextBox();
			this.TbxOutputPath = new System.Windows.Forms.TextBox();
			this.GbxYburnConfig.SuspendLayout();
			this.LayoutBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// GbxYburnConfig
			// 
			this.GbxYburnConfig.Controls.Add(this.LayoutBottom);
			this.GbxYburnConfig.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GbxYburnConfig.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GbxYburnConfig.Location = new System.Drawing.Point(0, 0);
			this.GbxYburnConfig.Margin = new System.Windows.Forms.Padding(10);
			this.GbxYburnConfig.Name = "GbxYburnConfig";
			this.GbxYburnConfig.Padding = new System.Windows.Forms.Padding(10);
			this.GbxYburnConfig.Size = new System.Drawing.Size(600, 100);
			this.GbxYburnConfig.TabIndex = 2;
			this.GbxYburnConfig.TabStop = false;
			this.GbxYburnConfig.Text = "Configuration Data";
			// 
			// LayoutBottom
			// 
			this.LayoutBottom.ColumnCount = 2;
			this.LayoutBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.LayoutBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.LayoutBottom.Controls.Add(this.BtnQQDataFile, 0, 0);
			this.LayoutBottom.Controls.Add(this.BtnOutputPath, 0, 1);
			this.LayoutBottom.Controls.Add(this.TbxQQDataFile, 1, 0);
			this.LayoutBottom.Controls.Add(this.TbxOutputPath, 1, 1);
			this.LayoutBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayoutBottom.Location = new System.Drawing.Point(10, 25);
			this.LayoutBottom.Name = "LayoutBottom";
			this.LayoutBottom.RowCount = 3;
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.LayoutBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LayoutBottom.Size = new System.Drawing.Size(580, 65);
			this.LayoutBottom.TabIndex = 0;
			// 
			// BtnQQDataFile
			// 
			this.BtnQQDataFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnQQDataFile.Location = new System.Drawing.Point(3, 3);
			this.BtnQQDataFile.Name = "BtnQQDataFile";
			this.BtnQQDataFile.Size = new System.Drawing.Size(139, 24);
			this.BtnQQDataFile.TabIndex = 0;
			this.BtnQQDataFile.Text = "Select QQDataFile";
			this.BtnQQDataFile.UseVisualStyleBackColor = true;
			// 
			// BtnOutputPath
			// 
			this.BtnOutputPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnOutputPath.Location = new System.Drawing.Point(3, 33);
			this.BtnOutputPath.Name = "BtnOutputPath";
			this.BtnOutputPath.Size = new System.Drawing.Size(139, 24);
			this.BtnOutputPath.TabIndex = 2;
			this.BtnOutputPath.Text = "Set OutputPath";
			this.BtnOutputPath.UseVisualStyleBackColor = true;
			// 
			// TbxQQDataFile
			// 
			this.TbxQQDataFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxQQDataFile.Location = new System.Drawing.Point(148, 3);
			this.TbxQQDataFile.Name = "TbxQQDataFile";
			this.TbxQQDataFile.ReadOnly = true;
			this.TbxQQDataFile.Size = new System.Drawing.Size(429, 22);
			this.TbxQQDataFile.TabIndex = 1;
			// 
			// TbxOutputPath
			// 
			this.TbxOutputPath.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TbxOutputPath.Location = new System.Drawing.Point(148, 33);
			this.TbxOutputPath.Name = "TbxOutputPath";
			this.TbxOutputPath.ReadOnly = true;
			this.TbxOutputPath.Size = new System.Drawing.Size(429, 22);
			this.TbxOutputPath.TabIndex = 3;
			// 
			// YburnConfigDataBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.GbxYburnConfig);
			this.Name = "YburnConfigDataBox";
			this.Size = new System.Drawing.Size(600, 100);
			this.GbxYburnConfig.ResumeLayout(false);
			this.LayoutBottom.ResumeLayout(false);
			this.LayoutBottom.PerformLayout();
			this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.GroupBox GbxYburnConfig;
      private System.Windows.Forms.TableLayoutPanel LayoutBottom;
      private System.Windows.Forms.TextBox TbxQQDataFile;
      private System.Windows.Forms.TextBox TbxOutputPath;
		public System.Windows.Forms.Button BtnQQDataFile;
		public System.Windows.Forms.Button BtnOutputPath;
	}
}
