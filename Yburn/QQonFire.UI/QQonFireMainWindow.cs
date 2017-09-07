using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yburn.Interfaces;
using Yburn.UI;

namespace Yburn.QQonFire.UI
{
	public partial class QQonFireMainWindow : Form
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQonFireMainWindow(
			string title,
			JobOrganizer jobOrganizer
			)
			: base()
		{
			InitializeComponent();

			SetToolTipMaker();
			SetJobOrganizer(jobOrganizer);

			Initialize(jobOrganizer);
			InitializeControls(title);

			UpdateControls();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void ShowErrorDialog(
			Exception exception
			)
		{
			MessageBox.Show(exception.ToString(), exception.GetType().Name,
				MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private static void WaitForOtherProcess()
		{
			Thread.Sleep(50);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private JobOrganizer JobOrganizer;

		private bool AskBeforeClose = true;

		private Task LogThread;

		private ToolTipMaker ToolTipMaker;

		private RichTextBox TextBoxLog;

		private StatusTrackingCtrl StatusTrackingCtrl;

		private delegate void GuiUpdateCallback();

		protected override void OnClosing(
			CancelEventArgs args
			)
		{
			if(AskBeforeClose &&
				MessageBox.Show("Do you really want to close the application?",
				"Close the application?", MessageBoxButtons.YesNo) == DialogResult.No)
			{
				args.Cancel = true;
			}

			base.OnClosing(args);
		}

		private void InitializeControls(
			string title
			)
		{
			this.Text = title;

			MenuStrip.Items.Add(QQonFirePanel.MenuEntry);
		}

		private void SetToolTipMaker()
		{
			ToolTip prototype = new ToolTip
			{
				AutoPopDelay = 15000,
				InitialDelay = 500,
				ReshowDelay = 500,
				ShowAlways = true
			};
			ToolTipMaker = new ToolTipMaker(prototype);
		}

		private void SetJobOrganizer(
			JobOrganizer jobOrganizer
			)
		{
			JobOrganizer = jobOrganizer;
			JobOrganizer.JobStart += OnJobStart;
			JobOrganizer.JobFinished += OnJobFinished;
			JobOrganizer.JobFailure += OnJobFailure;
		}

		private void Initialize(
			JobOrganizer jobOrganizer
			)
		{
			QQonFirePanel.Initialize(jobOrganizer, ToolTipMaker);
			TextBoxLog = QQonFirePanel.TextBoxLog;
			StatusTrackingCtrl = QQonFirePanel.StatusTrackingCtrl;

			SetYburnConfigDataBox();
		}

		private void SetYburnConfigDataBox()
		{
			YburnConfigDataBox.OutputPath = YburnConfigFile.OutputPath;
			YburnConfigDataBox.BtnOutputPath.Click += new EventHandler(MenuItemSelectOutputPath_Click);
			YburnConfigDataBox.QQDataPathFile = YburnConfigFile.QQDataPathFile;
			YburnConfigDataBox.BtnQQDataFile.Click += new EventHandler(MenuItemSelectQQDataFile_Click);
		}

		private void WriteLogMessages()
		{
			WaitForOtherProcess();

			int logHeaderBegin = WriteLogHeaderAndReturnBeginPosition();
			UpdateScreenWithTaskStatus();
			UpdateScreenWithResults(logHeaderBegin);
		}

		private int LogTextLastPosition
		{
			get
			{
				return TextBoxLog.Text.Length;
			}
		}

		private void LogTextAppend(
			string text
			)
		{
			TextBoxLog.AppendText(text);
		}

		private void LogTextReplace(
			int startPosition,
			int endPosition,
			string textReplace
			)
		{
			if(startPosition > endPosition)
			{
				throw new Exception("Start position has to be larger than end position.");
			}

			TextBoxLog.Select(startPosition, endPosition - startPosition);
			TextBoxLog.SelectedText = textReplace;
		}

		private void LogTextScrollDown()
		{
			TextBoxLog.ScrollToCaret();
		}

		private void UpdateStatus()
		{
			if(StatusTrackingCtrl != null)
			{
				StatusTrackingCtrl.UpdateStatus(JobOrganizer.StatusTitles, JobOrganizer.StatusValues);
			}
		}

		private void MenuItemOpenReadMe_Click(object sender, EventArgs e)
		{
			JobOrganizer.OpenReadMe();
		}

		private void MenuItemLoadParaFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				JobOrganizer.ProcessParameterFile(dialog.FileName);
			}
		}

		private void MenuItemQuit_Click(object sender, EventArgs e)
		{
			AskBeforeClose = false;
			Close();
		}

		private void MenuItemAbortProcess_Click(object sender, EventArgs e)
		{
			JobOrganizer.AbortRunningJob();
		}

		private void MenuItemClearScreen_Click(object sender, EventArgs e)
		{
			TextBoxLog.Text = string.Empty;
			StatusTrackingCtrl.Clear();
		}

		private void MenuItemSelectOutputPath_Click(object sender, EventArgs e)
		{
			YburnConfigFile.OutputPath = YburnConfigDataBox.SelectOutputPath();
		}

		private void MenuItemSelectQQDataFile_Click(object sender, EventArgs e)
		{
			YburnConfigFile.QQDataPathFile = YburnConfigDataBox.SelectQQDataFile();
		}

		private void OnJobStart(
			object sender,
			JobStartEventArgs args
			)
		{
			LogThread = Task.Factory.StartNew(WriteLogMessages, TaskCreationOptions.LongRunning);
			LogThread.ContinueWith(OnJobFailure, TaskContinuationOptions.OnlyOnFaulted);
		}

		private void OnJobFinished(
			object sender,
			JobFinishedEventArgs args
			)
		{
			Invoke(new GuiUpdateCallback(UpdateControls));
		}

		private void OnJobFailure(
			Task task
			)
		{
			if(task.Exception != null)
			{
				JobFailureEventArgs args = new JobFailureEventArgs
				{
					Exception = task.Exception.InnerException
				};
				OnJobFailure(this, args);
			}
		}

		private void OnJobFailure(
			object sender,
			JobFailureEventArgs args
			)
		{
			ShowErrorDialog(args.Exception);
		}

		private void MenuItemLoadBatchFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				JobOrganizer.ProcessBatchFile(dialog.FileName, QQonFirePanel.ControlsValues);
			}
		}

