namespace Yburn.Fireball
{
	public delegate double SimpleFireballFieldFunction(int xIndex, int yIndex);

	public class SimpleFireballField : FireballField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public SimpleFireballField(
			FireballFieldType type,
			double[,] values
			) : base(type, values.GetLength(0), values.GetLength(1))
		{
			Values = values;
		}

		public SimpleFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension,
			SimpleFireballFieldFunction function
			) : this(type, xDimension, yDimension)
		{
			AssertValidFunction(function);
			SetValues(function);
		}

		public SimpleFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension
			) : base(type, xDimension, yDimension)
		{
			Values = new double[XDimension, YDimension];
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double[,] Values
		{
			get;
			set;
		}

		public double TrapezoidalRuleSummedValues()
		{
			double sum = 0.5 * Values[0, 0];

			for(int i = 1; i < XDimension; i++)
			{
				sum += Values[i, 0];
			}
			for(int i = 1; i < YDimension; i++)
			{
				sum += Values[0, i];
			}
			sum *= 0.5;

			for(int i = 1; i < XDimension; i++)
			{
				for(int j = 1; j < YDimension; j++)
				{
					sum += Values[i, j];
				}
			}

			return sum;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void AssertValidFunction(
			SimpleFireballFieldFunction function
			)
		{
			if(function == null)
			{
				throw new InvalidFireballFieldFunctionException();
			}
		}

		protected void SetValues(
			SimpleFireballFieldFunction function
			)
		{
			for(int i = 0; i < XDimension; i++)
			{
				for(int j = 0; j < YDimension; j++)
				{
					Values[i, j] = function(i, j);
				}
			}
		}
	}
}