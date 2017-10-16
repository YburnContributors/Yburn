using System;
using System.Diagnostics;
using System.Text;
using Yburn.Fireball;

namespace Yburn.Workers
{
	partial class QQonFire
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static string[] SnapshotStatusTitles
		{
			get
			{
				return new string[] { "Time" };
			}
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public Process ShowSnapsX()
		{
			return StartGnuplot(DataFileName + "-plotX.plt");
		}

		public Process ShowSnapsY()
		{
			return StartGnuplot(DataFileName + "-plotY.plt");
		}

		public Process ShowSnapsXY()
		{
			return StartGnuplot(DataFileName + "-plotXY.plt");
		}

		public void MakeSnapshots()
		{
			PrepareJob("MakeSnapshots", SnapshotStatusTitles);

			if(SnapRate_per_fm <= 0)
			{
				throw new Exception("SnapRate <= 0.");
			}

			// All data is saved in the output file. Additionally, the corresponding gnuplot files (.plt)
			// are created to facilitate graphical visualization of the data.
			StringBuilder output = new StringBuilder();
			StringBuilder gnuFileStringX = new StringBuilder();
			StringBuilder gnuFileStringY = new StringBuilder();
			StringBuilder gnuFileStringXY = new StringBuilder();

			gnuFileStringX.AppendLine("reset");
			gnuFileStringX.AppendLine();
			gnuFileStringX.AppendLine(string.Format("stats '{0}' using 3", DataFileName));
			gnuFileStringX.AppendLine("set yrange [floor(STATS_min):ceil(STATS_max)]");
			gnuFileStringX.AppendLine();
			gnuFileStringX.AppendLine("set xlabel 'x (fm)'");
			gnuFileStringX.AppendLine();

			gnuFileStringY.AppendLine("reset");
			gnuFileStringY.AppendLine();
			gnuFileStringY.AppendLine(string.Format("stats '{0}' using 3", DataFileName));
			gnuFileStringY.AppendLine("set yrange [floor(STATS_min):ceil(STATS_max)]");
			gnuFileStringY.AppendLine();
			gnuFileStringY.AppendLine("set xlabel 'y (fm)'");
			gnuFileStringY.AppendLine();

			gnuFileStringXY.AppendLine("reset");
			gnuFileStringXY.AppendLine();
			gnuFileStringXY.AppendLine(string.Format("stats '{0}' using 3", DataFileName));
			gnuFileStringXY.AppendLine("set zrange [floor(STATS_min):ceil(STATS_max)]");
			gnuFileStringXY.AppendLine();
			gnuFileStringXY.AppendLine("set xlabel 'x (fm)'");
			gnuFileStringXY.AppendLine("set ylabel 'y (fm)'");
			gnuFileStringXY.AppendLine();

			CoordinateSystem system = CreateCoordinateSystem();
			Fireball.Fireball fireball = CreateFireball();
			BjorkenLifeTime_fm = fireball.BjorkenLifeTime;

			int index = 0;
			double dt = 1.0 / SnapRate_per_fm;
			do
			{
				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					break;
				}

				// get status of calculation
				StatusValues[0] = fireball.CurrentTime.ToString();

				output.AppendLine();
				output.AppendLine();
				output.AppendLine(string.Format(
					"#Time = {0}, Index {1}", fireball.CurrentTime, index));
				output.Append(fireball.FieldsToString(FireballFieldTypes, BottomiumStates));

				gnuFileStringX.AppendLine(string.Format(
					"plot '{0}' every {1} index {2} using 1:3 with points title 't = {3} fm/c'; pause .5",
					DataFileName, system.YAxis.Count, index, fireball.CurrentTime));
				gnuFileStringY.AppendLine(string.Format(
					"plot '{0}' every ::{1}::{2} index {3} using 2:3 with points title 't = {4} fm/c'; pause .5",
					DataFileName,
					system.FindClosestXAxisIndex(0) * system.YAxis.Count,
					(system.FindClosestXAxisIndex(0) + 1) * system.YAxis.Count - 1,
					index,
					fireball.CurrentTime));
				gnuFileStringXY.AppendLine(string.Format(
					"splot '{0}' index {1} using 1:2:3 with points title 't = {2} fm/c'; pause .5",
					DataFileName, index, fireball.CurrentTime));

				index++;
				fireball.Advance(dt);
			} while(fireball.MaximumTemperature > BreakupTemperature_MeV);

			LifeTime_fm = fireball.LifeTime;

			WriteOutputToLogAndDataFile(output);

			WriteFile(gnuFileStringX, DataFileName + "-plotX.plt");
			WriteFile(gnuFileStringY, DataFileName + "-plotY.plt");
			WriteFile(gnuFileStringXY, DataFileName + "-plotXY.plt");
		}

		public Process PlotFireballTemperatureEvolution()
		{
			CalculateFireballTemperatureEvolution();

			StringBuilder plotFile = new StringBuilder();
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced");
			plotFile.AppendLine();
			plotFile.AppendLine("set title ' Fireball temperature evolution'");
			plotFile.AppendLine("set xlabel 't (fm/c)'");
			plotFile.AppendLine("set ylabel 'T_{max} (MeV)'");
			plotFile.AppendLine();
			plotFile.AppendLine("set xrange [0:]");
			plotFile.AppendLine("set yrange [0:]");
			plotFile.AppendLine();
			plotFile.AppendFormat("set object rectangle from first 0, graph 0 to first {0}, graph 1"
				+ " behind fillcolor rgb 'black' fillstyle solid 0.1 noborder", ThermalTime_fm);
			plotFile.AppendLine();
			plotFile.AppendFormat("set label 'thermalization' at first {0}, graph 0.5"
				+ " center rotate by 90", ThermalTime_fm / 2);
			plotFile.AppendLine();
			plotFile.AppendFormat("set arrow from first {0}, first {1} to graph 1, first {1}"
				+ " nohead back linetype 0", ThermalTime_fm, QGPFormationTemperature_MeV);
			plotFile.AppendLine();
			plotFile.AppendLine();

			AppendPlotCommand(plotFile);

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		public void CalculateFireballTemperatureEvolution()
		{
			PrepareJob("PlotFireballTemperatureEvolution", SnapshotStatusTitles);

			if(SnapRate_per_fm <= 0)
			{
				throw new Exception("SnapRate <= 0.");
			}

			Fireball.Fireball fireball = CreateFireball();
			BjorkenLifeTime_fm = fireball.BjorkenLifeTime;

			StringBuilder output = new StringBuilder();
			output.AppendLine();
			output.AppendLine();
			output.AppendLine(string.Format("#{0,7}{1,20}", "Time", "MaximumTemperature"));
			output.AppendLine(string.Format("#{0,7}{1,20}", "(fm)", "(MeV)"));

			double dt = 1.0 / SnapRate_per_fm;
			do
			{
				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					break;
				}

				StatusValues[0] = fireball.CurrentTime.ToString();

				output.AppendLine(string.Format("{0,8:G4}{1,20:G4}",
					fireball.CurrentTime, fireball.MaximumTemperature));

				fireball.Advance(dt);
			} while(fireball.MaximumTemperature > BreakupTemperature_MeV);

			LifeTime_fm = fireball.LifeTime;

			WriteOutputToLogAndDataFile(output);
		}
	}
}
