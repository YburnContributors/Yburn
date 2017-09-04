using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.FormatUtil;
using Yburn.QQState;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class WorkerTests : Worker
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void GivenNullOrEmptyString_ReturnEmptyList()
		{
			AssertIsConvertedTo(null, new List<PotentialType> { });
			AssertIsConvertedTo("", new List<PotentialType> { });
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ThrowIfInvalidEnumEntry()
		{
			"invalid entry".ToValue<PotentialType>();
		}

		[TestMethod]
		public void GivenSpaceSeparatedList_ReturnEnumList()
		{
			AssertIsConvertedTo("Y1S x2P x3P",
				new List<BottomiumState> { BottomiumState.Y1S, BottomiumState.x2P, BottomiumState.x3P });
			AssertIsConvertedTo("GammaDiss None",
				new List<DecayWidthType> { DecayWidthType.GammaDiss, DecayWidthType.None });
		}

		[TestMethod]
		public void GivenMixedList_ReturnEnumList()
		{
			AssertIsConvertedTo("  Y1S x1P  , Y2S,\tx2P	Y3S ;x3P ",
				new List<BottomiumState> { BottomiumState.Y1S, BottomiumState.x1P,
					BottomiumState.Y2S, BottomiumState.x2P, BottomiumState.Y3S, BottomiumState.x3P });
		}

		// "Y1S,x1P,Y2S,x2P,Y3S,x3P"
		[TestMethod]
		public void GivenOneEntryContainsAnother_IncludeOnlySupposedEntry()
		{
			AssertIsConvertedTo("Complex_NoString",
				// PotentialType.Complex should not be contained
				new List<PotentialType> { PotentialType.Complex_NoString });
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertIsConvertedTo<TEnum>(
			string stringifiedList,
			List<TEnum> expectedList
			) where TEnum : struct, IConvertible
		{
			AssertSameEntries(expectedList, stringifiedList.ToValueList<TEnum>());
		}

		private static void AssertSameEntries<T>(
			List<T> expectedList,
			List<T> actualList
			)
		{
			Assert.AreEqual(expectedList.Count, actualList.Count);

			for(int i = 0; i < actualList.Count; i++)
			{
				Assert.AreEqual(expectedList[i], actualList[i]);
			}
		}

		/********************************************************************************************
		 * Inheritance of abstract methods
		 ********************************************************************************************/

		protected override void StartJob(
			string jobId
			)
		{
		}

		protected override Type GetEnumTypeByName(
			string enumName
			)
		{
			return typeof(int);
		}

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			return null;
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
		}
	}
}
