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
		public static readonly double LambdaQCDMeV = 267.324998266534;

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

			WaveFunctionFm = new Complex[Param.StepNumber + 1];
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

		public double DebyeMassMeV
		{
			get;
			protected set;
		}

		public double StepSizeFm
		{
			get
			{
				return Solver.StepSize;
			}
		}

		public double WaveVectorFm
		{
			get
			{
				return Math.Sqrt(Param.QuarkMassMeV * Math.Abs(Param.EnergyMeV))
					/ Constants.HbarCMeVFm;
			}
		}

		// values of the radial wave function, psi_nlm(x,y,z) = WaveFunction(r)*Y_lm(theta,phi)/r
		public Complex[] WaveFunctionFm
		{
			get;
			protected set;
		}

		// values of radial distance at which the wave function is evaluated
		public double[] RadiusFm
		{
			get;
			protected set;
		}

		public double SigmaEffMeV
		{
			get
			{
				return PotentialFm.SigmaEffMeV;
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

		protected Potential PotentialFm;

		protected RseSolver Solver;

		protected Complex EffectivePotential(
			double radiusFm
			)
		{
			double quarkMassFm = Param.QuarkMassMeV / Constants.HbarCMeVFm;
			double centrifugalTerm = Param.QuantumNumberL * (Param.QuantumNumberL + 1)
				/ radiusFm / radiusFm;

			return quarkMassFm * PotentialFm.Value(radiusFm) + new Complex(centrifugalTerm, 0);
		}

		protected Complex EffectivePotentialMinusEigenvalue(
			double radiusFm
			)
		{
			Complex eigenvalue = (new Complex(Param.EnergyMeV, -0.5 * Param.GammaDampMeV))
				* Param.QuarkMassMeV / Constants.HbarCMeVFm / Constants.HbarCMeVFm;

			return EffectivePotential(radiusFm) - eigenvalue;
		}

		private void SetDebyeMass()
		{
			DebyeMassMeV = Param.TemperatureMeV * Math.Sqrt(2 * Math.PI
				* AlphaThermal * (2 * Constants.NumberQCDColors + NumberLightFlavors) / 3.0);
		}

		private void SetCouplings()
		{
			AlphaS = RunningCoupling.Create(Param.RunningCouplingType);
			AlphaHard = AlphaS.Value(Param.QuarkMassMeV);
			AlphaSoft = AlphaS.Value(Param.SoftScaleMeV);
			AlphaThermal = Param.TemperatureMeV == 0 ?
				0 : AlphaS.Value(2 * Math.PI * Param.TemperatureMeV);
		}

		private void SetPotential()
		{
			switch(Param.PotentialType)
			{
				case PotentialType.Complex:
					PotentialFm = new ComplexPotential(AlphaSoft, Param.SigmaMeV,
						Param.ColorState, Param.TemperatureMeV, DebyeMassMeV);
					break;

				case PotentialType.Complex_NoString:
					PotentialFm = new ComplexPotential_NoString(AlphaSoft,
						Param.ColorState, Param.TemperatureMeV, DebyeMassMeV);
					break;

				case PotentialType.LowT:
					PotentialFm = new LowTemperaturePotential(AlphaSoft, Param.SigmaMeV,
						Param.ColorState, Param.TemperatureMeV, DebyeMassMeV);
					break;

				case PotentialType.LowT_NoString:
					PotentialFm = new LowTemperaturePotential_NoString(AlphaSoft,
						Param.ColorState, Param.TemperatureMeV, DebyeMassMeV);
					break;

				case PotentialType.Real:
					PotentialFm = new RealPotential(
						AlphaSoft, Param.SigmaMeV, Param.ColorState, DebyeMassMeV);
					break;

				case PotentialType.Real_NoString:
					PotentialFm = new RealPotential_NoString(AlphaSoft,
						Param.ColorState, DebyeMassMeV);
					break;

				case PotentialType.Tzero:
					PotentialFm = new VacuumPotential(
						AlphaSoft, Param.SigmaMeV, Param.ColorState);
					break;

				case PotentialType.Tzero_NoString:
					PotentialFm = new VacuumPotential_NoString(AlphaSoft, Param.ColorState);
					break;

				case PotentialType.SpinDependent:
					PotentialFm = new SpinDependentPotential(
						AlphaSoft, Param.SigmaMeV, Param.ColorState, Param.SpinState,
						Param.SpinCouplingRangeFm, Param.SpinCouplingStrengthMeV);
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
			if(Param.GammaDampMeV < 0
				&& Param.ColorState == ColorState.Singlet)
			{
				errorMessage += "GammaDamp < 0.\r\n";
			}
			if(Param.GammaDampMeV > 0
				&& Param.ColorState == ColorState.Octet)
			{
				errorMessage += "GammaDamp > 0.\r\n";
			}
			if(Param.MaxRadiusFm <= 0)
			{
				errorMessage += "MaxRadius <= 0.\r\n";
			}
			if(Param.QuantumNumberL < 0)
			{
				errorMessage += "QuantumNumberL < 0.\r\n";
			}
			if(Param.QuarkMassMeV <= 0)
			{
				errorMessage += "QuarkMass <= 0.\r\n";
			}
			if(Param.SoftScaleMeV < 0)
			{
				errorMessage += "SoftScale < 0.\r\n";
			}
			if(Param.StepNumber < 1)
			{
				errorMessage += "StepNumber < 1.\r\n";
			}
			if(Param.TchemMeV <= 0)
			{
				errorMessage += "Tchem <= 0.\r\n";
			}
			if(Param.TcritMeV < Param.TchemMeV)
			{
				errorMessage += "Tcrit < Tchem.\r\n";
			}
			if(Param.TemperatureMeV < 0)
			{
				errorMessage += "Temperature < 0.\r\n";
			}
			if(Param.TemperatureMeV < Param.TcritMeV
				&& Param.PotentialType != PotentialType.Tzero
				&& Param.PotentialType != PotentialType.Tzero_NoString
				&& Param.PotentialType != PotentialType.SpinDependent)
			{
				errorMessage += "PotentialType incompatible with T.\r\n";
			}

			if(!string.IsNullOrEmpty(errorMessage))
			{
				throw new Exception(errorMessage);
			}
		}
	}
}
