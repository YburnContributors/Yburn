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
			int numberAveragingAngles,
			double qgpFormationTemperature
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
			NumberAveragingAngles = numberAveragingAngles;
			QGPFormationTemperature = qgpFormationTemperature;

			AssertValidInput();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public string GetList(
			DecayWidthEvaluationType[] evaluationTypes
			)
		{
			if(evaluationTypes == null || evaluationTypes.Length < 1)
			{
				throw new Exception("No DecayWidthEvaluationTypes specified.");
			}

			DecayWidthAverager[] averagers = CreateDecayWidthAveragers();

			string list = GetList(evaluationTypes[0], averagers);

			for(int i = 1; i < evaluationTypes.Length; i++)
			{
				list += "\r\n";
				list += GetList(evaluationTypes[i], averagers);
			}

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

		private int NumberAveragingAngles;

		private double QGPFormationTemperature;

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
			List<KeyValuePair<double, double>> list = TemperatureDecayWidthListHelper.GetList(
				DataPathFile, state, DecayWidthType, PotentialTypes);

			return new DecayWidthAverager(list, NumberAveragingAngles, QGPFormationTemperature);
		}

		private string GetList(
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager[] averagers
			)
		{
			StringBuilder list = new StringBuilder();
			AppendHeader(list, evaluationType);
			AppendDataLines(list, evaluationType, averagers);
			list.AppendFormat("\r\n");

			return list.ToString();
		}

		private void AppendHeader(
			StringBuilder list,
			DecayWidthEvaluationType evaluationType
			)
		{
			list.AppendLine("#" + evaluationType.ToUIString());
			list.AppendFormat("{0,-20}", "#Temperature");
			foreach(BottomiumState state in BottomiumStates)
			{
				list.AppendFormat("{0,-20}", "DecayWidth(" + state + ")");
			}

			list.AppendLine();

			list.AppendFormat("{0,-20}", "#(MeV)");
			foreach(BottomiumState state in BottomiumStates)
			{
				list.AppendFormat("{0,-20}", "(MeV)");
			}

			list.AppendLine();
			list.AppendLine("#");
		}

		private void AppendDataLines(
			StringBuilder list,
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager[] averagers
			)
		{
			double temperature = MinTemperature;
			while(temperature <= MaxTemperature)
			{
				AppendDataLine(list, temperature, evaluationType, averagers);
				temperature += StepSize;
			}
		}

		private void AppendDataLine(
			StringBuilder list,
			double temperature,
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager[] averagers
			)
		{
			list.AppendFormat("{0,-20}", DoubleToString(temperature));
			foreach(DecayWidthAverager averager in averagers)
			{
				AppendDecayWidthValue(list, temperature, evaluationType, averager);
			}
			list.AppendFormat("\r\n");
		}

		private void AppendDecayWidthValue(
			StringBuilder list,
			double temperature,
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager averager
			)
		{
			list.AppendFormat("{0,-20}", DoubleToString(
				averager.GetDecayWidth(temperature, MediumVelocity, evaluationType)));
		}
	}
}