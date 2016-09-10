using System.Windows.Forms;

namespace Yburn.UI
{
	public partial class YburnConfigDataBox : UserControl
	{
		public YburnConfigDataBox()
		{
			InitializeComponent();
		}

		public string QQDataPathFile
		{
			get
			{
				return TbxQQDataFile.Text;
			}
			set
			{
				TbxQQDataFile.Text = value;
			}
		}

		public string OutputPath
		{
			get
			{
				return TbxOutputPath.Text;
			}
			set
			{
				TbxOutputPath.Text = value;
			}
		}

		public string SelectQQDataFile()
		{
			using(SaveFileDialog dialog = SelectArchiveDataFileDialog.Create())
			{
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					QQDataPathFile = dialog.FileName;
				}
			}

			return QQDataPathFile;
		}

		public string SelectOutputPath()
		{
			using(FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Select an output folder...";
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					OutputPath = dialog.SelectedPath + "\\";
				}
			}

			return OutputPath;
		}
	}
}