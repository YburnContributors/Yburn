using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.PhysUtil;

namespace Yburn.Workers
{
	public partial class Electromagnetism : Worker
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double EMFNormalization = Constants.ElementaryCharge
			* (Constants.HbarC_MeV_fm / Constants.RestMassPion_MeV)
			* (Constants.HbarC_MeV_fm / Constants.RestMassPion_MeV);

		private static double CalculateSpinStateOverlap(
			BottomiumState tripletState,
			double magneticFieldStrength
			)
		{
			Dictionary<int, double> hfs = QQDataProvider.GetHyperfineEnergySplitting(tripletState);
			Dictionary<int, double> overlap = new Dictionary<int, double>();

			foreach(int j in hfs.Keys)
			{
				double helper1 = 4 * Constants.MagnetonBottomQuark_fm * magneticFieldStrength
					* Constants.HbarC_MeV_fm / hfs[j];
				double helper2 = helper1 / (1 + Math.Sqrt(1 + helper1 * helper1));

				overlap.Add(j, helper2 * helper2 / (1 + helper2 * helper2));
			}

			return QQDataProvider.AverageHyperfineBottomiumProperties(tripletState, overlap);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Electromagnetism()
			: base()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double CalculateAverageSpinStateOverlap(
			BottomiumState tripletState,
			double properTime_fm
			)
		{
			FireballParam param = CreateFireballParam();

			LCFFieldFunction mixingCoefficientSquared = (x, y, rapidity) =>
			{
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				double B_per_fm2 = emf.CalculateMagneticFieldInLCF(
					properTime_fm, x, y, rapidity, QGPConductivity_MeV).Norm;

				return CalculateSpinStateOverlap(tripletState, B_per_fm2);
			};

			LCFFieldAverager avg = new LCFFieldAverager(param);

			return avg.AverageByBottomiumDistribution(mixingCoefficientSquared);
		}

		public void ShowEMFNormalizationFactor()
		{
			CurrentJobTitle = "ShowEMFNormalizationFactor";

			LogMessages.Clear();
			LogMessages.AppendLine("#EMF Normalization Factor:");
			LogMessages.AppendLine();
			LogMessages.AppendLine("1/fm² * e/(m_π)² = " + EMFNormalization.ToString());
			LogMessages.AppendLine();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected override Type GetEnumTypeByName(
			string enumName
			)
		{
			if(enumName == "EMFCalculationMethod")
			{
				return typeof(EMFCalculationMethod);
			}
			if(enumName == "NucleusShape")
			{
				return typeof(NucleusShape);
			}
			else
			{
				throw new Exception("Invalid enum name \"" + enumName + "\".");
			}
		}

		protected override void StartJob(
			string jobId
			)
		{
			switch(jobId)
			{
				case "PlotPointChargeAzimuthalMagneticField":
					PlotPointChargeAzimuthalMagneticField();
					break;

				case "PlotPointChargeLongitudinalElectricField":
					PlotPointChargeLongitudinalElectricField();
					break;

				case "PlotPointChargeRadialElectricField":
					PlotPointChargeRadialElectricField();
					break;

				case "PlotPointChargeAndNucleusFieldComponents":
					PlotPointChargeAndNucleusFieldComponents();
					break;

				case "PlotNucleusEMFStrengthInLCF":
					PlotNucleusEMFStrengthInLCF();
					break;

				case "PlotCollisionalEMFStrengthVersusTime":
					PlotCollisionalEMFStrengthVersusTime();
					break;

				case "PlotCollisionalEMFStrengthVersusImpactParam":
					PlotCollisionalEMFStrengthVersusImpactParam();
					break;

				case "PlotCollisionalEMFStrengthVersusTimeAndImpactParam":
					PlotCollisionalEMFStrengthVersusTimeAndImpactParam();
					break;

				case "PlotEMFStrengthInTransversePlane":
					PlotEMFStrengthInTransversePlane();
					break;

				case "PlotAverageCollisionalEMFStrength":
					PlotAverageCollisionalEMFStrength();
					break;

				case "PlotAverageSpinStateOverlap":
					PlotAverageSpinStateOverlap();
					break;

				case "ShowEMFNormalizationFactor":
					ShowEMFNormalizationFactor();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam
			{
				BeamRapidity = ParticleRapidity,
				DiffusenessA_fm = DiffusenessA_fm,
				DiffusenessB_fm = DiffusenessB_fm,
				EMFCalculationMethod = EMFCalculationMethod,
				EMFQuadratureOrder = EMFQuadratureOrder,
				GridCellSize_fm = GridCellSize_fm,
				GridRadius_fm = GridRadius_fm,
				ImpactParameter_fm = ImpactParameter_fm,
				NucleonNumberA = NucleonNumberA,
				NucleonNumberB = NucleonNumberB,
				NuclearRadiusA_fm = NuclearRadiusA_fm,
				NuclearRadiusB_fm = NuclearRadiusB_fm,
				NucleusShapeA = NucleusShapeA,
				NucleusShapeB = NucleusShapeB,
				ProtonNumberA = ProtonNumberA,
				ProtonNumberB = ProtonNumberB,
				QGPConductivity_MeV = QGPConductivity_MeV,
				TemperatureProfile = TemperatureProfile.NmixPHOBOS13
			};

			return param;
		}
	}
}
