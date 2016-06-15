using System;
using System.Collections.Generic;
using Yburn.Util;

namespace Yburn.Workers
{
	internal static class Extractor
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void TryExtract<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			ref T value
			) where T : IConvertible
		{
			string stringifiedValue;
			nameValuePairs.TryGetValue(key, out stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				value = stringifiedValue.ToValue<T>();
			}
		}

		public static void TryExtract<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			ref T[] value
			) where T : IConvertible
		{
			string stringifiedValue;
			nameValuePairs.TryGetValue(key, out stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				value = stringifiedValue.ToValueArray<T>();
			}
		}

		public static void TryExtract<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			ref T[][] value
			) where T : IConvertible
		{
			string stringifiedValue;
			nameValuePairs.TryGetValue(key, out stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				value = stringifiedValue.ToValueJaggedArray<T>();
			}
		}

		public static void Store<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T value
			) where T : IConvertible
		{
			nameValuePairs[key] = value.ToUIString();
		}

		public static void Store<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[] value
			) where T : IConvertible
		{
			nameValuePairs[key] = value.ToUIString();
		}

		public static void Store<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[][] value
			) where T : IConvertible
		{
			nameValuePairs[key] = value.ToUIString();
		}
	}
}