		private int WriteLogHeaderAndReturnBeginPosition()
		{
			int logHeaderBegin = 0;
			Invoke(new GuiUpdateCallback(() =>
			{
				logHeaderBegin = LogTextLastPosition;
				LogTextAppend(JobOrganizer.LogMessage);
				LogTextScrollDown();
			}));
			return logHeaderBegin;
		}

		private void UpdateScreenWithTaskStatus()
		{
			if(StatusTrackingCtrl != null)
			{
				while(JobOrganizer.IsJobRunning)
				{
					Invoke(new GuiUpdateCallback(UpdateStatus));
				}
			}
		}

		private void UpdateScreenWithResults(
			int logHeaderBegin
			)
		{
			Invoke(new GuiUpdateCallback(() =>
			{
				LogTextReplace(logHeaderBegin, LogTextLastPosition, JobOrganizer.LogMessage);
				LogTextScrollDown();
			}));
		}

		private void UpdateControls()
		{
			QQonFirePanel.ControlsValues = JobOrganizer.GetDataFromWorker();
		}

		private void MenutItemSaveValuesAsParameterFile_Click(object sender, EventArgs e)
		{
			using(SaveFileDialog dialog = SaveAsParaFileDialog.Create())
			{
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					JobOrganizer.SaveAsParameterFile(dialog.FileName, QQonFirePanel.ControlsValues);
				}
			}
		}
	}
}
