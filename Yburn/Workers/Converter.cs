using System;

namespace Yburn.Workers
{
	public static class Converter
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static string[] ToStringArray(
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

		public static double[] ToDoubleArray(
			this string stringifiedList,
			char[] separator = null
			)
		{
			string[] splittedList = stringifiedList.ToStringArray(separator);
			double[] array = new double[splittedList.Length];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = double.Parse(splittedList[i]);
			}

			return array;
		}

		public static double[][] ToDoubleArrayArray(
			this string stringifiedList
			)
		{
			string[] splittedList = stringifiedList.ToStringArray(new char[] { '\t', ';' });
			double[][] array = new double[splittedList.Length][];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = splittedList[i].ToDoubleArray(new char[] { ' ', ',' });
			}

			return array;
		}

		public static int[] ToIntArray(
			this string stringifiedList,
			char[] separator = null
			)
		{
			string[] splittedList = stringifiedList.ToStringArray(separator);
			int[] array = new int[splittedList.Length];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = int.Parse(splittedList[i]);
			}

			return array;
		}

		public static int[][] ToIntArrayArray(
			this string stringifiedList
			)
		{
			string[] splittedList = stringifiedList.ToStringArray(new char[] { '\t', ';' });
			int[][] array = new int[splittedList.Length][];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = splittedList[i].ToIntArray(new char[] { ' ', ',' });
			}

			return array;
		}

		public static TEnum[] ToEnumArray<TEnum>(
			this string stringifiedList
			) where TEnum : struct, IConvertible
		{
			if(string.IsNullOrEmpty(stringifiedList))
			{
				return new TEnum[] { };
			}
			else
			{
				string[] splittedList = stringifiedList.Split(new char[] { ' ', '\t', ',', ';' },
					StringSplitOptions.RemoveEmptyEntries);

				TEnum[] enumArray = new TEnum[splittedList.Length];
				for(int i = 0; i < enumArray.Length; i++)
				{
					enumArray[i] = (TEnum)Enum.Parse(typeof(TEnum), splittedList[i]);
				}

				return enumArray;
			}
		}

		public static string ToStringifiedList(
			this double[] array
			)
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = array[0].ToString("G4");
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += "," + array[i].ToString("G4");
				}

				return stringifiedList;
			}
		}

		public static string ToStringifiedList(
			this double[][] array
			)
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = array[0].ToStringifiedList();
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += ";" + array[i].ToStringifiedList();
				}

				return stringifiedList;
			}
		}

		public static string ToStringifiedList<T>(
			this T[] array
			) where T : IConvertible
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = array[0].ToString();
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += "," + array[i].ToString();
				}

				return stringifiedList;
			}
		}

		public static string ToStringifiedList<T>(
			this T[][] array
			) where T : IConvertible
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = array[0].ToStringifiedList<T>();
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += ";" + array[i].ToStringifiedList<T>();
				}

				return stringifiedList;
			}
		}
	}
}