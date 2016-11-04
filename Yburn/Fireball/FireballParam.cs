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

		public bool AreNucleusABIdentical
		{
			get
			{
				return NucleusShapeA == NucleusShapeB
					& NucleonNumberA == NucleonNumberB
					& ProtonNumberA == ProtonNumberB
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
				if(AreNucleusABIdentical)
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

		public List<double> FormationTimesFm;

		public double InitialMaximumTemperatureMeV;

		public double BreakupTemperatureMeV;

		public double BeamRapidity;

		public List<double> TransverseMomentaGeV;

		public ExpansionMode ExpansionMode;

		// initial transverse distribution of temperature
		public TemperatureProfile TemperatureProfile;

		public double QGPConductivityMeV;

		public EMFCalculationMethod EMFCalculationMethod;

		public NucleusShape NucleusShapeA;

		public NucleusShape NucleusShapeB;

		public ProtonProtonBaseline ProtonProtonBaseline;

		public DecayWidthRetrievalFunction DecayWidthRetrievalFunction;

		public FireballParam Clone()
		{
			FireballParam param = new FireballParam();

			param.BeamRapidity = BeamRapidity;
			param.DecayWidthRetrievalFunction = DecayWidthRetrievalFunction;
			param.DiffusenessAFm = DiffusenessAFm;
			param.DiffusenessBFm = DiffusenessBFm;
			param.EMFCalculationMethod = EMFCalculationMethod;
			param.ExpansionMode = ExpansionMode;
			param.FormationTimesFm = FormationTimesFm;
			param.GridCellSizeFm = GridCellSizeFm;
			param.GridRadiusFm = GridRadiusFm;
			param.ImpactParameterFm = ImpactParameterFm;
			param.InitialMaximumTemperatureMeV = InitialMaximumTemperatureMeV;
			param.BreakupTemperatureMeV = BreakupTemperatureMeV;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.ProtonProtonBaseline = ProtonProtonBaseline;
			param.QGPConductivityMeV = QGPConductivityMeV;
			param.NucleusShapeA = NucleusShapeA;
			param.NucleusShapeB = NucleusShapeB;
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTimeFm = ThermalTimeFm;
			param.TransverseMomentaGeV = TransverseMomentaGeV;

			return param;
		}

		public double[] GenerateDiscreteXAxis()
		{
			double[] x = new double[NumberGridPointsInX];

			if(AreNucleusABIdentical)
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