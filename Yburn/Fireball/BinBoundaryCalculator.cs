using System;
using System.Collections.Generic;
using System.Linq;
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
			List<List<int>> binBoundariesInPercent
			)
		{
			PrepareCalculation(binBoundariesInPercent);

			AssertInputValid();

			GetValuesFromFireball();
			CalculateBinBoundaries();
			CalculateMeanParticipants();
		}

		public List<int> NumberCentralityBins
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

		public List<List<double>> ImpactParamsAtBinBoundaries
		{
			get;
			private set;
		}

		public List<List<double>> ParticipantsAtBinBoundaries
		{
			get;
			private set;
		}

		public List<List<double>> MeanParticipantsInBin
		{
			get;
			private set;
		}

		public string[] StatusValues;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CancellationToken CancellationToken;

		private void PrepareCalculation(
			List<List<int>> binBoundariesInPercent
			)
		{
			BinBoundariesInPercent = binBoundariesInPercent;
			NumberCentralityBins = GetNumberCentralityBins();
		}

		private void AssertInputValid()
		{
			if(StatusValues != null && StatusValues.Length != 5)
			{
				throw new Exception("Length of StatusValues must be five.");
			}

			foreach(List<int> binBoundaries in BinBoundariesInPercent)
			{
				if(binBoundaries.Count < 2)
				{
					throw new NotEnoughCentralityBinBoundariesSpecifiedException();
				}

				binBoundaries.Sort();

				if(binBoundaries.First() < 0 || binBoundaries.Last() > 100)
				{
					throw new CentralityBinBoundariesOutOfRangeException();
				}
			}
		}

		private void GetValuesFromFireball()
		{
			ImpactParams = new List<double>();
			Ncolls = new List<double>();
			Nparts = new List<double>();
			DSigmaDbs = new List<double>();
			Sigmas = new List<double>();

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

		private FireballParam FireballParam;

		private List<List<int>> BinBoundariesInPercent;

		private void CalculateBinBoundaries()
		{
			ImpactParamsAtBinBoundaries = new List<List<double>>();
			ParticipantsAtBinBoundaries = new List<List<double>>();

			for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Count; binGroupIndex++)
			{
				ImpactParamsAtBinBoundaries.Add(new List<double>());
				ParticipantsAtBinBoundaries.Add(new List<double>());

				for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex] + 1; binIndex++)
				{
					int i = GetLastIndexBeforeBin(binGroupIndex, binIndex);
					ImpactParamsAtBinBoundaries.Last().Add(i * FireballParam.GridCellSizeFm);
					ParticipantsAtBinBoundaries.Last().Add(Nparts[i]);
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
			MeanParticipantsInBin = new List<List<double>>();

			for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Count; binGroupIndex++)
			{
				MeanParticipantsInBin.Add(new List<double>());

				for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
				{
					MeanParticipantsInBin.Last()
						.Add(CalculateMeanParticipantsInBin(binGroupIndex, binIndex));
				}
			}
		}

		private double CalculateMeanParticipantsInBin(
			int binGroupIndex,
			int binIndex
			)
		{
			double meanParticipants = 0;
			double norm = 0;

			for(int i = 0; i < Sigmas.Count; i++)
			{
				if(LiesInBin(ImpactParams[i], binGroupIndex, binIndex))
				{
					meanParticipants += Nparts[i] * DSigmaDbs[i];
					norm += DSigmaDbs[i];
				}
			}

			return meanParticipants / norm;
		}

		private List<int> GetNumberCentralityBins()
		{
			List<int> numberCentralityBins = new List<int>();
			foreach(List<int> binBoundaries in BinBoundariesInPercent)
			{
				numberCentralityBins.Add(binBoundaries.Count - 1);
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
}