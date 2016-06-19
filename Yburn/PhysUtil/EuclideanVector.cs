using System;

namespace Yburn.PhysUtil
{
	public abstract class EuclideanVectorBase<TSelf>
		where TSelf : EuclideanVectorBase<TSelf>, new()
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected EuclideanVectorBase(double[] components)
		{
			Components = (double[])components.Clone();
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static TSelf operator +(
			EuclideanVectorBase<TSelf> left,
			EuclideanVectorBase<TSelf> right
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
			EuclideanVectorBase<TSelf> left,
			EuclideanVectorBase<TSelf> right
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
			EuclideanVectorBase<TSelf> vector
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
			EuclideanVectorBase<TSelf> vector,
			double scalar
			)
		{
			return scalar * vector;
		}

		public static TSelf operator /(
			EuclideanVectorBase<TSelf> vector,
			double scalar
			)
		{
			return 1 / scalar * vector;
		}

		public static double operator *(
			EuclideanVectorBase<TSelf> left,
			EuclideanVectorBase<TSelf> right
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

		public double this[int index]
		{
			get
			{
				return Components[index];
			}
			set
			{
				Components[index] = value;
			}
		}

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

		private double[] Components;
	}

	public class EuclideanVector2D : EuclideanVectorBase<EuclideanVector2D>
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
			double x,
			double y
			)
		{
			return CreateAzimutalUnitVectorAtPosition(new EuclideanVector2D(x, y));
		}

		public static EuclideanVector2D CreateAzimutalUnitVectorAtPosition(
			EuclideanVector2D position
			)
		{
			double angle = position.AzimutalAngle;

			return EuclideanVector2D.CreateFromPolarCoordinates(1, angle + 0.5 * Math.PI);
		}

		public static EuclideanVector2D CreateRadialUnitVectorAtPosition(
			double x,
			double y
			)
		{
			return CreateRadialUnitVectorAtPosition(new EuclideanVector2D(x, y));
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
				return this[0];
			}
			set
			{
				this[0] = value;
			}
		}

		public double Y
		{
			get
			{
				return this[1];
			}
			set
			{
				this[1] = value;
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

	public class EuclideanVector3D : EuclideanVectorBase<EuclideanVector3D>
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
				return this[0];
			}
			set
			{
				this[0] = value;
			}
		}

		public double Y
		{
			get
			{
				return this[1];
			}
			set
			{
				this[1] = value;
			}
		}

		public double Z
		{
			get
			{
				return this[2];
			}
			set
			{
				this[2] = value;
			}
		}
	}
}