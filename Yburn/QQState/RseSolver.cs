/**************************************************************************************************
 * Solves for the eigenfunction of the stationary radial Schrödinger equation, i.e. a second order
 * ordinary differential equation of the type
 *
 *		z''(x) = f(x) z(x),
 *
 * where x is real while f and z may be complex.
 **************************************************************************************************/

using Meta.Numerics;
using System;

namespace Yburn.QQState
{
	public delegate Complex ComplexFunction(double x);

	public class RseSolver
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public RseSolver()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double InitialPosition;

		public Complex InitialDerivativeValue;

		public Complex InitialSolutionValue;

		public double FinalPosition;

		public int Samples;

		public double StepSize
		{
			get;
			private set;
		}

		// The function f in the description at the top.
		public ComplexFunction RightHandSide;

		public double[] PositionValues
		{
			get
			{
				double[] positionValues = new double[Samples + 1];

				positionValues[0] = Math.Min(InitialPosition, FinalPosition);
				for(int i = 1; i <= Samples; i++)
				{
					positionValues[i] = positionValues[0] + i * StepSize;
				}

				return positionValues;
			}
		}

		public Complex[] SolutionValues
		{
			get;
			private set;
		}

		public void Solve()
		{
			if(!IsInitialized)
			{
				Initialize();
			}

			if(InitialPosition < FinalPosition)
			{
				SolveLeftToRight();
			}
			else
			{
				SolveRightToLeft();
			}
		}

		public void SetFinalValuesAsNewInitialValues()
		{
			if(InitialPosition < FinalPosition)
			{
				NewInitialValuesFromRightEnd();
			}
			else
			{
				NewInitialValuesFromLeftEnd();
			}
		}

		private void NewInitialValuesFromLeftEnd()
		{
			InitialDerivativeValue = -Derivative;
			InitialSolutionValue = SolutionValues[0];
		}

		private void NewInitialValuesFromRightEnd()
		{
			InitialDerivativeValue = Derivative;
			InitialSolutionValue = SolutionValues[Samples];
		}

		public void Initialize()
		{
			AssertInputValid();

			SetStepSizes();
			SetSolutionValues();

			IsInitialized = true;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private bool IsInitialized = false;

		private double HalfStepSize;

		private double StepSizeSquareHalf;

		private double HalfStepSizeSquareHalf;

		private Complex CurrentRhsValue;

		private Complex IntermediateRhsValue;

		private Complex NextRhsValue;

		private Complex CrudeSolutionValue;

		private Complex IntermediateSolutionValue;

		private Complex FineSolutionValue;

		private Complex Derivative;

		private void AssertInputValid()
		{
			if(InitialPosition == FinalPosition)
			{
				throw new Exception("InitialPosition = FinalPosition");
			}
			if(Samples < 1)
			{
				throw new Exception("Samples < 1");
			}
		}

		private void SetStepSizes()
		{
			StepSize = Math.Abs(FinalPosition - InitialPosition) / Samples;
			StepSizeSquareHalf = 0.5 * StepSize * StepSize;
			HalfStepSize = 0.5 * StepSize;
			HalfStepSizeSquareHalf = 0.5 * HalfStepSize * HalfStepSize;
		}

		private void SetSolutionValues()
		{
			SolutionValues = new Complex[Samples + 1];
		}

		private void SolveLeftToRight()
		{
			Derivative = InitialDerivativeValue;
			SolutionValues[0] = InitialSolutionValue;

			NextRhsValue = RightHandSide(InitialPosition);

			double nextPosition;
			for(int i = 1; i <= Samples; i++)
			{
				nextPosition = InitialPosition + i * StepSize;

				CurrentRhsValue = NextRhsValue;
				IntermediateRhsValue = RightHandSide(nextPosition - HalfStepSize);
				NextRhsValue = RightHandSide(nextPosition);

				SolutionValues[i] = CalculateNextStep(SolutionValues[i - 1]);
			}
		}

		private void SolveRightToLeft()
		{
			Derivative = -InitialDerivativeValue;
			SolutionValues[Samples] = InitialSolutionValue;

			NextRhsValue = RightHandSide(InitialPosition);

			double nextPosition;
			for(int i = Samples - 1; i >= 0; i--)
			{
				nextPosition = FinalPosition + i * StepSize;

				CurrentRhsValue = NextRhsValue;
				IntermediateRhsValue = RightHandSide(nextPosition + HalfStepSize);
				NextRhsValue = RightHandSide(nextPosition);

				SolutionValues[i] = CalculateNextStep(SolutionValues[i + 1]);
			}
		}

		private Complex CalculateNextStep(
			Complex currentSolutionValue
			)
		{
			CrudeSolutionValue = currentSolutionValue
				* (1.0 + StepSizeSquareHalf * CurrentRhsValue) + StepSize * Derivative;
			IntermediateSolutionValue = currentSolutionValue *
				(1.0 + HalfStepSizeSquareHalf * CurrentRhsValue) + HalfStepSize * Derivative;
			FineSolutionValue = 2 * IntermediateSolutionValue
				* (1.0 + HalfStepSizeSquareHalf * IntermediateRhsValue) - currentSolutionValue;

			Derivative = (FineSolutionValue * (1.0 + HalfStepSizeSquareHalf * NextRhsValue)
				- IntermediateSolutionValue) / HalfStepSize;

			return (4 * FineSolutionValue - CrudeSolutionValue) / 3.0;
		}
	}
}