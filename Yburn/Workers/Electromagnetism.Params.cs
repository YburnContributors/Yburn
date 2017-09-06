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
			Store(nameValuePairs, "DiffusenessA_fm", DiffusenessA_fm);
			Store(nameValuePairs, "DiffusenessB_fm", DiffusenessB_fm);
			Store(nameValuePairs, "EMFCalculationMethod", EMFCalculationMethod);
			Store(nameValuePairs, "EMFQuadratureOrder", EMFQuadratureOrder);
			Store(nameValuePairs, "GridCellSize_fm", GridCellSize_fm);
			Store(nameValuePairs, "GridRadius_fm", GridRadius_fm);
			Store(nameValuePairs, "ImpactParameter_fm", ImpactParameter_fm);
			Store(nameValuePairs, "NuclearRadiusA_fm", NuclearRadiusA_fm);
			Store(nameValuePairs, "NuclearRadiusB_fm", NuclearRadiusB_fm);
			Store(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			Store(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Store(nameValuePairs, "ParticleRapidity", ParticleRapidity);
			Store(nameValuePairs, "ProtonNumberA", ProtonNumberA);
			Store(nameValuePairs, "ProtonNumberB", ProtonNumberB);
			Store(nameValuePairs, "QGPConductivity_MeV", QGPConductivity_MeV);
			Store(nameValuePairs, "RadialDistance_fm", RadialDistance_fm);
			Store(nameValuePairs, "NucleusShapeA", NucleusShapeA);
			Store(nameValuePairs, "NucleusShapeB", NucleusShapeB);
			Store(nameValuePairs, "Samples", Samples);
			Store(nameValuePairs, "StartTime_fm", StartTime_fm);
			Store(nameValuePairs, "StopTime_fm", StopTime_fm);

			return nameValuePairs;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			TryExtract(nameValuePairs, "DataFileName", ref DataFileName);
			TryExtract(nameValuePairs, "DiffusenessA_fm", ref DiffusenessA_fm);
			TryExtract(nameValuePairs, "DiffusenessB_fm", ref DiffusenessB_fm);
			TryExtract(nameValuePairs, "EMFCalculationMethod", ref EMFCalculationMethod);
			TryExtract(nameValuePairs, "EMFQuadratureOrder", ref EMFQuadratureOrder);
			TryExtract(nameValuePairs, "GridCellSize_fm", ref GridCellSize_fm);
			TryExtract(nameValuePairs, "GridRadius_fm", ref GridRadius_fm);
			TryExtract(nameValuePairs, "ImpactParameter_fm", ref ImpactParameter_fm);
			TryExtract(nameValuePairs, "NuclearRadiusA_fm", ref NuclearRadiusA_fm);
			TryExtract(nameValuePairs, "NuclearRadiusB_fm", ref NuclearRadiusB_fm);
			TryExtract(nameValuePairs, "NucleonNumberA", ref NucleonNumberA);
			TryExtract(nameValuePairs, "NucleonNumberB", ref NucleonNumberB);
			TryExtract(nameValuePairs, "ParticleRapidity", ref ParticleRapidity);
			TryExtract(nameValuePairs, "ProtonNumberA", ref ProtonNumberA);
			TryExtract(nameValuePairs, "ProtonNumberB", ref ProtonNumberB);
			TryExtract(nameValuePairs, "QGPConductivity_MeV", ref QGPConductivity_MeV);
			TryExtract(nameValuePairs, "RadialDistance_fm", ref RadialDistance_fm);
			TryExtract(nameValuePairs, "NucleusShapeA", ref NucleusShapeA);
			TryExtract(nameValuePairs, "NucleusShapeB", ref NucleusShapeB);
			TryExtract(nameValuePairs, "Samples", ref Samples);
			TryExtract(nameValuePairs, "StartTime_fm", ref StartTime_fm);
			TryExtract(nameValuePairs, "StopTime_fm", ref StopTime_fm);
		}

		private double DiffusenessA_fm;

		private double DiffusenessB_fm;

		private EMFCalculationMethod EMFCalculationMethod;

		private int EMFQuadratureOrder;

		private double GridCellSize_fm;

		private double GridRadius_fm;

		private double ImpactParameter_fm;

		private double NuclearRadiusA_fm;

		private double NuclearRadiusB_fm;

		private uint NucleonNumberA;

		private uint NucleonNumberB;

		private double ParticleRapidity;

		private uint ProtonNumberA;

		private uint ProtonNumberB;

		private double QGPConductivity_MeV;

		private double RadialDistance_fm;

		private NucleusShape NucleusShapeA;

		private NucleusShape NucleusShapeB;

		private int Samples;

		private double StartTime_fm;

		private double StopTime_fm;
	}
}
