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

		public double DiffusenessA_fm
		{
			get
			{
				return DiffusenessA_fm_Nullable.Value;
			}
			set
			{
				DiffusenessA_fm_Nullable = value;
			}
		}

		public double NuclearRadiusA_fm
		{
			get
			{
				return NuclearRadiusA_fm_Nullable.Value;
			}
			set
			{
				NuclearRadiusA_fm_Nullable = value;
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

		public double DiffusenessB_fm
		{
			get
			{
				return DiffusenessB_fm_Nullable.Value;
			}
			set
			{
				DiffusenessB_fm_Nullable = value;
			}
		}

		public double NuclearRadiusB_fm
		{
			get
			{
				return NuclearRadiusB_fm_Nullable.Value;
			}
			set
			{
				NuclearRadiusB_fm_Nullable = value;
			}
		}

		public double GridCellSize_fm
		{
			get
			{
				return GridCellSize_fm_Nullable.Value;
			}
			set
			{
				GridCellSize_fm_Nullable = value;
			}
		}

		public double GridRadius_fm
		{
			get
			{
				return GridRadius_fm_Nullable.Value;
			}
			set
			{
				GridRadius_fm_Nullable = value;
			}
		}

		public double ImpactParameter_fm
		{
			get
			{
				return ImpactParameter_fm_Nullable.Value;
			}
			set
			{
				ImpactParameter_fm_Nullable = value;
			}
		}

		public double ThermalTime_fm
		{
			get
			{
				return ThermalTime_fm_Nullable.Value;
			}
			set
			{
				ThermalTime_fm_Nullable = value;
			}
		}

		public Dictionary<BottomiumState, double> FormationTimes_fm
		{
			get
			{
				return new Dictionary<BottomiumState, double>(FormationTimes_fm_Nullable);
			}
			set
			{
				FormationTimes_fm_Nullable = new Dictionary<BottomiumState, double>(value);
			}
		}

		public double InitialMaximumTemperature_MeV
		{
			get
			{
				return InitialMaximumTemperature_MeV_Nullable.Value;
			}
			set
			{
				InitialMaximumTemperature_MeV_Nullable = value;
			}
		}

		public double BreakupTemperature_MeV
		{
			get
			{
				return BreakupTemperature_MeV_Nullable.Value;
			}
			set
			{
				BreakupTemperature_MeV_Nullable = value;
			}
		}

		public double BeamRapidity
		{
			get
			{
				return Math.Log(1e6 * CenterOfMassEnergyTeV / Constants.RestMassProton_MeV);
			}
			set
			{
				CenterOfMassEnergyTeV = 1e-6 * Math.Exp(value) * Constants.RestMassProton_MeV;
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

		public double QGPConductivity_MeV
		{
			get
			{
				return QGPConductivity_MeV_Nullable.Value;
			}
			set
			{
				QGPConductivity_MeV_Nullable = value;
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

		public double EMFUpdateInterval_fm
		{
			get
			{
				return EMFUpdateInterval_fm_Nullable.Value;
			}
			set
			{
				EMFUpdateInterval_fm_Nullable = value;
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
				while(i * GridCellSize_fm <= GridRadius_fm)
				{
					xAxis.Add(i * GridCellSize_fm);

					if(!IsSystemSymmetricInX)
					{
						xAxis.Add(-i * GridCellSize_fm);
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
				while(j * GridCellSize_fm <= GridRadius_fm)
				{
					yAxis.Add(j * GridCellSize_fm);

					if(!IsSystemSymmetricInY)
					{
						yAxis.Add(-j * GridCellSize_fm);
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
			param.DiffusenessA_fm_Nullable = DiffusenessA_fm_Nullable;
			param.DiffusenessB_fm_Nullable = DiffusenessB_fm_Nullable;
			param.EMFCalculationMethod_Nullable = EMFCalculationMethod_Nullable;
			param.EMFQuadratureOrder_Nullable = EMFQuadratureOrder_Nullable;
			param.EMFUpdateInterval_fm_Nullable = EMFUpdateInterval_fm_Nullable;
			param.ExpansionMode_Nullable = ExpansionMode_Nullable;
			param.FormationTimes_fm_Nullable = FormationTimes_fm_Nullable;
			param.GridCellSize_fm_Nullable = GridCellSize_fm_Nullable;
			param.GridRadius_fm_Nullable = GridRadius_fm_Nullable;
			param.ImpactParameter_fm_Nullable = ImpactParameter_fm_Nullable;
			param.InitialMaximumTemperature_MeV_Nullable = InitialMaximumTemperature_MeV_Nullable;
			param.BreakupTemperature_MeV_Nullable = BreakupTemperature_MeV_Nullable;
			param.NuclearRadiusA_fm_Nullable = NuclearRadiusA_fm_Nullable;
			param.NuclearRadiusB_fm_Nullable = NuclearRadiusB_fm_Nullable;
			param.NucleonNumberA_Nullable = NucleonNumberA_Nullable;
			param.NucleonNumberB_Nullable = NucleonNumberB_Nullable;
			param.ProtonNumberA_Nullable = ProtonNumberA_Nullable;
			param.ProtonNumberB_Nullable = ProtonNumberB_Nullable;
			param.QGPConductivity_MeV_Nullable = QGPConductivity_MeV_Nullable;
			param.NucleusShapeA_Nullable = NucleusShapeA_Nullable;
			param.NucleusShapeB_Nullable = NucleusShapeB_Nullable;
			param.TemperatureProfile_Nullable = TemperatureProfile_Nullable;
			param.ThermalTime_fm_Nullable = ThermalTime_fm_Nullable;
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
					& NuclearRadiusA_fm == NuclearRadiusB_fm
					& DiffusenessA_fm == DiffusenessB_fm;
			}
		}

		private uint? NucleonNumberA_Nullable;

		private uint? ProtonNumberA_Nullable;

		private double? DiffusenessA_fm_Nullable;

		private double? NuclearRadiusA_fm_Nullable;

		private uint? NucleonNumberB_Nullable;

		private uint? ProtonNumberB_Nullable;

		private double? DiffusenessB_fm_Nullable;

		private double? NuclearRadiusB_fm_Nullable;

		private double? GridCellSize_fm_Nullable;

		private double? GridRadius_fm_Nullable;

		private double? ImpactParameter_fm_Nullable;

		private double? ThermalTime_fm_Nullable;

		private Dictionary<BottomiumState, double> FormationTimes_fm_Nullable;

		private double? InitialMaximumTemperature_MeV_Nullable;

		private double? BreakupTemperature_MeV_Nullable;

		private double? CenterOfMassEnergyTeV_Nullable;

		private List<double> TransverseMomentaGeV_Nullable;

		private ExpansionMode? ExpansionMode_Nullable;

		private TemperatureProfile? TemperatureProfile_Nullable;

		private double? QGPConductivity_MeV_Nullable;

		private EMFCalculationMethod? EMFCalculationMethod_Nullable;

		private int? EMFQuadratureOrder_Nullable;

		private double? EMFUpdateInterval_fm_Nullable;

		private NucleusShape? NucleusShapeA_Nullable;

		private NucleusShape? NucleusShapeB_Nullable;

		private DecayWidthRetrievalFunction DecayWidthRetrievalFunction_Nullable;

		private bool? UseElectricField_Nullable;

		private bool? UseMagneticField_Nullable;
	}
}
