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
				double helper1 = 4 * Constants.MagnetonBottomQuarkFm * magneticFieldStrength
					* Constants.HbarCMeVFm / HyperfineEnergySplitting_MeV[J];
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
					return new double[] { Constants.RestMassY1SMeV - Constants.RestMassEta1SMeV };

				case BottomiumState.x1P:
					return new double[] {
						Constants.RestMassX1P0MeV - Constants.RestMassH1PMeV,
						Constants.RestMassX1P1MeV - Constants.RestMassH1PMeV,
						Constants.RestMassX1P2MeV - Constants.RestMassH1PMeV
					};

				case BottomiumState.Y2S:
					return new double[] { Constants.RestMassY2SMeV - Constants.RestMassEta2SMeV };

				case BottomiumState.x2P:
					return new double[] {
						Constants.RestMassX2P0MeV - Constants.RestMassH2PMeV,
						Constants.RestMassX2P1MeV - Constants.RestMassH2PMeV,
						Constants.RestMassX2P2MeV - Constants.RestMassH2PMeV
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
			double properTimeFm
			)
		{
			FireballParam param = CreateFireballParam();

			LCFFieldFunction mixingCoefficientSquared = (x, y, rapidity) =>
			{
				CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);

				double B_PerFm2 = emf.CalculateMagneticFieldPerFm2_LCF(
					properTimeFm, x, y, rapidity).Norm;

				return CalculateSpinStateOverlap(tripletState, B_PerFm2);
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

				case "PlotPointChargeAndNucleusEMF":
					PlotPointChargeAndNucleusEMF();
					break;

				case "PlotNucleusMagneticFieldStrengthInLCF":
					PlotNucleusMagneticFieldStrengthInLCF();
					break;

				case "PlotCentralMagneticFieldStrength":
					PlotCentralMagneticFieldStrength();
					break;

				case "PlotEMFStrengthInTransversePlane":
					PlotEMFStrengthInTransversePlane();
					break;

				case "PlotAverageElectricFieldStrength":
					PlotAverageElectricFieldStrength();
					break;

				case "PlotAverageMagneticFieldStrength":
					PlotAverageMagneticFieldStrength();
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
			FireballParam param = new FireballParam();

			param.BeamRapidity = ParticleRapidity;
			param.DiffusenessAFm = DiffusenessAFm;
			param.DiffusenessBFm = DiffusenessBFm;
			param.EMFCalculationMethod = EMFCalculationMethod;
			param.EMFQuadratureOrder = EMFQuadratureOrder;
			param.GridCellSizeFm = GridCellSizeFm;
			param.GridRadiusFm = GridRadiusFm;
			param.ImpactParameterFm = ImpactParameterFm;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.NucleusShapeA = NucleusShapeA;
			param.NucleusShapeB = NucleusShapeB;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.QGPConductivityMeV = QGPConductivity;
			param.TemperatureProfile = TemperatureProfile.NmixPHOBOS13;

			return param;
		}
	}
}