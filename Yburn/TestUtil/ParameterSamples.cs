using System.Collections.Generic;

namespace Yburn.TestUtil
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
				nameValuePairs["GridCellSize"] = "3333";
				nameValuePairs["GridRadius"] = "3333";
				nameValuePairs["ImpactParameter"] = "3333";
				nameValuePairs["NuclearRadiusA"] = "3333";
				nameValuePairs["NuclearRadiusB"] = "3333";
				nameValuePairs["NucleonNumberA"] = "3333";
				nameValuePairs["NucleonNumberB"] = "3333";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["PointChargeRapidity"] = "3333";
				nameValuePairs["ProtonNumberA"] = "3333";
				nameValuePairs["ProtonNumberB"] = "3333";
				nameValuePairs["QGPConductivityMeV"] = "3333";
				nameValuePairs["QuadratureOrder"] = "3333";
				nameValuePairs["RadialDistance"] = "3333";
				nameValuePairs["NucleusShapeA"] = "GaussianDistribution";
				nameValuePairs["NucleusShapeB"] = "WoodsSaxonPotential";
				nameValuePairs["Samples"] = "3333";
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
				nameValuePairs["DataFileName"] = "stdout.txt";
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

				nameValuePairs["BeamRapidity"] = "3333";
				nameValuePairs["BjorkenLifeTime"] = "3333";
				nameValuePairs["BottomiumStates"] = "Y1S,x3P";
				nameValuePairs["BreakupTemperature"] = "3333";
				nameValuePairs["CentralityBinBoundaries"] = "3,3,3";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["DecayWidthEvaluationType"] = "AveragedTemperature";
				nameValuePairs["DecayWidthType"] = "GammaTot";
				nameValuePairs["DiffusenessA"] = "3333";
				nameValuePairs["DiffusenessB"] = "3333";
				nameValuePairs["ExpansionMode"] = "Transverse";
				nameValuePairs["FeedDown3P"] = "3333";
				nameValuePairs["FireballFieldTypes"] = "Temperature";
				nameValuePairs["FormationTimes"] = "3,3,3,3,3,3";
				nameValuePairs["GridCellSize"] = "3333";
				nameValuePairs["GridRadius"] = "3333";
				nameValuePairs["ImpactParameter"] = "3333";
				nameValuePairs["ImpactParamsAtBinBoundaries"] = "3,3,3";
				nameValuePairs["InitialMaximumTemperature"] = "3333";
				nameValuePairs["LifeTime"] = "3333";
				nameValuePairs["MeanParticipantsInBin"] = "3,3,3";
				nameValuePairs["NuclearRadiusA"] = "3333";
				nameValuePairs["NuclearRadiusB"] = "3333";
				nameValuePairs["NucleonNumberA"] = "3333";
				nameValuePairs["NucleonNumberB"] = "3333";
				nameValuePairs["NumberAveragingAngles"] = "5";
				nameValuePairs["ParticipantsAtBinBoundaries"] = "3,3,3";
				nameValuePairs["PotentialTypes"] = "Tzero";
				nameValuePairs["ProtonNumberA"] = "3333";
				nameValuePairs["ProtonNumberB"] = "3333";
				nameValuePairs["ProtonProtonBaseline"] = "CMS2012";
				nameValuePairs["QGPFormationTemperature"] = "3333";
				nameValuePairs["NucleusShapeA"] = "GaussianDistribution";
				nameValuePairs["NucleusShapeB"] = "WoodsSaxonPotential";
				nameValuePairs["SnapRate"] = "3333";
				nameValuePairs["TemperatureProfile"] = "Ncoll13";
				nameValuePairs["ThermalTime"] = "3333";
				nameValuePairs["TransverseMomenta"] = "3,3,3,3";

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> InMediumDecayWidthSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

				nameValuePairs["BottomiumStates"] = "Y1S,x3P";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["DecayWidthEvaluationTypes"] = "AveragedDecayWidth";
				nameValuePairs["DecayWidthType"] = "GammaTot";
				nameValuePairs["MediumTemperatures"] = "100,200,300";
				nameValuePairs["MediumVelocities"] = "0.2";
				nameValuePairs["NumberAveragingAngles"] = "5";
				nameValuePairs["PotentialTypes"] = "Tzero";
				nameValuePairs["QGPFormationTemperature"] = "160";

				return nameValuePairs;
			}
		}
	}
}