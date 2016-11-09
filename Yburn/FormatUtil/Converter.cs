using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Yburn.FormatUtil
{
	public static class Converter
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static string ToUIString<T>(
			this T value
			) where T : IConvertible
		{
			if(value is double)
			{
				return string.Format(YburnFormat.YburnCulture, "{0:G4}", value);
			}
			else
			{
				return string.Format(YburnFormat.YburnCulture, "{0:G}", value);
			}
		}

		public static string ToUIString<T>(
			this List<T> list
			) where T : IConvertible
		{
			List<string> stringList = list.ConvertAll(element => element.ToUIString());

			return string.Join(",", stringList);
		}

		public static string ToUIString<T>(
			this List<List<T>> nestedList
			) where T : IConvertible
		{
			List<string> stringList = nestedList.ConvertAll(element => element.ToUIString());

			return string.Join(";", stringList);
		}

		public static string ToUIString<TKey, TValue>(
			this Dictionary<TKey, TValue> dictionary
			) where TKey : IConvertible where TValue : IConvertible
		{
			IEnumerable<string> stringifiedKeyValuePairs = dictionary.Select(
				element => element.Key.ToUIString() + ':' + element.Value.ToUIString());

			return string.Join(",", stringifiedKeyValuePairs);
		}

		public static T ToValue<T>(
			this string stringifiedValue
			) where T : IConvertible
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

			return (T)converter.ConvertFromString(
				null, YburnFormat.YburnCulture, stringifiedValue);
		}

		public static List<T> ToValueList<T>(
			this string stringifiedList,
			char[] separator = null
			) where T : IConvertible
		{
			List<string> stringifiedValues = SplitUIString(stringifiedList, separator);

			List<T> list = new List<T>();
			foreach(string stringifiedValue in stringifiedValues)
			{
				list.Add(stringifiedValue.ToValue<T>());
			}

			return list;
		}

		public static List<List<T>> ToNestedValueList<T>(
			this string stringifiedNestedList,
			char[] separator = null
			) where T : IConvertible
		{
			List<string> stringifiedLists
				= SplitUIString(stringifiedNestedList, new char[] { '\t', ';' });

			List<List<T>> nestedList = new List<List<T>>();
			foreach(string stringifiedList in stringifiedLists)
			{
				nestedList.Add(stringifiedList.ToValueList<T>(new char[] { ' ', ',' }));
			}

			return nestedList;
		}

		public static Dictionary<TKey, TValue> ToKeyValueDictionary<TKey, TValue>(
			this string stringifiedDictionary
			) where TKey : IConvertible where TValue : IConvertible
		{
			List<string> stringifiedKeyValuePairs = SplitUIString(stringifiedDictionary);

			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
			if(typeof(TKey).IsEnum)
			{
				dictionary = CreateEnumDictionary<TKey, TValue>();
			}

			foreach(string stringifiedKeyValuePair in stringifiedKeyValuePairs)
			{
				List<string> list = SplitUIString(stringifiedKeyValuePair, new char[] { ':' });
				if(list.Count != 2)
				{
					continue;
				}
				else
				{
					dictionary[list[0].ToValue<TKey>()] = list[1].ToValue<TValue>();
				}
			}

			return dictionary;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static List<string> SplitUIString(
			string stringifiedList,
			char[] separator = null
			)
		{
			if(separator == null)
			{
				separator = new char[] { ' ', '\t', ',', ';' };
			}
			if(string.IsNullOrEmpty(stringifiedList))
			{
				return new List<string>();
			}
			else
			{
				return new List<string>(stringifiedList.Split(
					separator, StringSplitOptions.RemoveEmptyEntries));
			}
		}

		private static Dictionary<TKey, TValue> CreateEnumDictionary<TKey, TValue>()
			where TKey : IConvertible where TValue : IConvertible
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

			foreach(TKey key in Enum.GetValues(typeof(TKey)))
			{
				dictionary.Add(key, default(TValue));
			}

			return dictionary;
		}
	}
}