using System;
using System.Windows.Forms;
using Yburn.UI;

namespace Yburn.SingleQQ.UI
{
	public partial class AlphaPlotParamForm : Form, PlotParamForm
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public AlphaPlotParamForm(
			string title,
			string[] runningCouplingTypes,
			string runningCouplingTypeSelection,
			string minEnergyMeV,
			string maxEnergyMeV,
			string samples,
			string dataFileName
			)
		{
			InitializeComponent();
			InitializeCheckedListBox(runningCouplingTypes);

			AcceptButton = BtnPlot;
			CancelButton = BtnLeave;

			SetInputData(title, runningCouplingTypeSelection, minEnergyMeV, maxEnergyMeV,
				samples, dataFileName);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public event PlotRequestEventHandler PlotRequest;

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void InitializeCheckedListBox(
			string[] runningCouplingTypes
			)
		{
			MsxRunningCouplingType.AddItems(runningCouplingTypes);
		}

		private void SetInputData(
			string title,
			string runningCouplingTypes,
			string minEnergyMeV,
			string maxEnergyMeV,
			string samples,
			string dataFileName
			)
		{
			Text = title;
			MsxRunningCouplingType.SelectionString = runningCouplingTypes;
			TbxMinEnergy.Text = minEnergyMeV;
			TbxMaxEnergy.Text = maxEnergyMeV;
			TbxSamples.Text = samples;
			TbxDataFileName.Text = dataFileName;
		}

		private void BtnPlot_Click(object sender, EventArgs e)
		{
			if(PlotRequest != null)
			{
				AlphaPlotRequestEventArgs args = new AlphaPlotRequestEventArgs();
				args.DataFileName = TbxDataFileName.Text;
				args.MaxEnergy = TbxMaxEnergy.Text;
				args.MinEnergy = TbxMinEnergy.Text;
				args.RunningCouplingTypeSelection = MsxRunningCouplingType.SelectionString;
				args.Samples = TbxSamples.Text;

				PlotRequest(this, args);
			}
		}
	}
}
