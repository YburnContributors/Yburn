using System;
using System.Collections.Generic;
using System.Text;
using Yburn.Fireball;
using Yburn.FormatUtil;
using Yburn.QQState;

namespace Yburn.Workers
{
	public class TemperatureDecayWidthPrinter
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public TemperatureDecayWidthPrinter(
			string dataPathFile,
			List<BottomiumState> bottomiumStates,
			List<PotentialType> potentialTypes,
			DecayWidthType decayWidthType,
			double qgpFormationTemperature,
			int numberAveragingAngles
			)
		{
			DataPathFile = dataPathFile;
			BottomiumStates = bottomiumStates;
			PotentialTypes = potentialTypes;
			DecayWidthType = decayWidthType;
			QGPFormationTemperature = qgpFormationTemperature;
			NumberAveragingAngles = numberAveragingAngles;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public string GetList(
			List<double> mediumTemperatures,
			List<double> mediumVelocities,
			List<DopplerShiftEvaluationType> evaluationTypes
			)
		{
			if(evaluationTypes == null || evaluationTypes.Count < 1)
			{
				throw new Exception("No DopplerShiftEvaluationTypes specified.");
			}

			StringBuilder builder = new StringBuilder();

			foreach(DopplerShiftEvaluationType evaluationType in evaluationTypes)
			{
				builder.Append(GetList(mediumTemperatures, mediumVelocities, evaluationType));
				builder.AppendLine();
				builder.AppendLine();
			}

			return builder.ToString();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly string DataPathFile;

		private readonly List<BottomiumState> BottomiumStates;

		private readonly List<PotentialType> PotentialTypes;

		private readonly DecayWidthType DecayWidthType;

		private readonly double QGPFormationTemperature;

		private readonly int NumberAveragingAngles;

		private DecayWidthProvider CreateDecayWidthProvider(
			DopplerShiftEvaluationType evaluationType
			)
		{
			return new DecayWidthProvider(
				DataPathFile,
				PotentialTypes,
				evaluationType,
				DecayWidthType,
				QGPFormationTemperature,
				NumberAveragingAngles);
		}

		private string GetList(
			List<double> mediumTemperatures,
			List<double> mediumVelocities,
			DopplerShiftEvaluationType evaluationType
			)
		{
			DecayWidthProvider provider = CreateDecayWidthProvider(evaluationType);

			StringBuilder list = new StringBuilder();
			AppendHeader(list, evaluationType);
			AppendDataLines(list, mediumTemperatures, mediumVelocities, provider);

			return list.ToString();
		}

		private void AppendHeader(
			StringBuilder list,
			DopplerShiftEvaluationType evaluationType
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
			List<double> mediumTemperatures,
			List<double> mediumVelocities,
			DecayWidthProvider provider
			)
		{
			foreach(double temperature in mediumTemperatures)
			{
				foreach(double velocity in mediumVelocities)
				{
					AppendDataLine(list, temperature, velocity, provider);
				}
				if((mediumTemperatures.Count > 1) && (mediumVelocities.Count > 1))
				{
					list.AppendLine();
				}
			}
		}

		private void AppendDataLine(
			StringBuilder list,
			double temperature,
			double velocity,
			DecayWidthProvider provider
			)
		{
			list.AppendFormat("{0,-20}", temperature.ToUIString());
			list.AppendFormat("{0,-20}", velocity.ToUIString());
			foreach(BottomiumState state in BottomiumStates)
			{
				AppendDecayWidthValue(list, state, temperature, velocity, provider);
			}
			list.AppendLine();
		}

		private void AppendDecayWidthValue(
			StringBuilder list,
			BottomiumState state,
			double temperature,
			double velocity,
			DecayWidthProvider provider
			)
		{
			list.AppendFormat("{0,-20}",
				provider.GetInMediumDecayWidth(state, temperature, velocity).ToUIString());
		}
	}
}