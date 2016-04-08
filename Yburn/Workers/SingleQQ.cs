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
using System.Text;
using Yburn.Fireball;
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
			string pathFile = GetQQDataPathFile();

			QQDataDoc.Write(pathFile, QuantumNumberN, QuantumNumberL, ColorState.ToString(),
				PotentialType.ToString(), Temperature, DebyeMass, RMS,
				SoftScale, UltraSoftScale, BoundMass, Energy,
				GammaDamp, GammaDiss, GammaTot);

			LogMessages.Clear();
			LogMessages.AppendLine("Results have been saved to QQ-data file.");
			LogMessages.AppendLine();
			LogMessages.AppendLine();
		}

		public void ShowArchivedQQData()
		{
			string pathFile = GetQQDataPathFile();
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
					AggressivenessAlpha, MaxEnergy, EnergySteps, QuarkMass, MaxRadius,
					StepNumber, AlphaHard, Sigma, SigmaEff, Tchem,
					Tcrit);
			}
		}

		public void CompareResultsWithArchivedData()
		{
			double debyeMass;
			double rMS;
			double softScale;
			double ultraSoftScale;
			double boundMass;
			double energy;
			double gammaDamp;
			double gammaDiss;
			double gammaTot;
			GetArchivedValues(out debyeMass, out rMS, out softScale, out ultraSoftScale,
				out boundMass, out energy, out gammaDamp, out gammaDiss, out gammaTot);

			CompareResultsWithArchivedData(debyeMass, rMS, softScale, ultraSoftScale, boundMass,
				energy, gammaDamp, gammaDiss, gammaTot);
		}

		public void CalculateBoundWaveFunction()
		{
			PrepareJob("CalculateBoundWaveFunction", BoundWaveStatusTitles);

			try
			{
				QQBoundState boundState = new QQBoundState(GetQQStateParam(), QuantumNumberN);
				boundState.CalculationCancelToken = JobCancelToken;
				boundState.StatusValues = StatusValues;
				boundState.UseFixedAlpha = UseFixedAlpha;

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
					Temperature.ToString("G6"),
					DebyeMass.ToString("G6"),
					RMS.ToString("G6"),
					SoftScale.ToString("G6"),
					UltraSoftScale.ToString("G6"),
					BoundMass.ToString("G6"),
					Energy.ToString("G6"),
					GammaDamp.ToString("G6")));
				LogMessages.Append(LogFooter);

				List<string> dataList = new List<string>();
				dataList.Add(LogHeader + "#\r\n#");
				dataList.AddRange(CreateWaveFunctionDataList(boundState));
				File.WriteAllLines(YburnConfigFile.OutputPath + Outfile, dataList);
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
				QQFreeState freeState = new QQFreeState(GetQQStateParam());
				freeState.CalculationCancelToken = JobCancelToken;
				freeState.StatusValues = StatusValues;

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
					Temperature.ToString("G6"),
					DebyeMass.ToString("G6"),
					SoftScale.ToString("G6"),
					BoundMass.ToString("G6"),
					Energy.ToString("G6"),
					GammaDamp.ToString("G6")));
				LogMessages.AppendLine(LogFooter);

				List<string> dataList = new List<string>();
				dataList.Add(LogHeader + "#\r\n#");
				dataList.AddRange(CreateWaveFunctionDataList(freeState));
				File.WriteAllLines(YburnConfigFile.OutputPath + Outfile, dataList);
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
				QQBoundState boundState = new QQBoundState(GetQQStateParam(), QuantumNumberN);
				boundState.CalculationCancelToken = JobCancelToken;
				boundState.StatusValues = StatusValues;

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
				DecayWidth decayWidth = new DecayWidth(JobCancelToken, boundState, MaxEnergy,
					EnergySteps, Temperature, StatusValues);
				decayWidth.CalculateGammaDiss();

				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					LogMessages.Clear();
					LogMessages.Append(LogHeader + LogFooter);
					return;
				}

				GammaDiss = decayWidth.GammaDissMeV;
				GammaTot = GammaDamp + GammaDiss;

				LogMessages.Clear();
				LogMessages.AppendLine(LogHeader + "#\r\n#");
				LogMessages.AppendLine("Temperature DebyeMass   <r^2>^1/2   SoftScale   US_Scale    BoundMass   Energy      GammaDamp   GammaDiss   GammaTot    ");
				LogMessages.AppendLine("(MeV)       (MeV)       (fm)        (Mev)       (Mev)       (MeV)       (MeV)       (MeV)       (MeV)       (MeV)       ");
				LogMessages.AppendLine();
				LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
					Temperature.ToString("G6"),
					DebyeMass.ToString("G6"),
					RMS.ToString("G6"),
					SoftScale.ToString("G6"),
					UltraSoftScale.ToString("G6"),
					BoundMass.ToString("G6"),
					Energy.ToString("G6"),
					GammaDamp.ToString("G6"),
					GammaDiss.ToString("G6"),
					GammaTot.ToString("G6")));
				LogMessages.Append(LogFooter);

				List<string> dataList = new List<string>();
				dataList.Add(LogHeader + "#\r\n#");
				dataList.AddRange(CreateWaveFunctionDataList(boundState));
				dataList.Add(string.Empty);
				dataList.Add(string.Empty);
				dataList.AddRange(decayWidth.CrossSectionStringList);
				File.WriteAllLines(YburnConfigFile.OutputPath + Outfile, dataList);
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
				QQBoundState boundState = new QQBoundState(GetQQStateParam(), QuantumNumberN);
				boundState.CalculationCancelToken = JobCancelToken;
				boundState.StatusValues = StatusValues;

				QuarkMass = boundState.SearchQuarkMass(BoundMass);

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
				File.WriteAllLines(YburnConfigFile.OutputPath + Outfile, dataList);
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
			List<string> dataList = new List<string>();
			dataList.Add(string.Format("{0,-10}{1,-18}{2,-18}",
					 "#r (fm)",
					 "Re(Psi) (fm^-1/2)",
					 "Im(Psi) (fm^-1/2)"));

			for(int j = 0; j <= state.Param.StepNumber; j++)
			{
				dataList.Add(string.Format("{0,-10}{1,-18}{2,-18}",
					 state.RadiusFm[j].ToString("G10"),
					 state.WaveFunctionFm[j].Re.ToString("G10"),
					 state.WaveFunctionFm[j].Im.ToString("G10")));
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

		private double AccuracyAlpha;

		private double AccuracyWaveFunction;

		private double AggressivenessAlpha;

		private double AlphaHard;

		private double AlphaSoft;

		private double AlphaThermal;

		private double AlphaUltraSoft;

		private double AvInvRadius;

		private double BoundMass;

		private ColorState ColorState;

		private double DebyeMass;

		private double StepSize;

		private double MaxEnergy;

		private double Energy;

		private int EnergySteps;

		private double GammaDamp;

		private double GammaDiss;

		private double GammaTot;

		private double WaveVector;

		private double StepsPerPeriod;

		private int QuantumNumberL;

		private int MaxShootingTrials;

		private int QuantumNumberN;

		private int NumberExtrema;

		private string Outfile = "stdout.txt";

		private PotentialType PotentialType;

		private double QuarkMass;

		private RunningCouplingType RunningCouplingType;

		private double MaxRadius;

		private double RMS;

		private int StepNumber;

		private double Sigma;

		private double SigmaEff;

		private double SoftScale;

		private SpinState SpinState;

		private double SpinCouplingStrength;

		private double SpinCouplingRange;

		private double Temperature;

		private double Tchem;

		private double Tcrit;

		private double Trials;

		private double UltraSoftScale;

		private bool UseFixedAlpha;

		protected override void SetVariableNameValueList(
			Dictionary<string, string> nameValuePairs
			)
		{
			AccuracyAlpha = Extractor.TryGetDouble(nameValuePairs, "AccuracyAlpha", AccuracyAlpha);
			AccuracyWaveFunction = Extractor.TryGetDouble(nameValuePairs, "AccuracyWaveFunction", AccuracyWaveFunction);
			AggressivenessAlpha = Extractor.TryGetDouble(nameValuePairs, "AggressivenessAlpha", AggressivenessAlpha);
			AlphaHard = Extractor.TryGetDouble(nameValuePairs, "AlphaHard", AlphaHard);
			AlphaSoft = Extractor.TryGetDouble(nameValuePairs, "AlphaSoft", AlphaSoft);
			AlphaThermal = Extractor.TryGetDouble(nameValuePairs, "AlphaThermal", AlphaThermal);
			AlphaUltraSoft = Extractor.TryGetDouble(nameValuePairs, "AlphaUltraSoft", AlphaUltraSoft);
			AvInvRadius = Extractor.TryGetDouble(nameValuePairs, "AvInvRadius", AvInvRadius);
			BoundMass = Extractor.TryGetDouble(nameValuePairs, "BoundMass", BoundMass);
			ColorState = Extractor.TryGetEnum<ColorState>(nameValuePairs, "ColorState", ColorState);
			DataFileName = Extractor.TryGetString(nameValuePairs, "DataFileName", DataFileName);
			DataFileName = Extractor.TryGetString(nameValuePairs, "Outfile", DataFileName);
			DebyeMass = Extractor.TryGetDouble(nameValuePairs, "DebyeMass", DebyeMass);
			Energy = Extractor.TryGetDouble(nameValuePairs, "Energy", Energy);
			EnergyScale = Extractor.TryGetDouble(nameValuePairs, "EnergyScale", EnergyScale);
			EnergySteps = Extractor.TryGetInt(nameValuePairs, "EnergySteps", EnergySteps);
			GammaDamp = Extractor.TryGetDouble(nameValuePairs, "GammaDamp", GammaDamp);
			GammaDiss = Extractor.TryGetDouble(nameValuePairs, "GammaDiss", GammaDiss);
			GammaTot = Extractor.TryGetDouble(nameValuePairs, "GammaTot", GammaTot);
			MaxEnergy = Extractor.TryGetDouble(nameValuePairs, "MaxEnergy", MaxEnergy);
			MaxRadius = Extractor.TryGetDouble(nameValuePairs, "MaxRadius", MaxRadius);
			MaxShootingTrials = Extractor.TryGetInt(nameValuePairs, "MaxShootingTrials", MaxShootingTrials);
			MinEnergy = Extractor.TryGetDouble(nameValuePairs, "MinEnergy", MinEnergy);
			MinRadius = Extractor.TryGetDouble(nameValuePairs, "MinRadius", MinRadius);
			NumberExtrema = Extractor.TryGetInt(nameValuePairs, "NumberExtrema", NumberExtrema);
			QuantumNumberL = Extractor.TryGetInt(nameValuePairs, "QuantumNumberL", QuantumNumberL);
			QuantumNumberN = Extractor.TryGetInt(nameValuePairs, "QuantumNumberN", QuantumNumberN);
			QuarkMass = Extractor.TryGetDouble(nameValuePairs, "QuarkMass", QuarkMass);
			Outfile = Extractor.TryGetString(nameValuePairs, "Outfile", Outfile);
			PotentialType = Extractor.TryGetEnum<PotentialType>(nameValuePairs, "PotentialType", PotentialType);
			RMS = Extractor.TryGetDouble(nameValuePairs, "RMS", RMS);
			RunningCouplingType = Extractor.TryGetEnum<RunningCouplingType>(nameValuePairs, "RunningCouplingType", RunningCouplingType);
			RunningCouplingTypeSelection = Extractor.TryGetString(nameValuePairs, "RunningCouplingTypeSelection", RunningCouplingTypeSelection);
			Samples = Extractor.TryGetInt(nameValuePairs, "Samples", Samples);
			Sigma = Extractor.TryGetDouble(nameValuePairs, "Sigma", Sigma);
			SigmaEff = Extractor.TryGetDouble(nameValuePairs, "SigmaEff", SigmaEff);
			SoftScale = Extractor.TryGetDouble(nameValuePairs, "SoftScale", SoftScale);
			SpinCouplingRange = Extractor.TryGetDouble(nameValuePairs, "SpinCouplingRange", SpinCouplingRange);
			SpinCouplingStrength = Extractor.TryGetDouble(nameValuePairs, "SpinCouplingStrength", SpinCouplingStrength);
			SpinState = Extractor.TryGetEnum<SpinState>(nameValuePairs, "SpinState", SpinState);
			StepNumber = Extractor.TryGetInt(nameValuePairs, "StepNumber", StepNumber);
			StepSize = Extractor.TryGetDouble(nameValuePairs, "StepSize", StepSize);
			StepsPerPeriod = Extractor.TryGetDouble(nameValuePairs, "StepsPerPeriod", StepsPerPeriod);
			Temperature = Extractor.TryGetDouble(nameValuePairs, "Temperature", Temperature);
			Tchem = Extractor.TryGetDouble(nameValuePairs, "Tchem", Tchem);
			Tcrit = Extractor.TryGetDouble(nameValuePairs, "Tcrit", Tcrit);
			Trials = Extractor.TryGetDouble(nameValuePairs, "Trials", Trials);
			UltraSoftScale = Extractor.TryGetDouble(nameValuePairs, "UltraSoftScale", UltraSoftScale);
			UseFixedAlpha = Extractor.TryGetBool(nameValuePairs, "UseFixedAlpha", UseFixedAlpha);
			WaveVector = Extractor.TryGetDouble(nameValuePairs, "WaveVector", WaveVector);
		}

		protected override Dictionary<string, string> GetVariableNameValueList()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["AccuracyAlpha"] = AccuracyAlpha.ToString();
			nameValuePairs["AccuracyWaveFunction"] = AccuracyWaveFunction.ToString();
			nameValuePairs["AggressivenessAlpha"] = AggressivenessAlpha.ToString();
			nameValuePairs["AlphaHard"] = AlphaHard.ToString();
			nameValuePairs["AlphaSoft"] = AlphaSoft.ToString();
			nameValuePairs["AlphaThermal"] = AlphaThermal.ToString();
			nameValuePairs["AlphaUltraSoft"] = AlphaUltraSoft.ToString();
			nameValuePairs["AvInvRadius"] = AvInvRadius.ToString();
			nameValuePairs["BoundMass"] = BoundMass.ToString();
			nameValuePairs["ColorState"] = ColorState.ToString();
			nameValuePairs["DataFileName"] = DataFileName;
			nameValuePairs["DebyeMass"] = DebyeMass.ToString();
			nameValuePairs["Energy"] = Energy.ToString();
			nameValuePairs["EnergyScale"] = EnergyScale.ToString();
			nameValuePairs["EnergySteps"] = EnergySteps.ToString();
			nameValuePairs["GammaDamp"] = GammaDamp.ToString();
			nameValuePairs["GammaDiss"] = GammaDiss.ToString();
			nameValuePairs["GammaTot"] = GammaTot.ToString();
			nameValuePairs["MaxEnergy"] = MaxEnergy.ToString();
			nameValuePairs["MaxRadius"] = MaxRadius.ToString();
			nameValuePairs["MaxShootingTrials"] = MaxShootingTrials.ToString();
			nameValuePairs["MinEnergy"] = MinEnergy.ToString();
			nameValuePairs["MinRadius"] = MinRadius.ToString();
			nameValuePairs["NumberExtrema"] = NumberExtrema.ToString();
			nameValuePairs["Outfile"] = Outfile.ToString();
			nameValuePairs["PotentialType"] = PotentialType.ToString();
			nameValuePairs["QuantumNumberL"] = QuantumNumberL.ToString();
			nameValuePairs["QuantumNumberN"] = QuantumNumberN.ToString();
			nameValuePairs["QuarkMass"] = QuarkMass.ToString();
			nameValuePairs["RunningCouplingType"] = RunningCouplingType.ToString();
			nameValuePairs["RunningCouplingTypeSelection"] = RunningCouplingTypeSelection;
			nameValuePairs["RMS"] = RMS.ToString();
			nameValuePairs["Samples"] = Samples.ToString();
			nameValuePairs["Sigma"] = Sigma.ToString();
			nameValuePairs["SigmaEff"] = SigmaEff.ToString();
			nameValuePairs["SoftScale"] = SoftScale.ToString();
			nameValuePairs["SpinCouplingRange"] = SpinCouplingRange.ToString();
			nameValuePairs["SpinCouplingStrength"] = SpinCouplingStrength.ToString();
			nameValuePairs["SpinState"] = SpinState.ToString();
			nameValuePairs["StepNumber"] = StepNumber.ToString();
			nameValuePairs["StepSize"] = StepSize.ToString();
			nameValuePairs["StepsPerPeriod"] = StepsPerPeriod.ToString();
			nameValuePairs["Temperature"] = Temperature.ToString();
			nameValuePairs["Tchem"] = Tchem.ToString();
			nameValuePairs["Tcrit"] = Tcrit.ToString();
			nameValuePairs["Trials"] = Trials.ToString();
			nameValuePairs["UltraSoftScale"] = UltraSoftScale.ToString();
			nameValuePairs["UseFixedAlpha"] = UseFixedAlpha.ToString();
			nameValuePairs["WaveVector"] = WaveVector.ToString();

			return nameValuePairs;
		}

		private void UpdateOutputParameters(
			QQState.QQState state
			)
		{
			Energy = state.Param.EnergyMeV;
			GammaDamp = state.Param.GammaDampMeV;
			SoftScale = state.Param.SoftScaleMeV;
			AlphaHard = state.AlphaHard;
			AlphaSoft = state.AlphaSoft;
			AlphaThermal = state.AlphaThermal;
			DebyeMass = state.DebyeMassMeV;
			Sigma = state.Param.SigmaMeV;
			SigmaEff = state.SigmaEffMeV;
			StepSize = state.StepSizeFm;
			StepsPerPeriod = state.WaveVectorFm * state.StepSizeFm;
			Tchem = state.Param.TchemMeV;
			Tcrit = state.Param.TcritMeV;
			Trials = state.Trials;
			WaveVector = state.WaveVectorFm;

			if(state is QQBoundState)
			{
				QQBoundState boundState = state as QQBoundState;
				NumberExtrema = boundState.NumberExtrema;
				BoundMass = boundState.BoundMassMeV;
				AvInvRadius = boundState.RadiusExpectationValue(-1);
				RMS = Math.Sqrt(boundState.RadiusExpectationValue(2));
				AlphaUltraSoft = boundState.AlphaUltraSoft;
				UltraSoftScale = boundState.UltraSoftScaleMeV;
			}
		}

		private QQStateParam GetQQStateParam()
		{
			QQStateParam param = new QQStateParam();
			param.AccuracyAlpha = AccuracyAlpha;
			param.AccuracyWaveFunction = AccuracyWaveFunction;
			param.AggressivenessAlpha = AggressivenessAlpha;
			param.ColorState = ColorState;
			param.EnergyMeV = Energy;
			param.GammaDampMeV = GammaDamp;
			param.MaxRadiusFm = MaxRadius;
			param.MaxShootingTrials = MaxShootingTrials;
			param.PotentialType = PotentialType;
			param.QuantumNumberL = QuantumNumberL;
			param.QuarkMassMeV = QuarkMass;
			param.RunningCouplingType = RunningCouplingType;
			param.SigmaMeV = Sigma;
			param.SoftScaleMeV = SoftScale;
			param.SpinCouplingRangeFm = SpinCouplingRange;
			param.SpinCouplingStrengthMeV = SpinCouplingStrength;
			param.SpinState = SpinState;
			param.StepNumber = StepNumber;
			param.TchemMeV = Tchem;
			param.TcritMeV = Tcrit;
			param.TemperatureMeV = Temperature;

			return param;
		}

		private void CompareResultsWithArchivedData(
			double debyeMass,
			double rMS,
			double softScale,
			double ultraSoftScale,
			double boundMass,
			double energy,
			double gammaDamp,
			double gammaDiss,
			double gammaTot
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
				DebyeMass.ToString("G6"),
				RMS.ToString("G6"),
				SoftScale.ToString("G6"),
				UltraSoftScale.ToString("G6"),
				BoundMass.ToString("G6"),
				Energy.ToString("G6"),
				GammaDamp.ToString("G6"),
				GammaDiss.ToString("G6"),
				GammaTot.ToString("G6")));
			LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
				"Archived:",
				debyeMass.ToString("G6"),
				rMS.ToString("G6"),
				softScale.ToString("G6"),
				ultraSoftScale.ToString("G6"),
				boundMass.ToString("G6"),
				energy.ToString("G6"),
				gammaDamp.ToString("G6"),
				gammaDiss.ToString("G6"),
				gammaTot.ToString("G6")));
			LogMessages.AppendLine(string.Format("{0,-12}{1,-12}{2,-12}{3,-12}{4,-12}{5,-12}{6,-12}{7,-12}{8,-12}{9,-12}",
				"Deviation:",
				(DebyeMass / debyeMass - 1).ToString("G3"),
				(RMS / rMS - 1).ToString("G3"),
				(SoftScale / softScale - 1).ToString("G3"),
				(UltraSoftScale / ultraSoftScale - 1).ToString("G3"),
				(BoundMass / boundMass - 1).ToString("G3"),
				(Energy / energy - 1).ToString("G3"),
				(GammaDamp / gammaDamp - 1).ToString("G3"),
				(GammaDiss / gammaDiss - 1).ToString("G3"),
				(GammaTot / gammaTot - 1).ToString("G3")));
			LogMessages.AppendLine();
			LogMessages.AppendLine();
		}

		private void GetArchivedValues(
			out double debyeMass,
			out double rMS,
			out double softScale,
			out double ultraSoftScale,
			out double boundMass,
			out double energy,
			out double gammaDamp,
			out double gammaDiss,
			out double gammaTot
			)
		{
			string pathFile = GetQQDataPathFile();

			debyeMass = QQDataDoc.GetValue(pathFile, QQDataColumns.DebyeMass,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			rMS = QQDataDoc.GetValue(pathFile, QQDataColumns.RMS,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			softScale = QQDataDoc.GetValue(pathFile, QQDataColumns.SoftScale,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			ultraSoftScale = QQDataDoc.GetValue(pathFile, QQDataColumns.UltraSoftScale,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			boundMass = QQDataDoc.GetValue(pathFile, QQDataColumns.BoundMass,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			energy = QQDataDoc.GetValue(pathFile, QQDataColumns.Energy,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			gammaDamp = QQDataDoc.GetValue(pathFile, QQDataColumns.GammaDamp,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			gammaDiss = QQDataDoc.GetValue(pathFile, QQDataColumns.GammaDiss,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
			gammaTot = QQDataDoc.GetValue(pathFile, QQDataColumns.GammaTot,
				QuantumNumberN, QuantumNumberL, ColorState.ToString(), PotentialType.ToString(),
				Temperature);
		}

		protected override string LogHeader
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(base.LogHeader);
				AppendLogHeaderLine(stringBuilder, "ColorState", ColorState);
				AppendLogHeaderLine(stringBuilder, "SpinState", SpinState);
				AppendLogHeaderLine(stringBuilder, "PotentialType", PotentialType);
				AppendLogHeaderLine(stringBuilder, "RunningCouplingType", RunningCouplingType);
				AppendLogHeaderLine(stringBuilder, "AccuracyAlpha", AccuracyAlpha);
				AppendLogHeaderLine(stringBuilder, "AccuracyWaveFunction", AccuracyWaveFunction);
				AppendLogHeaderLine(stringBuilder, "AggressivenessAlpha", AggressivenessAlpha);
				AppendLogHeaderLine(stringBuilder, "BoundMass", BoundMass);
				AppendLogHeaderLine(stringBuilder, "MaxEnergy", MaxEnergy);
				AppendLogHeaderLine(stringBuilder, "Energy", Energy);
				AppendLogHeaderLine(stringBuilder, "EnergySteps", EnergySteps);
				AppendLogHeaderLine(stringBuilder, "GammaDamp", GammaDamp);
				AppendLogHeaderLine(stringBuilder, "QuantumNumberL", QuantumNumberL);
				AppendLogHeaderLine(stringBuilder, "MaxShootingTrials", MaxShootingTrials);
				AppendLogHeaderLine(stringBuilder, "QuantumNumberN", QuantumNumberN);
				AppendLogHeaderLine(stringBuilder, "QuarkMass", QuarkMass);
				AppendLogHeaderLine(stringBuilder, "MaxRadius", MaxRadius);
				AppendLogHeaderLine(stringBuilder, "StepNumber", StepNumber);
				AppendLogHeaderLine(stringBuilder, "SoftScale", SoftScale);
				AppendLogHeaderLine(stringBuilder, "SpinCouplingStrength", SpinCouplingStrength);
				AppendLogHeaderLine(stringBuilder, "SpinCouplingRange", SpinCouplingRange);
				AppendLogHeaderLine(stringBuilder, "Temperature", Temperature);
				AppendLogHeaderLine(stringBuilder, "Tchem", Tchem);
				AppendLogHeaderLine(stringBuilder, "Tcrit", Tcrit);
				AppendLogHeaderLine(stringBuilder, "Outfile", Outfile);
				AppendLogHeaderLine(stringBuilder, "AlphaHard", AlphaHard);
				AppendLogHeaderLine(stringBuilder, "AlphaSoft", AlphaSoft);
				AppendLogHeaderLine(stringBuilder, "AlphaThermal", AlphaThermal);
				AppendLogHeaderLine(stringBuilder, "AlphaUltraSoft", AlphaUltraSoft);
				AppendLogHeaderLine(stringBuilder, "DebyeMass", DebyeMass);
				AppendLogHeaderLine(stringBuilder, "StepSize", StepSize);
				AppendLogHeaderLine(stringBuilder, "GammaDiss", GammaDiss);
				AppendLogHeaderLine(stringBuilder, "GammaTot", GammaTot);
				AppendLogHeaderLine(stringBuilder, "WaveVector", WaveVector);
				AppendLogHeaderLine(stringBuilder, "StepsPerPeriod", StepsPerPeriod);
				AppendLogHeaderLine(stringBuilder, "NumberExtrema", NumberExtrema);
				AppendLogHeaderLine(stringBuilder, "AvInvRadius", AvInvRadius);
				AppendLogHeaderLine(stringBuilder, "RMS", RMS);
				AppendLogHeaderLine(stringBuilder, "Sigma", Sigma);
				AppendLogHeaderLine(stringBuilder, "SigmaEff", SigmaEff);
				AppendLogHeaderLine(stringBuilder, "Trials", Trials);
				AppendLogHeaderLine(stringBuilder, "UltraSoftScale", UltraSoftScale);

				return stringBuilder.ToString();
			}
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