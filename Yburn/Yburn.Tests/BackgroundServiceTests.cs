using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Yburn.Interfaces;
using Yburn.TestUtil;

namespace Yburn.Tests
{
	[TestClass]
	public class BackgroundServiceTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			BackgroundService = new BackgroundService();
			FileCleaner = new FileCleaner();
		}

		[TestCleanup]
		public void CleanTestFiles()
		{
			FileCleaner.Clean();
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOutputPathException))]
		public void ThrowIfInvalidLogPathFile()
		{
			string strOldOutputPath = YburnConfigFile.OutputPath;
			YburnConfigFile.OutputPath = "C:\\InvalidPath\\";

			try
			{
				AttemptWritingToLogFile();
			}
			finally
			{
				YburnConfigFile.OutputPath = strOldOutputPath;
			}
		}

		[TestMethod]
		public void ManipulateSingleQQVariables()
		{
			BackgroundService.SetWorker(WorkerLoader.CreateInstance("SingleQQ"));
			BackgroundService.TransferDataToWorker(ParameterSamples.SingleQQSamples);

			AssertHelper.AssertAllElementsEqual(
				ParameterSamples.SingleQQSamples, BackgroundService.GetDataFromWorker());
		}

		[TestMethod]
		public void ManipulateQQonFireVariables()
		{
			BackgroundService.SetWorker(WorkerLoader.CreateInstance("QQonFire"));
			BackgroundService.TransferDataToWorker(ParameterSamples.QQonFireSamples);

			AssertHelper.AssertAllElementsEqual(
				ParameterSamples.QQonFireSamples, BackgroundService.GetDataFromWorker());
		}

		[TestMethod]
		public void ManipulateInMediumDecayWidthVariables()
		{
			BackgroundService.SetWorker(WorkerLoader.CreateInstance("InMediumDecayWidth"));
			BackgroundService.TransferDataToWorker(ParameterSamples.InMediumDecayWidthSamples);

			AssertHelper.AssertAllElementsEqual(
				ParameterSamples.InMediumDecayWidthSamples, BackgroundService.GetDataFromWorker());
		}

		[TestMethod]
		public void ProcessParameterFile_Electromagnetism()
		{
			string lastParameterFile = YburnConfigFile.LastParaFile;
			WriteTestParaFile(ParameterSamples.ElectromagnetismSamples);

			BackgroundService.SetWorker(WorkerLoader.CreateInstance("Electromagnetism"));
			BackgroundService.ProcessParameterFile(TestParameterFileName);

			AssertCorrectProcessing(ParameterSamples.ElectromagnetismSamples);
			YburnConfigFile.LastParaFile = lastParameterFile;
		}

		[TestMethod]
		public void ProcessParameterFile_SingleQQ()
		{
			string lastParameterFile = YburnConfigFile.LastParaFile;
			WriteTestParaFile(ParameterSamples.SingleQQSamples);

			BackgroundService.SetWorker(WorkerLoader.CreateInstance("SingleQQ"));
			BackgroundService.ProcessParameterFile(TestParameterFileName);

			AssertCorrectProcessing(ParameterSamples.SingleQQSamples);
			YburnConfigFile.LastParaFile = lastParameterFile;
		}

		[TestMethod]
		public void ProcessParameterFile_QQonFire()
		{
			string lastParameterFile = YburnConfigFile.LastParaFile;
			WriteTestParaFile(ParameterSamples.QQonFireSamples);

			BackgroundService.SetWorker(WorkerLoader.CreateInstance("QQonFire"));
			BackgroundService.ProcessParameterFile(TestParameterFileName);

			AssertCorrectProcessing(ParameterSamples.QQonFireSamples);
			YburnConfigFile.LastParaFile = lastParameterFile;
		}

		[TestMethod]
		public void ProcessParameterFile_InMediumDecayWidth()
		{
			string lastParameterFile = YburnConfigFile.LastParaFile;
			WriteTestParaFile(ParameterSamples.InMediumDecayWidthSamples);

			BackgroundService.SetWorker(WorkerLoader.CreateInstance("InMediumDecayWidth"));
			BackgroundService.ProcessParameterFile(TestParameterFileName);

			AssertCorrectProcessing(ParameterSamples.InMediumDecayWidthSamples);
			YburnConfigFile.LastParaFile = lastParameterFile;
		}

		[TestMethod]
		public void BackgroundServiceJobFailureEvent()
		{
			BackgroundService.SetWorker(WorkerLoader.CreateInstance("SingleQQ"));
			BackgroundService.JobFailure += RegisterJobFailure;
			BackgroundService.RequestNewJob(
				"CalculateBoundWaveFunction", new Dictionary<string, string>());

			WaitForJobFailure(2000);

			Assert.IsTrue(HasRegisteredJobFailure);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AttemptWritingToLogFile()
		{
			PrivateObject privateBackgroundService = new PrivateObject(typeof(BackgroundService));
			privateBackgroundService.SetField("LogMessages", new StringBuilder());
			privateBackgroundService.Invoke("WriteToLogFile");
		}

		private static string TestParameterFileName = "YburnTestParameterFile.txt";

		private static string GetStringContent(
			Dictionary<string, string> paramList
			)
		{
			StringBuilder content = new StringBuilder();
			foreach(KeyValuePair<string, string> param in paramList)
			{
				content.AppendFormat("{0} = {1}\r\n", param.Key, param.Value);
			}

			return content.ToString();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FileCleaner FileCleaner;

		private void WriteTestParaFile(
			Dictionary<string, string> paramList
			)
		{
			File.WriteAllText(TestParameterFileName, GetStringContent(paramList));
			FileCleaner.MarkForDelete(TestParameterFileName);
		}

		private BackgroundService BackgroundService;

		private void AssertCorrectProcessing(
			Dictionary<string, string> testParams
			)
		{
			AssertHelper.AssertAllElementsEqual(
				testParams, BackgroundService.GetDataFromWorker());
		}

		private bool HasRegisteredJobFailure = false;

		private void RegisterJobFailure(
			object sender,
			JobFailureEventArgs args
			)
		{
			HasRegisteredJobFailure = true;
		}

		private void WaitForJobFailure(
			uint milliSecondsTimeOut
			)
		{
			uint milliSecondsWaited = 0;
			while(!HasRegisteredJobFailure
				&& milliSecondsWaited < milliSecondsTimeOut)
			{
				milliSecondsWaited += 10;
				Thread.Sleep(10);
			}
		}
	}
}