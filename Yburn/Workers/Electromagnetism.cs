using System;
using System.Collections.Generic;
using System.Text;
using Yburn.Fireball;
using Yburn.Util;

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

		public void CalculateAverageMagneticFieldStrength()
		{
			GlauberCalculation glauber = new GlauberCalculation(CreateFireballParam());
			SimpleFireballField nCollField = glauber.NcollField;

			throw new NotImplementedException();
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
				return typeof(ShapeFunctionType);
			}
			else
			{
				throw new Exception("Invalid enum name \"" + enumName + "\".");
			}
		}

		private double DiffusenessAFm;

		private double DiffusenessBFm;

		private EMFCalculationMethod EMFCalculationMethod;

		private double GridCellSizeFm;

		private double GridRadiusFm;

		private double ImpactParameterFm;

		private double NuclearRadiusAFm;

		private double NuclearRadiusBFm;

		private int NucleonNumberA;

		private int NucleonNumberB;

		private string Outfile = "stdout.txt";

		private int ProtonNumberA;

		private int ProtonNumberB;

		private double QGPConductivityMeV;

		private ShapeFunctionType ShapeFunctionTypeA;

		private ShapeFunctionType ShapeFunctionTypeB;

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			DataFileName = Extractor.TryGetString(nameValuePairs, "DataFileName", DataFileName);
			DataFileName = Extractor.TryGetString(nameValuePairs, "Outfile", DataFileName);
			DiffusenessAFm = Extractor.TryGetDouble(nameValuePairs, "DiffusenessA", DiffusenessAFm);
			DiffusenessBFm = Extractor.TryGetDouble(nameValuePairs, "DiffusenessB", DiffusenessBFm);
			EMFCalculationMethod = Extractor.TryGetEnum<EMFCalculationMethod>(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			EMFCalculationMethodSelection = Extractor.TryGetEnumArray<EMFCalculationMethod>(nameValuePairs, "EMFCalculationMethodSelection", EMFCalculationMethodSelection);
			EffectiveTimeSamples = Extractor.TryGetInt(nameValuePairs, "EffectiveTimeSamples", EffectiveTimeSamples);
			GridCellSizeFm = Extractor.TryGetDouble(nameValuePairs, "GridCellSize", GridCellSizeFm);
			GridRadiusFm = Extractor.TryGetDouble(nameValuePairs, "GridRadius", GridRadiusFm);
			ImpactParameterFm = Extractor.TryGetDouble(nameValuePairs, "ImpactParam", ImpactParameterFm);
			LorentzFactor = Extractor.TryGetDouble(nameValuePairs, "LorentzFactor", LorentzFactor);
			NuclearRadiusAFm = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusA", NuclearRadiusAFm);
			NuclearRadiusBFm = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusB", NuclearRadiusBFm);
			NucleonNumberA = Extractor.TryGetInt(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			NucleonNumberB = Extractor.TryGetInt(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			ProtonNumberA = Extractor.TryGetInt(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			ProtonNumberB = Extractor.TryGetInt(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			QGPConductivityMeV = Extractor.TryGetDouble(nameValuePairs, "QGPConductivityMeV", QGPConductivityMeV);
			RadialDistance = Extractor.TryGetDouble(nameValuePairs, "RadialDistance", RadialDistance);
			ShapeFunctionTypeA = Extractor.TryGetEnum<ShapeFunctionType>(nameValuePairs, "ShapeFunctionTypeA", ShapeFunctionTypeA);
			ShapeFunctionTypeB = Extractor.TryGetEnum<ShapeFunctionType>(nameValuePairs, "ShapeFunctionTypeB", ShapeFunctionTypeB);
			StartEffectiveTime = Extractor.TryGetDouble(nameValuePairs, "StartEffectiveTime", StartEffectiveTime);
			StopEffectiveTime = Extractor.TryGetDouble(nameValuePairs, "StopEffectiveTime", StopEffectiveTime);
		}

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["DiffusenessA"] = DiffusenessAFm.ToString();
			nameValuePairs["DiffusenessB"] = DiffusenessBFm.ToString();
			nameValuePairs["EMFCalculationMethod"] = EMFCalculationMethod.ToString();
			nameValuePairs["EMFCalculationMethodSelection"] = EMFCalculationMethodSelection.ToStringifiedList();
			nameValuePairs["EffectiveTimeSamples"] = EffectiveTimeSamples.ToString();
			nameValuePairs["GridCellSize"] = GridCellSizeFm.ToString();
			nameValuePairs["GridRadius"] = GridRadiusFm.ToString();
			nameValuePairs["ImpactParam"] = ImpactParameterFm.ToString();
			nameValuePairs["LorentzFactor"] = LorentzFactor.ToString();
			nameValuePairs["NuclearRadiusA"] = NuclearRadiusAFm.ToString();
			nameValuePairs["NuclearRadiusB"] = NuclearRadiusBFm.ToString();
			nameValuePairs["NucleonNumberA"] = NucleonNumberA.ToString();
			nameValuePairs["NucleonNumberB"] = NucleonNumberB.ToString();
			nameValuePairs["Outfile"] = Outfile;
			nameValuePairs["ProtonNumberA"] = ProtonNumberA.ToString();
			nameValuePairs["ProtonNumberB"] = ProtonNumberB.ToString();
			nameValuePairs["QGPConductivityMeV"] = QGPConductivityMeV.ToString();
			nameValuePairs["RadialDistance"] = RadialDistance.ToString();
			nameValuePairs["ShapeFunctionTypeA"] = ShapeFunctionTypeA.ToString();
			nameValuePairs["ShapeFunctionTypeB"] = ShapeFunctionTypeB.ToString();
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
				AppendLogHeaderLines(stringBuilder, GetVariableNameValuePairs());

				return stringBuilder.ToString();
			}
		}

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.DiffusenessAFm = DiffusenessAFm;
			param.DiffusenessBFm = DiffusenessBFm;
			param.EMFCalculationMethod = EMFCalculationMethod;
			param.GridCellSizeFm = GridCellSizeFm;
			param.GridRadiusFm = GridRadiusFm;
			param.ImpactParameterFm = ImpactParameterFm;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.QGPConductivityMeV = QGPConductivityMeV;
			param.ShapeFunctionTypeA = ShapeFunctionTypeA;
			param.ShapeFunctionTypeB = ShapeFunctionTypeB;

			return param;
		}

		private FireballParam CreatePointChargeFireballParam(
			EMFCalculationMethod emfCalculationMethod
			)
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = emfCalculationMethod;
			param.QGPConductivityMeV = QGPConductivityMeV;

			return param;
		}
	}
}