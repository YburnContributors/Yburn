using System;
using System.Collections.Generic;
using System.Linq;
using Yburn.Fireball;
using Yburn.PhysUtil;
using Yburn.QQState;

namespace Yburn.Workers
{
	public class QQDataProvider
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

		public QQDataProvider(
			string dataPathFile,
			List<PotentialType> potentialTypes,
			DopplerShiftEvaluationType dopplerShiftEvaluationType,
			DecayWidthType decayWidthType,
			double qgpFormationTemperature,
			int numberAveragingAngles
			)
		{
			DataPathFile = dataPathFile;
			PotentialTypes = potentialTypes;
			DopplerShiftEvaluationType = dopplerShiftEvaluationType;
			DecayWidthType = decayWidthType;
			QGPFormationTemperature = qgpFormationTemperature;
			NumberAveragingAngles = numberAveragingAngles;

			DecayWidthAveragers = InitializeDecayWidthAveragers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double GetInMediumDecayWidth(
			BottomiumState state,
			double temperature,
			double velocity,
			double electricFieldStrength = 0,
			double magneticFieldStrength = 0
			)
		{
			return DecayWidthAveragers[state].GetInMediumDecayWidth(
				DopplerShiftEvaluationType, temperature, velocity, electricFieldStrength, magneticFieldStrength);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly string DataPathFile;

		private readonly DecayWidthType DecayWidthType;

		private readonly List<PotentialType> PotentialTypes;

		private readonly int NumberAveragingAngles;

		private readonly double QGPFormationTemperature;

		private readonly DopplerShiftEvaluationType DopplerShiftEvaluationType;

		private readonly Dictionary<BottomiumState, DecayWidthAverager> DecayWidthAveragers;

		private LinearInterpolation1D GetLinearInterpolationByTemperature(
			List<QQDataSet> dataSets,
			QQDataColumn dataColumn
			)
		{
			IOrderedEnumerable<QQDataSet> orderedDataSets
				= dataSets.OrderBy(dataSet => dataSet.Temperature);

			List<double> temperature = new List<double>(dataSets.Capacity);
			List<double> observable = new List<double>(dataSets.Capacity);

			foreach(QQDataSet dataSet in orderedDataSets)
			{
				temperature.Add(dataSet.Temperature);
				observable.Add(dataSet.GetData(dataColumn));
			}

			return new LinearInterpolation1D(temperature.ToArray(), observable.ToArray());
		}

		private Dictionary<BottomiumState, DecayWidthAverager> InitializeDecayWidthAveragers()
		{
			Dictionary<BottomiumState, DecayWidthAverager> averagers
				= new Dictionary<BottomiumState, DecayWidthAverager>();

			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				averagers.Add(state, CreateDecayWidthAverager(state));
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
