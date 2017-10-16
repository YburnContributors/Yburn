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
			GetBottomiumStateQuantumNumbers(state, out int n, out int l);

			return QQDataDoc.GetDataSets(dataPathFile, n, l, ColorState.Singlet, potentialTypes);
		}

		public static double AverageHyperfineBottomiumProperties(
			BottomiumState state,
			Dictionary<int, double> properties
			)
		{
			GetBottomiumStateQuantumNumbers(state, out int n, out int l);
			int s = 1;

			double average = 0;
			for(int j = Math.Abs(l - s); j <= l + s; j++)
			{
				average += (2 * j + 1) * properties[j];
			}

			return average / (2 * l + 1) / (2 * s + 1);
		}

		public static Dictionary<int, double> GetHyperfineEnergySplitting(
			BottomiumState state
			)
		{
			switch(state)
			{
				case BottomiumState.Y1S:
					return new Dictionary<int, double>
					{
						[1] = Constants.RestMassY1S_MeV - Constants.RestMassEta1S_MeV
					};

				case BottomiumState.x1P:
					return new Dictionary<int, double>
					{
						[0] = Constants.RestMassX1P0_MeV - Constants.RestMassH1P_MeV,
						[1] = Constants.RestMassX1P1_MeV - Constants.RestMassH1P_MeV,
						[2] = Constants.RestMassX1P2_MeV - Constants.RestMassH1P_MeV
					};

				case BottomiumState.Y2S:
					return new Dictionary<int, double>
					{
						[1] = Constants.RestMassY2S_MeV - Constants.RestMassEta2S_MeV
					};

				case BottomiumState.x2P:
					return new Dictionary<int, double>
					{
						[0] = Constants.RestMassX2P0_MeV - Constants.RestMassH2P_MeV,
						[1] = Constants.RestMassX2P1_MeV - Constants.RestMassH2P_MeV,
						[2] = Constants.RestMassX2P2_MeV - Constants.RestMassH2P_MeV
					};

				case BottomiumState.Y3S:
				case BottomiumState.x3P:
					throw new Exception("No data available for this BottomiumState.");

				default:
					throw new Exception("Invalid BottomiumState.");
			}
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

		private static int GetMagneticDipoleTermSign(
			BottomiumState state
			)
		{
			switch(state)
			{
				case BottomiumState.Y1S:
				case BottomiumState.x1P:
				case BottomiumState.Y2S:
				case BottomiumState.x2P:
					return Math.Sign(AverageHyperfineBottomiumProperties(
						state, GetHyperfineEnergySplitting(state)));

				case BottomiumState.Y3S:
				case BottomiumState.x3P:
					return 0;

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
			ElectricDipoleAlignment electricDipoleAlignment,
			DecayWidthType decayWidthType,
			double qgpFormationTemperature,
			int numberAveragingAngles
			)
		{
			DopplerShiftEvaluationType = dopplerShiftEvaluationType;
			ElectricDipoleAlignment = electricDipoleAlignment;
			DecayWidthType = decayWidthType;
			QGPFormationTemperature = qgpFormationTemperature;
			NumberAveragingAngles = numberAveragingAngles;

			DataSets = InitializeDataSets(dataPathFile, potentialTypes);
			DecayWidthAveragers = InitializeDecayWidthAveragers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public DecayWidthAverager CreateDecayWidthAverager(
			BottomiumState state
			)
		{
			return new DecayWidthAverager(
				CreateDecayWidthInterpolation(state),
				CreateLinearInterpolationByTemperature(DataSets[state], QQDataColumn.Energy),
				CreateLinearInterpolationByTemperature(DataSets[state], QQDataColumn.DisplacementRMS),
				DopplerShiftEvaluationType, ElectricDipoleAlignment, GetMagneticDipoleTermSign(state),
				QGPFormationTemperature, NumberAveragingAngles);
		}

		public double GetInMediumDecayWidth(
			BottomiumState state,
			double temperature,
			double velocity,
			double electricField,
			double magneticField
			)
		{
			return DecayWidthAveragers[state].GetInMediumDecayWidth(
				temperature, velocity, electricField, magneticField);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly Dictionary<BottomiumState, List<QQDataSet>> DataSets;

		private readonly DecayWidthType DecayWidthType;

		private readonly int NumberAveragingAngles;

		private readonly double QGPFormationTemperature;

		private readonly DopplerShiftEvaluationType DopplerShiftEvaluationType;

		private readonly ElectricDipoleAlignment ElectricDipoleAlignment;

		private readonly Dictionary<BottomiumState, DecayWidthAverager> DecayWidthAveragers;

		private Dictionary<BottomiumState, List<QQDataSet>> InitializeDataSets(
			string dataPathFile,
			List<PotentialType> potentialTypes
			)
		{
			Dictionary<BottomiumState, List<QQDataSet>> dataSets
				= new Dictionary<BottomiumState, List<QQDataSet>>();

			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				dataSets.Add(state, GetBoundStateDataSets(dataPathFile, potentialTypes, state));
			}

			return dataSets;
		}

		private LinearInterpolation1D CreateLinearInterpolationByTemperature(
			List<QQDataSet> dataSets,
			QQDataColumn dataColumn
			)
		{
			List<double> temperature = new List<double>(dataSets.Capacity);
			List<double> observable = new List<double>(dataSets.Capacity);

			if(dataSets.Count > 0)
			{
				IOrderedEnumerable<QQDataSet> orderedDataSets
					= dataSets.OrderBy(dataSet => dataSet.Temperature);

				foreach(QQDataSet dataSet in orderedDataSets)
				{
					temperature.Add(dataSet.Temperature);
					observable.Add(dataSet.GetData(dataColumn));
				}
			}
			else
			{
				temperature.Add(0);
				observable.Add(0);
			}

			return new LinearInterpolation1D(temperature.ToArray(), observable.ToArray());
		}

		private LinearInterpolation1D CreateDecayWidthInterpolation(
			BottomiumState state
			)
		{
			switch(DecayWidthType)
			{
				case DecayWidthType.None:
					return CreateLinearInterpolationByTemperature(
						DataSets[state], QQDataColumn.None);

				case DecayWidthType.GammaDamp:
					return CreateLinearInterpolationByTemperature(
						DataSets[state], QQDataColumn.GammaDamp);

				case DecayWidthType.GammaDiss:
					return CreateLinearInterpolationByTemperature(
						DataSets[state], QQDataColumn.GammaDiss);

				case DecayWidthType.GammaTot:
					return CreateLinearInterpolationByTemperature(
						DataSets[state], QQDataColumn.GammaTot);

				default:
					throw new Exception("Invalid DecayWidthType.");
			}
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
	}
}
