﻿using System;
using System.IO;
using Yburn.Fireball;
using Yburn.QQState;

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
			AssertInputValid_CalculateInMediumDecayWidth();

			PrepareJob("CalculateInMediumDecayWidth");

			string temperatureDecayWidthList = GetTemperatureDecayWidthList();

			LogMessages.AppendFormat("#\r\n#\r\n");
			LogMessages.AppendFormat(temperatureDecayWidthList);

			File.WriteAllText(YburnConfigFile.OutputPath + DataFileName,
				LogHeader + "#\r\n#\r\n" + temperatureDecayWidthList);
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
			else if(enumName == "DecayWidthEvaluationType")
			{
				return typeof(DecayWidthEvaluationType);
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

		private string GetTemperatureDecayWidthList()
		{
			BottomiumState[] bottomiumStates = BottomiumStates;

			TemperatureDecayWidthPrinter printer = new TemperatureDecayWidthPrinter(
				YburnConfigFile.QQDataPathFile, bottomiumStates, DecayWidthType, PotentialTypes,
				MinTemperature, MaxTemperature, TemperatureStepSize, MediumVelocity,
				NumberAveragingAngles, QGPFormationTemperature);

			return printer.GetList(DecayWidthEvaluationTypes);
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

		private void AssertInputValid_CalculateInMediumDecayWidth()
		{
			if(BottomiumStates.Length == 0)
			{
				throw new Exception("No bottomium states given.");
			}
		}
	}
}