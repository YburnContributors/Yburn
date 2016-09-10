using System;

namespace Yburn.Fireball
{
	public delegate double StateSpecificFireballFieldFunction(
		int ptIndex, int stateIndex, int xIndex, int yIndex);

	public class StateSpecificFireballField : FireballField
	{
		/********************************************************************************************
		* Constructors
		********************************************************************************************/

		public StateSpecificFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension,
			int ptDimension,
			StateSpecificFireballFieldFunction function
			) : this(type, xDimension, yDimension, ptDimension)
		{
			SetValues(function);
		}

		public StateSpecificFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension,
			int ptDimension
			) : base(type, xDimension, yDimension)
		{
			PtDimension = ptDimension;
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
			int ptIndex,
			BottomiumState state
			)
		{
			return new SimpleFireballField(Type, Values[ptIndex, (int)state]);
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

		protected int PtDimension;

		private void InitValues()
		{
			Values = new double[PtDimension, NumberBottomiumStates][,];
			for(int k = 0; k < PtDimension; k++)
			{
				for(int l = 0; l < NumberBottomiumStates; l++)
				{
					Values[k, l] = new double[XDimension, YDimension];
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
			for(int k = 0; k < PtDimension; k++)
			{
				for(int l = 0; l < NumberBottomiumStates; l++)
				{
					for(int i = 0; i < XDimension; i++)
					{
						for(int j = 0; j < YDimension; j++)
						{
							Values[k, l][i, j] = function(i, j, k, l);
						}
					}
				}
			}
		}
	}
}