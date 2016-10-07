using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

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

		public static string ConcatenateStringTable(
			this string[,] table,
			bool isFirstRowLabel,
			bool isFirstColumnLabel
			)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] columnFormatStrings = GetColumnFormatStrings(table, isFirstColumnLabel);

			for(int row = 0; row < table.GetLength(0); row++)
			{
				for(int col = 0; col < table.GetLength(1); col++)
				{
					stringBuilder.AppendFormat(columnFormatStrings[col], table[row, col]);
				}
				stringBuilder.AppendLine();

				if(row == 0 && isFirstRowLabel)
				{
					stringBuilder.AppendLine();
				}
			}

			return stringBuilder.ToString();
		}

		public static T[,] GetTransposedArray<T>(
			this T[,] array
			)
		{
			T[,] transpose = new T[array.GetLength(1), array.GetLength(0)];

			for(int i = 0; i < transpose.GetLength(0); i++)
			{
				for(int j = 0; j < transpose.GetLength(1); j++)
				{
					transpose[i, j] = array[j, i];
				}
			}

			return transpose;
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

		private static string[] GetColumnFormatStrings(
			string[,] table,
			bool isFirstColumnLabel
			)
		{
			int[] columnWidths = GetStringTableColumnWidths(table);
			string[] formatStrings = new string[columnWidths.Length];

			for(int i = 0; i < formatStrings.Length; i++)
			{
				formatStrings[i] = string.Format("{{0,{0}}}", columnWidths[i] + 2);
			}

			if(isFirstColumnLabel)
			{
				formatStrings[0] = string.Format("{{0,-{0}}}", columnWidths[0]);
			}

			return formatStrings;
		}

		private static int[] GetStringTableColumnWidths(
			string[,] table
			)
		{
			int[] columnWidths = new int[table.GetLength(1)];

			for(int col = 0; col < table.GetLength(1); col++)
			{
				string[] column = new string[table.GetLength(0)];
				for(int row = 0; row < table.GetLength(0); row++)
				{
					column[row] = table[row, col];
				}
				columnWidths[col] = column.Max(element => element.Length);
			}

			return columnWidths;
		}
	}
}