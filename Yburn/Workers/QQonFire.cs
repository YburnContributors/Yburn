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
using System.IO;
using System.Text;
using Yburn.Fireball;
using Yburn.FormatUtil;
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

				case "ShowY1SFeedDownFractions":
					ShowY1SFeedDownFractions();
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

				StatusValues[0] = impactParams[step].ToUIString();
				StatusValues[1] = nColls[step].ToUIString();
				StatusValues[2] = nCollQGPs[step].ToUIString();
				StatusValues[3] = nCollPions[step].ToUIString();

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
					impactParams[i].ToUIString(),
					nColls[i].ToUIString(),
					nCollQGPs[i].ToUIString(),
					nCollPions[i].ToUIString()));
			}

			LogMessages.Append(LogFooter);

			File.WriteAllText(YburnConfigFile.OutputPath + DataFileName, LogMessages.ToString());
		}

		public void CalculateBinBoundaries()
		{
			PrepareJob("CalculateBinBoundaries", BinBoundsStatusTitles);

			List<int> numberCentralityBins;
			List<List<string>> centralityBinStrings;
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
					impactParams[i].ToUIString(),
					nColls[i].ToUIString(),
					nParts[i].ToUIString(),
					dSigmadbs[i].ToUIString(),
					sigmas[i].ToUIString()));
			}

			LogMessages.AppendLine(string.Format("\r\n\r\n#{0,11}{1,19}{2,12}",
				"Centrality",
				"Bin size",
				"<Npart>"));
			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Count; binGroupIndex++)
			{
				LogMessages.AppendLine("#");
				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					LogMessages.AppendLine(string.Format("#{0,11}{1,8} < b < {2,4}{3,12}",
						centralityBinStrings[binGroupIndex][binIndex],
						ImpactParamsAtBinBoundaries[binGroupIndex][binIndex].ToUIString(),
						ImpactParamsAtBinBoundaries[binGroupIndex][binIndex + 1].ToUIString(),
						MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString()));
				}
			}

			LogMessages.Append(LogFooter);

			File.WriteAllText(YburnConfigFile.OutputPath + DataFileName, LogMessages.ToString());
		}

		private void CalculateBinBoundaries(
			out List<int> numberCentralityBins,
			out List<List<string>> centralityBinStrings,
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

			List<int> numberCentralityBins;
			List<List<string>> centralityBinStrings;
			List<double> impactParams;
			List<double> nColls;
			List<double> nParts;
			List<double> dSigmadbs;
			List<double> sigmas;
			CalculateBinBoundaries(out numberCentralityBins, out centralityBinStrings,
				out impactParams, out nColls, out nParts, out dSigmadbs, out sigmas);

			SetStatusVariables(SuppressionStatusTitles);
			DetermineMaxLifeTime();

			BottomiumVector[][][] qgpSuppressionFactors = CalculateQGPSuppressionFactors(numberCentralityBins);

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

			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Count; binGroupIndex++)
			{
				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					for(int pTIndex = 0; pTIndex < TransverseMomenta.Count; pTIndex++)
					{
						results.AppendFormat("{0,12}{1,12}{2,12}",
							centralityBinStrings[binGroupIndex][binIndex],
							MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
							TransverseMomenta[pTIndex].ToUIString());

						foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
						{
							results.AppendFormat("{0,12}",
								qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex][state].ToUIString());
						}

						results.AppendLine();
					}

					results.AppendFormat("{0,12}{1,12}{2,12}",
						centralityBinStrings[binGroupIndex][binIndex],
						MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
						"<pT>");

					foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
					{
						double[] rAAQGPsBinValues = new double[TransverseMomenta.Count];
						for(int pTIndex = 0; pTIndex < TransverseMomenta.Count; pTIndex++)
						{
							rAAQGPsBinValues[pTIndex] = qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex][state];
						}

						results.AppendFormat("{0,12}",
							TransverseMomentumAverager.Calculate(state, TransverseMomenta.ToArray(), rAAQGPsBinValues)
							.ToUIString());
					}

					results.AppendLine();
				}

				if(binGroupIndex < numberCentralityBins.Count - 1)
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
			double[][] rAAs = new double[TransverseMomenta.Count][];
			// run through the centrality bin groups
			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Count; binGroupIndex++)
			{
				// run through the centrality bins
				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					// run through the pT bins
					for(int pTIndex = 0; pTIndex < TransverseMomenta.Count; pTIndex++)
					{
						rAAs[pTIndex] = CalculateFullSuppressionFactors(qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex]);

						results.AppendFormat("{0,12}{1,12}{2,12}",
							centralityBinStrings[binGroupIndex][binIndex],
							MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
							TransverseMomenta[pTIndex].ToUIString());

						for(int stateIndex = 0; stateIndex < 5; stateIndex++)
						{
							results.AppendFormat("{0,15}",
								rAAs[pTIndex][stateIndex].ToUIString());
						}

						results.AppendLine();
					}

					results.AppendFormat("{0,12}{1,12}{2,12}",
						centralityBinStrings[binGroupIndex][binIndex],
						MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
						"<pT>");

					// average of all pT values
					for(int l = 0; l < 5; l++)
					{
						double[] rAABinValues = new double[TransverseMomenta.Count];
						for(int pTIndex = 0; pTIndex < TransverseMomenta.Count; pTIndex++)
						{
							rAABinValues[pTIndex] = rAAs[pTIndex][l];
						}

						results.AppendFormat("{0,15}",
							TransverseMomentumAverager.Calculate((BottomiumState)l,
							TransverseMomenta.ToArray(),
							rAABinValues).ToUIString());
					}

					results.AppendLine();
				}

				if(binGroupIndex < numberCentralityBins.Count - 1)
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

			List<List<QQDataSet>> dataSetLists = new List<List<QQDataSet>>();
			foreach(BottomiumState state in BottomiumStates)
			{
				dataSetLists.Add(DecayWidthProvider.GetBoundStateDataSets(
					GetQQDataPathFile(), PotentialTypes, state));
			}

			LogMessages.Clear();
			foreach(BottomiumState state in BottomiumStates)
			{
				LogMessages.AppendFormat("{0,16}", state.ToUIString());
			}

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			foreach(BottomiumState state in BottomiumStates)
			{
				LogMessages.AppendFormat("{0,6}{1,10}", "T", DecayWidthType);
			}

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			for(int i = 0; i < dataSetLists[0].Count; i++)
			{
				foreach(List<QQDataSet> dataSetList in dataSetLists)
				{
					if(i < dataSetList.Count)
					{
						LogMessages.AppendFormat("{0,6}{1,10}",
							dataSetList[i].Temperature.ToUIString(),
							dataSetList[i].GetGamma(DecayWidthType).ToUIString());
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

			BottomiumCascade cascade = new BottomiumCascade(DimuonDecaysFrompp);

			LogMessages.Clear();
			LogMessages.AppendFormat("Initial QQ populations:\r\n\r\n{0}\r\n\r\n",
				cascade.GetInitialQQPopulationsString());
		}

		public void ShowProtonProtonDimuonDecays()
		{
			CurrentJobTitle = "ShowProtonProtonDimuonDecays";

			BottomiumCascade cascade = new BottomiumCascade(DimuonDecaysFrompp);

			LogMessages.Clear();
			LogMessages.AppendFormat("Scaled pp dimuon decays:\r\n\r\n{0}\r\n\r\n",
				cascade.GetNormalizedProtonProtonDimuonDecaysString());
		}

		public void ShowY1SFeedDownFractions()
		{
			CurrentJobTitle = "ShowY1SFeedDownFractions";

			BottomiumCascade cascade = new BottomiumCascade(DimuonDecaysFrompp);

			LogMessages.Clear();
			LogMessages.AppendFormat("Y1S feed down fractions:\r\n\r\n{0}\r\n\r\n",
				cascade.GetY1SFeedDownFractionsString());
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
			else if(enumName == "NucleusShape")
			{
				return typeof(NucleusShape);
			}
			else if(enumName == "DecayWidthType")
			{
				return typeof(DecayWidthType);
			}
			else if(enumName == "DopplerShiftEvaluationType")
			{
				return typeof(DopplerShiftEvaluationType);
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
			param.TransverseMomentaGeV = new List<double> { 0 };
			param.ExpansionMode = ExpansionMode.Longitudinal;

			return new Fireball.Fireball(param);
		}

		private Fireball.Fireball CreateFireballToDetermineMaxLifeTime()
		{
			FireballParam param = CreateFireballParam();
			param.ImpactParameterFm = 0;
			param.TransverseMomentaGeV = new List<double> { 0 };

			return new Fireball.Fireball(param);
		}

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.BeamRapidity = BeamRapidity;
			param.BreakupTemperatureMeV = BreakupTemperature;
			param.DiffusenessAFm = DiffusenessA;
			param.DiffusenessBFm = DiffusenessB;
			param.ExpansionMode = ExpansionMode;
			param.FormationTimesFm = FormationTimes;
			param.GridCellSizeFm = GridCellSize;
			param.GridRadiusFm = GridRadius;
			param.ImpactParameterFm = ImpactParameter;
			param.InelasticppCrossSectionFm = InelasticppCrossSection;
			param.InitialMaximumTemperatureMeV = InitialMaximumTemperature;
			param.NuclearRadiusAFm = NuclearRadiusA;
			param.NuclearRadiusBFm = NuclearRadiusB;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NucleusShapeA = NucleusShapeA;
			param.NucleusShapeB = NucleusShapeB;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTimeFm = ThermalTime;
			param.TransverseMomentaGeV = TransverseMomenta;

			DecayWidthProvider provider = CreateDecayWidthProvider();
			param.DecayWidthRetrievalFunction = provider.GetInMediumDecayWidth;

			return param;
		}

		private DecayWidthProvider CreateDecayWidthProvider()
		{
			return new DecayWidthProvider(
				GetQQDataPathFile(),
				PotentialTypes,
				DopplerShiftEvaluationType,
				DecayWidthType,
				QGPFormationTemperature,
				NumberAveragingAngles);
		}

		private double[] CalculateFullSuppressionFactors(
			BottomiumVector qgpSuppressionFactors
			)
		{
			BottomiumCascade cascade = new BottomiumCascade(DimuonDecaysFrompp);

			BottomiumVector ppDimuonDecays = cascade.GetNormalizedProtonProtonDimuonDecays();
			double ppResult1S = ppDimuonDecays[BottomiumState.Y1S];
			double ppResult2S = ppDimuonDecays[BottomiumState.Y2S];
			double ppResult3S = ppDimuonDecays[BottomiumState.Y3S];

			BottomiumVector heavyIonDimuonDecays
				= cascade.CalculateDimuonDecays(qgpSuppressionFactors);
			double heavyIonResult1S = heavyIonDimuonDecays[BottomiumState.Y1S];
			double heavyIonResult2S = heavyIonDimuonDecays[BottomiumState.Y2S];
			double heavyIonResult3S = heavyIonDimuonDecays[BottomiumState.Y3S];

			return new double[] {
				heavyIonResult1S / ppResult1S,
				heavyIonResult2S / ppResult2S,
				heavyIonResult3S / ppResult3S,
				( heavyIonResult2S / heavyIonResult1S ) / ( ppResult2S / ppResult1S ),
				( heavyIonResult3S / heavyIonResult1S ) / ( ppResult3S / ppResult1S )
			};
		}

		protected BottomiumVector[][][] CalculateQGPSuppressionFactors(
			List<int> numberCentralityBins
			)
		{
			QGPSuppression qgpSuppression = new QGPSuppression(CreateFireballParam(),
				numberCentralityBins, ImpactParamsAtBinBoundaries, JobCancelToken);
			qgpSuppression.TrackStatus(StatusValues);

			return qgpSuppression.CalculateQGPSuppressionFactors();
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

		private List<List<string>> GetCentralityBinStrings(
			)
		{
			List<List<string>> centralityBinStrings = new List<List<string>>();
			for(int i = 0; i < CentralityBinBoundaries.Count; i++)
			{
				List<string> centralityBinGroup = new List<string>();
				for(int j = 0; j < CentralityBinBoundaries[i].Count - 1; j++)
				{
					centralityBinGroup.Add(string.Format("{0}-{1}%",
						CentralityBinBoundaries[i][j].ToUIString(),
						CentralityBinBoundaries[i][j + 1].ToUIString()));
				}
				centralityBinStrings.Add(centralityBinGroup);
			}

			return centralityBinStrings;
		}
	}
}