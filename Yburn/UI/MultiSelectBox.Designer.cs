namespace Yburn.UI
{
	partial class MultiSelectBox
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
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.CheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.SuspendLayout();
			// 
			// CheckedListBox
			// 
			this.CheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CheckedListBox.FormattingEnabled = true;
			this.CheckedListBox.Location = new System.Drawing.Point(0, 0);
			this.CheckedListBox.Name = "CheckedListBox";
			this.CheckedListBox.Size = new System.Drawing.Size(150, 150);
			this.CheckedListBox.TabIndex = 0;
			// 
			// MultiSelectBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CheckedListBox);
			this.Name = "MultiSelectBox";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckedListBox CheckedListBox;
	}
}
