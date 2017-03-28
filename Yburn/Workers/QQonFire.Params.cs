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

			Store(nameValuePairs, "BjorkenLifeTime", BjorkenLifeTime);
			Store(nameValuePairs, "BottomiumStates", BottomiumStates);
			Store(nameValuePairs, "BreakupTemperature", BreakupTemperature);
			Store(nameValuePairs, "CenterOfMassEnergy", CenterOfMassEnergy);
			Store(nameValuePairs, "CentralityBinBoundaries", CentralityBinBoundaries);
			Store(nameValuePairs, "DataFileName", DataFileName);
			Store(nameValuePairs, "DecayWidthType", DecayWidthType);
			Store(nameValuePairs, "DiffusenessA", DiffusenessA);
			Store(nameValuePairs, "DiffusenessB", DiffusenessB);
			Store(nameValuePairs, "DimuonDecaysFrompp", DimuonDecaysFrompp);
			Store(nameValuePairs, "DopplerShiftEvaluationType", DopplerShiftEvaluationType);
			Store(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			Store(nameValuePairs, "EMFQuadratureOrder", EMFQuadratureOrder);
			Store(nameValuePairs, "EMFUpdateInterval", EMFUpdateInterval);
			Store(nameValuePairs, "ElectricDipoleAlignment", ElectricDipoleAlignment);
			Store(nameValuePairs, "ExpansionMode", ExpansionMode);
			Store(nameValuePairs, "FireballFieldTypes", FireballFieldTypes);
			Store(nameValuePairs, "FormationTimes", FormationTimes);
			Store(nameValuePairs, "GridCellSize", GridCellSize);
			Store(nameValuePairs, "GridRadius", GridRadius);
			Store(nameValuePairs, "ImpactParameter", ImpactParameter);
			Store(nameValuePairs, "ImpactParamsAtBinBoundaries", ImpactParamsAtBinBoundaries);
			Store(nameValuePairs, "InitialMaximumTemperature", InitialMaximumTemperature);
			Store(nameValuePairs, "LifeTime", LifeTime);
			Store(nameValuePairs, "MeanParticipantsInBin", MeanParticipantsInBin);
			Store(nameValuePairs, "NuclearRadiusA", NuclearRadiusA);
			Store(nameValuePairs, "NuclearRadiusB", NuclearRadiusB);
			Store(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			Store(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Store(nameValuePairs, "NucleusShapeA", NucleusShapeA);
			Store(nameValuePairs, "NucleusShapeB", NucleusShapeB);
			Store(nameValuePairs, "NumberAveragingAngles", NumberAveragingAngles);
			Store(nameValuePairs, "ParticipantsAtBinBoundaries", ParticipantsAtBinBoundaries);
			Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Store(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			Store(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			Store(nameValuePairs, "QGPConductivity", QGPConductivity);
			Store(nameValuePairs, "QGPFormationTemperature", QGPFormationTemperature);
			Store(nameValuePairs, "SnapRate", SnapRate);
			Store(nameValuePairs, "TemperatureProfile", TemperatureProfile);
			Store(nameValuePairs, "ThermalTime", ThermalTime);
			Store(nameValuePairs, "TransverseMomenta", TransverseMomenta);
			Store(nameValuePairs, "UseElectricField", UseElectricField);
			Store(nameValuePairs, "UseMagneticField", UseMagneticField);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			TryExtract(nameValuePairs, "BjorkenLifeTime", ref BjorkenLifeTime);
			TryExtract(nameValuePairs, "BottomiumStates", ref BottomiumStates);
			TryExtract(nameValuePairs, "BreakupTemperature", ref BreakupTemperature);
			TryExtract(nameValuePairs, "CenterOfMassEnergy", ref CenterOfMassEnergy);
			TryExtract(nameValuePairs, "CentralityBinBoundaries", ref CentralityBinBoundaries);
			TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
			TryExtract(nameValuePairs, "DiffusenessA", ref DiffusenessA);
			TryExtract(nameValuePairs, "DiffusenessB", ref DiffusenessB);
			TryExtract(nameValuePairs, "DimuonDecaysFrompp", ref DimuonDecaysFrompp);
			TryExtract(nameValuePairs, "DopplerShiftEvaluationType", ref DopplerShiftEvaluationType);
			TryExtract(nameValuePairs, "EMFCalculationMethod", ref EMFCalculationMethod);
			TryExtract(nameValuePairs, "EMFQuadratureOrder", ref EMFQuadratureOrder);
			TryExtract(nameValuePairs, "EMFUpdateInterval", ref EMFUpdateInterval);
			TryExtract(nameValuePairs, "ElectricDipoleAlignment", ref ElectricDipoleAlignment);
			TryExtract(nameValuePairs, "ExpansionMode", ref ExpansionMode);
			TryExtract(nameValuePairs, "FireballFieldTypes", ref FireballFieldTypes);
			TryExtract(nameValuePairs, "FormationTimes", ref FormationTimes);
			TryExtract(nameValuePairs, "GridCellSize", ref GridCellSize);
			TryExtract(nameValuePairs, "GridRadius", ref GridRadius);
			TryExtract(nameValuePairs, "ImpactParameter", ref ImpactParameter);
			TryExtract(nameValuePairs, "ImpactParamsAtBinBoundaries", ref ImpactParamsAtBinBoundaries);
			TryExtract(nameValuePairs, "InitialMaximumTemperature", ref InitialMaximumTemperature);
			TryExtract(nameValuePairs, "LifeTime", ref LifeTime);
			TryExtract(nameValuePairs, "MeanParticipantsInBin", ref MeanParticipantsInBin);
			TryExtract(nameValuePairs, "NuclearRadiusA", ref NuclearRadiusA);
			TryExtract(nameValuePairs, "NuclearRadiusB", ref NuclearRadiusB);
			TryExtract(nameValuePairs, "NucleonNumberA", ref NucleonNumberA);
			TryExtract(nameValuePairs, "NucleonNumberB", ref NucleonNumberB);
			TryExtract(nameValuePairs, "NucleusShapeA", ref NucleusShapeA);
			TryExtract(nameValuePairs, "NucleusShapeB", ref NucleusShapeB);
			TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			TryExtract(nameValuePairs, "ParticipantsAtBinBoundaries", ref ParticipantsAtBinBoundaries);
			TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			TryExtract(nameValuePairs, "ProtonNumberA", ref ProtonNumberA);
			TryExtract(nameValuePairs, "ProtonNumberB", ref ProtonNumberB);
			TryExtract(nameValuePairs, "QGPConductivity", ref QGPConductivity);
			TryExtract(nameValuePairs, "QGPFormationTemperature", ref QGPFormationTemperature);
			TryExtract(nameValuePairs, "SnapRate", ref SnapRate);
			TryExtract(nameValuePairs, "TemperatureProfile", ref TemperatureProfile);
			TryExtract(nameValuePairs, "ThermalTime", ref ThermalTime);
			TryExtract(nameValuePairs, "TransverseMomenta", ref TransverseMomenta);
			TryExtract(nameValuePairs, "UseElectricField", ref UseElectricField);
			TryExtract(nameValuePairs, "UseMagneticField", ref UseMagneticField);
		}

		private double BjorkenLifeTime;

		private List<BottomiumState> BottomiumStates;

		private double CenterOfMassEnergy;

		private List<List<int>> CentralityBinBoundaries;

		private DopplerShiftEvaluationType DopplerShiftEvaluationType;

		private DecayWidthType DecayWidthType;

		private double DiffusenessA;

		private double DiffusenessB;

		private Dictionary<BottomiumState, double> DimuonDecaysFrompp;

		private ElectricDipoleAlignment ElectricDipoleAlignment;

		private EMFCalculationMethod EMFCalculationMethod;

		private double EMFUpdateInterval;

		private int EMFQuadratureOrder;

		private ExpansionMode ExpansionMode;

		private List<string> FireballFieldTypes;

		private Dictionary<BottomiumState, double> FormationTimes;

		private double GridCellSize;

		private double GridRadius;

		private double ImpactParameter;

		private List<List<double>> ImpactParamsAtBinBoundaries;

		private double InitialMaximumTemperature;

		private double LifeTime;

		private List<List<double>> MeanParticipantsInBin;

		private int NumberAveragingAngles;

		private double BreakupTemperature;

		private double NuclearRadiusA;

		private double NuclearRadiusB;

		private uint NucleonNumberA;

		private uint NucleonNumberB;

		private List<List<double>> ParticipantsAtBinBoundaries;

		private List<PotentialType> PotentialTypes;

		private uint ProtonNumberA;

		private uint ProtonNumberB;

		private double QGPFormationTemperature;

		private NucleusShape NucleusShapeA;

		private NucleusShape NucleusShapeB;

		private double QGPConductivity;

		private double SnapRate;

		private TemperatureProfile TemperatureProfile;

		private double ThermalTime;

		private List<double> TransverseMomenta;

		private bool UseElectricField;

		private bool UseMagneticField;
	}
}