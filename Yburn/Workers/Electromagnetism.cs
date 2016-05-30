using System;
using System.Collections.Generic;
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
			if(enumName == "ShapeFunction")
			{
				return typeof(ShapeFunction);
			}
			else
			{
				throw new Exception("Invalid enum name \"" + enumName + "\".");
			}
		}

		private double DiffusenessA;

		private double DiffusenessB;

		private EMFCalculationMethod EMFCalculationMethod;

		private double ImpactParam;

		private double NuclearRadiusA;

		private double NuclearRadiusB;

		private int NucleonNumberA;

		private int NucleonNumberB;

		private string Outfile = "stdout.txt";

		private int ProtonNumberA;

		private int ProtonNumberB;

		private double QGPConductivityMeV;

		private ShapeFunction ShapeFunctionA;

		private ShapeFunction ShapeFunctionB;

		protected override void SetVariableNameValueList(
			Dictionary<string, string> nameValuePairs
			)
		{
			DataFileName = Extractor.TryGetString(nameValuePairs, "DataFileName", DataFileName);
			DataFileName = Extractor.TryGetString(nameValuePairs, "Outfile", DataFileName);
			DiffusenessA = Extractor.TryGetDouble(nameValuePairs, "DiffusenessA", DiffusenessA);
			DiffusenessB = Extractor.TryGetDouble(nameValuePairs, "DiffusenessB", DiffusenessB);
			EMFCalculationMethod = Extractor.TryGetEnum<EMFCalculationMethod>(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			EMFCalculationMethodSelection = Extractor.TryGetEnumArray<EMFCalculationMethod>(nameValuePairs, "EMFCalculationMethodSelection", EMFCalculationMethodSelection);
			EffectiveTimeSamples = Extractor.TryGetInt(nameValuePairs, "EffectiveTimeSamples", EffectiveTimeSamples);
			ImpactParam = Extractor.TryGetDouble(nameValuePairs, "ImpactParam", ImpactParam);
			LorentzFactor = Extractor.TryGetDouble(nameValuePairs, "LorentzFactor", LorentzFactor);
			NuclearRadiusA = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusA", NuclearRadiusA);
			NuclearRadiusB = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusB", NuclearRadiusB);
			NucleonNumberA = Extractor.TryGetInt(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			NucleonNumberB = Extractor.TryGetInt(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			ProtonNumberA = Extractor.TryGetInt(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			ProtonNumberB = Extractor.TryGetInt(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			QGPConductivityMeV = Extractor.TryGetDouble(nameValuePairs, "QGPConductivityMeV", QGPConductivityMeV);
			RadialDistance = Extractor.TryGetDouble(nameValuePairs, "RadialDistance", RadialDistance);
			ShapeFunctionA = Extractor.TryGetEnum<ShapeFunction>(nameValuePairs, "ShapeFunctionA", ShapeFunctionA);
			ShapeFunctionB = Extractor.TryGetEnum<ShapeFunction>(nameValuePairs, "ShapeFunctionB", ShapeFunctionB);
			StartEffectiveTime = Extractor.TryGetDouble(nameValuePairs, "StartEffectiveTime", StartEffectiveTime);
			StopEffectiveTime = Extractor.TryGetDouble(nameValuePairs, "StopEffectiveTime", StopEffectiveTime);
		}

		protected override Dictionary<string, string> GetVariableNameValueList()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["DiffusenessA"] = DiffusenessA.ToString();
			nameValuePairs["DiffusenessB"] = DiffusenessB.ToString();
			nameValuePairs["EMFCalculationMethod"] = EMFCalculationMethod.ToString();
			nameValuePairs["EMFCalculationMethodSelection"] = EMFCalculationMethodSelection.ToStringifiedList();
			nameValuePairs["EffectiveTimeSamples"] = EffectiveTimeSamples.ToString();
			nameValuePairs["ImpactParam"] = ImpactParam.ToString();
			nameValuePairs["LorentzFactor"] = LorentzFactor.ToString();
			nameValuePairs["NuclearRadiusA"] = NuclearRadiusA.ToString();
			nameValuePairs["NuclearRadiusB"] = NuclearRadiusB.ToString();
			nameValuePairs["NucleonNumberA"] = NucleonNumberA.ToString();
			nameValuePairs["NucleonNumberB"] = NucleonNumberB.ToString();
			nameValuePairs["Outfile"] = Outfile;
			nameValuePairs["ProtonNumberA"] = ProtonNumberA.ToString();
			nameValuePairs["ProtonNumberB"] = ProtonNumberB.ToString();
			nameValuePairs["QGPConductivityMeV"] = QGPConductivityMeV.ToString();
			nameValuePairs["RadialDistance"] = RadialDistance.ToString();
			nameValuePairs["ShapeFunctionA"] = ShapeFunctionA.ToString();
			nameValuePairs["ShapeFunctionB"] = ShapeFunctionB.ToString();
			nameValuePairs["StartEffectiveTime"] = StartEffectiveTime.ToString();
			nameValuePairs["StopEffectiveTime"] = StopEffectiveTime.ToString();

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

				return stringBuilder.ToString();
			}
		}
	}
}