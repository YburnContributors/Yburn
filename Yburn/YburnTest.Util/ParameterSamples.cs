using System.Collections.Generic;

namespace Yburn.Tests.Util
{
	public static class ParameterSamples
	{
		public static Dictionary<string, string> ElectromagnetismSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
				nameValuePairs["DiffusenessA"] = "3333";
				nameValuePairs["DiffusenessB"] = "3333";
				nameValuePairs["EMFCalculationMethod"] = "URLimitFourierSynthesis";
				nameValuePairs["EMFCalculationMethodSelection"] = "URLimitFourierSynthesis";
				nameValuePairs["EffectiveTimeSamples"] = "3333";
				nameValuePairs["GridCellSize"] = "3333";
				nameValuePairs["GridRadius"] = "3333";
				nameValuePairs["ImpactParam"] = "3333";
				nameValuePairs["NuclearRadiusA"] = "3333";
				nameValuePairs["NuclearRadiusB"] = "3333";
				nameValuePairs["NucleonNumberA"] = "3333";
				nameValuePairs["NucleonNumberB"] = "3333";
				nameValuePairs["Outfile"] = "stdout.txt";
				nameValuePairs["PointChargeVelocity"] = "3333";
				nameValuePairs["ProtonNumberA"] = "3333";
				nameValuePairs["ProtonNumberB"] = "3333";
				nameValuePairs["QGPConductivityMeV"] = "3333";
				nameValuePairs["RadialDistance"] = "3333";
				nameValuePairs["ShapeFunctionTypeA"] = "GaussianDistribution";
				nameValuePairs["ShapeFunctionTypeB"] = "WoodsSaxonPotential";
				nameValuePairs["StartEffectiveTime"] = "3333";
				nameValuePairs["StopEffectiveTime"] = "3333";

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> SingleQQSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

				nameValuePairs["AccuracyAlpha"] = "3333";
				nameValuePairs["AccuracyWaveFunction"] = "3333";
				nameValuePairs["AggressivenessAlpha"] = "3333";
				nameValuePairs["AlphaHard"] = "3333";
				nameValuePairs["AlphaSoft"] = "3333";
				nameValuePairs["AlphaThermal"] = "3333";
				nameValuePairs["AlphaUltraSoft"] = "3333";
				nameValuePairs["AvInvRadius"] = "3333";
				nameValuePairs["BoundMass"] = "3333";
				nameValuePairs["ColorState"] = "Octet";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["DebyeMass"] = "3333";
				nameValuePairs["StepSize"] = "3333";
				nameValuePairs["Energy"] = "3333";
				nameValuePairs["EnergyScale"] = "5555";
				nameValuePairs["EnergySteps"] = "3333";
				nameValuePairs["GammaDamp"] = "3333";
				nameValuePairs["GammaDiss"] = "3333";
				nameValuePairs["GammaTot"] = "3333";
				nameValuePairs["MaxEnergy"] = "3333";
				nameValuePairs["MaxRadius"] = "3333";
				nameValuePairs["MinEnergy"] = "3333";
				nameValuePairs["MinRadius"] = "33";
				nameValuePairs["MaxShootingTrials"] = "3333";
				nameValuePairs["QuantumNumberL"] = "3333";
				nameValuePairs["QuantumNumberN"] = "3333";
				nameValuePairs["NumberExtrema"] = "3333";
				nameValuePairs["Outfile"] = "stdout.txt";
				nameValuePairs["PotentialType"] = "Real_NoString";
				nameValuePairs["QuarkMass"] = "3333";
				nameValuePairs["RMS"] = "3333";
				nameValuePairs["RunningCouplingType"] = "LOperturbative_Cutoff3";
				nameValuePairs["RunningCouplingTypeSelection"] = "1111";
				nameValuePairs["Samples"] = "4444";
				nameValuePairs["Sigma"] = "3333";
				nameValuePairs["SigmaEff"] = "3333";
				nameValuePairs["SoftScale"] = "3333";
				nameValuePairs["SpinCouplingStrength"] = "3333";
				nameValuePairs["SpinCouplingRange"] = "3333";
				nameValuePairs["SpinState"] = "Triplet";
				nameValuePairs["StepNumber"] = "3333";
				nameValuePairs["StepsPerPeriod"] = "3333";
				nameValuePairs["Temperature"] = "3333";
				nameValuePairs["Tchem"] = "3333";
				nameValuePairs["Tcrit"] = "3333";
				nameValuePairs["Trials"] = "3333";
				nameValuePairs["UltraSoftScale"] = "3333";
				nameValuePairs["UseFixedAlpha"] = "True";
				nameValuePairs["WaveVector"] = "3333";

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> QQonFireSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

