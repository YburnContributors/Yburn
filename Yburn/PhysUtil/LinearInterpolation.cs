using System;

namespace Yburn.PhysUtil
{
	/***********************************************************************************************
	 * LinearInterpolationBase contains variables and methods common to derived linear interpolation
	 * classes of a given dimension.
	 ***********************************************************************************************/

	public abstract class LinearInterpolationBase
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public LinearInterpolationBase(
			double[] x
			)
		{
			AssertValidXarray(x);

			N = x.Length;
			X = x;
			C = 0;
			Xmin = x[0];
			Xmax = x[N - 1];
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// shortcut to the upper array boundary
		public double Xmax
		{
			get;
			protected set;
		}

		// shortcut to the lower array boundary
		public double Xmin
		{
			get;
			protected set;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertValidXarray(
			double[] x
			)
		{
			if(x == null)
			{
				throw new ArgumentNullException("X-array is null.");
			}

			if(x.Length == 0)
			{
				throw new ArrayEmptyException("X-array is empty.");
			}

			if(IsArrayDisordered(x))
			{
				throw new ArrayDisorderedException("X-array points are disordered.");
			}
		}

		private static bool IsArrayDisordered(
			double[] x
			)
		{
			for(int i = 1; i < x.Length; i++)
			{
				if(x[i - 1] >= x[i])
				{
					return true;
				}
			}

			return false;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// X-array
		protected double[] X;

		// number of array points
		protected int N;

		// index of the array point closest to the last point of evaluation
		// C is chosen such that X[C] <= x < X[C + 1] for arbitrary x that
		// does not exceed the array boundary
		protected int C;

		// Finding C such that x is enclosed in the interval [X[C], X[C + 1]],
		protected void FindInterval(
			double x
			)
		{
			if(x < Xmin || x > Xmax)
			{
				throw new ArgumentOutOfRangeException();
			}

			if(x == X[C])
			{
				// if x = X[C] we do nothing because we have already found the right interval
				return;
			}
			else if(x < X[C])
			{
				// if x < X[C] we go towards smaller X values to find an index
				// such that X[i - 1] <= x < X[i] and set C = i - 1
				for(int i = C; i > 0; i--)
				{
					if(x >= X[i - 1])
					{
						C = i - 1;
						break;
					}
				}
			}
			else if(x > X[C])
			{
				// if x > X[C] we go towards larger X values to find an index
				// such that X[i] < x <= X[i + 1] and set C = i + 1 if x = X[i + 1]
				// and C = i if x < X[i]
				for(int i = C; i < N - 1; i++)
				{
					if(x <= X[i + 1])
					{
						C = x == X[i + 1] ? i + 1 : i;
						break;
					}
				}
			}
		}

		protected double InterpolateLinear(
			double x,
			double xlo,
			double xhi,
			double flo,
			double fhi
			)
		{
			return flo + (fhi - flo) * (x - xlo) / (xhi - xlo);
		}
	}

	[Serializable]
	public class ArrayEmptyException : Exception
	{
		public ArrayEmptyException(
			string message
			)
			: base(message)
		{
		}
	}

	[Serializable]
	public class ArrayDisorderedException : Exception
	{
		public ArrayDisorderedException(
			string message
			)
			: base(message)
		{
		}
	}

	[Serializable]
	public class InvalidArraySizesException : Exception
	{
		public InvalidArraySizesException(
			string message
			)
			: base(message)
		{
		}
	}

	/***********************************************************************************************
	 * Instances of the class LinearInterpolation1D are used to interpolate values of a scalar
	 * function f(x) defined on the real numbers x. The f[i] = f(x[i]) should be given on a set of
	 * ascending points x[i], i.e. x[i] < x[i+1]. The input has to be given in the form of arrays
	 * double[] x and double[] f.
	 ***********************************************************************************************/

	public class LinearInterpolation1D : LinearInterpolationBase
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public LinearInterpolation1D(
			double[] x,
			double[] f
			)
			: base(x)
		{
			AssertValidFarray(f, x.Length);

			F = f;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double GetValue(
			double x
			)
		{
			FindInterval(x);

			// if x is on a array point there is no need for interpolation
			if(x == X[C])
			{
				return F[C];
			}
			else
			{
				return InterpolateLinear(x, X[C], X[C + 1], F[C], F[C + 1]);
			}
		}

		public double GetValue(
			double x,
			double defaultIfBelowXmin,
			double defaultIfAboveXmax
			)
		{
			if(x < Xmin)
			{
				return defaultIfBelowXmin;
			}
			else if(x > Xmax)
			{
				return defaultIfAboveXmax;
			}
			else
			{
				return GetValue(x);
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertValidFarray(
			double[] f,
			int lengthX
			)
		{
			if(f == null)
			{
				throw new ArgumentNullException("F-array is null.");
			}

			if(f.Length != lengthX)
			{
				throw new InvalidArraySizesException(
					"Length of X-array does not match that of the F-array.");
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// function values
		private double[] F;
	}

	/***********************************************************************************************
	 * Instances of the class LinearInterpolation2D are used to interpolate values of a scalar
	 * function f(x,y) defined on the real numbers (x,y). The f[i,j] = f(x[i],y[j]) should be given
	 * on a set of ascending points (x[i],y[j]), i.e. x[i] < x[i+1] and y[j] < y[j+1]. The input has
	 * to be given in the form of one-dimensional arrays double[] x, double[] y and a two-
	 * dimensional array double[,] f.
	 ***********************************************************************************************/

	public class LinearInterpolation2D : LinearInterpolationBase
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public LinearInterpolation2D(
			double[] x,
			double[] y,
			double[,] f
			)
			: base(x)
		{
			AssertValidF2Darray(f, x.Length, y.Length);

			Xmin = x[0];
			Xmax = x[N - 1];
			Ymin = y[0];
			Ymax = y[y.Length - 1];

			Set1DInterpolators(y, f);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// shortcut to the upper array boundary
		public double Ymax
		{
			get;
			protected set;
		}

		// shortcut to the lower array boundary
		public double Ymin
		{
			get;
			protected set;
		}

		public double GetValue(
			double x,
			double y
			)
		{
			FindInterval(x);

			// if x is on a array point there is no need for interpolation
			if(x == X[C])
			{
				return LinearInterpolation1D[C].GetValue(y);
			}
			else
			{
				return InterpolateLinear(x, X[C], X[C + 1],
					LinearInterpolation1D[C].GetValue(y), LinearInterpolation1D[C + 1].GetValue(y));
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertValidF2Darray(
			double[,] f,
			int lengthX,
			int lengthY
			)
		{
			if(f == null)
			{
				throw new ArgumentNullException("F-array is null.");
			}

			if(f.GetLength(0) != lengthX)
			{
				throw new InvalidArraySizesException(
					"Length of X-array does not match that of the F-array.");
			}

			if(f.GetLength(1) != lengthY)
			{
				throw new InvalidArraySizesException(
					"Length of Y-array does not match that of the F-array.");
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// This array stores the Y-arrays and the two-dimensional Fij array. It is used to interpolate
		// in the Y-direction so its results can be used for a subsequent interpolation in the
		// X-direction.
		private LinearInterpolation1D[] LinearInterpolation1D;

		private void Set1DInterpolators(
			double[] y,
			double[,] f
			)
		{
			LinearInterpolation1D = new LinearInterpolation1D[N];
			for(int i = 0; i < N; i++)
			{
				// create new array from row
				double[] fy = new double[y.Length];
				for(int j = 0; j < y.Length; j++)
				{
					fy[j] = f[i, j];
				}

				LinearInterpolation1D[i] = new LinearInterpolation1D(y, fy);
			}
		}
	}
}
