using System;
using System.Windows.Forms;
using Yburn.UI;

namespace Yburn.SingleQQ.UI
{
    public partial class PionGDFPlotParamForm : Form, PlotParamForm
    {
        /********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

        public PionGDFPlotParamForm(
            string title,
            string energyScaleMeV,
            string samples,
            string dataFileName
            )
        {
            InitializeComponent();

            AcceptButton = BtnPlot;
            CancelButton = BtnLeave;

            SetInputData(title, energyScaleMeV, samples, dataFileName);
        }

        /********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

        public event PlotRequestEventHandler PlotRequest;

        /********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

        private void SetInputData(
            string title,
            string energyScaleMeV,
            string samples,
            string dataFileName
            )
        {
            Text = title;
            TbxEnergyScale.Text = energyScaleMeV;
            TbxSamples.Text = samples;
            TbxDataFileName.Text = dataFileName;
        }

        private void BtnPlot_Click(object sender, EventArgs e)
        {
            if(PlotRequest != null)
            {
                PionGDFPlotRequestEventArgs args = new PionGDFPlotRequestEventArgs();
                args.DataFileName = TbxDataFileName.Text;
                args.EnergyScale = TbxEnergyScale.Text;
                args.Samples = TbxSamples.Text;

                PlotRequest(this, args);
            }
        }
    }
}