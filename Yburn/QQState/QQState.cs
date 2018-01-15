using Meta.Numerics;
using System;
using System.Threading;
using Yburn.PhysUtil;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum ColorState
	{
		Singlet,
		Octet
	};

	public enum SpinState
	{
		Singlet,
		Triplet,
	};

	public abstract class QQState
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		// LambdaQCD = M_Z exp(-1/(b0 alpha_s(M_Z))),
		// M_Z = 91.2 GeV, alpha_s(M_Z) = 0.1197, b0 = 9/(2*PI)
		public static readonly double LambdaQCD_MeV = 267.324998266534;

		public static readonly int NumberLightFlavors = 3;

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQState(
			QQStateParam param
			)
		{
			Param = param.Clone();
			AssertValidParam();

			SetCouplings();
			SetDebyeMass();
			SetPotential();

			WaveFunction_fm = new Complex[Param.StepNumber + 1];
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public CancellationToken CalculationCancelToken;

		// information about the current state of the calculation to be communicated to the outside
		public string[] StatusValues;

		public QQStateParam Param
		{
			get;
			protected set;
		}

		// RunningCoupling(QuarkMass), hard scale
		public double AlphaHard
		{
			get;
			protected set;
		}

		// RunningCoupling(SoftScale), soft scale
		public double AlphaSoft
		{
			get;
			protected set;
		}

		// RunningCoupling(2*PI*T), thermal scale
		public double AlphaThermal
		{
			get;
			protected set;
		}

		public double DebyeMass_MeV
		{
			get;
			protected set;
		}

		public double StepSize_fm
		{
			get
			{
				return Solver.StepSize;
			}
		}

		public double WaveVector_fm
		{
			get
			{
				return Math.Sqrt(Param.QuarkMass_MeV * Math.Abs(Param.Energy_MeV))
					/ Constants.HbarC_MeV_fm;
			}
		}

		// values of the radial wave function, psi_nlm(x,y,z) = WaveFunction(r)*Y_lm(theta,phi)/r
		public Complex[] WaveFunction_fm
		{
			get;
			protected set;
		}

		// values of radial distance at which the wave function is evaluated
		public double[] Radius_fm
		{
			get;
			protected set;
		}

		public double SigmaEff_MeV
		{
			get
			{
				return Potential_fm.SigmaEff_MeV;
			}
		}

		// trials used so far to find the desired wave function
		public int Trials
		{
			get;
			protected set;
		}

		// successive change of Energy and GammaDamp in order to find
		// the eigenfunction of the radial hamiltonian
		public abstract void SearchEigenfunction();

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		// utility function to get the norm |z|^2 of an arbitrary complex number z
		protected static double Norm(
			Complex z
			)
		{
			double abs = ComplexMath.Abs(z);
			return abs * abs;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected RunningCoupling AlphaS;

		protected Potential Potential_fm;

		protected RseSolver Solver;

		protected Complex EffectivePotential(
			double radius_fm
			)
		{
			double quarkMass_fm = Param.QuarkMass_MeV / Constants.HbarC_MeV_fm;
			double centrifugalTerm = Param.QuantumNumberL * (Param.QuantumNumberL + 1)
				/ radius_fm / radius_fm;

			return quarkMass_fm * Potential_fm.Value(radius_fm) + new Complex(centrifugalTerm, 0);
		}

		protected Complex EffectivePotentialMinusEigenvalue(
			double radius_fm
			)
		{
			Complex eigenvalue = (new Complex(Param.Energy_MeV, -0.5 * Param.GammaDamp_MeV))
				* Param.QuarkMass_MeV / Constants.HbarC_MeV_fm / Constants.HbarC_MeV_fm;

			return EffectivePotential(radius_fm) - eigenvalue;
		}

		private void SetDebyeMass()
		{
			DebyeMass_MeV = Param.Temperature_MeV * Math.Sqrt(2 * Math.PI
				* AlphaThermal * (2 * Constants.NumberQCDColors + NumberLightFlavors) / 3.0);
		}

		private void SetCouplings()
		{
			AlphaS = RunningCoupling.Create(Param.RunningCouplingType);
			AlphaHard = AlphaS.Value(Param.QuarkMass_MeV);
			AlphaSoft = AlphaS.Value(Param.SoftScale_MeV);
			AlphaThermal = Param.Temperature_MeV == 0 ?
				0 : AlphaS.Value(2 * Math.PI * Param.Temperature_MeV);
		}

		private void SetPotential()
		{
			switch(Param.PotentialType)
			{
				case PotentialType.Complex:
					Potential_fm = new ComplexPotential(AlphaSoft, Param.Sigma_MeV,
						Param.ColorState, Param.Temperature_MeV, DebyeMass_MeV);
					break;

				case PotentialType.Complex_NoString:
					Potential_fm = new ComplexPotential_NoString(AlphaSoft,
						Param.ColorState, Param.Temperature_MeV, DebyeMass_MeV);
					break;

				case PotentialType.LowT:
					Potential_fm = new LowTemperaturePotential(AlphaSoft, Param.Sigma_MeV,
						Param.ColorState, Param.Temperature_MeV, DebyeMass_MeV);
					break;

				case PotentialType.LowT_NoString:
					Potential_fm = new LowTemperaturePotential_NoString(AlphaSoft,
						Param.ColorState, Param.Temperature_MeV, DebyeMass_MeV);
					break;

				case PotentialType.Real:
					Potential_fm = new RealPotential(
						AlphaSoft, Param.Sigma_MeV, Param.ColorState, DebyeMass_MeV);
					break;

				case PotentialType.Real_NoString:
					Potential_fm = new RealPotential_NoString(AlphaSoft,
						Param.ColorState, DebyeMass_MeV);
					break;

				case PotentialType.Tzero:
					Potential_fm = new VacuumPotential(
						AlphaSoft, Param.Sigma_MeV, Param.ColorState);
					break;

				case PotentialType.Tzero_NoString:
					Potential_fm = new VacuumPotential_NoString(AlphaSoft, Param.ColorState);
					break;

				case PotentialType.SpinDependent:
					Potential_fm = new SpinDependentPotential(
						AlphaSoft, Param.Sigma_MeV, Param.ColorState, Param.SpinState,
						Param.SpinCouplingRange_fm, Param.SpinCouplingStrength_MeV);
					break;

				default:
					throw new Exception("Invalid PotentialType.");
			}
		}

		private void AssertValidParam()
		{
			string errorMessage = string.Empty;
			if(Param.AccuracyAlpha <= 0)
			{
				throw new Exception("AccuracyAlpha <= 0.");
			}
			if(Param.AccuracyWaveFunction <= 0)
			{
				throw new Exception("AccuracyWave <= 0.");
			}
			if(Param.AggressivenessAlpha <= 0)
			{
				throw new Exception("AggressivenessAlpha <= 0.");
			}
			if(Param.AggressivenessAlpha > 1)
			{
				throw new Exception("AggressivenessAlpha > 1.");
			}
			if(Param.GammaDamp_MeV < 0 && Param.ColorState == ColorState.Singlet)
			{
				errorMessage += "GammaDamp < 0." + Environment.NewLine;
			}
			if(Param.GammaDamp_MeV > 0 && Param.ColorState == ColorState.Octet)
			{
				errorMessage += "GammaDamp > 0." + Environment.NewLine;
			}
			if(Param.MaxRadius_fm <= 0)
			{
				errorMessage += "MaxRadius <= 0." + Environment.NewLine;
			}
			if(Param.QuantumNumberL < 0)
			{
				errorMessage += "QuantumNumberL < 0." + Environment.NewLine;
			}
			if(Param.QuarkMass_MeV <= 0)
			{
				errorMessage += "QuarkMass <= 0." + Environment.NewLine;
			}
			if(Param.SoftScale_MeV < 0)
			{
				errorMessage += "SoftScale < 0." + Environment.NewLine;
			}
			if(Param.StepNumber < 1)
			{
				errorMessage += "StepNumber < 1." + Environment.NewLine;
			}
			if(Param.Tchem_MeV <= 0)
			{
				errorMessage += "Tchem <= 0." + Environment.NewLine;
			}
			if(Param.Tcrit_MeV < Param.Tchem_MeV)
			{
				errorMessage += "Tcrit < Tchem." + Environment.NewLine;
			}
			if(Param.Temperature_MeV < 0)
			{
				errorMessage += "Temperature < 0." + Environment.NewLine;
			}
			if(Param.Temperature_MeV < Param.Tcrit_MeV
				&& Param.PotentialType != PotentialType.Tzero
				&& Param.PotentialType != PotentialType.Tzero_NoString
				&& Param.PotentialType != PotentialType.SpinDependent)
			{
				errorMessage += "PotentialType incompatible with T." + Environment.NewLine;
			}

			if(!string.IsNullOrEmpty(errorMessage))
			{
				throw new Exception(errorMessage);
			}
		}
	}
}
