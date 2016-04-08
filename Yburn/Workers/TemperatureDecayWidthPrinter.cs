using System;
using System.Collections.Generic;
using System.Text;
using Yburn.Fireball;

namespace Yburn.Workers
{
	public class TemperatureDecayWidthPrinter
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public TemperatureDecayWidthPrinter(
			string dataPathFile,
			BottomiumState[] bottomiumStates,
			DecayWidthType decayWidthType,
			string[] potentialTypes,
			double minTemperature,
			double maxTemperature,
			double stepSize,
			double mediumVelocity,
			double[] averagingAngles
			)
		{
			DataPathFile = dataPathFile;
			BottomiumStates = bottomiumStates;
			DecayWidthType = decayWidthType;
			PotentialTypes = potentialTypes;
			MinTemperature = minTemperature;
			MaxTemperature = maxTemperature;
			StepSize = stepSize;
			MediumVelocity = mediumVelocity;
			AveragingAngles = averagingAngles;

			AssertValidInput();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public string GetList()
		{
			UseAveragedTemperature = false;
			DecayWidthAverager[] averagers = CreateDecayWidthAveragers();

			return GetList(averagers);
		}

		public string GetListUsingAveragedTemperature()
		{
			DecayWidthAverager[] averagers = CreateDecayWidthAveragers();

			UseAveragedTemperature = false;
			string list = GetList(averagers);

			UseAveragedTemperature = true;
			list += "\r\n";
			list += GetList(averagers);

			return list;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string DoubleToString(
			double aDouble
			)
		{
			return aDouble.ToString("G4");
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private string DataPathFile;

		private BottomiumState[] BottomiumStates;

		private DecayWidthType DecayWidthType;

		private string[] PotentialTypes;

		private double StepSize;

		private double MaxTemperature;

		private double MinTemperature;

		private double MediumVelocity;

		private double[] AveragingAngles;

		private bool UseAveragedTemperature;

		private void AssertValidInput()
		{
			if(StepSize <= 0)
			{
				throw new ArgumentException("StepSize <= 0.");
			}
		}

		private DecayWidthAverager[] CreateDecayWidthAveragers()
		{
			DecayWidthAverager[] averagers = new DecayWidthAverager[BottomiumStates.Length];
			for(int i = 0; i < averagers.Length; i++)
			{
				averagers[i] = CreateDecayWidthAverager(BottomiumStates[i]);
			}

			return averagers;
		}

		protected virtual DecayWidthAverager CreateDecayWidthAverager(
			BottomiumState state
			)
		{
			List<KeyValuePair<double, double>> list = TemperatureDecayWidthList.GetList(
				DataPathFile, state, DecayWidthType, PotentialTypes);

			return AveragingAngles == null || AveragingAngles.Length == 0 ?
				new DecayWidthAverager(list)
				: new DecayWidthAverager(list, AveragingAngles);
		}

		private string GetList(
			DecayWidthAverager[] averagers
			)
		{
			StringBuilder list = new StringBuilder();
			AppendHeader(list);
			AppendDataLines(averagers, list);
			list.AppendFormat("\r\n");

			return list.ToString();
		}

		private void AppendHeader(
			StringBuilder list
			)
		{
			AppendHeaderTemperature(list);
			foreach(BottomiumState state in BottomiumStates)
			{
				list.AppendFormat("{0,-20}", "DecayWidth(" + state + ")");
			}

			list.AppendFormat("\r\n");

			list.AppendFormat("{0,-20}", "#(MeV)");
			foreach(BottomiumState state in BottomiumStates)
			{
				list.AppendFormat("{0,-20}", "(MeV)");
			}

			list.AppendFormat("\r\n#\r\n");
		}

		private void AppendHeaderTemperature(
			StringBuilder list
			)
		{
			if(UseAveragedTemperature)
			{
				list.AppendFormat("{0,-20}", "#Eff. Temperature");
			}
			else
			{
				list.AppendFormat("{0,-20}", "#Temperature");
			}
		}

		private void AppendDataLines(
			DecayWidthAverager[] averagers,
			StringBuilder list
			)
		{
			double temperature = MinTemperature;
			while(temperature <= MaxTemperature)
			{
				AppendDataLine(averagers, list, temperature);
				temperature += StepSize;
			}
		}

		private void AppendDataLine(
			DecayWidthAverager[] averagers,
			StringBuilder list,
			double temperature
			)
		{
			AppendTemperatureValue(list, temperature);
			foreach(DecayWidthAverager averager in averagers)
			{
				AppendDecayWidthValue(list, temperature, averager);
			}
			list.AppendFormat("\r\n");
		}

		private void AppendTemperatureValue(
			StringBuilder list,
			double temperature
			)
		{
			if(UseAveragedTemperature)
			{
				list.AppendFormat("{0,-20}", DoubleToString(
					DecayWidthAverager.GetAveragedTemperature(temperature, MediumVelocity)));
			}
			else
			{
				list.AppendFormat("{0,-20}", DoubleToString(temperature));
			}
		}

		private void AppendDecayWidthValue(
			StringBuilder list,
			double temperature,
			DecayWidthAverager averager
			)
		{
			if(UseAveragedTemperature)
			{
				list.AppendFormat("{0,-20}", DoubleToString(
					averager.GetDecayWidthUsingAveragedTemperature(temperature, MediumVelocity)));
			}
			else
			{
				list.AppendFormat("{0,-20}", DoubleToString(
					averager.GetDecayWidth(temperature, MediumVelocity)));
			}
		}
	}
}