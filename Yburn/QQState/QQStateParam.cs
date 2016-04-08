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

		public ColorState ColorState;

		// difference between twice the quark mass and the bound state mass
		// and the potential at infinity
		public double EnergyMeV;

		// Decay width due to the imaginary part of the potential
		public double GammaDampMeV;

		public PotentialType PotentialType;

		public int QuantumNumberL;

		public double QuarkMassMeV;

		public RunningCouplingType RunningCouplingType;

		public double MaxRadiusFm;

		public int MaxShootingTrials;

		public double SigmaMeV;

		public double SoftScaleMeV;

		public double SpinCouplingStrengthMeV;

		public double SpinCouplingRangeFm;

		public SpinState SpinState;

		public int StepNumber;

		// chemical freeze-out temperature for the hadronic medium in fm^-1
		public double TchemMeV;

		// critical temperature for the phase transition from a hadronic medium to the QGP in fm^-1
		public double TcritMeV;

		public double TemperatureMeV;

		public QQStateParam Clone()
		{
			QQStateParam param = new QQStateParam();

			param.AccuracyAlpha = AccuracyAlpha;
			param.AccuracyWaveFunction = AccuracyWaveFunction;
			param.AggressivenessAlpha = AggressivenessAlpha;
			param.ColorState = ColorState;
			param.EnergyMeV = EnergyMeV;
			param.GammaDampMeV = GammaDampMeV;
			param.MaxRadiusFm = MaxRadiusFm;
			param.MaxShootingTrials = MaxShootingTrials;
			param.PotentialType = PotentialType;
			param.QuantumNumberL = QuantumNumberL;
			param.QuarkMassMeV = QuarkMassMeV;
			param.RunningCouplingType = RunningCouplingType;
			param.SigmaMeV = SigmaMeV;
			param.SoftScaleMeV = SoftScaleMeV;
			param.SpinCouplingStrengthMeV = SpinCouplingStrengthMeV;
			param.SpinCouplingRangeFm = SpinCouplingRangeFm;
			param.SpinState = SpinState;
			param.StepNumber = StepNumber;
			param.TchemMeV = TchemMeV;
			param.TcritMeV = TcritMeV;
			param.TemperatureMeV = TemperatureMeV;

			return param;
		}
	}
}