using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yburn.FileUtil;
using Yburn.Interfaces;

namespace Yburn
{
	public class BackgroundService : JobOrganizer
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public BackgroundService()
		{
			LogMessages = new StringBuilder();
			IsBatchProcessRunning = false;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public event JobStartEventHandler JobStart;

		public event JobFinishedEventHandler JobFinished;

		public event JobFailureEventHandler JobFailure;

		public string[] StatusTitles
		{
			get
			{
				return Worker.StatusTitles;
			}
		}

		public string[] StatusValues
		{
			get
			{
				return Worker.StatusValues;
			}
		}

		public string LogMessage
		{
			get
			{
				return LogMessages.ToString();
			}
		}

		public void SetWorker(
			Worker worker
			)
		{
			Worker = worker;
			Worker.LogMessages = LogMessages;
			Worker.JobStart += OnJobStart;
			Worker.JobFinished += OnJobFinished;
		}

		public string[] GetWorkerEnumEntries(
			string enumName
			)
		{
			return Worker.GetWorkerEnumEntries(enumName);
		}

		public void RequestNewJob(
			string jobId,
			Dictionary<string, string> parameterNameValuePairs
			)
		{
			if(IsBusy)
			{
				OnJobFailure(new DeclineWhileBusyException());
			}
			else
			{
				TryStartJob(jobId, parameterNameValuePairs);
			}
		}

		public bool IsJobRunning
		{
			get
			{
				return WorkThread != null && WorkThread.Status == TaskStatus.Running;
			}
		}

		public void AbortRunningJob()
		{
			if(CancellationTokenSource != null)
			{
				CancellationTokenSource.Cancel();
			}
			IsBatchProcessRunning = false;
		}

		public void OpenReadMe()
		{
			Process.Start("README.txt");
		}

		public void ProcessParameterFile(
			string pathFile
			)
		{
			if(IsBusy)
			{
				OnJobFailure(new DeclineWhileBusyException());
			}
			else if(!string.IsNullOrEmpty(pathFile))
			{
				TryProcessParameterFile(pathFile);
			}
		}

		public void SaveAsParameterFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			)
		{
			if(IsBusy)
			{
				OnJobFailure(new DeclineWhileBusyException());
			}
			else if(!string.IsNullOrEmpty(pathFile))
			{
				TrySaveAsParameterFile(pathFile, nameValuePairs);
			}
		}

