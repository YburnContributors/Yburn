using System.Collections.Generic;

namespace Yburn.Interfaces
{
	public interface JobOrganizer
	{
		event JobStartEventHandler JobStart;

		event JobFinishedEventHandler JobFinished;

		event JobFailureEventHandler JobFailure;

		string[] StatusValues
		{
			get;
		}

		string[] StatusTitles
		{
			get;
		}

		string LogMessage
		{
			get;
		}

		bool IsJobRunning
		{
			get;
		}

		void OpenReadMe();

		void ProcessParameterFile(
			string pathFile
			);

		void SaveAsParameterFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			);

		void ProcessBatchFile(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			);

		void RequestNewJob(
			string jobId,
			Dictionary<string, string> nameValuePairs
			);

		void AbortRunningJob();

		Dictionary<string, string> GetDataFromWorker();

		string[] GetWorkerEnumEntries(
			string enumName
			);
	}
}
