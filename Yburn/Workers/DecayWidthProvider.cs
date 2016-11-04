using System;
using System.Collections.Generic;
using Yburn.Fireball;
using Yburn.QQState;

namespace Yburn.Workers
{
	public class DecayWidthProvider
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static List<QQDataSet> GetBoundStateDataSets(
			string dataPathFile,
			List<PotentialType> potentialTypes,
			BottomiumState state
			)
		{
			int n;
			int l;
			GetBottomiumStateQuantumNumbers(state, out n, out l);

			return QQDataDoc.GetDataSets(dataPathFile, n, l, ColorState.Singlet, potentialTypes);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void GetBottomiumStateQuantumNumbers(
			BottomiumState state,
			out int n,
			out int l
			)
		{
			switch(state)
			{
				case BottomiumState.Y1S:
					n = 1;
					l = 0;
					break;

				case BottomiumState.x1P:
					n = 2;
					l = 1;
					break;

				case BottomiumState.Y2S:
					n = 2;
					l = 0;
					break;

				case BottomiumState.x2P:
					n = 3;
					l = 1;
					break;

				case BottomiumState.Y3S:
					n = 3;
					l = 0;
					break;

				case BottomiumState.x3P:
					n = 4;
					l = 1;
					break;

				default:
					throw new Exception("Invalid BottomiumState.");
			}
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public DecayWidthProvider(
			string dataPathFile,
			List<PotentialType> potentialTypes,
			DecayWidthEvaluationType decayWidthEvaluationType,
			DecayWidthType decayWidthType,
			double qgpFormationTemperature,
			int numberAveragingAngles
			)
		{
			DataPathFile = dataPathFile;
			PotentialTypes = potentialTypes;
			DecayWidthEvaluationType = decayWidthEvaluationType;
			DecayWidthType = decayWidthType;
			QGPFormationTemperature = qgpFormationTemperature;
			NumberAveragingAngles = numberAveragingAngles;

			DecayWidthAveragers = InitializeDecayWidthAveragers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double GetDecayWidth(
			BottomiumState state,
			double qgpTemperature,
			double velocity
			)
		{
			return DecayWidthAveragers[(int)state]
				.GetEffectiveDecayWidth(qgpTemperature, velocity, DecayWidthEvaluationType);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly string DataPathFile;

		private readonly DecayWidthType DecayWidthType;

		private readonly List<PotentialType> PotentialTypes;

		private readonly int NumberAveragingAngles;

		private readonly double QGPFormationTemperature;

		private readonly DecayWidthEvaluationType DecayWidthEvaluationType;

		private readonly List<DecayWidthAverager> DecayWidthAveragers;

		private List<DecayWidthAverager> InitializeDecayWidthAveragers()
		{
			List<DecayWidthAverager> averagers = new List<DecayWidthAverager>();
			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				averagers.Add(CreateDecayWidthAverager(state));
			}

			return averagers;
		}

		private DecayWidthAverager CreateDecayWidthAverager(
			BottomiumState state
			)
		{
			List<QQDataSet> dataSets = GetBoundStateDataSets(DataPathFile, PotentialTypes, state);

			return new DecayWidthAverager(
				dataSets, DecayWidthType, QGPFormationTemperature, NumberAveragingAngles);
		}
	}
}
