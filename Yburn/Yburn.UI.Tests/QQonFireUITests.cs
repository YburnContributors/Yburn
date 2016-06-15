using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;
using Yburn.QQonFire.UI;
using Yburn.Tests.Util;

namespace Yburn.UI.Tests
{
	[TestClass]
	public class QQonFireUITests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			BackgroundService = new BackgroundService();
			BackgroundService.SetWorker(new Workers.QQonFire());
		}

		[TestMethod]
		public void ManipulateQQonFirePanelControls()
		{
			QQonFirePanel = new QQonFirePanel();
			QQonFirePanel.Initialize(BackgroundService);
			QQonFirePanel.ControlsValues = ParameterSamples.QQonFireSamples;

			AssertHelper.AssertAllElementsEqual(
				ParameterSamples.QQonFireSamples, QQonFirePanel.ControlsValues);
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
				QQonFireMainWindow mainWindow = new QQonFireMainWindow("", BackgroundService);
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
		public void QQonFire_BackgroundworkerCanFillAllComboBoxes()
		{
			QQonFirePanel panel = new QQonFirePanel();
			panel.Initialize(BackgroundService);

			AssertAllComboBoxesHaveItems(panel.Controls);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private QQonFirePanel QQonFirePanel;

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