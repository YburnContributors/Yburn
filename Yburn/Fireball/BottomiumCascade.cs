using System;
using System.Text;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public static class BottomiumCascade
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static BottomiumCascadeMatrix CalculateBranchingRatioMatrix()
		{
			// Branching ratio B(X->Y) have non-zero values if the masses X,Y satisfy X > Y.
			BottomiumCascadeMatrix bMatrix = BottomiumCascadeMatrix.CreateEmptyMatrix();

			bMatrix[BottomiumState.Y1S, BottomiumState.x1P] = Constants.B1P1S;
			bMatrix[BottomiumState.Y1S, BottomiumState.Y2S] = Constants.B2S1S;
			bMatrix[BottomiumState.Y1S, BottomiumState.x2P] = Constants.B2P1S;
			bMatrix[BottomiumState.Y1S, BottomiumState.Y3S] = Constants.B3S1S;
			bMatrix[BottomiumState.Y1S, BottomiumState.x3P] = Constants.B3P1S;
			bMatrix[BottomiumState.x1P, BottomiumState.Y2S] = Constants.B2S1P;
			bMatrix[BottomiumState.x1P, BottomiumState.x2P] = Constants.B2P1P;
			bMatrix[BottomiumState.x1P, BottomiumState.Y3S] = Constants.B3S1P;
			bMatrix[BottomiumState.x1P, BottomiumState.x3P] = Constants.B3P1P;
			bMatrix[BottomiumState.Y2S, BottomiumState.x2P] = Constants.B2P2S;
			bMatrix[BottomiumState.Y2S, BottomiumState.Y3S] = Constants.B3S2S;
			bMatrix[BottomiumState.Y2S, BottomiumState.x3P] = Constants.B3P2S;
			bMatrix[BottomiumState.x2P, BottomiumState.Y3S] = Constants.B3S2P;
			bMatrix[BottomiumState.x2P, BottomiumState.x3P] = Constants.B3P2P;
			bMatrix[BottomiumState.Y3S, BottomiumState.x3P] = Constants.B3P3S;

			return bMatrix;
		}

		public static string GetBranchingRatioMatrixString()
		{
			StringBuilder matrixString = new StringBuilder();
			matrixString.AppendLine(CalculateBranchingRatioMatrix()
				.GetMatrixString(extractGammaTot3P: true));

			Table<string> muTable = new Table<string>(string.Empty, 2, 4);
			muTable[0, 0] = "State";
			muTable[0, 1] = BottomiumState.Y1S.ToUIString();
			muTable[0, 2] = BottomiumState.Y2S.ToUIString();
			muTable[0, 3] = BottomiumState.Y3S.ToUIString();
			muTable[1, 0] = "B(nS→µ±)";
			muTable[1, 1] = Constants.B1Smu.ToUIString();
			muTable[1, 2] = Constants.B2Smu.ToUIString();
			muTable[1, 3] = Constants.B3Smu.ToUIString();
			matrixString.Append(muTable.ToFormattedTableString(firstColumnContainsLabels: true));

			return matrixString.ToString();
		}

		public static BottomiumCascadeMatrix CalculateCumulativeMatrix()
		{
			BottomiumCascadeMatrix cMatrix = BottomiumCascadeMatrix.CreateUnitMatrix();
			BottomiumCascadeMatrix bMatrix = CalculateBranchingRatioMatrix();

			for(int j = 0; j < BottomiumStatesCount; j++)
			{
				for(int i = BottomiumStatesCount - 1; i >= 0; i--)
				{
					for(int k = BottomiumStatesCount - 1; k >= 0; k--)
					{
						var iState = (BottomiumState)i;
						var jState = (BottomiumState)j;
						var kState = (BottomiumState)k;
						cMatrix[iState, jState] += bMatrix[iState, kState] * cMatrix[kState, jState];
					}
				}
			}

			return cMatrix;
		}

		public static string GetCumulativeMatrixString()
		{
			return CalculateCumulativeMatrix().GetMatrixString(extractGammaTot3P: true);
		}

		public static BottomiumCascadeMatrix CalculateInverseCumulativeMatrix()
		{
			BottomiumCascadeMatrix inverseCMatrix = BottomiumCascadeMatrix.CreateEmptyMatrix();
			BottomiumCascadeMatrix bMatrix = CalculateBranchingRatioMatrix();

			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				foreach(BottomiumState j in Enum.GetValues(typeof(BottomiumState)))
				{
					if(i == j)
					{
						inverseCMatrix[i, j] = 1 - bMatrix[i, j];
					}
					else
					{
						inverseCMatrix[i, j] = -bMatrix[i, j];
					}
				}
			}

			return inverseCMatrix;
		}

		public static string GetInverseCumulativeMatrixString()
		{
			return CalculateInverseCumulativeMatrix().GetMatrixString(extractGammaTot3P: true);
		}

		public static BottomiumCascadeMatrix CalculateDimuonDecayMatrix()
		{
			BottomiumCascadeMatrix dMatrix = BottomiumCascadeMatrix.CreateEmptyMatrix();

			dMatrix[BottomiumState.Y1S, BottomiumState.Y1S] = Constants.B1Smu;
			dMatrix[BottomiumState.x1P, BottomiumState.x1P] = Constants.B1Smu * Constants.B1P1S;
			dMatrix[BottomiumState.Y2S, BottomiumState.Y2S] = Constants.B2Smu;
			dMatrix[BottomiumState.x2P, BottomiumState.x2P] = Constants.B1Smu * Constants.B2P1S;
			dMatrix[BottomiumState.Y3S, BottomiumState.Y3S] = Constants.B3Smu;
			dMatrix[BottomiumState.x3P, BottomiumState.x3P] = Constants.B1Smu * Constants.B3P1S;

			return dMatrix;
		}

		public static BottomiumCascadeMatrix CalculateInverseDimuonDecayMatrix()
		{
			BottomiumCascadeMatrix inverseDMatrix = BottomiumCascadeMatrix.CreateEmptyMatrix();

			inverseDMatrix[BottomiumState.Y1S, BottomiumState.Y1S] = 1.0 / Constants.B1Smu;
			inverseDMatrix[BottomiumState.x1P, BottomiumState.x1P] = 1.0 / (Constants.B1Smu * Constants.B1P1S);
			inverseDMatrix[BottomiumState.Y2S, BottomiumState.Y2S] = 1.0 / Constants.B2Smu;
			inverseDMatrix[BottomiumState.x2P, BottomiumState.x2P] = 1.0 / (Constants.B1Smu * Constants.B2P1S);
			inverseDMatrix[BottomiumState.Y3S, BottomiumState.Y3S] = 1.0 / Constants.B3Smu;
			inverseDMatrix[BottomiumState.x3P, BottomiumState.x3P] = 1.0 / (Constants.B1Smu * Constants.B3P1S);

			return inverseDMatrix;
		}

		public static BottomiumVector CalculateDimuonDecays(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P,
			BottomiumVector qgpSuppressionFactors
			)
		{
			BottomiumCascadeMatrix cMatrix = CalculateCumulativeMatrix();
			BottomiumCascadeMatrix dMatrix = CalculateDimuonDecayMatrix();
			BottomiumVector reducedPops = CalculateReducedInitialQQPopulations(
				ppBaseline, feedDown3P, qgpSuppressionFactors);

			BottomiumVector tmpPop = cMatrix * reducedPops;

			return dMatrix * tmpPop;
		}

		private static BottomiumVector CalculateReducedInitialQQPopulations(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P,
			BottomiumVector qgpSuppressionFactors
			)
		{
			BottomiumVector initialPops = CalculateInitialQQPopulations(ppBaseline, feedDown3P);

			BottomiumCascadeMatrix qgpSuppressionMatrix =
				BottomiumCascadeMatrix.CreateDiagonalMatrix(qgpSuppressionFactors);

			return qgpSuppressionMatrix * initialPops;
		}

		public static BottomiumVector CalculateInitialQQPopulations(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			BottomiumCascadeMatrix inverseCMatrix = CalculateInverseCumulativeMatrix();
			BottomiumVector popsBeforeMuonDecay =
				CalculateppPopulationsBeforeMuonDecay(ppBaseline, feedDown3P);

			return inverseCMatrix * popsBeforeMuonDecay;
		}

		public static BottomiumVector CalculateInitialQQPopulationsWithDimuonBranchingRatiosIncluded(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			BottomiumVector initialPops = CalculateInitialQQPopulations(ppBaseline, feedDown3P);

			initialPops[BottomiumState.Y1S] *= Constants.B1Smu;
			initialPops[BottomiumState.x1P] *= Constants.B1Smu;
			initialPops[BottomiumState.Y2S] *= Constants.B2Smu;
			initialPops[BottomiumState.x2P] *= Constants.B2Smu;
			initialPops[BottomiumState.Y3S] *= Constants.B3Smu;
			initialPops[BottomiumState.x3P] *= Constants.B3Smu;

			return initialPops;
		}

		public static string GetInitialQQPopulationsString(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			BottomiumVector initialPops = CalculateInitialQQPopulations(ppBaseline, feedDown3P);
			BottomiumVector initialPopsBR =
				CalculateInitialQQPopulationsWithDimuonBranchingRatiosIncluded(
					ppBaseline, feedDown3P);

			string[] initialPopsRows = initialPops
				.GetVectorString(description: "", extractGammaTot3P: true)
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			string[] stateNames = initialPopsRows[0]
				.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			string[] initialPopsFormattedValues = initialPopsRows[1]
				.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			string[] initialPopsBRRows = initialPopsBR
				.GetVectorString(description: "", extractGammaTot3P: true)
				.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			string[] initialPopsBRFormattedValues = initialPopsBRRows[1]
				.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			Table<string> combinedEntries = new Table<string>();
			combinedEntries.AddRow(initialPopsFormattedValues, string.Empty);
			combinedEntries.AddRow(initialPopsBRFormattedValues, string.Empty);

			return combinedEntries.ToFormattedTableString(
				stateNames,
				new string[] { "N^i_AA,nl/N^f_pp,1S/B(nS→µ±)", "N^i_AA,nl/N^f_pp,1S" });
		}

		public static BottomiumVector GetProtonProtonDimuonDecays(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			BottomiumVector decays = BottomiumVector.CreateEmptyVector();
			switch(ppBaseline)
			{
				case ProtonProtonBaseline.CMS2012:
				case ProtonProtonBaseline.Estimate502TeV:
					decays[BottomiumState.Y1S] = Constants.ProtonProtonDimuonDecaysY1S;
					decays[BottomiumState.x1P] = Constants.ProtonProtonDimuonDecaysx1P;
					decays[BottomiumState.Y2S] = Constants.ProtonProtonDimuonDecaysY2S;
					decays[BottomiumState.x2P] = Constants.ProtonProtonDimuonDecaysx2P;
					decays[BottomiumState.Y3S] = Constants.ProtonProtonDimuonDecaysY3S;
					decays[BottomiumState.x3P] = feedDown3P;
					break;

				default:
					throw new Exception("Invalid Baseline.");
			}

			return decays;
		}

		public static string GetProtonProtonDimuonDecaysString(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			return GetProtonProtonDimuonDecays(ppBaseline, feedDown3P)
				.GetVectorString(description: "N^f_pp,nl/N^f_pp,1S");
		}

		public static BottomiumVector CalculateY1SFeedDownFractions(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			BottomiumVector initialPops = CalculateInitialQQPopulations(ppBaseline, feedDown3P);
			BottomiumCascadeMatrix bMatrix = CalculateBranchingRatioMatrix();
			BottomiumCascadeMatrix cMatrix = CalculateCumulativeMatrix();

			BottomiumVector tmpPops = cMatrix * initialPops;
			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				tmpPops[i] *= Constants.B1Smu * bMatrix[BottomiumState.Y1S, i];
			}
			tmpPops[BottomiumState.Y1S] = Constants.B1Smu * initialPops[BottomiumState.Y1S];

			return tmpPops;
		}

		public static string GetY1SFeedDownFractionsString(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			return CalculateY1SFeedDownFractions(ppBaseline, feedDown3P)
				.GetVectorString(description: "Fraction");
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static int BottomiumStatesCount
		{
			get
			{
				return Enum.GetValues(typeof(BottomiumState)).Length;
			}
		}

		private static BottomiumVector CalculateppPopulationsBeforeMuonDecay(
			ProtonProtonBaseline ppBaseline,
			double feedDown3P
			)
		{
			BottomiumVector ppDimuonDecays =
				GetProtonProtonDimuonDecays(ppBaseline, feedDown3P);
			BottomiumCascadeMatrix inverseDMatrix =
				CalculateInverseDimuonDecayMatrix();

			return inverseDMatrix * ppDimuonDecays;
		}
	}
}