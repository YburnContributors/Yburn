/**************************************************************************************************
 * Abstract base for Yburn's file reader classes.
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace Yburn
{
	public abstract class FileReader
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected FileReader()
		{
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

	/***********************************************************************************************
	* Instances of the class ParaFileReader may be used to read out a parameter file and save the
	* names and values of variables assigned by a "=" in pairs of two strings. Commentary may be
	* introced by a "#". Multiple values may be given by inserting a comma. I.e. the text
	*
	* #some commentary
	* Var1  =   0.5   Var2=1
	*
	* Var3=  -3e5   # more commentary
	* Var4 =4,   5.0  6
	*
	* yields the pairs ("Var1", "0.5"), ("Var2", "1"), ("Var3", "-3e5"), ("Var4", "4, 5.0").
	************************************************************************************************/

	public class ParaFileReader : FileReader
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void Read(
			string pathFile,
			out Dictionary<string, string> nameValuePairs
			)
		{
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));
			Read(allLines, out nameValuePairs);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected ParaFileReader()
			: base()
		{
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void Read(
			List<string> allLines,
			out Dictionary<string, string> nameValuePairs
			)
		{
			ParaFileReader reader = new ParaFileReader();

			reader.RemoveLinesWithoutEqualSign(allLines);
			reader.RemoveCommentaryLines(allLines);
			reader.RemoveCommentaryWithinLines(allLines);
			reader.RemoveTabsAndSpaces(allLines);
			reader.ExtractParameterNamesAndValues(allLines, out nameValuePairs);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void ExtractParameterNamesAndValues(
			List<string> allLines,
			out Dictionary<string, string> nameValuePairs
			)
		{
			nameValuePairs = new Dictionary<string, string>();
			foreach(string line in allLines)
			{
				string[] nameValuePair = line.Split(new char[] { '=' }, 2);
				nameValuePairs.Add(nameValuePair[0], nameValuePair[1]);
			}
		}
	}

	/***********************************************************************************************
	 * Instances of the class BatchFileReader read out a parameter file like ParaFileReader.
	 * Additionally one may include job-statements of the kind "Job = [JobTitle]", which may be used
	 * to launch processes. The data is returned in a list of dictionaries. Each dictionary contains
	 * name-value pairs of variables and a job-statment as the last entry.
	 ***********************************************************************************************/

	public class BatchFileReader : FileReader
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void Read(
			string pathFile,
			out List<Dictionary<string, string>> commandList
			)
		{
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));
			Read(allLines, out commandList);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected BatchFileReader()
			: base()
		{
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void Read(
			List<string> allLines,
			out List<Dictionary<string, string>> commandList
			)
		{
			BatchFileReader reader = new BatchFileReader();

			reader.RemoveLinesWithoutEqualSign(allLines);
			reader.RemoveCommentaryLines(allLines);
			reader.RemoveCommentaryWithinLines(allLines);
			reader.RemoveTabsAndSpaces(allLines);
			reader.ExtractCommands(allLines, out commandList);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void ExtractCommands(
			List<string> allLines,
			out List<Dictionary<string, string>> commandList
			)
		{
			commandList = new List<Dictionary<string, string>>();

			Dictionary<string, string> dummyNameValueDict = new Dictionary<string, string>();
			foreach(string line in allLines)
			{
				string[] nameValuePair = line.Split(new char[] { '=' }, 2);
				dummyNameValueDict.Add(nameValuePair[0], nameValuePair[1]);

				if(nameValuePair[0] == "Job")
				{
					commandList.Add(dummyNameValueDict);
					dummyNameValueDict = new Dictionary<string, string>();
				}
			}
		}
	}

	/***********************************************************************************************
	 * Instances of the class TableFileReader read out a file containing a table of numerical data.
	 * The data is returned in a two dimensional jagged double array. The first array index
     * labels the column and the second the line number of the original table.
	 ***********************************************************************************************/

	public class TableFileReader : FileReader
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void Read(
			string pathFile,
			out double[][] columnSortedTable
			)
		{
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));
			Read(allLines, out columnSortedTable);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected TableFileReader()
			: base()
		{
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void Read(
			List<string> allLines,
			out double[][] columnSortedTable
			)
		{
			TableFileReader reader = new TableFileReader();
			double[][] lineSortedTable;

			reader.RemoveCommentaryLines(allLines);
			reader.RemoveCommentaryWithinLines(allLines);
			reader.RemoveEmptyLines(allLines);
			reader.ExtractValues(allLines, out lineSortedTable);
			reader.SwitchColumnAndLineIndex(lineSortedTable, out columnSortedTable);
		}

		private static void AssertRectangularShape(
			double[][] lineSortedTable
			)
		{
			for(int lineIndex = 1; lineIndex < lineSortedTable.Length; lineIndex++)
			{
				if(lineSortedTable[lineIndex].Length != lineSortedTable[0].Length)
				{
					throw new Exception(
						"Anomalous number of entries in line " + (lineIndex + 1).ToString() + ".");
				}
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void RemoveEmptyLines(
			List<string> allLines
			)
		{
			for(int j = allLines.Count - 1; j >= 0; j--)
			{
				if(string.IsNullOrWhiteSpace(allLines[j]))
				{
					allLines.RemoveAt(j);
				}
			}
		}

		private void ExtractValues(
			List<string> allLines,
			out double[][] lineSortedTable
			)
		{
			lineSortedTable = new double[allLines.Count][];

			for(int lineIndex = allLines.Count - 1; lineIndex >= 0; lineIndex--)
			{
				string[] values = allLines[lineIndex].Split(new char[] { ' ', '\t' },
					StringSplitOptions.RemoveEmptyEntries);

				double[] line = new double[values.Length];
				for(int columnIndex = 0; columnIndex < values.Length; columnIndex++)
				{
					line[columnIndex] = double.Parse(values[columnIndex]);
				}

				lineSortedTable[lineIndex] = line;
			}
		}

		private void SwitchColumnAndLineIndex(
			double[][] lineSortedTable,
			out double[][] columnSortedTable
			)
		{
			AssertRectangularShape(lineSortedTable);
			int numberOfLines = lineSortedTable.Length;
			int numberOfColumns = lineSortedTable[0].Length;

			columnSortedTable = new double[numberOfColumns][];
			for(int columnIndex = 0; columnIndex < numberOfColumns; columnIndex++)
			{
				double[] column = new double[numberOfLines];
				for(int lineIndex = 0; lineIndex < numberOfLines; lineIndex++)
				{
					column[lineIndex] = lineSortedTable[lineIndex][columnIndex];
				}
				columnSortedTable[columnIndex] = column;
			}
		}
	}
}