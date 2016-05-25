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

		public static TEnum TryGetEnum<TEnum>(
			Dictionary<string, string> nameValuePairs,
			string key,
			TEnum defaultIfNull
			) where TEnum : struct, IConvertible
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: (TEnum)Enum.Parse(typeof(TEnum), value);
		}

		public static TEnum[] TryGetEnumArray<TEnum>(
			Dictionary<string, string> nameValuePairs,
			string key,
			TEnum[] defaultIfNull
			) where TEnum : struct, IConvertible
		{
			string value = TryGetString(nameValuePairs, key);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToEnumArray<TEnum>();
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