/**************************************************************************************************
 * The RseShootingSolver utilizes a shooting method to solve for the eigenfunction of the stationary
 * radial Schrödinger equation, i.e. a second order ordinary differential equation of the type
 *
 *		z''(x) = (f(x) - E) z(x),
 *
 * where x is real while f, E and z may be complex. In particular, z is the radial part of the wave
 * function, x is the relative coordinate and E the eigenvalue of the Schroedinger equation.
 * The shooting algorithm is performed by varying E in the complex plane. It stops when the desired
 * accuracy is achieved or the maximum number of trials is reached. A SolutionAccuracyMeasure has to
 * be given in order to determine the current deviation from the exact solution.
 * If the solution needs to satisfy a certain condition, one may specify a SolutionConstraint. The
 * shooting gets aborted when this SolutionCondition is violated.
 * Furthermore, one may specify an ActionAfterIteration, i.e. a set of instruction that is performed
 * after every shoot.
 **************************************************************************************************/

using Meta.Numerics;
using System;
using System.Threading;

namespace Yburn.QQState
{
	public delegate double SolutionAccuracyMeasure();

	public delegate bool SolutionConstraint();

	public delegate void ActionAfterIteration();

	public class RseShootingSolver
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public RseShootingSolver()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Solve()
		{
			Initialize();

			CalculateSolution();
			LastAccuracy = CurrentAccuracy;
			LastEigenvalue = CurrentEigenvalue;

			while(!IsAccuracySufficient
				&& !CancellationToken.IsCancellationRequested)
			{
				ThrowIfTooManyAttempts();
				ThrowIfSolutionConstraintViolated();

				ImproveSolution();
			}
		}

		public SolutionAccuracyMeasure SolutionAccuracyMeasure;

		public SolutionConstraint SolutionConstraint;

		public ActionAfterIteration ActionAfterIteration;

		public RseSolver Solver;

		public double DesiredAccuracy;

		public double Aggressiveness;

		public int MaxTrials;

		public Complex Eigenvalue
		{
			get
			{
				return CurrentEigenvalue;
			}
			set
			{
				CurrentEigenvalue = value;
			}
		}

		public int Trials
		{
			get;
			private set;
		}

		public CancellationToken CancellationToken;

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static Complex TurnBackwards(
			Complex currentDirection
			)
		{
			return -currentDirection;
		}

		private static Complex TurnLeft(
			Complex currentDirection
			)
		{
			return Complex.I * currentDirection;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void Initialize()
		{
			AssertValidInput();

			Trials = 0;

			DirectionOfNextStep = new Complex(1, 0);
			SolverEffectivePotential = Solver.RightHandSide;
			Solver.RightHandSide = ModifiedPotential;
		}

		private void AssertValidInput()
		{
			if(DesiredAccuracy <= 0)
			{
				throw new NonPositiveAccuracyException();
			}

			if(Aggressiveness == 0)
			{
				throw new ZeroAggressivenessException();
			}

			if(SolutionAccuracyMeasure == null)
			{
				throw new MissingAccuracyMeasureException();
			}

			if(Solver == null)
			{
				throw new MissingRseSolverException();
			}
		}

		private Complex DirectionOfNextStep;

		private ComplexFunction SolverEffectivePotential;

		private Complex ModifiedPotential(
			double x
			)
		{
			return SolverEffectivePotential(x) - CurrentEigenvalue;
		}

		private Complex CurrentEigenvalue;

		private Complex LastEigenvalue;

		private double LastAccuracy;

		private double CurrentAccuracy;

		private bool IsAccuracySufficient
		{
			get
			{
				return CurrentAccuracy <= DesiredAccuracy;
			}
		}

		private void CalculateSolution()
		{
			Solver.Solve();
			Trials++;

			PerformActionAfterIteration();

			CurrentAccuracy = SolutionAccuracyMeasure();
		}

		private void ImproveSolution()
		{
			LastAccuracy = CurrentAccuracy;
			LastEigenvalue = CurrentEigenvalue;
			Complex stepSize = Aggressiveness * LastAccuracy;

			CurrentEigenvalue = LastEigenvalue + stepSize * DirectionOfNextStep;
			CalculateSolution();

			if(IsNewSolutionLessAccurate)
			{
				DirectionOfNextStep = TurnBackwards(DirectionOfNextStep);
				CurrentEigenvalue = LastEigenvalue + stepSize * DirectionOfNextStep;
				CalculateSolution();

				if(IsNewSolutionLessAccurate)
				{
					DirectionOfNextStep = TurnLeft(DirectionOfNextStep);
					CurrentEigenvalue = LastEigenvalue + stepSize * DirectionOfNextStep;
					CalculateSolution();

					if(IsNewSolutionLessAccurate)
					{
						DirectionOfNextStep = TurnBackwards(DirectionOfNextStep);
						CurrentEigenvalue = LastEigenvalue + stepSize * DirectionOfNextStep;
						CalculateSolution();

						if(IsNewSolutionLessAccurate)
						{
							throw new NoSolutionFoundException("Trapped in a local minimum.");
						}
					}
				}
			}
		}

		private bool IsNewSolutionLessAccurate
		{
			get
			{
				return CurrentAccuracy > LastAccuracy;
			}
		}

		private void ThrowIfTooManyAttempts()
		{
			if(Trials > MaxTrials)
			{
				throw new NoSolutionFoundException("Maximum number of trials exceeded.");
			}
		}

		private void ThrowIfSolutionConstraintViolated()
		{
			if(IsSolutionConstraintViolated)
			{
				throw new NoSolutionFoundException("No solution could be found.");
			}
		}

		private bool IsSolutionConstraintViolated
		{
			get
			{
				return SolutionConstraint == null ?
					false : !SolutionConstraint();
			}
		}

		private void PerformActionAfterIteration()
		{
			if(ActionAfterIteration != null)
			{
				ActionAfterIteration();
			}
		}
	}

	[Serializable]
	public class NonPositiveAccuracyException : Exception
	{
		public NonPositiveAccuracyException()
			: base("Accuracy must be positive.")
		{
		}
	}

	[Serializable]
	public class NoSolutionFoundException : Exception
	{
		public NoSolutionFoundException(
			string message
			)
			: base(message)
		{
		}
	}

	[Serializable]
	public class MissingRseSolverException : Exception
	{
		public MissingRseSolverException()
			: base("Solver is not set.")
		{
		}
	}

	[Serializable]
	public class MissingAccuracyMeasureException : Exception
	{
		public MissingAccuracyMeasureException()
			: base("SolutionAccuracyMeasure is null.")
		{
		}
	}

	[Serializable]
	public class ZeroAggressivenessException : Exception
	{
		public ZeroAggressivenessException()
			: base("Aggressiveness must not be zero.")
		{
		}
	}
}
