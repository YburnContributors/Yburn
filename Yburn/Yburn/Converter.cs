using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Yburn
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
				return string.Format(CultureInfo.InvariantCulture, "{0:G4}", value);
			}
			else
			{
				return string.Format(CultureInfo.InvariantCulture, "{0:G}", value);
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

		public static T ToValue<T>(
			this string stringifiedValue
			) where T : IConvertible
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

			return (T)converter.ConvertFromString(
				null, CultureInfo.InvariantCulture, stringifiedValue);
		}

		public static List<T> ToValueList<T>(
			this string stringifiedList,
			char[] separator = null
			) where T : IConvertible
		{
			List<string> splittedList = SplitUIString(stringifiedList, separator);

			List<T> list = new List<T>();
			foreach(string stringifiedValue in splittedList)
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
			List<string> splittedList = SplitUIString(stringifiedNestedList, new char[] { '\t', ';' });

			List<List<T>> nestedList = new List<List<T>>();
			foreach(string stringifiedList in splittedList)
			{
				nestedList.Add(stringifiedList.ToValueList<T>(new char[] { ' ', ',' }));
			}

			return nestedList;
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
	}
}