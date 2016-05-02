using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yburn.Fireball;

namespace Yburn.Workers
{
	public partial class Electromagnetism : Worker
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Electromagnetism()
			: base()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		private FireballParam CreateFireballParam(
			EMFCalculationMethod emfCalculationMethod
			)
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = emfCalculationMethod;
			param.FourierFrequencySteps = FourierFrequencySteps;
			param.MaxFourierFrequency = MaxFourierFrequency;
			param.MinFourierFrequency = MinFourierFrequency;
			param.QGPConductivityMeV = QGPConductivityMeV;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected override Type GetEnumTypeByName(
			string enumName
			)
		{
			if(enumName == "EMFCalculationMethod")
			{
				return typeof(EMFCalculationMethod);
			}
			else
			{
				throw new Exception("Invalid enum name \"" + enumName + "\".");
			}
		}

		private EMFCalculationMethod EMFCalculationMethod;

		private int FourierFrequencySteps;

		private double MaxFourierFrequency;

		private double MinFourierFrequency;

		private string Outfile = "stdout.txt";

		private double QGPConductivityMeV;

		protected override void SetVariableNameValueList(
			Dictionary<string, string> nameValuePairs
			)
		{
			QGPConductivityMeV = Extractor.TryGetDouble(nameValuePairs, "QGPConductivityMeV", QGPConductivityMeV);
			EMFCalculationMethod = Extractor.TryGetEnum<EMFCalculationMethod>(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			MinFourierFrequency = Extractor.TryGetDouble(nameValuePairs, "MinFourierFrequency", MinFourierFrequency);
			MaxFourierFrequency = Extractor.TryGetDouble(nameValuePairs, "MaxFourierFrequency", MaxFourierFrequency);
			FourierFrequencySteps = Extractor.TryGetInt(nameValuePairs, "FourierFrequencySteps", FourierFrequencySteps);
			LorentzFactor = Extractor.TryGetDouble(nameValuePairs, "LorentzFactor", LorentzFactor);
			RadialDistance = Extractor.TryGetDouble(nameValuePairs, "RadialDistance", RadialDistance);
			StartEffectiveTime = Extractor.TryGetDouble(nameValuePairs, "StartEffectiveTime", StartEffectiveTime);
			StopEffectiveTime = Extractor.TryGetDouble(nameValuePairs, "StopEffectiveTime", StopEffectiveTime);
			EffectiveTimeSamples = Extractor.TryGetInt(nameValuePairs, "EffectiveTimeSamples", EffectiveTimeSamples);
			EMFCalculationMethodSelection = Extractor.TryGetEnumArray<EMFCalculationMethod>(nameValuePairs, "EMFCalculationMethodSelection", EMFCalculationMethodSelection);
			DataFileName = Extractor.TryGetString(nameValuePairs, "DataFileName", DataFileName);
			DataFileName = Extractor.TryGetString(nameValuePairs, "Outfile", DataFileName);
		}

		protected override Dictionary<string, string> GetVariableNameValueList()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			nameValuePairs["QGPConductivityMeV"] = QGPConductivityMeV.ToString();
			nameValuePairs["EMFCalculationMethod"] = EMFCalculationMethod.ToString();
			nameValuePairs["MinFourierFrequency"] = MinFourierFrequency.ToString();
			nameValuePairs["MaxFourierFrequency"] = MaxFourierFrequency.ToString();
			nameValuePairs["FourierFrequencySteps"] = FourierFrequencySteps.ToString();
			nameValuePairs["LorentzFactor"] = LorentzFactor.ToString();
			nameValuePairs["RadialDistance"] = RadialDistance.ToString();
			nameValuePairs["StartEffectiveTime"] = StartEffectiveTime.ToString();
			nameValuePairs["StopEffectiveTime"] = StopEffectiveTime.ToString();
			nameValuePairs["EffectiveTimeSamples"] = EffectiveTimeSamples.ToString();
			nameValuePairs["EMFCalculationMethodSelection"] = Converter.EnumArrayToString<EMFCalculationMethod>(EMFCalculationMethodSelection);
			nameValuePairs["Outfile"] = Outfile;

			return nameValuePairs;
		}

		protected override void StartJob(
			string jobId
			)
		{
			switch(jobId)
			{
				case "PlotPointChargeAzimutalMagneticField":
					PlotPointChargeAzimutalMagneticField();
					break;

				case "PlotPointChargeLongitudinalElectricField":
					PlotPointChargeLongitudinalElectricField();
					break;

				case "PlotPointChargeRadialElectricField":
					PlotPointChargeRadialElectricField();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}

		protected override string LogHeader
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(base.LogHeader);
				AppendLogHeaderLine(stringBuilder, "QGPConductivityMeV", QGPConductivityMeV);
				AppendLogHeaderLine(stringBuilder, "EMFCalculationMethod", EMFCalculationMethod);
				AppendLogHeaderLine(stringBuilder, "MinFourierFrequency", MinFourierFrequency);
				AppendLogHeaderLine(stringBuilder, "MaxFourierFrequency", MaxFourierFrequency);
				AppendLogHeaderLine(stringBuilder, "FourierFrequencySteps", FourierFrequencySteps);

				return stringBuilder.ToString();
			}
		}
	}
}