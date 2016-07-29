using System.Collections.Generic;
using Yburn.Fireball;

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

			Extractor.Store(nameValuePairs, "BeamRapidity", BeamRapidity);
			Extractor.Store(nameValuePairs, "BjorkenLifeTime", BjorkenLifeTime);
			Extractor.Store(nameValuePairs, "BottomiumStates", BottomiumStates);
			Extractor.Store(nameValuePairs, "CentralityBinBoundaries", CentralityBinBoundaries);
			Extractor.Store(nameValuePairs, "DataFileName", DataFileName);
			Extractor.Store(nameValuePairs, "DecayWidthAveragingAngles", DecayWidthAveragingAngles);
			Extractor.Store(nameValuePairs, "DecayWidthEvaluationType", DecayWidthEvaluationType);
			Extractor.Store(nameValuePairs, "DecayWidthType", DecayWidthType);
			Extractor.Store(nameValuePairs, "DiffusenessA", DiffusenessA);
			Extractor.Store(nameValuePairs, "DiffusenessB", DiffusenessB);
			Extractor.Store(nameValuePairs, "ExpansionMode", ExpansionMode);
			Extractor.Store(nameValuePairs, "FeedDown3P", FeedDown3P);
			Extractor.Store(nameValuePairs, "FireballFieldTypes", FireballFieldTypes);
			Extractor.Store(nameValuePairs, "FormationTimes", FormationTimes);
			Extractor.Store(nameValuePairs, "GridCellSize", GridCellSize);
			Extractor.Store(nameValuePairs, "GridRadius", GridRadius);
			Extractor.Store(nameValuePairs, "ImpactParameter", ImpactParameter);
			Extractor.Store(nameValuePairs, "ImpactParamsAtBinBoundaries", ImpactParamsAtBinBoundaries);
			Extractor.Store(nameValuePairs, "InitialMaximumTemperature", InitialMaximumTemperature);
			Extractor.Store(nameValuePairs, "LifeTime", LifeTime);
			Extractor.Store(nameValuePairs, "MeanParticipantsInBin", MeanParticipantsInBin);
			Extractor.Store(nameValuePairs, "BreakupTemperature", BreakupTemperature);
			Extractor.Store(nameValuePairs, "NuclearRadiusA", NuclearRadiusA);
			Extractor.Store(nameValuePairs, "NuclearRadiusB", NuclearRadiusB);
			Extractor.Store(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			Extractor.Store(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Extractor.Store(nameValuePairs, "ParticipantsAtBinBoundaries", ParticipantsAtBinBoundaries);
			Extractor.Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Extractor.Store(nameValuePairs, "ProtonProtonBaseline", ProtonProtonBaseline);
			Extractor.Store(nameValuePairs, "ShapeFunctionTypeA", ShapeFunctionTypeA);
			Extractor.Store(nameValuePairs, "ShapeFunctionTypeB", ShapeFunctionTypeB);
			Extractor.Store(nameValuePairs, "SnapRate", SnapRate);
			Extractor.Store(nameValuePairs, "TemperatureProfile", TemperatureProfile);
			Extractor.Store(nameValuePairs, "ThermalTime", ThermalTime);
			Extractor.Store(nameValuePairs, "TransverseMomenta", TransverseMomenta);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			Extractor.TryExtract(nameValuePairs, "BeamRapidity", ref BeamRapidity);
			Extractor.TryExtract(nameValuePairs, "BjorkenLifeTime", ref BjorkenLifeTime);
			Extractor.TryExtract(nameValuePairs, "BottomiumStates", ref BottomiumStates);
			Extractor.TryExtract(nameValuePairs, "CentralityBinBoundaries", ref CentralityBinBoundaries);
			Extractor.TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			Extractor.TryExtract(nameValuePairs, "DecayWidthAveragingAngles", ref DecayWidthAveragingAngles);
			Extractor.TryExtract(nameValuePairs, "DecayWidthEvaluationType", ref DecayWidthEvaluationType);
			Extractor.TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
			Extractor.TryExtract(nameValuePairs, "DiffusenessA", ref DiffusenessA);
			Extractor.TryExtract(nameValuePairs, "DiffusenessB", ref DiffusenessB);
			Extractor.TryExtract(nameValuePairs, "ExpansionMode", ref ExpansionMode);
			Extractor.TryExtract(nameValuePairs, "FeedDown3P", ref FeedDown3P);
			Extractor.TryExtract(nameValuePairs, "FireballFieldTypes", ref FireballFieldTypes);
			Extractor.TryExtract(nameValuePairs, "FormationTimes", ref FormationTimes);
			Extractor.TryExtract(nameValuePairs, "GridCellSize", ref GridCellSize);
			Extractor.TryExtract(nameValuePairs, "GridRadius", ref GridRadius);
			Extractor.TryExtract(nameValuePairs, "ImpactParameter", ref ImpactParameter);
			Extractor.TryExtract(nameValuePairs, "ImpactParamsAtBinBoundaries", ref ImpactParamsAtBinBoundaries);
			Extractor.TryExtract(nameValuePairs, "InitialMaximumTemperature", ref InitialMaximumTemperature);
			Extractor.TryExtract(nameValuePairs, "LifeTime", ref LifeTime);
			Extractor.TryExtract(nameValuePairs, "MeanParticipantsInBin", ref MeanParticipantsInBin);
			Extractor.TryExtract(nameValuePairs, "BreakupTemperature", ref BreakupTemperature);
			Extractor.TryExtract(nameValuePairs, "NuclearRadiusA", ref NuclearRadiusA);
			Extractor.TryExtract(nameValuePairs, "NuclearRadiusB", ref NuclearRadiusB);
			Extractor.TryExtract(nameValuePairs, "NucleonNumberA", ref NucleonNumberA);
			Extractor.TryExtract(nameValuePairs, "NucleonNumberB", ref NucleonNumberB);
			Extractor.TryExtract(nameValuePairs, "ParticipantsAtBinBoundaries", ref ParticipantsAtBinBoundaries);
			Extractor.TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			Extractor.TryExtract(nameValuePairs, "ProtonProtonBaseline", ref ProtonProtonBaseline);
			Extractor.TryExtract(nameValuePairs, "ShapeFunctionTypeA", ref ShapeFunctionTypeA);
			Extractor.TryExtract(nameValuePairs, "ShapeFunctionTypeB", ref ShapeFunctionTypeB);
			Extractor.TryExtract(nameValuePairs, "SnapRate", ref SnapRate);
			Extractor.TryExtract(nameValuePairs, "TemperatureProfile", ref TemperatureProfile);
			Extractor.TryExtract(nameValuePairs, "ThermalTime", ref ThermalTime);
			Extractor.TryExtract(nameValuePairs, "TransverseMomenta", ref TransverseMomenta);
		}

		private double BeamRapidity;

		private double BjorkenLifeTime;

		private string BottomiumStates = string.Empty;

		private int[][] CentralityBinBoundaries = new int[0][];

		private double[] DecayWidthAveragingAngles = new double[0];

		private DecayWidthEvaluationType DecayWidthEvaluationType;

		private DecayWidthType DecayWidthType;

		private double DiffusenessA;

		private double DiffusenessB;

		private ExpansionMode ExpansionMode;

		protected double FeedDown3P;

		private string[] FireballFieldTypes = new string[0];

		private double[] FormationTimes = new double[0];

		private double GridCellSize;

		private double GridRadius;

		private double ImpactParameter;

		private double[][] ImpactParamsAtBinBoundaries = new double[0][];

		private double InitialMaximumTemperature;

		private double LifeTime;

		private double[][] MeanParticipantsInBin = new double[0][];

		private double BreakupTemperature;

		private double NuclearRadiusA;

		private double NuclearRadiusB;

		private int NucleonNumberA;

		private int NucleonNumberB;

		private double[][] ParticipantsAtBinBoundaries = new double[0][];

		private string[] PotentialTypes = new string[0];

		protected ProtonProtonBaseline ProtonProtonBaseline;

		private ShapeFunctionType ShapeFunctionTypeA;

		private ShapeFunctionType ShapeFunctionTypeB;

		private double SnapRate;

		private TemperatureProfile TemperatureProfile;

		private double ThermalTime;

		private double[] TransverseMomenta = new double[0];
	}
}