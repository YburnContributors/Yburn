using System;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public struct BottomiumVector
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static BottomiumVector CreateEmptyVector()
		{
			return new BottomiumVector(new double[BottomiumStatesCount]);
		}

		public static BottomiumVector operator *(
			BottomiumCascadeMatrix matrix,
			BottomiumVector vector
			)
		{
			BottomiumVector result = CreateEmptyVector();

			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				foreach(BottomiumState j in Enum.GetValues(typeof(BottomiumState)))
				{
					result[i] += matrix[i, j] * vector[j];
				}
			}

			return result;
		}

		public static BottomiumVector operator +(
			BottomiumVector left,
			BottomiumVector right
			)
		{
			BottomiumVector result = CreateEmptyVector();

			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				result[i] = left[i] + right[i];
			}

			return result;
		}

		public static BottomiumVector operator *(
			double scalar,
			BottomiumVector vector
			)
		{
			BottomiumVector result = CreateEmptyVector();

			foreach(BottomiumState i in Enum.GetValues(typeof(BottomiumState)))
			{
				result[i] = scalar * vector[i];
			}

			return result;
		}

		public static BottomiumVector operator *(
			BottomiumVector vector,
			double scalar
			)
		{
			return scalar * vector;
		}

		public static BottomiumVector operator /(
			BottomiumVector vector,
			double scalar
			)
		{
			return (1 / scalar) * vector;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly int BottomiumStatesCount =
			Enum.GetValues(typeof(BottomiumState)).Length;

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		private BottomiumVector(
			double[] entries
			)
		{
			Entries = entries;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public int Dimension
		{
			get
			{
				return Entries.Length;
			}
		}

		public double this[BottomiumState state]
		{
			get
			{
				return Entries[(int)state];
			}
			set
			{
				Entries[(int)state] = value;
			}
		}

		public string GetVectorString(
			string description = "Population",
			bool extractGammaTot3P = false,
			bool transposeVector = true
			)
		{
			return GetStringifiedRepresentation(
				description: description,
				extractGammaTot3P: extractGammaTot3P,
				transposeVector: transposeVector).ConcatenateStringTable(true, true);
		}

		public string[,] GetStringifiedRepresentation(
			string description = "Population",
			bool extractGammaTot3P = false,
			bool transposeVector = true
			)
		{
			string[,] stringifiedVector = new string[BottomiumStatesCount + 1, 2];

			stringifiedVector[0, 0] = "State";
			stringifiedVector[0, 1] = description;
			for(int i = 0; i < BottomiumStatesCount; i++)
			{
				stringifiedVector[i + 1, 0] = Enum.GetNames(typeof(BottomiumState))[i];
				stringifiedVector[i + 1, 1] = Entries[i].ToUIString();
			}

			if(extractGammaTot3P)
			{
				stringifiedVector[(int)BottomiumState.x3P + 1, 1] =
					(Entries[(int)BottomiumState.x3P] / Constants.GammaTot3P).ToUIString()
					+ "*GammaTot3P/eV";
			}

			if(transposeVector)
			{
				stringifiedVector = stringifiedVector.GetTransposedArray();
			}

			return stringifiedVector;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double[] Entries;
	}
}
