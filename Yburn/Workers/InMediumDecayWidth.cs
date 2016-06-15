using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Yburn.Fireball;
using Yburn.QQState;
using Yburn.Util;

namespace Yburn.Workers
{
	public partial class InMediumDecayWidth : Worker
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public InMediumDecayWidth()
			: base()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void CalculateInMediumDecayWidth()
		{
			PrepareJob("CalculateInMediumDecayWidth");

			string temperatureDecayWidthList = GetTemperatureDecayWidthList();

			LogMessages.AppendFormat("#\r\n#\r\n");
			LogMessages.AppendFormat(temperatureDecayWidthList);

			File.WriteAllText(YburnConfigFile.OutputPath + Outfile,
				LogHeader + "#\r\n#\r\n" + temperatureDecayWidthList);
		}

		public Process PlotInMediumDecayWidth()
		{
			AssertInputValid_PlotInMediumDecayWidth();

			StringBuilder plotFile = new StringBuilder();
			AppendHeader_InMediumDecayWidth(plotFile);
			plotFile.AppendLine();
			plotFile.Append("plot");
			AppendPlotCommands_AveragedDecayWidth(plotFile);
			AppendPlotCommands_AveragedTemperature(plotFile);

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected override Type GetEnumTypeByName(
			string enumName
			)
		{
			if(enumName == "BottomiumState")
			{
				return typeof(BottomiumState);
			}
			else if(enumName == "DecayWidthType")
			{
				return typeof(DecayWidthType);
			}
			else if(enumName == "PotentialType")
			{
				return typeof(PotentialType);
			}
			else
			{
				throw new Exception("Invalid enum name \"" + enumName + "\".");
			}
		}

		private string DataPathFile
		{
			get
			{
				return YburnConfigFile.OutputPath + Outfile;
			}
		}

		private string FormattedDataPathFile
		{
			get
			{
				return DataPathFile.Replace("\\", "/");
			}
		}

		private string FormattedPlotPathFile
		{
			get
			{
				return FormattedDataPathFile + ".plt";
			}
		}

		private void WritePlotFile(
			StringBuilder plotFile
			)
		{
			File.WriteAllText(FormattedPlotPathFile, plotFile.ToString());
		}

		private Process StartGnuplot()
		{
			return Process.Start("wgnuplot", "\"" + FormattedPlotPathFile + "\" --persist");
		}

		private string GetTemperatureDecayWidthList()
		{
			BottomiumState[] bottomiumStates = BottomiumStates;

			TemperatureDecayWidthPrinter printer = new TemperatureDecayWidthPrinter(
				YburnConfigFile.QQDataPathFile, bottomiumStates, DecayWidthType, PotentialTypes,
				MinTemperature, MaxTemperature, TemperatureStepSize, MediumVelocity,
				DecayWidthAveragingAngles);

			return UseAveragedTemperature ?
				printer.GetListUsingAveragedTemperature() : printer.GetList();
		}

		protected override void StartJob(
			string jobId
			)
		{
			switch(jobId)
			{
				case "CalculateInMediumDecayWidth":
					CalculateInMediumDecayWidth();
					break;

				case "PlotInMediumDecayWidth":
					PlotInMediumDecayWidth();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}

		private void AssertInputValid_PlotInMediumDecayWidth()
		{
			if(BottomiumStates.Length == 0)
			{
				throw new Exception("No bottomium states given.");
			}
		}

		private void AppendHeader_InMediumDecayWidth(
			StringBuilder plotFile
			)
		{
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,800");
			plotFile.AppendLine();
			plotFile.AppendLine("set key top left");
			plotFile.AppendLine("set key spacing 1.1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"" + InMediumDecayWidthPlottingTitle + "\"");
			plotFile.AppendLine("set xlabel \"T (MeV)\"");
			plotFile.AppendLine("set ylabel \"{/Symbol G}_{nl} (MeV)\"");
		}

		private string InMediumDecayWidthPlottingTitle
		{
			get
			{
				return "In medium decay widths {/Symnbol G}_{nl}, averaged over angles "
					+ DecayWidthAveragingAngles.ToUIString();
			}
		}

		private void AppendPlotCommands_AveragedDecayWidth(
			StringBuilder plotFile
			)
		{
			BottomiumState[] bottomiumStates = BottomiumStates;
			int colNumber = 2;
			foreach(BottomiumState state in bottomiumStates)
			{
				plotFile.AppendLine("	\"" + FormattedDataPathFile
					+ "\" index 0 using 1:" + colNumber + " with linespoints title \""
					+ GetBottomiumStateGnuplotCode(state) + ", {/Symbol G}_{averaged}(T)\",	\\");
				colNumber++;
			}
		}

		private void AppendPlotCommands_AveragedTemperature(
			StringBuilder plotFile
			)
		{
			BottomiumState[] bottomiumStates = BottomiumStates;
			int colNumber = 2;
			foreach(BottomiumState state in bottomiumStates)
			{
				plotFile.AppendLine("	\"" + FormattedDataPathFile
					+ "\" index 1 using 1:" + colNumber + " with linespoints title \""
				+ GetBottomiumStateGnuplotCode(state) + ", {/Symbol G}(T_{averaged})\",	\\");
				colNumber++;
			}
		}

		private static string GetBottomiumStateGnuplotCode(
			BottomiumState state
			)
		{
			switch(state)
			{
				case BottomiumState.Y1S:
					return "{/Symbol U}(1S)";

				case BottomiumState.x1P:
					return "{/Symbol c}(1P)";

				case BottomiumState.Y2S:
					return "{/Symbol U}(2S)";

				case BottomiumState.x2P:
					return "{/Symbol c}(2P)";

				case BottomiumState.Y3S:
					return "{/Symbol U}(3S)";

				case BottomiumState.x3P:
					return "{/Symbol c}(3P)";

				default:
					throw new Exception("Invalid BottomiumState.");
			}
		}
	}
}