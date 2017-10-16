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
			CoordinateSystem system
			) : base(type, system)
		{
			Values = new double[XDimension, YDimension];
		}

		public SimpleFireballField(
			FireballFieldType type,
			CoordinateSystem system,
			double[,] values
			) : this(type, system)
		{
			SetValues(values);
		}

		public SimpleFireballField(
			FireballFieldType type,
			CoordinateSystem system,
			SimpleFireballFieldFunction function
			) : this(type, system)
		{
			SetValues(function);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double this[int xIndex, int yIndex]
		{
			get
			{
				return Values[xIndex, yIndex];
			}
		}

		public double[,] GetValues()
		{
			return (double[,])Values.Clone();
		}

		public void SetValues(
			double[,] values
			)
		{
			AssertValidArray(values);
			Values = (double[,])values.Clone();
		}

		public void SetValues(
			SimpleFireballFieldFunction function
			)
		{
			AssertValidFunction(function);

			for(int i = 0; i < XDimension; i++)
			{
				for(int j = 0; j < YDimension; j++)
				{
					Values[i, j] = function(i, j);
				}
			}
		}

		public double IntegrateValues()
		{
			return System.SymmetryFactor * System.GridCellSize * System.GridCellSize
				* TrapezoidalRuleSummedValues();
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double[,] Values;

		private void AssertValidArray(
			double[,] array
			)
		{
			if(array == null || array.GetLength(0) != XDimension || array.GetLength(1) != YDimension)
			{
				throw new InvalidFireballFieldArrayException();
			}
		}

		private void AssertValidFunction(
			SimpleFireballFieldFunction function
			)
		{
			if(function == null)
			{
				throw new InvalidFireballFieldFunctionException();
			}
		}

		private double TrapezoidalRuleSummedValues()
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
	}
}
