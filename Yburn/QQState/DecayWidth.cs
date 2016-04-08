/**************************************************************************************************
 * Pion masses are taken from K.A. Olive et al. (Particle Data Group), Chin. Phys. C38, 090001
 * (2014)
 **************************************************************************************************/

using Meta.Numerics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public class DecayWidth
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public DecayWidth(
			CancellationToken calculationCancelToken,
			QQBoundState boundState,
			double maxEnergyMeV,
			int energySteps,
			double temperatureMeV,
			string[] statusValues = null
			)
		{
			if(boundState.Param.ColorState != ColorState.Singlet)
			{
				throw new Exception("Incoming state has to be color Singlet.");
			}
			if(energySteps <= 0)
			{
				throw new Exception("EnergySteps <= 0.");
			}
			if(temperatureMeV < boundState.Param.TchemMeV)
			{
				throw new Exception("Temperature < Tchem.");
			}
			if(statusValues != null && statusValues.Length != 3)
			{
				throw new Exception("Length of StatusValues must be three.");
			}

			CalculationCancelToken = calculationCancelToken;
			BoundState = boundState;
			BoundParam = BoundState.Param;
			BoundWave = BoundState.WaveFunctionFm;
			RadiusFm = BoundState.RadiusFm;
			TemperatureMeV = temperatureMeV;
			MaxEnergyMeV = maxEnergyMeV;
			EnergySteps = energySteps;
			MinEnergyMeV = GetMinEnergy();

			if(MaxEnergyMeV <= MinEnergyMeV)
			{
				throw new Exception("Invalid energy range.");
			}

			GammaDampMeV = 0;

			// to be calculated after the cross section
			GammaDissMeV = -1;

			CheckStepSizeSmallEnough();

			// domain of calculcation is divided into EnergySteps cells
			CrossSectionMb = new double[EnergySteps + 1];
			HadronicCrossSectionMb = new double[EnergySteps + 1];
			EnergyMeV = GetEnergyValueArray();

			StatusValues = statusValues;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// partial decay width due to gluodissociation
		public double GammaDissMeV
		{
			get;
			private set;
		}

		public List<string> CrossSectionStringList
		{
			get
			{
				List<string> dataList = new List<string>();
				dataList.Add(string.Format("{0,-18}{1,-18}{2,-18}",
					 "#E (MeV)",
					 "SigmaG (mb)",
					 "SigmaPi (mb)"));

				for(int j = 0; j <= EnergySteps; j++)
				{
					dataList.Add(string.Format("{0,-18}{1,-18}{2,-18}",
							EnergyMeV[j].ToString("G10"),
							CrossSectionMb[j].ToString("G10"),
							HadronicCrossSectionMb[j].ToString("G10")));
				}

				return dataList;
			}
		}

		// Calculates the cross section CrossSection in the energy interval MinEnergy - MaxEnergyMeV
		// and subsequently the gluodissociation decay width GammaDissMeV.
		public void CalculateGammaDiss()
		{
			CrossSectionMb = GetCrossSection();

			if(!CalculationCancelToken.IsCancellationRequested)
			{
				if(TemperatureMeV >= BoundParam.TcritMeV)
				{
					// we have a QGP
					GammaDissMeV = GetQGPDecayWidth();
				}
				else if(TemperatureMeV >= BoundParam.TchemMeV)
				{
					// we have a hadronic medium
					GammaDissMeV = GetHadronicDecayWidth();
				}
				else
				{
					// zero decay width below the chemical freeze-out temperature
					GammaDissMeV = 0;
				}
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double MinEnergyMeV;

		private double MaxEnergyMeV;

		private int EnergySteps;

		private double[] EnergyMeV;

		// partial decay width due to landau damping
		protected double GammaDampMeV;

		private double[] CrossSectionMb;

		private double[] HadronicCrossSectionMb;

		private QQStateParam BoundParam;

		// radial wave function of the bound state that gets dissociated in fm^-1/2
		private Complex[] BoundWave;

		private QQBoundState BoundState;

		// values of radial distance at which the wave functions are evaluated
		private double[] RadiusFm;

		private double TemperatureMeV;

		// information about the current state of the calculation to be communicated to the outside
		private string[] StatusValues;

		private CancellationToken CalculationCancelToken;

		private double GetMinEnergy()
		{
			switch(BoundParam.PotentialType)
			{
				case PotentialType.Tzero:
				case PotentialType.Tzero_NoString:
					// hadronic medium - dissociation into b-meson pair
					return 2 * PhysConst.BMesonMass - BoundState.BoundMassMeV;

				default:
					// we have a QGP
					return Math.Abs(BoundParam.EnergyMeV);
			}
		}

		private double[] GetEnergyValueArray()
		{
			double[] energy = new double[EnergySteps + 1];

			double dE = Math.Exp(Math.Log(MaxEnergyMeV / MinEnergyMeV) / EnergySteps);
			energy[0] = MinEnergyMeV;

			for(int n = 0; n < EnergySteps; n++)
			{
				energy[n + 1] = energy[n] * dE;
			}

			return energy;
		}

		private double GetHadronicDecayWidth()
		{
			HadronicCrossSectionMb = GetHadronicCrossSection();

			// perform Bose-Einstein integral for massive pions, considering
			// two charged and one neutral degree of freedom
			double integral = 0;
			for(int j = 1; j < EnergySteps; j++)
			{
				integral += HadronicCrossSectionMb[j]
					* (2.0 * BEdist(EnergyMeV[j], PhysConst.ChargedPionMass)
						+ BEdist(EnergyMeV[j], PhysConst.NeutralPionMass))
					* EnergyMeV[j] * EnergyMeV[j]
					* (EnergyMeV[j + 1] - EnergyMeV[j - 1]);
			}

			// first step vanishes because CrossSection[0] = 0
			integral += HadronicCrossSectionMb[EnergySteps]
				* (2.0 * BEdist(EnergyMeV[EnergySteps], PhysConst.ChargedPionMass)
					+ BEdist(EnergyMeV[EnergySteps], PhysConst.NeutralPionMass))
				* EnergyMeV[EnergySteps] * EnergyMeV[EnergySteps]
				* (EnergyMeV[EnergySteps] - EnergyMeV[EnergySteps - 1]);
			integral *= 0.5;

			// MeV^3 * mb  -->  MeV^3 * 0.1 fm^2  -->  0.1 MeV
			return 0.5 / PhysConst.PI / PhysConst.PI * integral * 0.1 / PhysConst.HBARC / PhysConst.HBARC;
		}

		private double[] GetHadronicCrossSection()
		{
			double[] hadronicCrossSection = new double[EnergySteps + 1];

			// because CrossSection[0] = 0
			hadronicCrossSection[0] = 0;

			for(int i = 1; i <= EnergySteps; i++)
			{
				hadronicCrossSection[i] = 0;

				// bjorken x
				double[] dX = new double[i + 1];
				dX[0] = 0;
				for(int j = 1; j <= i; j++)
				{
					dX[j] = EnergyMeV[j] / EnergyMeV[i];
				}

				// integrate over dX
				for(int j = 1; j < i; j++)
				{
					hadronicCrossSection[i] += (dX[j + 1] - dX[j - 1])
						* PionGDF.GetValue(dX[j], EnergyMeV[j]) * CrossSectionMb[j];
				}

				// first step vanishes because CrossSection[0] = 0
				hadronicCrossSection[i] += (dX[i] - dX[i - 1])
					* PionGDF.GetValue(dX[i], EnergyMeV[i]) * CrossSectionMb[i];
				hadronicCrossSection[i] *= 0.5;
			}

			return hadronicCrossSection;
		}

		private double GetQGPDecayWidth()
		{
			// perform Bose-Einstein integral for massless gluons
			double integral = 0;
			for(int j = 1; j < EnergySteps; j++)
			{
				integral += CrossSectionMb[j] * BEdist(EnergyMeV[j])
					* EnergyMeV[j] * EnergyMeV[j]
					* (EnergyMeV[j + 1] - EnergyMeV[j - 1]);
			}

			// first step vanishes because CrossSection[0] = 0
			integral += CrossSectionMb[EnergySteps] * BEdist(EnergyMeV[EnergySteps])
				* EnergyMeV[EnergySteps] * EnergyMeV[EnergySteps]
				* (EnergyMeV[EnergySteps] - EnergyMeV[EnergySteps - 1]);
			integral *= 0.5;

			// include 16 gluonic degrees of freedom
			// MeV^3 * mb  -->  MeV^3 * 0.1 fm^2  -->  0.1 MeV
			return 16 * 0.5 / PhysConst.PI / PhysConst.PI * integral
				* 0.1 / PhysConst.HBARC / PhysConst.HBARC;
		}

		// Check at the maximum energy value.
		private void CheckStepSizeSmallEnough()
		{
			int l = BoundParam.QuantumNumberL;
			if(l == 0)
			{
				GetFreeWaveFunction(MaxEnergyMeV, 1);
			}
			else
			{
				GetFreeWaveFunction(MaxEnergyMeV, l - 1);
				GetFreeWaveFunction(MaxEnergyMeV, l + 1);
			}
		}

		private double[] GetCrossSection()
		{
			double[] crossSection = new double[EnergySteps + 1];

			crossSection[0] = 0;
			for(int j = 1; j <= EnergySteps; j++)
			{
				if(CalculationCancelToken.IsCancellationRequested)
				{
					break;
				}

				LogInfo(j);

				// the unit is mb (factor of 10 is for conversion to from fm^-2 to mb)
				crossSection[j] =
					10 * PhysConst.PI * PhysConst.PI / 9.0 * BoundState.AlphaUltraSoft
					* BoundParam.QuarkMassMeV / PhysConst.HBARC
					* EnergyMeV[j] / Math.Sqrt(BoundParam.QuarkMassMeV
					* (EnergyMeV[j] - MinEnergyMeV))
					* RadialOverlapIntegral(EnergyMeV[j]);
			}

			return crossSection;
		}

		private void LogInfo(
			int index
			)
		{
			StatusValues[0] = MinEnergyMeV.ToString("G4");
			StatusValues[1] = EnergyMeV[index].ToString("G4");
			StatusValues[2] = MaxEnergyMeV.ToString("G4");
		}

		// in fm^3
		private double RadialOverlapIntegral(
			double energyMeV
			)
		{
			int l = BoundParam.QuantumNumberL;
			if(l == 0)
			{
				return Math.Pow(ComplexMath.Abs(RadialOverlapIntegral(energyMeV, 1)), 2);
			}
			else
			{
				return ((l + 1) * Math.Pow(ComplexMath.Abs(RadialOverlapIntegral(energyMeV, l + 1)), 2)
					+ l * Math.Pow(ComplexMath.Abs(RadialOverlapIntegral(energyMeV, l - 1)), 2))
					/ (2 * l + 1.0);
			}
		}

		// in fm^3/2
		private Complex RadialOverlapIntegral(
			double gluonEnergyMeV,
			int quantumNumberL
			)
		{
			Complex[] freeWave = GetFreeWaveFunction(gluonEnergyMeV, quantumNumberL);
			int n = BoundParam.StepNumber;
			Complex integral = 0;
			for(int j = 1; j < n; j++)
			{
				integral += RadiusFm[j] * BoundWave[j] * freeWave[j].Conjugate;
			}

			integral += 0.5 * (RadiusFm[n] * BoundWave[n] * freeWave[n].Conjugate
				+ RadiusFm[0] * BoundWave[0] * freeWave[0].Conjugate);

			return integral * BoundState.StepSizeFm;
		}

		private Complex[] GetFreeWaveFunction(
			double gluonEnergyMeV,
			int quantumNumberL
			)
		{
			QQFreeState freeState = new QQFreeState(
				GetFreeStateParam(gluonEnergyMeV, quantumNumberL));
			freeState.CalculationCancelToken = CalculationCancelToken;
			freeState.SearchEigenfunction();

			return freeState.WaveFunctionFm;
		}

		private QQStateParam GetFreeStateParam(
			double gluonEnergyMeV,
			int quantumNumberL
			)
		{
			QQStateParam param = BoundParam.Clone();
			param.AccuracyAlpha = 1e-6;
			param.AccuracyWaveFunction = 1e-9;
			param.AggressivenessAlpha = 0.5;
			param.ColorState = ColorState.Octet;
			param.EnergyMeV = gluonEnergyMeV - MinEnergyMeV;
			param.GammaDampMeV = -GammaDampMeV / 8.0; /*GammaDampMeV = 0*/
			param.MaxShootingTrials = 0;
			param.PotentialType = PotentialType.Tzero_NoString;
			param.QuantumNumberL = quantumNumberL;
			param.TemperatureMeV = TemperatureMeV;

			return param;
		}

		// Bose-Einstein distribution for massless particles
		private double BEdist(
			double energyMeV
			)
		{
			return 1.0 / (Math.Exp(energyMeV / TemperatureMeV) - 1.0);
		}

		// Bose-Einstein distribution for massive particles
		private double BEdist(
			double energyMeV,
			double massMeV
			)
		{
			return 1.0 / (
				Math.Exp(Math.Sqrt(energyMeV * energyMeV + massMeV * massMeV) / TemperatureMeV) - 1.0);
		}
	}
}