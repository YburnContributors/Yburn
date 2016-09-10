namespace Yburn.Fireball
{
	public delegate double SimpleFireballFieldDiscreteFunction(int xIndex, int yIndex);

	public delegate double SimpleFireballFieldContinuousFunction(double x, double y);

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
			DiscreteValues = values;
		}

		public SimpleFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension,
			SimpleFireballFieldDiscreteFunction function
			) : this(type, xDimension, yDimension)
		{
			AssertValidFunction(function);
			InitializeDiscreteValues(function);
		}

		public SimpleFireballField(
			FireballFieldType type,
			double[] xAxis,
			double[] yAxis,
			SimpleFireballFieldContinuousFunction function
			) : this(type, xAxis.Length, yAxis.Length)
		{
			AssertValidFunction(function);
			InitializeDiscreteValues(function, xAxis, yAxis);
		}

		public SimpleFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension
			) : base(type, xDimension, yDimension)
		{
			DiscreteValues = new double[XDimension, YDimension];
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public double this[int xIndex, int yIndex]
		{
			get
			{
				return DiscreteValues[xIndex, yIndex];
			}
		}

		public double[,] GetDiscreteValues()
		{
			return (double[,])DiscreteValues.Clone();
		}

		public void SetDiscreteValues(double[,] discreteValues)
		{
			DiscreteValues = (double[,])discreteValues.Clone();
		}

		public double GetMaxValue()
		{
			double maxValue = DiscreteValues[0, 0];
			for(int i = 0; i < XDimension; i++)
			{
				for(int j = 0; j < YDimension; j++)
				{
					if(DiscreteValues[i, j] > maxValue)
					{
						maxValue = DiscreteValues[i, j];
					}
				}
			}
			return maxValue;
		}

		public double TrapezoidalRuleSummedValues()
		{
			double sum = 0.5 * DiscreteValues[0, 0];

			for(int i = 1; i < XDimension; i++)
			{
				sum += DiscreteValues[i, 0];
			}
			for(int i = 1; i < YDimension; i++)
			{
				sum += DiscreteValues[0, i];
			}
			sum *= 0.5;

			for(int i = 1; i < XDimension; i++)
			{
				for(int j = 1; j < YDimension; j++)
				{
					sum += DiscreteValues[i, j];
				}
			}

			return sum;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void AssertValidFunction(
			SimpleFireballFieldDiscreteFunction function
			)
		{
			if(function == null)
			{
				throw new InvalidFireballFieldFunctionException();
			}
		}

		private void AssertValidFunction(
			SimpleFireballFieldContinuousFunction function
			)
		{
			if(function == null)
			{
				throw new InvalidFireballFieldFunctionException();
			}
		}

		protected double[,] DiscreteValues;

		protected void InitializeDiscreteValues(
			SimpleFireballFieldDiscreteFunction function
			)
		{
			for(int i = 0; i < XDimension; i++)
			{
				for(int j = 0; j < YDimension; j++)
				{
					DiscreteValues[i, j] = function(i, j);
				}
			}
		}

		protected void InitializeDiscreteValues(
			SimpleFireballFieldContinuousFunction function,
			double[] xAxis,
			double[] yAxis
			)
		{
			for(int i = 0; i < XDimension; i++)
			{
				for(int j = 0; j < YDimension; j++)
				{
					DiscreteValues[i, j] = function(xAxis[i], yAxis[j]);
				}
			}
		}
	}
}