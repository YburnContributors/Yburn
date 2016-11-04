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

		public uint? NucleonNumberA
		{
			get; set;
		}

		public uint? ProtonNumberA
		{
			get; set;
		}

		public double? DiffusenessAFm
		{
			get; set;
		}

		public double? NuclearRadiusAFm
		{
			get; set;
		}

		public uint? NucleonNumberB
		{
			get; set;
		}

		public uint? ProtonNumberB
		{
			get; set;
		}

		public double? DiffusenessBFm
		{
			get; set;
		}

		public double? NuclearRadiusBFm
		{
			get; set;
		}

		public double? GridCellSizeFm
		{
			get; set;
		}

		public double? GridRadiusFm
		{
			get; set;
		}

		public double? ImpactParameterFm
		{
			get; set;
		}

		public double? ThermalTimeFm
		{
			get; set;
		}

		public List<double> FormationTimesFm
		{
			get
			{
				if(FormationTimesFm_Internal == null)
				{
					return null;
				}
				else
				{
					return new List<double>(FormationTimesFm_Internal);
				}
			}
			set
			{
				if(value == null)
				{
					FormationTimesFm_Internal = null;
				}
				else
				{
					FormationTimesFm_Internal = new List<double>(value);
				}
			}
		}

		public double? InitialMaximumTemperatureMeV
		{
			get; set;
		}

		public double? BreakupTemperatureMeV
		{
			get; set;
		}

		public double? BeamRapidity
		{
			get; set;
		}

		public List<double> TransverseMomentaGeV
		{
			get
			{
				if(TransverseMomentaGeV_Internal == null)
				{
					return null;
				}
				else
				{
					return new List<double>(TransverseMomentaGeV_Internal);
				}
			}
			set
			{
				if(value == null)
				{
					TransverseMomentaGeV_Internal = null;
				}
				else
				{
					TransverseMomentaGeV_Internal = new List<double>(value);
				}
			}
		}

		public ExpansionMode? ExpansionMode
		{
			get; set;
		}

		// initial transverse distribution of temperature
		public TemperatureProfile? TemperatureProfile
		{
			get; set;
		}

		public double? QGPConductivityMeV
		{
			get; set;
		}

		public EMFCalculationMethod? EMFCalculationMethod
		{
			get; set;
		}

		public NucleusShape? NucleusShapeA
		{
			get; set;
		}

		public NucleusShape? NucleusShapeB
		{
			get; set;
		}

		public ProtonProtonBaseline? ProtonProtonBaseline
		{
			get; set;
		}

		public DecayWidthRetrievalFunction DecayWidthRetrievalFunction
		{
			get; set;
		}

		public bool AreNucleusABIdentical
		{
			get
			{
				return NucleusShapeA.Value == NucleusShapeB.Value
					& NucleonNumberA.Value == NucleonNumberB.Value
					& ProtonNumberA.Value == ProtonNumberB.Value
					& NuclearRadiusAFm.Value == NuclearRadiusBFm.Value
					& DiffusenessAFm.Value == DiffusenessBFm.Value;
			}
		}

		public int NumberGridPoints
		{
			get
			{
				return Convert.ToInt32(Math.Round(GridRadiusFm.Value / GridCellSizeFm.Value)) + 1;
			}
			set
			{
				GridRadiusFm = (value - 1) * GridCellSizeFm.Value;
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

		public List<double> GenerateDiscreteXAxis()
		{
			List<double> x = new List<double>();

			if(AreNucleusABIdentical)
			{
				for(int i = 0; i < NumberGridPointsInX; i++)
				{
					x.Add(GridCellSizeFm.Value * i);
				}
			}
			else
			{
				for(int i = 0; i < NumberGridPointsInX; i++)
				{
					x.Add(GridCellSizeFm.Value * (i + 1 - NumberGridPoints));
				}
			}

			return x;
		}

		public List<double> GenerateDiscreteYAxis()
		{
			List<double> y = new List<double>();

			for(int i = 0; i < NumberGridPointsInY; i++)
			{
				y.Add(GridCellSizeFm.Value * i);
			}

			return y;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private List<double> FormationTimesFm_Internal;

		private List<double> TransverseMomentaGeV_Internal;
	}
}