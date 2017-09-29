using System;
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
			else if(enumName == "DopplerShiftEvaluationType")
			{
				return typeof(DopplerShiftEvaluationType);
			}
			else if(enumName == "DecayWidthType")
			{
				return typeof(DecayWidthType);
			}
			else if(enumName == "ElectricDipoleAlignment")
			{
				return typeof(ElectricDipoleAlignment);
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
			TemperatureDecayWidthPrinter printer = new TemperatureDecayWidthPrinter(
				YburnConfigFile.QQDataPathFile, BottomiumStates, PotentialTypes, DecayWidthType,
				QGPFormationTemperature_MeV, NumberAveragingAngles);

			return printer.GetList(
				DopplerShiftEvaluationTypes, ElectricDipoleAlignment,
				MediumTemperatures_MeV, MediumVelocities, ElectricFieldStrength_per_fm2, MagneticFieldStrength_per_fm2);
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

				case "PlotDecayWidthsFromQQDataFile":
					PlotDecayWidthsFromQQDataFile();
					break;

				case "PlotEnergiesFromQQDataFile":
					PlotEnergiesFromQQDataFile();
					break;

				case "PlotInMediumDecayWidthsVersusMediumTemperature":
					PlotInMediumDecayWidthsVersusMediumTemperature();
					break;

				case "PlotInMediumDecayWidthsVersusMediumVelocity":
					PlotInMediumDecayWidthsVersusMediumVelocity();
					break;

				case "PlotDecayWidthEvaluatedAtDopplerShiftedTemperature":
					PlotDecayWidthEvaluatedAtDopplerShiftedTemperature();
					break;

				case "PlotElectromagneticallyShiftedEnergies":
					PlotElectromagneticallyShiftedEnergies();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}

		private void AssertInputValid_CalculateInMediumDecayWidth()
		{
			if(BottomiumStates.Count == 0)
			{
				throw new Exception("No bottomium states given.");
			}
		}
	}
}
