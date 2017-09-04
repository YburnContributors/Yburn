using System;
using System.Text;
using System.Windows.Forms;

namespace Yburn.UI
{
	public partial class MultiSelectBox : UserControl
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public MultiSelectBox()
		{
			InitializeComponent();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void AddItems(
			object[] items
			)
		{
			CheckedListBox.Items.AddRange(items);
		}

		public string SelectionString
		{
			get
			{
				return MakeSelectionString();
			}

			set
			{
				SetSelectionFromString(value);
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string[] SelectionStringToArray(
			string selection
			)
		{
			return selection.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private string MakeSelectionString()
		{
			StringBuilder selection = new StringBuilder();
			foreach(string checkedItem in CheckedListBox.CheckedItems)
			{
				selection.AppendFormat("{0},", checkedItem);
			}
			RemoveLastComma(selection);

			return selection.ToString();
		}

		private void RemoveLastComma(
			StringBuilder selection
			)
		{
			if(selection.Length > 0)
			{
				selection.Remove(selection.Length - 1, 1);
			}
		}

		private void SetSelectionFromString(
			string selection
			)
		{
			string[] selectedValues = SelectionStringToArray(selection);

			for(int i = 0; i < CheckedListBox.Items.Count; i++)
			{
				CheckedListBox.SetItemChecked(i,
					IsItemInSelection(selectedValues, i));
			}
		}

		private bool IsItemInSelection(
			string[] selectedValues,
			int itemIndex
			)
		{
			foreach(string entry in selectedValues)
			{
				if(entry == CheckedListBox.Items[itemIndex].ToString())
				{
					return true;
				}
			}

			return false;
		}
	}
}
