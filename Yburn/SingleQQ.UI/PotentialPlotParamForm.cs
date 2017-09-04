using System;
using System.Windows.Forms;
using Yburn.UI;

namespace Yburn.SingleQQ.UI
{
	public partial class PotentialPlotParamForm : Form, PlotParamForm
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public PotentialPlotParamForm(
			string title,
			string[] potentialTypes,
			string potentialType,
			string alphaSoft,
			string sigmaMeV,
			string[] colorStates,
			string colorState,
			string temperatureMeV,
			string debyeMassMeV,
			string[] spinStates,
			string spinState,
			string spinCouplingRangeFm,
			string spinCouplingStrengthMeV,
			string minRadiusFm,
			string maxRadiusFm,
			string samples,
			string dataFileName
			)
		{
			InitializeComponent();
			InitializeComboBoxes(colorStates, potentialTypes, spinStates);

			AcceptButton = BtnPlot;
			CancelButton = BtnLeave;

			SetInputData(
				title, potentialType, alphaSoft, sigmaMeV, colorState, temperatureMeV,
				debyeMassMeV, spinState, spinCouplingRangeFm, spinCouplingStrengthMeV,
				minRadiusFm, maxRadiusFm, samples, dataFileName);
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

		private void InitializeComboBoxes(
			string[] colorStates,
			string[] potentialTypes,
			string[] spinStates
			)
		{
			CbxColorState.Items.AddRange(colorStates);
			CbxPotentialType.Items.AddRange(potentialTypes);
			CbxSpinState.Items.AddRange(spinStates);
		}

		private void SetInputData(
			string title,
			string potentialType,
			string alphaSoft,
			string sigmaMeV,
			string colorState,
			string temperatureMeV,
			string debyeMassMeV,
			string spinState,
			string spinCouplingRangeFm,
			string spinCouplingStrengthMeV,
			string minRadiusFm,
			string maxRadiusFm,
			string samples,
			string dataFileName
			)
		{
			Text = title;

			TbxAlphaSoft.Text = alphaSoft;
			CbxColorState.Text = colorState;
			TbxDataFileName.Text = dataFileName;
			TbxDebyeMass.Text = debyeMassMeV;
			TbxMinRadius.Text = minRadiusFm;
			TbxMaxRadius.Text = maxRadiusFm;
			CbxPotentialType.Text = potentialType;
			TbxSamples.Text = samples;
			TbxSigma.Text = sigmaMeV;
			CbxSpinState.Text = spinState;
			TbxSpinCouplingRange.Text = spinCouplingRangeFm;
			TbxSpinCouplingStrength.Text = spinCouplingStrengthMeV;
			TbxTemperature.Text = temperatureMeV;
		}

		private void BtnPlot_Click(object sender, EventArgs e)
		{
			if(PlotRequest != null)
			{
				PotentialPlotRequestEventArgs args = new PotentialPlotRequestEventArgs();
				args.AlphaSoft = TbxAlphaSoft.Text;
				args.ColorState = CbxColorState.Text;
				args.DataFileName = TbxDataFileName.Text;
				args.DebyeMass = TbxDebyeMass.Text;
				args.MinRadius = TbxMinRadius.Text;
				args.MaxRadius = TbxMaxRadius.Text;
				args.PotentialType = CbxPotentialType.Text;
				args.Samples = TbxSamples.Text;
				args.Sigma = TbxSigma.Text;
				args.SpinState = CbxSpinState.Text;
				args.SpinCouplingRange = TbxSpinCouplingRange.Text;
				args.SpinCouplingStrength = TbxSpinCouplingStrength.Text;
				args.Temperature = TbxTemperature.Text;

				PlotRequest(this, args);
			}
		}
	}
}
