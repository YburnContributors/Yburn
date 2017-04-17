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
				return NucleonNumberA_Nullable.Value;
			}
			set
			{
				NucleonNumberA_Nullable = value;
			}
		}

		public uint ProtonNumberA
		{
			get
			{
				return ProtonNumberA_Nullable.Value;
			}
			set
			{
				ProtonNumberA_Nullable = value;
			}
		}

		public double DiffusenessAFm
		{
			get
			{
				return DiffusenessAFm_Nullable.Value;
			}
			set
			{
				DiffusenessAFm_Nullable = value;
			}
		}

		public double NuclearRadiusAFm
		{
			get
			{
				return NuclearRadiusAFm_Nullable.Value;
			}
			set
			{
				NuclearRadiusAFm_Nullable = value;
			}
		}

		public uint NucleonNumberB
		{
			get
			{
				return NucleonNumberB_Nullable.Value;
			}
			set
			{
				NucleonNumberB_Nullable = value;
			}
		}

		public uint ProtonNumberB
		{
			get
			{
				return ProtonNumberB_Nullable.Value;
			}
			set
			{
				ProtonNumberB_Nullable = value;
			}
		}

		public double DiffusenessBFm
		{
			get
			{
				return DiffusenessBFm_Nullable.Value;
			}
			set
			{
				DiffusenessBFm_Nullable = value;
			}
		}

		public double NuclearRadiusBFm
		{
			get
			{
				return NuclearRadiusBFm_Nullable.Value;
			}
			set
			{
				NuclearRadiusBFm_Nullable = value;
			}
		}

		public double GridCellSizeFm
		{
			get
			{
				return GridCellSizeFm_Nullable.Value;
			}
			set
			{
				GridCellSizeFm_Nullable = value;
			}
		}

		public double GridRadiusFm
		{
			get
			{
				return GridRadiusFm_Nullable.Value;
			}
			set
			{
				GridRadiusFm_Nullable = value;
			}
		}

		public double ImpactParameterFm
		{
			get
			{
				return ImpactParameterFm_Nullable.Value;
			}
			set
			{
				ImpactParameterFm_Nullable = value;
			}
		}

		public double ThermalTimeFm
		{
			get
			{
				return ThermalTimeFm_Nullable.Value;
			}
			set
			{
				ThermalTimeFm_Nullable = value;
			}
		}

		public Dictionary<BottomiumState, double> FormationTimesFm
		{
			get
			{
				return new Dictionary<BottomiumState, double>(FormationTimesFm_Nullable);
			}
			set
			{
				FormationTimesFm_Nullable = new Dictionary<BottomiumState, double>(value);
			}
		}

		public double InitialMaximumTemperatureMeV
		{
			get
			{
				return InitialMaximumTemperatureMeV_Nullable.Value;
			}
			set
			{
				InitialMaximumTemperatureMeV_Nullable = value;
			}
		}

		public double BreakupTemperatureMeV
		{
			get
			{
				return BreakupTemperatureMeV_Nullable.Value;
			}
			set
			{
				BreakupTemperatureMeV_Nullable = value;
			}
		}

		public double BeamRapidity
		{
			get
			{
				return Math.Log(1e6 * CenterOfMassEnergyTeV / Constants.RestMassProtonMeV);
			}
			set
			{
				CenterOfMassEnergyTeV = 1e-6 * Math.Exp(value) * Constants.RestMassProtonMeV;
			}
		}

		public double CenterOfMassEnergyTeV
		{
			get
			{
				return CenterOfMassEnergyTeV_Nullable.Value;
			}
			set
			{
				CenterOfMassEnergyTeV_Nullable = value;
			}
		}

		public List<double> TransverseMomentaGeV
		{
			get
			{
				return new List<double>(TransverseMomentaGeV_Nullable);
			}
			set
			{
				TransverseMomentaGeV_Nullable = new List<double>(value);
			}
		}

		public ExpansionMode ExpansionMode
		{
			get
			{
				return ExpansionMode_Nullable.Value;
			}
			set
			{
				ExpansionMode_Nullable = value;
			}
		}

		// initial transverse distribution of temperature
		public TemperatureProfile TemperatureProfile
		{
			get
			{
				return TemperatureProfile_Nullable.Value;
			}
			set
			{
				TemperatureProfile_Nullable = value;
			}
		}

		public double QGPConductivityMeV
		{
			get
			{
				return QGPConductivityMeV_Nullable.Value;
			}
			set
			{
				QGPConductivityMeV_Nullable = value;
			}
		}

		public EMFCalculationMethod EMFCalculationMethod
		{
			get
			{
				return EMFCalculationMethod_Nullable.Value;
			}
			set
			{
				EMFCalculationMethod_Nullable = value;
			}
		}

		public int EMFQuadratureOrder
		{
			get
			{
				return EMFQuadratureOrder_Nullable.Value;
			}
			set
			{
				EMFQuadratureOrder_Nullable = value;
			}
		}

		public double EMFUpdateIntervalFm
		{
			get
			{
				return EMFUpdateIntervalFm_Nullable.Value;
			}
			set
			{
				EMFUpdateIntervalFm_Nullable = value;
			}
		}

		public NucleusShape NucleusShapeA
		{
			get
			{
				return NucleusShapeA_Nullable.Value;
			}
			set
			{
				NucleusShapeA_Nullable = value;
			}
		}

		public NucleusShape NucleusShapeB
		{
			get
			{
				return NucleusShapeB_Nullable.Value;
			}
			set
			{
				NucleusShapeB_Nullable = value;
			}
		}

		public DecayWidthRetrievalFunction DecayWidthRetrievalFunction
		{
			get
			{
				return (DecayWidthRetrievalFunction)DecayWidthRetrievalFunction_Nullable.Clone();
			}
			set
			{
				DecayWidthRetrievalFunction_Nullable = (DecayWidthRetrievalFunction)value.Clone();
			}
		}

		public bool UseElectricField
		{
			get
			{
				return UseElectricField_Nullable.Value;
			}
			set
			{
				UseElectricField_Nullable = value;
			}
		}

		public bool UseMagneticField
		{
			get
			{
				return UseMagneticField_Nullable.Value;
			}
			set
			{
				UseMagneticField_Nullable = value;
			}
		}

		public double NucleusPositionA
		{
			get
			{
				double d = 0.5 * (NuclearRadiusAFm + NuclearRadiusBFm - ImpactParameterFm);

				return Math.Min(d - NuclearRadiusAFm, 0) + Math.Max(d - NuclearRadiusBFm, 0);
			}
		}

		public double NucleusPositionB
		{
			get
			{
				return NucleusPositionA + ImpactParameterFm;
			}
		}

		public bool IsSystemSymmetricInX
		{
			get
			{
				return AreNucleusABIdentical;
			}
		}

		public bool IsSystemSymmetricInY
		{
			get
			{
				return true;
			}
		}

		public int SystemSymmetryFactor
		{
			get
			{
				int factor = 1;

				if(IsSystemSymmetricInX)
				{
					factor *= 2;
				}

				if(IsSystemSymmetricInY)
				{
					factor *= 2;
				}

				return factor;
			}
		}

		public double[] XAxis
		{
			get
			{
				List<double> xAxis = new List<double>();
				xAxis.Add(0);

				int i = 1;
				while(i * GridCellSizeFm <= GridRadiusFm)
				{
					xAxis.Add(i * GridCellSizeFm);

					if(!IsSystemSymmetricInX)
					{
						xAxis.Add(-i * GridCellSizeFm);
					}

					i++;
				}

				xAxis.Sort();

				return xAxis.ToArray();
			}
		}

		public double[] YAxis
		{
			get
			{
				List<double> yAxis = new List<double>();
				yAxis.Add(0);

				int j = 1;
				while(j * GridCellSizeFm <= GridRadiusFm)
				{
					yAxis.Add(j * GridCellSizeFm);

					if(!IsSystemSymmetricInY)
					{
						yAxis.Add(-j * GridCellSizeFm);
					}

					j++;
				}

				yAxis.Sort();

				return yAxis.ToArray();
			}
		}

		public FireballParam Clone()
		{
			FireballParam param = new FireballParam();

			param.CenterOfMassEnergyTeV_Nullable = CenterOfMassEnergyTeV_Nullable;
			param.DecayWidthRetrievalFunction_Nullable = DecayWidthRetrievalFunction_Nullable;
			param.DiffusenessAFm_Nullable = DiffusenessAFm_Nullable;
			param.DiffusenessBFm_Nullable = DiffusenessBFm_Nullable;
			param.EMFCalculationMethod_Nullable = EMFCalculationMethod_Nullable;
			param.EMFQuadratureOrder_Nullable = EMFQuadratureOrder_Nullable;
			param.EMFUpdateIntervalFm_Nullable = EMFUpdateIntervalFm_Nullable;
			param.ExpansionMode_Nullable = ExpansionMode_Nullable;
			param.FormationTimesFm_Nullable = FormationTimesFm_Nullable;
			param.GridCellSizeFm_Nullable = GridCellSizeFm_Nullable;
			param.GridRadiusFm_Nullable = GridRadiusFm_Nullable;
			param.ImpactParameterFm_Nullable = ImpactParameterFm_Nullable;
			param.InitialMaximumTemperatureMeV_Nullable = InitialMaximumTemperatureMeV_Nullable;
			param.BreakupTemperatureMeV_Nullable = BreakupTemperatureMeV_Nullable;
			param.NuclearRadiusAFm_Nullable = NuclearRadiusAFm_Nullable;
			param.NuclearRadiusBFm_Nullable = NuclearRadiusBFm_Nullable;
			param.NucleonNumberA_Nullable = NucleonNumberA_Nullable;
			param.NucleonNumberB_Nullable = NucleonNumberB_Nullable;
			param.ProtonNumberA_Nullable = ProtonNumberA_Nullable;
			param.ProtonNumberB_Nullable = ProtonNumberB_Nullable;
			param.QGPConductivityMeV_Nullable = QGPConductivityMeV_Nullable;
			param.NucleusShapeA_Nullable = NucleusShapeA_Nullable;
			param.NucleusShapeB_Nullable = NucleusShapeB_Nullable;
			param.TemperatureProfile_Nullable = TemperatureProfile_Nullable;
			param.ThermalTimeFm_Nullable = ThermalTimeFm_Nullable;
			param.TransverseMomentaGeV_Nullable = TransverseMomentaGeV_Nullable;
			param.UseElectricField_Nullable = UseElectricField_Nullable;
			param.UseMagneticField_Nullable = UseMagneticField_Nullable;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private bool AreNucleusABIdentical
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

		private uint? NucleonNumberA_Nullable;

		private uint? ProtonNumberA_Nullable;

		private double? DiffusenessAFm_Nullable;

		private double? NuclearRadiusAFm_Nullable;

		private uint? NucleonNumberB_Nullable;

		private uint? ProtonNumberB_Nullable;

		private double? DiffusenessBFm_Nullable;

		private double? NuclearRadiusBFm_Nullable;

		private double? GridCellSizeFm_Nullable;

		private double? GridRadiusFm_Nullable;

		private double? ImpactParameterFm_Nullable;

		private double? ThermalTimeFm_Nullable;

		private Dictionary<BottomiumState, double> FormationTimesFm_Nullable;

		private double? InitialMaximumTemperatureMeV_Nullable;

		private double? BreakupTemperatureMeV_Nullable;

		private double? CenterOfMassEnergyTeV_Nullable;

		private List<double> TransverseMomentaGeV_Nullable;

		private ExpansionMode? ExpansionMode_Nullable;

		private TemperatureProfile? TemperatureProfile_Nullable;

		private double? QGPConductivityMeV_Nullable;

		private EMFCalculationMethod? EMFCalculationMethod_Nullable;

		private int? EMFQuadratureOrder_Nullable;

		private double? EMFUpdateIntervalFm_Nullable;

		private NucleusShape? NucleusShapeA_Nullable;

		private NucleusShape? NucleusShapeB_Nullable;

		private DecayWidthRetrievalFunction DecayWidthRetrievalFunction_Nullable;

		private bool? UseElectricField_Nullable;

		private bool? UseMagneticField_Nullable;
	}
}
