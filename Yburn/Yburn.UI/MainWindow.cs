using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yburn.Interfaces;
using Yburn.Util;

namespace Yburn.UI
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public partial class MainWindow : Form
	{
		/********************************************************************************************
		* Public static members, functions and properties
		********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public MainWindow(
			string title,
			JobOrganizer jobOrganizer
			)
			: base()
		{
			InitializeComponent();

			SetToolTipMaker();
			SetJobOrganizer(jobOrganizer);

			InitializePanels(jobOrganizer);
			InitializeControls(title);

			UpdateControls();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

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

		private Dictionary<string, string>[] AllControlsValues
		{
			get
			{
				Dictionary<string, string>[] nameValuePairs = new Dictionary<string, string>[]
				{
					SingleQQPanel.ControlsValues,
					InMediumDecayWidthPanel.ControlsValues,
					QQonFirePanel.ControlsValues
				};

				return nameValuePairs;
			}
		}

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

			TextBoxLog = SingleQQPanel.TextBoxLog;
			StatusTrackingCtrl = SingleQQPanel.StatusTrackingCtrl;
			MenuStrip.Items.Add(SingleQQPanel.MenuEntry);
			MenuStrip.Items.Add(InMediumDecayWidthPanel.MenuEntry);
			MenuStrip.Items.Add(QQonFirePanel.MenuEntry);
		}

		private void SetToolTipMaker()
		{
			ToolTip prototype = new ToolTip();
			prototype.AutoPopDelay = 15000;
			prototype.InitialDelay = 500;
			prototype.ReshowDelay = 500;
			prototype.ShowAlways = true;
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

		private void InitializePanels(
			JobOrganizer jobOrganizer
			)
		{
			SingleQQPanel.Initialize(jobOrganizer, ToolTipMaker);
			InMediumDecayWidthPanel.Initialize(jobOrganizer, ToolTipMaker);
			QQonFirePanel.Initialize(jobOrganizer, ToolTipMaker);
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

		private void SingleQQPanel_Enter(object sender, EventArgs e)
		{
			TextBoxLog = SingleQQPanel.TextBoxLog;
			StatusTrackingCtrl = SingleQQPanel.StatusTrackingCtrl;
		}

		private void InMediumDecayWidthPanel_Enter(object sender, EventArgs e)
		{
			TextBoxLog = InMediumDecayWidthPanel.TextBoxLog;
			StatusTrackingCtrl = InMediumDecayWidthPanel.StatusTrackingCtrl;
		}

		private void QQonFirePanel_Enter(object sender, EventArgs e)
		{
			TextBoxLog = QQonFirePanel.TextBoxLog;
			StatusTrackingCtrl = QQonFirePanel.StatusTrackingCtrl;
		}

		private void MenuItemSetOutputPath_Click(object sender, EventArgs e)
		{
			using(FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Select an output folder...";
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					YburnConfigFile.OutputPath = dialog.SelectedPath + "\\";
				}
			}
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
				JobFailureEventArgs args = new JobFailureEventArgs();
				args.Exception = task.Exception.InnerException;
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
				JobOrganizer.ProcessBatchFile(dialog.FileName, AllControlsValues);
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
			SingleQQPanel.ControlsValues = JobOrganizer.GetDataFromWorkers("SingleQQ");
			InMediumDecayWidthPanel.ControlsValues = JobOrganizer.GetDataFromWorkers("InMediumDecayWidth");
			QQonFirePanel.ControlsValues = JobOrganizer.GetDataFromWorkers("QQonFire");
		}

		private void QQonFirePanel_Load(object sender, EventArgs e)
		{
		}
	}
}