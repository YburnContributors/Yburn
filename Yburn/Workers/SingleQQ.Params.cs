using System.Collections.Generic;
using Yburn.QQState;

namespace Yburn.Workers
{
	partial class SingleQQ
	{
		/********************************************************************************************
		   * Private/protected members, functions and properties
		   ********************************************************************************************/

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			Store(nameValuePairs, "AccuracyAlpha", AccuracyAlpha);
			Store(nameValuePairs, "AccuracyWaveFunction", AccuracyWaveFunction);
			Store(nameValuePairs, "AggressivenessAlpha", AggressivenessAlpha);
			Store(nameValuePairs, "AggressivenessEnergy", AggressivenessEnergy);
			Store(nameValuePairs, "AlphaHard", AlphaHard);
			Store(nameValuePairs, "AlphaSoft", AlphaSoft);
			Store(nameValuePairs, "AlphaThermal", AlphaThermal);
			Store(nameValuePairs, "AlphaUltraSoft", AlphaUltraSoft);
			Store(nameValuePairs, "AvgInvDisplacement_per_fm", AvgInvDisplacement_per_fm);
			Store(nameValuePairs, "BoundMass_MeV", BoundMass_MeV);
			Store(nameValuePairs, "ColorState", ColorState);
			Store(nameValuePairs, "DataFileName", DataFileName);
			Store(nameValuePairs, "DebyeMass_MeV", DebyeMass_MeV);
			Store(nameValuePairs, "Energy_MeV", Energy_MeV);
			Store(nameValuePairs, "EnergyScale_MeV", EnergyScale_MeV);
			Store(nameValuePairs, "EnergySteps", EnergySteps);
			Store(nameValuePairs, "GammaDamp_MeV", GammaDamp_MeV);
			Store(nameValuePairs, "GammaDiss_MeV", GammaDiss_MeV);
			Store(nameValuePairs, "GammaTot_MeV", GammaTot_MeV);
			Store(nameValuePairs, "MaxEnergy_MeV", MaxEnergy_MeV);
			Store(nameValuePairs, "MaxRadius_fm", MaxRadius_fm);
			Store(nameValuePairs, "MaxShootingTrials", MaxShootingTrials);
			Store(nameValuePairs, "MinEnergy_MeV", MinEnergy_MeV);
			Store(nameValuePairs, "MinRadius_fm", MinRadius_fm);
			Store(nameValuePairs, "NumberExtrema", NumberExtrema);
			Store(nameValuePairs, "PotentialType", PotentialType);
			Store(nameValuePairs, "QuantumNumberL", QuantumNumberL);
			Store(nameValuePairs, "QuantumNumberN", QuantumNumberN);
			Store(nameValuePairs, "QuarkMass_MeV", QuarkMass_MeV);
			Store(nameValuePairs, "DisplacementRMS_fm", DisplacementRMS_fm);
			Store(nameValuePairs, "RunningCouplingType", RunningCouplingType);
			Store(nameValuePairs, "RunningCouplingTypeSelection", RunningCouplingTypeSelection);
			Store(nameValuePairs, "Samples", Samples);
			Store(nameValuePairs, "Sigma_MeV2", Sigma_MeV2);
			Store(nameValuePairs, "SigmaEff_MeV2", SigmaEff_MeV2);
			Store(nameValuePairs, "SoftScale_MeV", SoftScale_MeV);
			Store(nameValuePairs, "SpinCouplingRange_fm", SpinCouplingRange_fm);
			Store(nameValuePairs, "SpinCouplingStrength_MeV", SpinCouplingStrength_MeV);
			Store(nameValuePairs, "SpinState", SpinState);
			Store(nameValuePairs, "StepNumber", StepNumber);
			Store(nameValuePairs, "StepSize", StepSize);
			Store(nameValuePairs, "StepsPerPeriod", StepsPerPeriod);
			Store(nameValuePairs, "Tchem_MeV", Tchem_MeV);
			Store(nameValuePairs, "Tcrit_MeV", Tcrit_MeV);
			Store(nameValuePairs, "Temperature_MeV", Temperature_MeV);
			Store(nameValuePairs, "Trials", Trials);
			Store(nameValuePairs, "UltraSoftScale_MeV", UltraSoftScale_MeV);
			Store(nameValuePairs, "UseFixedAlpha", UseFixedAlpha);
			Store(nameValuePairs, "WaveVector_per_fm", WaveVector_per_fm);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
		   Dictionary<string, string> nameValuePairs
		   )
		{
			TryExtract(nameValuePairs, "AccuracyAlpha", ref AccuracyAlpha);
			TryExtract(nameValuePairs, "AccuracyWaveFunction", ref AccuracyWaveFunction);
			TryExtract(nameValuePairs, "AggressivenessAlpha", ref AggressivenessAlpha);
			TryExtract(nameValuePairs, "AggressivenessEnergy", ref AggressivenessEnergy);
			TryExtract(nameValuePairs, "AlphaHard", ref AlphaHard);
			TryExtract(nameValuePairs, "AlphaSoft", ref AlphaSoft);
			TryExtract(nameValuePairs, "AlphaThermal", ref AlphaThermal);
			TryExtract(nameValuePairs, "AlphaUltraSoft", ref AlphaUltraSoft);
			TryExtract(nameValuePairs, "AvgInvDisplacement_per_fm", ref AvgInvDisplacement_per_fm);
			TryExtract(nameValuePairs, "BoundMass_MeV", ref BoundMass_MeV);
			TryExtract(nameValuePairs, "ColorState", ref ColorState);
			TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			TryExtract(nameValuePairs, "DebyeMass_MeV", ref DebyeMass_MeV);
			TryExtract(nameValuePairs, "Energy_MeV", ref Energy_MeV);
			TryExtract(nameValuePairs, "EnergyScale_MeV", ref EnergyScale_MeV);
			TryExtract(nameValuePairs, "EnergySteps", ref EnergySteps);
			TryExtract(nameValuePairs, "GammaDamp_MeV", ref GammaDamp_MeV);
			TryExtract(nameValuePairs, "GammaDiss_MeV", ref GammaDiss_MeV);
			TryExtract(nameValuePairs, "GammaTot_MeV", ref GammaTot_MeV);
			TryExtract(nameValuePairs, "MaxEnergy_MeV", ref MaxEnergy_MeV);
			TryExtract(nameValuePairs, "MaxRadius_fm", ref MaxRadius_fm);
			TryExtract(nameValuePairs, "MaxShootingTrials", ref MaxShootingTrials);
			TryExtract(nameValuePairs, "MinEnergy_MeV", ref MinEnergy_MeV);
			TryExtract(nameValuePairs, "MinRadius_fm", ref MinRadius_fm);
			TryExtract(nameValuePairs, "NumberExtrema", ref NumberExtrema);
			TryExtract(nameValuePairs, "PotentialType", ref PotentialType);
			TryExtract(nameValuePairs, "QuantumNumberL", ref QuantumNumberL);
			TryExtract(nameValuePairs, "QuantumNumberN", ref QuantumNumberN);
			TryExtract(nameValuePairs, "QuarkMass_MeV", ref QuarkMass_MeV);
			TryExtract(nameValuePairs, "DisplacementRMS_fm", ref DisplacementRMS_fm);
			TryExtract(nameValuePairs, "RunningCouplingType", ref RunningCouplingType);
			TryExtract(nameValuePairs, "RunningCouplingTypeSelection", ref RunningCouplingTypeSelection);
			TryExtract(nameValuePairs, "Samples", ref Samples);
			TryExtract(nameValuePairs, "Sigma_MeV2", ref Sigma_MeV2);
			TryExtract(nameValuePairs, "SigmaEff_MeV2", ref SigmaEff_MeV2);
			TryExtract(nameValuePairs, "SoftScale_MeV", ref SoftScale_MeV);
			TryExtract(nameValuePairs, "SpinCouplingRange_fm", ref SpinCouplingRange_fm);
			TryExtract(nameValuePairs, "SpinCouplingStrength_MeV", ref SpinCouplingStrength_MeV);
			TryExtract(nameValuePairs, "SpinState", ref SpinState);
			TryExtract(nameValuePairs, "StepNumber", ref StepNumber);
			TryExtract(nameValuePairs, "StepSize", ref StepSize);
			TryExtract(nameValuePairs, "StepsPerPeriod", ref StepsPerPeriod);
			TryExtract(nameValuePairs, "Tchem_MeV", ref Tchem_MeV);
			TryExtract(nameValuePairs, "Tcrit_MeV", ref Tcrit_MeV);
			TryExtract(nameValuePairs, "Temperature_MeV", ref Temperature_MeV);
			TryExtract(nameValuePairs, "Trials", ref Trials);
			TryExtract(nameValuePairs, "UltraSoftScale_MeV", ref UltraSoftScale_MeV);
			TryExtract(nameValuePairs, "UseFixedAlpha", ref UseFixedAlpha);
			TryExtract(nameValuePairs, "WaveVector_per_fm", ref WaveVector_per_fm);
		}

