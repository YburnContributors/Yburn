using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Yburn.Tests
{
	[TestClass]
	public class TableTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CreateEmptyTable()
		{
			Table<double> table = new Table<double>();

			Assert.AreEqual(0, table.NumberOfRows);
			Assert.AreEqual(0, table.NumberOfColumns);
		}

		[TestMethod]
		public void CreateTableOfGivenSize()
		{
			Table<double> table = new Table<double>(double.NaN, 2, 3);

			Assert.AreEqual(2, table.NumberOfRows);
			Assert.AreEqual(3, table.NumberOfColumns);
		}

		[TestMethod]
		public void CreateTableFrom1DArray()
		{
			Table<double> table = new Table<double>(new double[] { 1, 2, 3 });

			Assert.AreEqual(1, table.NumberOfColumns);
			Assert.AreEqual(3, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[1, 0]);
			Assert.AreEqual(3, table[2, 0]);
		}

		[TestMethod]
		public void CreateTableFrom2DArray()
		{
			Table<double> table = new Table<double>(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });

			Assert.AreEqual(2, table.NumberOfColumns);
			Assert.AreEqual(3, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[0, 1]);
			Assert.AreEqual(3, table[1, 0]);
			Assert.AreEqual(4, table[1, 1]);
			Assert.AreEqual(5, table[2, 0]);
			Assert.AreEqual(6, table[2, 1]);
		}

		[TestMethod]
		public void AddColumnToEmptyTable()
		{
			Table<double> table = new Table<double>();

			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);

			Assert.AreEqual(3, table.NumberOfRows);
			Assert.AreEqual(1, table.NumberOfColumns);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[1, 0]);
			Assert.AreEqual(3, table[2, 0]);
		}

		[TestMethod]
		public void AddShorterColumnToTable()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5 }, double.NaN);

			Assert.AreEqual(2, table.NumberOfColumns);
			Assert.AreEqual(3, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[1, 0]);
			Assert.AreEqual(3, table[2, 0]);
			Assert.AreEqual(4, table[0, 1]);
			Assert.AreEqual(5, table[1, 1]);
			Assert.AreEqual(double.NaN, table[2, 1]);
		}

		[TestMethod]
		public void AddLongerColumnToTable()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2 }, double.NaN);
			table.AddColumn(new double[] { 4, 5, 6 }, double.NaN);

			Assert.AreEqual(2, table.NumberOfColumns);
			Assert.AreEqual(3, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[1, 0]);
			Assert.AreEqual(double.NaN, table[2, 0]);
			Assert.AreEqual(4, table[0, 1]);
			Assert.AreEqual(5, table[1, 1]);
			Assert.AreEqual(6, table[2, 1]);
		}

		[TestMethod]
		public void InsertColumnIntoTable()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5 }, double.NaN);

			table.InsertColumn(1, new double[] { 6, 7 }, double.NaN);

			Assert.AreEqual(3, table.NumberOfColumns);
			Assert.AreEqual(3, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[1, 0]);
			Assert.AreEqual(3, table[2, 0]);
			Assert.AreEqual(6, table[0, 1]);
			Assert.AreEqual(7, table[1, 1]);
			Assert.AreEqual(double.NaN, table[2, 1]);
			Assert.AreEqual(4, table[0, 2]);
			Assert.AreEqual(5, table[1, 2]);
			Assert.AreEqual(double.NaN, table[2, 2]);
		}

		[TestMethod]
		public void AddRowToEmptyTable()
		{
			Table<double> table = new Table<double>();

			table.AddRow(new double[] { 1, 2, 3 }, double.NaN);

			Assert.AreEqual(1, table.NumberOfRows);
			Assert.AreEqual(3, table.NumberOfColumns);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[0, 1]);
			Assert.AreEqual(3, table[0, 2]);
		}

		[TestMethod]
		public void AddShorterRowToTable()
		{
			Table<double> table = new Table<double>();
			table.AddRow(new double[] { 1, 2, 3 }, double.NaN);
			table.AddRow(new double[] { 4, 5 }, double.NaN);

			Assert.AreEqual(3, table.NumberOfColumns);
			Assert.AreEqual(2, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[0, 1]);
			Assert.AreEqual(3, table[0, 2]);
			Assert.AreEqual(4, table[1, 0]);
			Assert.AreEqual(5, table[1, 1]);
			Assert.AreEqual(double.NaN, table[1, 2]);
		}

		[TestMethod]
		public void AddLongerRowToTable()
		{
			Table<double> table = new Table<double>();
			table.AddRow(new double[] { 1, 2 }, double.NaN);
			table.AddRow(new double[] { 4, 5, 6 }, double.NaN);

			Assert.AreEqual(3, table.NumberOfColumns);
			Assert.AreEqual(2, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[0, 1]);
			Assert.AreEqual(double.NaN, table[0, 2]);
			Assert.AreEqual(4, table[1, 0]);
			Assert.AreEqual(5, table[1, 1]);
			Assert.AreEqual(6, table[1, 2]);
		}

		[TestMethod]
		public void InsertRowIntoTable()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5 }, double.NaN);

			table.InsertRow(1, new double[] { 6, 7 }, double.NaN);

			Assert.AreEqual(2, table.NumberOfColumns);
			Assert.AreEqual(4, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(6, table[1, 0]);
			Assert.AreEqual(2, table[2, 0]);
			Assert.AreEqual(3, table[3, 0]);
			Assert.AreEqual(4, table[0, 1]);
			Assert.AreEqual(7, table[1, 1]);
			Assert.AreEqual(5, table[2, 1]);
			Assert.AreEqual(double.NaN, table[3, 1]);
		}

		[TestMethod]
		public void FillTableWithFunctionValues()
		{
			List<double> abscissaX = new List<double>() { 2, 10 };
			List<double> abscissaY = new List<double>() { -5, 0, 5 };
			Func<double, double, double> function = (x, y) => y / x;

			Table<double> table
				= Table<double>.FillWithFunctionValues(double.NaN, abscissaX, abscissaY, function);

			Assert.AreEqual(abscissaX.Count + 1, table.NumberOfRows);
			Assert.AreEqual(abscissaY.Count + 1, table.NumberOfColumns);

			Assert.AreEqual(-5, table[0, 1]);
			Assert.AreEqual(0, table[0, 2]);
			Assert.AreEqual(5, table[0, 3]);

			Assert.AreEqual(2, table[1, 0]);
			Assert.AreEqual(10, table[2, 0]);

			Assert.AreEqual(-2.5, table[1, 1]);
			Assert.AreEqual(0, table[1, 2]);
			Assert.AreEqual(2.5, table[1, 3]);

			Assert.AreEqual(-0.5, table[2, 1]);
			Assert.AreEqual(0, table[2, 2]);
			Assert.AreEqual(0.5, table[2, 3]);
		}

		[TestMethod]
		public void GetTableColumn()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5, 6 }, double.NaN);

			List<double> column0 = table.GetColumn(0);
			Assert.AreEqual(1, column0[0]);
			Assert.AreEqual(2, column0[1]);
			Assert.AreEqual(3, column0[2]);

			List<double> column1 = table.GetColumn(1);
			Assert.AreEqual(4, column1[0]);
			Assert.AreEqual(5, column1[1]);
			Assert.AreEqual(6, column1[2]);
		}

		[TestMethod]
		public void GetTableRow()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5, 6 }, double.NaN);

			List<double> row0 = table.GetRow(0);
			Assert.AreEqual(1, row0[0]);
			Assert.AreEqual(4, row0[1]);

			List<double> row1 = table.GetRow(1);
			Assert.AreEqual(2, row1[0]);
			Assert.AreEqual(5, row1[1]);

			List<double> row2 = table.GetRow(2);
			Assert.AreEqual(3, row2[0]);
			Assert.AreEqual(6, row2[1]);
		}

		[TestMethod]
		public void TransposeTable()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5 }, double.NaN);
			table.Transpose();

			Assert.AreEqual(3, table.NumberOfColumns);
			Assert.AreEqual(2, table.NumberOfRows);

			Assert.AreEqual(1, table[0, 0]);
			Assert.AreEqual(2, table[0, 1]);
			Assert.AreEqual(3, table[0, 2]);
			Assert.AreEqual(4, table[1, 0]);
			Assert.AreEqual(5, table[1, 1]);
			Assert.AreEqual(double.NaN, table[1, 2]);
		}

		[TestMethod]
		public void GetFormattedTableString()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5 }, double.NaN);

			string tableString = table.ToFormattedTableString();
			string expectedString = "1    4\r\n2    5\r\n3  NaN\r\n";

			Assert.AreEqual(expectedString, tableString);
		}

		[TestMethod]
		public void GetFormattedTableStringWithColumnLabels()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5 }, double.NaN);

			string tableString = table.ToFormattedTableString(new List<string> { "Col1", "Col2" });
			string expectedString = "Col1  Col2\r\n   1     4\r\n   2     5\r\n   3   NaN\r\n";

			Assert.AreEqual(expectedString, tableString);
		}

		[TestMethod]
		public void GetFormattedTableStringWithColumnAndRowLabels()
		{
			Table<double> table = new Table<double>();
			table.AddColumn(new double[] { 1, 2, 3 }, double.NaN);
			table.AddColumn(new double[] { 4, 5 }, double.NaN);

			string tableString = table.ToFormattedTableString(
				new List<string> { "Col1", "Col2" },
				new List<string> { "Row1", "Row2", "Row3" });
			string expectedString = "      Col1  Col2\r\nRow1     1     4\r\nRow2     2     5\r\nRow3     3   NaN\r\n";

			Assert.AreEqual(expectedString, tableString);
		}
	}
}
