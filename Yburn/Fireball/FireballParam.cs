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

		public uint NucleonNumberA
		{
			get
			{
				return Nullable_NucleonNumberA.Value;
			}
			set
			{
				Nullable_NucleonNumberA = value;
			}
		}

		public uint ProtonNumberA
		{
			get
			{
				return Nullable_ProtonNumberA.Value;
			}
			set
			{
				Nullable_ProtonNumberA = value;
			}
		}

		public double DiffusenessA_fm
		{
			get
			{
				return Nullable_DiffusenessA_fm.Value;
			}
			set
			{
				Nullable_DiffusenessA_fm = value;
			}
		}

		public double NuclearRadiusA_fm
		{
			get
			{
				return Nullable_NuclearRadiusA_fm.Value;
			}
			set
			{
				Nullable_NuclearRadiusA_fm = value;
			}
		}

		public uint NucleonNumberB
		{
			get
			{
				return Nullable_NucleonNumberB.Value;
			}
			set
			{
				Nullable_NucleonNumberB = value;
			}
		}

		public uint ProtonNumberB
		{
			get
			{
				return Nullable_ProtonNumberB.Value;
			}
			set
			{
				Nullable_ProtonNumberB = value;
			}
		}

		public double DiffusenessB_fm
		{
			get
			{
				return Nullable_DiffusenessB_fm.Value;
			}
			set
			{
				Nullable_DiffusenessB_fm = value;
			}
		}

		public double NuclearRadiusB_fm
		{
			get
			{
				return Nullable_NuclearRadiusB_fm.Value;
			}
			set
			{
				Nullable_NuclearRadiusB_fm = value;
			}
		}

		public double GridCellSize_fm
		{
			get
			{
				return Nullable_GridCellSize_fm.Value;
			}
			set
			{
				Nullable_GridCellSize_fm = value;
			}
		}

		public double GridRadius_fm
		{
			get
			{
				return Nullable_GridRadius_fm.Value;
			}
			set
			{
				Nullable_GridRadius_fm = value;
			}
		}

		public double ImpactParameter_fm
		{
			get
			{
				return Nullable_ImpactParameter_fm.Value;
			}
			set
			{
				Nullable_ImpactParameter_fm = value;
			}
		}

		public double ThermalTime_fm
		{
			get
			{
				return Nullable_ThermalTime_fm.Value;
			}
			set
			{
				Nullable_ThermalTime_fm = value;
			}
		}

		public Dictionary<BottomiumState, double> FormationTimes_fm
		{
			get
			{
				return new Dictionary<BottomiumState, double>(Nullable_FormationTimes_fm);
			}
			set
			{
				Nullable_FormationTimes_fm = new Dictionary<BottomiumState, double>(value);
			}
		}

		public double InitialMaximumTemperature_MeV
		{
			get
			{
				return Nullable_InitialMaximumTemperature_MeV.Value;
			}
			set
			{
				Nullable_InitialMaximumTemperature_MeV = value;
			}
		}

		public double BreakupTemperature_MeV
		{
			get
			{
				return Nullable_BreakupTemperature_MeV.Value;
			}
			set
			{
				Nullable_BreakupTemperature_MeV = value;
			}
		}

		public double QGPFormationTemperature_MeV
		{
			get
			{
				return Nullable_QGPFormationTemperature_MeV.Value;
			}
			set
			{
				Nullable_QGPFormationTemperature_MeV = value;
			}
		}

		public double BeamRapidity
		{
			get
			{
				return Math.Log(1e6 * CenterOfMassEnergy_TeV / Constants.RestMassProton_MeV);
			}
			set
			{
				CenterOfMassEnergy_TeV = 1e-6 * Math.Exp(value) * Constants.RestMassProton_MeV;
			}
		}

		public double CenterOfMassEnergy_TeV
		{
			get
			{
				return Nullable_CenterOfMassEnergy_TeV.Value;
			}
			set
			{
				Nullable_CenterOfMassEnergy_TeV = value;
			}
		}

		public List<double> TransverseMomenta_GeV
		{
			get
			{
				return new List<double>(Nullable_TransverseMomenta_GeV);
			}
			set
			{
				Nullable_TransverseMomenta_GeV = new List<double>(value);
			}
		}

		public ExpansionMode ExpansionMode
		{
			get
			{
				return Nullable_ExpansionMode.Value;
			}
			set
			{
				Nullable_ExpansionMode = value;
			}
		}

		// initial transverse distribution of temperature
		public TemperatureProfile TemperatureProfile
		{
			get
			{
				return Nullable_TemperatureProfile.Value;
			}
			set
			{
				Nullable_TemperatureProfile = value;
			}
		}

		public double QGPConductivity_MeV
		{
			get
			{
				return Nullable_QGPConductivity_MeV.Value;
			}
			set
			{
				Nullable_QGPConductivity_MeV = value;
			}
		}

		public EMFCalculationMethod EMFCalculationMethod
		{
			get
			{
				return Nullable_EMFCalculationMethod.Value;
			}
			set
			{
				Nullable_EMFCalculationMethod = value;
			}
		}

		public int EMFQuadratureOrder
		{
			get
			{
				return Nullable_EMFQuadratureOrder.Value;
			}
			set
			{
				Nullable_EMFQuadratureOrder = value;
			}
		}

		public double EMFUpdateInterval_fm
		{
			get
			{
				return Nullable_EMFUpdateInterval_fm.Value;
			}
			set
			{
				Nullable_EMFUpdateInterval_fm = value;
			}
		}

		public NucleusShape NucleusShapeA
		{
			get
			{
				return Nullable_NucleusShapeA.Value;
			}
			set
			{
				Nullable_NucleusShapeA = value;
			}
		}

		public NucleusShape NucleusShapeB
		{
			get
			{
				return Nullable_NucleusShapeB.Value;
			}
			set
			{
				Nullable_NucleusShapeB = value;
			}
		}

		public DecayWidthRetrievalFunction DecayWidthRetrievalFunction
		{
			get
			{
				return (DecayWidthRetrievalFunction)Nullable_DecayWidthRetrievalFunction.Clone();
			}
			set
			{
				Nullable_DecayWidthRetrievalFunction = (DecayWidthRetrievalFunction)value.Clone();
			}
		}

		public bool UseElectricField
		{
			get
			{
				return Nullable_UseElectricField.Value;
			}
			set
			{
				Nullable_UseElectricField = value;
			}
		}

		public bool UseMagneticField
		{
			get
			{
				return Nullable_UseMagneticField.Value;
			}
			set
			{
				Nullable_UseMagneticField = value;
			}
		}

		public double PartonPeakRapidity
		{
			get
			{
				double nucleonNumber = 0.5 * (NucleonNumberA + NucleonNumberB);

				return 1 / (1 + 0.2) * (BeamRapidity - Math.Log(nucleonNumber) / 6) - 0.2;
			}
		}

		public double NucleusPositionA
		{
			get
			{
				double d = 0.5 * (NuclearRadiusA_fm + NuclearRadiusB_fm - ImpactParameter_fm);

				return Math.Min(d - NuclearRadiusA_fm, 0) + Math.Max(d - NuclearRadiusB_fm, 0);
			}
		}

		public double NucleusPositionB
		{
			get
			{
				return NucleusPositionA + ImpactParameter_fm;
			}
		}

		public bool IsCollisionSymmetric
		{
			get
			{
				return NucleusShapeA == NucleusShapeB
					& NucleonNumberA == NucleonNumberB
					& ProtonNumberA == ProtonNumberB
					& NuclearRadiusA_fm == NuclearRadiusB_fm
					& DiffusenessA_fm == DiffusenessB_fm;
			}
		}

		public FireballParam Clone()
		{
			FireballParam param = new FireballParam
			{
				Nullable_CenterOfMassEnergy_TeV = Nullable_CenterOfMassEnergy_TeV,
				Nullable_DecayWidthRetrievalFunction = Nullable_DecayWidthRetrievalFunction,
				Nullable_DiffusenessA_fm = Nullable_DiffusenessA_fm,
				Nullable_DiffusenessB_fm = Nullable_DiffusenessB_fm,
				Nullable_EMFCalculationMethod = Nullable_EMFCalculationMethod,
				Nullable_EMFQuadratureOrder = Nullable_EMFQuadratureOrder,
				Nullable_EMFUpdateInterval_fm = Nullable_EMFUpdateInterval_fm,
				Nullable_ExpansionMode = Nullable_ExpansionMode,
				Nullable_FormationTimes_fm = Nullable_FormationTimes_fm,
				Nullable_GridCellSize_fm = Nullable_GridCellSize_fm,
				Nullable_GridRadius_fm = Nullable_GridRadius_fm,
				Nullable_ImpactParameter_fm = Nullable_ImpactParameter_fm,
				Nullable_InitialMaximumTemperature_MeV = Nullable_InitialMaximumTemperature_MeV,
				Nullable_BreakupTemperature_MeV = Nullable_BreakupTemperature_MeV,
				Nullable_QGPFormationTemperature_MeV = Nullable_QGPFormationTemperature_MeV,
				Nullable_NuclearRadiusA_fm = Nullable_NuclearRadiusA_fm,
				Nullable_NuclearRadiusB_fm = Nullable_NuclearRadiusB_fm,
				Nullable_NucleonNumberA = Nullable_NucleonNumberA,
				Nullable_NucleonNumberB = Nullable_NucleonNumberB,
				Nullable_ProtonNumberA = Nullable_ProtonNumberA,
				Nullable_ProtonNumberB = Nullable_ProtonNumberB,
				Nullable_QGPConductivity_MeV = Nullable_QGPConductivity_MeV,
				Nullable_NucleusShapeA = Nullable_NucleusShapeA,
				Nullable_NucleusShapeB = Nullable_NucleusShapeB,
				Nullable_TemperatureProfile = Nullable_TemperatureProfile,
				Nullable_ThermalTime_fm = Nullable_ThermalTime_fm,
				Nullable_TransverseMomenta_GeV = Nullable_TransverseMomenta_GeV,
				Nullable_UseElectricField = Nullable_UseElectricField,
				Nullable_UseMagneticField = Nullable_UseMagneticField
			};

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private uint? Nullable_NucleonNumberA;

		private uint? Nullable_ProtonNumberA;

		private double? Nullable_DiffusenessA_fm;

		private double? Nullable_NuclearRadiusA_fm;

		private uint? Nullable_NucleonNumberB;

		private uint? Nullable_ProtonNumberB;

		private double? Nullable_DiffusenessB_fm;

		private double? Nullable_NuclearRadiusB_fm;

		private double? Nullable_GridCellSize_fm;

		private double? Nullable_GridRadius_fm;

		private double? Nullable_ImpactParameter_fm;

		private double? Nullable_ThermalTime_fm;

		private Dictionary<BottomiumState, double> Nullable_FormationTimes_fm;

		private double? Nullable_InitialMaximumTemperature_MeV;

		private double? Nullable_BreakupTemperature_MeV;

		private double? Nullable_QGPFormationTemperature_MeV;

		private double? Nullable_CenterOfMassEnergy_TeV;

		private List<double> Nullable_TransverseMomenta_GeV;

		private ExpansionMode? Nullable_ExpansionMode;

		private TemperatureProfile? Nullable_TemperatureProfile;

		private double? Nullable_QGPConductivity_MeV;

		private EMFCalculationMethod? Nullable_EMFCalculationMethod;

		private int? Nullable_EMFQuadratureOrder;

		private double? Nullable_EMFUpdateInterval_fm;

		private NucleusShape? Nullable_NucleusShapeA;

		private NucleusShape? Nullable_NucleusShapeB;

		private DecayWidthRetrievalFunction Nullable_DecayWidthRetrievalFunction;

		private bool? Nullable_UseElectricField;

		private bool? Nullable_UseMagneticField;
	}
}
