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
			string sigma_MeV,
			string[] colorStates,
			string colorState,
			string temperature_MeV,
			string debyeMass_MeV,
			string[] spinStates,
			string spinState,
			string spinCouplingRange_fm,
			string spinCouplingStrength_MeV,
			string minRadius_fm,
			string maxRadius_fm,
			string samples,
			string dataFileName
			)
		{
			InitializeComponent();
			InitializeComboBoxes(colorStates, potentialTypes, spinStates);

			AcceptButton = BtnPlot;
			CancelButton = BtnLeave;

			SetInputData(
				title, potentialType, alphaSoft, sigma_MeV, colorState, temperature_MeV,
				debyeMass_MeV, spinState, spinCouplingRange_fm, spinCouplingStrength_MeV,
				minRadius_fm, maxRadius_fm, samples, dataFileName);
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
			string sigma_MeV,
			string colorState,
			string temperature_MeV,
			string debyeMass_MeV,
			string spinState,
			string spinCouplingRange_fm,
			string spinCouplingStrength_MeV,
			string minRadius_fm,
			string maxRadius_fm,
			string samples,
			string dataFileName
			)
		{
			Text = title;

			TbxAlphaSoft.Text = alphaSoft;
			CbxColorState.Text = colorState;
			TbxDataFileName.Text = dataFileName;
			TbxDebyeMass.Text = debyeMass_MeV;
			TbxMinRadius.Text = minRadius_fm;
			TbxMaxRadius.Text = maxRadius_fm;
			CbxPotentialType.Text = potentialType;
			TbxSamples.Text = samples;
			TbxSigma.Text = sigma_MeV;
			CbxSpinState.Text = spinState;
			TbxSpinCouplingRange.Text = spinCouplingRange_fm;
			TbxSpinCouplingStrength.Text = spinCouplingStrength_MeV;
			TbxTemperature.Text = temperature_MeV;
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
