using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yburn.Tests.Util;

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
			AssertHelper.AssertRoundedEqual(0, flatImpactParams[0]);
			AssertHelper.AssertRoundedEqual(3.2, flatImpactParams[1]);
			AssertHelper.AssertRoundedEqual(4.4, flatImpactParams[2]);
			AssertHelper.AssertRoundedEqual(6.8, flatImpactParams[3]);
			AssertHelper.AssertRoundedEqual(8.4, flatImpactParams[4]);
			AssertHelper.AssertRoundedEqual(9.6, flatImpactParams[5]);
			AssertHelper.AssertRoundedEqual(10.8, flatImpactParams[6]);
			AssertHelper.AssertRoundedEqual(12, flatImpactParams[7]);
			AssertHelper.AssertRoundedEqual(21.2, flatImpactParams[8]);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;
	}
}