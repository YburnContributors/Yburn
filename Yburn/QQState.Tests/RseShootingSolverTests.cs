using Meta.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Yburn.TestUtil;

namespace Yburn.QQState.Tests
{
	[TestClass]
	public class RseShootingSolverTests
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		[ClassCleanup]
		public static void CleanTestFiles()
		{
			FileCleaner.Clean();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		[ExpectedException(typeof(NonPositiveAccuracyException))]
		public void ThrowIfNegativeAccuracy()
		{
			ShootingSolver = new RseShootingSolver();
			ShootingSolver.DesiredAccuracy = -1;

			ShootingSolver.Solve();
		}

		[TestMethod]
		[ExpectedException(typeof(NonPositiveAccuracyException))]
		public void ThrowIfZeroAccuracy()
		{
			ShootingSolver = new RseShootingSolver();
			ShootingSolver.DesiredAccuracy = 0;

			ShootingSolver.Solve();
		}

		[TestMethod]
		[ExpectedException(typeof(ZeroAggressivenessException))]
		public void ThrowIfZeroAggressiveness()
		{
			ShootingSolver = new RseShootingSolver();
			ShootingSolver.DesiredAccuracy = 1;
			ShootingSolver.Aggressiveness = 0;

			ShootingSolver.Solve();
		}

		[TestMethod]
		[ExpectedException(typeof(MissingAccuracyMeasureException))]
		public void ThrowIfMissingAccuracyMeasure()
		{
			ShootingSolver = new RseShootingSolver();
			ShootingSolver.DesiredAccuracy = 1;
			ShootingSolver.Aggressiveness = 1;

			ShootingSolver.Solve();
		}

		[TestMethod]
		[ExpectedException(typeof(MissingRseSolverException))]
		public void ThrowIfMissingSolver()
		{
			ShootingSolver = new RseShootingSolver();
			ShootingSolver.DesiredAccuracy = 1;
			ShootingSolver.Aggressiveness = 1;
			ShootingSolver.SolutionAccuracyMeasure = () =>
			{
				return 0;
			};
			ShootingSolver.Solver = null;

			ShootingSolver.Solve();
		}

		[TestMethod]
		[ExpectedException(typeof(NoSolutionFoundException))]
		public void ThrowIfTooManyAttempts()
		{
			InitRseSolver_Dummy();

			ShootingSolver = new RseShootingSolver();
			ShootingSolver.DesiredAccuracy = 1;
			ShootingSolver.Aggressiveness = 1;
			ShootingSolver.SolutionAccuracyMeasure = () =>
			{
				return 10 * ShootingSolver.DesiredAccuracy;
			};
			ShootingSolver.Solver = Solver;
			ShootingSolver.MaxTrials = -1;

			ShootingSolver.Solve();
		}

		// Solves the following DGL:
		//
		//	z''(x) = (1 - E) z(x),
		//
		// where the wanted solution, z(x) = sin(x), E = 0, is used as starting condition and hence no
		// break up condition has to be specified.
		[TestMethod]
		public void SinDGL_ExactInput()
		{
			InitShootingSolver_SinExactInput();
			ShootingSolver.Solve();

			AssertDeviationBelow(DesiredAccuracy, ShootingSolver.Eigenvalue, new Complex(0, 0));
			AssertMaxDeviationBelow(DesiredAccuracy,
				Solver.PositionValues, Solver.SolutionValues, ComplexSin);
		}

		// Solves the following DGL:
		//
		//	z''(x) = - E z(x),
		//
		// where z(x) = sin(x), i.e. E = 1 is the wanted solution.
		[TestMethod]
		public void SinDGLAsEigenvalueProblem()
		{
			InitShootingSolver_SinEigenvalueProblem();
			ShootingSolver.Solve();

			AssertDeviationBelow(DesiredAccuracy, ShootingSolver.Eigenvalue, new Complex(1, 0));
			AssertMaxDeviationBelow(DesiredAccuracy,
				Solver.PositionValues, Solver.SolutionValues, ComplexSin);
		}

		// Solves the following DGL:
		//
		//	z''(x) = - E z(x),
		//
		// where z(x) = exp(-(1+I)*x), i.e. E = -2*I is the wanted solution.
		[TestMethod]
		public void ComplexEigenvalueProblem()
		{
			InitShootingSolver_ComplexEigenvalueProblem();
			ShootingSolver.Solve();

			AssertDeviationBelow(DesiredAccuracy, ShootingSolver.Eigenvalue, new Complex(0, -2));
			AssertMaxDeviationBelow(DesiredAccuracy,
				Solver.PositionValues, Solver.SolutionValues, ComplexTestFunction);
		}

		// Tries to solve the DGL from ComplexEigenvalueProblem(). While E = -2*I is the wanted
		// solution, we break up the shooting process as soon as |E| >= 1.999
		[TestMethod]
		[ExpectedException(typeof(NoSolutionFoundException))]
		public void ThrowIfSolutionConstraintViolated()
		{
			InitShootingSolver_SolutionConstraintViolated();
			ShootingSolver.Solve();
		}

		// Solves the DGL from ComplexEigenvalueProblem().
		[TestMethod]
		public void UpdateStatusWhileShooting()
		{
			List<string> statusValueList = new List<string>();

			InitShootingSolver_UpdateStatusWhileShooting(statusValueList);
			ShootingSolver.Solve();

			Assert.AreEqual(ShootingSolver.Trials, statusValueList.Count);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static FileCleaner FileCleaner = new FileCleaner();

		private static void AssertDeviationBelow(
			double DesiredAccuracy,
			Complex complex1,
			Complex complex2
			)
		{
			Assert.IsTrue(ComplexMath.Abs(complex1 - complex2) < DesiredAccuracy,
				string.Format("Calculated eigenvalue: {0}\r\n"
				+ "Expected eigenvalue: {1}\r\n"
				+ "Max. tolerated deviation: {2}",
				complex1, complex2, DesiredAccuracy));
		}

		private static void AssertMaxDeviationBelow(
			double accuracy,
			double[] xValues,
			Complex[] yValues,
			ComplexFunction analyticSolution
			)
		{
			Complex[] analyticValues = new Complex[yValues.Length];
			for(int i = 0; i < yValues.Length; i++)
			{
				analyticValues[i] = analyticSolution(xValues[i]);
			}

			AssertMaxDeviationBelow(accuracy, xValues, yValues, analyticValues);
		}

		private static void AssertMaxDeviationBelow(
			double accuracy,
			double[] xValues,
			Complex[] yValues,
			Complex[] analyticValues
			)
		{
			double deviation;
			for(int i = 0; i < yValues.Length; i++)
			{
				deviation = ComplexMath.Abs(yValues[i] - analyticValues[i]);

				if(double.IsNaN(deviation)
					|| double.IsInfinity(deviation))
				{
					Assert.Fail();
				}

				Assert.IsTrue(deviation < accuracy,
					string.Format("Max. tolerated deviation: {0}\r\nActual deviation: {1}.\r\n"
					+ "This occurred at position x = {2}",
					accuracy, deviation, xValues[i]));
			}
		}

		private static Complex SinDglRhs(
			double x
			)
		{
			return new Complex(-1, 0);
		}

		private static Complex ZeroDglRhs(
			double x
			)
		{
			return new Complex(0, 0);
		}

		private static Complex ComplexSin(
			double x
			)
		{
			return new Complex(Math.Sin(x), 0);
		}

		private static Complex ComplexTestFunction(
			double x
			)
		{
			return ComplexMath.Exp(new Complex(-x, -x));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private RseShootingSolver ShootingSolver;

		private RseSolver Solver;

		private double DesiredAccuracy;

		private int MaxShootingTrials;

		private double Aggressiveness;

		private void InitShootingSolver_Dummy()
		{
			DesiredAccuracy = 1;
			Aggressiveness = 1;

			InitRseSolver_Dummy();

			ShootingSolver = new RseShootingSolver();
			ShootingSolver.SolutionAccuracyMeasure = () =>
			{
				return 10 * DesiredAccuracy;
			};
		}

		private void InitRseSolver_Dummy()
		{
			Solver = new RseSolver();
			Solver.InitialPosition = 0;
			Solver.FinalPosition = 1;
			Solver.Samples = 1;
			Solver.RightHandSide = ZeroDglRhs;
		}

		private void InitShootingSolver_SinExactInput()
		{
			DesiredAccuracy = 1e-7;
			Aggressiveness = 1;
			MaxShootingTrials = 1;

			InitRseSolver_SinExactInput();

			ShootingSolver = new RseShootingSolver();
			ShootingSolver.Solver = Solver;
			ShootingSolver.DesiredAccuracy = DesiredAccuracy;
			ShootingSolver.Aggressiveness = Aggressiveness;
			ShootingSolver.SolutionAccuracyMeasure = () =>
			{
				return ComplexMath.Abs(Solver.SolutionValues[5000]);
			};
		}

		private void InitRseSolver_SinExactInput()
		{
			Solver = new RseSolver();
			Solver.InitialPosition = 0;
			Solver.FinalPosition = 2 * Math.PI;
			Solver.Samples = 10000;
			Solver.RightHandSide = SinDglRhs;
			Solver.InitialSolutionValue = new Complex(0, 0);
			Solver.InitialDerivativeValue = new Complex(1, 0);
		}

		private void InitShootingSolver_SinEigenvalueProblem()
		{
			DesiredAccuracy = 1e-7;
			Aggressiveness = 0.2;
			MaxShootingTrials = 100;

			InitRseSolver_SinEigenvalueProblem();

			ShootingSolver = new RseShootingSolver();
			ShootingSolver.Solver = Solver;
			ShootingSolver.DesiredAccuracy = DesiredAccuracy;
			ShootingSolver.Aggressiveness = Aggressiveness;
			ShootingSolver.MaxTrials = MaxShootingTrials;
			ShootingSolver.Eigenvalue = new Complex(0, 0);
			ShootingSolver.SolutionAccuracyMeasure = () =>
			{
				return ComplexMath.Abs(Solver.SolutionValues[10000]);
			};
		}

		private void InitRseSolver_SinEigenvalueProblem()
		{
			Solver = new RseSolver();
			Solver.InitialPosition = 0;
			Solver.FinalPosition = 2 * Math.PI;
			Solver.Samples = 10000;
			Solver.RightHandSide = ZeroDglRhs;
			Solver.InitialSolutionValue = new Complex(0, 0);
			Solver.InitialDerivativeValue = new Complex(1, 0);
		}

		private void InitShootingSolver_ComplexEigenvalueProblem()
		{
			DesiredAccuracy = 1e-7;
			Aggressiveness = 0.2;
			MaxShootingTrials = 100;

			InitRseSolver_ComplexEigenvalueProblem();

			ShootingSolver = new RseShootingSolver();
			ShootingSolver.Solver = Solver;
			ShootingSolver.DesiredAccuracy = DesiredAccuracy;
			ShootingSolver.Aggressiveness = Aggressiveness;
			ShootingSolver.MaxTrials = MaxShootingTrials;
			ShootingSolver.Eigenvalue = new Complex(0.3, -1.5);
			ShootingSolver.SolutionAccuracyMeasure = () =>
			{
				return ComplexMath.Abs(Solver.SolutionValues[10000] - ComplexTestFunction(Math.PI));
			};
		}

		private void InitRseSolver_ComplexEigenvalueProblem()
		{
			Solver = new RseSolver();
			Solver.InitialPosition = 0;
			Solver.FinalPosition = Math.PI;
			Solver.Samples = 10000;
			Solver.RightHandSide = ZeroDglRhs;
			Solver.InitialSolutionValue = new Complex(1, 0);
			Solver.InitialDerivativeValue = new Complex(-1, -1);
		}

		private void InitShootingSolver_SolutionConstraintViolated()
		{
			InitShootingSolver_ComplexEigenvalueProblem();
			ShootingSolver.SolutionConstraint = () =>
			{
				return ComplexMath.Abs(ShootingSolver.Eigenvalue) < 1.999;
			};
		}

		private void InitShootingSolver_UpdateStatusWhileShooting(
			List<string> statusValueList
			)
		{
			InitShootingSolver_ComplexEigenvalueProblem();
			ShootingSolver.ActionAfterIteration = () =>
			{
				statusValueList.Add(string.Format("{0}, {1}, {2}, {3}",
					ShootingSolver.Trials,
					ShootingSolver.Eigenvalue.Re,
					ShootingSolver.Eigenvalue.Im,
					Solver.SolutionValues[10000]));
			};
		}
	}
}
