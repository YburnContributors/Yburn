using Meta.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using Yburn.TestUtil;

namespace Yburn.QQState.Tests
{
	[TestClass]
	public class RseSolverTests
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
		public void SolveSinLeftRight()
		{
			RseSolver solver = new RseSolver();
			solver.InitialPosition = 0;
			solver.FinalPosition = -10;
			solver.Samples = 10000;
			solver.RightHandSide = SinDglRhs;
			solver.InitialSolutionValue = new Complex(0, 0);
			solver.InitialDerivativeValue = new Complex(1, 0);

			solver.Solve();

			double maxDeviation = GetMaxDeviation(solver.PositionValues, solver.SolutionValues,
				ComplexSin);

			Assert.IsTrue(maxDeviation < 1e-7);
		}

		[TestMethod]
		public void SolveSinRightLeft()
		{
			RseSolver solver = new RseSolver();
			solver.InitialPosition = 0;
			solver.FinalPosition = 10;
			solver.Samples = 10000;
			solver.RightHandSide = SinDglRhs;
			solver.InitialSolutionValue = new Complex(0, 0);
			solver.InitialDerivativeValue = new Complex(1, 0);

			solver.Solve();

			double maxDeviation = GetMaxDeviation(solver.PositionValues, solver.SolutionValues,
				ComplexSin);

			Assert.IsTrue(maxDeviation < 1e-7);
		}

		[TestMethod]
		public void SolveHydrogen10()
		{
			RseSolver solver = new RseSolver();
			solver.InitialPosition = 40;
			solver.FinalPosition = 0;
			solver.Samples = 15000;
			solver.RightHandSide = CoulombRightHandSideN1L0;
			solver.InitialSolutionValue = new Complex(1e-40, 0);
			solver.InitialDerivativeValue = new Complex(0, 0);

			solver.Solve();

			Normalize(solver.SolutionValues, solver.StepSize, 1);

			MakePlotFile(solver.PositionValues, solver.SolutionValues, HydrogenWaveFunctionN1L0);

			double maxDeviation = GetMaxDeviation(solver.PositionValues, solver.SolutionValues,
				HydrogenWaveFunctionN1L0);

			Assert.IsTrue(maxDeviation < 1e-7);
		}

		[TestMethod]
		public void SolveHydrogen21()
		{
			RseSolver solver = new RseSolver();
			solver.InitialPosition = 40;
			solver.FinalPosition = 0;
			solver.Samples = 70000;
			solver.RightHandSide = CoulombRightHandSideN2L1;
			solver.InitialSolutionValue = new Complex(1e-40, 0);
			solver.InitialDerivativeValue = new Complex(0, 0);

			solver.Solve();

			Normalize(solver.SolutionValues, solver.StepSize, 2);

			MakePlotFile(solver.PositionValues, solver.SolutionValues, HydrogenWaveFunctionN2L1);

			double maxDeviation = GetMaxDeviation(solver.PositionValues, solver.SolutionValues,
				HydrogenWaveFunctionN2L1);

			Assert.IsTrue(maxDeviation < 1e-5);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static FileCleaner FileCleaner = new FileCleaner();

		private static Complex SinDglRhs(
			double x
			)
		{
			return new Complex(-1, 0);
		}

		private static Complex ComplexSin(
			double x
			)
		{
			return new Complex(Math.Sin(x), 0);
		}

		private static double GetMaxDeviation(
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

			return GetMaxDeviation(xValues, yValues, analyticValues);
		}

		private static double GetMaxDeviation(
			double[] xValues,
			Complex[] yValues,
			Complex[] analyticValues
			)
		{
			double maxDeviation = 0;
			double currentDeviation;
			for(int i = 0; i < yValues.Length; i++)
			{
				currentDeviation = ComplexMath.Abs(yValues[i] - analyticValues[i]);

				if(double.IsNaN(currentDeviation)
					|| double.IsInfinity(currentDeviation))
				{
					return currentDeviation;
				}

				if(currentDeviation > maxDeviation)
				{
					maxDeviation = currentDeviation;
				}
			}

			return maxDeviation;
		}

		private static void MakePlotFile(
			double[] positions,
			Complex[] values,
			ComplexFunction analyticValues
			)
		{
			StringBuilder builder = new StringBuilder();
			for(int i = 0; i < positions.Length; i++)
			{
				builder.AppendFormat("{0,-22}{1,-22}{2,-22}\r\n",
					positions[i].ToString(),
					values[i].Re.ToString(),
					analyticValues(positions[i]).Re.ToString());
			}

			string pathFile = "RseSolverHydroTest.txt";
			File.WriteAllText(pathFile, builder.ToString());
			FileCleaner.MarkForDelete(pathFile);
		}

		private static Complex CoulombRightHandSideN1L0(
			double x
			)
		{
			return CoulombRightHandSide(x, 1, 0);
		}

		private static Complex CoulombRightHandSideN2L1(
			double x
			)
		{
			return CoulombRightHandSide(x, 2, 1);
		}

		private static Complex CoulombRightHandSide(
			double x,
			double n,
			double l
			)
		{
			return new Complex((l * (l + 1) / x - n) / x + 0.25, 0);
		}

		private static Complex HydrogenWaveFunctionN1L0(
			double x
			)
		{
			return new Complex(x * Math.Exp(-0.5 * x), 0);
		}

		private static Complex HydrogenWaveFunctionN2L1(
			double x
			)
		{
			return new Complex(x * x * Math.Exp(-0.5 * x) / Math.Sqrt(24), 0);
		}

		private void Normalize(
			Complex[] solution,
			double stepSize,
			double n
			)
		{
			double integral = 0;
			int maxIndex = solution.Length - 1;
			for(int i = 1; i < maxIndex; i++)
			{
				integral += ComplexMath.Abs(solution[i]) * ComplexMath.Abs(solution[i]);
			}
			integral += 0.5 * (ComplexMath.Abs(solution[0]) * ComplexMath.Abs(solution[0])
				+ ComplexMath.Abs(solution[maxIndex]) * ComplexMath.Abs(solution[maxIndex]));
			integral *= stepSize * n / 2.0; // xn = 2*C1*r/n

			double sqrtIntegral = Math.Sqrt(integral);
			for(int j = 0; j <= maxIndex; j++)
			{
				solution[j] /= sqrtIntegral;
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/
	}
}