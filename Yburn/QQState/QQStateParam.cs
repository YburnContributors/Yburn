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
			QQStateParam param = new QQStateParam
			{
				AccuracyAlpha = AccuracyAlpha,
				AccuracyWaveFunction = AccuracyWaveFunction,
				AggressivenessAlpha = AggressivenessAlpha,
				AggressivenessEnergy = AggressivenessEnergy,
				ColorState = ColorState,
				Energy_MeV = Energy_MeV,
				GammaDamp_MeV = GammaDamp_MeV,
				MaxRadius_fm = MaxRadius_fm,
				MaxShootingTrials = MaxShootingTrials,
				PotentialType = PotentialType,
				QuantumNumberL = QuantumNumberL,
				QuarkMass_MeV = QuarkMass_MeV,
				RunningCouplingType = RunningCouplingType,
				Sigma_MeV = Sigma_MeV,
				SoftScale_MeV = SoftScale_MeV,
				SpinCouplingStrength_MeV = SpinCouplingStrength_MeV,
				SpinCouplingRange_fm = SpinCouplingRange_fm,
				SpinState = SpinState,
				StepNumber = StepNumber,
				Tchem_MeV = Tchem_MeV,
				Tcrit_MeV = Tcrit_MeV,
				Temperature_MeV = Temperature_MeV
			};

			return param;
		}
	}
}
