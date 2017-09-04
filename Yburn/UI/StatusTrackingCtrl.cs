using System;
using System.Windows.Forms;

namespace Yburn.UI
{
	public partial class StatusTrackingCtrl : UserControl
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public StatusTrackingCtrl()
		{
			InitializeComponent();
			SetLabelsAndTextBoxes();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void UpdateStatus(
			string[] statusTitles,
			string[] statusValues
			)
		{
			if(statusTitles != null && statusValues != null)
			{
				AssertInputValid(statusTitles.Length, statusValues.Length);

				UpdateTitles(statusTitles);
				UpdateValues(statusValues);
			}
		}

		public void Clear()
		{
			ClearLabels();
			ClearTextBoxes();
		}

		/********************************************************************************************
 		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static int MaxNumberControls = 7;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private Label[] Labels;

		private TextBox[] TextBoxes;

		private void SetLabelsAndTextBoxes()
		{
			Labels = new Label[]
			{
				Label1,
				Label2,
				Label3,
				Label4,
				Label5,
				Label6,
				Label7,
				Label8
			};

			TextBoxes = new TextBox[]
			{
				TextBox1,
				TextBox2,
				TextBox3,
				TextBox4,
				TextBox5,
				TextBox6,
				TextBox7,
				TextBox8
			};
		}

		private void ClearTextBoxes()
		{
			foreach(TextBox textBox in TextBoxes)
			{
				textBox.Text = string.Empty;
				textBox.Visible = false;
			}
		}

		private void ClearLabels()
		{
			foreach(Label label in Labels)
			{
				label.Text = string.Empty;
			}
		}

		private static void AssertInputValid(
			int numberStatusTitles,
			int numberStatusValues
			)
		{
			if(numberStatusTitles != numberStatusValues)
			{
				throw new Exception("Number of titles and values does not match.");
			}

			if(numberStatusValues > MaxNumberControls)
			{
				throw new Exception("Number of status variables exceeds iMaxIndex.");
			}
		}

		private void UpdateTitles(
			string[] statusTitles
			)
		{
			for(int i = 0; i < statusTitles.Length; i++)
			{
				Labels[i].Text = statusTitles[i];
			}

			for(int i = statusTitles.Length; i < MaxNumberControls; i++)
			{
				Labels[i].Text = string.Empty;
			}
		}

		private void UpdateValues(
			string[] statusValues
			)
		{
			for(int i = 0; i < statusValues.Length; i++)
			{
				TextBoxes[i].Text = statusValues[i];
				TextBoxes[i].Visible = true;
			}

			for(int i = statusValues.Length; i < MaxNumberControls; i++)
			{
				TextBoxes[i].Text = string.Empty;
				TextBoxes[i].Visible = false;
			}
		}
	}
}
