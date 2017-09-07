/**************************************************************************************************
 * Pion masses are taken from K.A. Olive et al. (Particle Data Group), Chin. Phys. C38, 090001
 * (2014)
 **************************************************************************************************/

using Meta.Numerics;
using System;
using System.Collections.Generic;
using System.Threading;
using Yburn.PhysUtil;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public class DecayWidth
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public DecayWidth(
			CancellationToken calculationCancelToken,
			QQBoundState boundState,
			double maxEnergy_MeV,
			int energySteps,
			double temperature_MeV,
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
			if(temperature_MeV < boundState.Param.Tchem_MeV)
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
			BoundWave = BoundState.WaveFunction_fm;
			Radius_fm = BoundState.Radius_fm;
			Temperature_MeV = temperature_MeV;
			MaxEnergy_MeV = maxEnergy_MeV;
			EnergySteps = energySteps;
			MinEnergy_MeV = GetMinEnergy();

			if(MaxEnergy_MeV <= MinEnergy_MeV)
			{
				throw new Exception("Invalid energy range.");
			}

			GammaDamp_MeV = 0;

			// to be calculated after the cross section
			GammaDiss_MeV = -1;

			CheckStepSizeSmallEnough();

			// domain of calculcation is divided into EnergySteps cells
			CrossSectionMb = new double[EnergySteps + 1];
			HadronicCrossSectionMb = new double[EnergySteps + 1];
			Energy_MeV = GetEnergyValueArray();

			StatusValues = statusValues;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// partial decay width due to gluodissociation
		public double GammaDiss_MeV
		{
			get;
			private set;
		}

		public List<string> CrossSectionStringList
		{
			get
			{
				List<string> dataList = new List<string>
				{
					string.Format("{0,-18}{1,-18}{2,-18}", "#E (MeV)", "SigmaG (mb)", "SigmaPi (mb)")
				};

				for(int j = 0; j <= EnergySteps; j++)
				{
					dataList.Add(string.Format("{0,-18}{1,-18}{2,-18}",
							Energy_MeV[j].ToString("G10"),
							CrossSectionMb[j].ToString("G10"),
							HadronicCrossSectionMb[j].ToString("G10")));
				}

				return dataList;
			}
		}

		// Calculates the cross section CrossSection in the energy interval MinEnergy - MaxEnergy
		// and subsequently the gluodissociation decay width GammaDiss.
		public void CalculateGammaDiss()
		{
			CrossSectionMb = GetCrossSection();

			if(!CalculationCancelToken.IsCancellationRequested)
			{
				if(Temperature_MeV >= BoundParam.Tcrit_MeV)
				{
					// we have a QGP
					GammaDiss_MeV = GetQGPDecayWidth();
				}
				else if(Temperature_MeV >= BoundParam.Tchem_MeV)
				{
					// we have a hadronic medium
					GammaDiss_MeV = GetHadronicDecayWidth();
				}
				else
				{
					// zero decay width below the chemical freeze-out temperature
					GammaDiss_MeV = 0;
				}
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double MinEnergy_MeV;

		private double MaxEnergy_MeV;

		private int EnergySteps;

		private double[] Energy_MeV;

		// partial decay width due to landau damping
		protected double GammaDamp_MeV;

		private double[] CrossSectionMb;

		private double[] HadronicCrossSectionMb;

		private QQStateParam BoundParam;

		// radial wave function of the bound state that gets dissociated in fm^-1/2
		private Complex[] BoundWave;

		private QQBoundState BoundState;

		// values of radial distance at which the wave functions are evaluated
		private double[] Radius_fm;

		private double Temperature_MeV;

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
					return 2 * Constants.RestMassBMeson_MeV - BoundState.BoundMass_MeV;

				default:
					// we have a QGP
					return Math.Abs(BoundParam.Energy_MeV);
			}
		}

		private double[] GetEnergyValueArray()
		{
			double[] energy = new double[EnergySteps + 1];

			double dE = Math.Exp(Math.Log(MaxEnergy_MeV / MinEnergy_MeV) / EnergySteps);
			energy[0] = MinEnergy_MeV;

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
					* (2.0 * BEdist(Energy_MeV[j], Constants.RestMassPionPlus_MeV)
						+ BEdist(Energy_MeV[j], Constants.RestMassPionZero_MeV))
					* Energy_MeV[j] * Energy_MeV[j]
					* (Energy_MeV[j + 1] - Energy_MeV[j - 1]);
			}

			// first step vanishes because CrossSection[0] = 0
			integral += HadronicCrossSectionMb[EnergySteps]
				* (2.0 * BEdist(Energy_MeV[EnergySteps], Constants.RestMassPionPlus_MeV)
					+ BEdist(Energy_MeV[EnergySteps], Constants.RestMassPionZero_MeV))
				* Energy_MeV[EnergySteps] * Energy_MeV[EnergySteps]
				* (Energy_MeV[EnergySteps] - Energy_MeV[EnergySteps - 1]);
			integral *= 0.5;

			// MeV^3 * mb  -->  MeV^3 * 0.1 fm^2  -->  0.1 MeV
			return 0.5 / Math.PI / Math.PI * integral * 0.1 / Constants.HbarC_MeV_fm / Constants.HbarC_MeV_fm;
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
					dX[j] = Energy_MeV[j] / Energy_MeV[i];
				}

				// integrate over dX
				for(int j = 1; j < i; j++)
				{
					hadronicCrossSection[i] += (dX[j + 1] - dX[j - 1])
						* PionGDF.GetValue(dX[j], Energy_MeV[j]) * CrossSectionMb[j];
				}

				// first step vanishes because CrossSection[0] = 0
				hadronicCrossSection[i] += (dX[i] - dX[i - 1])
					* PionGDF.GetValue(dX[i], Energy_MeV[i]) * CrossSectionMb[i];
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
				integral += CrossSectionMb[j] * BEdist(Energy_MeV[j])
					* Energy_MeV[j] * Energy_MeV[j]
					* (Energy_MeV[j + 1] - Energy_MeV[j - 1]);
			}

			// first step vanishes because CrossSection[0] = 0
			integral += CrossSectionMb[EnergySteps] * BEdist(Energy_MeV[EnergySteps])
				* Energy_MeV[EnergySteps] * Energy_MeV[EnergySteps]
				* (Energy_MeV[EnergySteps] - Energy_MeV[EnergySteps - 1]);
			integral *= 0.5;

			// include 16 gluonic degrees of freedom
			// MeV^3 * mb  -->  MeV^3 * 0.1 fm^2  -->  0.1 MeV
			return 16 * 0.5 / Math.PI / Math.PI * integral
				* 0.1 / Constants.HbarC_MeV_fm / Constants.HbarC_MeV_fm;
		}

		// Check at the maximum energy value.
		private void CheckStepSizeSmallEnough()
		{
			int l = BoundParam.QuantumNumberL;
			if(l == 0)
			{
				GetFreeWaveFunction(MaxEnergy_MeV, 1);
			}
			else
			{
				GetFreeWaveFunction(MaxEnergy_MeV, l - 1);
				GetFreeWaveFunction(MaxEnergy_MeV, l + 1);
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
				crossSection[j]
					= 10 * Math.PI * Math.PI / 9.0 * BoundState.AlphaUltraSoft
					* BoundParam.QuarkMass_MeV / Constants.HbarC_MeV_fm
					* Energy_MeV[j] / Math.Sqrt(BoundParam.QuarkMass_MeV
					* (Energy_MeV[j] - MinEnergy_MeV))
					* RadialOverlapIntegral(Energy_MeV[j]);
			}

			return crossSection;
		}

		private void LogInfo(
			int index
			)
		{
			StatusValues[0] = MinEnergy_MeV.ToString("G4");
			StatusValues[1] = Energy_MeV[index].ToString("G4");
			StatusValues[2] = MaxEnergy_MeV.ToString("G4");
		}

		// in fm^3
		private double RadialOverlapIntegral(
			double energy_MeV
			)
		{
			int l = BoundParam.QuantumNumberL;
			if(l == 0)
			{
				return Math.Pow(ComplexMath.Abs(RadialOverlapIntegral(energy_MeV, 1)), 2);
			}
			else
			{
				return ((l + 1) * Math.Pow(ComplexMath.Abs(RadialOverlapIntegral(energy_MeV, l + 1)), 2)
					+ l * Math.Pow(ComplexMath.Abs(RadialOverlapIntegral(energy_MeV, l - 1)), 2))
					/ (2 * l + 1.0);
			}
		}

		// in fm^3/2
		private Complex RadialOverlapIntegral(
			double gluonEnergy_MeV,
			int quantumNumberL
			)
		{
			Complex[] freeWave = GetFreeWaveFunction(gluonEnergy_MeV, quantumNumberL);
			int n = BoundParam.StepNumber;
			Complex integral = 0;
			for(int j = 1; j < n; j++)
			{
				integral += Radius_fm[j] * BoundWave[j] * freeWave[j].Conjugate;
			}

			integral += 0.5 * (Radius_fm[n] * BoundWave[n] * freeWave[n].Conjugate
				+ Radius_fm[0] * BoundWave[0] * freeWave[0].Conjugate);

			return integral * BoundState.StepSize_fm;
		}

		private Complex[] GetFreeWaveFunction(
			double gluonEnergy_MeV,
			int quantumNumberL
			)
		{
			QQFreeState freeState = new QQFreeState(GetFreeStateParam(gluonEnergy_MeV, quantumNumberL))
			{
				CalculationCancelToken = CalculationCancelToken
			};

			freeState.SearchEigenfunction();

			return freeState.WaveFunction_fm;
		}

		private QQStateParam GetFreeStateParam(
			double gluonEnergy_MeV,
			int quantumNumberL
			)
		{
			QQStateParam param = BoundParam.Clone();
			param.AccuracyAlpha = 1e-6;
			param.AccuracyWaveFunction = 1e-9;
			param.AggressivenessAlpha = 0.5;
			param.AggressivenessEnergy = quantumNumberL > 0 ? 0.02 : 40;
			param.ColorState = ColorState.Octet;
			param.Energy_MeV = gluonEnergy_MeV - MinEnergy_MeV;
			param.GammaDamp_MeV = -GammaDamp_MeV / 8.0; /*GammaDamp_MeV = 0*/
			param.MaxShootingTrials = 0;
			param.PotentialType = PotentialType.Tzero_NoString;
			param.QuantumNumberL = quantumNumberL;
			param.Temperature_MeV = Temperature_MeV;

			return param;
		}

		// Bose-Einstein distribution for massless particles
		private double BEdist(
			double energy_MeV
			)
		{
			return 1.0 / (Math.Exp(energy_MeV / Temperature_MeV) - 1.0);
		}

		// Bose-Einstein distribution for massive particles
		private double BEdist(
			double energy_MeV,
			double mass_MeV
			)
		{
			return 1.0 / (
				Math.Exp(Math.Sqrt(energy_MeV * energy_MeV + mass_MeV * mass_MeV) / Temperature_MeV) - 1.0);
		}
	}
}
