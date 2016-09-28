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

namespace Yburn.Workers
{
	public partial class QQonFire : Worker
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
				return new string[] { "ImpactParameter (fm)", "InitialMaximumTemperature (MeV)", "LifeTime (fm/c)" };
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

				case "ShowInitialQQPopulations":
					ShowInitialQQPopulations();
					break;

				case "ShowProtonProtonDimuonDecays":
					ShowProtonProtonDimuonDecays();
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

				Fireball.Fireball fireball = CreateFireballToCalcDirectPionDecayWidth(
					impactParams[step]);

				// Set BjorkenLifeTime for the LogHeader
				if(step == 0)
				{
					BjorkenLifeTime = fireball.BjorkenLifeTime;
				}

				// calculate the areas
				double nCollQGP;
				double nCollPion;
				fireball.CalculateNcolls(BreakupTemperature, out nCollQGP, out nCollPion);
				nCollQGPs.Add(nCollQGP);
				nCollPions.Add(nCollPion);
				nColls.Add(fireball.IntegrateFireballField("Ncoll"));

				StatusValues[0] = impactParams[step].ToString("G3");
				StatusValues[1] = nColls[step].ToString("G4");
				StatusValues[2] = nCollQGPs[step].ToString("G4");
				StatusValues[3] = nCollPions[step].ToString("G4");

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

