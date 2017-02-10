using System.Collections.Generic;
using Yburn.QQState;

namespace Yburn.Workers
{
   partial class SingleQQ
   {
      /********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

      protected override Dictionary<string, string> GetVariableNameValuePairs() {
         Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

         Store(nameValuePairs, "AccuracyAlpha", AccuracyAlpha);
         Store(nameValuePairs, "AccuracyWaveFunction", AccuracyWaveFunction);
         Store(nameValuePairs, "AggressivenessAlpha", AggressivenessAlpha);
         Store(nameValuePairs, "AggressivenessEnergy", AggressivenessEnergy);
         Store(nameValuePairs, "AlphaHard", AlphaHard);
         Store(nameValuePairs, "AlphaSoft", AlphaSoft);
         Store(nameValuePairs, "AlphaThermal", AlphaThermal);
         Store(nameValuePairs, "AlphaUltraSoft", AlphaUltraSoft);
         Store(nameValuePairs, "AvInvRadius", AvInvRadius);
         Store(nameValuePairs, "BoundMass", BoundMass);
         Store(nameValuePairs, "ColorState", ColorState);
         Store(nameValuePairs, "DataFileName", DataFileName);
         Store(nameValuePairs, "DebyeMass", DebyeMass);
         Store(nameValuePairs, "Energy", Energy);
         Store(nameValuePairs, "EnergyScale", EnergyScale);
         Store(nameValuePairs, "EnergySteps", EnergySteps);
         Store(nameValuePairs, "GammaDamp", GammaDamp);
         Store(nameValuePairs, "GammaDiss", GammaDiss);
         Store(nameValuePairs, "GammaTot", GammaTot);
         Store(nameValuePairs, "MaxEnergy", MaxEnergy);
         Store(nameValuePairs, "MaxRadius", MaxRadius);
         Store(nameValuePairs, "MaxShootingTrials", MaxShootingTrials);
         Store(nameValuePairs, "MinEnergy", MinEnergy);
         Store(nameValuePairs, "MinRadius", MinRadius);
         Store(nameValuePairs, "NumberExtrema", NumberExtrema);
         Store(nameValuePairs, "PotentialType", PotentialType);
         Store(nameValuePairs, "QuantumNumberL", QuantumNumberL);
         Store(nameValuePairs, "QuantumNumberN", QuantumNumberN);
         Store(nameValuePairs, "QuarkMass", QuarkMass);
         Store(nameValuePairs, "RMS", RMS);
         Store(nameValuePairs, "RunningCouplingType", RunningCouplingType);
         Store(nameValuePairs, "RunningCouplingTypeSelection", RunningCouplingTypeSelection);
         Store(nameValuePairs, "Samples", Samples);
         Store(nameValuePairs, "Sigma", Sigma);
         Store(nameValuePairs, "SigmaEff", SigmaEff);
         Store(nameValuePairs, "SoftScale", SoftScale);
         Store(nameValuePairs, "SpinCouplingRange", SpinCouplingRange);
         Store(nameValuePairs, "SpinCouplingStrength", SpinCouplingStrength);
         Store(nameValuePairs, "SpinState", SpinState);
         Store(nameValuePairs, "StepNumber", StepNumber);
         Store(nameValuePairs, "StepSize", StepSize);
         Store(nameValuePairs, "StepsPerPeriod", StepsPerPeriod);
         Store(nameValuePairs, "Tchem", Tchem);
         Store(nameValuePairs, "Tcrit", Tcrit);
         Store(nameValuePairs, "Temperature", Temperature);
         Store(nameValuePairs, "Trials", Trials);
         Store(nameValuePairs, "UltraSoftScale", UltraSoftScale);
         Store(nameValuePairs, "UseFixedAlpha", UseFixedAlpha);
         Store(nameValuePairs, "WaveVector", WaveVector);

         return nameValuePairs;
      }

      protected override void SetVariableNameValuePairs(
         Dictionary<string, string> nameValuePairs
         ) {
         TryExtract(nameValuePairs, "AccuracyAlpha", ref AccuracyAlpha);
         TryExtract(nameValuePairs, "AccuracyWaveFunction", ref AccuracyWaveFunction);
         TryExtract(nameValuePairs, "AggressivenessAlpha", ref AggressivenessAlpha);
         TryExtract(nameValuePairs, "AggressivenessEnergy", ref AggressivenessEnergy);
         TryExtract(nameValuePairs, "AlphaHard", ref AlphaHard);
         TryExtract(nameValuePairs, "AlphaSoft", ref AlphaSoft);
         TryExtract(nameValuePairs, "AlphaThermal", ref AlphaThermal);
         TryExtract(nameValuePairs, "AlphaUltraSoft", ref AlphaUltraSoft);
         TryExtract(nameValuePairs, "AvInvRadius", ref AvInvRadius);
         TryExtract(nameValuePairs, "BoundMass", ref BoundMass);
         TryExtract(nameValuePairs, "ColorState", ref ColorState);
         TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
         TryExtract(nameValuePairs, "DebyeMass", ref DebyeMass);
         TryExtract(nameValuePairs, "Energy", ref Energy);
         TryExtract(nameValuePairs, "EnergyScale", ref EnergyScale);
         TryExtract(nameValuePairs, "EnergySteps", ref EnergySteps);
         TryExtract(nameValuePairs, "GammaDamp", ref GammaDamp);
         TryExtract(nameValuePairs, "GammaDiss", ref GammaDiss);
         TryExtract(nameValuePairs, "GammaTot", ref GammaTot);
         TryExtract(nameValuePairs, "MaxEnergy", ref MaxEnergy);
         TryExtract(nameValuePairs, "MaxRadius", ref MaxRadius);
         TryExtract(nameValuePairs, "MaxShootingTrials", ref MaxShootingTrials);
         TryExtract(nameValuePairs, "MinEnergy", ref MinEnergy);
         TryExtract(nameValuePairs, "MinRadius", ref MinRadius);
         TryExtract(nameValuePairs, "NumberExtrema", ref NumberExtrema);
         TryExtract(nameValuePairs, "PotentialType", ref PotentialType);
         TryExtract(nameValuePairs, "QuantumNumberL", ref QuantumNumberL);
         TryExtract(nameValuePairs, "QuantumNumberN", ref QuantumNumberN);
         TryExtract(nameValuePairs, "QuarkMass", ref QuarkMass);
         TryExtract(nameValuePairs, "RMS", ref RMS);
         TryExtract(nameValuePairs, "RunningCouplingType", ref RunningCouplingType);
         TryExtract(nameValuePairs, "RunningCouplingTypeSelection", ref RunningCouplingTypeSelection);
         TryExtract(nameValuePairs, "Samples", ref Samples);
         TryExtract(nameValuePairs, "Sigma", ref Sigma);
         TryExtract(nameValuePairs, "SigmaEff", ref SigmaEff);
         TryExtract(nameValuePairs, "SoftScale", ref SoftScale);
         TryExtract(nameValuePairs, "SpinCouplingRange", ref SpinCouplingRange);
         TryExtract(nameValuePairs, "SpinCouplingStrength", ref SpinCouplingStrength);
         TryExtract(nameValuePairs, "SpinState", ref SpinState);
         TryExtract(nameValuePairs, "StepNumber", ref StepNumber);
         TryExtract(nameValuePairs, "StepSize", ref StepSize);
         TryExtract(nameValuePairs, "StepsPerPeriod", ref StepsPerPeriod);
         TryExtract(nameValuePairs, "Tchem", ref Tchem);
         TryExtract(nameValuePairs, "Tcrit", ref Tcrit);
         TryExtract(nameValuePairs, "Temperature", ref Temperature);
         TryExtract(nameValuePairs, "Trials", ref Trials);
         TryExtract(nameValuePairs, "UltraSoftScale", ref UltraSoftScale);
         TryExtract(nameValuePairs, "UseFixedAlpha", ref UseFixedAlpha);
         TryExtract(nameValuePairs, "WaveVector", ref WaveVector);
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

      private List<RunningCouplingType> RunningCouplingTypeSelection = new List<RunningCouplingType>();

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