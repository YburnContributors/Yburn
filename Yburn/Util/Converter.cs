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
			this T[] array
			) where T : IConvertible
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
			) where T : IConvertible
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
			string[] splittedList = SplitUIString(stringifiedList, separator);

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
			string[] splittedList = SplitUIString(stringifiedList, new char[] { '\t', ';' });

			T[][] jaggedArray = new T[splittedList.Length][];
			for(int i = 0; i < jaggedArray.Length; i++)
			{
				jaggedArray[i] = splittedList[i].ToValueArray<T>(new char[] { ' ', ',' });
			}

			return jaggedArray;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string[] SplitUIString(
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
				return new string[0];
			}
			else
			{
				return stringifiedList.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			}
		}
	}
}