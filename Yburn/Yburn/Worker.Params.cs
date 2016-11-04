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
			ref List<T> list
			) where T : IConvertible
		{
			string stringifiedValue;
			nameValuePairs.TryGetValue(key, out stringifiedValue);

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
			string stringifiedValue;
			nameValuePairs.TryGetValue(key, out stringifiedValue);

			if(!string.IsNullOrEmpty(stringifiedValue))
			{
				nestedList = stringifiedValue.ToNestedValueList<T>();
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
				nameValuePairs[key] = string.Empty;
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
				nameValuePairs[key] = string.Empty;
			}
			nameValuePairs[key] = nestedList.ToUIString();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected string DataFileName = "stdout.txt";
	}
}