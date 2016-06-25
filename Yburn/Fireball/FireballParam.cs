using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

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

		public double GridRadiusFm;

		public int NumberGridPoints
		{
			get
			{
				return Convert.ToInt32(Math.Round(GridRadiusFm / GridCellSizeFm)) + 1;
			}
			set
			{
				GridRadiusFm = (value - 1) * GridCellSizeFm;
			}
		}

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

		public double ParticleVelocity
		{
			get
			{
				return Math.Tanh(BeamRapidity);
			}
			set
			{
				BeamRapidity = Functions.Artanh(value);
			}
		}

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

		public ShapeFunctionType ShapeFunctionTypeA;

		public ShapeFunctionType ShapeFunctionTypeB;

		public ProtonProtonBaseline ProtonProtonBaseline;

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
			param.GridCellSizeFm = GridCellSizeFm;
			param.GridRadiusFm = GridRadiusFm;
			param.ImpactParameterFm = ImpactParameterFm;
			param.InitialCentralTemperatureMeV = InitialCentralTemperatureMeV;
			param.MinimalCentralTemperatureMeV = MinimalCentralTemperatureMeV;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.ProtonProtonBaseline = ProtonProtonBaseline;
			param.QGPConductivityMeV = QGPConductivityMeV;
			param.ShapeFunctionTypeA = ShapeFunctionTypeA;
			param.ShapeFunctionTypeB = ShapeFunctionTypeB;
			param.TemperatureDecayWidthList = TemperatureDecayWidthList;
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTimeFm = ThermalTimeFm;
			param.TransverseMomentaGeV = TransverseMomentaGeV;

			return param;
		}

		public double[] GenerateDiscreteXAxis()
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

		public double[] GenerateDiscreteYAxis()
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