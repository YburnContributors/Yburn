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
			int numberAveragingAngles,
			double qgpFormationTemperature
			)
		{
			DataPathFile = dataPathFile;
			BottomiumStates = bottomiumStates;
			DecayWidthType = decayWidthType;
			PotentialTypes = potentialTypes;
			NumberAveragingAngles = numberAveragingAngles;
			QGPFormationTemperature = qgpFormationTemperature;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public string GetList(
			double[] mediumTemperatures,
			double[] mediumVelocities,
			DecayWidthEvaluationType[] evaluationTypes
			)
		{
			DecayWidthAverager[] averagers = CreateDecayWidthAveragers();

			if(evaluationTypes == null || evaluationTypes.Length < 1)
			{
				throw new Exception("No DecayWidthEvaluationTypes specified.");
			}

			string list =
				GetList(mediumTemperatures, mediumVelocities, evaluationTypes[0], averagers);

			for(int i = 1; i < evaluationTypes.Length; i++)
			{
				list += "\r\n";
				list += GetList(mediumTemperatures, mediumVelocities, evaluationTypes[i], averagers);
			}

			return list;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private string DataPathFile;

		private BottomiumState[] BottomiumStates;

		private DecayWidthType DecayWidthType;

		private string[] PotentialTypes;

		private int NumberAveragingAngles;

		private double QGPFormationTemperature;

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
			double[] mediumTemperatures,
			double[] mediumVelocities,
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager[] averagers
			)
		{
			StringBuilder list = new StringBuilder();
			AppendHeader(list, evaluationType);
			AppendDataLines(list, mediumTemperatures, mediumVelocities, evaluationType, averagers);
			list.AppendFormat("\r\n");

			return list.ToString();
		}

		private void AppendHeader(
			StringBuilder list,
			DecayWidthEvaluationType evaluationType
			)
		{
			list.AppendLine("#" + evaluationType.ToUIString());
			list.AppendFormat("{0,-20}", "#MediumTemperature");
			list.AppendFormat("{0,-20}", "MediumVelocity");
			foreach(BottomiumState state in BottomiumStates)
			{
				list.AppendFormat("{0,-20}", "DecayWidth(" + state + ")");
			}

			list.AppendLine();

			list.AppendFormat("{0,-20}", "#(MeV)");
			list.AppendFormat("{0,-20}", "(c)");
			foreach(BottomiumState state in BottomiumStates)
			{
				list.AppendFormat("{0,-20}", "(MeV)");
			}

			list.AppendLine();
			list.AppendLine("#");
		}

		private void AppendDataLines(
			StringBuilder list,
			double[] mediumTemperatures,
			double[] mediumVelocities,
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager[] averagers
			)
		{
			foreach(double temperature in mediumTemperatures)
			{
				foreach(double velocity in mediumVelocities)
				{
					AppendDataLine(list, temperature, velocity, evaluationType, averagers);
				}
			}
		}

		private void AppendDataLine(
			StringBuilder list,
			double temperature,
			double velocity,
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager[] averagers
			)
		{
			list.AppendFormat("{0,-20}", temperature.ToUIString());
			list.AppendFormat("{0,-20}", velocity.ToUIString());
			foreach(DecayWidthAverager averager in averagers)
			{
				AppendDecayWidthValue(list, temperature, velocity, evaluationType, averager);
			}
			list.AppendFormat("\r\n");
		}

		private void AppendDecayWidthValue(
			StringBuilder list,
			double temperature,
			double velocity,
			DecayWidthEvaluationType evaluationType,
			DecayWidthAverager averager
			)
		{
			list.AppendFormat("{0,-20}",
				averager.GetDecayWidth(temperature, velocity, evaluationType).ToUIString());
		}
	}
}