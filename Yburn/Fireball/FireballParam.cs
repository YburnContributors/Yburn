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

		public int ProtonNumberA;

		public double DiffusenessAFm;

		public double NuclearRadiusAFm;

		public int NucleonNumberB;

		public int ProtonNumberB;

		public double DiffusenessBFm;

		public double NuclearRadiusBFm;

		public double GridCellSizeFm;

		public int NumberGridCells;

		public int NumberGridCellsInX;

		public int NumberGridCellsInY;

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

		public double QGPConductivityMeV;

		public EMFCalculationMethod EMFCalculationMethod;

		public string FtexsLogPathFile;

		public ShapeFunction ShapeFunctionA;

		public ShapeFunction ShapeFunctionB;

		public FireballParam Clone()
		{
			FireballParam param = new FireballParam();

			param.BeamRapidity = BeamRapidity;
			param.DecayWidthAveragingAngles = DecayWidthAveragingAngles;
			param.DecayWidthEvaluationType = DecayWidthEvaluationType;
			param.DiffusenessAFm = DiffusenessAFm;
			param.DiffusenessBFm = DiffusenessBFm;
			param.EMFCalculationMethod = EMFCalculationMethod;
			param.ExpansionMode = ExpansionMode;
			param.FormationTimesFm = FormationTimesFm;
			param.FtexsLogPathFile = FtexsLogPathFile;
			param.GridCellSizeFm = GridCellSizeFm;
			param.ImpactParamFm = ImpactParamFm;
			param.InitialCentralTemperatureMeV = InitialCentralTemperatureMeV;
			param.MinimalCentralTemperatureMeV = MinimalCentralTemperatureMeV;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.NumberGridCells = NumberGridCells;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.QGPConductivityMeV = QGPConductivityMeV;
			param.TemperatureDecayWidthList = TemperatureDecayWidthList;
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTimeFm = ThermalTimeFm;
			param.TransverseMomentaGeV = TransverseMomentaGeV;
			param.ShapeFunctionA = ShapeFunctionA;
			param.ShapeFunctionB = ShapeFunctionB;
			param.NumberGridCellsInX = NumberGridCellsInX;
			param.NumberGridCellsInY = NumberGridCellsInY;

			return param;
		}
	}
}