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
		public void ConstructEuclideanVector2DFromDoubles()
		{
			EuclideanVector2D vec2D = new EuclideanVector2D(1, 2);
			Assert.AreEqual(1, vec2D.X);
			Assert.AreEqual(2, vec2D.Y);
		}

		[TestMethod]
		public void ConstructEuclideanVector3DFromDoubles()
		{
			EuclideanVector3D vec3D = new EuclideanVector3D(3, 4, 5);
			Assert.AreEqual(3, vec3D.X);
			Assert.AreEqual(4, vec3D.Y);
			Assert.AreEqual(5, vec3D.Z);
		}

		[TestMethod]
		public void ConstructEuclideanVector3DFromVectorAndDouble()
		{
			EuclideanVector2D vec2D = new EuclideanVector2D(1, 2);
			EuclideanVector3D vec3D = new EuclideanVector3D(vec2D, 3);

			Assert.AreEqual(1, vec3D.X);
			Assert.AreEqual(2, vec3D.Y);
			Assert.AreEqual(3, vec3D.Z);
		}

		[TestMethod]
		public void ManipulateEuclideanVector2DComponents()
		{
			EuclideanVector2D vec2D = new EuclideanVector2D();
			vec2D.X = 1;
			vec2D.Y = 2;

			Assert.AreEqual(1, vec2D.X);
			Assert.AreEqual(2, vec2D.Y);
		}

		[TestMethod]
		public void ManipulateEuclideanVector3DComponents()
		{
			EuclideanVector3D vec3D = new EuclideanVector3D();
			vec3D.X = 3;
			vec3D.Y = 4;
			vec3D.Z = 5;

			Assert.AreEqual(3, vec3D.X);
			Assert.AreEqual(4, vec3D.Y);
			Assert.AreEqual(5, vec3D.Z);
		}

		[TestMethod]
		public void GetEuclideanVectorDimension()
		{
			EuclideanVector2D vec2D = new EuclideanVector2D();
			Assert.AreEqual(2, vec2D.Dimension);

			EuclideanVector3D vec3D = new EuclideanVector3D();
			Assert.AreEqual(3, vec3D.Dimension);
		}

		[TestMethod]
		public void GetEuclideanVectorNorm()
		{
			EuclideanVector2D vector = new EuclideanVector2D(3, 4);
			Assert.AreEqual(5, vector.Norm);
		}

		[TestMethod]
		public void GetEuclideanVector2DAzimutalAngle()
		{
			EuclideanVector2D vector = new EuclideanVector2D(0, 1);
			double angle = vector.AzimutalAngle;

			Assert.AreEqual(0.5 * Math.PI, angle);
		}

		[TestMethod]
		public void ConvertEuclideanVectorToDoubleArray()
		{
			EuclideanVector2D vector = new EuclideanVector2D(1, 2);
			double[] array = vector.ToDoubleArray();

			Assert.AreEqual(2, array.Length);
			Assert.AreEqual(1, array[0]);
			Assert.AreEqual(2, array[1]);
		}

		[TestMethod]
		public void AddEuclideanVectors()
		{
			EuclideanVector2D a = new EuclideanVector2D(1, 2);
			EuclideanVector2D b = new EuclideanVector2D(3, 4);

			EuclideanVector2D result = a + b;

			Assert.AreEqual(4, result.X);
			Assert.AreEqual(6, result.Y);
		}

		[TestMethod]
		public void SubstractEuclideanVectors()
		{
			EuclideanVector2D a = new EuclideanVector2D(1, 2);
			EuclideanVector2D b = new EuclideanVector2D(4, 3);

			EuclideanVector2D result = a - b;

			Assert.AreEqual(-3, result.X);
			Assert.AreEqual(-1, result.Y);
		}

		[TestMethod]
		public void MultiplyEuclideanVectorWithScalar()
		{
			EuclideanVector2D vector = new EuclideanVector2D(1, 2);
			double scalar = 2;

			EuclideanVector2D result = scalar * vector;

			Assert.AreEqual(2, result.X);
			Assert.AreEqual(4, result.Y);
		}

		[TestMethod]
		public void MultiplyEuclideanVectorWithEuclideanVector()
		{
			EuclideanVector2D a = new EuclideanVector2D(1, 2);
			EuclideanVector2D b = new EuclideanVector2D(3, 4);

			double result = a * b;

			Assert.AreEqual(11, result);
		}

		[TestMethod]
		public void CreateEuclideanVector2DFromPolarCoordinates()
		{
			double radius = 2;
			double azimutalAngle = 0.5 * Math.PI;
			EuclideanVector2D vector =
				EuclideanVector2D.CreateFromPolarCoordinates(radius, azimutalAngle);

			Assert.AreEqual(0, vector.X, 1e-15);
			Assert.AreEqual(2, vector.Y);
		}

		[TestMethod]
		public void CreateAzimutalUnitVectorAtPosition()
		{
			double[,] positions = new double[,] {
				{ -1.0, -1.0 }, { 1.0, -1.0 }, { 1.0, 1.0 }, { -1.0, 1.0 } };

			for(int i = 0; i < positions.GetLength(0); i++)
			{
				EuclideanVector2D position = new EuclideanVector2D(positions[i, 0], positions[i, 1]);
				EuclideanVector2D vector =
					EuclideanVector2D.CreateAzimutalUnitVectorAtPosition(position);

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
				EuclideanVector2D position = new EuclideanVector2D(positions[i, 0], positions[i, 1]);
				EuclideanVector2D vector =
					EuclideanVector2D.CreateRadialUnitVectorAtPosition(position);

				Assert.AreEqual(positions[i, 0] / Math.Sqrt(2), vector.X, 1e-15);
				Assert.AreEqual(positions[i, 1] / Math.Sqrt(2), vector.Y, 1e-15);
			}
		}
	}
}