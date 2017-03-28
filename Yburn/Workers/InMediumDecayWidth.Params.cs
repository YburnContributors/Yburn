using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.QQState;

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
			Store(nameValuePairs, "DopplerShiftEvaluationTypes", DopplerShiftEvaluationTypes);
			Store(nameValuePairs, "DecayWidthType", DecayWidthType);
			Store(nameValuePairs, "ElectricDipoleAlignment", ElectricDipoleAlignment);
			Store(nameValuePairs, "ElectricFieldStrength", ElectricFieldStrength);
			Store(nameValuePairs, "MagneticFieldStrength", MagneticFieldStrength);
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
			TryExtract(nameValuePairs, "DopplerShiftEvaluationTypes", ref DopplerShiftEvaluationTypes);
			TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
			TryExtract(nameValuePairs, "ElectricDipoleAlignment", ref ElectricDipoleAlignment);
			TryExtract(nameValuePairs, "ElectricFieldStrength", ref ElectricFieldStrength);
			TryExtract(nameValuePairs, "MagneticFieldStrength", ref MagneticFieldStrength);
			TryExtract(nameValuePairs, "MediumTemperatures", ref MediumTemperatures);
			TryExtract(nameValuePairs, "MediumVelocities", ref MediumVelocities);
			TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			TryExtract(nameValuePairs, "QGPFormationTemperature", ref QGPFormationTemperature);
		}

		private List<BottomiumState> BottomiumStates;

		private List<DopplerShiftEvaluationType> DopplerShiftEvaluationTypes;

		private DecayWidthType DecayWidthType;

		private ElectricDipoleAlignment ElectricDipoleAlignment;

		private double ElectricFieldStrength;

		private double MagneticFieldStrength;

		private List<double> MediumTemperatures;

		private List<double> MediumVelocities;

		private int NumberAveragingAngles;

		private List<PotentialType> PotentialTypes;

		private double QGPFormationTemperature;
	}
}
