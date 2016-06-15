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

		public static T TryGetValue<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T defaultIfNull
			) where T : IConvertible
		{
			string stringifiedValue;
			nameValuePairs.TryGetValue(key, out stringifiedValue);

			return string.IsNullOrEmpty(stringifiedValue) ?
				defaultIfNull
				: stringifiedValue.ToValue<T>();
		}

		public static void TryGetValue<T>(
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

		public static T[] TryGetValueArray<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[] defaultIfNull
			) where T : IConvertible
		{
			string value = TryGetValue(nameValuePairs, key, string.Empty);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToValueArray<T>();
		}

		public static void TryGetValue<T>(
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

		public static T[][] TryGetJaggedValueArray<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[][] defaultIfNull
			) where T : IConvertible
		{
			string value = TryGetValue(nameValuePairs, key, string.Empty);
			return string.IsNullOrEmpty(value) ?
				defaultIfNull
				: value.ToValueJaggedArray<T>();
		}

		public static void TryGetValue<T>(
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

		public static void TrySetValue<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T value
			) where T : IFormattable
		{
			nameValuePairs[key] = value.ToUIString();
		}

		public static void TrySetValue<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[] value
			) where T : IFormattable
		{
			nameValuePairs[key] = value.ToUIString();
		}

		public static void TrySetValue<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[][] value
			) where T : IFormattable
		{
			nameValuePairs[key] = value.ToUIString();
		}
	}
}