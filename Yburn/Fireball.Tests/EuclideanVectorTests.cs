using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class EuclideanVectorTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void GetDimensionOfEuclideanVector()
		{
			double[] components = new double[5];
			EuclideanVector vector = new EuclideanVector(components);

			Assert.AreEqual(5, vector.Dimension);
		}

		[TestMethod]
		public void CalculateNormOfEuclideanVector()
		{
			double[] components = new double[] { 3, 4 };
			EuclideanVector vector = new EuclideanVector(components);

			Assert.AreEqual(5, vector.Norm);
		}

		[TestMethod]
		public void NegateEuclideanVector()
		{
			EuclideanVector vector = new EuclideanVector(new double[] { 1, 2, 3 });

			EuclideanVector negatedVector = vector.Negate();
			double[] negatedComponents = negatedVector.ToDoubleArray();

			Assert.AreEqual(-1, negatedComponents[0]);
			Assert.AreEqual(-2, negatedComponents[1]);
			Assert.AreEqual(-3, negatedComponents[2]);
		}

		[TestMethod]
		public void ConvertEuclideanVectorToDoubleArray()
		{
			double[] sourceArray = { 1, 2, 3, 4, 5 };
			EuclideanVector vector = new EuclideanVector(sourceArray);
			double[] retrievedArray = vector.ToDoubleArray();

			Assert.AreEqual(sourceArray.Length, retrievedArray.Length);
			for(int i = 0; i < sourceArray.Length; i++)
			{
				Assert.AreEqual(sourceArray[i], retrievedArray[i]);
			}
		}

		[TestMethod]
		public void AddEuclideanVectorsOfEqualDimension()
		{
			EuclideanVector v = new EuclideanVector(new double[] { 1, 2, 3 });
			EuclideanVector w = new EuclideanVector(new double[] { 4, 5, 6 });

			EuclideanVector result = v + w;
			double[] resultComponents = result.ToDoubleArray();

			Assert.AreEqual(5, resultComponents[0]);
			Assert.AreEqual(7, resultComponents[1]);
			Assert.AreEqual(9, resultComponents[2]);
		}

		[TestMethod]
		[ExpectedException(typeof(VectorDimensionMismatchException))]
		public void AddEuclideanVectorsOfUnequalDimension()
		{
			EuclideanVector v = new EuclideanVector(new double[] { 1, 2, 3 });
			EuclideanVector w = new EuclideanVector(new double[] { 4, 5 });

			EuclideanVector result = v + w;
		}

		[TestMethod]
		public void MultiplyEuclideanVectorsOfEqualDimension()
		{
			EuclideanVector v = new EuclideanVector(new double[] { 1, 2, 3 });
			EuclideanVector w = new EuclideanVector(new double[] { 4, 5, 6 });

			double result = v * w;

			Assert.AreEqual(32, result);
		}

		[TestMethod]
		[ExpectedException(typeof(VectorDimensionMismatchException))]
		public void MultiplyEuclideanVectorsOfUnequalDimension()
		{
			EuclideanVector v = new EuclideanVector(new double[] { 1, 2, 3 });
			EuclideanVector w = new EuclideanVector(new double[] { 4, 5 });

			double result = v * w;
		}

		[TestMethod]
		public void GetAzimutalAngleOfEuclideanVector()
		{
			EuclideanVector vector = new EuclideanVector(new double[] { 0, 1 });
			double azimutalAngle = vector.AzimutalAngle;

			Assert.AreEqual(0.5 * Math.PI, azimutalAngle);
		}

		[TestMethod]
		[ExpectedException(typeof(InsufficientVectorDimensionException))]
		public void GetAzimutalAngleOf1DEuclideanVector()
		{
			EuclideanVector vector = new EuclideanVector(new double[] { 1 });
			double azimutalAngle = vector.AzimutalAngle;
		}

		[TestMethod]
		public void AssureEuclideanVector2DHasCorrectDimension()
		{
			EuclideanVector2D vector = new EuclideanVector2D();

			Assert.AreEqual(2, vector.Dimension);
		}

		[TestMethod]
		public void AssureEuclideanVector2DHasCorrectComponents()
		{
			EuclideanVector2D vector = new EuclideanVector2D(1, 2);

			Assert.AreEqual(1, vector.X);
			Assert.AreEqual(2, vector.Y);
		}

		[TestMethod]
		public void ManipulateEuclideanVector2DComponents()
		{
			EuclideanVector2D vector = new EuclideanVector2D();
			vector.X = 1;
			vector.Y = 2;

			Assert.AreEqual(1, vector.X);
			Assert.AreEqual(2, vector.Y);
		}

		[TestMethod]
		public void CreateEuclideanVector2DFromPolarCoordinates()
		{
			double radius = 2;
			double azimutalAngle = 0.5 * Math.PI;
			EuclideanVector2D vector = EuclideanVector2D.CreateFromPolarCoordinates(radius, azimutalAngle);

			Assert.AreEqual(0, vector.X, 1e-15);
			Assert.AreEqual(2, vector.Y, 1e-15);
		}

		[TestMethod]
		public void CreateAzimutalUnitVectorAtPosition()
		{
			double[,] positions = new double[,] {
				{ -1.0, -1.0 }, { 1.0, -1.0 }, { 1.0, 1.0 }, { -1.0, 1.0 } };

			for(int i = 0; i < positions.GetLength(0); i++)
			{
				EuclideanVector2D vector = EuclideanVector2D.CreateAzimutalUnitVectorAtPosition(
					positions[i, 0], positions[i, 1]);

				Assert.AreEqual(-positions[i, 1] / Math.Sqrt(2), vector.X, 1e-15);
				Assert.AreEqual(positions[i, 0] / Math.Sqrt(2), vector.Y, 1e-15);
			}
		}

		[TestMethod]
		public void CreateRadialUnitVectorAtPosition()
		{
			double[,] positions = new double[,] {
				{ -1.0, -1.0 }, { 1.0, -1.0 }, { 1.0, 1.0 }, { -1.0, 1.0 } };

			for(int i = 0; i < positions.GetLength(0); i++)
			{
				EuclideanVector2D vector = EuclideanVector2D.CreateRadialUnitVectorAtPosition(
					positions[i, 0], positions[i, 1]);

				Assert.AreEqual(positions[i, 0] / Math.Sqrt(2), vector.X, 1e-15);
				Assert.AreEqual(positions[i, 1] / Math.Sqrt(2), vector.Y, 1e-15);
			}
		}

		[TestMethod]
		public void AssureEuclideanVector3DHasCorrectDimension()
		{
			EuclideanVector3D vector = new EuclideanVector3D();

			Assert.AreEqual(3, vector.Dimension);
		}

		[TestMethod]
		public void AssureEuclideanVector3DHasCorrectComponents()
		{
			EuclideanVector3D vector = new EuclideanVector3D(1, 2, 3);

			Assert.AreEqual(1, vector.X);
			Assert.AreEqual(2, vector.Y);
			Assert.AreEqual(3, vector.Z);
		}

		[TestMethod]
		public void ManipulateEuclideanVector3DComponents()
		{
			EuclideanVector3D vector = new EuclideanVector3D();
			vector.X = 1;
			vector.Y = 2;
			vector.Z = 3;

			Assert.AreEqual(1, vector.X);
			Assert.AreEqual(2, vector.Y);
			Assert.AreEqual(3, vector.Z);
		}
	}
}