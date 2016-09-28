using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class QGPSuppressionTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			CancellationTokenSource = new CancellationTokenSource();
			CancellationToken = CancellationTokenSource.Token;
		}

		[TestMethod]
		public void ConvertRaggedToFlatImpactParamsArray()
		{
			QGPSuppression suppression = new QGPSuppression(
				new FireballParam(), NumberCentralityBins, ImpactParamsAtBinBoundaries, CancellationToken);

			double[] flatImpactParams = suppression.FlatImpactParamsAtBinBoundaries;

			AssertCorrectFlatImpactParamsArray(flatImpactParams);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly int[] NumberCentralityBins = new int[] { 5, 6 };

		private static readonly double[][] ImpactParamsAtBinBoundaries = new double[][] {
			new double[] { 3.2, 4.4, 8.4, 9.6, 10.8, 21.2 },
			new double[] { 0, 4.4, 6.8, 8.4, 9.6, 10.8, 12 }};

		private static void AssertCorrectFlatImpactParamsArray(
			double[] flatImpactParams
			)
		{
			AssertHelper.AssertApproximatelyEqual(0, flatImpactParams[0]);
			AssertHelper.AssertApproximatelyEqual(3.2, flatImpactParams[1]);
			AssertHelper.AssertApproximatelyEqual(4.4, flatImpactParams[2]);
			AssertHelper.AssertApproximatelyEqual(6.8, flatImpactParams[3]);
			AssertHelper.AssertApproximatelyEqual(8.4, flatImpactParams[4]);
			AssertHelper.AssertApproximatelyEqual(9.6, flatImpactParams[5]);
			AssertHelper.AssertApproximatelyEqual(10.8, flatImpactParams[6]);
			AssertHelper.AssertApproximatelyEqual(12, flatImpactParams[7]);
			AssertHelper.AssertApproximatelyEqual(21.2, flatImpactParams[8]);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;
	}
}