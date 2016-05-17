using System;
using System.Collections.Generic;

namespace Yburn.Workers
{
	internal static class Extractor
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static bool TryGetBool(
			Dictionary<string, string> nameValuePairs,
			string key,
			bool defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: bool.Parse(value);
		}

		public static double TryGetDouble(
			Dictionary<string, string> nameValuePairs,
			string key,
			double defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: double.Parse(value);
		}

		public static string[] TryGetStringArray(
			Dictionary<string, string> nameValuePairs,
			string key,
			string[] defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToStringArray();
		}

		public static double[] TryGetDoubleArray(
			Dictionary<string, string> nameValuePairs,
			string key,
			double[] defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToDoubleArray();
		}

		public static double[][] TryGetDoubleArrayArray(
			Dictionary<string, string> nameValuePairs,
			string key,
			double[][] defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToDoubleArrayArray();
		}

		public static int TryGetInt(
			Dictionary<string, string> nameValuePairs,
			string key,
			int defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: int.Parse(value);
		}

		public static int[] TryGetIntArray(
			Dictionary<string, string> nameValuePairs,
			string key,
			int[] defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToIntArray();
		}

		public static int[][] TryGetIntArrayArray(
			Dictionary<string, string> nameValuePairs,
			string key,
			int[][] defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToIntArrayArray();
		}

		public static EnumType TryGetEnum<EnumType>(
			Dictionary<string, string> nameValuePairs,
			string key,
			EnumType defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: (EnumType)Enum.Parse(typeof(EnumType), value);
		}

		public static EnumType[] TryGetEnumArray<EnumType>(
			Dictionary<string, string> nameValuePairs,
			string key,
			EnumType[] defaultIfNull
			)
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToEnumArray<EnumType>();
		}

		public static string TryGetString(
			Dictionary<string, string> nameValuePairs,
			string key
			)
		{
			return TryGetString(nameValuePairs, key, null);
		}

		public static string TryGetString(
			Dictionary<string, string> nameValuePairs,
			string key,
			string defaultIfNull
			)
		{
			string value;
			nameValuePairs.TryGetValue(key, out value);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value;
		}
	}
}