		public void ProcessBatchFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			)
		{
			if(IsBusy)
			{
				OnJobFailure(new DeclineWhileBusyException());
			}
			else if(!string.IsNullOrEmpty(pathFile))
			{
				TryProcessBatchFile(pathFile, nameValuePairs);
			}
		}

		public Dictionary<string, string> GetDataFromWorker()
		{
			return Worker.VariableNameValuePairs;
		}

		public void TransferDataToWorker(
			Dictionary<string, string> nameValuePairs
			)
		{
			Worker.VariableNameValuePairs = nameValuePairs;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string LogPathFile
		{
			get
			{
				return YburnConfigFile.OutputPath + "log.txt";
			}
		}

		private static void ThrowIfInvalidOutputPath()
		{
			if(!string.IsNullOrEmpty(YburnConfigFile.OutputPath)
				&& !Directory.Exists(YburnConfigFile.OutputPath))
			{
				throw new InvalidOutputPathException();
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private Worker Worker;

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken JobCancelToken;

		private Task WorkThread;

		private StringBuilder LogMessages;

		private bool IsBatchProcessRunning;

		private List<Dictionary<string, string>> BatchCommandList;

		private bool IsBusy
		{
			get
			{
				return IsJobRunning || IsBatchProcessRunning;
			}
		}

		private Task Launch(
			string jobId,
			Dictionary<string, string> parameterNameValuePairs
			)
		{
			return Task.Factory.StartNew(() =>
			{
				Worker.JobCancelToken = JobCancelToken;
				Worker.StartJob(jobId, parameterNameValuePairs);
			}, TaskCreationOptions.LongRunning);
		}

		private void TryStartJob(
			string jobId,
			Dictionary<string, string> parameterNameValuePairs
			)
		{
			try
			{
				PrepareCancellationToken();
				StartJob(jobId, parameterNameValuePairs);
			}
			catch(Exception exception)
			{
				OnJobFailure(exception);
			}
		}

		private void PrepareCancellationToken()
		{
			CancellationTokenSource = new CancellationTokenSource();
			JobCancelToken = CancellationTokenSource.Token;
		}

		private void StartJob(
			string jobId,
			Dictionary<string, string> parameterNameValuePairs
			)
		{
			WorkThread = Launch(jobId, parameterNameValuePairs);
			WorkThread.ContinueWith(OnJobFailure, TaskContinuationOptions.OnlyOnFaulted);
		}

		private void OnJobStart(
			object sender,
			JobStartEventArgs args
			)
		{
			NativeMethods.KeepSystemAwake();

			if(JobStart != null)
			{
				JobStart(this, args);
			}
		}

		private void OnJobFinished(
			object sender,
			JobFinishedEventArgs args
			)
		{
			NativeMethods.AllowSleepMode();

			WriteToLogFile();

			if(JobFinished != null)
			{
				JobFinished(this, args);
			}

			if(IsBatchProcessRunning)
			{
				PerformBatchProcess();
			}
		}

		private void WriteToLogFile()
		{
			ThrowIfInvalidOutputPath();
			File.AppendAllText(LogPathFile, LogMessage);
		}

		private void OnJobFailure(
			Task task
			)
		{
			NativeMethods.AllowSleepMode();

			if(task.Exception != null)
			{
				OnJobFailure(task.Exception.InnerException);
			}
		}

		private void OnJobFailure(
			Exception exception
			)
		{
			if(JobFailure != null)
			{
				JobFailureEventArgs args = new JobFailureEventArgs();
				args.Exception = exception;
				JobFailure(this, args);
			}
		}

		private void TryProcessParameterFile(
			string pathFile
			)
		{
			try
			{
				OnJobStart(this, new JobStartEventArgs());
				StartProcessParameterFile(pathFile);
				OnJobFinished(this, new JobFinishedEventArgs());
			}
			catch(Exception exception)
			{
				OnJobFailure(exception);
			}
		}

		private void StartProcessParameterFile(
			string pathFile
			)
		{
			Dictionary<string, string> nameValuePairs;
			ParaFileReader.Read(pathFile, out nameValuePairs);
			TransferDataToWorker(nameValuePairs);

			YburnConfigFile.LastParaFile = pathFile;
		}

		private void TrySaveAsParameterFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			)
		{
			try
			{
				OnJobStart(this, new JobStartEventArgs());
				StartSaveAsParameterFile(pathFile, nameValuePairs);
				OnJobFinished(this, new JobFinishedEventArgs());
			}
			catch(Exception exception)
			{
				OnJobFailure(exception);
			}
		}

		private void StartSaveAsParameterFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			)
		{
			ParaFileWriter.Write(pathFile, nameValuePairs);

			YburnConfigFile.LastParaFile = pathFile;
		}

		private void TryProcessBatchFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			)
		{
			try
			{
				StartProcessBatchFile(pathFile, nameValuePairs);
			}
			catch(Exception exception)
			{
				OnJobFailure(exception);
			}
		}

		private void StartProcessBatchFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			)
		{
			TransferDataToWorker(nameValuePairs);
			BatchFileReader.Read(pathFile, out BatchCommandList);
			PerformBatchProcess();
		}

		private void PerformBatchProcess()
		{
			if(BatchCommandList.Count > 0)
			{
				IsBatchProcessRunning = true;

				PerformOneJob(BatchCommandList[0]);
				BatchCommandList.RemoveAt(0);
			}
			else
			{
				IsBatchProcessRunning = false;
			}
		}

		private void PerformOneJob(
			Dictionary<string, string> commands
			)
		{
			if(commands.ContainsKey("Job"))
			{
				// let external tasks depending on the previous job terminate before we continue
				WorkThread = null;
				Thread.Sleep(1000);

				TryStartJob(commands["Job"], commands);
			}
		}
	}
}
