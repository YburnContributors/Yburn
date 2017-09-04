using System;

namespace Yburn.PhysUtil
{
	public struct SpatialVector
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public SpatialVector(
			double x,
			double y,
			double z
			)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static SpatialVector operator +(
			SpatialVector left,
			SpatialVector right
			)
		{
			return new SpatialVector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		public static SpatialVector operator -(
			SpatialVector left,
			SpatialVector right
			)
		{
			return left + (-1 * right);
		}

		public static SpatialVector operator *(
			double scalar,
			SpatialVector vector
			)
		{
			return new SpatialVector(scalar * vector.X, scalar * vector.Y, scalar * vector.Z);
		}

		public static SpatialVector operator *(
			SpatialVector vector,
			double scalar
			)
		{
			return scalar * vector;
		}

		public static SpatialVector operator /(
			SpatialVector vector,
			double scalar
			)
		{
			return (1 / scalar) * vector;
		}

		public static double operator *(
			SpatialVector left,
			SpatialVector right
			)
		{
			return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
		}

		public static SpatialVector ConvertCylindricalToEuclideanVectorFieldComponents(
			double positionX,
			double positionY,
			double radialPart,
			double azimuthalPart,
			double longitudinalPart
			)
		{
			return radialPart * CreateRadialUnitVectorAtPosition(positionX, positionY)
				+ azimuthalPart * CreateAzimuthalUnitVectorAtPosition(positionX, positionY)
				+ new SpatialVector(0, 0, longitudinalPart);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly double X;

		public readonly double Y;

		public readonly double Z;

		public double AzimuthalAngle
		{
			get
			{
				return Math.Atan2(Y, X);
			}
		}

		public SpatialVector Direction
		{
			get
			{
				if(Norm > 0)
				{
					return this / Norm;
				}
				else
				{
					return this;
				}
			}
		}

		public double Norm
		{
			get
			{
				return Math.Sqrt(this * this);
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static SpatialVector CreateRadialUnitVectorAtPosition(
			double x,
			double y
			)
		{
			return new SpatialVector(x, y, 0).Direction;
		}

		private static SpatialVector CreateAzimuthalUnitVectorAtPosition(
			double x,
			double y
			)
		{
			return new SpatialVector(-y, x, 0).Direction;
		}
	}
}
