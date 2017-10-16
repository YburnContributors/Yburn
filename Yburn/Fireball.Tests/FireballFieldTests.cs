using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void SimpleFireballField_ThrowIfInvalidInput()
		{
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldTypeException),
				(FireballFieldType)(-1), (SimpleFireballFieldFunction)null);
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldFunctionException),
				FireballFieldType.Temperature, (SimpleFireballFieldFunction)null);

			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldTypeException),
				(FireballFieldType)(-1), (double[,])null);
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldArrayException),
				FireballFieldType.Temperature, (double[,])null);
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldArrayException),
				FireballFieldType.Temperature, new double[11, 10]);
		}

		[TestMethod]
		public void StateSpecificFireballField_ThrowIfInvalidInput()
		{
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldTypeException),
				(FireballFieldType)(-1), null, null);
			AssertThrowsWhenGivenParams(typeof(ArgumentNullException),
				FireballFieldType.Temperature, null, null);
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldFunctionException),
				FireballFieldType.Temperature, new List<double>(), null);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CoordinateSystem CoordinateSystem = new CoordinateSystem(10, 1, true);

		private void AssertThrowsWhenGivenParams(
			Type exceptionType,
			FireballFieldType fireballFieldType,
			double[,] values
			)
		{
			bool threwException = false;
			try
			{
				new SimpleFireballField(fireballFieldType, CoordinateSystem, values);
			}
			catch(Exception ex)
			{
				Assert.IsInstanceOfType(ex, exceptionType);
				threwException = true;
			}

			Assert.IsTrue(threwException,
				"No Exception was thrown, expected: " + exceptionType.ToString() + ".");
		}

		private void AssertThrowsWhenGivenParams(
			Type exceptionType,
			FireballFieldType fireballFieldType,
			SimpleFireballFieldFunction function
			)
		{
			bool threwException = false;
			try
			{
				new SimpleFireballField(fireballFieldType, CoordinateSystem, function);
			}
			catch(Exception ex)
			{
				Assert.IsInstanceOfType(ex, exceptionType);
				threwException = true;
			}

			Assert.IsTrue(threwException,
				"No Exception was thrown, expected: " + exceptionType.ToString() + ".");
		}

		private void AssertThrowsWhenGivenParams(
			Type exceptionType,
			FireballFieldType fireballFieldType,
			IList<double> transverseMomenta,
			StateSpecificFireballFieldFunction function
			)
		{
			bool threwException = false;
			try
			{
				new StateSpecificFireballField(
					fireballFieldType, CoordinateSystem, transverseMomenta, function);
			}
			catch(Exception ex)
			{
				Assert.IsInstanceOfType(ex, exceptionType);
				threwException = true;
			}

			Assert.IsTrue(threwException,
				"No Exception was thrown, expected: " + exceptionType.ToString() + ".");
		}
	}
}
