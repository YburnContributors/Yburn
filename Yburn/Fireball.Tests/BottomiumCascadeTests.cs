using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yburn.TestUtil;

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
				BottomiumCascade.CalculateCumulativeMatrix()
				* BottomiumCascade.CalculateInverseCumulativeMatrix());
			AssertIsUnitMatrix(
				BottomiumCascade.CalculateInverseCumulativeMatrix()
				* BottomiumCascade.CalculateCumulativeMatrix());
		}

		[TestMethod]
		public void CalculateInitialQQPopulations()
		{
			ProtonProtonBaseline ppBaseline = ProtonProtonBaseline.CMS2012;
			double feedDown3P = 0.06;

			BottomiumVector initialQQPopulations = BottomiumCascade.CalculateInitialQQPopulations(
				ppBaseline, feedDown3P);
			AssertCorrectInitialQQPopulations(initialQQPopulations);
		}

		[TestMethod]
		public void CalculateY1SFeedDownFractions()
		{
			ProtonProtonBaseline ppBaseline = ProtonProtonBaseline.CMS2012;
			double feedDown3P = 0.06;

			BottomiumVector feedDownFractions = BottomiumCascade.CalculateY1SFeedDownFractions(
				ppBaseline, feedDown3P);
			AssertCorrectY1SFeedDownFractions(feedDownFractions, feedDown3P);
		}

		[TestMethod]
		public void FeedDownCascadeReproducesProtonProtonDimuonDecays()
		{
			ProtonProtonBaseline ppBaseline = ProtonProtonBaseline.CMS2012;
			double feedDown3P = 0.06;

			BottomiumVector qgpSuppressionFactors = BottomiumVector.CreateEmptyVector();
			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				qgpSuppressionFactors[state] = 1;
			}

			BottomiumVector ppDimuonDecays =
				BottomiumCascade.CalculateDimuonDecays(ppBaseline, feedDown3P, qgpSuppressionFactors);

			AssertCorrectProtonProtonDimuonDecays(ppDimuonDecays, ppBaseline, feedDown3P);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertIsSquareOfDimenions(
			int dimension,
			BottomiumCascadeMatrix matrix
			)
		{
			Assert.AreEqual(dimension, matrix.Dimensions[0]);
			Assert.AreEqual(dimension, matrix.Dimensions[1]);
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
			BottomiumCascadeMatrix multipliedMatrix
			)
		{
			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				foreach(BottomiumState j in Enum.GetValues(typeof(BottomiumState)))
				{
					AssertHelper.AssertApproximatelyEqual(
						UnitMatrixEntries(i, j), multipliedMatrix[i, j]);
				}
			}
		}

		private static int UnitMatrixEntries(
			BottomiumState i,
			BottomiumState j
			)
		{
			if(i == j)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		private static void AssertCorrectInitialQQPopulations(
			BottomiumVector initialQQPopulations
			)
		{
			AssertHelper.AssertApproximatelyEqual(13.831681883072372, initialQQPopulations[BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(43.694709398023143, initialQQPopulations[BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(17.730737923019692, initialQQPopulations[BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(45.626563577477185, initialQQPopulations[BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(10.893164282464252, initialQQPopulations[BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(7.6588791939455159E+99, initialQQPopulations[BottomiumState.x3P]);
		}

		private static void AssertCorrectY1SFeedDownFractions(
			BottomiumVector feedDownFractions,
			double feedDown3P
			)
		{
			AssertHelper.AssertApproximatelyEqual(0.34302571070019483, feedDownFractions[BottomiumState.Y1S]);
			AssertHelper.AssertApproximatelyEqual(0.271, feedDownFractions[BottomiumState.x1P]);
			AssertHelper.AssertApproximatelyEqual(0.19033036269430051, feedDownFractions[BottomiumState.Y2S]);
			AssertHelper.AssertApproximatelyEqual(0.105, feedDownFractions[BottomiumState.x2P]);
			AssertHelper.AssertApproximatelyEqual(0.030643926605504589, feedDownFractions[BottomiumState.Y3S]);
			AssertHelper.AssertApproximatelyEqual(feedDown3P, feedDownFractions[BottomiumState.x3P]);
		}

		private static void AssertCorrectProtonProtonDimuonDecays(
			BottomiumVector ppDimuonDecays,
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			BottomiumVector expected =
				BottomiumCascade.GetProtonProtonDimuonDecays(ppBaseline, feedDown3P);

			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				AssertHelper.AssertApproximatelyEqual(expected[state], ppDimuonDecays[state]);
			}
		}
	}
}