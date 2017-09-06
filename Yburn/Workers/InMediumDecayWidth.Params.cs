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
			Store(nameValuePairs, "ElectricFieldStrength_per_fm2", ElectricFieldStrength_per_fm2);
			Store(nameValuePairs, "MagneticFieldStrength_per_fm2", MagneticFieldStrength_per_fm2);
			Store(nameValuePairs, "MediumTemperatures_MeV", MediumTemperatures_MeV);
			Store(nameValuePairs, "MediumVelocities", MediumVelocities);
			Store(nameValuePairs, "NumberAveragingAngles", NumberAveragingAngles);
			Store(nameValuePairs, "PotentialTypes", PotentialTypes);
			Store(nameValuePairs, "QGPFormationTemperature_MeV", QGPFormationTemperature_MeV);

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
			TryExtract(nameValuePairs, "ElectricFieldStrength_per_fm2", ref ElectricFieldStrength_per_fm2);
			TryExtract(nameValuePairs, "MagneticFieldStrength_per_fm2", ref MagneticFieldStrength_per_fm2);
			TryExtract(nameValuePairs, "MediumTemperatures_MeV", ref MediumTemperatures_MeV);
			TryExtract(nameValuePairs, "MediumVelocities", ref MediumVelocities);
			TryExtract(nameValuePairs, "NumberAveragingAngles", ref NumberAveragingAngles);
			TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
			TryExtract(nameValuePairs, "QGPFormationTemperature_MeV", ref QGPFormationTemperature_MeV);
		}

		private List<BottomiumState> BottomiumStates;

		private List<DopplerShiftEvaluationType> DopplerShiftEvaluationTypes;

		private DecayWidthType DecayWidthType;

		private ElectricDipoleAlignment ElectricDipoleAlignment;

		private double ElectricFieldStrength_per_fm2;

		private double MagneticFieldStrength_per_fm2;

		private List<double> MediumTemperatures_MeV;

		private List<double> MediumVelocities;

		private int NumberAveragingAngles;

		private List<PotentialType> PotentialTypes;

		private double QGPFormationTemperature_MeV;
	}
}
