using System;
using System.Collections.Generic;

namespace Yburn
{
	partial class Worker
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/
		public Dictionary<string, string> VariableNameValuePairs
		{
			get
			{
				return GetVariableNameValuePairs();
			}
			set
			{
				SetVariableNameValuePairs(value ?? new Dictionary<string, string>());
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		protected static void TryExtract<T>(
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

		protected static void TryExtract<T>(
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

		protected static void TryExtract<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			ref T[][] value
			) where T : IConvertible
		{
			string stringifiedValue;
			nameValuePairs.TryGetValue(key, out stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				value = stringifiedValue.ToJaggedValueArray<T>();
			}
		}

		protected static void Store<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T value
			) where T : IConvertible
		{
			nameValuePairs[key] = value.ToUIString();
		}

		protected static void Store<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[] value
			) where T : IConvertible
		{
			nameValuePairs[key] = value.ToUIString();
		}

		protected static void Store<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			T[][] value
			) where T : IConvertible
		{
			nameValuePairs[key] = value.ToUIString();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected string DataFileName = "stdout.txt";
	}
}