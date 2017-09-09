using System;
using Yburn.Fireball;
using Yburn.PhysUtil;

namespace Yburn.Workers
{
	public partial class Electromagnetism : Worker
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double CalculateSpinStateOverlap(
			BottomiumState tripletState,
			double magneticFieldStrength
			)
		{
			double[] HyperfineEnergySplitting_MeV = GetHyperfineEnergySplitting(tripletState);
			int Jmax = HyperfineEnergySplitting_MeV.Length - 1;

			double result = 0;

			for(int J = 0; J <= Jmax; J++)
			{
				double helper1 = 4 * Constants.MagnetonBottomQuark_fm * magneticFieldStrength
					* Constants.HbarC_MeV_fm / HyperfineEnergySplitting_MeV[J];
				double helper2 = helper1 / (1 + Math.Sqrt(1 + helper1 * helper1));

				double coefficient = helper2 * helper2 / (1 + helper2 * helper2);

				result += (2 * J + 1) * coefficient;
			}

			return result / (Jmax + 1) / (Jmax + 1);
		}

		private static double[] GetHyperfineEnergySplitting(
			BottomiumState tripletState
			)
		{
			switch(tripletState)
			{
				case BottomiumState.Y1S:
					return new double[] { Constants.RestMassY1S_MeV - Constants.RestMassEta1S_MeV };

				case BottomiumState.x1P:
					return new double[] {
						Constants.RestMassX1P0_MeV - Constants.RestMassH1P_MeV,
						Constants.RestMassX1P1_MeV - Constants.RestMassH1P_MeV,
						Constants.RestMassX1P2_MeV - Constants.RestMassH1P_MeV
					};

				case BottomiumState.Y2S:
					return new double[] { Constants.RestMassY2S_MeV - Constants.RestMassEta2S_MeV };

				case BottomiumState.x2P:
					return new double[] {
						Constants.RestMassX2P0_MeV - Constants.RestMassH2P_MeV,
						Constants.RestMassX2P1_MeV - Constants.RestMassH2P_MeV,
						Constants.RestMassX2P2_MeV - Constants.RestMassH2P_MeV
					};

				case BottomiumState.Y3S:
				case BottomiumState.x3P:
				default:
					throw new Exception("Invalid BottomiumState.");
			}
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
