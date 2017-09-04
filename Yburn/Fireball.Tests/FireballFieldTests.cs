using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
			AssertThrowsWhenGivenParams(typeof(OverflowException),
				(FireballFieldType)(-1), -1, -1, null);
			AssertThrowsWhenGivenParams(typeof(OverflowException),
				(FireballFieldType)(-1), 0, -1, null);
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldTypeException),
				(FireballFieldType)(-1), 0, 0, null);
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldFunctionException),
				FireballFieldType.Temperature, 0, 0, null);
		}

		[TestMethod]
		public void StateSpecificFireballField_ThrowIfInvalidInput()
		{
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldTypeException),
				(FireballFieldType)(-1), -1, -1, -1, null);
			AssertThrowsWhenGivenParams(typeof(OverflowException),
				FireballFieldType.Temperature, -1, -1, -1, null);
			AssertThrowsWhenGivenParams(typeof(OverflowException),
				FireballFieldType.Temperature, 0, -1, -1, null);
			AssertThrowsWhenGivenParams(typeof(OverflowException),
				FireballFieldType.Temperature, 0, 0, -1, null);
			AssertThrowsWhenGivenParams(typeof(InvalidFireballFieldFunctionException),
				FireballFieldType.Temperature, 0, 0, 0, null);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void AssertThrowsWhenGivenParams(
			Type exceptionType,
			FireballFieldType fireballFieldType,
			int xDimension,
			int yDimension,
			SimpleFireballFieldDiscreteFunction function
			)
		{
			bool threwException = false;
			try
			{
				new SimpleFireballField(fireballFieldType, xDimension, yDimension, function);
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
			int xDimension,
			int yDimension,
			int pTDimension,
			StateSpecificFireballFieldFunction function
			)
		{
			bool threwException = false;
			try
			{
				new StateSpecificFireballField(fireballFieldType,
					xDimension, yDimension, pTDimension, function);
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
