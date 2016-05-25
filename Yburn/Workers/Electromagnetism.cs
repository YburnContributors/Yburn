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

		private double DiffusenessA;

		private double DiffusenessB;

		private EMFCalculationMethod EMFCalculationMethod;

		private int FourierFrequencySteps;

		private double ImpactParam;

		private double MaxFourierFrequency;

		private double MinFourierFrequency;

		private double NuclearRadiusA;

		private double NuclearRadiusB;

		private int NucleonNumberA;

		private int NucleonNumberB;

		private string Outfile = "stdout.txt";

		private int ProtonNumberA;

		private int ProtonNumberB;

		private double QGPConductivityMeV;

		protected override void SetVariableNameValueList(
			Dictionary<string, string> nameValuePairs
			)
		{
			DiffusenessA = Extractor.TryGetDouble(nameValuePairs, "DiffusenessA", DiffusenessA);
			DiffusenessB = Extractor.TryGetDouble(nameValuePairs, "DiffusenessB", DiffusenessB);
			NucleonNumberA = Extractor.TryGetInt(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			NucleonNumberB = Extractor.TryGetInt(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			NuclearRadiusA = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusA", NuclearRadiusA);
			NuclearRadiusB = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusB", NuclearRadiusB);
			ProtonNumberA = Extractor.TryGetInt(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			ProtonNumberB = Extractor.TryGetInt(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			ImpactParam = Extractor.TryGetDouble(nameValuePairs, "ImpactParam", ImpactParam);
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

			nameValuePairs["DiffusenessA"] = DiffusenessA.ToString();
			nameValuePairs["DiffusenessB"] = DiffusenessB.ToString();
			nameValuePairs["NucleonNumberA"] = NucleonNumberA.ToString();
			nameValuePairs["NucleonNumberB"] = NucleonNumberB.ToString();
			nameValuePairs["NuclearRadiusA"] = NuclearRadiusA.ToString();
			nameValuePairs["NuclearRadiusB"] = NuclearRadiusB.ToString();
			nameValuePairs["ProtonNumberA"] = ProtonNumberA.ToString();
			nameValuePairs["ProtonNumberB"] = ProtonNumberB.ToString();
			nameValuePairs["ImpactParam"] = ImpactParam.ToString();
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
			nameValuePairs["EMFCalculationMethodSelection"] = EMFCalculationMethodSelection.ToStringifiedList();
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