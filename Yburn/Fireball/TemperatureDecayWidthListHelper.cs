/**************************************************************************************************
 * Reads from the input file and extract temperature and decay width values for the specified
 * potential- and decay width types.
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace Yburn.Fireball
{
	public static class TemperatureDecayWidthListHelper
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static List<KeyValuePair<double, double>>[] GetList(
			string dataPathFile,
			DecayWidthType decayWidthType,
			string[] potentialTypes
			)
		{
			List<KeyValuePair<double, double>>[] list
				= new List<KeyValuePair<double, double>>[NumberBottomiumStates];
			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				list[(int)state] = GetList(dataPathFile, state, decayWidthType, potentialTypes);
			}

			return list;
		}

		public static List<KeyValuePair<double, double>> GetList(
			string dataPathFile,
			BottomiumState state,
			DecayWidthType decayWidthType,
			string[] potentialTypes
			)
		{
			QQDataColumns dataColumn = GetDecayWidthColumn(decayWidthType);
			AssertValidInput(dataPathFile, dataColumn);

			return GetList(dataPathFile, dataColumn, potentialTypes, state);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static int NumberBottomiumStates = Enum.GetValues(typeof(BottomiumState)).Length;

		private static void AssertValidInput(
			string dataPathFile,
			QQDataColumns dataColumn
			)
		{
			if(!File.Exists(dataPathFile))
			{
				throw new FileNotFoundException("Data file can not be found.");
			}

			if(dataColumn != QQDataColumns.GammaDamp
				&& dataColumn != QQDataColumns.GammaDiss
				&& dataColumn != QQDataColumns.GammaTot)
			{
				throw new ArgumentException("Invalid DecayWidthType.");
			}
		}

		private static List<KeyValuePair<double, double>> GetList(
			string dataPathFile,
			QQDataColumns dataColumn,
			string[] potentialTypes,
			BottomiumState state
			)
		{
			int n;
			int l;
			GetBottomiumStateQuantumNumbers(state, out n, out l);

			return QQDataDoc.GetValueList(dataPathFile, dataColumn, n, l,
				"Singlet", potentialTypes, null, null);
		}

		private static QQDataColumns GetDecayWidthColumn(
			DecayWidthType decayWidthType
			)
		{
			QQDataColumns dataColumn = QQDataColumns.None;
			Enum.TryParse(decayWidthType.ToString(), out dataColumn);

			return dataColumn;
		}

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
	}
}