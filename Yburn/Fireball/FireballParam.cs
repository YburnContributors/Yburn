using System;
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

		public bool AreParticlesABIdentical
		{
			get
			{
				return ShapeFunctionTypeA == ShapeFunctionTypeB
					& NucleonNumberA == NucleonNumberB
					& NuclearRadiusAFm == NuclearRadiusBFm
					& DiffusenessAFm == DiffusenessBFm;
			}
		}

		public int NucleonNumberA;

		public int ProtonNumberA;

		public double DiffusenessAFm;

		public double NuclearRadiusAFm;

		public int NucleonNumberB;

		public int ProtonNumberB;

		public double DiffusenessBFm;

		public double NuclearRadiusBFm;

		public double GridCellSizeFm;

		public int NumberGridPoints;

		public int NumberGridPointsInX
		{
			get
			{
				if(AreParticlesABIdentical)
				{
					return NumberGridPoints;
				}
				else
				{
					return 2 * NumberGridPoints - 1;
				}
			}
		}

		public int NumberGridPointsInY
		{
			get
			{
				return NumberGridPoints;
			}
		}

		public double ImpactParameterFm;

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

		public ShapeFunctionType ShapeFunctionTypeA;

		public ShapeFunctionType ShapeFunctionTypeB;

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
			param.ImpactParameterFm = ImpactParameterFm;
			param.InitialCentralTemperatureMeV = InitialCentralTemperatureMeV;
			param.MinimalCentralTemperatureMeV = MinimalCentralTemperatureMeV;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.NumberGridPoints = NumberGridPoints;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.QGPConductivityMeV = QGPConductivityMeV;
			param.TemperatureDecayWidthList = TemperatureDecayWidthList;
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTimeFm = ThermalTimeFm;
			param.TransverseMomentaGeV = TransverseMomentaGeV;
			param.ShapeFunctionTypeA = ShapeFunctionTypeA;
			param.ShapeFunctionTypeB = ShapeFunctionTypeB;

			return param;
		}

		public double[] GenerateDiscretizedXAxis()
		{
			double[] x = new double[NumberGridPointsInX];

			if(AreParticlesABIdentical)
			{
				for(int i = 0; i < x.Length; i++)
				{
					x[i] = GridCellSizeFm * i;
				}
			}
			else
			{
				for(int i = 0; i < x.Length; i++)
				{
					x[i] = GridCellSizeFm * (i + 1 - NumberGridPoints);
				}
			}

			return x;
		}

		public double[] GenerateDiscretizedYAxis()
		{
			double[] y = new double[NumberGridPointsInY];

			for(int i = 0; i < y.Length; i++)
			{
				y[i] = GridCellSizeFm * i;
			}

			return y;
		}
	}
}