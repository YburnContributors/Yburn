using System.Collections.Generic;

namespace Yburn.Fireball
{
	public class FireballParam
	{
		/********************************************************************************************
		   * Constructors
		   ********************************************************************************************/

		public FireballParam()
		{
		}

		/********************************************************************************************
		   * Public members, functions and properties
		   ********************************************************************************************/

		public int NucleonNumberA;

		public double DiffusenessFmA;

		public double NuclearRadiusFmA;

		public int NucleonNumberB;

		public double DiffusenessFmB;

		public double NuclearRadiusFmB;

		public double GridCellSizeFm;

		public int NumberGridCells;

		public double ImpactParamFm;

		public double ThermalTimeFm;

		public double[] FormationTimesFm;

		public double InitialCentralTemperatureMeV;

		public double MinimalCentralTemperatureMeV;

		public double BeamRapidity;

		public double[] TransverseMomentaGeV;

		public DecayWidthEvaluationType DecayWidthEvaluationType;

		public ExpansionMode ExpansionMode;

		// initial transverse distribution of temperature
		public TemperatureProfile TemperatureProfile;

		// both in MeV
		public List<KeyValuePair<double, double>>[] TemperatureDecayWidthList;

		public double[] DecayWidthAveragingAngles;

		public string FtexsLogPathFile;

		public FireballParam Clone()
		{
			FireballParam param = new FireballParam();

			param.BeamRapidity = BeamRapidity;
			param.DecayWidthAveragingAngles = DecayWidthAveragingAngles;
			param.DecayWidthEvaluationType = DecayWidthEvaluationType;
			param.DiffusenessFmA = DiffusenessFmA;
			param.DiffusenessFmB = DiffusenessFmB;
			param.ExpansionMode = ExpansionMode;
			param.FormationTimesFm = FormationTimesFm;
			param.FtexsLogPathFile = FtexsLogPathFile;
			param.GridCellSizeFm = GridCellSizeFm;
			param.ImpactParamFm = ImpactParamFm;
			param.InitialCentralTemperatureMeV = InitialCentralTemperatureMeV;
			param.MinimalCentralTemperatureMeV = MinimalCentralTemperatureMeV;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NuclearRadiusFmA = NuclearRadiusFmA;
			param.NuclearRadiusFmB = NuclearRadiusFmB;
			param.NumberGridCells = NumberGridCells;
			param.TemperatureDecayWidthList = TemperatureDecayWidthList;
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTimeFm = ThermalTimeFm;
			param.TransverseMomentaGeV = TransverseMomentaGeV;

			return param;
		}
	}
}