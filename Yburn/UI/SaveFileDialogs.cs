using System.Windows.Forms;

namespace Yburn.UI
{
	public static class SelectArchiveDataFileDialog
	{
		public static SaveFileDialog Create()
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Title = "Select an archive data file...",
				Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
				SupportMultiDottedExtensions = true,
				DefaultExt = "txt",
				AddExtension = true,
				OverwritePrompt = false
			};

			return dialog;
		}
	}

	public static class SaveAsParaFileDialog
	{
		public static SaveFileDialog Create()
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Title = "Save current values as parameter file...",
				Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
				SupportMultiDottedExtensions = true,
				DefaultExt = "txt",
				AddExtension = true,
				OverwritePrompt = true
			};

			return dialog;
		}
	}
}
