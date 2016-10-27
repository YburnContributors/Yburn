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
			Store(nameValuePairs, "MediumTemperatures", MediumTemperatures);
			Store(nameValuePairs, "MediumVelocities", MediumVelocities);
			Store(nameValuePairs, "NumberAveragingAngles", NumberAveragingAngles);
			Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Store(nameValuePairs, "QGPFormationTemperature", QGPFormationTemperature);

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
			TryExtract(nameValuePairs, "MediumTemperatures", ref MediumTemperatures);
			TryExtract(nameValuePairs, "MediumVelocities", ref MediumVelocities);
			TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			TryExtract(nameValuePairs, "QGPFormationTemperature", ref QGPFormationTemperature);
		}

		private BottomiumState[] BottomiumStates = new BottomiumState[0];

		private DecayWidthEvaluationType[] DecayWidthEvaluationTypes;

		private DecayWidthType DecayWidthType;

		private double[] MediumTemperatures;

		private double[] MediumVelocities;

		private int NumberAveragingAngles;

		private string[] PotentialTypes = new string[0];

		private double QGPFormationTemperature;
	}
}
