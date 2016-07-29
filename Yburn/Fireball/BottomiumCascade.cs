using System;
using System.Text;

namespace Yburn.Fireball
{
    public static class BottomiumCascade
    {
        /********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

        public const double GammaTot3P = 1e100;

        public static double[,] CalculateBranchingRatioMatrix()
        {
            // Branching ratio B(X->Y) have non-zero values if the masses X,Y satisfy X > Y.
            CascadeMatrix branchingRatioMatrix = new CascadeMatrix();
            for(int i = 0; i < BottomiumStatesCount; i++)
            {
                for(int j = 0; j < BottomiumStatesCount; j++)
                {
                    branchingRatioMatrix.Matrix[i, j] = 0;
                }
            }

            branchingRatioMatrix.SetElement("Y1S", "x1P", B1P1S);
            branchingRatioMatrix.SetElement("Y1S", "Y2S", B2S1S);
            branchingRatioMatrix.SetElement("Y1S", "x2P", B2P1S);
            branchingRatioMatrix.SetElement("Y1S", "Y3S", B3S1S);
            branchingRatioMatrix.SetElement("Y1S", "x3P", B3P1S / GammaTot3P);
            branchingRatioMatrix.SetElement("x1P", "Y2S", B2S1P);
            branchingRatioMatrix.SetElement("x1P", "x2P", B2P1P);
            branchingRatioMatrix.SetElement("x1P", "Y3S", B3S1P);
            branchingRatioMatrix.SetElement("x1P", "x3P", B3P1P / GammaTot3P);
            branchingRatioMatrix.SetElement("Y2S", "x2P", B2P2S);
            branchingRatioMatrix.SetElement("Y2S", "Y3S", B3S2S);
            branchingRatioMatrix.SetElement("Y2S", "x3P", B3P2S / GammaTot3P);
            branchingRatioMatrix.SetElement("x2P", "Y3S", B3S2P);
            branchingRatioMatrix.SetElement("x2P", "x3P", B3P2P / GammaTot3P);
            branchingRatioMatrix.SetElement("Y3S", "x3P", B3P3S / GammaTot3P);

            return branchingRatioMatrix.Matrix;
        }

        public static string GetBranchingRatioMatrixString()
        {
            StringBuilder matrixString = new StringBuilder();
            matrixString.AppendLine(GetMatrixString(CalculateBranchingRatioMatrix()));
            matrixString.AppendFormat("{0,-3}{1,12}{2,24}{3,24}\r\n",
                "mu",
                B1Smu.ToString("G4"),
                B2Smu.ToString("G4"),
                B3Smu.ToString("G4"));

            return matrixString.ToString();
        }

        public static double[,] CalculateCumulativeMatrix()
        {
            double[,] cMatrix = GetUnitMatrix();
            double[,] bMatrix = CalculateBranchingRatioMatrix();
            for(int j = 0; j < BottomiumStatesCount; j++)
            {
                for(int i = BottomiumStatesCount - 1; i >= 0; i--)
                {
                    for(int k = BottomiumStatesCount - 1; k >= 0; k--)
                    {
                        cMatrix[i, j] += bMatrix[i, k] * cMatrix[k, j];
                    }
                }
            }

            return cMatrix;
        }

        public static string GetCumulativeMatrixString()
        {
            return GetMatrixString(CalculateCumulativeMatrix());
        }

        public static double[,] CalculateInverseCumulativeMatrix()
        {
            double[,] inverseCMatrix = new double[BottomiumStatesCount, BottomiumStatesCount];
            double[,] bMatrix = CalculateBranchingRatioMatrix();
            for(int i = 0; i < BottomiumStatesCount; i++)
            {
                for(int j = 0; j < BottomiumStatesCount; j++)
                {
                    inverseCMatrix[i, j] = i == j ? 1 - bMatrix[i, j] : -bMatrix[i, j];
                }
            }

            return inverseCMatrix;
        }

        public static string GetInverseCumulativeMatrixString()
        {
            return GetMatrixString(CalculateInverseCumulativeMatrix());
        }

        public static double[] GetInitialQQPopulations(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P
            )
        {
            double[] initialQQPopulations = new double[BottomiumStatesCount];
            double[,] inverseCMatrix = CalculateInverseCumulativeMatrix();
            double[] popsBeforeMuonDecay = GetppPopulationsBeforeMuonDecay(
                ProtonProtonBaseline, FeedDown3P);
            for(int i = 0; i < BottomiumStatesCount; i++)
            {
                initialQQPopulations[i] = 0;
                for(int j = 0; j < BottomiumStatesCount; j++)
                {
                    initialQQPopulations[i] += inverseCMatrix[i, j] * popsBeforeMuonDecay[j];
                }
            }

            return initialQQPopulations;
        }

        public static string GetInitialQQPopulationsString(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P
            )
        {
            StringBuilder popsString = new StringBuilder();
            foreach(string sStateName in Enum.GetNames(typeof(BottomiumState)))
            {
                popsString.AppendFormat("{0,10}", sStateName);
            }

            popsString.AppendLine();
            popsString.AppendLine();

            double[] pops = GetInitialQQPopulations(ProtonProtonBaseline, FeedDown3P);
            foreach(BottomiumState eState in Enum.GetValues(typeof(BottomiumState)))
            {
                popsString.AppendFormat("{0,10}",
                    pops[(int)eState].ToString("G4"));
            }

            popsString.AppendLine();

            return popsString.ToString();
        }

        public static string GetProtonProtonDimuonDecaysString(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P
            )
        {
            StringBuilder ppDimuonDecaysString = new StringBuilder();
            foreach(string sStateName in Enum.GetNames(typeof(BottomiumState)))
            {
                ppDimuonDecaysString.AppendFormat("{0,10}", sStateName);
            }

            ppDimuonDecaysString.AppendLine();
            ppDimuonDecaysString.AppendLine();

            double[] ppDimuonDecays = GetProtonProtonDimuonDecays(ProtonProtonBaseline, FeedDown3P);
            foreach(BottomiumState eState in Enum.GetValues(typeof(BottomiumState)))
            {
                ppDimuonDecaysString.AppendFormat("{0,10}",
                    ppDimuonDecays[(int)eState].ToString("G4"));
            }

            ppDimuonDecaysString.AppendLine();

            return ppDimuonDecaysString.ToString();
        }

        public static double[] GetY1SFeedDown(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P
            )
        {
            CascadeVector initialPops = new CascadeVector(
                GetInitialQQPopulations(ProtonProtonBaseline, FeedDown3P));
            CascadeMatrix bMatrix = new CascadeMatrix(CalculateBranchingRatioMatrix());
            CascadeMatrix cMatrix = new CascadeMatrix(CalculateCumulativeMatrix());

            return new double[]
            {
                B1Smu * initialPops.GetElement("Y1S"),
                B1Smu * bMatrix.GetElement("Y1S", "x1P")
                    * ( initialPops.GetElement("x1P")
                    + cMatrix.GetElement("x1P", "Y2S") * initialPops.GetElement("Y2S")
                    + cMatrix.GetElement("x1P", "x2P") * initialPops.GetElement("x2P")
                    + cMatrix.GetElement("x1P", "Y3S") * initialPops.GetElement("Y3S")
                    + cMatrix.GetElement("x1P", "x3P") * initialPops.GetElement("x3P") ),
                B1Smu * bMatrix.GetElement("Y1S", "Y2S")
                    * ( initialPops.GetElement("Y2S")
                    + cMatrix.GetElement("Y2S", "x2P") * initialPops.GetElement("x2P")
                    + cMatrix.GetElement("Y2S", "Y3S") * initialPops.GetElement("Y3S")
                    + cMatrix.GetElement("Y2S", "x3P") * initialPops.GetElement("x3P") ),
                B1Smu * bMatrix.GetElement("Y1S", "x2P")
                    * ( initialPops.GetElement("x2P")
                    + cMatrix.GetElement("x2P", "Y3S") * initialPops.GetElement("Y3S")
                    + cMatrix.GetElement("x2P", "x3P") * initialPops.GetElement("x3P") ),
                B1Smu * bMatrix.GetElement("Y1S", "Y3S")
                    * ( initialPops.GetElement("Y3S")
                    + cMatrix.GetElement("Y3S", "x3P") * initialPops.GetElement("x3P") ),
                B1Smu * bMatrix.GetElement("Y1S", "x3P")
                    * initialPops.GetElement("x3P")
            };
        }

        public static string GetY1SFeedDownString(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P
            )
        {
            StringBuilder feedDownString = new StringBuilder();
            foreach(string sStateName in Enum.GetNames(typeof(BottomiumState)))
            {
                feedDownString.AppendFormat("{0,10}", sStateName);
            }

            feedDownString.AppendLine();
            feedDownString.AppendLine();

            double[] feedDown = GetY1SFeedDown(ProtonProtonBaseline, FeedDown3P);
            foreach(BottomiumState eState in Enum.GetValues(typeof(BottomiumState)))
            {
                feedDownString.AppendFormat("{0,10}",
                    feedDown[(int)eState].ToString("G4"));
            }

            feedDownString.AppendLine();

            return feedDownString.ToString();
        }

        public static double GetDimuonDecays(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P,
            double[] qgpSuppressionFactor,
            BottomiumState eState
            )
        {
            double[] tmpPop = new double[BottomiumStatesCount];
            double[,] cMatrix = CalculateCumulativeMatrix();
            for(int i = 0; i < BottomiumStatesCount; i++)
            {
                double[] reducedPops = GetReducedInitialQQPopulations(
                    ProtonProtonBaseline, FeedDown3P, qgpSuppressionFactor);

                tmpPop[i] = 0;
                for(int j = 0; j < BottomiumStatesCount; j++)
                {
                    tmpPop[i] += cMatrix[i, j] * reducedPops[j];
                }
            }

            switch(eState)
            {
                case BottomiumState.Y1S:
                    return B1Smu * tmpPop[(int)BottomiumState.Y1S];

                case BottomiumState.Y2S:
                    return B2Smu * tmpPop[(int)BottomiumState.Y2S];

                case BottomiumState.Y3S:
                    return B3Smu * tmpPop[(int)BottomiumState.Y3S];

                default:
                    throw new Exception("Invalid state.");
            }
        }

        public static double[] GetProtonProtonDimuonDecays(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P
            )
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

                case ProtonProtonBaseline.Estimate502TeV:
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

        private static double[,] GetUnitMatrix()
        {
            double[,] matrix = new double[BottomiumStatesCount, BottomiumStatesCount];
            for(int i = 0; i < BottomiumStatesCount; i++)
            {
                for(int j = 0; j < BottomiumStatesCount; j++)
                {
                    matrix[i, j] = i == j ? 1 : 0;
                }
            }

            return matrix;
        }

        // Partial widths for GammaCS
        //   static double B3P3S = (8.5 + 3*11.5 + 5*13.9)/9.0;

        //   static double B3P2P = 1e-7;

        //   static double B3P2S = (1.0 + 3*3.5 + 5*6.5)/9.0;

        //   static double B3P1P = 1e-7;

        //   static double B3P1S = (0.29 + 3*3.1 + 5*8.7)/9.0;

        // Partial widths for GammaAFrel
        private const double B3P3S = (7.8 + 3 * 9.4 + 5 * 11.4) / 9.0;

        private const double B3P2P = 1e-7;

        private const double B3P2S = (1.0 + 3 * 2.4 + 5 * 4.4) / 9.0;

        private const double B3P1P = 1e-7;

        private const double B3P1S = (0.33 + 3 * 1.7 + 5 * 4.6) / 9.0;

        private const double B3S2P = (0.131 + 0.126 + 0.059);

        private const double B3S2S = 0.106;

        private const double B3S1P = (0.0099 + 0.0009 + 0.0027);

        private const double B3S1S = (0.0437 + 0.022);

        private const double B2P1S = (0.009 + 3 * (0.0163 + 0.092) + 5 * (0.011 + 0.07)) / 9.0;

        private const double B2P1P = (3 * 0.0091 + 5 * 0.0051) / 9.0;

        private const double B2P2S = (0.046 + 3 * 0.199 + 5 * 0.106) / 9.0;

        private const double B2S1P = (0.069 + 0.0715 + 0.038);

        private const double B2S1S = (0.1792 + 0.086);

        private const double B1P1S = (0.0176 + 3 * 0.339 + 5 * 0.191) / 9.0;

        private const double B3Smu = 0.0218;

        private const double B2Smu = 0.0193;

        private const double B1Smu = 0.0248;

        private static double[] GetppPopulationsBeforeMuonDecay(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P
            )
        {
            double[] ppDimuonDecays = GetProtonProtonDimuonDecays(ProtonProtonBaseline, FeedDown3P);

            CascadeMatrix bMatrix = new CascadeMatrix(CalculateBranchingRatioMatrix());
            return new double[] {
                ppDimuonDecays[(int)BottomiumState.Y1S] / B1Smu,
                ppDimuonDecays[(int)BottomiumState.x1P] / B1Smu / bMatrix.GetElement("Y1S", "x1P"),
                ppDimuonDecays[(int)BottomiumState.Y2S] / B2Smu,
                ppDimuonDecays[(int)BottomiumState.x2P] / B1Smu / bMatrix.GetElement("Y1S", "x2P"),
                ppDimuonDecays[(int)BottomiumState.Y3S] / B3Smu,
                ppDimuonDecays[(int)BottomiumState.x3P] / B1Smu / bMatrix.GetElement("Y1S", "x3P")
            };
        }

        private static double[] GetReducedInitialQQPopulations(
            ProtonProtonBaseline ProtonProtonBaseline,
            double FeedDown3P,
            double[] relativeOccupation
            )
        {
            double[] initialQQPopulations = GetInitialQQPopulations(ProtonProtonBaseline, FeedDown3P);
            for(int i = 0; i < BottomiumStatesCount; i++)
            {
                initialQQPopulations[i] *= relativeOccupation[i];
            }

            return initialQQPopulations;
        }

        private static string GetMatrixString(
            double[,] matrix
            )
        {
            StringBuilder matrixString = new StringBuilder();

            matrixString.AppendFormat("{0,-3}", "");
            foreach(string sStateName in Enum.GetNames(typeof(BottomiumState)))
            {
                matrixString.AppendFormat("{0,12}", sStateName);
            }

            matrixString.AppendLine();
            matrixString.AppendLine();
            foreach(BottomiumState eState in Enum.GetValues(typeof(BottomiumState)))
            {
                matrixString.AppendFormat("{0,-3}", eState.ToString());
                for(int j = 0; j < BottomiumStatesCount; j++)
                {
                    matrixString.AppendFormat("{0,12}",
                        matrix[(int)eState, j].ToString("G4"));
                }

                matrixString.AppendLine();
            }

            return matrixString.ToString();
        }

        /********************************************************************************************
		 * Private/protected classes
		 ********************************************************************************************/

        private class CascadeMatrix
        {
            public CascadeMatrix()
            {
                Matrix = new double[BottomiumStatesCount, BottomiumStatesCount];
            }

            public CascadeMatrix(
                double[,] matrix
                )
            {
                Matrix = matrix;
            }

            public double[,] Matrix
            {
                get;
                private set;
            }

            public double GetElement(
                string state1,
                string state2
                )
            {
                return Matrix[Parse(state1), Parse(state2)];
            }

            public void SetElement(
                string state1,
                string state2,
                double value
                )
            {
                Matrix[Parse(state1), Parse(state2)] = value;
            }

            private static int Parse(
                string s
                )
            {
                return (int)Enum.Parse(typeof(BottomiumState), s);
            }
        }

        private class CascadeVector
        {
            public CascadeVector(
                double[] vector
                )
            {
                Vector = vector;
            }

            public double[] Vector
            {
                get;
                private set;
            }

            public double GetElement(
                string state
                )
            {
                return Vector[Parse(state)];
            }

            public void SetElement(
                string state,
                double value
                )
            {
                Vector[Parse(state)] = value;
            }

            private static int Parse(
                string s
                )
            {
                return (int)Enum.Parse(typeof(BottomiumState), s);
            }
        }
    }
}