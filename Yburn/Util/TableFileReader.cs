using System;
using System.Collections.Generic;
using System.IO;

namespace Yburn.Util
{
	/***********************************************************************************************
	 * Instances of the class TableFileReader read out a file containing a table of numerical data.
	 * The data is returned in a two dimensional jagged double array. The first array index labels
	 * the column and the second the line number of the original table.
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