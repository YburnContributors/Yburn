using System.Collections.Generic;

namespace Yburn.TestUtil
{
	public static class ParameterSamples
	{
		public static Dictionary<string, string> ElectromagnetismSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
				{
					["DiffusenessA_fm"] = "3333",
					["DiffusenessB_fm"] = "3333",
					["EMFCalculationMethod"] = "URLimitFourierSynthesis",
					["EMFQuadratureOrder"] = "3333",
					["GridCellSize_fm"] = "3333",
					["GridRadius_fm"] = "3333",
					["ImpactParameter_fm"] = "3333",
					["NuclearRadiusA_fm"] = "3333",
					["NuclearRadiusB_fm"] = "3333",
					["NucleonNumberA"] = "3333",
					["NucleonNumberB"] = "3333",
					["DataFileName"] = "stdout.txt",
					["ParticleRapidity"] = "3333",
					["ProtonNumberA"] = "3333",
					["ProtonNumberB"] = "3333",
					["QGPConductivity_MeV"] = "3333",
					["RadialDistance_fm"] = "3333",
					["NucleusShapeA"] = "GaussianDistribution",
					["NucleusShapeB"] = "WoodsSaxonPotential",
					["Samples"] = "3333",
					["StartTime_fm"] = "3333",
					["StopTime_fm"] = "3333"
				};

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> SingleQQSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
				{
					["AccuracyAlpha"] = "3333",
					["AccuracyWaveFunction"] = "3333",
					["AggressivenessAlpha"] = "3333",
					["AggressivenessEnergy"] = "33",
					["AlphaHard"] = "3333",
					["AlphaSoft"] = "3333",
					["AlphaThermal"] = "3333",
					["AlphaUltraSoft"] = "3333",
					["AvgInvDisplacement_per_fm"] = "3333",
					["BoundMass_MeV"] = "3333",
					["ColorState"] = "Octet",
					["DataFileName"] = "stdout.txt",
					["DebyeMass_MeV"] = "3333",
					["StepSize"] = "3333",
					["Energy_MeV"] = "3333",
					["EnergyScale_MeV"] = "5555",
					["EnergySteps"] = "3333",
					["GammaDamp_MeV"] = "3333",
					["GammaDiss_MeV"] = "3333",
					["GammaTot_MeV"] = "3333",
					["MaxEnergy_MeV"] = "3333",
					["MaxRadius_fm"] = "3333",
					["MinEnergy_MeV"] = "3333",
					["MinRadius_fm"] = "33",
					["MaxShootingTrials"] = "3333",
					["QuantumNumberL"] = "3333",
					["QuantumNumberN"] = "3333",
					["NumberExtrema"] = "3333",
					["DataFileName"] = "stdout.txt",
					["PotentialType"] = "Real_NoString",
					["QuarkMass_MeV"] = "3333",
					["DisplacementRMS_fm"] = "3333",
					["RunningCouplingType"] = "LOperturbative_Cutoff3",
					["RunningCouplingTypeSelection"] = "1111",
					["Samples"] = "4444",
					["Sigma_MeV2"] = "3333",
					["SigmaEff_MeV2"] = "3333",
					["SoftScale_MeV"] = "3333",
					["SpinCouplingStrength_MeV"] = "3333",
					["SpinCouplingRange_fm"] = "3333",
					["SpinState"] = "Triplet",
					["StepNumber"] = "3333",
					["StepsPerPeriod"] = "3333",
					["Temperature_MeV"] = "3333",
					["Tchem_MeV"] = "3333",
					["Tcrit_MeV"] = "3333",
					["Trials"] = "3333",
					["UltraSoftScale_MeV"] = "3333",
					["UseFixedAlpha"] = "True",
					["WaveVector_per_fm"] = "3333"
				};

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> QQonFireSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
				{
					["BjorkenLifeTime_fm"] = "3333",
					["BottomiumStates"] = "Y1S,x3P",
					["BreakupTemperature_MeV"] = "3333",
					["CenterOfMassEnergy_TeV"] = "3333",
					["CentralityBinBoundaries_percent"] = "3,3,3,3",
					["DataFileName"] = "stdout.txt",
					["DecayWidthType"] = "GammaTot",
					["DiffusenessA_fm"] = "3333",
					["DiffusenessB_fm"] = "3333",
					["DimuonDecaysFrompp"] = "Y1S:3,x1P:3,Y2S:3,x2P:3,Y3S:3,x3P:3",
					["DopplerShiftEvaluationType"] = "AveragedTemperature",
					["EMFCalculationMethod"] = "DiffusionApproximation",
					["EMFQuadratureOrder"] = "3333",
					["EMFUpdateInterval_fm"] = "3333",
					["ElectricDipoleAlignment"] = "Random",
					["ExpansionMode"] = "Transverse",
					["FireballFieldTypes"] = "Temperature",
					["FormationTimes_fm"] = "Y1S:3,x1P:3,Y2S:3,x2P:3,Y3S:3,x3P:3",
					["GridCellSize_fm"] = "3333",
					["GridRadius_fm"] = "3333",
					["ImpactParameter_fm"] = "3333",
					["ImpactParamsAtBinBoundaries_fm"] = "3,3,3,3",
					["InitialMaximumTemperature_MeV"] = "3333",
					["LifeTime_fm"] = "3333",
					["MeanParticipantsInBin"] = "3,3,3,3",
					["NuclearRadiusA_fm"] = "3333",
					["NuclearRadiusB_fm"] = "3333",
					["NucleonNumberA"] = "3333",
					["NucleonNumberB"] = "3333",
					["NucleusShapeA"] = "GaussianDistribution",
					["NucleusShapeB"] = "WoodsSaxonPotential",
					["NumberAveragingAngles"] = "5",
					["ParticipantsAtBinBoundaries"] = "3,3,3,3",
					["PotentialTypes"] = "Tzero",
					["ProtonNumberA"] = "3333",
					["ProtonNumberB"] = "3333",
					["QGPConductivity_MeV"] = "3333",
					["QGPFormationTemperature_MeV"] = "3333",
					["SnapRate_per_fm"] = "3333",
					["TemperatureProfile"] = "Ncoll13",
					["ThermalTime_fm"] = "3333",
					["TransverseMomenta_GeV"] = "3,3,3,3",
					["UseElectricField"] = "False",
					["UseMagneticField"] = "False"
				};

				return nameValuePairs;
			}
		}

		public static Dictionary<string, string> InMediumDecayWidthSamples
		{
			get
			{
				Dictionary<string, string> nameValuePairs = new Dictionary<string, string>
				{
					["BottomiumStates"] = "Y1S,x3P",
					["DataFileName"] = "stdout.txt",
					["DecayWidthType"] = "GammaTot",
					["DopplerShiftEvaluationTypes"] = "AveragedDecayWidth",
					["ElectricDipoleAlignment"] = "Random",
					["ElectricFieldStrength_per_fm2"] = "3333",
					["MagneticFieldStrength_per_fm2"] = "3333",
					["MediumTemperatures_MeV"] = "100,200,300",
					["MediumVelocities"] = "0.2",
					["NumberAveragingAngles"] = "5",
					["PotentialTypes"] = "Tzero",
					["QGPFormationTemperature_MeV"] = "160"
				};

				return nameValuePairs;
			}
		}
	}
}
