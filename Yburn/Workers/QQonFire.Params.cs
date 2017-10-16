using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.QQState;

namespace Yburn.Workers
{
	partial class QQonFire
	{
		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			Store(nameValuePairs, "BjorkenLifeTime_fm", BjorkenLifeTime_fm);
			Store(nameValuePairs, "BottomiumStates", BottomiumStates);
			Store(nameValuePairs, "BreakupTemperature_MeV", BreakupTemperature_MeV);
			Store(nameValuePairs, "CenterOfMassEnergy_TeV", CenterOfMassEnergy_TeV);
			Store(nameValuePairs, "CentralityBinBoundaries_percent", CentralityBinBoundaries_percent);
			Store(nameValuePairs, "DataFileName", DataFileName);
			Store(nameValuePairs, "DecayWidthType", DecayWidthType);
			Store(nameValuePairs, "DiffusenessA_fm", DiffusenessA_fm);
			Store(nameValuePairs, "DiffusenessB_fm", DiffusenessB_fm);
			Store(nameValuePairs, "DimuonDecaysFrompp", DimuonDecaysFrompp);
			Store(nameValuePairs, "DopplerShiftEvaluationType", DopplerShiftEvaluationType);
			Store(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			Store(nameValuePairs, "EMFQuadratureOrder", EMFQuadratureOrder);
			Store(nameValuePairs, "EMFUpdateInterval_fm", EMFUpdateInterval_fm);
			Store(nameValuePairs, "ElectricDipoleAlignment", ElectricDipoleAlignment);
			Store(nameValuePairs, "ExpansionMode", ExpansionMode);
			Store(nameValuePairs, "FireballFieldTypes", FireballFieldTypes);
			Store(nameValuePairs, "FormationTimes_fm", FormationTimes_fm);
			Store(nameValuePairs, "GridCellSize_fm", GridCellSize_fm);
			Store(nameValuePairs, "GridRadius_fm", GridRadius_fm);
			Store(nameValuePairs, "ImpactParameter_fm", ImpactParameter_fm);
			Store(nameValuePairs, "ImpactParamsAtBinBoundaries_fm", ImpactParamsAtBinBoundaries_fm);
			Store(nameValuePairs, "InitialMaximumTemperature_MeV", InitialMaximumTemperature_MeV);
			Store(nameValuePairs, "LifeTime_fm", LifeTime_fm);
			Store(nameValuePairs, "MeanParticipantsInBin", MeanParticipantsInBin);
			Store(nameValuePairs, "NuclearRadiusA_fm", NuclearRadiusA_fm);
			Store(nameValuePairs, "NuclearRadiusB_fm", NuclearRadiusB_fm);
			Store(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			Store(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Store(nameValuePairs, "NucleusShapeA", NucleusShapeA);
			Store(nameValuePairs, "NucleusShapeB", NucleusShapeB);
			Store(nameValuePairs, "NumberAveragingAngles", NumberAveragingAngles);
			Store(nameValuePairs, "ParticipantsAtBinBoundaries", ParticipantsAtBinBoundaries);
			Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Store(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			Store(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			Store(nameValuePairs, "QGPConductivity_MeV", QGPConductivity_MeV);
			Store(nameValuePairs, "QGPFormationTemperature_MeV", QGPFormationTemperature_MeV);
			Store(nameValuePairs, "SnapRate_per_fm", SnapRate_per_fm);
			Store(nameValuePairs, "TemperatureProfile", TemperatureProfile);
			Store(nameValuePairs, "ThermalTime_fm", ThermalTime_fm);
			Store(nameValuePairs, "TransverseMomenta_GeV", TransverseMomenta_GeV);
			Store(nameValuePairs, "UseElectricField", UseElectricField);
			Store(nameValuePairs, "UseMagneticField", UseMagneticField);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			TryExtract(nameValuePairs, "BjorkenLifeTime_fm", ref BjorkenLifeTime_fm);
			TryExtract(nameValuePairs, "BottomiumStates", ref BottomiumStates);
			TryExtract(nameValuePairs, "BreakupTemperature_MeV", ref BreakupTemperature_MeV);
			TryExtract(nameValuePairs, "CenterOfMassEnergy_TeV", ref CenterOfMassEnergy_TeV);
			TryExtract(nameValuePairs, "CentralityBinBoundaries_percent", ref CentralityBinBoundaries_percent);
			TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
			TryExtract(nameValuePairs, "DiffusenessA_fm", ref DiffusenessA_fm);
			TryExtract(nameValuePairs, "DiffusenessB_fm", ref DiffusenessB_fm);
			TryExtract(nameValuePairs, "DimuonDecaysFrompp", ref DimuonDecaysFrompp);
			TryExtract(nameValuePairs, "DopplerShiftEvaluationType", ref DopplerShiftEvaluationType);
			TryExtract(nameValuePairs, "EMFCalculationMethod", ref EMFCalculationMethod);
			TryExtract(nameValuePairs, "EMFQuadratureOrder", ref EMFQuadratureOrder);
			TryExtract(nameValuePairs, "EMFUpdateInterval_fm", ref EMFUpdateInterval_fm);
			TryExtract(nameValuePairs, "ElectricDipoleAlignment", ref ElectricDipoleAlignment);
			TryExtract(nameValuePairs, "ExpansionMode", ref ExpansionMode);
			TryExtract(nameValuePairs, "FireballFieldTypes", ref FireballFieldTypes);
			TryExtract(nameValuePairs, "FormationTimes_fm", ref FormationTimes_fm);
			TryExtract(nameValuePairs, "GridCellSize_fm", ref GridCellSize_fm);
			TryExtract(nameValuePairs, "GridRadius_fm", ref GridRadius_fm);
			TryExtract(nameValuePairs, "ImpactParameter_fm", ref ImpactParameter_fm);
			TryExtract(nameValuePairs, "ImpactParamsAtBinBoundaries_fm", ref ImpactParamsAtBinBoundaries_fm);
			TryExtract(nameValuePairs, "InitialMaximumTemperature_MeV", ref InitialMaximumTemperature_MeV);
			TryExtract(nameValuePairs, "LifeTime_fm", ref LifeTime_fm);
			TryExtract(nameValuePairs, "MeanParticipantsInBin", ref MeanParticipantsInBin);
			TryExtract(nameValuePairs, "NuclearRadiusA_fm", ref NuclearRadiusA_fm);
			TryExtract(nameValuePairs, "NuclearRadiusB_fm", ref NuclearRadiusB_fm);
			TryExtract(nameValuePairs, "NucleonNumberA", ref NucleonNumberA);
			TryExtract(nameValuePairs, "NucleonNumberB", ref NucleonNumberB);
			TryExtract(nameValuePairs, "NucleusShapeA", ref NucleusShapeA);
			TryExtract(nameValuePairs, "NucleusShapeB", ref NucleusShapeB);
			TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			TryExtract(nameValuePairs, "ParticipantsAtBinBoundaries", ref ParticipantsAtBinBoundaries);
			TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			TryExtract(nameValuePairs, "ProtonNumberA", ref ProtonNumberA);
			TryExtract(nameValuePairs, "ProtonNumberB", ref ProtonNumberB);
			TryExtract(nameValuePairs, "QGPConductivity_MeV", ref QGPConductivity_MeV);
			TryExtract(nameValuePairs, "QGPFormationTemperature_MeV", ref QGPFormationTemperature_MeV);
			TryExtract(nameValuePairs, "SnapRate_per_fm", ref SnapRate_per_fm);
			TryExtract(nameValuePairs, "TemperatureProfile", ref TemperatureProfile);
			TryExtract(nameValuePairs, "ThermalTime_fm", ref ThermalTime_fm);
			TryExtract(nameValuePairs, "TransverseMomenta_GeV", ref TransverseMomenta_GeV);
			TryExtract(nameValuePairs, "UseElectricField", ref UseElectricField);
			TryExtract(nameValuePairs, "UseMagneticField", ref UseMagneticField);
		}

		private double BjorkenLifeTime_fm;

		private List<BottomiumState> BottomiumStates;

		private double CenterOfMassEnergy_TeV;

		private List<List<int>> CentralityBinBoundaries_percent;

		private DopplerShiftEvaluationType DopplerShiftEvaluationType;

		private DecayWidthType DecayWidthType;

		private double DiffusenessA_fm;

		private double DiffusenessB_fm;

		private Dictionary<BottomiumState, double> DimuonDecaysFrompp;

		private ElectricDipoleAlignment ElectricDipoleAlignment;

		private EMFCalculationMethod EMFCalculationMethod;

		private double EMFUpdateInterval_fm;

		private int EMFQuadratureOrder;

		private ExpansionMode ExpansionMode;

		private List<FireballFieldType> FireballFieldTypes;

		private Dictionary<BottomiumState, double> FormationTimes_fm;

		private double GridCellSize_fm;

		private double GridRadius_fm;

		private double ImpactParameter_fm;

		private List<List<double>> ImpactParamsAtBinBoundaries_fm;

		private double InitialMaximumTemperature_MeV;

		private double LifeTime_fm;

		private List<List<double>> MeanParticipantsInBin;

		private int NumberAveragingAngles;

		private double BreakupTemperature_MeV;

		private double NuclearRadiusA_fm;

		private double NuclearRadiusB_fm;

		private uint NucleonNumberA;

		private uint NucleonNumberB;

		private List<List<double>> ParticipantsAtBinBoundaries;

		private List<PotentialType> PotentialTypes;

		private uint ProtonNumberA;

		private uint ProtonNumberB;

		private double QGPFormationTemperature_MeV;

		private NucleusShape NucleusShapeA;

		private NucleusShape NucleusShapeB;

		private double QGPConductivity_MeV;

		private double SnapRate_per_fm;

		private TemperatureProfile TemperatureProfile;

		private double ThermalTime_fm;

		private List<double> TransverseMomenta_GeV;

		private bool UseElectricField;

		private bool UseMagneticField;
	}
}
