/***********************************************************************************************
 * Saves parameter name-value pairs into a parameter file.
 ***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yburn.FormatUtil;

namespace Yburn.FileUtil
{
	public class ParaFileWriter
	{
		/********************************************************************************************
         * Public static members, functions and properties
         ********************************************************************************************/

		public static void Write(
			string pathFile,
			Dictionary<string, string> nameValuePairs
			)
		{
			File.WriteAllText(pathFile, GetParaFileText(nameValuePairs));
		}

		public static string GetParaFileText(
			Dictionary<string, string> nameValuePairs
			)
		{
			return IsNullOrEmpty(nameValuePairs) ?
				string.Empty : GetNonEmptyParaFileText(nameValuePairs);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected ParaFileWriter()
		{
			YburnFormat.UseYburnFormat();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static bool IsNullOrEmpty(
			Dictionary<string, string> nameValuePairs
			)
		{
			return nameValuePairs == null || nameValuePairs.Count == 0;
		}

		private static string GetNonEmptyParaFileText(
			Dictionary<string, string> nameValuePairs
			)
		{
			StringBuilder paraFileText = new StringBuilder();
			int length = GetLongestNameLength(nameValuePairs);
			foreach(KeyValuePair<string, string> nameValuePair in nameValuePairs)
			{
				AppendParaFileLine(paraFileText, length, nameValuePair);
			}

			return paraFileText.ToString();
		}

		private static int GetLongestNameLength(
			Dictionary<string, string> nameValuePairs
			)
		{
			int length = 0;
			foreach(string name in nameValuePairs.Keys)
			{
				length = Math.Max(length, name.Length);
			}

			return length;
		}

		private static void AppendParaFileLine(
			StringBuilder paraFileText,
			int length,
			KeyValuePair<string, string> nameValuePair
			)
		{
			if(!string.IsNullOrEmpty(nameValuePair.Key)
				&& !string.IsNullOrEmpty(nameValuePair.Value))
			{
				paraFileText.AppendFormat("{0,-" + length + "} = {1}" + Environment.NewLine,
					nameValuePair.Key, nameValuePair.Value);
			}
		}
	}
}
