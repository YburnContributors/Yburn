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
            Extractor.Store(nameValuePairs, "DecayWidthAveragingAngles", DecayWidthAveragingAngles);
            Extractor.Store(nameValuePairs, "DecayWidthType", DecayWidthType);
            Extractor.Store(nameValuePairs, "MaxTemperature", MaxTemperature);
            Extractor.Store(nameValuePairs, "MediumVelocity", MediumVelocity);
            Extractor.Store(nameValuePairs, "MinTemperature", MinTemperature);
            Extractor.Store(nameValuePairs, "PotentialTypes", PotentialTypes);
            Extractor.Store(nameValuePairs, "TemperatureStepSize", TemperatureStepSize);
            Extractor.Store(nameValuePairs, "UseAveragedTemperature", UseAveragedTemperature);

            return nameValuePairs;
        }

        protected override void SetVariableNameValuePairs(
            Dictionary<string, string> nameValuePairs
            )
        {
            Extractor.TryExtract(nameValuePairs, "BottomiumStates", ref BottomiumStates);
            Extractor.TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
            Extractor.TryExtract(nameValuePairs, "DecayWidthAveragingAngles", ref DecayWidthAveragingAngles);
            Extractor.TryExtract(nameValuePairs, "DecayWidthType", ref DecayWidthType);
            Extractor.TryExtract(nameValuePairs, "MaxTemperature", ref MaxTemperature);
            Extractor.TryExtract(nameValuePairs, "MediumVelocity", ref MediumVelocity);
            Extractor.TryExtract(nameValuePairs, "MinTemperature", ref MinTemperature);
            Extractor.TryExtract(nameValuePairs, "PotentialTypes", ref PotentialTypes);
            Extractor.TryExtract(nameValuePairs, "TemperatureStepSize", ref TemperatureStepSize);
            Extractor.TryExtract(nameValuePairs, "UseAveragedTemperature", ref UseAveragedTemperature);
        }

        private BottomiumState[] BottomiumStates = new BottomiumState[0];

        private double[] DecayWidthAveragingAngles = new double[0];

        private DecayWidthType DecayWidthType;

        private double MaxTemperature;

        private double MediumVelocity;

        private double MinTemperature;

        private string[] PotentialTypes = new string[0];

        private double TemperatureStepSize;

        private bool UseAveragedTemperature;
    }
}
