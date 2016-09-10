using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Yburn.Interfaces;

namespace Yburn.Tests
{
	[TestClass]
	public class BatchProcessingTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void ProcessBatchFile()
		{
			NumberJobsFinished = 0;

			BackgroundService = CreateDummyBackgroundService();
			BackgroundService.ProcessBatchFile(DummyWorker.CreateDummyBatchFile(),
				EmptyDictionary);

			while(NumberJobsFinished != 3)
			{
				Thread.Sleep(10);
			}

			AssertNoExceptionThrown();
		}

		[TestMethod]
		public void NewBatchBeforePreviousIsFinished()
		{
			NumberJobsFinished = 0;

			BackgroundService = CreateDummyBackgroundService();
			BackgroundService.ProcessBatchFile(DummyWorker.CreateDummyBatchFile(),
				EmptyDictionary);

			Thread.Sleep(10);

			BackgroundService.ProcessBatchFile(DummyWorker.CreateDummyBatchFile(),
				EmptyDictionary);

			while(NumberJobsFinished < 2)
			{
				Thread.Sleep(10);
			}

			Assert_DeclineWhileBusyException_Thrown();
		}

		[TestMethod]
		public void NewBatchInBetweenJobsOfPrevious()
		{
			BackgroundService = CreateDummyBackgroundService();
			BackgroundService.ProcessBatchFile(DummyWorker.CreateDummyBatchFile(),
				EmptyDictionary);

			Thread.Sleep(110);

			BackgroundService.ProcessBatchFile(DummyWorker.CreateDummyBatchFile(),
				EmptyDictionary);

			while(NumberJobsFinished < 2)
			{
				Thread.Sleep(10);
			}

			Assert_DeclineWhileBusyException_Thrown();
		}

		[TestMethod]
		public void NewJobInBetweenJobsOfPrevious()
		{
			BackgroundService = CreateDummyBackgroundService();
			BackgroundService.ProcessBatchFile(DummyWorker.CreateDummyBatchFile(),
				EmptyDictionary);

			Thread.Sleep(110);

			BackgroundService.RequestNewJob("", new Dictionary<string, string>());

			while(NumberJobsFinished < 2)
			{
				Thread.Sleep(10);
			}

			Assert.IsTrue(NumberJobsFinished == 2);

			Assert_DeclineWhileBusyException_Thrown();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private BackgroundService BackgroundService;

		private int NumberJobsFinished;

		private Exception InnerException;

		private Dictionary<string, string> EmptyDictionary
		{
			get
			{
				return new Dictionary<string, string>();
			}
		}

		private BackgroundService CreateDummyBackgroundService()
		{
			BackgroundService service = new BackgroundService();
			service.SetWorker(new DummyWorker());
			service.JobFailure += RegisterExeption;
			service.JobFinished += RegisterJobFinished;

			return service;
		}

		private void RegisterExeption(
			object sender,
			JobFailureEventArgs args
			)
		{
			InnerException = args.Exception;
		}

		private void RegisterJobFinished(
			object sender,
			JobFinishedEventArgs args
			)
		{
			NumberJobsFinished++;
		}

		private void AssertNoExceptionThrown()
		{
			if(InnerException != null)
			{
				Assert.Fail(InnerException.ToString());
			}
		}

		private void Assert_DeclineWhileBusyException_Thrown()
		{
			if(InnerException != null)
			{
				Assert.IsTrue(InnerException is DeclineWhileBusyException);
			}
		}
	}

	internal class DummyWorker : Worker
	{
		public static string CreateDummyBatchFile()
		{
			List<string> commands = new List<string>();
			commands.Add("Job = 0");
			commands.Add("Job = 0");
			commands.Add("Job = 0");

			File.WriteAllLines(DummyBatchPathFile, commands);

			return DummyBatchPathFile;
		}

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			return new Dictionary<string, string>();
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> param
			)
		{
		}

		protected override Type GetEnumTypeByName(
			string enumName
			)
		{
			return typeof(int);
		}

		protected override void StartJob(
			string jobId
			)
		{
			Thread.Sleep(100);
		}

		private static readonly string DummyBatchPathFile
			= YburnConfigFile.OutputPath + "DummyBatchFile.txt";
	}
}