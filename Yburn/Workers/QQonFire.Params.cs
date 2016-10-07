﻿using System.Collections.Generic;
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

			Store(nameValuePairs, "BeamRapidity", BeamRapidity);
			Store(nameValuePairs, "BjorkenLifeTime", BjorkenLifeTime);
			Store(nameValuePairs, "BottomiumStates", BottomiumStates);
			Store(nameValuePairs, "CentralityBinBoundaries", CentralityBinBoundaries);
			Store(nameValuePairs, "DataFileName", DataFileName);
			Store(nameValuePairs, "DecayWidthEvaluationType", DecayWidthEvaluationType);
			Store(nameValuePairs, "DecayWidthType", DecayWidthType);
			Store(nameValuePairs, "DiffusenessA", DiffusenessA);
			Store(nameValuePairs, "DiffusenessB", DiffusenessB);
			Store(nameValuePairs, "ExpansionMode", ExpansionMode);
			Store(nameValuePairs, "FeedDown3P", FeedDown3P);
			Store(nameValuePairs, "FireballFieldTypes", FireballFieldTypes);
			Store(nameValuePairs, "FormationTimes", FormationTimes);
			Store(nameValuePairs, "GridCellSize", GridCellSize);
			Store(nameValuePairs, "GridRadius", GridRadius);
			Store(nameValuePairs, "ImpactParameter", ImpactParameter);
			Store(nameValuePairs, "ImpactParamsAtBinBoundaries", ImpactParamsAtBinBoundaries);
			Store(nameValuePairs, "InitialMaximumTemperature", InitialMaximumTemperature);
			Store(nameValuePairs, "LifeTime", LifeTime);
			Store(nameValuePairs, "MeanParticipantsInBin", MeanParticipantsInBin);
			Store(nameValuePairs, "BreakupTemperature", BreakupTemperature);
			Store(nameValuePairs, "NuclearRadiusA", NuclearRadiusA);
			Store(nameValuePairs, "NuclearRadiusB", NuclearRadiusB);
			Store(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			Store(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Store(nameValuePairs, "NumberAveragingAngles", NumberAveragingAngles);
			Store(nameValuePairs, "ParticipantsAtBinBoundaries", ParticipantsAtBinBoundaries);
			Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Store(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			Store(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			Store(nameValuePairs, "ProtonProtonBaseline", ProtonProtonBaseline);
			Store(nameValuePairs, "QGPFormationTemperature", QGPFormationTemperature);
			Store(nameValuePairs, "NucleusShapeA", NucleusShapeA);
			Store(nameValuePairs, "NucleusShapeB", NucleusShapeB);
			Store(nameValuePairs, "SnapRate", SnapRate);
			Store(nameValuePairs, "TemperatureProfile", TemperatureProfile);
			Store(nameValuePairs, "ThermalTime", ThermalTime);
			Store(nameValuePairs, "TransverseMomenta", TransverseMomenta);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			TryExtract(nameValuePairs, "BeamRapidity", ref BeamRapidity);
			TryExtract(nameValuePairs, "BjorkenLifeTime", ref BjorkenLifeTime);
			TryExtract(nameValuePairs, "BottomiumStates", ref BottomiumStates);
			TryExtract(nameValuePairs, "CentralityBinBoundaries", ref CentralityBinBoundaries);
			TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			TryExtract(nameValuePairs, "DecayWidthEvaluationType", ref DecayWidthEvaluationType);
			TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
			TryExtract(nameValuePairs, "DiffusenessA", ref DiffusenessA);
			TryExtract(nameValuePairs, "DiffusenessB", ref DiffusenessB);
			TryExtract(nameValuePairs, "ExpansionMode", ref ExpansionMode);
			TryExtract(nameValuePairs, "FeedDown3P", ref FeedDown3P);
			TryExtract(nameValuePairs, "FireballFieldTypes", ref FireballFieldTypes);
			TryExtract(nameValuePairs, "FormationTimes", ref FormationTimes);
			TryExtract(nameValuePairs, "GridCellSize", ref GridCellSize);
			TryExtract(nameValuePairs, "GridRadius", ref GridRadius);
			TryExtract(nameValuePairs, "ImpactParameter", ref ImpactParameter);
			TryExtract(nameValuePairs, "ImpactParamsAtBinBoundaries", ref ImpactParamsAtBinBoundaries);
			TryExtract(nameValuePairs, "InitialMaximumTemperature", ref InitialMaximumTemperature);
			TryExtract(nameValuePairs, "LifeTime", ref LifeTime);
			TryExtract(nameValuePairs, "MeanParticipantsInBin", ref MeanParticipantsInBin);
			TryExtract(nameValuePairs, "BreakupTemperature", ref BreakupTemperature);
			TryExtract(nameValuePairs, "NuclearRadiusA", ref NuclearRadiusA);
			TryExtract(nameValuePairs, "NuclearRadiusB", ref NuclearRadiusB);
			TryExtract(nameValuePairs, "NucleonNumberA", ref NucleonNumberA);
			TryExtract(nameValuePairs, "NucleonNumberB", ref NucleonNumberB);
			TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			TryExtract(nameValuePairs, "ParticipantsAtBinBoundaries", ref ParticipantsAtBinBoundaries);
			TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			TryExtract(nameValuePairs, "ProtonNumberA", ref ProtonNumberA);
			TryExtract(nameValuePairs, "ProtonNumberB", ref ProtonNumberB);
			TryExtract(nameValuePairs, "ProtonProtonBaseline", ref ProtonProtonBaseline);
			TryExtract(nameValuePairs, "QGPFormationTemperature", ref QGPFormationTemperature);
			TryExtract(nameValuePairs, "NucleusShapeA", ref NucleusShapeA);
			TryExtract(nameValuePairs, "NucleusShapeB", ref NucleusShapeB);
			TryExtract(nameValuePairs, "SnapRate", ref SnapRate);
			TryExtract(nameValuePairs, "TemperatureProfile", ref TemperatureProfile);
			TryExtract(nameValuePairs, "ThermalTime", ref ThermalTime);
			TryExtract(nameValuePairs, "TransverseMomenta", ref TransverseMomenta);
		}

		private double BeamRapidity;

		private double BjorkenLifeTime;

		private string BottomiumStates = string.Empty;

		private int[][] CentralityBinBoundaries = new int[0][];

		private DecayWidthEvaluationType DecayWidthEvaluationType;

		private DecayWidthType DecayWidthType;

		private double DiffusenessA;

		private double DiffusenessB;

		private ExpansionMode ExpansionMode;

		private double FeedDown3P;

		private string[] FireballFieldTypes = new string[0];

		private double[] FormationTimes = new double[0];

		private double GridCellSize;

		private double GridRadius;

		private double ImpactParameter;

		private double[][] ImpactParamsAtBinBoundaries = new double[0][];

		private double InitialMaximumTemperature;

		private double LifeTime;

		private double[][] MeanParticipantsInBin = new double[0][];

		private int NumberAveragingAngles;

		private double BreakupTemperature;

		private double NuclearRadiusA;

		private double NuclearRadiusB;

		private int NucleonNumberA;

		private int NucleonNumberB;

		private double[][] ParticipantsAtBinBoundaries = new double[0][];

		private string[] PotentialTypes = new string[0];

		private ProtonProtonBaseline ProtonProtonBaseline;

		private int ProtonNumberA;

		private int ProtonNumberB;

		private double QGPFormationTemperature;

		private NucleusShape NucleusShapeA;

		private NucleusShape NucleusShapeB;

		private double SnapRate;

		private TemperatureProfile TemperatureProfile;

		private double ThermalTime;

		private double[] TransverseMomenta = new double[0];
	}
}