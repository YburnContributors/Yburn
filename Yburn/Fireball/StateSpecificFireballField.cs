using System;

namespace Yburn.Fireball
{
	public delegate double StateSpecificFireballFieldFunction(
		int xIndex, int yIndex, int pTIndex, int stateIndex);

	public class StateSpecificFireballField : FireballField
	{
		/********************************************************************************************
		* Constructors
		********************************************************************************************/

		public StateSpecificFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension,
			int pTDimension,
			StateSpecificFireballFieldFunction function
			) : this(type, xDimension, yDimension, pTDimension)
		{
			SetValues(function);
		}

		public StateSpecificFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension,
			int pTDimension
			) : base(type, xDimension, yDimension)
		{
			PTDimension = pTDimension;
			InitValues();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double[,][,] Values
		{
			get;
			set;
		}

		public SimpleFireballField GetSimpleFireballField(
			int pTIndex,
			BottomiumState state
			)
		{
			return new SimpleFireballField(Type, Values[pTIndex, (int)state]);
		}

		public void SetValues(
			StateSpecificFireballFieldFunction function
			)
		{
			AssertValidFunction(function);
			DoSetValues(function);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		protected static readonly int NumberBottomiumStates
			= Enum.GetValues(typeof(BottomiumState)).Length;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected int PTDimension;

		private void InitValues()
		{
			Values = new double[PTDimension, NumberBottomiumStates][,];
			for(int pT = 0; pT < PTDimension; pT++)
			{
				for(int state = 0; state < NumberBottomiumStates; state++)
				{
					Values[pT, state] = new double[XDimension, YDimension];
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
