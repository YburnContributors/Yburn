using System.Windows.Forms;

namespace Yburn.UI
{
	public static class SelectArchiveDataFileDialog
	{
		public static SaveFileDialog Create()
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Title = "Select an archive data file...";
			dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			dialog.SupportMultiDottedExtensions = true;
			dialog.DefaultExt = "txt";
			dialog.AddExtension = true;
			dialog.OverwritePrompt = false;

			return dialog;
		}
	}

	public static class SaveAsParaFileDialog
	{
		public static SaveFileDialog Create()
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Title = "Save current values as parameter file...";
			dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			dialog.SupportMultiDottedExtensions = true;
			dialog.DefaultExt = "txt";
			dialog.AddExtension = true;
			dialog.OverwritePrompt = true;

			return dialog;
		}
	}
}