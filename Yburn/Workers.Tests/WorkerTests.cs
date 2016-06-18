﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.QQState;
using Yburn.Util;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class WorkerTests : Yburn.Worker
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void GivenNullOrEmptyString_ReturnEmptyList()
		{
			AssertIsConvertedTo(null, new PotentialType[] { });
			AssertIsConvertedTo("", new PotentialType[] { });
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ThrowIfInvalidEnumEntry()
		{
			"invalid entry".ToValueArray<PotentialType>();
		}

		[TestMethod]
		public void GivenSpaceSeparatedList_ReturnEnumList()
		{
			AssertIsConvertedTo("Y1S x2P x3P",
				new BottomiumState[] { BottomiumState.Y1S, BottomiumState.x2P, BottomiumState.x3P });
			AssertIsConvertedTo("GammaDiss None",
				new DecayWidthType[] { DecayWidthType.GammaDiss, DecayWidthType.None });
		}

		[TestMethod]
		public void GivenMixedList_ReturnEnumList()
		{
			AssertIsConvertedTo("  Y1S x1P  , Y2S,\tx2P	Y3S ;x3P ",
				new BottomiumState[] { BottomiumState.Y1S, BottomiumState.x1P,
					BottomiumState.Y2S, BottomiumState.x2P, BottomiumState.Y3S, BottomiumState.x3P });
		}

		// "Y1S,x1P,Y2S,x2P,Y3S,x3P"
		[TestMethod]
		public void GivenOneEntryContainsAnother_IncludeOnlySupposedEntry()
		{
			AssertIsConvertedTo("Complex_NoString",
				// PotentialType.Complex should not be contained
				new PotentialType[] { PotentialType.Complex_NoString });
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void AssertIsConvertedTo<TEnum>(
			string stringifiedList,
			TEnum[] expectedArray
			) where TEnum : struct, IConvertible
		{
			AssertSameEntries(expectedArray, stringifiedList.ToValueArray<TEnum>());
		}

		private static void AssertSameEntries<T>(
			T[] expectedArray,
			T[] actualArray
			)
		{
			Assert.AreEqual(expectedArray.Length, actualArray.Length);

			for(int i = 0; i < actualArray.Length; i++)
			{
				Assert.AreEqual(expectedArray[i], actualArray[i]);
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