using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yburn.TestUtil;

namespace Yburn.PhysUtil.Tests
{
	[TestClass]
	public class LinearInterpolationTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ThrowIf_InterpolationData_XNull()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(null, new double[] { 1 });
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayEmptyException))]
		public void ThrowIf_InterpolationData_XEmpty()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { }, new double[] { 1 });
		}

		[TestMethod]
		[ExpectedException(typeof(ArrayDisorderedException))]
		public void ThrowIf_InterpolationData_XDisordered()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { 1, 3, 2 }, new double[] { 1 });
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ThrowIf_InterpolationData_FNull()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { 1, 2, 3 }, null);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidArraySizesException))]
		public void ThrowIf_InterpolationData_FAndXareDifferentSize()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { 1, 2, 3 }, new double[] { });
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ThrowIf_ArgumentOutOfRange()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { 1, 2, 3 }, new double[] { 1, 2, 3 });
			interpolation.GetValue(-1);
		}

		[TestMethod]
		public void OnDataPoints_InterpolationExact()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { -3.1, 0.5, 4, 7.3 },
				new double[] { 23.3, -234.5, 45.4, -0.497 });
			AssertHelper.AssertRoundedEqual(23.3, interpolation.GetValue(-3.1));
			AssertHelper.AssertRoundedEqual(-234.5, interpolation.GetValue(0.5));
			AssertHelper.AssertRoundedEqual(45.4, interpolation.GetValue(4));
			AssertHelper.AssertRoundedEqual(-0.497, interpolation.GetValue(7.3));
		}

		[TestMethod]
		public void GivenLinearInput_InterpolationExact()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { -4, -3, 0, 1, 5 },
				new double[] { -6, -4, 2, 4, 12 });
			AssertHelper.AssertRoundedEqual(-6, interpolation.GetValue(-4));
			AssertHelper.AssertRoundedEqual(-4, interpolation.GetValue(-3));
			AssertHelper.AssertRoundedEqual(-2, interpolation.GetValue(-2));
			AssertHelper.AssertRoundedEqual(0, interpolation.GetValue(-1));
			AssertHelper.AssertRoundedEqual(2, interpolation.GetValue(0));
			AssertHelper.AssertRoundedEqual(4, interpolation.GetValue(1));
			AssertHelper.AssertRoundedEqual(6, interpolation.GetValue(2));
			AssertHelper.AssertRoundedEqual(8, interpolation.GetValue(3));
			AssertHelper.AssertRoundedEqual(10, interpolation.GetValue(4));
			AssertHelper.AssertRoundedEqual(12, interpolation.GetValue(5));
		}

		[TestMethod]
		public void GivenSineValues_InterpolateLinear()
		{
			LinearInterpolation1D interpolation = new LinearInterpolation1D(
				new double[] { -4, -3, 0, 1, 5, 8, 9 },
				new double[] { Math.Sin(-4), Math.Sin(-3), Math.Sin(0), Math.Sin(1), Math.Sin(5), Math.Sin(8), Math.Sin(9) });
			AssertHelper.AssertRoundedEqual(Math.Sin(-4), interpolation.GetValue(-4));
			AssertHelper.AssertRoundedEqual(Math.Sin(-3) / 1.5, interpolation.GetValue(-2));
			AssertHelper.AssertRoundedEqual(Math.Sin(0), interpolation.GetValue(0));
			AssertHelper.AssertRoundedEqual((Math.Sin(5) + 3 * Math.Sin(1)) / 4.0, interpolation.GetValue(2));
			AssertHelper.AssertRoundedEqual((3 * Math.Sin(5) + Math.Sin(1)) / 4.0, interpolation.GetValue(4));
			AssertHelper.AssertRoundedEqual((Math.Sin(8) + 2 * Math.Sin(5)) / 3.0, interpolation.GetValue(6));
			AssertHelper.AssertRoundedEqual((Math.Sin(9) + Math.Sin(8)) / 2.0, interpolation.GetValue(8.5));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidArraySizesException))]
		public void ThrowIf_InterpolationData_FAndYareDifferentSize()
		{
			LinearInterpolation2D interpolation = new LinearInterpolation2D(
				new double[] { 1 }, new double[] { 1, 2, 3 }, new double[,] { { 1, 2 } });
		}

		[TestMethod]
		public void GivenLinear2DInput_InterpolationExact()
		{
			LinearInterpolation2D interpolation = new LinearInterpolation2D(
				new double[] { -4, -3, 0 },
				new double[] { -1, 2, 5 },
				new double[,] { { -5, -2, 1 }, { -4, -1, 2 }, { -1, 2, 5 } });
			AssertHelper.AssertRoundedEqual(-0.5, interpolation.GetValue(-3.5, 3));
			AssertHelper.AssertRoundedEqual(0, interpolation.GetValue(-2, 2));
			AssertHelper.AssertRoundedEqual(2, interpolation.GetValue(-1, 3));
			AssertHelper.AssertRoundedEqual(0.3, interpolation.GetValue(-2.9, 3.2));
			AssertHelper.AssertRoundedEqual(-1.5, interpolation.GetValue(-4, 2.5));
			AssertHelper.AssertRoundedEqual(1, interpolation.GetValue(0, 1));
			AssertHelper.AssertRoundedEqual(0.6, interpolation.GetValue(-1, 1.6));
			AssertHelper.AssertRoundedEqual(-3.1, interpolation.GetValue(-3.2, 0.1));
			AssertHelper.AssertRoundedEqual(2.5, interpolation.GetValue(-2, 4.5));
		}
	}
}