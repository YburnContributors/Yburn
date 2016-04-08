using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yburn.UI;

namespace Yburn.SingleQQ
{
	public partial class SingleQQMainWindow
	{
		private void SetMenuToolTips()
		{
			MenuItemCalculateBoundWave.ToolTipText =
				"Searches the bound state eigenfunction and eigenvalue\r\n"
				+ "by a shooting algorithm that varies Energy, GammaDamp\r\n"
				+ "and SoftScale.";
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

		private void MenuItemSelectOutputPath_Click(object sender, EventArgs e)
		{
			YburnConfigDataBox.SelectOutputPath();
			Dictionary<string, string> param = new Dictionary<string, string>();
			param.Add("OutputPath", YburnConfigDataBox.OutputPath);
			JobOrganizer.RequestNewJob("SetOutputPath", param);
		}

		private void MenuItemSelectQQDataFile_Click(object sender, EventArgs e)
		{
			YburnConfigDataBox.SelectQQDataFile();
			Dictionary<string, string> param = new Dictionary<string, string>();
			param.Add("QQDataFile", YburnConfigDataBox.QQDataPathFile);
			JobOrganizer.RequestNewJob("SetQQDataFile", param);
		}

		private void MenuItemLoadBatchFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				JobOrganizer.ProcessBatchFile(dialog.FileName, ControlsValues);
			}
		}

		private void MenutItemSaveValuesAsParameterFile_Click(object sender, EventArgs e)
		{
			using(SaveFileDialog dialog = SaveAsParaFileDialog.Create())
			{
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					JobOrganizer.SaveAsParameterFile(dialog.FileName, ControlsValues);
				}
			}
		}

		private void MenuItemArchiveQQData_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ArchiveQQData", ControlsValues);
		}

		private void MenuItemShowArchivedQQData_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("ShowArchivedQQData", ControlsValues);
		}

		private void MenuItemCalculateBoundWave_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateBoundWaveFunction", ControlsValues);
		}

		private void MenuItemCalculateFreeWave_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateFreeWaveFunction", ControlsValues);
		}

		private void MenuItemCalculateGammaDiss_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateDissociationDecayWidth", ControlsValues);
		}

		private void MenuItemCalculateQuarkMass_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CalculateQuarkMass", ControlsValues);
		}

		private void MenuItemPlotWave_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotWaveFunction", PlotterUITool.ControlsValues);
		}

		private void MenuItemPlotCrossSection_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("PlotCrossSection", PlotterUITool.ControlsValues);
		}

		private void MenuItemSelectArchiveDataFile_Click(object sender, EventArgs e)
		{
			using(SaveFileDialog dialog = SelectArchiveDataFileDialog.Create())
			{
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					Dictionary<string, string> parameters = new Dictionary<string, string>();
					parameters.Add("QQDataPathFile", dialog.FileName);
					JobOrganizer.RequestNewJob("CreateNewArchiveDataFile", parameters);
				}
			}
		}

		private void MenuItemCompareResultsWithArchivedData_Click(object sender, EventArgs e)
		{
			JobOrganizer.RequestNewJob("CompareResultsWithArchivedData", ControlsValues);
		}

		private void MenuItemPlotAlpha_Click(object sender, EventArgs e)
		{
			AlphaPlotParamForm plotForm = PlotterUITool.CreateAlphaPlotParamForm(ControlsValues);
			plotForm.PlotRequest += OnAlphaPlotRequest;

			plotForm.ShowDialog();
		}

		private void OnAlphaPlotRequest(
			object sender,
			PlotRequestEventArgs args
			)
		{
			JobOrganizer.RequestNewJob("PlotAlpha", PlotterUITool.ControlsValues);
		}

		private void MenuItemPlotPionGDF_Click(object sender, EventArgs e)
		{
			PionGDFPlotParamForm plotForm = PlotterUITool.CreatePionGDFPlotParamForm(ControlsValues);
			plotForm.PlotRequest += OnPionGDFPlotRequest;

			plotForm.ShowDialog();
		}

		private void OnPionGDFPlotRequest(
			object sender,
			PlotRequestEventArgs args
			)
		{
			JobOrganizer.RequestNewJob("PlotPionGDF", PlotterUITool.ControlsValues);
		}

		private void MenuItemPlotPotential_Click(object sender, EventArgs e)
		{
			PotentialPlotParamForm plotForm = PlotterUITool.CreatePotentialPlotParamForm(
				ControlsValues);
			plotForm.PlotRequest += OnPotentialPlotRequest;

			plotForm.ShowDialog();
		}

		private void OnPotentialPlotRequest(
			object sender,
			PlotRequestEventArgs args
			)
		{
			JobOrganizer.RequestNewJob("PlotQQPotential", PlotterUITool.ControlsValues);
		}
	}
}