				nameValuePairs["ExpansionMode"] = "Transverse";
				nameValuePairs["DecayWidthType"] = "GammaTot";
				nameValuePairs["DecayWidthAveragingAngles"] = "10,20,30";
				nameValuePairs["TemperatureProfile"] = "Ncoll13";
				nameValuePairs["ProtonProtonBaseline"] = "CMS2012";
				nameValuePairs["DiffusenessA"] = "3333";
				nameValuePairs["DiffusenessB"] = "3333";
				nameValuePairs["FireballFieldTypes"] = "Temperature";
				nameValuePairs["ImpactParam"] = "3333";
				nameValuePairs["FeedDown3P"] = "3333";
				nameValuePairs["NucleonNumberA"] = "3333";
				nameValuePairs["NucleonNumberB"] = "3333";
				nameValuePairs["Outfile"] = "stdout.txt";
				nameValuePairs["PotentialTypes"] = "Tzero";
				nameValuePairs["TransverseMomenta"] = "3,3,3,3";
				nameValuePairs["NuclearRadiusA"] = "3333";
				nameValuePairs["NuclearRadiusB"] = "3333";
				nameValuePairs["SnapRate"] = "3333";
				nameValuePairs["CentralityBinBoundaries"] = "3,3,3";
				nameValuePairs["ImpactParamsAtBinBoundaries"] = "3,3,3";
				nameValuePairs["ParticipantsAtBinBoundaries"] = "3,3,3";
				nameValuePairs["MeanParticipantsInBin"] = "3,3,3";
				nameValuePairs["BottomiumStates"] = "Y1S,x3P";
				nameValuePairs["InitialCentralTemperature"] = "3333";
				nameValuePairs["MinimalCentralTemperature"] = "3333";
				nameValuePairs["FormationTimes"] = "3,3,3,3,3,3";
				nameValuePairs["ThermalTime"] = "3333";
				nameValuePairs["GridCellSize"] = "3333";
				nameValuePairs["GridRadius"] = "3333";
				nameValuePairs["BeamRapidity"] = "3333";
				nameValuePairs["BjorkenLifeTime"] = "3333";
				nameValuePairs["LifeTime"] = "3333";
				nameValuePairs["DecayWidthEvaluationType"] = "AveragedTemperature";
				nameValuePairs["ShapeFunctionTypeA"] = "GaussianDistribution";
				nameValuePairs["ShapeFunctionTypeB"] = "WoodsSaxonPotential";

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> InMediumDecayWidthSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

				nameValuePairs["MinTemperature"] = "100.5";
				nameValuePairs["MaxTemperature"] = "310";
				nameValuePairs["BottomiumStates"] = "Y1S,x3P";
				nameValuePairs["TemperatureStepSize"] = "6.5";
				nameValuePairs["DecayWidthType"] = "GammaTot";
				nameValuePairs["DecayWidthAveragingAngles"] = "10,20,30";
				nameValuePairs["MediumVelocity"] = "0.2";
				nameValuePairs["PotentialTypes"] = "Tzero";
				nameValuePairs["UseAveragedTemperature"] = "True";
				nameValuePairs["Outfile"] = "stdout.txt";

				return nameValuePairs;
			}
		}
	}
}