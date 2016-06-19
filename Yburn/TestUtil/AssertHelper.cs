using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yburn.TestUtil
{
	public static class AssertHelper
	{
		/********************************************************************************************
		   * Public static members, functions and properties
		   ********************************************************************************************/

		public static void AssertRoundedEqual(
			double expected,
			double actual,
			uint digits = 15
			)
		{
			if(expected == 0)
			{
				actual = Math.Round(actual, Convert.ToInt32(digits));
			}
			string format = "G" + digits.ToString("D");
			Assert.AreEqual(expected.ToString(format), actual.ToString(format));
		}

		public static void AssertAllElementsEqual(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			AssertAllKeysEqual(dict1, dict2);
			AssertAllValuesEqual(dict1, dict2);
		}

		public static void AssertFirstContainsSecond(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			AssertFirstContainsKeysOfSecond(dict1, dict2);
			AssertFirstContainsValuesOfSecond(dict1, dict2);
		}

		/********************************************************************************************
		   * Private/protected static members, functions and properties
		   ********************************************************************************************/

		private static void AssertAllValuesEqual(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			foreach(string key in dict1.Keys)
			{
				Assert.AreEqual(dict1[key].Trim(), dict2[key].Trim());
			}
		}

		private static void AssertAllKeysEqual(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			List<string> missingKeys = new List<string>();
			missingKeys.AddRange(GetKeysMissingInFirst(dict1, dict2));
			missingKeys.AddRange(GetKeysMissingInSecond(dict1, dict2));

			AssertNoMissingKeys(missingKeys);
		}

		private static List<string> GetKeysMissingInFirst(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			return GetKeysMissingInSecond(dict2, dict1);
		}

		private static List<string> GetKeysMissingInSecond(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			List<string> missingKeys = new List<string>();
			foreach(string dict1Key in dict1.Keys)
			{
				if(!dict2.ContainsKey(dict1Key))
				{
					missingKeys.Add(dict1Key);
				}
			}

			return missingKeys;
		}

		private static void AssertNoMissingKeys(
		   List<string> missingKeys
		   )
		{
			Assert.IsTrue(missingKeys.Count == 0,
			   GetMissingKeyNameList(missingKeys));
		}

		private static string GetMissingKeyNameList(
		   List<string> missingKeys
		   )
		{
			StringBuilder message = new StringBuilder("List of missing keys:");
			foreach(string key in missingKeys)
			{
				message.AppendFormat(" {0}", key);
			}

			return message.ToString();
		}

		private static void AssertFirstContainsKeysOfSecond(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			List<string> missingKeys = new List<string>();
			missingKeys.AddRange(GetKeysMissingInFirst(dict1, dict2));

			AssertNoMissingKeys(missingKeys);
		}

		private static void AssertFirstContainsValuesOfSecond(
		   Dictionary<string, string> dict1,
		   Dictionary<string, string> dict2
		   )
		{
			foreach(string key in dict2.Keys)
			{
				Assert.AreEqual(dict1[key].Trim(), dict2[key].Trim());
			}
		}
	}
}