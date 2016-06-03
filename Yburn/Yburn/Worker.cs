using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Yburn.Interfaces;
using Yburn.Util;

namespace Yburn
{
	public abstract class Worker
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

		public Dictionary<string, string> VariableNameValuePairs
		{
			get
			{
				return GetVariableNameValuePairs();
			}
			set
			{
				SetVariableNameValuePairs(value ?? new Dictionary<string, string>());
			}
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

		protected static string GetQQDataPathFile()
		{
			string pathFile = YburnConfigFile.QQDataPathFile;
			if(string.IsNullOrEmpty(pathFile))
			{
				throw new Exception("Invalid QQ-data file.");
			}
			return pathFile;
		}

		protected static void AppendLogHeaderLines(
			 StringBuilder stringBuilder,
			 Dictionary<string, string> nameValuePairs
			)
		{
			foreach(KeyValuePair<string, string> nameValuePair in nameValuePairs)
			{
				AppendLogHeaderLine(stringBuilder, nameValuePair.Key, nameValuePair.Value);
			}
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 bool value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToString());
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 double value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToString("G4"));
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 double[] value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToStringifiedList());
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 double[][] value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToStringifiedList());
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 Enum value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToString());
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 int value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToString());
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 int[] value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToStringifiedList());
		}

		protected static void AppendLogHeaderLine(
			 StringBuilder stringBuilder,
			 string name,
			 int[][] value
			 )
		{
			AppendLogHeaderLine(stringBuilder, name, value.ToStringifiedList());
		}

		protected static void AppendLogHeaderLine(
		 StringBuilder stringBuilder,
		 string name,
		 string value
		 )
		{
			stringBuilder.AppendLine(string.Format("#{0,35}    {1}", name, value));
		}

		protected static void AppendLogHeaderLine(
		 StringBuilder stringBuilder,
		 string name,
		 string[] value
		 )
		{
			stringBuilder.AppendLine(string.Format("#{0,35}    {1}", name, value.ToStringifiedList()));
		}

		/********************************************************************************************
 		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected CancellationToken JobCancelToken;

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
			JobStartTimeStampString = JobStartTimeStamp.ToString("yyyy-MM-dd-HH-mm-ss");
		}

		private DateTime JobStartTimeStamp;

		private string JobStartTimeStampString;

		protected virtual string LogHeader
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				AppendLogHeaderLine(stringBuilder, "Name and Version", NameVersion);
				AppendLogHeaderLine(stringBuilder, "Job specifier", CurrentJobTitle);
				AppendLogHeaderLine(stringBuilder, "Job started at", JobStartTimeStampString);

				return stringBuilder.ToString();
			}
		}

		protected string LogFooter
		{
			get
			{
				string timePassed = DateTime.Now.Subtract(JobStartTimeStamp)
					.TotalSeconds.ToString("F0");

				return JobCancelToken.IsCancellationRequested ?
					"#\r\n#\r\n#Job aborted after " + timePassed + " seconds.\r\n\r\n\r\n"
					: "\r\n\r\n#Job completed after " + timePassed + " seconds.\r\n\r\n\r\n";
			}
		}
	}

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