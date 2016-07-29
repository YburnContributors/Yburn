using System;
using System.Collections.Generic;
using System.Threading;

namespace Yburn.Fireball
{
    public class QGPSuppression
    {
        /********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

        public QGPSuppression(
            FireballParam fireballParam,
                int[] numberCentralityBins,
            double[][] impactParamsAtBinBoundaries,
            CancellationToken cancellationToken
            )
        {
            FireballParam = fireballParam.Clone();
            NumberCentralityBins = numberCentralityBins;
            ImpactParamsAtBinBoundaries = impactParamsAtBinBoundaries;
            CancellationToken = cancellationToken;

            FlatImpactParamsAtBinBoundaries = GetFlatImpactParams();
            NumberFlatBins = FlatImpactParamsAtBinBoundaries.Length - 1;
            ArrayReshapingMask = GetArrayReshapingMask();
        }

        /********************************************************************************************
	  * Public members, functions and properties
	  ********************************************************************************************/

        public double[] FlatImpactParamsAtBinBoundaries
        {
            get;
            private set;
        }

        public int[][] ArrayReshapingMask
        {
            get;
            private set;
        }

        public int[][][] BinsReshapingMask
        {
            get;
            private set;
        }

        public double[][][][] CalculateQGPSuppressionFactors()
        {
            InitializeMembers();
            CalculateQGPSuppressionFactorsBinwise();

            return QGPSuppressionFactors;
        }

        public void TrackStatus(
            string[] statusValues
            )
        {
            if(statusValues.Length != 3)
            {
                throw new Exception("Length of StatusValues must be three.");
            }

            StatusValues = statusValues;
        }

