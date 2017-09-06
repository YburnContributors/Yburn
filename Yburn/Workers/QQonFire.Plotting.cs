using System;
using System.Text;

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

		public void ShowSnapsX()
		{
			StartGnuplot(BuildSnapsFileName(DataFileName) + "-plotX.plt");
		}

		public void ShowSnapsY()
		{
			StartGnuplot(BuildSnapsFileName(DataFileName) + "-plotY.plt");
		}

		public void ShowSnapsXY()
		{
			StartGnuplot(BuildSnapsFileName(DataFileName) + "-plotXY.plt");
		}

		public void MakeSnapshots()
		{
			PrepareJob("MakeSnapshots", SnapshotStatusTitles);

			if(SnapRate_per_fm <= 0)
			{
				throw new Exception("SnapRate <= 0.");
			}

			Fireball.Fireball fireball = CreateFireball();
			BjorkenLifeTime_fm = fireball.BjorkenLifeTime;

			// extract path and file name of outfile and extension separately
			string fileName = BuildSnapsFileName(DataFileName);

			// All data is saved in the output file. Additionally, the corresponding gnuplot files (.plt)
			// are created to facilitate graphical visualization of the data.
			StringBuilder dataFileString = new StringBuilder();
			StringBuilder gnuFileStringX = new StringBuilder();
			StringBuilder gnuFileStringY = new StringBuilder();
			StringBuilder gnuFileStringXY = new StringBuilder();

			int numberGridPoints = Convert.ToInt32(Math.Round(GridRadius_fm / GridCellSize_fm)) + 1;

			gnuFileStringX.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\n\r\n",
				GridRadius_fm.ToString()));
			gnuFileStringY.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\n\r\n",
				GridRadius_fm.ToString()));
			gnuFileStringXY.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\nset yr[0:{1,3}]\r\n\r\n",
				GridRadius_fm.ToString(), GridRadius_fm.ToString()));

			string xPlotStringBegin = "p \"" + fileName
				+ "\" every " + numberGridPoints.ToString() + " index ";
			string xPlotStringEnd = " u 1:3 w p; pause .5";
			string yPlotStringBegin = "p \"" + fileName
				+ "\" every ::::" + (numberGridPoints - 1).ToString() + " index ";
			string yPlotStringEnd = " u 2:3 w p; pause .5";
			string xyPlotStringBegin = "sp \"" + fileName + "\" index ";
			string xyPlotStringEnd = " u 1:2:3 w p; pause .5";

			int index = 0;
			double dt = 1.0 / SnapRate_per_fm;
			double currentTime;
			while(fireball.MaximumTemperature > BreakupTemperature_MeV)
			{
				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					break;
				}

				// advance fireball except for the first snapshot
				if(index != 0)
				{
					fireball.Advance(dt);
				}

				// get status of calculation
				currentTime = fireball.CurrentTime;
				StatusValues[0] = currentTime.ToString();

				dataFileString.AppendLine("\r\n\r\n#Time = "
					+ currentTime.ToString() + ", Index " + index);
				dataFileString.Append(fireball.FieldsToString(FireballFieldTypes, BottomiumStates));

				gnuFileStringX.AppendLine(xPlotStringBegin + index + xPlotStringEnd);
				gnuFileStringY.AppendLine(yPlotStringBegin + index + yPlotStringEnd);
				gnuFileStringXY.AppendLine(xyPlotStringBegin + index + xyPlotStringEnd);

				index++;
			}

			LifeTime_fm = fireball.LifeTime;

			// append final results in the output file and exchange the old header with a new one
			LogMessages.Clear();
			LogMessages.Append(LogHeader + LogFooter);
			dataFileString.Insert(0, LogHeader);
			dataFileString.Append(LogFooter);

			WriteFile(dataFileString, fileName);
			WriteFile(gnuFileStringX, fileName + "-plotX.plt");
			WriteFile(gnuFileStringY, fileName + "-plotY.plt");
			WriteFile(gnuFileStringXY, fileName + "-plotXY.plt");
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private string BuildSnapsFileName(
			string dataFileName
			)
		{
			string nameWithoutExtension = dataFileName;
			string extension = string.Empty;
			int indexOfDot = dataFileName.LastIndexOf(".");

			if(indexOfDot >= 0)
			{
				nameWithoutExtension = dataFileName.Substring(0, indexOfDot);
				extension = dataFileName.Substring(indexOfDot, dataFileName.Length - indexOfDot);
			}

			return (nameWithoutExtension + "-b" + ImpactParameter_fm + extension).Replace("\\", "/");
		}
	}
}
