using System;
using System.Collections.Generic;
using Yburn.FormatUtil;

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
			nameValuePairs.TryGetValue(key, out string stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				value = stringifiedValue.ToValue<T>();
			}
		}

		protected static void TryExtract<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			ref List<T> list
			) where T : IConvertible
		{
			nameValuePairs.TryGetValue(key, out string stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				list = stringifiedValue.ToValueList<T>();
			}
		}

		protected static void TryExtract<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			ref List<List<T>> nestedList
			) where T : IConvertible
		{
			nameValuePairs.TryGetValue(key, out string stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				nestedList = stringifiedValue.ToNestedValueList<T>();
			}
		}

		protected static void TryExtract<TKey, TValue>(
			Dictionary<string, string> nameValuePairs,
			string key,
			ref Dictionary<TKey, TValue> dictionary
			) where TKey : IConvertible where TValue : IConvertible
		{
			nameValuePairs.TryGetValue(key, out string stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				dictionary = stringifiedValue.ToKeyValueDictionary<TKey, TValue>();
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
			List<T> list
			) where T : IConvertible
		{
			if(list == null)
			{
				list = new List<T>();
			}
			nameValuePairs[key] = list.ToUIString();
		}

		protected static void Store<T>(
			Dictionary<string, string> nameValuePairs,
			string key,
			List<List<T>> nestedList
			) where T : IConvertible
		{
			if(nestedList == null)
			{
				nestedList = new List<List<T>>();
			}
			nameValuePairs[key] = nestedList.ToUIString();
		}

		protected static void Store<TKey, TValue>(
			Dictionary<string, string> nameValuePairs,
			string key,
			Dictionary<TKey, TValue> dictionary
			) where TKey : IConvertible where TValue : IConvertible
		{
			if(dictionary == null)
			{
				dictionary = new Dictionary<TKey, TValue>();
			}
			nameValuePairs[key] = dictionary.ToUIString();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected string DataFileName = "stdout.txt";
	}
}
