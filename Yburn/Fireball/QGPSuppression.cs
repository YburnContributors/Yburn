using System;
using System.Collections.Generic;
using System.Linq;
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
			List<int> numberCentralityBins,
			List<List<double>> impactParamsAtBinBoundaries,
			CancellationToken cancellationToken
			)
		{
			FireballParam = fireballParam.Clone();
			NumberCentralityBins = numberCentralityBins;
			ImpactParamsAtBinBoundaries = impactParamsAtBinBoundaries;
			CancellationToken = cancellationToken;

			FlatImpactParamsAtBinBoundaries = GetFlatImpactParams();
			NumberFlatBins = FlatImpactParamsAtBinBoundaries.Count - 1;
			ArrayReshapingMask = GetArrayReshapingMask();
		}

		/********************************************************************************************
	     * Public members, functions and properties
	     ********************************************************************************************/

		public List<double> FlatImpactParamsAtBinBoundaries
		{
			get;
			private set;
		}

		public List<List<int>> ArrayReshapingMask
		{
			get;
			private set;
		}

		public BottomiumVector[][][] CalculateQGPSuppressionFactors()
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
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private string[] StatusValues;

		private CancellationToken CancellationToken;

		private FireballParam FireballParam;

		private List<List<double>> ImpactParamsAtBinBoundaries;

		private List<int> NumberCentralityBins;

		private int NumberFlatBins;

		private BottomiumVector[][][] QGPSuppressionFactors;

		private BottomiumVector[][] FlatQGPSuppressionFactors;

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
			CurrentImpactParam = FireballParam.GridCellSize_fm;
		}

		private List<double> GetFlatImpactParams()
		{
			SortedSet<double> impactParamsSet = new SortedSet<double>();
			for(int i = 0; i < ImpactParamsAtBinBoundaries.Count; i++)
			{
				impactParamsSet.UnionWith(ImpactParamsAtBinBoundaries[i]);
			}

			return new List<double>(impactParamsSet);
		}

		private List<List<int>> GetArrayReshapingMask()
		{
			List<List<int>> boundariesMask = new List<List<int>>();

			for(int i = 0; i < NumberCentralityBins.Count; i++)
			{
				boundariesMask.Add(new List<int>());

				for(int j = 0; j < NumberCentralityBins[i] + 1; j++)
				{
					boundariesMask.Last().Add(
						FlatImpactParamsAtBinBoundaries.IndexOf(ImpactParamsAtBinBoundaries[i][j]));
				}
			}

			return boundariesMask;
		}

		private void InitQGPSuppressionFactors()
		{
			FlatQGPSuppressionFactors = new BottomiumVector[NumberFlatBins][];
			for(int binIndex = 0; binIndex < NumberFlatBins; binIndex++)
			{
				FlatQGPSuppressionFactors[binIndex]
					= new BottomiumVector[FireballParam.TransverseMomenta_GeV.Count];
				for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomenta_GeV.Count; pTIndex++)
				{
					FlatQGPSuppressionFactors[binIndex][pTIndex] = new BottomiumVector();
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
				double maximumTemperature = fireball.MaximumTemperature;

				CalculateFireballEvolution(fireball);
				CollectResults(fireball);
				UpdateStatus(maximumTemperature, fireball.LifeTime);

				CurrentImpactParam += FireballParam.GridCellSize_fm;
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
			param.ImpactParameter_fm = CurrentImpactParam;

			return new Fireball(param);
		}

		private void CalculateFireballEvolution(
			Fireball fireball
			)
		{
			while(fireball.MaximumTemperature > FireballParam.BreakupTemperature_MeV
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
				+= CurrentImpactParam * fireball.IntegrateFireballField(FireballFieldType.Overlap);

			for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomenta_GeV.Count; pTIndex++)
			{
				foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
				{
					FlatQGPSuppressionFactors[CurrentBinIndex][pTIndex][state]
						+= CurrentImpactParam * fireball.IntegrateFireballField(
							FireballFieldType.UnscaledSuppression, state, pTIndex);
				}
			}
		}

		private void UpdateStatus(
			double maximumTemperature,
			double lifeTime
			)
		{
			if(StatusValues != null)
			{
				StatusValues[0] = CurrentImpactParam.ToString("G5");
				StatusValues[1] = maximumTemperature.ToString("G5");
				StatusValues[2] = lifeTime.ToString("G5");
			}
		}

		private void ReshapeFlatArrays()
		{
			NormalizationFactors = new double[NumberCentralityBins.Count][];
			QGPSuppressionFactors = new BottomiumVector[NumberCentralityBins.Count][][];
			for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Count; binGroupIndex++)
			{
				NormalizationFactors[binGroupIndex] = new double[NumberCentralityBins[binGroupIndex]];
				QGPSuppressionFactors[binGroupIndex]
					 = new BottomiumVector[NumberCentralityBins[binGroupIndex]][];
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
				 = new BottomiumVector[FireballParam.TransverseMomenta_GeV.Count];
			for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomenta_GeV.Count; pTIndex++)
			{
				QGPSuppressionFactors[binGroupIndex][binIndex][pTIndex] = new BottomiumVector();
				for(int flatBinIndex = ArrayReshapingMask[binGroupIndex][binIndex];
					 flatBinIndex < ArrayReshapingMask[binGroupIndex][binIndex + 1];
					 flatBinIndex++)
				{
					QGPSuppressionFactors[binGroupIndex][binIndex][pTIndex]
					+= FlatQGPSuppressionFactors[flatBinIndex][pTIndex];
				}
			}
		}

		private void NormalizeQGPSuppressionFactors()
		{
			for(int binGroupIndex = 0; binGroupIndex < NumberCentralityBins.Count; binGroupIndex++)
			{
				for(int binIndex = 0; binIndex < NumberCentralityBins[binGroupIndex]; binIndex++)
				{
					for(int pTIndex = 0; pTIndex < FireballParam.TransverseMomenta_GeV.Count; pTIndex++)
					{
						QGPSuppressionFactors[binGroupIndex][binIndex][pTIndex]
							 /= NormalizationFactors[binGroupIndex][binIndex];
					}
				}
			}
		}
	}
}
