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

		public double AccuracyAlpha
		{
			get
			{
				return Nullable_AccuracyAlpha.Value;
			}
			set
			{
				Nullable_AccuracyAlpha = value;
			}
		}

		public double AccuracyWaveFunction
		{
			get
			{
				return Nullable_AccuracyWaveFunction.Value;
			}
			set
			{
				Nullable_AccuracyWaveFunction = value;
			}
		}

		// determines how strong AlphaS is changed between each step
		public double AggressivenessAlpha
		{
			get
			{
				return Nullable_AggressivenessAlpha.Value;
			}
			set
			{
				Nullable_AggressivenessAlpha = value;
			}
		}

		public double AggressivenessEnergy
		{
			get
			{
				return Nullable_AggressivenessEnergy.Value;
			}
			set
			{
				Nullable_AggressivenessEnergy = value;
			}
		}

		public ColorState ColorState
		{
			get
			{
				return Nullable_ColorState.Value;
			}
			set
			{
				Nullable_ColorState = value;
			}
		}

		// difference between twice the quark mass and the bound state mass
		// and the potential at infinity
		public double Energy_MeV
		{
			get
			{
				return Nullable_Energy_MeV.Value;
			}
			set
			{
				Nullable_Energy_MeV = value;
			}
		}

		// Decay width due to the imaginary part of the potential
		public double GammaDamp_MeV
		{
			get
			{
				return Nullable_GammaDamp_MeV.Value;
			}
			set
			{
				Nullable_GammaDamp_MeV = value;
			}
		}

		public PotentialType PotentialType
		{
			get
			{
				return Nullable_PotentialType.Value;
			}
			set
			{
				Nullable_PotentialType = value;
			}
		}

		public int QuantumNumberL
		{
			get
			{
				return Nullable_QuantumNumberL.Value;
			}
			set
			{
				Nullable_QuantumNumberL = value;
			}
		}

		public double QuarkMass_MeV
		{
			get
			{
				return Nullable_QuarkMass_MeV.Value;
			}
			set
			{
				Nullable_QuarkMass_MeV = value;
			}
		}

		public RunningCouplingType RunningCouplingType
		{
			get
			{
				return Nullable_RunningCouplingType.Value;
			}
			set
			{
				Nullable_RunningCouplingType = value;
			}
		}

		public double MaxRadius_fm
		{
			get
			{
				return Nullable_MaxRadius_fm.Value;
			}
			set
			{
				Nullable_MaxRadius_fm = value;
			}
		}

		public int MaxShootingTrials
		{
			get
			{
				return Nullable_MaxShootingTrials.Value;
			}
			set
			{
				Nullable_MaxShootingTrials = value;
			}
		}

		public double Sigma_MeV
		{
			get
			{
				return Nullable_Sigma_MeV.Value;
			}
			set
			{
				Nullable_Sigma_MeV = value;
			}
		}

		public double SoftScale_MeV
		{
			get
			{
				return Nullable_SoftScale_MeV.Value;
			}
			set
			{
				Nullable_SoftScale_MeV = value;
			}
		}

		public double SpinCouplingStrength_MeV
		{
			get
			{
				return Nullable_SpinCouplingStrength_MeV.Value;
			}
			set
			{
				Nullable_SpinCouplingStrength_MeV = value;
			}
		}

		public double SpinCouplingRange_fm
		{
			get
			{
				return Nullable_SpinCouplingRange_fm.Value;
			}
			set
			{
				Nullable_SpinCouplingRange_fm = value;
			}
		}

		public SpinState SpinState
		{
			get
			{
				return Nullable_SpinState.Value;
			}
			set
			{
				Nullable_SpinState = value;
			}
		}

		public int StepNumber
		{
			get
			{
				return Nullable_StepNumber.Value;
			}
			set
			{
				Nullable_StepNumber = value;
			}
		}

		// chemical freeze-out temperature for the hadronic medium in fm^-1
		public double Tchem_MeV
		{
			get
			{
				return Nullable_Tchem_MeV.Value;
			}
			set
			{
				Nullable_Tchem_MeV = value;
			}
		}

		// critical temperature for the phase transition from a hadronic medium to the QGP in fm^-1
		public double Tcrit_MeV
		{
			get
			{
				return Nullable_Tcrit_MeV.Value;
			}
			set
			{
				Nullable_Tcrit_MeV = value;
			}
		}

		public double Temperature_MeV
		{
			get
			{
				return Nullable_Temperature_MeV.Value;
			}
			set
			{
				Nullable_Temperature_MeV = value;
			}
		}

		public QQStateParam Clone()
		{
			QQStateParam param = new QQStateParam
			{
				Nullable_AccuracyAlpha = Nullable_AccuracyAlpha,
				Nullable_AccuracyWaveFunction = Nullable_AccuracyWaveFunction,
				Nullable_AggressivenessAlpha = Nullable_AggressivenessAlpha,
				Nullable_AggressivenessEnergy = Nullable_AggressivenessEnergy,
				Nullable_ColorState = Nullable_ColorState,
				Nullable_Energy_MeV = Nullable_Energy_MeV,
				Nullable_GammaDamp_MeV = Nullable_GammaDamp_MeV,
				Nullable_MaxRadius_fm = Nullable_MaxRadius_fm,
				Nullable_MaxShootingTrials = Nullable_MaxShootingTrials,
				Nullable_PotentialType = Nullable_PotentialType,
				Nullable_QuantumNumberL = Nullable_QuantumNumberL,
				Nullable_QuarkMass_MeV = Nullable_QuarkMass_MeV,
				Nullable_RunningCouplingType = Nullable_RunningCouplingType,
				Nullable_Sigma_MeV = Nullable_Sigma_MeV,
				Nullable_SoftScale_MeV = Nullable_SoftScale_MeV,
				Nullable_SpinCouplingStrength_MeV = Nullable_SpinCouplingStrength_MeV,
				Nullable_SpinCouplingRange_fm = Nullable_SpinCouplingRange_fm,
				Nullable_SpinState = Nullable_SpinState,
				Nullable_StepNumber = Nullable_StepNumber,
				Nullable_Tchem_MeV = Nullable_Tchem_MeV,
				Nullable_Tcrit_MeV = Nullable_Tcrit_MeV,
				Nullable_Temperature_MeV = Nullable_Temperature_MeV
			};

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double? Nullable_AccuracyAlpha;

		private double? Nullable_AccuracyWaveFunction;

		private double? Nullable_AggressivenessAlpha;

		private double? Nullable_AggressivenessEnergy;

		private ColorState? Nullable_ColorState;

		private double? Nullable_Energy_MeV;

		private double? Nullable_GammaDamp_MeV;

		private PotentialType? Nullable_PotentialType;

		private int? Nullable_QuantumNumberL;

		private double? Nullable_QuarkMass_MeV;

		private RunningCouplingType? Nullable_RunningCouplingType;

		private double? Nullable_MaxRadius_fm;

		private int? Nullable_MaxShootingTrials;

		private double? Nullable_Sigma_MeV;

		private double? Nullable_SoftScale_MeV;

		private double? Nullable_SpinCouplingStrength_MeV;

		private double? Nullable_SpinCouplingRange_fm;

		private SpinState? Nullable_SpinState;

		private int? Nullable_StepNumber;

		private double? Nullable_Tchem_MeV;

		private double? Nullable_Tcrit_MeV;

		private double? Nullable_Temperature_MeV;
	}
}
