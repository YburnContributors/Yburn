using System;
using Yburn.Fireball;

namespace Yburn.Workers
{
	public partial class Electromagnetism : Worker
	{
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
				case "PlotPointChargeAzimutalMagneticField":
					PlotPointChargeAzimutalMagneticField();
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

				case "PlotNucleusMagneticFieldStrengthInLCF":
					PlotNucleusMagneticFieldStrengthInLCF();
					break;

				case "PlotCentralMagneticFieldStrength":
					PlotCentralMagneticFieldStrength();
					break;

				case "PlotAverageMagneticFieldStrength":
					PlotAverageMagneticFieldStrength();
					break;

				case "PlotSpinStateOverlap":
					PlotSpinStateOverlap();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}

		private FireballParam CreateFireballParam(
			EMFCalculationMethod emfCalculationMethod
			)
		{
			FireballParam param = new FireballParam();

			param.BeamRapidity = 7.99;
			param.DiffusenessAFm = DiffusenessAFm;
			param.DiffusenessBFm = DiffusenessBFm;
			param.EMFCalculationMethod = emfCalculationMethod;
			param.GridCellSizeFm = 0.4;
			param.GridRadiusFm = 10;
			param.ImpactParameterFm = ImpactParameterFm;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NuclearRadiusAFm = NuclearRadiusAFm;
			param.NuclearRadiusBFm = NuclearRadiusBFm;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.QGPConductivityMeV = QGPConductivityMeV;
			param.NucleusShapeA = NucleusShapeA;
			param.NucleusShapeB = NucleusShapeB;

			return param;
		}
	}
}