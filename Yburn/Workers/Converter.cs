using System;

namespace Yburn.Workers
{
	public static class Converter
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static string[] StringToStringArray(
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
				return new string[] { };
			}
			else
			{
				return stringifiedList.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		public static double[] StringToDoubleArray(
			string stringifiedList,
			char[] separator = null
			)
		{
			string[] splittedList = StringToStringArray(stringifiedList, separator);
			double[] array = new double[splittedList.Length];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = double.Parse(splittedList[i]);
			}

			return array;
		}

		public static double[][] StringToDoubleArrayArray(
			string stringifiedList
			)
		{
			string[] splittedList = StringToStringArray(stringifiedList, new char[] { '\t', ';' });
			double[][] array = new double[splittedList.Length][];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = StringToDoubleArray(splittedList[i], new char[] { ' ', ',' });
			}

			return array;
		}

		public static int[] StringToIntArray(
			string stringifiedList,
			char[] separator = null
			)
		{
			string[] splittedList = StringToStringArray(stringifiedList, separator);
			int[] array = new int[splittedList.Length];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = int.Parse(splittedList[i]);
			}

			return array;
		}

		public static int[][] StringToIntArrayArray(
			string stringifiedList
			)
		{
			string[] splittedList = StringToStringArray(stringifiedList, new char[] { '\t', ';' });
			int[][] array = new int[splittedList.Length][];
			for(int i = 0; i < array.Length; i++)
			{
				array[i] = StringToIntArray(splittedList[i], new char[] { ' ', ',' });
			}

			return array;
		}

		public static EnumType[] StringToEnumArray<EnumType>(
			string stringifiedList
			)
		{
			if(string.IsNullOrEmpty(stringifiedList))
			{
				return new EnumType[] { };
			}
			else
			{
				string[] splittedList = stringifiedList.Split(new char[] { ' ', '\t', ',', ';' },
					StringSplitOptions.RemoveEmptyEntries);

				EnumType[] enumArray = new EnumType[splittedList.Length];
				for(int i = 0; i < enumArray.Length; i++)
				{
					enumArray[i] = (EnumType)Enum.Parse(typeof(EnumType), splittedList[i]);
				}

				return enumArray;
			}
		}

		public static string StringArrayToString(
			string[] array
			)
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = array[0];
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += "," + array[i];
				}

				return stringifiedList;
			}
		}

		public static string DoubleArrayToString(
			double[] array
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

		public static string DoubleArrayArrayToString(
			double[][] array
			)
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = DoubleArrayToString(array[0]);
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += ";" + DoubleArrayToString(array[i]);
				}

				return stringifiedList;
			}
		}

		public static string IntArrayToString(
			int[] array
			)
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

		public static string IntArrayArrayToString(
			int[][] array
			)
		{
			if(array == null || array.Length == 0)
			{
				return string.Empty;
			}
			else
			{
				string stringifiedList = IntArrayToString(array[0]);
				for(int i = 1; i < array.Length; i++)
				{
					stringifiedList += ";" + IntArrayToString(array[i]);
				}

				return stringifiedList;
			}
		}

		public static string EnumArrayToString<EnumType>(
			EnumType[] array
			)
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
	}
}