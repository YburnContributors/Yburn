using System;
using System.Collections.Generic;
using System.IO;

namespace Yburn
{
	public static class YburnConfigFile
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static string QQDataPathFile
		{
			get
			{
				return GetValue("QQDataPathFile");
			}
			set
			{
				SetValue("QQDataPathFile", value);
			}
		}

		public static string LastParaFile
		{
			get
			{
				return GetValue("LastParaFile");
			}
			set
			{
				SetValue("LastParaFile", value);
			}
		}

		public static string OutputPath
		{
			get
			{
				return GetValue("OutputPath");
			}
			set
			{
				SetValue("OutputPath", value);
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string ConfigPathFile = "..\\..\\..\\YburnConfigFile.txt";

		private static string CommentarySign = "#";

		private static string GetValue(
			string parameterName
			)
		{
			if(File.Exists(ConfigPathFile))
			{
				return GetValue(File.ReadAllLines(ConfigPathFile), parameterName);
			}

			return string.Empty;
		}

		private static string GetValue(
			string[] allLines,
			string parameterName
			)
		{
			foreach(string line in allLines)
			{
				if(!line.StartsWith(CommentarySign)
					&& line.StartsWith(parameterName))
				{
					return ExtractParameterValue(line);
				}
			}

			return string.Empty;
		}

		private static string ExtractParameterValue(
			string line
			)
		{
			string[] splittedLine = line.Split(new char[] { ' ' }, 2,
				StringSplitOptions.RemoveEmptyEntries);
			if(splittedLine.Length > 1)
			{
				return splittedLine[1];
			}
			else
			{
				return string.Empty;
			}
		}

		private static void SetValue(
			string parameterName,
			string parameterValue
			)
		{
			if(!File.Exists(ConfigPathFile))
			{
				File.Create(ConfigPathFile).Close();
			}

			List<string> allLines = new List<string>(File.ReadAllLines(ConfigPathFile));
			SetValue(allLines, parameterName, parameterValue);
		}

		private static void SetValue(
			List<string> allLines,
			string parameterName,
			string parameterValue
			)
		{
			int lineIndex = GetLineIndex(allLines, parameterName);
			string newNameValuePair = parameterName + " " + parameterValue;
			if(lineIndex >= 0)
			{
				allLines[lineIndex] = newNameValuePair;
			}
			else
			{
				allLines.Add(newNameValuePair);
			}

			File.WriteAllLines(ConfigPathFile, allLines);
		}

		private static int GetLineIndex(
			List<string> allLines,
			string parameterName
			)
		{
			foreach(string line in allLines)
			{
				if(!line.StartsWith(CommentarySign)
					&& line.StartsWith(parameterName))
				{
					return allLines.IndexOf(line);
				}
			}

			return -1;
		}
	}
}