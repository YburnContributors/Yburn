using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			Assert.AreEqual(0, flatImpactParams[0]);
			Assert.AreEqual(3.2, flatImpactParams[1]);
			Assert.AreEqual(4.4, flatImpactParams[2]);
			Assert.AreEqual(6.8, flatImpactParams[3]);
			Assert.AreEqual(8.4, flatImpactParams[4]);
			Assert.AreEqual(9.6, flatImpactParams[5]);
			Assert.AreEqual(10.8, flatImpactParams[6]);
			Assert.AreEqual(12, flatImpactParams[7]);
			Assert.AreEqual(21.2, flatImpactParams[8]);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationTokenSource CancellationTokenSource;

		private CancellationToken CancellationToken;
	}
}