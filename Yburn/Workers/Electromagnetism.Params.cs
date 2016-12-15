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

			Store(nameValuePairs, "DataFileName", DataFileName);
			Store(nameValuePairs, "DiffusenessA", DiffusenessAFm);
			Store(nameValuePairs, "DiffusenessB", DiffusenessBFm);
			Store(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			Store(nameValuePairs, "EMFCalculationMethodSelection", EMFCalculationMethodSelection);
			Store(nameValuePairs, "EMFQuadratureOrder", EMFQuadratureOrder);
			Store(nameValuePairs, "GridCellSize", GridCellSizeFm);
			Store(nameValuePairs, "GridRadius", GridRadiusFm);
			Store(nameValuePairs, "ImpactParameter", ImpactParameterFm);
			Store(nameValuePairs, "NuclearRadiusA", NuclearRadiusAFm);
			Store(nameValuePairs, "NuclearRadiusB", NuclearRadiusBFm);
			Store(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			Store(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Store(nameValuePairs, "PointChargeRapidity", PointChargeRapidity);
			Store(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			Store(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			Store(nameValuePairs, "QGPConductivity", QGPConductivity);
			Store(nameValuePairs, "RadialDistance", RadialDistance);
			Store(nameValuePairs, "NucleusShapeA", NucleusShapeA);
			Store(nameValuePairs, "NucleusShapeB", NucleusShapeB);
			Store(nameValuePairs, "Samples", Samples);
			Store(nameValuePairs, "StartEffectiveTime", StartEffectiveTime);
			Store(nameValuePairs, "StopEffectiveTime", StopEffectiveTime);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			TryExtract(nameValuePairs, "DiffusenessA", ref DiffusenessAFm);
			TryExtract(nameValuePairs, "DiffusenessB", ref DiffusenessBFm);
			TryExtract(nameValuePairs, "EMFCalculationMethod", ref EMFCalculationMethod);
			TryExtract(nameValuePairs, "EMFCalculationMethodSelection", ref EMFCalculationMethodSelection);
			TryExtract(nameValuePairs, "EMFQuadratureOrder", ref EMFQuadratureOrder);
			TryExtract(nameValuePairs, "GridCellSize", ref GridCellSizeFm);
			TryExtract(nameValuePairs, "GridRadius", ref GridRadiusFm);
			TryExtract(nameValuePairs, "ImpactParameter", ref ImpactParameterFm);
			TryExtract(nameValuePairs, "NuclearRadiusA", ref NuclearRadiusAFm);
			TryExtract(nameValuePairs, "NuclearRadiusB", ref NuclearRadiusBFm);
			TryExtract(nameValuePairs, "NucleonNumberA", ref NucleonNumberA);
			TryExtract(nameValuePairs, "NucleonNumberB", ref NucleonNumberB);
			TryExtract(nameValuePairs, "PointChargeRapidity", ref PointChargeRapidity);
			TryExtract(nameValuePairs, "ProtonNumberA", ref ProtonNumberA);
			TryExtract(nameValuePairs, "ProtonNumberB", ref ProtonNumberB);
			TryExtract(nameValuePairs, "QGPConductivity", ref QGPConductivity);
			TryExtract(nameValuePairs, "RadialDistance", ref RadialDistance);
			TryExtract(nameValuePairs, "NucleusShapeA", ref NucleusShapeA);
			TryExtract(nameValuePairs, "NucleusShapeB", ref NucleusShapeB);
			TryExtract(nameValuePairs, "Samples", ref Samples);
			TryExtract(nameValuePairs, "StartEffectiveTime", ref StartEffectiveTime);
			TryExtract(nameValuePairs, "StopEffectiveTime", ref StopEffectiveTime);
		}

		private double DiffusenessAFm;

		private double DiffusenessBFm;

		private EMFCalculationMethod EMFCalculationMethod;

		private List<EMFCalculationMethod> EMFCalculationMethodSelection;

		private int EMFQuadratureOrder;

		private double GridCellSizeFm;

		private double GridRadiusFm;

		private double ImpactParameterFm;

		private double NuclearRadiusAFm;

		private double NuclearRadiusBFm;

		private uint NucleonNumberA;

		private uint NucleonNumberB;

		private double PointChargeRapidity;

		private uint ProtonNumberA;

		private uint ProtonNumberB;

		private double QGPConductivity;

		private double RadialDistance;

		private NucleusShape NucleusShapeA;

		private NucleusShape NucleusShapeB;

		private int Samples;

		private double StartEffectiveTime;

		private double StopEffectiveTime;
	}
}
