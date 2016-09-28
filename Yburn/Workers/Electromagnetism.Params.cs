using System.Collections.Generic;
using Yburn.Fireball;

namespace Yburn.Workers
{
	partial class Electromagnetism
	{
		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();

			Extractor.Store(nameValuePairs, "DataFileName", DataFileName);
			Extractor.Store(nameValuePairs, "DiffusenessA", DiffusenessAFm);
			Extractor.Store(nameValuePairs, "DiffusenessB", DiffusenessBFm);
			Extractor.Store(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			Extractor.Store(nameValuePairs, "EMFCalculationMethodSelection", EMFCalculationMethodSelection);
			Extractor.Store(nameValuePairs, "EffectiveTimeSamples", EffectiveTimeSamples);
			Extractor.Store(nameValuePairs, "GridCellSize", GridCellSizeFm);
			Extractor.Store(nameValuePairs, "GridRadius", GridRadiusFm);
			Extractor.Store(nameValuePairs, "ImpactParameter", ImpactParameterFm);
			Extractor.Store(nameValuePairs, "NuclearRadiusA", NuclearRadiusAFm);
			Extractor.Store(nameValuePairs, "NuclearRadiusB", NuclearRadiusBFm);
			Extractor.Store(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			Extractor.Store(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Extractor.Store(nameValuePairs, "PointChargeRapidity", PointChargeRapidity);
			Extractor.Store(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			Extractor.Store(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			Extractor.Store(nameValuePairs, "QuadratureOrder", QuadratureOrder);
			Extractor.Store(nameValuePairs, "QGPConductivityMeV", QGPConductivityMeV);
			Extractor.Store(nameValuePairs, "RadialDistance", RadialDistance);
			Extractor.Store(nameValuePairs, "NucleusShapeA", NucleusShapeA);
			Extractor.Store(nameValuePairs, "NucleusShapeB", NucleusShapeB);
			Extractor.Store(nameValuePairs, "StartEffectiveTime", StartEffectiveTime);
			Extractor.Store(nameValuePairs, "StopEffectiveTime", StopEffectiveTime);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			Extractor.TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			Extractor.TryExtract(nameValuePairs, "DiffusenessA", ref DiffusenessAFm);
			Extractor.TryExtract(nameValuePairs, "DiffusenessB", ref DiffusenessBFm);
			Extractor.TryExtract(nameValuePairs, "EMFCalculationMethod", ref EMFCalculationMethod);
			Extractor.TryExtract(nameValuePairs, "EMFCalculationMethodSelection", ref EMFCalculationMethodSelection);
			Extractor.TryExtract(nameValuePairs, "EffectiveTimeSamples", ref EffectiveTimeSamples);
			Extractor.TryExtract(nameValuePairs, "GridCellSize", ref GridCellSizeFm);
			Extractor.TryExtract(nameValuePairs, "GridRadius", ref GridRadiusFm);
			Extractor.TryExtract(nameValuePairs, "ImpactParameter", ref ImpactParameterFm);
			Extractor.TryExtract(nameValuePairs, "NuclearRadiusA", ref NuclearRadiusAFm);
			Extractor.TryExtract(nameValuePairs, "NuclearRadiusB", ref NuclearRadiusBFm);
			Extractor.TryExtract(nameValuePairs, "NucleonNumberA", ref NucleonNumberA);
			Extractor.TryExtract(nameValuePairs, "NucleonNumberB", ref NucleonNumberB);
			Extractor.TryExtract(nameValuePairs, "PointChargeRapidity", ref PointChargeRapidity);
			Extractor.TryExtract(nameValuePairs, "ProtonNumberA", ref ProtonNumberA);
			Extractor.TryExtract(nameValuePairs, "ProtonNumberB", ref ProtonNumberB);
			Extractor.TryExtract(nameValuePairs, "QuadratureOrder", ref QuadratureOrder);
			Extractor.TryExtract(nameValuePairs, "QGPConductivityMeV", ref QGPConductivityMeV);
			Extractor.TryExtract(nameValuePairs, "RadialDistance", ref RadialDistance);
			Extractor.TryExtract(nameValuePairs, "NucleusShapeA", ref NucleusShapeA);
			Extractor.TryExtract(nameValuePairs, "NucleusShapeB", ref NucleusShapeB);
			Extractor.TryExtract(nameValuePairs, "StartEffectiveTime", ref StartEffectiveTime);
			Extractor.TryExtract(nameValuePairs, "StopEffectiveTime", ref StopEffectiveTime);
		}

		private double DiffusenessAFm;

		private double DiffusenessBFm;

		private int EffectiveTimeSamples;

		private EMFCalculationMethod EMFCalculationMethod;

		private EMFCalculationMethod[] EMFCalculationMethodSelection = new EMFCalculationMethod[0];

		private double GridCellSizeFm;

		private double GridRadiusFm;

		private double ImpactParameterFm;

		private double NuclearRadiusAFm;

		private double NuclearRadiusBFm;

		private int NucleonNumberA;

		private int NucleonNumberB;

		private double PointChargeRapidity;

		private int ProtonNumberA;

		private int ProtonNumberB;

		private int QuadratureOrder;

		private double QGPConductivityMeV;

		private double RadialDistance;

		private NucleusShape NucleusShapeA;

		private NucleusShape NucleusShapeB;

		private double StartEffectiveTime;

		private double StopEffectiveTime;
	}
}