        /********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

        private static readonly int NumberBottomiumStates
            = Enum.GetValues(typeof(BottomiumState)).Length;

        /********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

        private string[] StatusValues;

        private CancellationToken CancellationToken;

        private FireballParam FireballParam;

        private double[][] ImpactParamsAtBinBoundaries;

        private int[] NumberCentralityBins;

        private int NumberFlatBins;

        private double[][][][] QGPSuppressionFactors;

        private double[][][] FlatQGPSuppressionFactors;

        private double[][] NormalizationFactors;

        private double[] FlatNormalizationFactors;

        private double CurrentImpactParam;

        private int CurrentBinIndex;

        private void InitializeMembers()
        {
            CurrentBinIndex = 0;
            FlatNormalizationFactors = new double[NumberFlatBins];
            InitQGPSuppressionFactors();

            // impactParam = 0 does not contribute
            CurrentImpactParam = FireballParam.GridCellSizeFm;
        }

        private double[] GetFlatImpactParams()
        {
            SortedSet<double> impactParamsSet = new SortedSet<double>();
            for(int i = 0; i < ImpactParamsAtBinBoundaries.Length; i++)
            {
                impactParamsSet.UnionWith(ImpactParamsAtBinBoundaries[i]);
            }

            double[] flatImpactParams = new double[impactParamsSet.Count];
            impactParamsSet.CopyTo(flatImpactParams);

            return flatImpactParams;
        }

        private int[][] GetArrayReshapingMask()
        {
            int[][] boundariesMask = new int[NumberCentralityBins.Length][];
            for(int i = 0; i < boundariesMask.Length; i++)
            {
                boundariesMask[i] = new int[NumberCentralityBins[i] + 1];
                for(int j = 0; j < boundariesMask[i].Length; j++)
                {
                    boundariesMask[i][j] = Array.IndexOf(
                         FlatImpactParamsAtBinBoundaries, ImpactParamsAtBinBoundaries[i][j]);
                }
            }

            return boundariesMask;
        }

        private void InitQGPSuppressionFactors()
        {
            FlatQGPSuppressionFactors = new double[NumberFlatBins][][];
            for(int binIndex = 0; binIndex < NumberFlatBins; binIndex++)
            {
                FlatQGPSuppressionFactors[binIndex]
                          = new double[FireballParam.TransverseMomentaGeV.Length][];
                for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomentaGeV.Length; pTIndex++)
                {
                    FlatQGPSuppressionFactors[binIndex][pTIndex] = new double[NumberBottomiumStates];
                }
            }
        }

        private void CalculateQGPSuppressionFactorsBinwise()
        {
            while(!HasFinishedAllBins
                     && !CancellationToken.IsCancellationRequested)
            {
                CalculateUnnormalizedQGPSuppressionFactors();
                CurrentBinIndex++;
            }

            ReshapeFlatArrays();
            NormalizeQGPSuppressionFactors();
        }

        private bool HasFinishedAllBins
        {
            get
            {
                return CurrentBinIndex == NumberFlatBins;
            }
        }

        private void CalculateUnnormalizedQGPSuppressionFactors()
        {
            while(!HasFinishedCurrentBin
                && !CancellationToken.IsCancellationRequested)
            {
                Fireball fireball = CreateFireball();
                double centralTemperature = fireball.CentralTemperature;

                CalculateFireballEvolution(fireball);
                CollectResults(fireball);
                UpdateStatus(centralTemperature, fireball.LifeTime);

                CurrentImpactParam += FireballParam.GridCellSizeFm;
            }
        }

        private bool HasFinishedCurrentBin
        {
            get
            {
                return CurrentImpactParam >= ImpactParamAtNextBoundary;
            }
        }

        private double ImpactParamAtNextBoundary
        {
            get
            {
                return FlatImpactParamsAtBinBoundaries[CurrentBinIndex + 1];
            }
        }

        private Fireball CreateFireball()
        {
            FireballParam param = FireballParam.Clone();
            param.ImpactParameterFm = CurrentImpactParam;

            return new Fireball(param);
        }

        private void CalculateFireballEvolution(
            Fireball fireball
            )
        {
            while(fireball.CentralTemperature > FireballParam.MinimalCentralTemperatureMeV
                && !CancellationToken.IsCancellationRequested)
            {
                // use small time steps so that an abort command by the user is registered faster
                fireball.Advance(0.1);
            }
        }

        private void CollectResults(
            Fireball fireball
            )
        {
            FlatNormalizationFactors[CurrentBinIndex]
                += CurrentImpactParam * fireball.IntegrateFireballField("Overlap");

            for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomentaGeV.Length; pTIndex++)
            {
                for(int stateIndex = 0; stateIndex < NumberBottomiumStates; stateIndex++)
                {
                    FlatQGPSuppressionFactors[CurrentBinIndex][pTIndex][stateIndex] +=
                        CurrentImpactParam * fireball.IntegrateFireballField(
                        "UnscaledSuppression", (BottomiumState)stateIndex, pTIndex);
                }
            }
        }

        private void UpdateStatus(
            double centralTemperature,
            double lifeTime
            )
        {
            if(StatusValues != null)
            {
                StatusValues[0] = CurrentImpactParam.ToString("G5");
                StatusValues[1] = centralTemperature.ToString("G5");
                StatusValues[2] = lifeTime.ToString("G5");
            }
        }

        private void ReshapeFlatArrays()
        {
            NormalizationFactors = new double[NumberCentralityBins.Length][];
            QGPSuppressionFactors = new double[NumberCentralityBins.Length][][][];
            for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Length; binGroupIndex++)
            {
                NormalizationFactors[binGroupIndex] = new double[NumberCentralityBins[binGroupIndex]];
                QGPSuppressionFactors[binGroupIndex]
                     = new double[NumberCentralityBins[binGroupIndex]][][];
                for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
                {
                    ReshapeNormalizationFactors(binGroupIndex, binIndex);
                    ReshapeQGPSuppressionFactors(binGroupIndex, binIndex);
                }
            }
        }

        private void ReshapeNormalizationFactors(
             int binGroupIndex,
             int binIndex
             )
        {
            for(int flatBinIndex = ArrayReshapingMask[binGroupIndex][binIndex];
                 flatBinIndex < ArrayReshapingMask[binGroupIndex][binIndex + 1];
                 flatBinIndex++)
            {
                NormalizationFactors[binGroupIndex][binIndex]
                     += FlatNormalizationFactors[flatBinIndex];
            }
        }

        private void ReshapeQGPSuppressionFactors(
             int binGroupIndex,
             int binIndex
             )
        {
            QGPSuppressionFactors[binGroupIndex][binIndex]
                 = new double[FireballParam.TransverseMomentaGeV.Length][];
            for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomentaGeV.Length; pTIndex++)
            {
                QGPSuppressionFactors[binGroupIndex][binIndex][pTIndex]
                     = new double[NumberBottomiumStates];
                for(int stateIndex = 0; stateIndex < NumberBottomiumStates; stateIndex++)
                {
                    for(int flatBinIndex = ArrayReshapingMask[binGroupIndex][binIndex];
                         flatBinIndex < ArrayReshapingMask[binGroupIndex][binIndex + 1];
                         flatBinIndex++)
                    {
                        QGPSuppressionFactors[binGroupIndex][binIndex][pTIndex][stateIndex]
                        += FlatQGPSuppressionFactors[flatBinIndex][pTIndex][stateIndex];
                    }
                }
            }
        }

        private void NormalizeQGPSuppressionFactors()
        {
            for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Length; binGroupIndex++)
            {
                for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
                {
                    for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomentaGeV.Length; pTIndex++)
                    {
                        for(int stateIndex = 0; stateIndex < NumberBottomiumStates; stateIndex++)
                        {
                            QGPSuppressionFactors[binGroupIndex][binIndex][pTIndex][stateIndex]
                                 /= NormalizationFactors[binGroupIndex][binIndex];
                        }
                    }
                }
            }
        }
    }
}