using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Yburn.InMediumDecayWidth.UI;
using Yburn.TestUtil;

namespace Yburn.UI.Tests
{
	[TestClass]
	public class InMediumDecayWidthUITests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			BackgroundService = new BackgroundService();
			BackgroundService.SetWorker(new Workers.InMediumDecayWidth());
		}

		[TestMethod]
		public void ManipulateInMediumDecayWidthPanelControls()
		{
			InMediumDecayWidthPanel = new InMediumDecayWidthPanel();
			InMediumDecayWidthPanel.Initialize(BackgroundService);
			InMediumDecayWidthPanel.ControlsValues = ParameterSamples.InMediumDecayWidthSamples;

			AssertHelper.AssertAllElementsEqual(
				ParameterSamples.InMediumDecayWidthSamples, InMediumDecayWidthPanel.ControlsValues);
		}

		[TestMethod]
		public void StartWithEmptyParameterFile()
		{
			string lastParameterFile = YburnConfigFile.LastParaFile;
			string emptyParameterFile = "EmptyParameterFile.txt";
			File.CreateText(emptyParameterFile).Close();

			try
			{
				BackgroundService.ProcessParameterFile(emptyParameterFile);
				InMediumDecayWidthMainWindow mainWindow = new InMediumDecayWidthMainWindow("", BackgroundService);
			}
			catch(NullReferenceException)
			{
				Assert.Fail();
			}
			finally
			{
				File.Delete(emptyParameterFile);
				YburnConfigFile.LastParaFile = lastParameterFile;
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private InMediumDecayWidthPanel InMediumDecayWidthPanel;

		private BackgroundService BackgroundService;
	}
}
