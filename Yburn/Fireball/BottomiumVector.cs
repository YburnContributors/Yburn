using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class BottomiumVector
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static BottomiumVector operator +(
			BottomiumVector left,
			BottomiumVector right
			)
		{
			BottomiumVector result = new BottomiumVector();

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
			BottomiumVector result = new BottomiumVector();

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

		private static readonly int BottomiumStatesCount
			= Enum.GetValues(typeof(BottomiumState)).Length;

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public BottomiumVector()
		{
		}

		public BottomiumVector(
			Dictionary<BottomiumState, double> entries
			) : this()
		{
			foreach(BottomiumState state in entries.Keys)
			{
				this[state] = entries[state];
			}
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
			string[] labels = Enum.GetNames(typeof(BottomiumState));
			Table<string> table = new Table<string>(GetStringifiedRepresentation(extractGammaTot3P));

			if(transposeVector)
			{
				table.Transpose();
				return table.ToFormattedTableString(labels, new string[] { description });
			}
			else
			{
				return table.ToFormattedTableString(new string[] { description }, labels);
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double[] Entries = new double[BottomiumStatesCount];

		private string[] GetStringifiedRepresentation(
			bool extractGammaTot3P
			)
		{
			string[] stringifiedEntries = Array.ConvertAll(Entries, x => x.ToUIString());

			if(extractGammaTot3P)
			{
				ExtractGammaTot3PFromStringifiedRepresentation(stringifiedEntries);
			}

			return stringifiedEntries;
		}

		private void ExtractGammaTot3PFromStringifiedRepresentation(
			string[] stringifiedEntries
			)
		{
			if(stringifiedEntries[(int)BottomiumState.x3P] != "0")
			{
				stringifiedEntries[(int)BottomiumState.x3P]
					= (this[BottomiumState.x3P] / Constants.GammaTot3P).ToUIString() + "*GammaTot3P/eV";
			}
		}
	}
}
