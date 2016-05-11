using System;

namespace Yburn.Fireball
{
	public class EuclideanVector
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public EuclideanVector(double[] components)
		{
			Components = components;
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static EuclideanVector operator +(EuclideanVector v, EuclideanVector w)
		{
			if(v.Dimension == w.Dimension)
			{
				double[] vComponents = v.ToDoubleArray();
				double[] wComponents = w.ToDoubleArray();
				double[] addedComponents = new double[vComponents.Length];

				for(int i = 0; i < addedComponents.Length; i++)
				{
					addedComponents[i] = vComponents[i] + wComponents[i];
				}

				return new EuclideanVector(addedComponents);
			}
			else
			{
				throw new VectorDimensionMismatchException();
			}
		}

		public static EuclideanVector operator -(EuclideanVector v, EuclideanVector w)
		{
			return v + w.Negate();
		}

		public static double operator *(EuclideanVector v, EuclideanVector w)
		{
			return CalculateScalarProduct(v, w);
		}

		public static double CalculateScalarProduct(EuclideanVector v, EuclideanVector w)
		{
			if(v.Dimension == w.Dimension)
			{
				double[] vComponents = v.ToDoubleArray();
				double[] wComponents = w.ToDoubleArray();
				double scalarProduct = 0;

				for(int i = 0; i < vComponents.Length; i++)
				{
					scalarProduct += vComponents[i] * wComponents[i];
				}

				return scalarProduct;
			}
			else
			{
				throw new VectorDimensionMismatchException();
			}
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
				return Math.Sqrt(CalculateScalarProduct(this, this));
			}
		}

		public double AzimutalAngle
		{
			get
			{
				if(Dimension >= 2)
				{
					return Math.Atan2(Components[1], Components[0]);
				}
				else
				{
					throw new InsufficientVectorDimensionException(
						"Vector dimension has to be at least 2.");
				}
			}
		}

		public EuclideanVector Negate()
		{
			double[] negatedComponents = new double[Components.Length];

			for(int i = 0; i < negatedComponents.Length; i++)
			{
				negatedComponents[i] = -Components[i];
			}

			return new EuclideanVector(negatedComponents);
		}

		public double[] ToDoubleArray()
		{
			return Components;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double[] Components;
	}

	public class EuclideanVector2D : EuclideanVector
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
			double positionX,
			double positionY
			)
		{
			double angle = new EuclideanVector2D(positionX, positionY).AzimutalAngle;

			return EuclideanVector2D.CreateFromPolarCoordinates(1, angle + 0.5 * Math.PI);
		}

		public static EuclideanVector2D CreateRadialUnitVectorAtPosition(
			double positionX,
			double positionY
			)
		{
			double angle = new EuclideanVector2D(positionX, positionY).AzimutalAngle;

			return EuclideanVector2D.CreateFromPolarCoordinates(1, angle);
		}

		public static EuclideanVector2D operator +(EuclideanVector2D v, EuclideanVector2D w)
		{
			return v + w;
		}

		public static EuclideanVector2D operator -(EuclideanVector2D v, EuclideanVector2D w)
		{
			return v - w;
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
	}

	public class EuclideanVector3D : EuclideanVector
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

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static EuclideanVector3D operator +(EuclideanVector3D v, EuclideanVector3D w)
		{
			return v + w;
		}

		public static EuclideanVector3D operator -(EuclideanVector3D v, EuclideanVector3D w)
		{
			return v - w;
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

	public class VectorDimensionMismatchException : Exception
	{
		public VectorDimensionMismatchException()
			: base("Vectors have different dimensions.")
		{
		}
	}

	public class InsufficientVectorDimensionException : Exception
	{
		public InsufficientVectorDimensionException(string message)
			: base(message)
		{
		}
	}
}