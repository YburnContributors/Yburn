using System;
using System.Collections.Generic;
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
			DataFileName = Extractor.TryGetValue(nameValuePairs, "DataFileName", DataFileName);
			DataFileName = Extractor.TryGetValue(nameValuePairs, "Outfile", DataFileName);
			DiffusenessAFm = Extractor.TryGetValue(nameValuePairs, "DiffusenessA", DiffusenessAFm);
			DiffusenessBFm = Extractor.TryGetValue(nameValuePairs, "DiffusenessB", DiffusenessBFm);
			EMFCalculationMethod = Extractor.TryGetValue(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			EMFCalculationMethodSelection = Extractor.TryGetValueArray(nameValuePairs, "EMFCalculationMethodSelection", EMFCalculationMethodSelection);
			EffectiveTimeSamples = Extractor.TryGetValue(nameValuePairs, "EffectiveTimeSamples", EffectiveTimeSamples);
			GridCellSizeFm = Extractor.TryGetValue(nameValuePairs, "GridCellSize", GridCellSizeFm);
			GridRadiusFm = Extractor.TryGetValue(nameValuePairs, "GridRadius", GridRadiusFm);
			ImpactParameterFm = Extractor.TryGetValue(nameValuePairs, "ImpactParam", ImpactParameterFm);
			NuclearRadiusAFm = Extractor.TryGetValue(nameValuePairs, "NuclearRadiusA", NuclearRadiusAFm);
			NuclearRadiusBFm = Extractor.TryGetValue(nameValuePairs, "NuclearRadiusB", NuclearRadiusBFm);
			NucleonNumberA = Extractor.TryGetValue(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			NucleonNumberB = Extractor.TryGetValue(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			PointChargeVelocity = Extractor.TryGetValue(nameValuePairs, "PointChargeVelocity", PointChargeVelocity);
			ProtonNumberA = Extractor.TryGetValue(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			ProtonNumberB = Extractor.TryGetValue(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			QGPConductivityMeV = Extractor.TryGetValue(nameValuePairs, "QGPConductivityMeV", QGPConductivityMeV);
			RadialDistance = Extractor.TryGetValue(nameValuePairs, "RadialDistance", RadialDistance);
			ShapeFunctionTypeA = Extractor.TryGetValue<ShapeFunctionType>(nameValuePairs, "ShapeFunctionTypeA", ShapeFunctionTypeA);
			ShapeFunctionTypeB = Extractor.TryGetValue<ShapeFunctionType>(nameValuePairs, "ShapeFunctionTypeB", ShapeFunctionTypeB);
			StartEffectiveTime = Extractor.TryGetValue(nameValuePairs, "StartEffectiveTime", StartEffectiveTime);
			StopEffectiveTime = Extractor.TryGetValue(nameValuePairs, "StopEffectiveTime", StopEffectiveTime);
		}

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["DiffusenessA"] = DiffusenessAFm.ToString();
			nameValuePairs["DiffusenessB"] = DiffusenessBFm.ToString();
			nameValuePairs["EMFCalculationMethod"] = EMFCalculationMethod.ToString();
			nameValuePairs["EMFCalculationMethodSelection"] = EMFCalculationMethodSelection.ToUIString();
			nameValuePairs["EffectiveTimeSamples"] = EffectiveTimeSamples.ToString();
			nameValuePairs["GridCellSize"] = GridCellSizeFm.ToString();
			nameValuePairs["GridRadius"] = GridRadiusFm.ToString();
			nameValuePairs["ImpactParam"] = ImpactParameterFm.ToString();
			nameValuePairs["NuclearRadiusA"] = NuclearRadiusAFm.ToString();
			nameValuePairs["NuclearRadiusB"] = NuclearRadiusBFm.ToString();
			nameValuePairs["NucleonNumberA"] = NucleonNumberA.ToString();
			nameValuePairs["NucleonNumberB"] = NucleonNumberB.ToString();
			nameValuePairs["Outfile"] = Outfile;
			nameValuePairs["PointChargeVelocity"] = PointChargeVelocity.ToString();
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

				case "PlotCentralMagneticFieldStrength":
					PlotCentralMagneticFieldStrength();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}

		private FireballParam CreateFireballParam(
			EMFCalculationMethod emfCalculationMethod
			)
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = emfCalculationMethod;
			param.QGPConductivityMeV = QGPConductivityMeV;

			param.BeamRapidity = 7.99;
			param.DiffusenessAFm = DiffusenessAFm;
			param.DiffusenessBFm = DiffusenessBFm;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.ImpactParameterFm = ImpactParameterFm;

			return param;
		}
	}
}