using System;
using System.Collections.Generic;
using System.Threading;

namespace Yburn.Fireball
{
    public class BinBoundaryCalculator
    {
        /********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

        public BinBoundaryCalculator(
            FireballParam fireballParam,
            CancellationToken cancellationToken
            )
        {
            FireballParam = fireballParam.Clone();
            CancellationToken = cancellationToken;
        }

        /********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

        public void Calculate(
            int[][] binBoundariesInPercent
            )
        {
            PrepareCalculation(binBoundariesInPercent);

            AssertInputValid();

            GetValuesFromFireball();
            CalculateBinBoundaries();
            CalculateMeanParticipants();
        }

        public int[] NumberCentralityBins
        {
            get;
            private set;
        }

        public List<double> ImpactParams
        {
            get;
            private set;
        }

        public List<double> Ncolls
        {
            get;
            private set;
        }

        public List<double> Nparts
        {
            get;
            private set;
        }

        public List<double> DSigmaDbs
        {
            get;
            private set;
        }

        public List<double> Sigmas
        {
            get;
            private set;
        }

        public double[][] ImpactParamsAtBinBoundaries
        {
            get;
            private set;
        }

        public double[][] ParticipantsAtBinBoundaries
        {
            get;
            private set;
        }

        public double[][] MeanParticipantsInBin
        {
            get;
            private set;
        }

        public string[] StatusValues;

        /********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

        private static bool IsOrderedArray(
            int[] boundaries
            )
        {
            for(int i = 1; i < boundaries.Length; i++)
            {
                if(boundaries[i - 1] >= boundaries[i])
                {
                    return false;
                }
            }

            return true;
        }

        /********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

        private CancellationToken CancellationToken;

        private void PrepareCalculation(
            int[][] binBoundariesInPercent
            )
        {
            BinBoundariesInPercent = binBoundariesInPercent;
            NumberCentralityBins = GetNumberCentralityBins();
            ImpactParamsAtBinBoundaries = new double[NumberCentralityBins.Length][];
            ParticipantsAtBinBoundaries = new double[NumberCentralityBins.Length][];
            MeanParticipantsInBin = new double[NumberCentralityBins.Length][];
            for(int i = 0; i < NumberCentralityBins.Length; i++)
            {
                ImpactParamsAtBinBoundaries[i] = new double[NumberCentralityBins[i] + 1];
                ParticipantsAtBinBoundaries[i] = new double[NumberCentralityBins[i] + 1];
                MeanParticipantsInBin[i] = new double[NumberCentralityBins[i]];
            }

            ImpactParams = new List<double>();
            Ncolls = new List<double>();
            Nparts = new List<double>();
            DSigmaDbs = new List<double>();
            Sigmas = new List<double>();
        }

        private void AssertInputValid()
        {
            if(StatusValues != null && StatusValues.Length != 5)
            {
                throw new Exception("Length of StatusValues must be five.");
            }

            for(int i = 0; i < BinBoundariesInPercent.Length; i++)
            {
                if(BinBoundariesInPercent[i].Length < 2)
                {
                    throw new NotEnoughCentralityBinBoundariesSpecifiedException();
                }

                if(!IsOrderedArray(BinBoundariesInPercent[i]))
                {
                    throw new CentralityBinBoundariesDisorderedException();
                }

                if(BinBoundariesInPercent[i][0] < 0
                    || BinBoundariesInPercent[i][BinBoundariesInPercent[i].Length - 1] > 100)
                {
                    throw new CentralityBinBoundariesOutOfRangeException();
                }
            }
        }

        private void GetValuesFromFireball()
        {
            int step = 0;
            while(!BreakUpCalculation(step))
            {
                if(CancellationToken.IsCancellationRequested)
                {
                    break;
                }

                ImpactParams.Add(step * FireballParam.GridCellSizeFm);
                GetValuesFromFireball(ImpactParams[step]);

                step++;
            }
        }

        private bool BreakUpCalculation(
            int step
            )
        {
            // Sigma[0] = 0
            return step > 1 && (DSigmaDbs[step - 1] / Sigmas[step - 1]) < 1e-5;
        }

        private void GetValuesFromFireball(
            double impactParam
            )
        {
            double ncoll;
            double npart;
            double dsigmadb;
            double sigma;
            GetValuesFromFireball(impactParam, out ncoll, out npart, out dsigmadb, out sigma);

            Ncolls.Add(ncoll);
            Nparts.Add(npart);
            DSigmaDbs.Add(dsigmadb);
            Sigmas.Add(sigma);

            UpdateStatus(impactParam, ncoll, npart, dsigmadb, sigma);
        }

        private void GetValuesFromFireball(
            double impactParam,
            out double ncoll,
            out double npart,
            out double dsigmadb,
            out double sigma
            )
        {
            FireballParam param = FireballParam.Clone();
            param.ImpactParameterFm = impactParam;

            GlauberCalculation calc = new GlauberCalculation(param);
            ncoll = calc.GetTotalNumberCollisions();
            npart = calc.GetTotalNumberParticipants();

            dsigmadb = 2 * Math.PI * impactParam * (1.0 - Math.Exp(-ncoll));
            sigma = param.GridCellSizeFm * dsigmadb;
            if(Sigmas.Count > 0)
            {
                sigma += Sigmas[Sigmas.Count - 1];
            }
        }

        private void UpdateStatus(
            double impactParam,
            double ncoll,
            double npart,
            double dsigmadb,
            double sigma
            )
        {
            if(StatusValues != null)
            {
                StatusValues[0] = impactParam.ToString("G3");
                StatusValues[1] = ncoll.ToString("G4");
                StatusValues[2] = npart.ToString("G4");
                StatusValues[3] = dsigmadb.ToString("G4");
                StatusValues[4] = sigma.ToString("G4");
            }
        }

        private Fireball CreateFireball(
            double impactParam
            )
        {
            FireballParam param = FireballParam.Clone();
            param.ImpactParameterFm = impactParam;
            param.TransverseMomentaGeV = new double[] { 0 };
            param.ExpansionMode = ExpansionMode.Longitudinal;

            return new Fireball(param);
        }

        private FireballParam FireballParam;

        private int[][] BinBoundariesInPercent;

        private void CalculateBinBoundaries()
        {
            for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Length; binGroupIndex++)
            {
                for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex] + 1; binIndex++)
                {
                    int i = GetLastIndexBeforeBin(binGroupIndex, binIndex);
                    ImpactParamsAtBinBoundaries[binGroupIndex][binIndex] = i * FireballParam.GridCellSizeFm;
                    ParticipantsAtBinBoundaries[binGroupIndex][binIndex] = Nparts[i];
                }
            }
        }

        private int GetLastIndexBeforeBin(
            int binGroupIndex,
            int binIndex
            )
        {
            for(int i = 0; i < Sigmas.Count - 1; i++)
            {
                if(IsLastIndexBeforeBin(binGroupIndex, binIndex, i))
                {
                    return i;
                }
            }

            throw new Exception("Index of bin boundary could not be found.");
        }

        private bool IsLastIndexBeforeBin(
            int binGroupIndex,
            int binIndex,
            int i
            )
        {
            return Sigmas[i] / Sigmas[Sigmas.Count - 1] <= 0.01 * BinBoundariesInPercent[binGroupIndex][binIndex]
                && Sigmas[i + 1] / Sigmas[Sigmas.Count - 1] >= 0.01 * BinBoundariesInPercent[binGroupIndex][binIndex];
        }

        private void CalculateMeanParticipants()
        {
            for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Length; binGroupIndex++)
            {
                for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
                {
                    CalculateMeanParticipantsInBin(binGroupIndex, binIndex);
                }
            }
        }

        private void CalculateMeanParticipantsInBin(
            int binGroupIndex,
            int binIndex
            )
        {
            double norm = 0;
            for(int i = 0; i < Sigmas.Count; i++)
            {
                if(LiesInBin(ImpactParams[i], binGroupIndex, binIndex))
                {
                    MeanParticipantsInBin[binGroupIndex][binIndex] += Nparts[i] * DSigmaDbs[i];
                    norm += DSigmaDbs[i];
                }
            }

            MeanParticipantsInBin[binGroupIndex][binIndex] /= norm;
        }

        private int[] GetNumberCentralityBins()
        {
            int[] numberCentralityBins = new int[BinBoundariesInPercent.Length];
            for(int i = 0; i < numberCentralityBins.Length; i++)
            {
                numberCentralityBins[i] = BinBoundariesInPercent[i].Length - 1;
            }
            return numberCentralityBins;
        }

        private bool LiesInBin(
            double impactParam,
            int binGroupIndex,
            int binIndex
            )
        {
            return impactParam >= ImpactParamsAtBinBoundaries[binGroupIndex][binIndex]
                && impactParam < ImpactParamsAtBinBoundaries[binGroupIndex][binIndex + 1];
        }
    }

    [Serializable]
    public class NotEnoughCentralityBinBoundariesSpecifiedException : Exception
    {
        public NotEnoughCentralityBinBoundariesSpecifiedException()
            : base("Less than 2 CentralityBinBoundaries specified.")
        {
        }
    }

    [Serializable]
    public class CentralityBinBoundariesOutOfRangeException : ArgumentOutOfRangeException
    {
        public CentralityBinBoundariesOutOfRangeException()
            : base("CentralityBinBoundaries are out of range. Must be between 0 and 100.")
        {
        }
    }

    [Serializable]
    public class CentralityBinBoundariesDisorderedException : Exception
    {
        public CentralityBinBoundariesDisorderedException()
            : base("CentralityBinBoundaries are disordered.")
        {
        }
    }
}