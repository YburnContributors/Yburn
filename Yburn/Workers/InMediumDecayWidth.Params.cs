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

			Store(nameValuePairs, "BottomiumStates", BottomiumStates);
			Store(nameValuePairs, "DataFileName", DataFileName);
			Store(nameValuePairs, "DecayWidthEvaluationTypes", DecayWidthEvaluationTypes);
			Store(nameValuePairs, "DecayWidthType", DecayWidthType);
			Store(nameValuePairs, "MaxTemperature", MaxTemperature);
			Store(nameValuePairs, "MediumVelocity", MediumVelocity);
			Store(nameValuePairs, "MinTemperature", MinTemperature);
			Store(nameValuePairs, "NumberAveragingAngles", NumberAveragingAngles);
			Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Store(nameValuePairs, "QGPFormationTemperature", QGPFormationTemperature);
			Store(nameValuePairs, "TemperatureStepSize", TemperatureStepSize);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			TryExtract(nameValuePairs, "BottomiumStates", ref BottomiumStates);
			TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			TryExtract(nameValuePairs, "DecayWidthEvaluationTypes", ref DecayWidthEvaluationTypes);
			TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
			TryExtract(nameValuePairs, "MaxTemperature", ref MaxTemperature);
			TryExtract(nameValuePairs, "MediumVelocity", ref MediumVelocity);
			TryExtract(nameValuePairs, "MinTemperature", ref MinTemperature);
			TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			TryExtract(nameValuePairs, "QGPFormationTemperature", ref QGPFormationTemperature);
			TryExtract(nameValuePairs, "TemperatureStepSize", ref TemperatureStepSize);
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
