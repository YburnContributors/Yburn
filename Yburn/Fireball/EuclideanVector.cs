using System;

namespace Yburn.Fireball
{
	public abstract class EuclideanVector<TSelf>
		where TSelf : EuclideanVector<TSelf>, new()
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected EuclideanVector(double[] components)
		{
			Components = (double[])components.Clone();
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static TSelf operator +(
			EuclideanVector<TSelf> left,
			EuclideanVector<TSelf> right
			)
		{
			TSelf result = new TSelf();

			for(int i = 0; i < result.Dimension; i++)
			{
				result.Components[i] = left.Components[i] + right.Components[i];
			}

			return result;
		}

		public static TSelf operator -(
			EuclideanVector<TSelf> left,
			EuclideanVector<TSelf> right
			)
		{
			TSelf result = new TSelf();

			for(int i = 0; i < result.Dimension; i++)
			{
				result.Components[i] = left.Components[i] - right.Components[i];
			}

			return result;
		}

		public static TSelf operator *(
			double scalar,
			EuclideanVector<TSelf> vector
			)
		{
			TSelf result = new TSelf();

			for(int i = 0; i < result.Dimension; i++)
			{
				result.Components[i] = scalar * vector.Components[i];
			}

			return result;
		}

		public static TSelf operator *(
			EuclideanVector<TSelf> vector,
			double scalar
			)
		{
			return scalar * vector;
		}

		public static double operator *(
			EuclideanVector<TSelf> left,
			EuclideanVector<TSelf> right
			)
		{
			double result = 0;

			for(int i = 0; i < left.Dimension; i++)
			{
				result += left.Components[i] * right.Components[i];
			}

			return result;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public int Dimension
		{
			get
			{
				return Components.Length;
			}
		}

		public double Norm
		{
			get
			{
				return Math.Sqrt(this * this);
			}
		}

		public double[] ToDoubleArray()
		{
			return (double[])Components.Clone();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double[] Components;
	}

	public class EuclideanVector2D : EuclideanVector<EuclideanVector2D>
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public EuclideanVector2D(double x, double y)
			: base(new double[] { x, y })
		{
		}

		public EuclideanVector2D()
			: this(0, 0)
		{
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static EuclideanVector2D CreateFromPolarCoordinates(
			double radius,
			double azimutalAngle
			)
		{
			return new EuclideanVector2D(
				radius * Math.Cos(azimutalAngle),
				radius * Math.Sin(azimutalAngle));
		}

		public static EuclideanVector2D CreateAzimutalUnitVectorAtPosition(
			EuclideanVector2D position
			)
		{
			double angle = position.AzimutalAngle;

			return EuclideanVector2D.CreateFromPolarCoordinates(1, angle + 0.5 * Math.PI);
		}

		public static EuclideanVector2D CreateRadialUnitVectorAtPosition(
			EuclideanVector2D position
			)
		{
			double angle = position.AzimutalAngle;

			return EuclideanVector2D.CreateFromPolarCoordinates(1, angle);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double X
		{
			get
			{
				return Components[0];
			}
			set
			{
				Components[0] = value;
			}
		}

		public double Y
		{
			get
			{
				return Components[1];
			}
			set
			{
				Components[1] = value;
			}
		}

		public double AzimutalAngle
		{
			get
			{
				return Math.Atan2(Y, X);
			}
		}
	}

	public class EuclideanVector3D : EuclideanVector<EuclideanVector3D>
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public EuclideanVector3D(double x, double y, double z)
			: base(new double[] { x, y, z })
		{
		}

		public EuclideanVector3D()
			: this(0, 0, 0)
		{
		}

		public EuclideanVector3D(EuclideanVector2D vectorWithXAndY, double z)
			: this(vectorWithXAndY.X, vectorWithXAndY.Y, z)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double X
		{
			get
			{
				return Components[0];
			}
			set
			{
				Components[0] = value;
			}
		}

		public double Y
		{
			get
			{
				return Components[1];
			}
			set
			{
				Components[1] = value;
			}
		}

		public double Z
		{
			get
			{
				return Components[2];
			}
			set
			{
				Components[2] = value;
			}
		}
	}
}