﻿namespace Yburn.Fireball
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
			int yDimension
			) : this(type, new double[xDimension, yDimension])
		{
		}

		public SimpleFireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension,
			SimpleFireballFieldDiscreteFunction function
			) : this(type, xDimension, yDimension)
		{
			AssertValidFunction(function);
			SetDiscreteValues(function);
		}

		public SimpleFireballField(
			FireballFieldType type,
			double[] xAxis,
			double[] yAxis,
			SimpleFireballFieldContinuousFunction function
			) : this(type, xAxis.Length, yAxis.Length)
		{
			AssertValidFunction(function);
			SetDiscreteValues(function, xAxis, yAxis);
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

		public void SetDiscreteValues(
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

		public void SetDiscreteValues(
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

		public double GetMaxValueForFixedY(
			int yIndex
			)
		{
			double maxValue = DiscreteValues[0, yIndex];

			for(int i = 0; i < XDimension; i++)
			{
				if(DiscreteValues[i, yIndex] > maxValue)
				{
					maxValue = DiscreteValues[i, yIndex];
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

		protected double[,] DiscreteValues;

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
	}
}
