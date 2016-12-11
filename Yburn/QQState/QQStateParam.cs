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
				return AccuracyAlpha_Nullable.Value;
			}
			set
			{
				AccuracyAlpha_Nullable = value;
			}
		}

		public double AccuracyWaveFunction
		{
			get
			{
				return AccuracyWaveFunction_Nullable.Value;
			}
			set
			{
				AccuracyWaveFunction_Nullable = value;
			}
		}

		// determines how strong AlphaS is changed between each step
		public double AggressivenessAlpha
		{
			get
			{
				return AggressivenessAlpha_Nullable.Value;
			}
			set
			{
				AggressivenessAlpha_Nullable = value;
			}
		}

		public ColorState ColorState
		{
			get
			{
				return ColorState_Nullable.Value;
			}
			set
			{
				ColorState_Nullable = value;
			}
		}

		// difference between twice the quark mass and the bound state mass
		// and the potential at infinity
		public double EnergyMeV
		{
			get
			{
				return EnergyMeV_Nullable.Value;
			}
			set
			{
				EnergyMeV_Nullable = value;
			}
		}

		// Decay width due to the imaginary part of the potential
		public double GammaDampMeV
		{
			get
			{
				return GammaDampMeV_Nullable.Value;
			}
			set
			{
				GammaDampMeV_Nullable = value;
			}
		}

		public PotentialType PotentialType
		{
			get
			{
				return PotentialType_Nullable.Value;
			}
			set
			{
				PotentialType_Nullable = value;
			}
		}

		public int QuantumNumberL
		{
			get
			{
				return QuantumNumberL_Nullable.Value;
			}
			set
			{
				QuantumNumberL_Nullable = value;
			}
		}

		public double QuarkMassMeV
		{
			get
			{
				return QuarkMassMeV_Nullable.Value;
			}
			set
			{
				QuarkMassMeV_Nullable = value;
			}
		}

		public RunningCouplingType RunningCouplingType
		{
			get
			{
				return RunningCouplingType_Nullable.Value;
			}
			set
			{
				RunningCouplingType_Nullable = value;
			}
		}

		public double MaxRadiusFm
		{
			get
			{
				return MaxRadiusFm_Nullable.Value;
			}
			set
			{
				MaxRadiusFm_Nullable = value;
			}
		}

		public int MaxShootingTrials
		{
			get
			{
				return MaxShootingTrials_Nullable.Value;
			}
			set
			{
				MaxShootingTrials_Nullable = value;
			}
		}

		public double SigmaMeV
		{
			get
			{
				return SigmaMeV_Nullable.Value;
			}
			set
			{
				SigmaMeV_Nullable = value;
			}
		}

		public double SoftScaleMeV
		{
			get
			{
				return SoftScaleMeV_Nullable.Value;
			}
			set
			{
				SoftScaleMeV_Nullable = value;
			}
		}

		public double SpinCouplingStrengthMeV
		{
			get
			{
				return SpinCouplingStrengthMeV_Nullable.Value;
			}
			set
			{
				SpinCouplingStrengthMeV_Nullable = value;
			}
		}

		public double SpinCouplingRangeFm
		{
			get
			{
				return SpinCouplingRangeFm_Nullable.Value;
			}
			set
			{
				SpinCouplingRangeFm_Nullable = value;
			}
		}

		public SpinState SpinState
		{
			get
			{
				return SpinState_Nullable.Value;
			}
			set
			{
				SpinState_Nullable = value;
			}
		}

		public int StepNumber
		{
			get
			{
				return StepNumber_Nullable.Value;
			}
			set
			{
				StepNumber_Nullable = value;
			}
		}

		// chemical freeze-out temperature for the hadronic medium in fm^-1
		public double TchemMeV
		{
			get
			{
				return TchemMeV_Nullable.Value;
			}
			set
			{
				TchemMeV_Nullable = value;
			}
		}

		// critical temperature for the phase transition from a hadronic medium to the QGP in fm^-1
		public double TcritMeV
		{
			get
			{
				return TcritMeV_Nullable.Value;
			}
			set
			{
				TcritMeV_Nullable = value;
			}
		}

		public double TemperatureMeV
		{
			get
			{
				return TemperatureMeV_Nullable.Value;
			}
			set
			{
				TemperatureMeV_Nullable = value;
			}
		}

		public QQStateParam Clone()
		{
			QQStateParam param = new QQStateParam();

			param.AccuracyAlpha_Nullable = AccuracyAlpha_Nullable;
			param.AccuracyWaveFunction_Nullable = AccuracyWaveFunction_Nullable;
			param.AggressivenessAlpha_Nullable = AggressivenessAlpha_Nullable;
			param.ColorState_Nullable = ColorState_Nullable;
			param.EnergyMeV_Nullable = EnergyMeV_Nullable;
			param.GammaDampMeV_Nullable = GammaDampMeV_Nullable;
			param.MaxRadiusFm_Nullable = MaxRadiusFm_Nullable;
			param.MaxShootingTrials_Nullable = MaxShootingTrials_Nullable;
			param.PotentialType_Nullable = PotentialType_Nullable;
			param.QuantumNumberL_Nullable = QuantumNumberL_Nullable;
			param.QuarkMassMeV_Nullable = QuarkMassMeV_Nullable;
			param.RunningCouplingType_Nullable = RunningCouplingType_Nullable;
			param.SigmaMeV_Nullable = SigmaMeV_Nullable;
			param.SoftScaleMeV_Nullable = SoftScaleMeV_Nullable;
			param.SpinCouplingStrengthMeV_Nullable = SpinCouplingStrengthMeV_Nullable;
			param.SpinCouplingRangeFm_Nullable = SpinCouplingRangeFm_Nullable;
			param.SpinState_Nullable = SpinState_Nullable;
			param.StepNumber_Nullable = StepNumber_Nullable;
			param.TchemMeV_Nullable = TchemMeV_Nullable;
			param.TcritMeV_Nullable = TcritMeV_Nullable;
			param.TemperatureMeV_Nullable = TemperatureMeV_Nullable;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double? AccuracyAlpha_Nullable;

		private double? AccuracyWaveFunction_Nullable;

		private double? AggressivenessAlpha_Nullable;

		private ColorState? ColorState_Nullable;

		private double? EnergyMeV_Nullable;

		private double? GammaDampMeV_Nullable;

		private PotentialType? PotentialType_Nullable;

		private int? QuantumNumberL_Nullable;

		private double? QuarkMassMeV_Nullable;

		private RunningCouplingType? RunningCouplingType_Nullable;

		private double? MaxRadiusFm_Nullable;

		private int? MaxShootingTrials_Nullable;

		private double? SigmaMeV_Nullable;

		private double? SoftScaleMeV_Nullable;

		private double? SpinCouplingStrengthMeV_Nullable;

		private double? SpinCouplingRangeFm_Nullable;

		private SpinState? SpinState_Nullable;

		private int? StepNumber_Nullable;

		private double? TchemMeV_Nullable;

		private double? TcritMeV_Nullable;

		private double? TemperatureMeV_Nullable;
	}
}