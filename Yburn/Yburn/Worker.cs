using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Yburn.Interfaces;

namespace Yburn
{
	public abstract partial class Worker
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Worker()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public string NameVersion
		{
			get;
			set;
		}

		public StringBuilder LogMessages
		{
			get;
			set;
		}

		public void StartJob(
			string jobId,
			Dictionary<string, string> nameValuePairs
			)
		{
			VariableNameValuePairs = nameValuePairs;
			LogMessages.Clear();

			OnJobStart();
			StartJob(jobId);
			OnJobFinished();
		}

		public string[] StatusTitles
		{
			get;
			private set;
		}

		public string[] StatusValues
		{
			get;
			protected set;
		}

		public event JobStartEventHandler JobStart;

		public event JobFinishedEventHandler JobFinished;

		public string[] GetWorkerEnumEntries(
			string enumName
			)
		{
			return Enum.GetNames(GetEnumTypeByName(enumName));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		protected static string QQDataPathFile
		{
			get
			{
				string pathFile = YburnConfigFile.QQDataPathFile;
				if(string.IsNullOrEmpty(pathFile))
				{
					throw new Exception("Invalid QQ-data file.");
				}
				return pathFile;
			}
		}

		private static void AppendVariableNameValuePairs(
			 StringBuilder stringBuilder,
			 Dictionary<string, string> nameValuePairs
			)
		{
			foreach(KeyValuePair<string, string> nameValuePair in nameValuePairs)
			{
				AppendLogHeaderLine(stringBuilder, nameValuePair.Key, nameValuePair.Value);
			}
		}

		private static void AppendLogHeaderLine(
		 StringBuilder stringBuilder,
		 string name,
		 string value
		 )
		{
			stringBuilder.AppendLine(string.Format("#{0,35}    {1}", name, value));
		}

		/********************************************************************************************
 		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		public CancellationToken JobCancelToken;

		protected abstract void StartJob(
		 string jobId
		 );

		private void OnJobStart()
		{
			if(JobStart != null)
			{
				JobStart(this, new JobStartEventArgs());
			}
		}

		private void OnJobFinished()
		{
			if(JobFinished != null)
			{
				JobFinished(this, new JobFinishedEventArgs());
			}
		}

		protected abstract Type GetEnumTypeByName(
			string enumName
			);

		protected abstract Dictionary<string, string> GetVariableNameValuePairs();

		protected abstract void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			);

		protected void PrepareJob(
			string jobTitle
			)
		{
			CurrentJobTitle = jobTitle;

			SetJobStartTimeStamp();

			LogMessages.Clear();
			LogMessages.Append(LogHeader);
		}

		protected void PrepareJob(
			string jobTitle,
			string[] statusTitles
			)
		{
			CurrentJobTitle = jobTitle;

			SetStatusVariables(statusTitles);
			SetJobStartTimeStamp();

			LogMessages.Clear();
			LogMessages.Append(LogHeader);
		}

		protected void SetStatusVariables(
			 string[] statusTitles
			 )
		{
			StatusTitles = statusTitles;
			StatusValues = new string[StatusTitles.Length];
		}

		protected string CurrentJobTitle;

		private void SetJobStartTimeStamp()
		{
			JobStartTimeStamp = DateTime.Now;
			JobStartTimeStampString = JobStartTimeStamp.ToString("s");
		}

		private DateTime JobStartTimeStamp;

		private string JobStartTimeStampString;

		protected string LogHeader
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				AppendLogHeaderLine(stringBuilder, "Name and Version", NameVersion);
				AppendLogHeaderLine(stringBuilder, "Job specifier", CurrentJobTitle);
				AppendLogHeaderLine(stringBuilder, "Job started at", JobStartTimeStampString);
				AppendVariableNameValuePairs(stringBuilder, VariableNameValuePairs);

				return stringBuilder.ToString();
			}
		}

		protected string LogFooter
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();

				if(JobCancelToken.IsCancellationRequested)
				{
					stringBuilder.AppendLine("#");
					stringBuilder.AppendLine("#");
					stringBuilder.Append("#Job aborted after ");
				}
				else
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					stringBuilder.Append("#Job completed after ");
				}

				stringBuilder.Append(GetTimePassedString(DateTime.Now.Subtract(JobStartTimeStamp)));
				stringBuilder.AppendLine(".");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();

				return stringBuilder.ToString();
			}
		}

		private string GetTimePassedString(
			TimeSpan timePassed
			)
		{
			if(timePassed.Days > 0)
			{
				return string.Format("{0}d{1:00}h{2:00}m{3:00}s",
					timePassed.Days, timePassed.Hours, timePassed.Minutes, timePassed.Seconds);
			}
			else if(timePassed.Hours > 0)
			{
				return string.Format("{0}h{1:00}m{2:00}s",
					timePassed.Hours, timePassed.Minutes, timePassed.Seconds);
			}
			else if(timePassed.Minutes > 0)
			{
				return string.Format("{0}m{1:00}s",
					timePassed.Minutes, timePassed.Seconds);
			}
			else
			{
				return string.Format("{0}s",
					timePassed.Seconds);
			}
		}
	}

	[Serializable]
	public class InvalidJobException : Exception
	{
		public InvalidJobException(
			string jobId
			)
			: base("Invalid JobId:" + jobId + ".")
		{
		}
	}
}
