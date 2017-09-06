namespace Yburn.QQState
{
	public class QQStateParam
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQStateParam()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double AccuracyAlpha;

		public double AccuracyWaveFunction;

		// determines how strong AlphaS is changed between each step
		public double AggressivenessAlpha;

		public double AggressivenessEnergy;

		public ColorState ColorState;

		// difference between twice the quark mass and the bound state mass
		// and the potential at infinity
		public double Energy_MeV;

		// Decay width due to the imaginary part of the potential
		public double GammaDamp_MeV;

		public PotentialType PotentialType;

		public int QuantumNumberL;

		public double QuarkMass_MeV;

		public RunningCouplingType RunningCouplingType;

		public double MaxRadius_fm;

		public int MaxShootingTrials;

		public double Sigma_MeV;

		public double SoftScale_MeV;

		public double SpinCouplingStrength_MeV;

		public double SpinCouplingRange_fm;

		public SpinState SpinState;

		public int StepNumber;

		// chemical freeze-out temperature for the hadronic medium in fm^-1
		public double Tchem_MeV;

		// critical temperature for the phase transition from a hadronic medium to the QGP in fm^-1
		public double Tcrit_MeV;

		public double Temperature_MeV;

		public QQStateParam Clone()
		{
			QQStateParam param = new QQStateParam();

			param.AccuracyAlpha = AccuracyAlpha;
			param.AccuracyWaveFunction = AccuracyWaveFunction;
			param.AggressivenessAlpha = AggressivenessAlpha;
			param.AggressivenessEnergy = AggressivenessEnergy;
			param.ColorState = ColorState;
			param.Energy_MeV = Energy_MeV;
			param.GammaDamp_MeV = GammaDamp_MeV;
			param.MaxRadius_fm = MaxRadius_fm;
			param.MaxShootingTrials = MaxShootingTrials;
			param.PotentialType = PotentialType;
			param.QuantumNumberL = QuantumNumberL;
			param.QuarkMass_MeV = QuarkMass_MeV;
			param.RunningCouplingType = RunningCouplingType;
			param.Sigma_MeV = Sigma_MeV;
			param.SoftScale_MeV = SoftScale_MeV;
			param.SpinCouplingStrength_MeV = SpinCouplingStrength_MeV;
			param.SpinCouplingRange_fm = SpinCouplingRange_fm;
			param.SpinState = SpinState;
			param.StepNumber = StepNumber;
			param.Tchem_MeV = Tchem_MeV;
			param.Tcrit_MeV = Tcrit_MeV;
			param.Temperature_MeV = Temperature_MeV;

			return param;
		}
	}
}
