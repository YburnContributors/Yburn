using System.Collections.Generic;
using Yburn.Fireball;

namespace Yburn.Workers
{
	partial class InMediumDecayWidth
	{
		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			Extractor.Store(nameValuePairs, "BottomiumStates", BottomiumStates);
			Extractor.Store(nameValuePairs, "DataFileName", DataFileName);
			Extractor.Store(nameValuePairs, "DecayWidthEvaluationTypes", DecayWidthEvaluationTypes);
			Extractor.Store(nameValuePairs, "DecayWidthType", DecayWidthType);
			Extractor.Store(nameValuePairs, "MaxTemperature", MaxTemperature);
			Extractor.Store(nameValuePairs, "MediumVelocity", MediumVelocity);
			Extractor.Store(nameValuePairs, "MinTemperature", MinTemperature);
			Extractor.Store(nameValuePairs, "NumberAveragingAngles", NumberAveragingAngles);
			Extractor.Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Extractor.Store(nameValuePairs, "QGPFormationTemperature", QGPFormationTemperature);
			Extractor.Store(nameValuePairs, "TemperatureStepSize", TemperatureStepSize);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			Extractor.TryExtract(nameValuePairs, "BottomiumStates", ref BottomiumStates);
			Extractor.TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			Extractor.TryExtract(nameValuePairs, "DecayWidthEvaluationTypes", ref DecayWidthEvaluationTypes);
			Extractor.TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
			Extractor.TryExtract(nameValuePairs, "MaxTemperature", ref MaxTemperature);
			Extractor.TryExtract(nameValuePairs, "MediumVelocity", ref MediumVelocity);
			Extractor.TryExtract(nameValuePairs, "MinTemperature", ref MinTemperature);
			Extractor.TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			Extractor.TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			Extractor.TryExtract(nameValuePairs, "QGPFormationTemperature", ref QGPFormationTemperature);
			Extractor.TryExtract(nameValuePairs, "TemperatureStepSize", ref TemperatureStepSize);
		}

		private BottomiumState[] BottomiumStates = new BottomiumState[0];

		private DecayWidthEvaluationType[] DecayWidthEvaluationTypes;

		private DecayWidthType DecayWidthType;

		private double MaxTemperature;

		private double MediumVelocity;

		private double MinTemperature;

		private int NumberAveragingAngles;

		private string[] PotentialTypes = new string[0];

		private double QGPFormationTemperature;

		private double TemperatureStepSize;
	}
}
