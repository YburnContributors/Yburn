using System;
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

			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				matrix[i, i] = 1;
			}

			return matrix;
		}

		public static BottomiumCascadeMatrix CreateDiagonalMatrix(
			BottomiumVector diagonalElements
			)
		{
			BottomiumCascadeMatrix matrix = CreateEmptyMatrix();

			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				matrix[i, i] = diagonalElements[i];
			}

			return matrix;
		}

		public static BottomiumCascadeMatrix operator *(
			BottomiumCascadeMatrix left,
			BottomiumCascadeMatrix right
			)
		{
			BottomiumCascadeMatrix result = CreateEmptyMatrix();

			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				foreach(BottomiumState j in Enum.GetValues(typeof(BottomiumState)))
				{
					foreach(BottomiumState k in Enum.GetValues(typeof(BottomiumState)))
					{
						result[i, j] += left[i, k] * right[k, j];
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
			bool extractGammaTot3P = false
			)
		{
			return GetStringifiedRepresentation(extractGammaTot3P).ConcatenateStringTable(true, true);
		}

		public string[,] GetStringifiedRepresentation(
			bool extractGammaTot3P = false
			)
		{
			string[,] stringifiedMatrix =
				new string[BottomiumStatesCount + 1, BottomiumStatesCount + 1];

			stringifiedMatrix[0, 0] = "State";
			for(int i = 0; i < BottomiumStatesCount; i++)
			{
				stringifiedMatrix[i + 1, 0] = Enum.GetNames(typeof(BottomiumState))[i];
				stringifiedMatrix[0, i + 1] = Enum.GetNames(typeof(BottomiumState))[i];

				for(int j = 0; j < BottomiumStatesCount; j++)
				{
					stringifiedMatrix[i + 1, j + 1] = Entries[i, j].ToUIString();
				}
			}

			if(extractGammaTot3P)
			{
				foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
				{
					if(i != BottomiumState.x3P)
					{
						if(stringifiedMatrix[(int)BottomiumState.x3P + 1, (int)i + 1] != "0")
						{
							stringifiedMatrix[(int)BottomiumState.x3P + 1, (int)i + 1] =
								(Entries[(int)BottomiumState.x3P, (int)i] / Constants.GammaTot3P)
								.ToUIString() + "*GammaTot3P/eV";
						}

						if(stringifiedMatrix[(int)i + 1, (int)BottomiumState.x3P + 1] != "0")
						{
							stringifiedMatrix[(int)i + 1, (int)BottomiumState.x3P + 1] =
								(Entries[(int)i, (int)BottomiumState.x3P] * Constants.GammaTot3P)
								.ToUIString() + "/GammaTot3P*eV";
						}
					}
				}
			}

			return stringifiedMatrix;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double[,] Entries;
	}
}
