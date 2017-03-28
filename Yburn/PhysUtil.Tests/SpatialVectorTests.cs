using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Yburn.PhysUtil.Tests
{
	[TestClass]
	public class SpatialVectorTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CreateVectorFromCoordinates()
		{
			SpatialVector vector = new SpatialVector(1, 2, 3);

			Assert.AreEqual(1, vector.X);
			Assert.AreEqual(2, vector.Y);
			Assert.AreEqual(3, vector.Z);
		}

		[TestMethod]
		public void AddVectors()
		{
			SpatialVector left = new SpatialVector(1, 2, 3);
			SpatialVector right = new SpatialVector(4, 5, 6);
			SpatialVector sum = left + right;

			Assert.AreEqual(5, sum.X);
			Assert.AreEqual(7, sum.Y);
			Assert.AreEqual(9, sum.Z);
		}

		[TestMethod]
		public void SubstractVectors()
		{
			SpatialVector left = new SpatialVector(1, 2, 3);
			SpatialVector right = new SpatialVector(6, 5, 4);
			SpatialVector difference = left - right;

			Assert.AreEqual(-5, difference.X);
			Assert.AreEqual(-3, difference.Y);
			Assert.AreEqual(-1, difference.Z);
		}

		[TestMethod]
		public void MultiplyVectorWithScalar()
		{
			SpatialVector vector = new SpatialVector(1, 2, 3);
			double scalar = 2;
			SpatialVector productLeft = scalar * vector;
			SpatialVector productRight = vector * scalar;

			Assert.AreEqual(2, productLeft.X);
			Assert.AreEqual(4, productLeft.Y);
			Assert.AreEqual(6, productLeft.Z);

			Assert.AreEqual(2, productRight.X);
			Assert.AreEqual(4, productRight.Y);
			Assert.AreEqual(6, productRight.Z);
		}

		[TestMethod]
		public void DivideVectorByScalar()
		{
			SpatialVector vector = new SpatialVector(1, 2, 3);
			double scalar = 2;
			SpatialVector quotient = vector / scalar;

			Assert.AreEqual(0.5, quotient.X);
			Assert.AreEqual(1, quotient.Y);
			Assert.AreEqual(1.5, quotient.Z);
		}

		[TestMethod]
		public void MultiplyVectorWithVector()
		{
			SpatialVector left = new SpatialVector(1, 2, 3);
			SpatialVector right = new SpatialVector(4, 5, 6);
			double product = left * right;

			Assert.AreEqual(32, product);
		}

		[TestMethod]
		public void ConvertVectorFieldComponents()
		{
			double radialPart = 1;
			double azimuthalPart = 2;
			double longitudinalPart = 3;

			SpatialVector vector1 = SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				1, 0, radialPart, azimuthalPart, longitudinalPart);
			Assert.AreEqual(1, vector1.X);
			Assert.AreEqual(2, vector1.Y);
			Assert.AreEqual(3, vector1.Z);

			SpatialVector vector2 = SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				0, 1, radialPart, azimuthalPart, longitudinalPart);
			Assert.AreEqual(-2, vector2.X);
			Assert.AreEqual(1, vector2.Y);
			Assert.AreEqual(3, vector2.Z);

			SpatialVector vector3 = SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				-1, 0, radialPart, azimuthalPart, longitudinalPart);
			Assert.AreEqual(-1, vector3.X);
			Assert.AreEqual(-2, vector3.Y);
			Assert.AreEqual(3, vector3.Z);

			SpatialVector vector4 = SpatialVector.ConvertCylindricalToEuclideanVectorFieldComponents(
				0, -1, radialPart, azimuthalPart, longitudinalPart);
			Assert.AreEqual(2, vector4.X);
			Assert.AreEqual(-1, vector4.Y);
			Assert.AreEqual(3, vector4.Z);
		}

		[TestMethod]
		public void GetVectorAzimuthalAngle()
		{
			double angle1 = new SpatialVector(1, 0, 0).AzimuthalAngle;
			Assert.AreEqual(0, angle1);

			double angle2 = new SpatialVector(1, 1, 0).AzimuthalAngle;
			Assert.AreEqual(0.25 * Math.PI, angle2);

			double angle3 = new SpatialVector(-1, 0, 0).AzimuthalAngle;
			Assert.AreEqual(Math.PI, angle3);

			double angle4 = new SpatialVector(1, -1, 0).AzimuthalAngle;
			Assert.AreEqual(-0.25 * Math.PI, angle4);
		}

		[TestMethod]
		public void GetVectorDirection()
		{
			SpatialVector vector = new SpatialVector(1, 2, 3);
			SpatialVector direction = vector.Direction;

			Assert.AreEqual(1 / Math.Sqrt(14), direction.X);
			Assert.AreEqual(2 / Math.Sqrt(14), direction.Y);
			Assert.AreEqual(3 / Math.Sqrt(14), direction.Z);
		}

		[TestMethod]
		public void GetVectorNorm()
		{
			SpatialVector vector = new SpatialVector(1, 2, 3);
			double norm = vector.Norm;

			Assert.AreEqual(Math.Sqrt(14), norm);
		}
	}
}
