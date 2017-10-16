using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Yburn.Fireball
{
	public delegate double StateSpecificFireballFieldFunction(
		int xIndex, int yIndex, int pTIndex, int stateIndex);

	public class StateSpecificFireballField : FireballField
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		protected static readonly ReadOnlyCollection<BottomiumState> BottomiumStates
			= Array.AsReadOnly((BottomiumState[])Enum.GetValues(typeof(BottomiumState)));

		protected static int NumberBottomiumStates
		{
			get
			{
				return BottomiumStates.Count;
			}
		}

		/********************************************************************************************
		* Constructors
		********************************************************************************************/

		public StateSpecificFireballField(
			FireballFieldType type,
			CoordinateSystem system,
			IList<double> transverseMomenta
			) : base(type, system)
		{
			TransverseMomenta = new ReadOnlyCollection<double>(transverseMomenta);
			InitValues(out Values);
		}

		public StateSpecificFireballField(
			FireballFieldType type,
			CoordinateSystem system,
			IList<double> transverseMomenta,
			StateSpecificFireballFieldFunction function
			) : this(type, system, transverseMomenta)
		{
			SetValues(function);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double this[int xIndex, int yIndex, int pTIndex, int stateIndex]
		{
			get
			{
				return Values[pTIndex, stateIndex][xIndex, yIndex];
			}
		}

		public SimpleFireballField GetSimpleFireballField(
			int pTIndex,
			BottomiumState state
			)
		{
			return new SimpleFireballField(Type, System, Values[pTIndex, (int)state]);
		}

		public void SetValues(
			StateSpecificFireballFieldFunction function
			)
		{
			AssertValidFunction(function);
			DoSetValues(function);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double[,][,] Values;

		protected readonly ReadOnlyCollection<double> TransverseMomenta;

		protected int PTDimension
		{
			get
			{
				return TransverseMomenta.Count;
			}
		}

		private void InitValues(
			out double[,][,] values
			)
		{
			values = new double[PTDimension, NumberBottomiumStates][,];
			for(int pT = 0; pT < PTDimension; pT++)
			{
				for(int state = 0; state < NumberBottomiumStates; state++)
				{
					values[pT, state] = new double[XDimension, YDimension];
				}
			}
		}

		private void AssertValidFunction(
			StateSpecificFireballFieldFunction function
			)
		{
			if(function == null)
			{
				throw new InvalidFireballFieldFunctionException();
			}
		}

		private void DoSetValues(
			StateSpecificFireballFieldFunction function
			)
		{
			for(int pT = 0; pT < PTDimension; pT++)
			{
				for(int state = 0; state < NumberBottomiumStates; state++)
				{
					for(int x = 0; x < XDimension; x++)
					{
						for(int y = 0; y < YDimension; y++)
						{
							Values[pT, state][x, y] = function(x, y, pT, state);
						}
					}
				}
			}
		}
	}
}
