/**************************************************************************************************
 * The class QQonFire is a WorkerApp and performs numerical calculations concerned with a given
 * quarkonium distribution within the QGP formed in the fireball in relativistic heavy ion
 * collisions. It includes the class Fireball and is capable of performing two-dimensional perfect
 * fluid-hydrodynamics of a transverse expanding medium in a Bjorken-spacetime. QQonFire contains
 * methods for the calculation of centrality bin-boundaries, nuclear modification factors (RAA),
 * snapshots of the fireball evolution and decay cascade calculations within the bottomium family.
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Yburn.Fireball;
using Yburn.QQState;
using Yburn.Util;

namespace Yburn.Workers
{
	public class QQonFire : Worker
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static string[] BinBoundsStatusTitles
		{
			get
			{
				return new string[] { "b", "Ncoll", "Npart", "dSigma/db", "Sigma" };
			}
		}

		public static string[] DirectPionDecayWidthsStatusTitles
		{
			get
			{
				return new string[] { "b", "Ncoll", "NcollQGP", "NcollPion" };
			}
		}

		public static string[] SnapshotStatusTitles
		{
			get
			{
				return new string[] { "Time" };
			}
		}

		public static string[] SuppressionStatusTitles
		{
			get
			{
				return new string[] { "ImpactParam (fm)", "InitialCentralTemperature (MeV)", "LifeTime (fm/c)" };
			}
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQonFire()
			: base()
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		protected override void StartJob(
			string jobId
			)
		{
			switch(jobId)
			{
				case "CalculateBinBoundaries":
					CalculateBinBoundaries();
					break;

				case "CalculateDirectPionDecayWidths":
					CalculateDirectPionDecayWidths();
					break;

				case "MakeSnapshots":
					MakeSnapshots();
					break;

				case "ShowBranchingRatio":
					ShowBranchingRatioMatrix();
					break;

				case "ShowCumulativeMatrix":
					ShowCumulativeMatrix();
					break;

				case "ShowInverseCumulativeMatrix":
					ShowInverseCumulativeMatrix();
					break;

				case "ShowY1SFeedDown":
					ShowY1SFeedDown();
					break;

				case "ShowDecayWidthInput":
					ShowDecayWidthInput();
					break;

				case "ShowInitialPopulations":
					ShowInitialPopulations();
					break;

				case "ShowProtonProtonYields":
					ShowProtonProtonYields();
					break;

				case "ShowSnapsX":
					ShowSnapsX();
					break;

				case "ShowSnapsXY":
					ShowSnapsXY();
					break;

				case "ShowSnapsY":
					ShowSnapsY();
					break;

				case "CalculateSuppression":
					CalculateSuppression();
					break;

				default:
					throw new InvalidJobException(jobId);
			}
		}

		public void CalculateDirectPionDecayWidths()
		{
			PrepareJob("CalculateDirectPionDecayWidths", DirectPionDecayWidthsStatusTitles);

			List<double> impactParams = new List<double>();
			List<double> nCollQGPs = new List<double>();
			List<double> nCollPions = new List<double>();
			List<double> nColls = new List<double>();
			int step = 0;
			do
			{
				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					LogMessages.Clear();
					LogMessages.Append(LogHeader + "#\r\n#\r\n" + LogFooter);
					return;
				}

				impactParams.Add(step * GridCellSize);

				using(Fireball.Fireball fireball = CreateFireballToCalcDirectPionDecayWidth(
					impactParams[step]))
				{
					// Set BjorkenLifeTime for the LogHeader
					if(step == 0)
					{
						BjorkenLifeTime = fireball.BjorkenLifeTime;
					}

					// calculate the areas
					double nCollQGP;
					double nCollPion;
					fireball.CalculateNcolls(MinimalCentralTemperature, out nCollQGP, out nCollPion);
					nCollQGPs.Add(nCollQGP);
					nCollPions.Add(nCollPion);
					nColls.Add(fireball.IntegrateFireballField("Ncoll"));

					StatusValues[0] = impactParams[step].ToString("G3");
					StatusValues[1] = nColls[step].ToString("G4");
					StatusValues[2] = nCollQGPs[step].ToString("G4");
					StatusValues[3] = nCollPions[step].ToString("G4");
				}

				step++;
			}
			while((step == 1 ? true : nColls[step - 1] > 1));

			// quit here if process has been aborted
			if(JobCancelToken.IsCancellationRequested)
			{
				LogMessages.Clear();
				LogMessages.Append(LogHeader + "#\r\n#\r\n" + LogFooter);
				return;
			}

			// prepare output
			LogMessages.Clear();
			LogMessages.Append(LogHeader);
			LogMessages.AppendFormat("#\r\n#\r\n#{0,5}{1,12}{2,12}{3,12}\r\n",
				"b", "Ncoll", "NcollQGP", "NcollPion");
			LogMessages.AppendFormat("#{0,5}{1,12}{2,12}{3,12}\r\n\r\n",
				"(fm)", "", "", "");
			for(int i = 0; i < impactParams.Count; i++)
			{
				LogMessages.AppendLine(string.Format("{0,6}{1,12}{2,12}{3,12}",
					impactParams[i].ToString("G3"),
					nColls[i].ToString("G4"),
					nCollQGPs[i].ToString("G4"),
					nCollPions[i].ToString("G4")));
			}

			LogMessages.Append(LogFooter);

			File.WriteAllText(YburnConfigFile.OutputPath + Outfile, LogMessages.ToString());
		}

		public void CalculateBinBoundaries()
		{
			PrepareJob("CalculateBinBoundaries", BinBoundsStatusTitles);

			List<double> impactParams;
			List<double> nColls;
			List<double> nParts;
			List<double> dSigmadbs;
			List<double> sigmas;
			CalculateBinBoundaries(out impactParams, out nColls, out nParts, out dSigmadbs, out sigmas);

			if(JobCancelToken.IsCancellationRequested)
			{
				LogMessages.Clear();
				LogMessages.Append(LogHeader + "#\r\n#\r\n" + LogFooter);
				return;
			}

			// prepare output
			LogMessages.Clear();
			LogMessages.Append(LogHeader);
			LogMessages.AppendFormat("#\r\n#\r\n#{0,5}{1,12}{2,12}{3,12}{4,12}\r\n",
				"b", "Ncoll", "Npart", "dSigma/db", "Sigma");
			LogMessages.AppendFormat("#{0,5}{1,12}{2,12}{3,12}{4,12}\r\n",
				"(fm)", "", "", "(fm)", "(fm^2)");
			for(int i = 0; i < impactParams.Count; i++)
			{
				LogMessages.AppendLine(string.Format("{0,6}{1,12}{2,12}{3,12}{4,12}",
					impactParams[i].ToString("G3"),
					nColls[i].ToString("G4"),
					nParts[i].ToString("G4"),
					dSigmadbs[i].ToString("G4"),
					sigmas[i].ToString("G4")));
			}

			LogMessages.AppendLine(string.Format("\r\n\r\n#{0,11}{1,19}{2,12}",
				"Centrality",
				"Bin size",
				"<Npart>"));
			for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Length; binGroupIndex++)
			{
				LogMessages.AppendLine("#");
				for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
				{
					LogMessages.AppendLine(string.Format("#{0,11}{1,8} < b < {2,4}{3,12}",
						CentralityBinStrings[binGroupIndex][binIndex],
						ImpactParamsAtBinBoundaries[binGroupIndex][binIndex].ToString("G4"),
						ImpactParamsAtBinBoundaries[binGroupIndex][binIndex + 1].ToString("G4"),
						MeanParticipantsInBin[binGroupIndex][binIndex].ToString("G4")));
				}
			}

			LogMessages.Append(LogFooter);

			File.WriteAllText(YburnConfigFile.OutputPath + Outfile, LogMessages.ToString());
		}

		private void CalculateBinBoundaries(
			out List<double> impactParams,
			out List<double> nColls,
			out List<double> nParts,
			out List<double> dSigmadbs,
			out List<double> sigmas
				)
		{
			BinBoundaryCalculator calculator = new BinBoundaryCalculator(CreateFireballParam(),
				JobCancelToken);
			calculator.StatusValues = StatusValues;
			calculator.Calculate(CentralityBinBoundaries);

			impactParams = calculator.ImpactParams;
			nColls = calculator.Ncolls;
			nParts = calculator.Nparts;
			dSigmadbs = calculator.DSigmaDbs;
			sigmas = calculator.Sigmas;

			NumberCentralityBins = calculator.NumberCentralityBins;
			CentralityBinStrings = GetCentralityBinStrings();
			ImpactParamsAtBinBoundaries = calculator.ImpactParamsAtBinBoundaries;
			ParticipantsAtBinBoundaries = calculator.ParticipantsAtBinBoundaries;
			MeanParticipantsInBin = calculator.MeanParticipantsInBin;
		}

		public void CalculateSuppression()
		{
			PrepareJob("CalculateSuppression", BinBoundsStatusTitles);

			List<double> impactParams;
			List<double> nColls;
			List<double> nParts;
			List<double> dSigmadbs;
			List<double> sigmas;
			CalculateBinBoundaries(out impactParams, out nColls, out nParts, out dSigmadbs, out sigmas);

			SetStatusVariables(SuppressionStatusTitles);
			DetermineMaxLifeTime();

			double[][][][] qgpSuppressionFactors = CalculateQGPSuppressionFactors();

			// quit here if process has been aborted
			if(JobCancelToken.IsCancellationRequested)
			{
				LogMessages.Clear();
				LogMessages.Append(LogHeader + "#\r\n#\r\n" + LogFooter);
				return;
			}

			// The preliminary suppression factors have been calculated. Now come the final suppression factors
			// and output to LogStream.
			StringBuilder results = new StringBuilder();

			results.AppendFormat("#Preliminary suppression factors:\r\n#\r\n#{0,11}{1,12}{2,12}",
				"Centrality",
				"<Npart>",
				"pT (GeV/c)");
			foreach(string sStateName in Enum.GetNames(typeof(BottomiumState)))
			{
				results.AppendFormat("{0,12}", string.Format("RAAQGP({0})", sStateName));
			}

			results.Append("\r\n#\r\n");

			for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Length; binGroupIndex++)
			{
				for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
				{
					for(int pTIndex = 0; pTIndex < TransverseMomenta.Length; pTIndex++)
					{
						results.AppendFormat("{0,12}{1,12}{2,12}",
							CentralityBinStrings[binGroupIndex][binIndex],
							MeanParticipantsInBin[binGroupIndex][binIndex].ToString("G4"),
							TransverseMomenta[pTIndex].ToString("G4"));

						for(int stateIndex = 0; stateIndex < NumberBottomiumStates; stateIndex++)
						{
							results.AppendFormat("{0,12}",
								qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex][stateIndex].ToString("G3"));
						}

						results.AppendLine();
					}

					results.AppendFormat("{0,12}{1,12}{2,12}",
						CentralityBinStrings[binGroupIndex][binIndex],
						MeanParticipantsInBin[binGroupIndex][binIndex].ToString("G4"),
						"<pT>");

					foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
					{
						double[] rAAQGPsBinValues = new double[TransverseMomenta.Length];
						for(int pTIndex = 0; pTIndex < TransverseMomenta.Length; pTIndex++)
						{
							rAAQGPsBinValues[pTIndex] = qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex][(int)state];
						}

						results.AppendFormat("{0,12}",
							TransverseMomentumAverager.Calculate(state, TransverseMomenta, rAAQGPsBinValues)
							.ToString("G3"));
					}

					results.AppendLine();
				}

				if(binGroupIndex < NumberCentralityBins.Length - 1)
				{
					results.AppendLine();
				}
			}

			results.AppendFormat(
				"\r\n\r\n#Final suppression factors:\r\n#\r\n#{0,11}{1,12}{2,12}{3,15}{4,15}{5,15}{6,15}{7,15}\r\n#\r\n",
				"Centrality",
				"<Npart>",
				"pT (GeV/c)",
				"RAA(Y1S)",
				"RAA(Y2S)",
				"RAA(Y3S)",
				"(2S/1S)PbPb-pp",
				"(3S/1S)PbPb-pp");

			// calculate final suppression factors
			double[][] rAAs = new double[TransverseMomenta.Length][];
			// run through the centrality bin groups
			for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Length; binGroupIndex++)
			{
				// run through the centrality bins
				for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
				{
					// run through the pT bins
					for(int pTIndex = 0; pTIndex < TransverseMomenta.Length; pTIndex++)
					{
						rAAs[pTIndex] = CalculateFullSuppressionFactors(qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex]);

						results.AppendFormat("{0,12}{1,12}{2,12}",
							CentralityBinStrings[binGroupIndex][binIndex],
							MeanParticipantsInBin[binGroupIndex][binIndex].ToString("G4"),
							TransverseMomenta[pTIndex].ToString("G4"));

						for(int l = 0; l < 5; l++)
						{
							results.AppendFormat("{0,15}",
								rAAs[pTIndex][l].ToString("G3"));
						}

						results.AppendLine();
					}

					results.AppendFormat("{0,12}{1,12}{2,12}",
						CentralityBinStrings[binGroupIndex][binIndex],
						MeanParticipantsInBin[binGroupIndex][binIndex].ToString("G4"),
						"<pT>");

					// average of all pT values
					for(int l = 0; l < 5; l++)
					{
						double[] rAABinValues = new double[TransverseMomenta.Length];
						for(int pTIndex = 0; pTIndex < TransverseMomenta.Length; pTIndex++)
						{
							rAABinValues[pTIndex] = rAAs[pTIndex][l];
						}

						results.AppendFormat("{0,15}", TransverseMomentumAverager.Calculate((BottomiumState)l, TransverseMomenta, rAABinValues)
							.ToString("G3"));
					}

					results.AppendLine();
				}

				if(binGroupIndex < NumberCentralityBins.Length - 1)
				{
					results.AppendLine();
				}
			}

			// store information in LogString, print it out and save it to file
			string logString = LogHeader + "\r\n\r\n" + results.ToString() + LogFooter;

			File.WriteAllText(YburnConfigFile.OutputPath + Outfile, logString);

			LogMessages.Clear();
			LogMessages.Append(logString);
		}

		public void ShowBranchingRatioMatrix()
		{
			CurrentJobTitle = "ShowBranchingRatioMatrix";

			LogMessages.Clear();
			LogMessages.AppendFormat("Branching ratio matrix:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetBranchingRatioMatrixString());
		}

		public void ShowCumulativeMatrix()
		{
			CurrentJobTitle = "ShowCumulativeMatrix";

			LogMessages.Clear();
			LogMessages.AppendFormat("Cumulative matrix:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetCumulativeMatrixString());
		}

		public void ShowInverseCumulativeMatrix()
		{
			CurrentJobTitle = "ShowInverseCumulativeMatrix";

			LogMessages.Clear();
			LogMessages.AppendFormat("Inverse cumulative matrix:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetInverseCumulativeMatrixString());
		}

		public void ShowDecayWidthInput()
		{
			CurrentJobTitle = "ShowDecayWidthInput";

			List<KeyValuePair<double, double>>[] tGammaList = TemperatureDecayWidthList.GetList(
				GetQQDataPathFile(), DecayWidthType, PotentialTypes);

			LogMessages.Clear();
			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				LogMessages.AppendFormat("{0,16}", state.ToString());
			}

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				LogMessages.AppendFormat("{0,6}{1,10}", "T", DecayWidthType);
			}

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			for(int i = 0; i < tGammaList[0].Count; i++)
			{
				foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
				{
					if(i < tGammaList[(int)state].Count)
					{
						LogMessages.AppendFormat("{0,6}{1,10}",
							tGammaList[(int)state][i].Key.ToString("G4"),
							tGammaList[(int)state][i].Value.ToString("G4"));
					}
					else
					{
						LogMessages.AppendFormat("{0,16}", "");
					}
				}

				LogMessages.AppendLine();
			}

			LogMessages.AppendLine();
			LogMessages.AppendLine();
		}

		public void ShowInitialPopulations()
		{
			CurrentJobTitle = "ShowInitialPopulations";

			LogMessages.Clear();
			LogMessages.AppendFormat("Initial populations:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetInitialPopulationsString(GetProtonProtonYields()));
		}

		public void ShowProtonProtonYields()
		{
			CurrentJobTitle = "ShowProtonProtonYields";

			LogMessages.Clear();
			LogMessages.AppendFormat("pp yields:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetProtonProtonYieldsString(GetProtonProtonYields()));
		}

		public void ShowSnapsX()
		{
			Process.Start("wgnuplot", "--persist \""
				+ BuildPlotPathFile(YburnConfigFile.OutputPath + Outfile) + "-plotX.plt\"");
		}

		public void ShowSnapsY()
		{
			Process.Start("wgnuplot", "--persist \""
				+ BuildPlotPathFile(YburnConfigFile.OutputPath + Outfile) + "-plotY.plt\"");
		}

		public void ShowSnapsXY()
		{
			Process.Start("wgnuplot", "--persist \""
				+ BuildPlotPathFile(YburnConfigFile.OutputPath + Outfile) + "-plotXY.plt\"");
		}

		public void ShowY1SFeedDown()
		{
			CurrentJobTitle = "ShowY1SFeedDown";

			LogMessages.Clear();
			LogMessages.AppendFormat("Y1S feed down:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetY1SFeedDownString(GetProtonProtonYields()));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string FtexsLogPathFile
		{
			get
			{
				return YburnConfigFile.OutputPath + "FtexsLogFile.txt";
			}
		}

		private static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

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
			else if(enumName == "ShapeFunction")
			{
				return typeof(ShapeFunctionType);
			}
			else if(enumName == "DecayWidthType")
			{
				return typeof(DecayWidthType);
			}
			else if(enumName == "DecayWidthEvaluationType")
			{
				return typeof(DecayWidthEvaluationType);
			}
			else if(enumName == "ExpansionMode")
			{
				return typeof(ExpansionMode);
			}
			else if(enumName == "FireballFieldType")
			{
				return typeof(FireballFieldType);
			}
			else if(enumName == "PotentialType")
			{
				return typeof(PotentialType);
			}
			else if(enumName == "TemperatureProfile")
			{
				return typeof(TemperatureProfile);
			}
			else if(enumName == "ProtonProtonBaseline")
			{
				return typeof(ProtonProtonBaseline);
			}
			else
			{
				throw new Exception("Invalid enum name \"" + enumName + "\".");
			}
		}

		private double BjorkenLifeTime;

		private DecayWidthType DecayWidthType;

		private double DiffusenessA;

		private double DiffusenessB;

		private ExpansionMode ExpansionMode;

		protected double FeedDown3P;

		private string[] FireballFieldTypes = new string[0];

		private double ImpactParam;

		private double LifeTime;

		private int NucleonNumberA;

		private int NucleonNumberB;

		private string Outfile = "stdout.txt";

		private string[] PotentialTypes = new string[0];

		protected ProtonProtonBaseline ProtonProtonBaseline;

		private TemperatureProfile TemperatureProfile;

		private double[] TransverseMomenta = new double[0];

		private double NuclearRadiusA;

		private double NuclearRadiusB;

		private DecayWidthEvaluationType DecayWidthEvaluationType;

		private double[] DecayWidthAveragingAngles = new double[0];

		private double SnapRate;

		private int[][] CentralityBinBoundaries = new int[0][];

		protected int[] NumberCentralityBins;

		private string[][] CentralityBinStrings;

		private double[][] ImpactParamsAtBinBoundaries = new double[0][];

		private double[][] ParticipantsAtBinBoundaries = new double[0][];

		private double[][] MeanParticipantsInBin = new double[0][];

		private string BottomiumStates = string.Empty;

		private double InitialCentralTemperature;

		private double MinimalCentralTemperature;

		private double[] FormationTimes = new double[0];

		private double ThermalTime;

		private double GridCellSize;

		private double GridRadius
		{
			get
			{
				return NumberGridPoints * GridCellSize;
			}
			set
			{
				NumberGridPoints = Convert.ToInt32(Math.Round(value / GridCellSize));
			}
		}

		private int NumberGridPoints;

		private double BeamRapidity;

		private ShapeFunctionType ShapeFunctionTypeA;

		private ShapeFunctionType ShapeFunctionTypeB;

		private Fireball.Fireball CreateFireball()
		{
			return new Fireball.Fireball(CreateFireballParam());
		}

		private Fireball.Fireball CreateFireballToCalcDirectPionDecayWidth(
			double impactParam
			)
		{
			FireballParam param = CreateFireballParam();
			param.ImpactParameterFm = impactParam;
			param.TransverseMomentaGeV = new double[] { 0 };
			param.ExpansionMode = ExpansionMode.Longitudinal;

			return new Fireball.Fireball(param);
		}

		private Fireball.Fireball CreateFireballToDetermineMaxLifeTime()
		{
			FireballParam param = CreateFireballParam();
			param.ImpactParameterFm = 0;
			param.TransverseMomentaGeV = new double[] { 0 };

			return new Fireball.Fireball(param);
		}

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.NucleonNumberA = NucleonNumberA;
			param.DiffusenessAFm = DiffusenessA;
			param.NuclearRadiusAFm = NuclearRadiusA;
			param.NucleonNumberB = NucleonNumberB;
			param.DiffusenessBFm = DiffusenessB;
			param.NuclearRadiusBFm = NuclearRadiusB;
			param.GridCellSizeFm = GridCellSize;
			param.NumberGridPoints = NumberGridPoints;
			param.ImpactParameterFm = ImpactParam;
			param.ThermalTimeFm = ThermalTime;
			param.FormationTimesFm = FormationTimes;
			param.InitialCentralTemperatureMeV = InitialCentralTemperature;
			param.MinimalCentralTemperatureMeV = MinimalCentralTemperature;
			param.BeamRapidity = BeamRapidity;
			param.TransverseMomentaGeV = TransverseMomenta;
			param.DecayWidthEvaluationType = DecayWidthEvaluationType;
			param.ExpansionMode = ExpansionMode;
			param.TemperatureProfile = TemperatureProfile;
			param.FtexsLogPathFile = FtexsLogPathFile;
			param.DecayWidthAveragingAngles = DecayWidthAveragingAngles;
			param.TemperatureDecayWidthList = TemperatureDecayWidthList.GetList(
					GetQQDataPathFile(), DecayWidthType, PotentialTypes);
			param.ShapeFunctionTypeA = ShapeFunctionTypeA;
			param.ShapeFunctionTypeB = ShapeFunctionTypeB;

			return param;
		}

		private string BuildPlotPathFile(
			string outPathFile
			)
		{
			int indexOfDot = outPathFile.LastIndexOf(".");
			string tempPathFile = outPathFile.Substring(0, indexOfDot);
			string extension = outPathFile.Substring(indexOfDot, outPathFile.Length - indexOfDot);
			return (tempPathFile + "-b" + ImpactParam + extension).Replace("\\", "/");
		}

		private double[] CalculateFullSuppressionFactors(
			double[] qgpSuppressionFactors
			)
		{
			double[] ppYields = GetProtonProtonYields();
			double[] ppQGPSuppressionFactors = { 1, 1, 1, 1, 1, 1 };

			double ppResult1S = BottomiumCascade.GetDimuonDecays(
				ppYields, ppQGPSuppressionFactors, BottomiumState.Y1S);
			double ppResult2S = BottomiumCascade.GetDimuonDecays(
				ppYields, ppQGPSuppressionFactors, BottomiumState.Y2S);
			double ppResult3S = BottomiumCascade.GetDimuonDecays(
				ppYields, ppQGPSuppressionFactors, BottomiumState.Y3S);

			double heavyIonResult1S = BottomiumCascade.GetDimuonDecays(
				ppYields, qgpSuppressionFactors, BottomiumState.Y1S);
			double heavyIonResult2S = BottomiumCascade.GetDimuonDecays(
				ppYields, qgpSuppressionFactors, BottomiumState.Y2S);
			double heavyIonResult3S = BottomiumCascade.GetDimuonDecays(
				ppYields, qgpSuppressionFactors, BottomiumState.Y3S);

			return new double[] {
				heavyIonResult1S / ppResult1S,
				heavyIonResult2S / ppResult2S,
				heavyIonResult3S / ppResult3S,
				( heavyIonResult2S / heavyIonResult1S ) / ( ppResult2S / ppResult1S ),
				( heavyIonResult3S / heavyIonResult1S ) / ( ppResult3S / ppResult1S )
			};
		}

		protected double[][][][] CalculateQGPSuppressionFactors()
		{
			QGPSuppression qgpSuppression = new QGPSuppression(CreateFireballParam(),
				NumberCentralityBins, ImpactParamsAtBinBoundaries, JobCancelToken);
			qgpSuppression.TrackStatus(StatusValues);

			return qgpSuppression.CalculateQGPSuppressionFactors();
		}

		public void MakeSnapshots()
		{
			PrepareJob("MakeSnapshots", SnapshotStatusTitles);

			if(SnapRate <= 0)
			{
				throw new Exception("SnapRate <= 0.");
			}

			using(Fireball.Fireball fireball = CreateFireball())
			{
				BjorkenLifeTime = fireball.BjorkenLifeTime;

				// extract path and file name of outfile and extension separately
				string pathFile = BuildPlotPathFile(YburnConfigFile.OutputPath + Outfile);

				// All data is saved in the output file. Additionally, the corresponding gnuplot files (.plt)
				// are created to facilitate graphical visualization of the data.
				StringBuilder dataFileString = new StringBuilder();
				StringBuilder gnuFileStringX = new StringBuilder();
				StringBuilder gnuFileStringY = new StringBuilder();
				StringBuilder gnuFileStringXY = new StringBuilder();

				double range = GridCellSize * (NumberGridPoints - 1);
				gnuFileStringX.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\n\r\n",
					range.ToString("G3")));
				gnuFileStringY.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\n\r\n",
					range.ToString("G3")));
				gnuFileStringXY.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\nset yr[0:{1,3}]\r\n\r\n",
					range.ToString("G3"), range.ToString("G3")));

				string xPlotStringBegin = "p \"" + pathFile
					+ "\" every " + NumberGridPoints.ToString() + " index ";
				string xPlotStringEnd = " u 1:3 w p; pause .5";
				string yPlotStringBegin = "p \"" + pathFile
					+ "\" every ::::" + (NumberGridPoints - 1).ToString() + " index ";
				string yPlotStringEnd = " u 2:3 w p; pause .5";
				string xYPlotStringBegin = "sp \"" + pathFile + "\" index ";
				string xYPlotStringEnd = " u 1:2:3 w p; pause .5";

				int index = 0;
				double dt = 1.0 / SnapRate;
				double currentTime;
				while(fireball.CentralTemperature > MinimalCentralTemperature)
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
					StatusValues[0] = currentTime.ToString("G3");

					dataFileString.AppendLine("\r\n\r\n#Time = "
						+ currentTime.ToString() + ", Index " + index);
					dataFileString.Append(fireball.FieldsToString(FireballFieldTypes, BottomiumStates));

					gnuFileStringX.AppendLine(xPlotStringBegin + index + xPlotStringEnd);
					gnuFileStringY.AppendLine(yPlotStringBegin + index + yPlotStringEnd);
					gnuFileStringXY.AppendLine(xYPlotStringBegin + index + xYPlotStringEnd);

					index++;
				}

				LifeTime = fireball.LifeTime;

				// append final results in the output file and exchange the old header with a new one
				LogMessages.Clear();
				LogMessages.Append(LogHeader + "#\r\n#\r\n" + LogFooter);
				dataFileString.Append(LogMessages.ToString());
				dataFileString.Insert(0, LogHeader);

				File.WriteAllText(pathFile, dataFileString.ToString());
				File.WriteAllText(pathFile + "-plotX.plt", gnuFileStringX.ToString());
				File.WriteAllText(pathFile + "-plotY.plt", gnuFileStringY.ToString());
				File.WriteAllText(pathFile + "-plotXY.plt", gnuFileStringXY.ToString());
			}
		}

		// get Bjorken- and QGP lifetime for ImpactParam = 0
		private void DetermineMaxLifeTime()
		{
			using(Fireball.Fireball fireball = CreateFireballToDetermineMaxLifeTime())
			{
				// Evolving the fireball to calculate the maximum QGP LifeTime
				while(fireball.CentralTemperature > MinimalCentralTemperature)
				{
					// quit here if process has been aborted
					if(JobCancelToken.IsCancellationRequested)
					{
						return;
					}

					fireball.Advance(0.1);
				}

				LifeTime = fireball.LifeTime;
				BjorkenLifeTime = fireball.BjorkenLifeTime;
			}
		}

		private string[][] GetCentralityBinStrings(
			)
		{
			string[][] centralityBinStrings = new string[CentralityBinBoundaries.Length][];
			for(int i = 0; i < centralityBinStrings.Length; i++)
			{
				string[] centralityBinGroup = new string[CentralityBinBoundaries[i].Length - 1];
				for(int j = 0; j < centralityBinGroup.Length; j++)
				{
					centralityBinGroup[j] = string.Format("{0}-{1}%",
						 CentralityBinBoundaries[i][j].ToString(),
						 CentralityBinBoundaries[i][j + 1].ToString());
				}
				centralityBinStrings[i] = centralityBinGroup;
			}

			return centralityBinStrings;
		}

		protected double[] GetProtonProtonYields()
		{
			switch(ProtonProtonBaseline)
			{
				case ProtonProtonBaseline.CMS2012:
					return new double[] {
						1.0, // Y1S
						0.271, // x1P
						0.56, // Y2S
						0.105, // x2P
						0.41, // Y3S
						FeedDown3P // x3P
					};

				default:
					throw new Exception("Invalid Baseline.");
			}
		}

		protected override void SetVariableNameValuePairs(
			Dictionary<string, string> nameValuePairs
			)
		{
			BeamRapidity = Extractor.TryGetDouble(nameValuePairs, "BeamRapidity", BeamRapidity);
			BjorkenLifeTime = Extractor.TryGetDouble(nameValuePairs, "BjorkenLifeTime", BjorkenLifeTime);
			BottomiumStates = Extractor.TryGetString(nameValuePairs, "BottomiumStates", BottomiumStates);
			CentralityBinBoundaries = Extractor.TryGetIntArrayArray(nameValuePairs, "CentralityBinBoundaries", CentralityBinBoundaries);
			DecayWidthAveragingAngles = Extractor.TryGetDoubleArray(nameValuePairs, "DecayWidthAveragingAngles", DecayWidthAveragingAngles);
			DecayWidthEvaluationType = Extractor.TryGetEnum<DecayWidthEvaluationType>(nameValuePairs, "DecayWidthEvaluationType", DecayWidthEvaluationType);
			DecayWidthType = Extractor.TryGetEnum<DecayWidthType>(nameValuePairs, "DecayWidthType", DecayWidthType);
			DiffusenessA = Extractor.TryGetDouble(nameValuePairs, "DiffusenessA", DiffusenessA);
			DiffusenessB = Extractor.TryGetDouble(nameValuePairs, "DiffusenessB", DiffusenessB);
			ExpansionMode = Extractor.TryGetEnum<ExpansionMode>(nameValuePairs, "ExpansionMode", ExpansionMode);
			FeedDown3P = Extractor.TryGetDouble(nameValuePairs, "FeedDown3P", FeedDown3P);
			FireballFieldTypes = Extractor.TryGetStringArray(nameValuePairs, "FireballFieldTypes", FireballFieldTypes);
			FormationTimes = Extractor.TryGetDoubleArray(nameValuePairs, "FormationTimes", FormationTimes);
			GridCellSize = Extractor.TryGetDouble(nameValuePairs, "GridCellSize", GridCellSize);
			GridRadius = Extractor.TryGetDouble(nameValuePairs, "GridRadius", GridRadius);
			ImpactParam = Extractor.TryGetDouble(nameValuePairs, "ImpactParam", ImpactParam);
			ImpactParamsAtBinBoundaries = Extractor.TryGetDoubleArrayArray(nameValuePairs, "ImpactParamsAtBinBoundaries", ImpactParamsAtBinBoundaries);
			InitialCentralTemperature = Extractor.TryGetDouble(nameValuePairs, "InitialCentralTemperature", InitialCentralTemperature);
			LifeTime = Extractor.TryGetDouble(nameValuePairs, "LifeTime", LifeTime);
			MeanParticipantsInBin = Extractor.TryGetDoubleArrayArray(nameValuePairs, "MeanParticipantsInBin", MeanParticipantsInBin);
			MinimalCentralTemperature = Extractor.TryGetDouble(nameValuePairs, "MinimalCentralTemperature", MinimalCentralTemperature);
			NuclearRadiusA = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusA", NuclearRadiusA);
			NuclearRadiusB = Extractor.TryGetDouble(nameValuePairs, "NuclearRadiusB", NuclearRadiusB);
			NucleonNumberA = Extractor.TryGetInt(nameValuePairs, "NucleonNumberA", NucleonNumberA);
			NucleonNumberB = Extractor.TryGetInt(nameValuePairs, "NucleonNumberB", NucleonNumberB);
			Outfile = Extractor.TryGetString(nameValuePairs, "Outfile", Outfile);
			ParticipantsAtBinBoundaries = Extractor.TryGetDoubleArrayArray(nameValuePairs, "ParticipantsAtBinBoundaries", ParticipantsAtBinBoundaries);
			PotentialTypes = Extractor.TryGetStringArray(nameValuePairs, "PotentialTypes", PotentialTypes);
			ProtonProtonBaseline = Extractor.TryGetEnum<ProtonProtonBaseline>(nameValuePairs, "ProtonProtonBaseline", ProtonProtonBaseline);
			ShapeFunctionTypeA = Extractor.TryGetEnum<ShapeFunctionType>(nameValuePairs, "ShapeFunctionTypeA", ShapeFunctionTypeA);
			ShapeFunctionTypeB = Extractor.TryGetEnum<ShapeFunctionType>(nameValuePairs, "ShapeFunctionTypeB", ShapeFunctionTypeB);
			SnapRate = Extractor.TryGetDouble(nameValuePairs, "SnapRate", SnapRate);
			TemperatureProfile = Extractor.TryGetEnum<TemperatureProfile>(nameValuePairs, "TemperatureProfile", TemperatureProfile);
			ThermalTime = Extractor.TryGetDouble(nameValuePairs, "ThermalTime", ThermalTime);
			TransverseMomenta = Extractor.TryGetDoubleArray(nameValuePairs, "TransverseMomenta", TransverseMomenta);
		}

		protected override Dictionary<string, string> GetVariableNameValuePairs()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs["BeamRapidity"] = BeamRapidity.ToString();
			nameValuePairs["BjorkenLifeTime"] = BjorkenLifeTime.ToString();
			nameValuePairs["BottomiumStates"] = BottomiumStates;
			nameValuePairs["CentralityBinBoundaries"] = CentralityBinBoundaries.ToStringifiedList();
			nameValuePairs["DecayWidthAveragingAngles"] = DecayWidthAveragingAngles.ToStringifiedList();
			nameValuePairs["DecayWidthEvaluationType"] = DecayWidthEvaluationType.ToString();
			nameValuePairs["DecayWidthType"] = DecayWidthType.ToString();
			nameValuePairs["DiffusenessA"] = DiffusenessA.ToString();
			nameValuePairs["DiffusenessB"] = DiffusenessB.ToString();
			nameValuePairs["ExpansionMode"] = ExpansionMode.ToString();
			nameValuePairs["FeedDown3P"] = FeedDown3P.ToString();
			nameValuePairs["FireballFieldTypes"] = FireballFieldTypes.ToStringifiedList();
			nameValuePairs["FormationTimes"] = FormationTimes.ToStringifiedList();
			nameValuePairs["GridCellSize"] = GridCellSize.ToString();
			nameValuePairs["GridRadius"] = GridRadius.ToString();
			nameValuePairs["ImpactParam"] = ImpactParam.ToString();
			nameValuePairs["ImpactParamsAtBinBoundaries"] = ImpactParamsAtBinBoundaries.ToStringifiedList();
			nameValuePairs["InitialCentralTemperature"] = InitialCentralTemperature.ToString();
			nameValuePairs["LifeTime"] = LifeTime.ToString();
			nameValuePairs["MeanParticipantsInBin"] = MeanParticipantsInBin.ToStringifiedList();
			nameValuePairs["MinimalCentralTemperature"] = MinimalCentralTemperature.ToString();
			nameValuePairs["NuclearRadiusA"] = NuclearRadiusA.ToString();
			nameValuePairs["NuclearRadiusB"] = NuclearRadiusB.ToString();
			nameValuePairs["NucleonNumberA"] = NucleonNumberA.ToString();
			nameValuePairs["NucleonNumberB"] = NucleonNumberB.ToString();
			nameValuePairs["Outfile"] = Outfile;
			nameValuePairs["ParticipantsAtBinBoundaries"] = ParticipantsAtBinBoundaries.ToStringifiedList();
			nameValuePairs["PotentialTypes"] = PotentialTypes.ToStringifiedList();
			nameValuePairs["ProtonProtonBaseline"] = ProtonProtonBaseline.ToString();
			nameValuePairs["ShapeFunctionTypeA"] = ShapeFunctionTypeA.ToString();
			nameValuePairs["ShapeFunctionTypeB"] = ShapeFunctionTypeB.ToString();
			nameValuePairs["SnapRate"] = SnapRate.ToString();
			nameValuePairs["TemperatureProfile"] = TemperatureProfile.ToString();
			nameValuePairs["ThermalTime"] = ThermalTime.ToString();
			nameValuePairs["TransverseMomenta"] = TransverseMomenta.ToStringifiedList();

			return nameValuePairs;
		}
	}
}