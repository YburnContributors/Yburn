using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class CoordinateSystem
	{
		public static void SwitchFromLabToLCFCoordinates(
			double t,
			double z,
			out double tau,
			out double rapidity
			)
		{
			tau = Math.Sqrt(t * t - z * z);
			rapidity = Functions.Artanh(z / t);
		}

		public static void SwitchFromLCFToLabCoordinates(
			double tau,
			double rapidity,
			out double t,
			out double z
			)
		{
			t = tau * Math.Cosh(rapidity);
			z = tau * Math.Sinh(rapidity);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public CoordinateSystem(
			double gridRadius,
			double gridCellSize,
			bool isCollisionSymmetric
			)
		{
			GridRadius = gridRadius;
			GridCellSize = gridCellSize;
			IsCollisionSymmetric = isCollisionSymmetric;

			if(isCollisionSymmetric)
			{
				SymmetryFactor *= 2;
			}

			AssertValidMembers();
			InitCoordinateAxes(out XAxis, out YAxis);
		}

		public CoordinateSystem(
			FireballParam param
			) : this(param.GridRadius_fm, param.GridCellSize_fm, param.IsCollisionSymmetric)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public readonly double GridRadius;

		public readonly double GridCellSize;

		public readonly bool IsCollisionSymmetric;

		public readonly int SymmetryFactor = 2;

		public readonly ReadOnlyCollection<double> XAxis;

		public readonly ReadOnlyCollection<double> YAxis;

		public int FindClosestXAxisIndex(
			double value
			)
		{
			return XAxis.Select((x, i) => new { Diff = Math.Abs(x - value), Index = i })
				.Aggregate((x, y) => x.Diff < y.Diff ? x : y).Index;
		}

		public int FindClosestYAxisIndex(
			double value
			)
		{
			return YAxis.Select((x, i) => new { Diff = Math.Abs(x - value), Index = i })
				.Aggregate((x, y) => x.Diff < y.Diff ? x : y).Index;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void InitCoordinateAxes(
			out ReadOnlyCollection<double> xAxis,
			out ReadOnlyCollection<double> yAxis
			)
		{
			SortedSet<double> halfAxis = new SortedSet<double> { 0 };
			int i = 1;
			while(i * GridCellSize <= GridRadius)
			{
				halfAxis.Add(i * GridCellSize);
				i++;
			}

			SortedSet<double> fullAxis = new SortedSet<double>(halfAxis);
			foreach(double item in halfAxis)
			{
				fullAxis.Add(-item);
			}

			if(IsCollisionSymmetric)
			{
				xAxis = halfAxis.ToList().AsReadOnly();
			}
			else
			{
				xAxis = fullAxis.ToList().AsReadOnly();
			}

			yAxis = halfAxis.ToList().AsReadOnly();
		}

		private void AssertValidMembers()
		{
			if(GridRadius < 0)
			{
				throw new Exception("GridRadius < 0.");
			}

			if(GridCellSize <= 0)
			{
				throw new Exception("GridCellSize <= 0.");
			}
		}
	}
}
