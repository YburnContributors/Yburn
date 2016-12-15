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

			Extractor.Store(nameValuePairs, "AccuracyAlpha", AccuracyAlpha);
			Extractor.Store(nameValuePairs, "AccuracyWaveFunction", AccuracyWaveFunction);
			Extractor.Store(nameValuePairs, "AggressivenessAlpha", AggressivenessAlpha);
			Extractor.Store(nameValuePairs, "AggressivenessEnergy", AggressivenessEnergy);
			Extractor.Store(nameValuePairs, "AlphaHard", AlphaHard);
			Extractor.Store(nameValuePairs, "AlphaSoft", AlphaSoft);
			Extractor.Store(nameValuePairs, "AlphaThermal", AlphaThermal);
			Extractor.Store(nameValuePairs, "AlphaUltraSoft", AlphaUltraSoft);
			Extractor.Store(nameValuePairs, "AvInvRadius", AvInvRadius);
			Extractor.Store(nameValuePairs, "BoundMass", BoundMass);
			Extractor.Store(nameValuePairs, "ColorState", ColorState);
			Extractor.Store(nameValuePairs, "DataFileName", DataFileName);
			Extractor.Store(nameValuePairs, "DebyeMass", DebyeMass);
			Extractor.Store(nameValuePairs, "Energy", Energy);
			Extractor.Store(nameValuePairs, "EnergyScale", EnergyScale);
			Extractor.Store(nameValuePairs, "EnergySteps", EnergySteps);
			Extractor.Store(nameValuePairs, "GammaDamp", GammaDamp);
			Extractor.Store(nameValuePairs, "GammaDiss", GammaDiss);
			Extractor.Store(nameValuePairs, "GammaTot", GammaTot);
			Extractor.Store(nameValuePairs, "MaxEnergy", MaxEnergy);
			Extractor.Store(nameValuePairs, "MaxRadius", MaxRadius);
			Extractor.Store(nameValuePairs, "MaxShootingTrials", MaxShootingTrials);
			Extractor.Store(nameValuePairs, "MinEnergy", MinEnergy);
			Extractor.Store(nameValuePairs, "MinRadius", MinRadius);
			Extractor.Store(nameValuePairs, "NumberExtrema", NumberExtrema);
			Extractor.Store(nameValuePairs, "PotentialType", PotentialType);
			Extractor.Store(nameValuePairs, "QuantumNumberL", QuantumNumberL);
			Extractor.Store(nameValuePairs, "QuantumNumberN", QuantumNumberN);
			Extractor.Store(nameValuePairs, "QuarkMass", QuarkMass);
			Extractor.Store(nameValuePairs, "RMS", RMS);
			Extractor.Store(nameValuePairs, "RunningCouplingType", RunningCouplingType);
			Extractor.Store(nameValuePairs, "RunningCouplingTypeSelection", RunningCouplingTypeSelection);
			Extractor.Store(nameValuePairs, "Samples", Samples);
			Extractor.Store(nameValuePairs, "Sigma", Sigma);
			Extractor.Store(nameValuePairs, "SigmaEff", SigmaEff);
			Extractor.Store(nameValuePairs, "SoftScale", SoftScale);
			Extractor.Store(nameValuePairs, "SpinCouplingRange", SpinCouplingRange);
			Extractor.Store(nameValuePairs, "SpinCouplingStrength", SpinCouplingStrength);
			Extractor.Store(nameValuePairs, "SpinState", SpinState);
			Extractor.Store(nameValuePairs, "StepNumber", StepNumber);
			Extractor.Store(nameValuePairs, "StepSize", StepSize);
			Extractor.Store(nameValuePairs, "StepsPerPeriod", StepsPerPeriod);
			Extractor.Store(nameValuePairs, "Tchem", Tchem);
			Extractor.Store(nameValuePairs, "Tcrit", Tcrit);
			Extractor.Store(nameValuePairs, "Temperature", Temperature);
			Extractor.Store(nameValuePairs, "Trials", Trials);
			Extractor.Store(nameValuePairs, "UltraSoftScale", UltraSoftScale);
			Extractor.Store(nameValuePairs, "UseFixedAlpha", UseFixedAlpha);
			Extractor.Store(nameValuePairs, "WaveVector", WaveVector);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			Extractor.TryExtract(nameValuePairs, "AccuracyAlpha", ref AccuracyAlpha);
			Extractor.TryExtract(nameValuePairs, "AccuracyWaveFunction", ref AccuracyWaveFunction);
			Extractor.TryExtract(nameValuePairs, "AggressivenessAlpha", ref AggressivenessAlpha);
			Extractor.TryExtract(nameValuePairs, "AggressivenessEnergy", ref AggressivenessEnergy);
			Extractor.TryExtract(nameValuePairs, "AlphaHard", ref AlphaHard);
			Extractor.TryExtract(nameValuePairs, "AlphaSoft", ref AlphaSoft);
			Extractor.TryExtract(nameValuePairs, "AlphaThermal", ref AlphaThermal);
			Extractor.TryExtract(nameValuePairs, "AlphaUltraSoft", ref AlphaUltraSoft);
			Extractor.TryExtract(nameValuePairs, "AvInvRadius", ref AvInvRadius);
			Extractor.TryExtract(nameValuePairs, "BoundMass", ref BoundMass);
			Extractor.TryExtract(nameValuePairs, "ColorState", ref ColorState);
			Extractor.TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			Extractor.TryExtract(nameValuePairs, "DebyeMass", ref DebyeMass);
			Extractor.TryExtract(nameValuePairs, "Energy", ref Energy);
			Extractor.TryExtract(nameValuePairs, "EnergyScale", ref EnergyScale);
			Extractor.TryExtract(nameValuePairs, "EnergySteps", ref EnergySteps);
			Extractor.TryExtract(nameValuePairs, "GammaDamp", ref GammaDamp);
			Extractor.TryExtract(nameValuePairs, "GammaDiss", ref GammaDiss);
			Extractor.TryExtract(nameValuePairs, "GammaTot", ref GammaTot);
			Extractor.TryExtract(nameValuePairs, "MaxEnergy", ref MaxEnergy);
			Extractor.TryExtract(nameValuePairs, "MaxRadius", ref MaxRadius);
			Extractor.TryExtract(nameValuePairs, "MaxShootingTrials", ref MaxShootingTrials);
			Extractor.TryExtract(nameValuePairs, "MinEnergy", ref MinEnergy);
			Extractor.TryExtract(nameValuePairs, "MinRadius", ref MinRadius);
			Extractor.TryExtract(nameValuePairs, "NumberExtrema", ref NumberExtrema);
			Extractor.TryExtract(nameValuePairs, "PotentialType", ref PotentialType);
			Extractor.TryExtract(nameValuePairs, "QuantumNumberL", ref QuantumNumberL);
			Extractor.TryExtract(nameValuePairs, "QuantumNumberN", ref QuantumNumberN);
			Extractor.TryExtract(nameValuePairs, "QuarkMass", ref QuarkMass);
			Extractor.TryExtract(nameValuePairs, "RMS", ref RMS);
			Extractor.TryExtract(nameValuePairs, "RunningCouplingType", ref RunningCouplingType);
			Extractor.TryExtract(nameValuePairs, "RunningCouplingTypeSelection", ref RunningCouplingTypeSelection);
			Extractor.TryExtract(nameValuePairs, "Samples", ref Samples);
			Extractor.TryExtract(nameValuePairs, "Sigma", ref Sigma);
			Extractor.TryExtract(nameValuePairs, "SigmaEff", ref SigmaEff);
			Extractor.TryExtract(nameValuePairs, "SoftScale", ref SoftScale);
			Extractor.TryExtract(nameValuePairs, "SpinCouplingRange", ref SpinCouplingRange);
			Extractor.TryExtract(nameValuePairs, "SpinCouplingStrength", ref SpinCouplingStrength);
			Extractor.TryExtract(nameValuePairs, "SpinState", ref SpinState);
			Extractor.TryExtract(nameValuePairs, "StepNumber", ref StepNumber);
			Extractor.TryExtract(nameValuePairs, "StepSize", ref StepSize);
			Extractor.TryExtract(nameValuePairs, "StepsPerPeriod", ref StepsPerPeriod);
			Extractor.TryExtract(nameValuePairs, "Tchem", ref Tchem);
			Extractor.TryExtract(nameValuePairs, "Tcrit", ref Tcrit);
			Extractor.TryExtract(nameValuePairs, "Temperature", ref Temperature);
			Extractor.TryExtract(nameValuePairs, "Trials", ref Trials);
			Extractor.TryExtract(nameValuePairs, "UltraSoftScale", ref UltraSoftScale);
			Extractor.TryExtract(nameValuePairs, "UseFixedAlpha", ref UseFixedAlpha);
			Extractor.TryExtract(nameValuePairs, "WaveVector", ref WaveVector);
		}

		private double AccuracyAlpha;

		private double AccuracyWaveFunction;

		private double AggressivenessAlpha;

		private double AggressivenessEnergy;

		private double AlphaHard;

		private double AlphaSoft;

		private double AlphaThermal;

		private double AlphaUltraSoft;

		private double AvInvRadius;

		private double BoundMass;

		private ColorState ColorState;

		private double DebyeMass;

		private double Energy;

		private double EnergyScale;

		private int EnergySteps;

		private double GammaDamp;

		private double GammaDiss;

		private double GammaTot;

		private double MaxEnergy;

		private double MaxRadius;

		private int MaxShootingTrials;

		private double MinEnergy;

		private double MinRadius;

		private int NumberExtrema;

		private PotentialType PotentialType;

		private int QuantumNumberL;

		private int QuantumNumberN;

		private double QuarkMass;

		private double RMS;

		private RunningCouplingType RunningCouplingType;

		private RunningCouplingType[] RunningCouplingTypeSelection = new RunningCouplingType[0];

		private int Samples;

		private double Sigma;

		private double SigmaEff;

		private double SoftScale;

		private double SpinCouplingRange;

		private double SpinCouplingStrength;

		private SpinState SpinState;

		private int StepNumber;

		private double StepSize;

		private double StepsPerPeriod;

		private double Tchem;

		private double Tcrit;

		private double Temperature;

		private double Trials;

		private double UltraSoftScale;

		private bool UseFixedAlpha;

		private double WaveVector;
	}
}