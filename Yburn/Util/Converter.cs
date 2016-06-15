using System;
using System.ComponentModel;
using System.Globalization;

namespace Yburn.Util
{
	public static class Converter
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static string ToUIString<T>(
			this T value
			) where T : IFormattable
		{
			if(value is double)
			{
				return value.ToString("G4", CultureInfo.InvariantCulture);
			}
			return value.ToString("G", CultureInfo.InvariantCulture);
		}

		public static string ToUIString<T>(
			this T[] array
			) where T : IFormattable
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = array[0].ToUIString();
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += "," + array[i].ToUIString();
				}

				return stringifiedList;
			}
		}

		public static string ToUIString<T>(
			this T[][] array
			) where T : IFormattable
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = array[0].ToUIString<T>();
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += ";" + array[i].ToUIString<T>();
				}

				return stringifiedList;
			}
		}

		public static T ToValue<T>(
			this string uiString
			) where T : IConvertible
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

			return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, uiString);
		}

		public static T[] ToValueArray<T>(
			this string stringifiedList,
			char[] separator = null
			) where T : IConvertible
		{
			string[] splittedList = stringifiedList.ToStringArray(separator);

			T[] array = new T[splittedList.Length];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = splittedList[i].ToValue<T>();
			}

			return array;
		}

		public static T[][] ToValueJaggedArray<T>(
			this string stringifiedList
			) where T : IConvertible
		{
			string[] splittedList = stringifiedList.ToStringArray(new char[] { '\t', ';' });

			T[][] jaggedArray = new T[splittedList.Length][];
			for(int i = 0; i < jaggedArray.Length; i++)
			{
				jaggedArray[i] = splittedList[i].ToValueArray<T>(new char[] { ' ', ',' });
			}

			return jaggedArray;
		}

		public static string ToStringifiedList(
			this string[] array
			)
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				return string.Join(",", array);
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string[] ToStringArray(
			this string stringifiedList,
			char[] separator = null
			)
		{
			if(separator == null)
			{
				separator = new char[] { ' ', '\t', ',', ';' };
			}
			if(string.IsNullOrEmpty(stringifiedList))
			{
				return new string[] { };
			}
			else
			{
				return stringifiedList.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			}
		}
	}
}