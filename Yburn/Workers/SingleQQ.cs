/**************************************************************************************************
 *  The class SingleQQ is a WorkerApp and performs numerical calculations concerned with single
 *  quarkonium states in a thermal medium (the QGP). It includes the classes DecayWidth, QQState,
 *  QQBoundState and QQFreeState and is capable of calculating the quark mass from given
 *  experimental data, bound state and free wave functions as well as the gluodissociation decay
 *  width. Also it may call gnuplot to plot wave functions, potentials and cross sections.
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using Yburn.QQState;

namespace Yburn.Workers
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public partial class SingleQQ : Worker
	{
		/********************************************************************************************
		* Public static members, functions and properties
		********************************************************************************************/

		public static string[] BoundWaveStatusTitles
		{
			get
			{
				return new string[] {
					"Trials", "BoundMass", "SoftScale", "Energy", "GammaDamp", "|Psi(0)|", "Extrema"
				};
			}
		}

		public static string[] FreeWaveStatusTitles
		{
			get
			{
				return new string[] {
					"Radius", "LastMaximum", "CurrentMaximum", "Deviation"
				};
			}
		}

		public static string[] CrossSectionStatusTitles
		{
			get
			{
				return new string[] { "MinEnergy", "Energy", "MaxEnergy" };
			}
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public SingleQQ()
			: base()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void ArchiveQQData()
		{
			string pathFile = QQDataPathFile;

			QQDataSet dataSet = new QQDataSet(
				n: QuantumNumberN,
				l: QuantumNumberL,
				colorState: ColorState,
				potentialType: PotentialType,
				temperature: Temperature_MeV,
				debyeMass: DebyeMass_MeV,
				displacementRMS: DisplacementRMS_fm,
				softScale: SoftScale_MeV,
				ultraSoftScale: UltraSoftScale_MeV,
				boundMass: BoundMass_MeV,
				energy: Energy_MeV,
				gammaDamp: GammaDamp_MeV,
				gammaDiss: GammaDiss_MeV,
				gammaTot: GammaTot_MeV);

			QQDataDoc.Write(pathFile, dataSet);

			LogMessages.Clear();
			LogMessages.AppendLine("Results have been saved to QQ-data file.");
			LogMessages.AppendLine();
			LogMessages.AppendLine();
		}

		public void ShowArchivedQQData()
		{
			string pathFile = QQDataPathFile;
			LogMessages.Clear();
			LogMessages.AppendLine("Current QQ-data file:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(pathFile);
			LogMessages.AppendLine();
			LogMessages.AppendLine(QQDataDoc.ReadAllText(pathFile));
		}

		public void CreateNewArchiveDataFile()
		{
			string pathFile = YburnConfigFile.QQDataPathFile;
			if(!File.Exists(pathFile))
			{
				QQDataDoc.CreateNewDataDoc(pathFile, AccuracyAlpha, AccuracyWaveFunction,
					AggressivenessAlpha, MaxEnergy_MeV, EnergySteps, QuarkMass_MeV, MaxRadius_fm,
					StepNumber, Sigma_MeV2, Tchem_MeV,
					Tcrit_MeV);
			}
		}

		public void CompareResultsWithArchivedData()
		{
			QQDataSet dataSet = GetArchivedValues();

			CompareResultsWithDataSet(dataSet);
		}

		public void CalculateBoundWaveFunction()
		{
			PrepareJob("CalculateBoundWaveFunction", BoundWaveStatusTitles);

			try
			{
				QQBoundState boundState = new QQBoundState(GetQQStateParam(), QuantumNumberN)
				{
					CalculationCancelToken = JobCancelToken,
					StatusValues = StatusValues,
					UseFixedAlpha = UseFixedAlpha
				};

				boundState.SearchEigenfunction();

				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					LogMessages.Clear();
					LogMessages.Append(LogHeader + LogFooter);
					return;
				}

				UpdateOutputParameters(boundState);

				LogMessages.Clear();
				LogMessages.AppendLine(LogHeader + "#\r\n#");
				LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}",
					"Temperature",
					"DebyeMass",
					"<r^2>^1/2",
					"SoftScale",
					"US_Scale",
					"BoundMass",
					"Energy",
					"GammaDamp"));
				LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}",
					"(MeV)",
					"(MeV)",
					"(fm)",
					"(Mev)",
					"(Mev)",
					"(MeV)",
					"(MeV)",
					"(MeV)"));
				LogMessages.AppendLine();
				LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}",
					Temperature_MeV.ToString("G6"),
					DebyeMass_MeV.ToString("G6"),
					DisplacementRMS_fm.ToString("G6"),
					SoftScale_MeV.ToString("G6"),
					UltraSoftScale_MeV.ToString("G6"),
					BoundMass_MeV.ToString("G6"),
					Energy_MeV.ToString("G6"),
					GammaDamp_MeV.ToString("G6")));
				LogMessages.Append(LogFooter);

				List<string> dataList = new List<string>();
				dataList.Add(LogHeader + "#\r\n#");
				dataList.AddRange(CreateWaveFunctionDataList(boundState));
				File.WriteAllLines(YburnConfigFile.OutputPath + DataFileName, dataList);
			}
			catch
			{
				throw;
			}
		}

		public void CalculateFreeWaveFunction()
		{
			PrepareJob("CalculateFreeWaveFunction", FreeWaveStatusTitles);

			try
			{
				QQFreeState freeState = new QQFreeState(GetQQStateParam())
				{
					CalculationCancelToken = JobCancelToken,
					StatusValues = StatusValues
				};

				freeState.SearchEigenfunction();

				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					LogMessages.Clear();
					LogMessages.Append(LogHeader + LogFooter);
					return;
				}

				UpdateOutputParameters(freeState);

				LogMessages.Clear();
				LogMessages.AppendLine(LogHeader + "#\r\n#");
				LogMessages.AppendLine("Temperature DebyeMass   SoftScale   BoundMass   Energy      GammaDamp   ");
				LogMessages.AppendLine("(MeV)       (MeV)       (MeV)       (MeV)       (MeV)       (MeV)       ");
				LogMessages.AppendLine();
				LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}",
					Temperature_MeV.ToString("G6"),
					DebyeMass_MeV.ToString("G6"),
					SoftScale_MeV.ToString("G6"),
					BoundMass_MeV.ToString("G6"),
					Energy_MeV.ToString("G6"),
					GammaDamp_MeV.ToString("G6")));
				LogMessages.AppendLine(LogFooter);

				List<string> dataList = new List<string>();
				dataList.Add(LogHeader + "#\r\n#");
				dataList.AddRange(CreateWaveFunctionDataList(freeState));
				File.WriteAllLines(YburnConfigFile.OutputPath + DataFileName, dataList);
			}
			catch
			{
				throw;
			}
		}

		public void CalculateDissociationDecayWidth()
		{
			PrepareJob("CalculateDissociationDecayWidth", BoundWaveStatusTitles);

			try
			{
				QQBoundState boundState = new QQBoundState(GetQQStateParam(), QuantumNumberN)
				{
					CalculationCancelToken = JobCancelToken,
					StatusValues = StatusValues
				};

				boundState.SearchEigenfunction();

				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					LogMessages.Clear();
					LogMessages.Append(LogHeader + LogFooter);
					return;
				}

				UpdateOutputParameters(boundState);

				SetStatusVariables(CrossSectionStatusTitles);

				// calculate decay width and cross section
				DecayWidth decayWidth = new DecayWidth(JobCancelToken, boundState, MaxEnergy_MeV,
					EnergySteps, Temperature_MeV, StatusValues);
				decayWidth.CalculateGammaDiss();

				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					LogMessages.Clear();
					LogMessages.Append(LogHeader + LogFooter);
					return;
				}

				GammaDiss_MeV = decayWidth.GammaDiss_MeV;
				GammaTot_MeV = GammaDamp_MeV + GammaDiss_MeV;

				LogMessages.Clear();
				LogMessages.AppendLine(LogHeader + "#\r\n#");
				LogMessages.AppendLine("Temperature DebyeMass   <r^2>^1/2   SoftScale   US_Scale    BoundMass   Energy      GammaDamp   GammaDiss   GammaTot    ");
				LogMessages.AppendLine("(MeV)       (MeV)       (fm)        (Mev)       (Mev)       (MeV)       (MeV)       (MeV)       (MeV)       (MeV)       ");
				LogMessages.AppendLine();
				LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
					Temperature_MeV.ToString("G6"),
					DebyeMass_MeV.ToString("G6"),
					DisplacementRMS_fm.ToString("G6"),
					SoftScale_MeV.ToString("G6"),
					UltraSoftScale_MeV.ToString("G6"),
					BoundMass_MeV.ToString("G6"),
					Energy_MeV.ToString("G6"),
					GammaDamp_MeV.ToString("G6"),
					GammaDiss_MeV.ToString("G6"),
					GammaTot_MeV.ToString("G6")));
				LogMessages.Append(LogFooter);

				List<string> dataList = new List<string>();
				dataList.Add(LogHeader + "#\r\n#");
				dataList.AddRange(CreateWaveFunctionDataList(boundState));
				dataList.Add(string.Empty);
				dataList.Add(string.Empty);
				dataList.AddRange(decayWidth.CrossSectionStringList);
				File.WriteAllLines(YburnConfigFile.OutputPath + DataFileName, dataList);
			}
			catch
			{
				throw;
			}
		}

		public void CalculateQuarkMass()
		{
			PrepareJob("CalculateQuarkMass", BoundWaveStatusTitles);

			try
			{
				QQBoundState boundState = new QQBoundState(GetQQStateParam(), QuantumNumberN)
				{
					CalculationCancelToken = JobCancelToken,
					StatusValues = StatusValues
				};

				QuarkMass_MeV = boundState.SearchQuarkMass(BoundMass_MeV);

				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					LogMessages.Clear();
					LogMessages.Append(LogHeader + LogFooter);
					return;
				}

				UpdateOutputParameters(boundState);

				LogMessages.Clear();
				LogMessages.AppendLine(LogHeader + "#\r\n#\r\n" + LogFooter);

				List<string> dataList = new List<string>();
				dataList.Add(LogHeader + "#\r\n#");
				dataList.AddRange(CreateWaveFunctionDataList(boundState));
				File.WriteAllLines(YburnConfigFile.OutputPath + DataFileName, dataList);
			}
			catch
			{
				throw;
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static List<string> CreateWaveFunctionDataList(
			QQState.QQState state
			)
		{
			List<string> dataList = new List<string>
			{
				string.Format("{0,-10}{1,-18}{2,-18}", "#r (fm)", "Re(Psi) (fm^-1/2)", "Im(Psi) (fm^-1/2)")
			};

			for(int j = 0; j <= state.Param.StepNumber; j++)
			{
				dataList.Add(string.Format("{0,-10}{1,-18}{2,-18}",
					 state.Radius_fm[j].ToString("G10"),
					 state.WaveFunction_fm[j].Re.ToString("G10"),
					 state.WaveFunction_fm[j].Im.ToString("G10")));
			}

			return dataList;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected override Type GetEnumTypeByName(
			string enumName
			)
		{
			if(enumName == "ColorState")
			{
				return typeof(ColorState);
			}
			else if(enumName == "PotentialType")
			{
				return typeof(PotentialType);
			}
			else if(enumName == "RunningCouplingType")
			{
				return typeof(RunningCouplingType);
			}
			else if(enumName == "SpinState")
			{
				return typeof(SpinState);
			}
			else
			{
				throw new Exception("Invalid enum name \"" + enumName + "\".");
			}
		}

		private void UpdateOutputParameters(
			QQState.QQState state
			)
		{
			Energy_MeV = state.Param.Energy_MeV;
			GammaDamp_MeV = state.Param.GammaDamp_MeV;
			SoftScale_MeV = state.Param.SoftScale_MeV;
			AlphaHard = state.AlphaHard;
			AlphaSoft = state.AlphaSoft;
			AlphaThermal = state.AlphaThermal;
			DebyeMass_MeV = state.DebyeMass_MeV;
			Sigma_MeV2 = state.Param.Sigma_MeV;
			SigmaEff_MeV2 = state.SigmaEff_MeV;
			StepSize = state.StepSize_fm;
			StepsPerPeriod = state.WaveVector_fm * state.StepSize_fm;
			Tchem_MeV = state.Param.Tchem_MeV;
			Tcrit_MeV = state.Param.Tcrit_MeV;
			Trials = state.Trials;
			WaveVector_per_fm = state.WaveVector_fm;

			if(state is QQBoundState)
			{
				QQBoundState boundState = state as QQBoundState;
				NumberExtrema = boundState.NumberExtrema;
				BoundMass_MeV = boundState.BoundMass_MeV;
				AvgInvDisplacement_per_fm = boundState.RadiusExpectationValue(-1);
				DisplacementRMS_fm = Math.Sqrt(boundState.RadiusExpectationValue(2));
				AlphaUltraSoft = boundState.AlphaUltraSoft;
				UltraSoftScale_MeV = boundState.UltraSoftScale_MeV;
			}
		}

		private QQStateParam GetQQStateParam()
		{
			QQStateParam param = new QQStateParam
			{
				AccuracyAlpha = AccuracyAlpha,
				AccuracyWaveFunction = AccuracyWaveFunction,
				AggressivenessAlpha = AggressivenessAlpha,
				AggressivenessEnergy = AggressivenessEnergy,
				ColorState = ColorState,
				Energy_MeV = Energy_MeV,
				GammaDamp_MeV = GammaDamp_MeV,
				MaxRadius_fm = MaxRadius_fm,
				MaxShootingTrials = MaxShootingTrials,
				PotentialType = PotentialType,
				QuantumNumberL = QuantumNumberL,
				QuarkMass_MeV = QuarkMass_MeV,
				RunningCouplingType = RunningCouplingType,
				Sigma_MeV = Sigma_MeV2,
				SoftScale_MeV = SoftScale_MeV,
				SpinCouplingRange_fm = SpinCouplingRange_fm,
				SpinCouplingStrength_MeV = SpinCouplingStrength_MeV,
				SpinState = SpinState,
				StepNumber = StepNumber,
				Tchem_MeV = Tchem_MeV,
				Tcrit_MeV = Tcrit_MeV,
				Temperature_MeV = Temperature_MeV
			};

			return param;
		}

		private void CompareResultsWithDataSet(
			QQDataSet dataSet
			)
		{
			LogMessages.Clear();
			LogMessages.AppendLine("Comparison of current results to archived data:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
				"",
				"DebyeMass",
				"<r^2>^1/2",
				"SoftScale",
				"US_Scale",
				"BoundMass",
				"Energy",
				"GammaDamp",
				"GammaDiss",
				"GammaTot"));
			LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
				"",
				"(MeV)",
				"(fm)",
				"(Mev)",
				"(Mev)",
				"(MeV)",
				"(MeV)",
				"(MeV)",
				"(MeV)",
				"(MeV)"));
			LogMessages.AppendLine();
			LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
				"Current:",
				DebyeMass_MeV.ToString("G6"),
				DisplacementRMS_fm.ToString("G6"),
				SoftScale_MeV.ToString("G6"),
				UltraSoftScale_MeV.ToString("G6"),
				BoundMass_MeV.ToString("G6"),
				Energy_MeV.ToString("G6"),
				GammaDamp_MeV.ToString("G6"),
				GammaDiss_MeV.ToString("G6"),
				GammaTot_MeV.ToString("G6")));
			LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
				"Archived:",
				dataSet.DebyeMass.ToString("G6"),
				dataSet.DisplacementRMS.ToString("G6"),
				dataSet.SoftScale.ToString("G6"),
				dataSet.UltraSoftScale.ToString("G6"),
				dataSet.BoundMass.ToString("G6"),
				dataSet.Energy.ToString("G6"),
				dataSet.GammaDamp.ToString("G6"),
				dataSet.GammaDiss.ToString("G6"),
				dataSet.GammaTot.ToString("G6")));
			LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
				"Deviation:",
				(DebyeMass_MeV / dataSet.DebyeMass - 1).ToString("G3"),
				(DisplacementRMS_fm / dataSet.DisplacementRMS - 1).ToString("G3"),
				(SoftScale_MeV / dataSet.SoftScale - 1).ToString("G3"),
				(UltraSoftScale_MeV / dataSet.UltraSoftScale - 1).ToString("G3"),
				(BoundMass_MeV / dataSet.BoundMass - 1).ToString("G3"),
				(Energy_MeV / dataSet.Energy - 1).ToString("G3"),
				(GammaDamp_MeV / dataSet.GammaDamp - 1).ToString("G3"),
				(GammaDiss_MeV / dataSet.GammaDiss - 1).ToString("G3"),
				(GammaTot_MeV / dataSet.GammaTot - 1).ToString("G3")));
			LogMessages.AppendLine();
			LogMessages.AppendLine();
		}

		private QQDataSet GetArchivedValues()
		{
			string pathFile = QQDataPathFile;

			return QQDataDoc.GetDataSet(pathFile, QuantumNumberN, QuantumNumberL, ColorState,
				new List<PotentialType> { PotentialType }, Temperature_MeV);
		}

		protected override void StartJob(
			string jobId
			)
		{
			switch(jobId)
			{
				case "ArchiveQQData":
					ArchiveQQData();
					break;

				case "CalculateBoundWaveFunction":
					CalculateBoundWaveFunction();
					break;

				case "CompareResultsWithArchivedData":
					CompareResultsWithArchivedData();
					break;

				case "CreateNewArchiveDataFile":
					CreateNewArchiveDataFile();
					break;

				case "CalculateFreeWaveFunction":
					CalculateFreeWaveFunction();
					break;

				case "CalculateDissociationDecayWidth":
					CalculateDissociationDecayWidth();
					break;

				case "CalculateQuarkMass":
					CalculateQuarkMass();
					break;

				case "PlotAlpha":
					PlotAlpha();
					break;

				case "PlotCrossSection":
					PlotCrossSection();
					break;

				case "PlotPionGDF":
					PlotPionGDF();
					break;

				case "PlotQQPotential":
					PlotQQPotential();
					break;

				case "PlotWaveFunction":
					PlotWaveFunction();
					break;

				case "ShowArchivedQQData":
					ShowArchivedQQData();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}
	}
}
