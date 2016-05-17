using Meta.Numerics;
using Meta.Numerics.Functions;
using System;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public class QQFreeState : QQState
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQFreeState(
			QQStateParam param
			)
			: base(param)
		{
			InitSolver();
			AssertInputValid();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// successive change of Energy and GammaDamp in order to find
		// the eigenfunction of the radial hamiltonian
		public override void SearchEigenfunction()
		{
			Trials = 0;

			if(PotentialFm.IsReal)
			{
				Param.GammaDampMeV = 0;
			}

			CalculateWaveFunction();
		}

		/********************************************************************************************
		* Private/protected static members, functions and properties
		********************************************************************************************/

		private static readonly double DesiredDegreeOfConvergence = 1e-6;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double LastMaximum = double.Epsilon;

		private double CurrentMaximum = 0;

		private double DegreeOfConvergence;

		private double MaxStepSizeFm
		{
			get
			{
				return 3e-3 * 2 * Math.PI / WaveVectorFm;
			}
		}

		private void AssertInputValid()
		{
			if(StepSizeFm > MaxStepSizeFm)
			{
				throw new Exception("StepSize > MaxStepSize.");
			}
			if(Param.PotentialType != PotentialType.Tzero_NoString)
			{
				throw new Exception("Only PotentialType.Tzero_NoString is currently supported.");
			}
			if(Param.EnergyMeV < 0)
			{
				throw new Exception("Energy < 0.");
			}
			if(StatusValues != null && StatusValues.Length != 4)
			{
				throw new Exception("Length of StatusValues must be four.");
			}
		}

		private void InitSolver()
		{
			Solver = new RseSolver();
			Solver.InitialPosition = 0;
			Solver.FinalPosition = Param.MaxRadiusFm;
			Solver.Samples = Param.StepNumber;
			Solver.RightHandSide = EffectivePotentialMinusEigenvalue;
			Solver.Initialize();

			RadiusFm = Solver.PositionValues;

			SetSolverInitialValues();
			Solver.Initialize();
		}

		// Since, for L>0, Psi(0) = dPsi/dr(0) = 0, one has to evaluate the first step via
		// a series expansion and shrink the domain of calculation by one step size
		private void SetSolverInitialValues()
		{
			Complex initWave;
			Complex initDeriv;
			FirstStepSeriesExpansion(out initWave, out initDeriv);
			Solver.InitialSolutionValue = initWave;
			Solver.InitialDerivativeValue = initDeriv;

			Solver.InitialPosition += StepSizeFm;
			Solver.Samples -= 1;
		}

		// Loop for solving the radial schroedinger equation for continuum states.
		// The values of the maxima of |Psi| are also evaluated. They have to converge
		// for the subsequent normalization by the free wave function to make sense.
		protected void CalculateWaveFunction()
		{
			Trials++;

			Solver.Solve();
			BuildWaveFunction();

			while(!IsConverging()
				&& !CalculationCancelToken.IsCancellationRequested)
			{
				UpdateStatusValues();
				SolveInExtendedRegion();
			}

			NormalizeWaveFunction();
		}

		private void BuildWaveFunction()
		{
			WaveFunctionFm[0] = 0;
			for(int j = 1; j <= Param.StepNumber; j++)
			{
				WaveFunctionFm[j] = Solver.SolutionValues[j - 1];
			}
		}

		private bool IsConverging()
		{
			double[] radius = Solver.PositionValues;
			Complex[] waveFunction = Solver.SolutionValues;

			double lastValue = ComplexMath.Abs(waveFunction[0]);
			double currentValue = ComplexMath.Abs(waveFunction[1]);
			double nextValue = 0;

			for(int j = 1; j < waveFunction.Length - 1; j++)
			{
				nextValue = ComplexMath.Abs(waveFunction[j]);
				if(currentValue >= lastValue
					&& currentValue >= nextValue)
				{
					LastMaximum = CurrentMaximum;
					CurrentMaximum = FindExtremum(radius[j - 1], radius[j], radius[j + 1],
						lastValue, currentValue, nextValue);

					DegreeOfConvergence = Math.Abs(CurrentMaximum / LastMaximum - 1.0);
					if(DegreeOfConvergence <= DesiredDegreeOfConvergence)
					{
						return true;
					}
				}
				lastValue = currentValue;
				currentValue = nextValue;
			}

			return false;
		}

		private double FindExtremum(
			double xa,
			double xb,
			double xc,
			double ya,
			double yb,
			double yc
			)
		{
			// take three arguments xa, xb, xc and the corresponding function values ya, yb, yc
			// and evaluate the extremum by approximating the function by a parabola approximation
			// f(x)  = yc (x-xa)(x-xb)/(xc-xa)/(xc-xb) + ya (x-xb)(x-xc)/(xa-xb)/(xa-xc)
			//      + yb (x-xc)(x-xa)/(xb-xc)/(xb-xa)
			// f'(x) = 2 x [ yc/(xc-xa)/(xc-xb) + ya/(xa-xb)/(xa-xc) + yb/(xb-xc)/(xb-xa) ]
			//      - yc (xa+xb)/(xc-xa)/(xc-xb) - ya (xb+xc)/(xa-xb)/(xa-xc)
			//      - yb (xc+xa)/(xb-xc)/(xb-xa)
			double ab = xa - xb, bc = xb - xc, ca = xc - xa;
			double ycab = yc * ab, yabc = ya * bc, ybca = yb * ca;
			double xtrm = 0.5 * (ycab * (xa + xb) + yabc * (xb + xc) + ybca * (xc + xa))
				/ (ycab + yabc + ybca);

			return -(ycab * (xtrm - xa) * (xtrm - xb) + yabc * (xtrm - xb) * (xtrm - xc)
				+ ybca * (xtrm - xc) * (xtrm - xa)) / ab / bc / ca;
		}

		private void SolveInExtendedRegion()
		{
			ReinitSolverInExtendedRegion();
			Solver.Solve();
		}

		private void ReinitSolverInExtendedRegion()
		{
			Solver.SetFinalValuesAsNewInitialValues();
			Solver.Samples = Param.StepNumber;
			Solver.InitialPosition = Solver.FinalPosition;
			Solver.FinalPosition = Solver.InitialPosition + MaxStepSizeFm * Solver.Samples;

			Solver.Initialize();
		}

		private void NormalizeWaveFunction()
		{
			double normFactor = Math.Sqrt(2 / Math.PI) / CurrentMaximum;
			for(int j = 0; j <= Param.StepNumber; j++)
			{
				WaveFunctionFm[j] *= normFactor;
			}
		}

		private void FirstStepSeriesExpansion(
			out Complex wave,
			out Complex deriv
			)
		{
			int MAX = 10;
			double[] A = new double[MAX];
			double DebyeSum = 0;
			double dx = WaveVectorFm * StepSizeFm;
			double DK = DebyeMassMeV / WaveVectorFm / PhysConst.HBARC;
			double ZK = PotentialFm.AlphaEff * Param.QuarkMassMeV / WaveVectorFm / PhysConst.HBARC;
			double SK = DebyeMassMeV == 0 ? 0
				: SigmaEffMeV / DebyeMassMeV / PhysConst.HBARC / WaveVectorFm / WaveVectorFm;

			A[0] = 1.0;
			A[1] = 0.5 * ZK / (Param.QuantumNumberL + 1.0);
			for(int j = 2; j < MAX; j++)
			{
				for(int k = 0; k <= j - 2; k++)
				{
					DebyeSum += Math.Pow(-DK, k) / AdvancedIntegerMath.Factorial(k)
						* (SK + DK * ZK / (k + 1.0)) * A[j - 2 - k];
				}

				A[j] = (ZK * A[j - 1] - A[j - 2] - DebyeSum)
					/ (j + 2.0 * Param.QuantumNumberL + 1.0) / j;
			}

			if(A[MAX - 1] * Math.Pow(dx, MAX - 1) > 1e-14)
			{
				throw new Exception("Last term is suspiciously large.");
			}

			double sum = 0;
			double dsum = 0;

			for(int j = 0; j < MAX; j++)
			{
				// x = WaveVector*Radius
				sum += A[j] * Math.Pow(dx, j + Param.QuantumNumberL + 1);
				dsum += (j + Param.QuantumNumberL + 1) * A[j]
					* Math.Pow(dx, j + Param.QuantumNumberL);
			}

			wave = new Complex(sum, 0);
			deriv = new Complex(dsum * WaveVectorFm, 0); // <- factor of WaveVector is important
		}

		protected void UpdateStatusValues()
		{
			if(StatusValues != null)
			{
				StatusValues[0] = Solver.FinalPosition.ToString("G4");
				StatusValues[1] = LastMaximum.ToString("G6");
				StatusValues[2] = CurrentMaximum.ToString("G6");
				StatusValues[3] = DegreeOfConvergence.ToString("G6");
			}
		}
	}
}