		private double AccuracyAlpha;

		private double AccuracyWaveFunction;

		private double AggressivenessAlpha;

		private double AggressivenessEnergy;

		private double AlphaHard;

		private double AlphaSoft;

		private double AlphaThermal;

		private double AlphaUltraSoft;

		private double AvgInvDisplacement_per_fm;

		private double BoundMass_MeV;

		private ColorState ColorState;

		private double DebyeMass_MeV;

		private double Energy_MeV;

		private double EnergyScale_MeV;

		private int EnergySteps;

		private double GammaDamp_MeV;

		private double GammaDiss_MeV;

		private double GammaTot_MeV;

		private double MaxEnergy_MeV;

		private double MaxRadius_fm;

		private int MaxShootingTrials;

		private double MinEnergy_MeV;

		private double MinRadius_fm;

		private int NumberExtrema;

		private PotentialType PotentialType;

		private int QuantumNumberL;

		private int QuantumNumberN;

		private double QuarkMass_MeV;

		private double DisplacementRMS_fm;

		private RunningCouplingType RunningCouplingType;

		private List<RunningCouplingType> RunningCouplingTypeSelection = new List<RunningCouplingType>();

		private int Samples;

		private double Sigma_MeV2;

		private double SigmaEff_MeV2;

		private double SoftScale_MeV;

		private double SpinCouplingRange_fm;

		private double SpinCouplingStrength_MeV;

		private SpinState SpinState;

		private int StepNumber;

		private double StepSize;

		private double StepsPerPeriod;

		private double Tchem_MeV;

		private double Tcrit_MeV;

		private double Temperature_MeV;

		private double Trials;

		private double UltraSoftScale_MeV;

		private bool UseFixedAlpha;

		private double WaveVector_per_fm;
	}
}
