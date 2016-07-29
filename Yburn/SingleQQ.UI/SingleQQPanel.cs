using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Yburn.Interfaces;
using Yburn.UI;

namespace Yburn.SingleQQ.UI
{
    public partial class SingleQQPanel : UserControl
    {
        /********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

        public SingleQQPanel()
            : base()
        {
            InitializeComponent();

            Name = "SingleQQ";
            LayoutBottom.AutoScrollMinSize = new Size(0, 1027);
        }

        /********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

        public void Initialize(
            JobOrganizer jobOrganizer,
            ToolTipMaker toolTipMaker
            )
        {
            Initialize(jobOrganizer);
            MakeToolTips(toolTipMaker);
        }

        public void Initialize(
            JobOrganizer jobOrganizer
            )
        {
            SetJobOrganizer(jobOrganizer);
            InitializeComboBoxes();
        }

        public RichTextBox TextBoxLog
        {
            get
            {
                return CtrlTextBoxLog;
            }
        }

        public StatusTrackingCtrl StatusTrackingCtrl
        {
            get
            {
                return CtrlStatusTrackingCtrl;
            }
        }

        public Dictionary<string, string> ControlsValues
        {
            get
            {
                return GetControlsValues();
            }
            set
            {
                SetControlsValues(value);
            }
        }

        /********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

        private JobOrganizer JobOrganizer;

        private string DataFileName = string.Empty;

        private string EnergyScale = string.Empty;

        private string MinEnergy = string.Empty;

        private string MinRadius = string.Empty;

        private string RunningCouplingTypeSelection = string.Empty;

        private string Samples = string.Empty;

        private void SetJobOrganizer(
            JobOrganizer jobOrganizer
            )
        {
            JobOrganizer = jobOrganizer;
        }

        private void InitializeComboBoxes()
        {
            CbxColorState.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("ColorState"));
            CbxPotentialType.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("PotentialType"));
            CbxRunningCouplingType.Items.AddRange(
                JobOrganizer.GetWorkerEnumEntries("RunningCouplingType"));
            CbxSpinState.Items.AddRange(JobOrganizer.GetWorkerEnumEntries("SpinState"));
        }

        private Dictionary<string, string> GetControlsValues()
        {
            Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
            nameValuePairs["AccuracyAlpha"] = TbxAccuracyAlpha.Text;
            nameValuePairs["AccuracyWaveFunction"] = TbxAccuracyWaveFunction.Text;
            nameValuePairs["AggressivenessAlpha"] = TbxAggressivenessAlpha.Text;
            nameValuePairs["AlphaHard"] = TbxAlphaHard.Text;
            nameValuePairs["AlphaSoft"] = TbxAlphaSoft.Text;
            nameValuePairs["AlphaThermal"] = TbxAlphaThermal.Text;
            nameValuePairs["AlphaUltraSoft"] = TbxAlphaUltraSoft.Text;
            nameValuePairs["AvInvRadius"] = TbxAvInvRadius.Text;
            nameValuePairs["BoundMass"] = TbxBoundMass.Text;
            nameValuePairs["ColorState"] = CbxColorState.Text;
            nameValuePairs["DataFileName"] = DataFileName;
            nameValuePairs["DebyeMass"] = TbxDebyeMass.Text;
            nameValuePairs["Energy"] = TbxEnergy.Text;
            nameValuePairs["EnergyScale"] = EnergyScale;
            nameValuePairs["EnergySteps"] = TbxEnergySteps.Text;
            nameValuePairs["GammaDamp"] = TbxGammaDamp.Text;
            nameValuePairs["GammaDiss"] = TbxGammaDiss.Text;
            nameValuePairs["GammaTot"] = TbxGammaTot.Text;
            nameValuePairs["MaxEnergy"] = TbxMaxEnergy.Text;
            nameValuePairs["MaxRadius"] = TbxMaxRadius.Text;
            nameValuePairs["MinEnergy"] = MinEnergy;
            nameValuePairs["MinRadius"] = MinRadius;
            nameValuePairs["MaxShootingTrials"] = TbxMaxShootingTrials.Text;
            nameValuePairs["NumberExtrema"] = TbxNumberExtrema.Text;
            nameValuePairs["DataFileName"] = TbxDataFileName.Text;
            nameValuePairs["PotentialType"] = CbxPotentialType.Text;
            nameValuePairs["QuantumNumberL"] = TbxQuantumNumberL.Text;
            nameValuePairs["QuantumNumberN"] = TbxQuantumNumberN.Text;
            nameValuePairs["QuarkMass"] = TbxQuarkMass.Text;
            nameValuePairs["RMS"] = TbxRMS.Text;
            nameValuePairs["RunningCouplingType"] = CbxRunningCouplingType.Text;
            nameValuePairs["RunningCouplingTypeSelection"] = RunningCouplingTypeSelection;
            nameValuePairs["Samples"] = Samples;
            nameValuePairs["Sigma"] = TbxSigma.Text;
            nameValuePairs["SigmaEff"] = TbxSigmaEff.Text;
            nameValuePairs["SoftScale"] = TbxSoftScale.Text;
            nameValuePairs["SpinCouplingRange"] = TbxSpinCouplingRange.Text;
            nameValuePairs["SpinCouplingStrength"] = TbxSpinCouplingStrength.Text;
            nameValuePairs["SpinState"] = CbxSpinState.Text;
            nameValuePairs["StepNumber"] = TbxStepNumber.Text;
            nameValuePairs["StepSize"] = TbxStepSize.Text;
            nameValuePairs["StepsPerPeriod"] = TbxStepsPerPeriod.Text;
            nameValuePairs["Temperature"] = TbxTemperature.Text;
            nameValuePairs["Tchem"] = TbxTchem.Text;
            nameValuePairs["Tcrit"] = TbxTcrit.Text;
            nameValuePairs["Trials"] = TbxTrials.Text;
            nameValuePairs["UltraSoftScale"] = TbxUltraSoftScale.Text;
            nameValuePairs["UseFixedAlpha"] = ChkUseFixedAlpha.Checked.ToString();
            nameValuePairs["WaveVector"] = TbxWaveVector.Text;

            return nameValuePairs;
        }

        private void SetControlsValues(
            Dictionary<string, string> nameValuePairs
            )
        {
            TbxAccuracyAlpha.Text = nameValuePairs["AccuracyAlpha"];
            TbxAccuracyWaveFunction.Text = nameValuePairs["AccuracyWaveFunction"];
            TbxAggressivenessAlpha.Text = nameValuePairs["AggressivenessAlpha"];
            TbxAlphaHard.Text = nameValuePairs["AlphaHard"];
            TbxAlphaSoft.Text = nameValuePairs["AlphaSoft"];
            TbxAlphaThermal.Text = nameValuePairs["AlphaThermal"];
            TbxAlphaUltraSoft.Text = nameValuePairs["AlphaUltraSoft"];
            TbxAvInvRadius.Text = nameValuePairs["AvInvRadius"];
            TbxBoundMass.Text = nameValuePairs["BoundMass"];
            CbxColorState.Text = nameValuePairs["ColorState"];
            DataFileName = nameValuePairs["DataFileName"];
            TbxDebyeMass.Text = nameValuePairs["DebyeMass"];
            TbxEnergy.Text = nameValuePairs["Energy"];
            EnergyScale = nameValuePairs["EnergyScale"];
            TbxEnergySteps.Text = nameValuePairs["EnergySteps"];
            TbxGammaDamp.Text = nameValuePairs["GammaDamp"];
            TbxGammaDiss.Text = nameValuePairs["GammaDiss"];
            TbxGammaTot.Text = nameValuePairs["GammaTot"];
            TbxMaxEnergy.Text = nameValuePairs["MaxEnergy"];
            TbxMaxRadius.Text = nameValuePairs["MaxRadius"];
            TbxMaxShootingTrials.Text = nameValuePairs["MaxShootingTrials"];
            MinEnergy = nameValuePairs["MinEnergy"];
            MinRadius = nameValuePairs["MinRadius"];
            TbxNumberExtrema.Text = nameValuePairs["NumberExtrema"];
            TbxDataFileName.Text = nameValuePairs["DataFileName"];
            CbxPotentialType.Text = nameValuePairs["PotentialType"];
            TbxQuantumNumberL.Text = nameValuePairs["QuantumNumberL"];
            TbxQuantumNumberN.Text = nameValuePairs["QuantumNumberN"];
            TbxQuarkMass.Text = nameValuePairs["QuarkMass"];
            TbxRMS.Text = nameValuePairs["RMS"];
            CbxRunningCouplingType.Text = nameValuePairs["RunningCouplingType"];
            RunningCouplingTypeSelection = nameValuePairs["RunningCouplingTypeSelection"];
            Samples = nameValuePairs["Samples"];
            TbxSigma.Text = nameValuePairs["Sigma"];
            TbxSigmaEff.Text = nameValuePairs["SigmaEff"];
            TbxSoftScale.Text = nameValuePairs["SoftScale"];
            TbxSpinCouplingRange.Text = nameValuePairs["SpinCouplingRange"];
            TbxSpinCouplingStrength.Text = nameValuePairs["SpinCouplingStrength"];
            CbxSpinState.Text = nameValuePairs["SpinState"];
            TbxStepNumber.Text = nameValuePairs["StepNumber"];
            TbxStepSize.Text = nameValuePairs["StepSize"];
            TbxStepsPerPeriod.Text = nameValuePairs["StepsPerPeriod"];
            TbxTemperature.Text = nameValuePairs["Temperature"];
            TbxTchem.Text = nameValuePairs["Tchem"];
            TbxTcrit.Text = nameValuePairs["Tcrit"];
            TbxTrials.Text = nameValuePairs["Trials"];
            TbxUltraSoftScale.Text = nameValuePairs["UltraSoftScale"];
            ChkUseFixedAlpha.Checked = bool.Parse(nameValuePairs["UseFixedAlpha"]);
            TbxWaveVector.Text = nameValuePairs["WaveVector"];
        }

        private void MakeToolTips(
            ToolTipMaker toolTipMaker
            )
        {
            toolTipMaker.Add(
                "Desired shooting accuracy in the running coupling AlphaSoft,\r\n"
                + "equals the difference in AlphaSoft between two consecutive\r\n"
                + "steps. AccuracyAlpha > 0.",
                LblAccuracyAlpha, TbxAccuracyAlpha);
            toolTipMaker.Add(
                "Desired shooting accuracy in the wave function, equals the\r\n"
                + "absolute value of the wave function at the origin.\r\n"
                + "AccuracyWaveFunction > 0.",
                LblAccuracyWaveFunction, TbxAccuracyWaveFunction);
            toolTipMaker.Add(
                "Controls the strength with which the shooting algorithm reacts to changes\r\n"
                + "in the running coupling. 0 <= AggressivenessAlpha < 1.",
                LblAggressivenessAlpha, TbxAggressivenessAlpha);
            toolTipMaker.Add(
                "Running coupling evaluated at the hard scale (QuarkMass).",
                LblAlphaHard, TbxAlphaHard);
            toolTipMaker.Add(
                "Running coupling evaluated at the soft scale (<1/r>).",
                LblAlphaSoft, TbxAlphaSoft);
            toolTipMaker.Add(
                "Running coupling evaluated at the thermal scale.",
                LblAlphaThermal, TbxAlphaThermal);
            toolTipMaker.Add(
                "Running coupling evaluated at the ultra soft scale.",
                LblAlphaUltraSoft, TbxAlphaUltraSoft);
            toolTipMaker.Add(
                "Total mass of the bottomium.",
                LblBoundMass, TbxBoundMass);
            toolTipMaker.Add(
                "Binding energy of the bottomium, defined relative to the potential-at-infinity\r\n"
                + "for converging potentials. Otherwise it is defined as the real part of the\r\n"
                + "eigenvalue of the Schroedinger equation.",
                LblEnergy, TbxEnergy);
            toolTipMaker.Add(
                "Decay width due to collisional damping, defined as twice the negative imaginary\r\n"
                + "part of the eigenvalue of the Schroedinger equation.",
                LblGammaDamp, TbxGammaDamp);
            toolTipMaker.Add(
                "Maximum number of trials when the Schroedinger equation is solved by the shooting\r\n"
                + "method. If MaxShootingTrials <= 0, the Schroedinger equation is solved only once.",
                LblMaxShootingTrials, TbxMaxShootingTrials);
            toolTipMaker.Add(
                "Color state of the bottomium. Can be color-singlet or color-octet.",
                LblColorState, CbxColorState);
            toolTipMaker.Add(
                "Spin state of the bottomium. Can be spin-singlet or spin-triplet.",
                LblSpinState, CbxSpinState);
            toolTipMaker.Add("Debye mass due to color screening in the quark-gluon plasma.",
                LblDebyeMass, TbxDebyeMass);
            toolTipMaker.Add(
                "Name of the output file. The standard output path can be set\r\n"
                + "in the menu \"File\" using \"Set output path\".",
                LblDataFileName, TbxDataFileName);
            toolTipMaker.Add(
                "Different interaction potentials:\r\n\r\n"
                + "Complex - Complex potential used e.g. in Nendzig, Wolschin (2014),\r\n"
                + "LowT - Also complex, valid for low temperatures (Brambilla et al., 2008),\r\n"
                + "Real - Obtained from the real part of \"Complex\",\r\n"
                + "Tzero - Cornell potential,\r\n"
                + "SpinDependent - Cornell potential with spin interaction term.\r\n\r\n"
                + "All potentials except the spin-dependent one are also available with vanishing\r\n"
                + "string coupling. The \"Tzero\"-potential becomes a simple Coulomb potential in\r\n"
                + "this case.",
                LblPotentialType, CbxPotentialType);
            toolTipMaker.Add(
                "Angular momentum quantum number, L = 0, 1, 2, ...",
                LblQuantumNumberL, TbxQuantumNumberL);
            toolTipMaker.Add(
                "Principle momentum quantum number, N > L, N = 1, 2, 3, ...",
                LblQuantumNumberN, TbxQuantumNumberN);
            toolTipMaker.Add(
                "Temperature of the quark-gluon plasma.",
                LblTemperature, TbxTemperature);
            toolTipMaker.Add(
                "Chemical freeze-out temperature of the hadronic medium.",
                LblTchem, TbxTchem);
            toolTipMaker.Add(
                "Critical temperature for the transition between hadronic medium\r\n"
                + "and quark-gluon plasma.",
                LblTcrit, TbxTcrit);
            toolTipMaker.Add(
                "Distance scale of the exponentially damped spin coupling.",
                LblSpinCouplingRange, TbxSpinCouplingRange);
            toolTipMaker.Add(
                "Scaling factor for the strength of the spin coupling. The absolute value of the\r\n"
                + "coupling term differs by a factor of order one at the origin.",
                LblSpinCouplingStrength, TbxSpinCouplingStrength);
            toolTipMaker.Add(
                "Different parameterizations of the running coupling:\r\n\r\n"
                + "LOPerturbative - Leading order perturbative calculations,\r\n"
                + "LOPerturbative_Cutoff1 - Leading order perturbative calculations cut off at 1,\r\n"
                + "LOPerturbative_Cutoff3 - Leading order perturbative calculations cut off at 3,\r\n"
                + "NonPerturbative_Fischer - Fit to non-perturbative calculations,\r\n"
                + "NonPerturbative_ITP - Fit to non-perturbative calculations.",
                LblRunningCouplingType, CbxRunningCouplingType);
            toolTipMaker.Add(
                "Distance of radial steps at which the wave function is calculated.",
                LblStepNumber, TbxStepNumber);
            toolTipMaker.Add(
                "String tension in the Cornell- and similar potentials.",
                LblSigma, TbxSigma);
            toolTipMaker.Add(
                "Mass of a single bottom quark in vacuum.",
                LblQuarkMass, TbxQuarkMass);
        }
    }
}