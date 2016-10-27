using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public struct BottomiumCascadeMatrix
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static BottomiumCascadeMatrix CreateEmptyMatrix()
		{
			return new BottomiumCascadeMatrix(new double[BottomiumStatesCount, BottomiumStatesCount]);
		}

		public static BottomiumCascadeMatrix CreateUnitMatrix()
		{
			BottomiumCascadeMatrix matrix = CreateEmptyMatrix();

			foreach(BottomiumState s in Enum.GetValues(typeof(BottomiumState)))
			{
				matrix[s, s] = 1;
			}

			return matrix;
		}

		public static BottomiumCascadeMatrix CreateDiagonalMatrix(
			BottomiumVector diagonalElements
			)
		{
			BottomiumCascadeMatrix matrix = CreateEmptyMatrix();

			foreach(BottomiumState s in Enum.GetValues(typeof(BottomiumState)))
			{
				matrix[s, s] = diagonalElements[s];
			}

			return matrix;
		}

		public static BottomiumCascadeMatrix operator *(
			BottomiumCascadeMatrix left,
			BottomiumCascadeMatrix right
			)
		{
			BottomiumCascadeMatrix result = CreateEmptyMatrix();

			foreach(BottomiumState s in Enum.GetValues(typeof(BottomiumState)))
			{
				foreach(BottomiumState t in Enum.GetValues(typeof(BottomiumState)))
				{
					foreach(BottomiumState u in Enum.GetValues(typeof(BottomiumState)))
					{
						result[s, t] += left[s, u] * right[u, t];
					}
				}
			}

			return result;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly int BottomiumStatesCount =
			Enum.GetValues(typeof(BottomiumState)).Length;

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		private BottomiumCascadeMatrix(
			double[,] entries
			)
		{
			Entries = entries;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public int[] Dimensions
		{
			get
			{
				return new int[] { Entries.GetLength(0), Entries.GetLength(1) };
			}
		}

		public double this[BottomiumState state1, BottomiumState state2]
		{
			get
			{
				return Entries[(int)state1, (int)state2];
			}
			set
			{
				Entries[(int)state1, (int)state2] = value;
			}
		}

		public string GetMatrixString(
			string description = "State",
			bool extractGammaTot3P = false
			)
		{
			List<string> labels = new List<string>(Enum.GetNames(typeof(BottomiumState)));

			return new Table<string>(GetStringifiedRepresentation(extractGammaTot3P))
				.ToFormattedTableString(labels, labels, description);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double[,] Entries;

		private string[,] GetStringifiedRepresentation(
			bool extractGammaTot3P
			)
		{
			string[,] stringifiedEntries = new string[BottomiumStatesCount, BottomiumStatesCount];
			for(int i = 0; i < BottomiumStatesCount; i++)
			{
				for(int j = 0; j < BottomiumStatesCount; j++)
				{
					stringifiedEntries[i, j] = Entries[i, j].ToUIString();
				}
			}

			if(extractGammaTot3P)
			{
				ExtractGammaTot3PFromStringifiedRepresentation(stringifiedEntries);
			}

			return stringifiedEntries;
		}

		private void ExtractGammaTot3PFromStringifiedRepresentation(
			string[,] stringifiedEntries
			)
		{
			foreach(BottomiumState s in Enum.GetValues(typeof(BottomiumState)))
			{
				if(s != BottomiumState.x3P)
				{
					if(stringifiedEntries[(int)BottomiumState.x3P, (int)s] != "0")
					{
						stringifiedEntries[(int)BottomiumState.x3P, (int)s] =
							(this[BottomiumState.x3P, s] / Constants.GammaTot3P).ToUIString()
							+ "*GammaTot3P/eV";
					}

					if(stringifiedEntries[(int)s, (int)BottomiumState.x3P] != "0")
					{
						stringifiedEntries[(int)s, (int)BottomiumState.x3P] =
							(this[s, BottomiumState.x3P] * Constants.GammaTot3P).ToUIString()
							+ "/GammaTot3P*eV";
					}
				}
			}
		}
	}
}
