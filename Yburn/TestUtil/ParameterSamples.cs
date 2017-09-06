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
				nameValuePairs["DiffusenessA_fm"] = "3333";
				nameValuePairs["DiffusenessB_fm"] = "3333";
				nameValuePairs["EMFCalculationMethod"] = "URLimitFourierSynthesis";
				nameValuePairs["EMFQuadratureOrder"] = "3333";
				nameValuePairs["GridCellSize_fm"] = "3333";
				nameValuePairs["GridRadius_fm"] = "3333";
				nameValuePairs["ImpactParameter_fm"] = "3333";
				nameValuePairs["NuclearRadiusA_fm"] = "3333";
				nameValuePairs["NuclearRadiusB_fm"] = "3333";
				nameValuePairs["NucleonNumberA"] = "3333";
				nameValuePairs["NucleonNumberB"] = "3333";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["ParticleRapidity"] = "3333";
				nameValuePairs["ProtonNumberA"] = "3333";
				nameValuePairs["ProtonNumberB"] = "3333";
				nameValuePairs["QGPConductivity_MeV"] = "3333";
				nameValuePairs["RadialDistance_fm"] = "3333";
				nameValuePairs["NucleusShapeA"] = "GaussianDistribution";
				nameValuePairs["NucleusShapeB"] = "WoodsSaxonPotential";
				nameValuePairs["Samples"] = "3333";
				nameValuePairs["StartTime_fm"] = "3333";
				nameValuePairs["StopTime_fm"] = "3333";

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
				nameValuePairs["AggressivenessEnergy"] = "33";
				nameValuePairs["AlphaHard"] = "3333";
				nameValuePairs["AlphaSoft"] = "3333";
				nameValuePairs["AlphaThermal"] = "3333";
				nameValuePairs["AlphaUltraSoft"] = "3333";
				nameValuePairs["AvgInvDisplacement_per_fm"] = "3333";
				nameValuePairs["BoundMass_MeV"] = "3333";
				nameValuePairs["ColorState"] = "Octet";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["DebyeMass_MeV"] = "3333";
				nameValuePairs["StepSize"] = "3333";
				nameValuePairs["Energy_MeV"] = "3333";
				nameValuePairs["EnergyScale_MeV"] = "5555";
				nameValuePairs["EnergySteps"] = "3333";
				nameValuePairs["GammaDamp_MeV"] = "3333";
				nameValuePairs["GammaDiss_MeV"] = "3333";
				nameValuePairs["GammaTot_MeV"] = "3333";
				nameValuePairs["MaxEnergy_MeV"] = "3333";
				nameValuePairs["MaxRadius_fm"] = "3333";
				nameValuePairs["MinEnergy_MeV"] = "3333";
				nameValuePairs["MinRadius_fm"] = "33";
				nameValuePairs["MaxShootingTrials"] = "3333";
				nameValuePairs["QuantumNumberL"] = "3333";
				nameValuePairs["QuantumNumberN"] = "3333";
				nameValuePairs["NumberExtrema"] = "3333";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["PotentialType"] = "Real_NoString";
				nameValuePairs["QuarkMass_MeV"] = "3333";
				nameValuePairs["DisplacementRMS_fm"] = "3333";
				nameValuePairs["RunningCouplingType"] = "LOperturbative_Cutoff3";
				nameValuePairs["RunningCouplingTypeSelection"] = "1111";
				nameValuePairs["Samples"] = "4444";
				nameValuePairs["Sigma_MeV2"] = "3333";
				nameValuePairs["SigmaEff_MeV2"] = "3333";
				nameValuePairs["SoftScale_MeV"] = "3333";
				nameValuePairs["SpinCouplingStrength_MeV"] = "3333";
				nameValuePairs["SpinCouplingRange_fm"] = "3333";
				nameValuePairs["SpinState"] = "Triplet";
				nameValuePairs["StepNumber"] = "3333";
				nameValuePairs["StepsPerPeriod"] = "3333";
				nameValuePairs["Temperature_MeV"] = "3333";
				nameValuePairs["Tchem_MeV"] = "3333";
				nameValuePairs["Tcrit_MeV"] = "3333";
				nameValuePairs["Trials"] = "3333";
				nameValuePairs["UltraSoftScale_MeV"] = "3333";
				nameValuePairs["UseFixedAlpha"] = "True";
				nameValuePairs["WaveVector_per_fm"] = "3333";

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> QQonFireSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

				nameValuePairs["BjorkenLifeTime_fm"] = "3333";
				nameValuePairs["BottomiumStates"] = "Y1S,x3P";
				nameValuePairs["BreakupTemperature_MeV"] = "3333";
				nameValuePairs["CenterOfMassEnergy_TeV"] = "3333";
				nameValuePairs["CentralityBinBoundaries_percent"] = "3,3,3,3";
				nameValuePairs["DataFileName"] = "stdout.txt";
				nameValuePairs["DecayWidthType"] = "GammaTot";
				nameValuePairs["DiffusenessA_fm"] = "3333";
				nameValuePairs["DiffusenessB_fm"] = "3333";
				nameValuePairs["DimuonDecaysFrompp"] = "Y1S:3,x1P:3,Y2S:3,x2P:3,Y3S:3,x3P:3";
				nameValuePairs["DopplerShiftEvaluationType"] = "AveragedTemperature";
				nameValuePairs["EMFCalculationMethod"] = "DiffusionApproximation";
				nameValuePairs["EMFQuadratureOrder"] = "3333";
				nameValuePairs["EMFUpdateInterval_fm"] = "3333";
				nameValuePairs["ElectricDipoleAlignment"] = "Random";
				nameValuePairs["ExpansionMode"] = "Transverse";
				nameValuePairs["FireballFieldTypes"] = "Temperature";
				nameValuePairs["FormationTimes_fm"] = "Y1S:3,x1P:3,Y2S:3,x2P:3,Y3S:3,x3P:3";
				nameValuePairs["GridCellSize_fm"] = "3333";
				nameValuePairs["GridRadius_fm"] = "3333";
				nameValuePairs["ImpactParameter_fm"] = "3333";
				nameValuePairs["ImpactParamsAtBinBoundaries_fm"] = "3,3,3,3";
				nameValuePairs["InitialMaximumTemperature_MeV"] = "3333";
				nameValuePairs["LifeTime_fm"] = "3333";
				nameValuePairs["MeanParticipantsInBin"] = "3,3,3,3";
				nameValuePairs["NuclearRadiusA_fm"] = "3333";
				nameValuePairs["NuclearRadiusB_fm"] = "3333";
				nameValuePairs["NucleonNumberA"] = "3333";
				nameValuePairs["NucleonNumberB"] = "3333";
				nameValuePairs["NucleusShapeA"] = "GaussianDistribution";
				nameValuePairs["NucleusShapeB"] = "WoodsSaxonPotential";
				nameValuePairs["NumberAveragingAngles"] = "5";
				nameValuePairs["ParticipantsAtBinBoundaries"] = "3,3,3,3";
				nameValuePairs["PotentialTypes"] = "Tzero";
				nameValuePairs["ProtonNumberA"] = "3333";
				nameValuePairs["ProtonNumberB"] = "3333";
				nameValuePairs["QGPConductivity_MeV"] = "3333";
				nameValuePairs["QGPFormationTemperature_MeV"] = "3333";
				nameValuePairs["SnapRate_per_fm"] = "3333";
				nameValuePairs["TemperatureProfile"] = "Ncoll13";
				nameValuePairs["ThermalTime_fm"] = "3333";
				nameValuePairs["TransverseMomenta_GeV"] = "3,3,3,3";
				nameValuePairs["UseElectricField"] = "False";
				nameValuePairs["UseMagneticField"] = "False";

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
				nameValuePairs["DecayWidthType"] = "GammaTot";
				nameValuePairs["DopplerShiftEvaluationTypes"] = "AveragedDecayWidth";
				nameValuePairs["ElectricDipoleAlignment"] = "Random";
				nameValuePairs["ElectricFieldStrength_per_fm2"] = "3333";
				nameValuePairs["MagneticFieldStrength_per_fm2"] = "3333";
				nameValuePairs["MediumTemperatures_MeV"] = "100,200,300";
				nameValuePairs["MediumVelocities"] = "0.2";
				nameValuePairs["NumberAveragingAngles"] = "5";
				nameValuePairs["PotentialTypes"] = "Tzero";
				nameValuePairs["QGPFormationTemperature_MeV"] = "160";

				return nameValuePairs;
			}
		}
	}
}
