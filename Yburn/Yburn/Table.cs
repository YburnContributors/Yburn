using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yburn.FormatUtil;

namespace Yburn
{
	public class Table<T> where T : IConvertible
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static Table<T> FillWithFunctionValues(
			T defaultElement,
			List<T> verticalAbscissa,
			List<T> horizontalAbscissa,
			Func<T, T, T> function
			)
		{
			Table<T> table = new Table<T>(
				defaultElement,
				verticalAbscissa.Count + 1,
				horizontalAbscissa.Count + 1);

			for(int v = 0; v < verticalAbscissa.Count; v++)
			{
				table[v + 1, 0] = verticalAbscissa[v];
				for(int h = 0; h < horizontalAbscissa.Count; h++)
				{
					table[0, h + 1] = horizontalAbscissa[h];
					table[v + 1, h + 1] = function(verticalAbscissa[v], horizontalAbscissa[h]);
				}
			}

			return table;
		}

		public static Table<string> CreateFromFormattedTableString(
			string formattedTableString
			)
		{
			Table<string> table = new Table<string>();

			string[] rows = formattedTableString.Split(
				new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			string columnSeparator = new string(' ', BlanksBetweenColumns);

			foreach(string row in rows)
			{
				table.AddRow(row.Split(
					new string[] { columnSeparator }, StringSplitOptions.RemoveEmptyEntries),
					string.Empty);
			}

			return table;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly int BlanksBetweenColumns = 2;

		private static string Concatenate(
			Table<string> table,
			bool alignFirstColumnLeft
			)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<string> formatStrings = GetColumnFormatStrings(table, alignFirstColumnLeft);

			for(int row = 0; row < table.NumberOfRows; row++)
			{
				for(int col = 0; col < table.NumberOfColumns; col++)
				{
					stringBuilder.AppendFormat(formatStrings[col], table[row, col]);
				}
				stringBuilder.AppendLine();
			}

			return stringBuilder.ToString();
		}

		private static List<string> GetColumnFormatStrings(
			Table<string> table,
			bool alignFirstColumnLeft
			)
		{
			List<int> columnWidths = GetColumnWidths(table);
			List<string> formatStrings = new List<string>();

			foreach(int width in columnWidths)
			{
				formatStrings.Add(string.Format("{{0,{0}}}", width + BlanksBetweenColumns));
			}

			if(alignFirstColumnLeft)
			{
				formatStrings[0] = string.Format("{{0,-{0}}}", columnWidths[0]);
			}
			else
			{
				formatStrings[0] = string.Format("{{0,{0}}}", columnWidths[0]);
			}

			return formatStrings;
		}

		private static List<int> GetColumnWidths(
			Table<string> table
			)
		{
			List<int> columnWidths = new List<int>();

			foreach(List<string> column in table.TableColumns)
			{
				columnWidths.Add(column.Max(element => element.Length));
			}

			return columnWidths;
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Table()
		{
		}

		public Table(
			T defaultElement,
			int numberOfRows,
			int numberOfColumns
			) : this()
		{
			for(int i = 0; i < numberOfColumns; i++)
			{
				TableColumns.Add(Enumerable.Repeat(defaultElement, numberOfRows).ToList());
			}
		}

		public Table(
			T[] column
			) : this()
		{
			AddColumn(column, default(T));
		}

		public Table(
			T[,] elements
			) : this(default(T), elements.GetLength(0), elements.GetLength(1))
		{
			for(int row = 0; row < NumberOfRows; row++)
			{
				for(int col = 0; col < NumberOfColumns; col++)
				{
					this[row, col] = elements[row, col];
				}
			}
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public T this[int row, int col]
		{
			get
			{
				return TableColumns[col][row];
			}
			set
			{
				TableColumns[col][row] = value;
			}
		}

		public int NumberOfColumns
		{
			get
			{
				return TableColumns.Count;
			}
		}

		public int NumberOfRows
		{
			get
			{
				if(NumberOfColumns == 0)
				{
					return 0;
				}
				else
				{
					return TableColumns.Max(column => column.Count);
				}
			}
		}

		public void AddColumn(
			IEnumerable<T> columnEntries,
			T defaultIfMissing
			)
		{
			List<T> column = columnEntries.ToList();
			AdjustNumberOfRows(column, defaultIfMissing);
			TableColumns.Add(column);
		}

		public void AddRow(
			IEnumerable<T> rowEntries,
			T defaultIfMissing
			)
		{
			List<T> row = rowEntries.ToList();
			AdjustNumberOfColumns(row, defaultIfMissing);
			for(int i = 0; i < row.Count; i++)
			{
				TableColumns[i].Add(row[i]);
			}
		}

		public void InsertColumn(
			int index,
			IEnumerable<T> columnEntries,
			T defaultIfMissing
			)
		{
			List<T> column = columnEntries.ToList();
			AdjustNumberOfRows(column, defaultIfMissing);
			TableColumns.Insert(index, column);
		}

		public void InsertRow(
			int index,
			IEnumerable<T> rowEntries,
			T defaultIfMissing
			)
		{
			List<T> row = rowEntries.ToList();
			AdjustNumberOfColumns(row, defaultIfMissing);
			for(int i = 0; i < row.Count; i++)
			{
				TableColumns[i].Insert(index, row[i]);
			}
		}

		public List<T> GetColumn(
			int index
			)
		{
			return new List<T>(TableColumns[index]);
		}

		public List<T> GetRow(
			int index
			)
		{
			List<T> row = new List<T>();
			foreach(List<T> column in TableColumns)
			{
				row.Add(column[index]);
			}

			return row;
		}

		public void Transpose()
		{
			List<List<T>> transposedTableColumns = new List<List<T>>();

			for(int i = 0; i < NumberOfRows; i++)
			{
				transposedTableColumns.Add(GetRow(i));
			}

			TableColumns = transposedTableColumns;
		}

		public string ToFormattedTableString(
			bool alignFirstColumnLeft = false
			)
		{
			return Concatenate(ToStringTable(), alignFirstColumnLeft);
		}

		public string ToFormattedTableString(
			IEnumerable<string> columnLabels,
			bool alignFirstColumnLeft = false
			)
		{
			Table<string> stringTable = ToStringTable();
			stringTable.InsertRow(0, columnLabels, string.Empty);

			return Concatenate(stringTable, alignFirstColumnLeft);
		}

		public string ToFormattedTableString(
			IEnumerable<string> columnLabels,
			IEnumerable<string> rowLabels,
			string title = "",
			bool alignFirstColumnLeft = false
			)
		{
			Table<string> stringTable = ToStringTable();
			stringTable.InsertRow(0, columnLabels, string.Empty);
			stringTable.InsertColumn(0, new List<string>() { title }.Concat(rowLabels), string.Empty);

			return Concatenate(stringTable, alignFirstColumnLeft);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private List<List<T>> TableColumns = new List<List<T>>();

		private void AdjustNumberOfColumns(
			List<T> rowToBeAdded,
			T defaultIfMissing
			)
		{
			for(int i = TableColumns.Count; i < rowToBeAdded.Count; i++)
			{
				TableColumns.Add(Enumerable.Repeat(defaultIfMissing, NumberOfRows).ToList());
			}
			for(int i = rowToBeAdded.Count; i < NumberOfColumns; i++)
			{
				rowToBeAdded.Add(defaultIfMissing);
			}
		}

		private void AdjustNumberOfRows(
			List<T> columnToBeAdded,
			T defaultIfMissing
			)
		{
			foreach(List<T> existingColumn in TableColumns)
			{
				for(int i = existingColumn.Count; i < columnToBeAdded.Count; i++)
				{
					existingColumn.Add(defaultIfMissing);
				}
			}
			for(int i = columnToBeAdded.Count; i < NumberOfRows; i++)
			{
				columnToBeAdded.Add(defaultIfMissing);
			}
		}

		private Table<string> ToStringTable()
		{
			Table<string> stringTable = new Table<string>();

			foreach(List<T> column in TableColumns)
			{
				stringTable.TableColumns.Add(column.ConvertAll(element => element.ToUIString()));
			}

			return stringTable;
		}
	}
}
