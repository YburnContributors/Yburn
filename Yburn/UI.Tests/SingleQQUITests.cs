using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;
using Yburn.SingleQQ.UI;
using Yburn.TestUtil;

namespace Yburn.UI.Tests
{
	[TestClass]
	public class SingleQQUITests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			BackgroundService = new BackgroundService();
			BackgroundService.SetWorker(new Workers.SingleQQ());
		}

		[TestMethod]
		public void ManipulateSingleQQPanelControls()
		{
			SingleQQPanel = new SingleQQPanel();
			SingleQQPanel.Initialize(BackgroundService);
			SingleQQPanel.ControlsValues = ParameterSamples.SingleQQSamples;

			AssertHelper.AssertAllElementsEqual(
				ParameterSamples.SingleQQSamples, SingleQQPanel.ControlsValues);
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
				SingleQQMainWindow mainWindow = new SingleQQMainWindow("", BackgroundService);
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

		[TestMethod]
		public void SingleQQ_BackgroundworkerCanFillAllComboBoxes()
		{
			SingleQQPanel panel = new SingleQQPanel();
			panel.Initialize(BackgroundService);

			AssertAllComboBoxesHaveItems(panel.Controls);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private SingleQQPanel SingleQQPanel;

		private BackgroundService BackgroundService;

		private void AssertAllComboBoxesHaveItems(
			Control.ControlCollection controls
			)
		{
			foreach(Control control in controls)
			{
				if(control is ComboBox)
				{
					AssertComboBoxHasItems(control as ComboBox);
				}

				if(control.Controls.Count > 0)
				{
					AssertAllComboBoxesHaveItems(control.Controls);
				}
			}
		}

		private void AssertComboBoxHasItems(
			ComboBox comboBox
			)
		{
			Assert.IsTrue(comboBox.Items.Count > 0,
				string.Format("The ComboBox \"{0}\" has no items.", comboBox.Name));
		}
	}
}