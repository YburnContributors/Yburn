using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballCoordinateSystemTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void InitializeSymmetryFactor()
		{
			int symmetric = new FireballCoordinateSystem(0, 1, true).SymmetryFactor;
			Assert.AreEqual(4, symmetric);

			int nonsymmetric = new FireballCoordinateSystem(0, 1, false).SymmetryFactor;
			Assert.AreEqual(2, nonsymmetric);
		}

		[TestMethod]
		public void GenerateXAxisForSymmetricCollision()
		{
			var xAxis = new FireballCoordinateSystem(4, 1, true).XAxis;

			Assert.AreEqual(5, xAxis.Count);
			Assert.AreEqual(0, xAxis[0]);
			Assert.AreEqual(1, xAxis[1]);
			Assert.AreEqual(2, xAxis[2]);
			Assert.AreEqual(3, xAxis[3]);
			Assert.AreEqual(4, xAxis[4]);
		}

		[TestMethod]
		public void GenerateXAxisForNonsymmetricCollision()
		{
			var xAxis = new FireballCoordinateSystem(4, 1, false).XAxis;

			Assert.AreEqual(9, xAxis.Count);
			Assert.AreEqual(-4, xAxis[0]);
			Assert.AreEqual(-3, xAxis[1]);
			Assert.AreEqual(-2, xAxis[2]);
			Assert.AreEqual(-1, xAxis[3]);
			Assert.AreEqual(0, xAxis[4]);
			Assert.AreEqual(1, xAxis[5]);
			Assert.AreEqual(2, xAxis[6]);
			Assert.AreEqual(3, xAxis[7]);
			Assert.AreEqual(4, xAxis[8]);
		}

		[TestMethod]
		public void GenerateYAxisForSymmetricCollision()
		{
			var yAxis = new FireballCoordinateSystem(9, 2, true).YAxis;

			Assert.AreEqual(5, yAxis.Count);
			Assert.AreEqual(0, yAxis[0]);
			Assert.AreEqual(2, yAxis[1]);
			Assert.AreEqual(4, yAxis[2]);
			Assert.AreEqual(6, yAxis[3]);
			Assert.AreEqual(8, yAxis[4]);
		}

		[TestMethod]
		public void GenerateYAxisForNonsymmetricCollision()
		{
			var yAxis = new FireballCoordinateSystem(9, 2, false).YAxis;

			Assert.AreEqual(5, yAxis.Count);
			Assert.AreEqual(0, yAxis[0]);
			Assert.AreEqual(2, yAxis[1]);
			Assert.AreEqual(4, yAxis[2]);
			Assert.AreEqual(6, yAxis[3]);
			Assert.AreEqual(8, yAxis[4]);
		}

		[TestMethod]
		public void FindClosestXAxisIndex()
		{
			FireballCoordinateSystem system = new FireballCoordinateSystem(10, 1, false);

			Assert.AreEqual(8, system.FindClosestXAxisIndex(-2.1));
			Assert.AreEqual(14, system.FindClosestXAxisIndex(3.8));
			Assert.AreEqual(3, system.FindClosestXAxisIndex(-7.5));
			Assert.AreEqual(20, system.FindClosestXAxisIndex(100));
		}

		[TestMethod]
		public void FindClosestYAxisIndex()
		{
			FireballCoordinateSystem system = new FireballCoordinateSystem(10, 1, false);

			Assert.AreEqual(2, system.FindClosestYAxisIndex(2.1));
			Assert.AreEqual(4, system.FindClosestYAxisIndex(3.8));
			Assert.AreEqual(8, system.FindClosestYAxisIndex(7.5));
			Assert.AreEqual(10, system.FindClosestYAxisIndex(100));
		}
	}
}
