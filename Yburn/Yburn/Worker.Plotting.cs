﻿using System.Collections.Generic;
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

		protected static List<double> GetLinearAbscissaList(
			double startValue,
			double stopValue,
			int samples,
			bool includeStopValue = true
			)
		{
			List<double> abscissaList = new List<double>();

			double step = (stopValue - startValue) / samples;
			for(int i = 0; i < samples; i++)
			{
				abscissaList.Add(startValue + step * i);
			}

			if(includeStopValue)
			{
				abscissaList.Add(stopValue);
			}

			return abscissaList;
		}

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
			List<double> fieldValues = GetPlotFunctionValueList(abscissae, function);
			dataList.Add(fieldValues);
		}

		protected static void AddSurfacePlotFunctionLists(
			List<List<double>> dataList,
			List<double> abscissaeX,
			List<double> abscissaeY,
			SurfacePlotFunction function
			)
		{
			foreach(double valueX in abscissaeX)
			{
				PlotFunction functionY = valueY => function(valueX, valueY);

				List<double> fieldValues = GetPlotFunctionValueList(abscissaeY, functionY);
				fieldValues.Insert(0, valueX);
				dataList.Add(fieldValues);
			}

			dataList.Insert(0, abscissaeY);
			dataList[0].Insert(0, double.NaN);
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

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected delegate List<List<double>> DataListCreator();

		protected delegate double PlotFunction(double x);

		protected delegate double SurfacePlotFunction(double x, double y);

		private string OutputPath
		{
			get
			{
				return YburnConfigFile.OutputPath;
			}
		}

		private string PlotFileName
		{
			get
			{
				return DataFileName + ".plt";
			}
		}

		protected void WriteDataFile(
			StringBuilder dataFileContent
			)
		{
			WriteFile(dataFileContent, DataFileName);
		}

		protected void WritePlotFile(
			StringBuilder plotFileContent
			)
		{
			WriteFile(plotFileContent, PlotFileName);
		}

		protected void WriteFile(
			StringBuilder fileContent,
			string fileName
			)
		{
			File.WriteAllText(OutputPath + fileName, fileContent.ToString());
		}

		protected void CreateDataFile(
			params DataListCreator[] dataListCreators
			)
		{
			StringBuilder dataFileContent = GetDataFileContent(dataListCreators[0]);
			for(int i = 1; i < dataListCreators.Length; i++)
			{
				dataFileContent.AppendLine();
				dataFileContent.AppendLine();
				dataFileContent.Append(GetDataFileContent(dataListCreators[i]));
			}

			WriteDataFile(dataFileContent);
		}

		private StringBuilder GetDataFileContent(
			DataListCreator dataListCreator
			)
		{
			List<List<double>> dataList = dataListCreator();

			StringBuilder dataFileContent = new StringBuilder();
			for(int i = 0; i < dataList[0].Count; i++)
			{
				WriteLine(dataList, dataFileContent, i);
			}

			return dataFileContent;
		}

		protected void AppendPlotCommands(
			StringBuilder plotFile,
			bool isFirstPlotCommand = true,
			int index = 0,
			int abscissaColumn = 1,
			int firstOrdinateColumn = 2,
			string style = "linespoints",
			params string[] titles
			)
		{
			if(isFirstPlotCommand)
			{
				plotFile.AppendLine("plot\\");
			}

			int ordinateColumn = firstOrdinateColumn;
			foreach(string title in titles)
			{
				AppendPlotDataCommand(plotFile, index, abscissaColumn, ordinateColumn, style, title);
				ordinateColumn++;
			}
		}

		protected void AppendPlotCommands(
			StringBuilder plotFile,
			int abscissaColumn = 1,
			int firstOrdinateColumn = 2,
			string style = "linespoints",
			bool appendToExistingPlotCommands = false,
			params string[][] titles
			)
		{
			if(appendToExistingPlotCommands == false)
			{
				plotFile.AppendLine("plot\\");
			}

			for(int index = 0; index < titles.Length; index++)
			{
				int ordinateColumn = firstOrdinateColumn;
				foreach(string title in titles[index])
				{
					AppendPlotDataCommand(
						plotFile, index, abscissaColumn, ordinateColumn, style, title);
					ordinateColumn++;
				}
			}
		}

		private void AppendPlotDataCommand(
			StringBuilder plotFile,
			int index,
			int abscissaColumn,
			int ordinateColumn,
			string style,
			string title
			)
		{
			plotFile.AppendFormat("'{0}' index {1} using {2}:{3} with {4} title '{5}',\\",
				DataFileName, index, abscissaColumn, ordinateColumn, style, title);
			plotFile.AppendLine();
		}

		protected void AppendSurfacePlotCommands(
			StringBuilder plotFile
			)
		{
			plotFile.AppendLine("set size 0.9,1");
			plotFile.AppendLine("set cblabel offset 2");
			plotFile.AppendLine();
			plotFile.AppendLine("set pm3d map");
			plotFile.AppendLine("set pm3d interpolate 0,0");
			plotFile.AppendLine();
			plotFile.AppendLine("set contour");
			plotFile.AppendLine("set cntrparam bspline");
			plotFile.AppendLine("set cntrlabel interval -1 font ',7'");
			plotFile.AppendLine("do for [i=1:8] { set linetype i linecolor rgb 'black' }");
			plotFile.AppendLine();

			plotFile.AppendFormat("splot '{0}' nonuniform matrix using 1:2:3 notitle dashtype '-'"
				+ ", '' nonuniform matrix using 1:2:3 notitle with labels nosurface",
				DataFileName);
			plotFile.AppendLine();
		}

		protected void AppendSavePlotAsPNG(
			StringBuilder plotFile
			)
		{
			plotFile.AppendLine();
			plotFile.AppendLine("set terminal pngcairo enhanced size 1000,500");
			plotFile.AppendLine("set output '" + DataFileName + ".png'");
			plotFile.AppendLine("replot");
			plotFile.AppendLine("set output");
		}

		protected Process StartGnuplot()
		{
			return StartGnuplot(PlotFileName);
		}

		protected Process StartGnuplot(
			string plotFileName
			)
		{
			ProcessStartInfo gnuplot = new ProcessStartInfo();
			gnuplot.FileName = "wgnuplot";
			gnuplot.Arguments = "\"" + plotFileName + "\" --persist";
			gnuplot.WorkingDirectory = OutputPath;

			return Process.Start(gnuplot);
		}
	}
}