using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Yburn
{
	partial class Worker
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		protected static void WriteLine(
			List<List<double>> dataList,
			StringBuilder plotFile,
			int index
			)
		{
			foreach(List<double> list in dataList)
			{
				plotFile.AppendFormat("{0,-25}",
					list[index].ToString());
			}
			plotFile.AppendLine();
		}

		protected static void AddPlotFunctionLists(
			List<List<double>> dataList,
			List<double> abscissae,
			PlotFunction function
			)
		{
			List<double> fieldValues =
				GetPlotFunctionValueList(abscissae, function);
			dataList.Add(fieldValues);
		}

		protected static List<double> GetPlotFunctionValueList(
			List<double> abscissae,
			PlotFunction function
			)
		{
			List<double> functionValues = new List<double>();

			foreach(double value in abscissae)
			{
				functionValues.Add(function(value));
			}

			return functionValues;
		}

		protected static List<double> GetLinearAbscissaList(
			double startValue,
			double stopValue,
			int samples
			)
		{
			List<double> abscissaList = new List<double>();

			double step = (stopValue - startValue) / samples;
			for(int i = 1; i <= samples; i++)
			{
				abscissaList.Add(startValue + step * i);
			}

			return abscissaList;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected delegate List<List<double>> DataListCreator();

		protected delegate double PlotFunction(double x);

		protected string DataPathFile
		{
			get
			{
				return YburnConfigFile.OutputPath + DataFileName;
			}
		}

		protected string FormattedDataPathFile
		{
			get
			{
				return DataPathFile.Replace("\\", "/");
			}
		}

		protected string FormattedPlotPathFile
		{
			get
			{
				return FormattedDataPathFile + ".plt";
			}
		}

		protected void WritePlotFile(
			StringBuilder plotFile
			)
		{
			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		protected void CreateDataFile(
			DataListCreator dataListCreator
			)
		{
			List<List<double>> dataList = dataListCreator();

			StringBuilder plotFile = new StringBuilder();
			for(int i = 0; i < dataList[0].Count; i++)
			{
				WriteLine(dataList, plotFile, i);
			}

			File.WriteAllText(DataPathFile, plotFile.ToString());
		}

		protected void AppendPlotCommands(
			StringBuilder plotFile,
			string[] titleList,
			string plotStyle = "lines"
			)
		{
			plotFile.AppendLine("plot\\");

			int plotColumn = 2;
			foreach(string title in titleList)
			{
				plotFile.AppendFormat("\"{0}\" using 1:{1} with {2} title \"{3}\",\\",
					FormattedDataPathFile, plotColumn, plotStyle, title);
				plotFile.AppendLine();
				plotColumn++;
			}
		}

		protected void AppendPlotCommands(
			StringBuilder plotFile,
			string[][] titleList,
			string plotStyle = "lines"
			)
		{
			plotFile.AppendLine("plot\\");

			for(int index = 0; index < titleList.Length; index++)
			{
				int plotColumn = 2;
				foreach(string title in titleList[index])
				{
					plotFile.AppendFormat("\"{0}\" index {1} using 1:{2} with {3} title \"{4}\",\\",
						FormattedDataPathFile, index, plotColumn, plotStyle, title);
					plotFile.AppendLine();
					plotColumn++;
				}
			}
		}

		protected Process StartGnuplot()
		{
			return Process.Start("wgnuplot", "\"" + FormattedPlotPathFile + "\" --persist");
		}
	}
}