			File.WriteAllText(YburnConfigFile.OutputPath + DataFileName, LogMessages.ToString());
		}

		public void CalculateBinBoundaries()
		{
			PrepareJob("CalculateBinBoundaries", BinBoundsStatusTitles);

			int[] numberCentralityBins;
			string[][] centralityBinStrings;
			List<double> impactParams;
			List<double> nColls;
			List<double> nParts;
			List<double> dSigmadbs;
			List<double> sigmas;
			CalculateBinBoundaries(out numberCentralityBins, out centralityBinStrings,
				out impactParams, out nColls, out nParts, out dSigmadbs, out sigmas);

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
			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Length; binGroupIndex++)
			{
				LogMessages.AppendLine("#");
				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					LogMessages.AppendLine(string.Format("#{0,11}{1,8} < b < {2,4}{3,12}",
						centralityBinStrings[binGroupIndex][binIndex],
						ImpactParamsAtBinBoundaries[binGroupIndex][binIndex].ToString("G4"),
						ImpactParamsAtBinBoundaries[binGroupIndex][binIndex + 1].ToString("G4"),
						MeanParticipantsInBin[binGroupIndex][binIndex].ToString("G4")));
				}
			}

			LogMessages.Append(LogFooter);

			File.WriteAllText(YburnConfigFile.OutputPath + DataFileName, LogMessages.ToString());
		}

		private void CalculateBinBoundaries(
			out int[] numberCentralityBins,
			out string[][] centralityBinStrings,
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

			centralityBinStrings = GetCentralityBinStrings();
			numberCentralityBins = calculator.NumberCentralityBins;
			ImpactParamsAtBinBoundaries = calculator.ImpactParamsAtBinBoundaries;
			ParticipantsAtBinBoundaries = calculator.ParticipantsAtBinBoundaries;
			MeanParticipantsInBin = calculator.MeanParticipantsInBin;
		}

		public void CalculateSuppression()
		{
			PrepareJob("CalculateSuppression", BinBoundsStatusTitles);

			int[] numberCentralityBins;
			string[][] centralityBinStrings;
			List<double> impactParams;
			List<double> nColls;
			List<double> nParts;
			List<double> dSigmadbs;
			List<double> sigmas;
			CalculateBinBoundaries(out numberCentralityBins, out centralityBinStrings,
				out impactParams, out nColls, out nParts, out dSigmadbs, out sigmas);

			SetStatusVariables(SuppressionStatusTitles);
			DetermineMaxLifeTime();

			double[][][][] qgpSuppressionFactors = CalculateQGPSuppressionFactors(numberCentralityBins);

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

			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Length; binGroupIndex++)
			{
				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					for(int pTIndex = 0; pTIndex < TransverseMomenta.Length; pTIndex++)
					{
						results.AppendFormat("{0,12}{1,12}{2,12}",
							centralityBinStrings[binGroupIndex][binIndex],
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
						centralityBinStrings[binGroupIndex][binIndex],
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

				if(binGroupIndex < numberCentralityBins.Length - 1)
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
			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Length; binGroupIndex++)
			{
				// run through the centrality bins
				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					// run through the pT bins
					for(int pTIndex = 0; pTIndex < TransverseMomenta.Length; pTIndex++)
					{
						rAAs[pTIndex] = CalculateFullSuppressionFactors(qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex]);

						results.AppendFormat("{0,12}{1,12}{2,12}",
							centralityBinStrings[binGroupIndex][binIndex],
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
						centralityBinStrings[binGroupIndex][binIndex],
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

				if(binGroupIndex < numberCentralityBins.Length - 1)
				{
					results.AppendLine();
				}
			}

			// store information in LogString, print it out and save it to file
			string logString = LogHeader + "\r\n\r\n" + results.ToString() + LogFooter;

			File.WriteAllText(YburnConfigFile.OutputPath + DataFileName, logString);

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

			List<KeyValuePair<double, double>>[] tGammaList = TemperatureDecayWidthListHelper.GetList(
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

		public void ShowInitialQQPopulations()
		{
			CurrentJobTitle = "ShowInitialQQPopulations";

			LogMessages.Clear();
			LogMessages.AppendFormat("Initial QQ populations:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetInitialQQPopulationsString(ProtonProtonBaseline, FeedDown3P));
		}

		public void ShowProtonProtonDimuonDecays()
		{
			CurrentJobTitle = "ShowProtonProtonDimuonDecays";

			LogMessages.Clear();
			LogMessages.AppendFormat("Scaled pp dimuon decays:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetProtonProtonDimuonDecaysString(ProtonProtonBaseline, FeedDown3P));
		}

		public void ShowSnapsX()
		{
			Process.Start("wgnuplot", "--persist \""
				+ BuildPlotPathFile(YburnConfigFile.OutputPath + DataFileName) + "-plotX.plt\"");
		}

		public void ShowSnapsY()
		{
			Process.Start("wgnuplot", "--persist \""
				+ BuildPlotPathFile(YburnConfigFile.OutputPath + DataFileName) + "-plotY.plt\"");
		}

		public void ShowSnapsXY()
		{
			Process.Start("wgnuplot", "--persist \""
				+ BuildPlotPathFile(YburnConfigFile.OutputPath + DataFileName) + "-plotXY.plt\"");
		}

		public void ShowY1SFeedDown()
		{
			CurrentJobTitle = "ShowY1SFeedDown";

			LogMessages.Clear();
			LogMessages.AppendFormat("Y1S feed down:\r\n\r\n{0}\r\n\r\n",
				BottomiumCascade.GetY1SFeedDownString(ProtonProtonBaseline, FeedDown3P));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

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

			param.BeamRapidity = BeamRapidity;
			param.NumberAveragingAngles = NumberAveragingAngles;
			param.DecayWidthEvaluationType = DecayWidthEvaluationType;
			param.DiffusenessAFm = DiffusenessA;
			param.DiffusenessBFm = DiffusenessB;
			param.ExpansionMode = ExpansionMode;
			param.FormationTimesFm = FormationTimes;
			param.GridCellSizeFm = GridCellSize;
			param.GridRadiusFm = GridRadius;
			param.ImpactParameterFm = ImpactParameter;
			param.InitialMaximumTemperatureMeV = InitialMaximumTemperature;
			param.BreakupTemperatureMeV = BreakupTemperature;
			param.NuclearRadiusAFm = NuclearRadiusA;
			param.NuclearRadiusBFm = NuclearRadiusB;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.ProtonProtonBaseline = ProtonProtonBaseline;
			param.QGPFormationTemperatureMeV = QGPFormationTemperature;
			param.ShapeFunctionTypeA = ShapeFunctionTypeA;
			param.ShapeFunctionTypeB = ShapeFunctionTypeB;
			param.TemperatureDecayWidthList = TemperatureDecayWidthListHelper.GetList(
				GetQQDataPathFile(), DecayWidthType, PotentialTypes);
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTimeFm = ThermalTime;
			param.TransverseMomentaGeV = TransverseMomenta;

			return param;
		}

		private string BuildPlotPathFile(
			string outPathFile
			)
		{
			int indexOfDot = outPathFile.LastIndexOf(".");
			string tempPathFile = outPathFile.Substring(0, indexOfDot);
			string extension = outPathFile.Substring(indexOfDot, outPathFile.Length - indexOfDot);
			return (tempPathFile + "-b" + ImpactParameter + extension).Replace("\\", "/");
		}

		private double[] CalculateFullSuppressionFactors(
			double[] qgpSuppressionFactors
			)
		{
			//double[] ppQGPSuppressionFactors = { 1, 1, 1, 1, 1, 1 };

			//double ppResult1S = BottomiumCascade.GetDimuonDecays(
			//	ppYields, ppQGPSuppressionFactors, BottomiumState.Y1S);
			//double ppResult2S = BottomiumCascade.GetDimuonDecays(
			//	ppYields, ppQGPSuppressionFactors, BottomiumState.Y2S);
			//double ppResult3S = BottomiumCascade.GetDimuonDecays(
			//	ppYields, ppQGPSuppressionFactors, BottomiumState.Y3S);

			double[] ppDimuonDecays = BottomiumCascade.GetProtonProtonDimuonDecays(
				ProtonProtonBaseline, FeedDown3P);
			double ppResult1S = ppDimuonDecays[(int)BottomiumState.Y1S];
			double ppResult2S = ppDimuonDecays[(int)BottomiumState.Y2S];
			double ppResult3S = ppDimuonDecays[(int)BottomiumState.Y3S];

			double heavyIonResult1S = BottomiumCascade.GetDimuonDecays(
				ProtonProtonBaseline, FeedDown3P, qgpSuppressionFactors, BottomiumState.Y1S);
			double heavyIonResult2S = BottomiumCascade.GetDimuonDecays(
				ProtonProtonBaseline, FeedDown3P, qgpSuppressionFactors, BottomiumState.Y2S);
			double heavyIonResult3S = BottomiumCascade.GetDimuonDecays(
				ProtonProtonBaseline, FeedDown3P, qgpSuppressionFactors, BottomiumState.Y3S);

			return new double[] {
				heavyIonResult1S / ppResult1S,
				heavyIonResult2S / ppResult2S,
				heavyIonResult3S / ppResult3S,
				( heavyIonResult2S / heavyIonResult1S ) / ( ppResult2S / ppResult1S ),
				( heavyIonResult3S / heavyIonResult1S ) / ( ppResult3S / ppResult1S )
			};
		}

		protected double[][][][] CalculateQGPSuppressionFactors(
			int[] numberCentralityBins
			)
		{
			QGPSuppression qgpSuppression = new QGPSuppression(CreateFireballParam(),
				numberCentralityBins, ImpactParamsAtBinBoundaries, JobCancelToken);
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

			Fireball.Fireball fireball = CreateFireball();
			BjorkenLifeTime = fireball.BjorkenLifeTime;

			// extract path and file name of outfile and extension separately
			string pathFile = BuildPlotPathFile(YburnConfigFile.OutputPath + DataFileName);

			// All data is saved in the output file. Additionally, the corresponding gnuplot files (.plt)
			// are created to facilitate graphical visualization of the data.
			StringBuilder dataFileString = new StringBuilder();
			StringBuilder gnuFileStringX = new StringBuilder();
			StringBuilder gnuFileStringY = new StringBuilder();
			StringBuilder gnuFileStringXY = new StringBuilder();

			int numberGridPoints = Convert.ToInt32(Math.Round(GridRadius / GridCellSize)) + 1;

			gnuFileStringX.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\n\r\n",
				GridRadius.ToString("G3")));
			gnuFileStringY.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\n\r\n",
				GridRadius.ToString("G3")));
			gnuFileStringXY.Append(string.Format("reset\r\n\r\nset xr[0:{0,3}]\r\nset yr[0:{1,3}]\r\n\r\n",
				GridRadius.ToString("G3"), GridRadius.ToString("G3")));

			string xPlotStringBegin = "p \"" + pathFile
				+ "\" every " + numberGridPoints.ToString() + " index ";
			string xPlotStringEnd = " u 1:3 w p; pause .5";
			string yPlotStringBegin = "p \"" + pathFile
				+ "\" every ::::" + (numberGridPoints - 1).ToString() + " index ";
			string yPlotStringEnd = " u 2:3 w p; pause .5";
			string xyPlotStringBegin = "sp \"" + pathFile + "\" index ";
			string xyPlotStringEnd = " u 1:2:3 w p; pause .5";

			int index = 0;
			double dt = 1.0 / SnapRate;
			double currentTime;
			while(fireball.MaximumTemperature > BreakupTemperature)
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
				gnuFileStringXY.AppendLine(xyPlotStringBegin + index + xyPlotStringEnd);

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

		// get Bjorken- and QGP lifetime for ImpactParameter = 0
		private void DetermineMaxLifeTime()
		{
			Fireball.Fireball fireball = CreateFireballToDetermineMaxLifeTime();
			// Evolving the fireball to calculate the maximum QGP LifeTime
			while(fireball.MaximumTemperature > BreakupTemperature)
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
	}
}