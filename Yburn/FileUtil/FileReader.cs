using System;
using System.Collections.Generic;
using Yburn.FormatUtil;

namespace Yburn.FileUtil
{
	public abstract class FileReader
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected FileReader()
		{
			YburnFormat.UseYburnFormat();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string GetSectionBeforeCommentary(
			string completeLine
			)
		{
			return completeLine.Split(
				new char[] { '#' }, 2, StringSplitOptions.RemoveEmptyEntries)[0];
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected void RemoveLinesWithoutEqualSign(
			List<string> allLines
			)
		{
			for(int j = allLines.Count - 1; j >= 0; j--)
			{
				if(!allLines[j].Contains("="))
				{
					allLines.RemoveAt(j);
				}
			}
		}

		protected void RemoveCommentaryLines(
			List<string> allLines
			)
		{
			for(int j = allLines.Count - 1; j >= 0; j--)
			{
				if(allLines[j].StartsWith("#"))
				{
					allLines.RemoveAt(j);
				}
			}
		}

		protected void RemoveCommentaryWithinLines(
			List<string> allLines
			)
		{
			for(int j = allLines.Count - 1; j >= 0; j--)
			{
				if(allLines[j].Contains("#"))
				{
					allLines[j] = GetSectionBeforeCommentary(allLines[j]);
				}
			}
		}

		protected void RemoveTabsAndSpaces(
			List<string> allLines
			)
		{
			for(int j = allLines.Count - 1; j >= 0; j--)
			{
				allLines[j] = allLines[j].Replace(" ", "").Replace("\t", "");
			}
		}
	}
}