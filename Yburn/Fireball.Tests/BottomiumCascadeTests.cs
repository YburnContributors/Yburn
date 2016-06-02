using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.Tests.Util;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class BottomiumCascadeTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void MatricesHaveCorrectDimensions()
		{
			int numberBottomiumStates = Enum.GetValues(typeof(BottomiumState)).Length;
			AssertIsSquareOfDimenions(
				numberBottomiumStates, BottomiumCascade.CalculateBranchingRatioMatrix());
			AssertIsSquareOfDimenions(
				numberBottomiumStates, BottomiumCascade.CalculateCumulativeMatrix());
			AssertIsSquareOfDimenions(
				numberBottomiumStates, BottomiumCascade.CalculateInverseCumulativeMatrix());
		}

		[TestMethod]
		public void CalculateCorrectInverseCumulativeMatrix()
		{
			AssertIsUnitMatrix(
				Multiply(BottomiumCascade.CalculateCumulativeMatrix(),
				BottomiumCascade.CalculateInverseCumulativeMatrix()));
			AssertIsUnitMatrix(
				Multiply(BottomiumCascade.CalculateInverseCumulativeMatrix(),
				BottomiumCascade.CalculateCumulativeMatrix()));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertIsSquareOfDimenions(
			int dimension,
			double[,] matrix
			)
		{
			Assert.AreEqual(dimension, matrix.GetLength(0));
			Assert.AreEqual(dimension, matrix.GetLength(1));
		}

		private static double[,] Multiply(
			double[,] m1,
			double[,] m2
			)
		{
			int size = m1.GetLength(0);
			double[,] M = new double[size, size];
			for(int i = 0; i < size; i++)
			{
				for(int j = 0; j < size; j++)
				{
					M[i, j] = 0;
					for(int k = 0; k < size; k++)
					{
						M[i, j] += m1[i, k] * m2[k, j];
					}
				}
			}

			return M;
		}

		private static void AssertIsUnitMatrix(
			double[,] multipliedMatrix
			)
		{
			for(int i = 0; i < multipliedMatrix.GetLength(0); i++)
			{
				for(int j = 0; j < multipliedMatrix.GetLength(1); j++)
				{
					AssertHelper.AssertRoundedEqual(UnitMatrix(i, j), multipliedMatrix[i, j]);
				}
			}
		}

		private static int UnitMatrix(
			int i,
			int j
			)
		{
			return i == j ? 1 : 0;
		}
	}
}