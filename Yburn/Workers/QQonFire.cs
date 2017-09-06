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
					LogMessages.Append(LogHeader + LogFooter);
					return;
				}

				impactParams.Add(step * GridCellSize_fm);

				Fireball.Fireball fireball = CreateFireballToCalcDirectPionDecayWidth(
					impactParams[step]);

				// Set BjorkenLifeTime for the LogHeader
				if(step == 0)
				{
					BjorkenLifeTime_fm = fireball.BjorkenLifeTime;
				}

				// calculate the areas
				double nCollQGP;
				double nCollPion;
				fireball.CalculateNcolls(BreakupTemperature_MeV, out nCollQGP, out nCollPion);
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
				LogMessages.Append(LogHeader + LogFooter);
				return;
			}

			// prepare output
			LogMessages.Clear();
			LogMessages.Append(LogHeader);

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			LogMessages.AppendLine(string.Format("#{0,7}{1,12}{2,12}{3,12}",
				"b (fm)", "Ncoll", "NcollQGP", "NcollPion"));
			LogMessages.AppendLine("#");
			for(int i = 0; i < impactParams.Count; i++)
			{
				LogMessages.AppendLine(string.Format("{0,8}{1,12}{2,12}{3,12}",
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

			// quit here if process has been aborted
			if(JobCancelToken.IsCancellationRequested)
			{
				LogMessages.Clear();
				LogMessages.Append(LogHeader + LogFooter);
				return;
			}

			// prepare output
			LogMessages.Clear();
			LogMessages.Append(LogHeader);

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			LogMessages.AppendLine(string.Format("#{0,7}{1,12}{2,12}{3,14}{4,14}",
				"b (fm)", "Ncoll", "Npart", "dσ/db (fm)", "σ (fm^2)"));
			LogMessages.AppendLine("#");
			for(int i = 0; i < impactParams.Count; i++)
			{
				LogMessages.AppendLine(string.Format("{0,8}{1,12}{2,12}{3,14}{4,14}",
					impactParams[i].ToUIString(),
					nColls[i].ToUIString(),
					nParts[i].ToUIString(),
					dSigmadbs[i].ToUIString(),
					sigmas[i].ToUIString()));
			}

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			LogMessages.AppendLine(string.Format("#{0,11}{1,19}{2,12}",
				"Centrality",
				"Bin size (fm)",
				"<Npart>"));
			LogMessages.AppendLine("#");
			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Count; binGroupIndex++)
			{
				if(binGroupIndex > 0)
				{
					LogMessages.AppendLine();
				}

				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					LogMessages.AppendLine(string.Format("{0,12}{1,8} < b < {2,4}{3,12}",
						centralityBinStrings[binGroupIndex][binIndex],
						ImpactParamsAtBinBoundaries_fm[binGroupIndex][binIndex].ToUIString(),
						ImpactParamsAtBinBoundaries_fm[binGroupIndex][binIndex + 1].ToUIString(),
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
			calculator.Calculate(CentralityBinBoundaries_percent);

			impactParams = calculator.ImpactParams;
			nColls = calculator.Ncolls;
			nParts = calculator.Nparts;
			dSigmadbs = calculator.DSigmaDbs;
			sigmas = calculator.Sigmas;

			centralityBinStrings = GetCentralityBinStrings();
			numberCentralityBins = calculator.NumberCentralityBins;
			ImpactParamsAtBinBoundaries_fm = calculator.ImpactParamsAtBinBoundaries;
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

			BottomiumVector[][][] qgpSuppressionFactors
				= CalculateQGPSuppressionFactors(numberCentralityBins);

			// quit here if process has been aborted
			if(JobCancelToken.IsCancellationRequested)
			{
				LogMessages.Clear();
				LogMessages.Append(LogHeader + LogFooter);
				return;
			}

			// prepare output
			LogMessages.Clear();
			LogMessages.Append(LogHeader);

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			LogMessages.AppendLine("#Preliminary suppression factors:");
			LogMessages.AppendLine("#");
			LogMessages.AppendFormat("#{0,11}{1,12}{2,12}",
				"Centrality", "<Npart>", "pT (GeV/c)");
			foreach(string sStateName in Enum.GetNames(typeof(BottomiumState)))
			{
				LogMessages.AppendFormat("{0,12}", string.Format("RAAQGP({0})", sStateName));
			}
			LogMessages.AppendLine();
			LogMessages.AppendLine("#");

			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Count; binGroupIndex++)
			{
				if(binGroupIndex > 0)
				{
					LogMessages.AppendLine();
				}

				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					for(int pTIndex = 0; pTIndex < TransverseMomenta_GeV.Count; pTIndex++)
					{
						LogMessages.AppendFormat("{0,12}{1,12}{2,12}",
							centralityBinStrings[binGroupIndex][binIndex],
							MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
							TransverseMomenta_GeV[pTIndex].ToUIString());

						foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
						{
							LogMessages.AppendFormat("{0,12}",
								qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex][state].ToUIString());
						}

						LogMessages.AppendLine();
					}

					LogMessages.AppendFormat("{0,12}{1,12}{2,12}",
						centralityBinStrings[binGroupIndex][binIndex],
						MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
						"<pT>");

					foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
					{
						double[] rAAQGPsBinValues = new double[TransverseMomenta_GeV.Count];
						for(int pTIndex = 0; pTIndex < TransverseMomenta_GeV.Count; pTIndex++)
						{
							rAAQGPsBinValues[pTIndex] = qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex][state];
						}

						LogMessages.AppendFormat("{0,12}",
							TransverseMomentumAverager.Calculate(state, TransverseMomenta_GeV.ToArray(), rAAQGPsBinValues)
							.ToUIString());
					}

					LogMessages.AppendLine();
				}
			}

			LogMessages.AppendLine();
			LogMessages.AppendLine();

			LogMessages.AppendLine("#Final suppression factors:");
			LogMessages.AppendLine("#");
			LogMessages.AppendLine(string.Format(
				"#{0,11}{1,12}{2,12}{3,15}{4,15}{5,15}{6,15}{7,15}",
				"Centrality",
				"<Npart>",
				"pT (GeV/c)",
				"RAA(Y1S)",
				"RAA(Y2S)",
				"RAA(Y3S)",
				"(2S/1S)PbPb-pp",
				"(3S/1S)PbPb-pp"));
			LogMessages.AppendLine("#");

			// calculate final suppression factors
			double[][] rAAs = new double[TransverseMomenta_GeV.Count][];
			// run through the centrality bin groups
			for(int binGroupIndex = 0; binGroupIndex < numberCentralityBins.Count; binGroupIndex++)
			{
				if(binGroupIndex > 0)
				{
					LogMessages.AppendLine();
				}

				// run through the centrality bins
				for(int binIndex = 0; binIndex < numberCentralityBins[binGroupIndex]; binIndex++)
				{
					// run through the pT bins
					for(int pTIndex = 0; pTIndex < TransverseMomenta_GeV.Count; pTIndex++)
					{
						rAAs[pTIndex] = CalculateFullSuppressionFactors(qgpSuppressionFactors[binGroupIndex][binIndex][pTIndex]);

						LogMessages.AppendFormat("{0,12}{1,12}{2,12}",
							centralityBinStrings[binGroupIndex][binIndex],
							MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
							TransverseMomenta_GeV[pTIndex].ToUIString());

						for(int stateIndex = 0; stateIndex < 5; stateIndex++)
						{
							LogMessages.AppendFormat("{0,15}",
								rAAs[pTIndex][stateIndex].ToUIString());
						}

						LogMessages.AppendLine();
					}

					LogMessages.AppendFormat("{0,12}{1,12}{2,12}",
						centralityBinStrings[binGroupIndex][binIndex],
						MeanParticipantsInBin[binGroupIndex][binIndex].ToUIString(),
						"<pT>");

					// average of all pT values
					for(int l = 0; l < 5; l++)
					{
						double[] rAABinValues = new double[TransverseMomenta_GeV.Count];
						for(int pTIndex = 0; pTIndex < TransverseMomenta_GeV.Count; pTIndex++)
						{
							rAABinValues[pTIndex] = rAAs[pTIndex][l];
						}

						LogMessages.AppendFormat("{0,15}",
							TransverseMomentumAverager.Calculate((BottomiumState)l,
							TransverseMomenta_GeV.ToArray(),
							rAABinValues).ToUIString());
					}

					LogMessages.AppendLine();
				}
			}

			LogMessages.Append(LogFooter);

			File.WriteAllText(YburnConfigFile.OutputPath + DataFileName, LogMessages.ToString());
		}

		public void ShowBranchingRatioMatrix()
		{
			CurrentJobTitle = "ShowBranchingRatioMatrix";

			LogMessages.Clear();
			LogMessages.AppendLine("#Branching ratio matrix:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(BottomiumCascade.GetBranchingRatioMatrixString());
			LogMessages.AppendLine();
		}

		public void ShowCumulativeMatrix()
		{
			CurrentJobTitle = "ShowCumulativeMatrix";

			LogMessages.Clear();
			LogMessages.AppendLine("#Cumulative matrix:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(BottomiumCascade.GetCumulativeMatrixString());
			LogMessages.AppendLine();
		}

		public void ShowInverseCumulativeMatrix()
		{
			CurrentJobTitle = "ShowInverseCumulativeMatrix";

			LogMessages.Clear();
			LogMessages.AppendLine("#Inverse cumulative matrix:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(BottomiumCascade.GetInverseCumulativeMatrixString());
			LogMessages.AppendLine();
		}

		public void ShowDecayWidthInput()
		{
			CurrentJobTitle = "ShowDecayWidthInput";

			List<List<QQDataSet>> dataSetLists = new List<List<QQDataSet>>();
			foreach(BottomiumState state in BottomiumStates)
			{
				dataSetLists.Add(QQDataProvider.GetBoundStateDataSets(
					QQDataPathFile, PotentialTypes, state));
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
			LogMessages.AppendLine("#Initial QQ populations:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(cascade.GetInitialQQPopulationsString());
			LogMessages.AppendLine();
		}

		public void ShowProtonProtonDimuonDecays()
		{
			CurrentJobTitle = "ShowProtonProtonDimuonDecays";

			BottomiumCascade cascade = new BottomiumCascade(DimuonDecaysFrompp);

			LogMessages.Clear();
			LogMessages.AppendLine("#Scaled pp dimuon decays:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(cascade.GetNormalizedProtonProtonDimuonDecaysString());
			LogMessages.AppendLine();
		}

		public void ShowY1SFeedDownFractions()
		{
			CurrentJobTitle = "ShowY1SFeedDownFractions";

			BottomiumCascade cascade = new BottomiumCascade(DimuonDecaysFrompp);

			LogMessages.Clear();
			LogMessages.AppendLine("#Y1S feed down fractions:");
			LogMessages.AppendLine();
			LogMessages.AppendLine(cascade.GetY1SFeedDownFractionsString());
			LogMessages.AppendLine();
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
			else if(enumName == "EMFCalculationMethod")
			{
				return typeof(EMFCalculationMethod);
			}
			else if(enumName == "ElectricDipoleAlignment")
			{
				return typeof(ElectricDipoleAlignment);
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
			param.ImpactParameter_fm = impactParam;
			param.TransverseMomentaGeV = new List<double> { 0 };
			param.ExpansionMode = ExpansionMode.Longitudinal;

			return new Fireball.Fireball(param);
		}

		private Fireball.Fireball CreateFireballToDetermineMaxLifeTime()
		{
			FireballParam param = CreateFireballParam();
			param.ImpactParameter_fm = 0;
			param.TransverseMomentaGeV = new List<double> { 0 };

			return new Fireball.Fireball(param);
		}

		private FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.BreakupTemperature_MeV = BreakupTemperature_MeV;
			param.CenterOfMassEnergyTeV = CenterOfMassEnergy_TeV;
			param.DiffusenessA_fm = DiffusenessA_fm;
			param.DiffusenessB_fm = DiffusenessB_fm;
			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.EMFQuadratureOrder = EMFQuadratureOrder;
			param.EMFUpdateInterval_fm = EMFUpdateInterval_fm;
			param.ExpansionMode = ExpansionMode;
			param.FormationTimes_fm = FormationTimes_fm;
			param.GridCellSize_fm = GridCellSize_fm;
			param.GridRadius_fm = GridRadius_fm;
			param.ImpactParameter_fm = ImpactParameter_fm;
			param.InitialMaximumTemperature_MeV = InitialMaximumTemperature_MeV;
			param.NuclearRadiusA_fm = NuclearRadiusA_fm;
			param.NuclearRadiusB_fm = NuclearRadiusB_fm;
			param.NucleonNumberA = NucleonNumberA;
			param.NucleonNumberB = NucleonNumberB;
			param.NucleusShapeA = NucleusShapeA;
			param.NucleusShapeB = NucleusShapeB;
			param.ProtonNumberA = ProtonNumberA;
			param.ProtonNumberB = ProtonNumberB;
			param.QGPConductivity_MeV = QGPConductivity_MeV;
			param.TemperatureProfile = TemperatureProfile;
			param.ThermalTime_fm = ThermalTime_fm;
			param.TransverseMomentaGeV = TransverseMomenta_GeV;
			param.UseElectricField = UseElectricField;
			param.UseMagneticField = UseMagneticField;

			QQDataProvider provider = CreateQQDataProvider();
			param.DecayWidthRetrievalFunction = provider.GetInMediumDecayWidth;

			return param;
		}

		private QQDataProvider CreateQQDataProvider()
		{
			return new QQDataProvider(
				QQDataPathFile,
				PotentialTypes,
				DopplerShiftEvaluationType,
				ElectricDipoleAlignment,
				DecayWidthType,
				QGPFormationTemperature_MeV,
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
				numberCentralityBins, ImpactParamsAtBinBoundaries_fm, JobCancelToken);
			qgpSuppression.TrackStatus(StatusValues);

			return qgpSuppression.CalculateQGPSuppressionFactors();
		}

		// get Bjorken- and QGP lifetime for ImpactParameter = 0
		private void DetermineMaxLifeTime()
		{
			Fireball.Fireball fireball = CreateFireballToDetermineMaxLifeTime();
			// Evolving the fireball to calculate the maximum QGP LifeTime
			while(fireball.MaximumTemperature > BreakupTemperature_MeV)
			{
				// quit here if process has been aborted
				if(JobCancelToken.IsCancellationRequested)
				{
					return;
				}

				fireball.Advance(0.1);
			}

			LifeTime_fm = fireball.LifeTime;
			BjorkenLifeTime_fm = fireball.BjorkenLifeTime;
		}

		private List<List<string>> GetCentralityBinStrings(
			)
		{
			List<List<string>> centralityBinStrings = new List<List<string>>();
			for(int i = 0; i < CentralityBinBoundaries_percent.Count; i++)
			{
				List<string> centralityBinGroup = new List<string>();
				for(int j = 0; j < CentralityBinBoundaries_percent[i].Count - 1; j++)
				{
					centralityBinGroup.Add(string.Format("{0}-{1}%",
						CentralityBinBoundaries_percent[i][j].ToUIString(),
						CentralityBinBoundaries_percent[i][j + 1].ToUIString()));
				}
				centralityBinStrings.Add(centralityBinGroup);
			}

			return centralityBinStrings;
		}
	}
}
