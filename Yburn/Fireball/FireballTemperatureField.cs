using System;

namespace Yburn.Fireball
{
	public class FireballTemperatureField : SimpleFireballField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballTemperatureField(
			int xDimension,
			int yDimension,
			bool isSymmetricInX,
			bool isSymmetricInY,
			SimpleFireballField temperatureScalingField,
			double initialMaximumTemperature,
			double thermalTime,
			double initialTime
			)
			: base(FireballFieldType.Temperature, xDimension, yDimension)
		{
			IsSymmetricInX = isSymmetricInX;
			IsSymmetricInY = isSymmetricInY;

			TemperatureScalingField = temperatureScalingField;
			InitialMaximumTemperature = initialMaximumTemperature;
			ThermalTime = thermalTime;
			InitialTime = initialTime;

			AssertValidInput();
			Initialize();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Advance(
			Ftexs solver
			)
		{
			DiscreteValues = solver.T;
			FindMaximumTemperature();
		}

		public void Advance(
			double newTime
			)
		{
			SetDiscreteValues((x, y) => Tnorm[x, y] / Math.Pow(newTime, 1 / 3.0));
			FindMaximumTemperature();
		}

		public double MaximumTemperature
		{
			get;
			private set;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// auxiliary field for the calculation of the temperature profile Temperature
		private SimpleFireballField Tnorm;

		private double InitialMaximumTemperature;

		private double InitialTime;

		private double ThermalTime;

		private SimpleFireballField TemperatureScalingField;

		private bool IsSymmetricInX;

		private bool IsSymmetricInY;

		private void AssertValidInput()
		{
			if(TemperatureScalingField == null)
			{
				throw new InvalidFireballFieldFunctionException();
			}

			if(InitialMaximumTemperature <= 0)
			{
				throw new Exception("InitialMaximumTemperature <= 0.");
			}

			if(ThermalTime <= 0)
			{
				throw new Exception("ThermalTime <= 0.");
			}

			if(InitialTime <= 0)
			{
				throw new Exception("InitialTime <= 0.");
			}
		}

		private void Initialize()
		{
			InitTnorm();
			Advance(InitialTime);
		}

		// temperature is normalized such that T(0, 0, ThermalTime_fm) = T0
		// for a central collision (ImpactParameter = 0) and TransverseMomentum = 0
		private void InitTnorm()
		{
			double norm = InitialMaximumTemperature * Math.Pow(ThermalTime, 1 / 3.0);
			Tnorm = new SimpleFireballField(FireballFieldType.Tnorm, XDimension, YDimension, (x, y) =>
				{
					return norm * TemperatureScalingField[x, y];
				});
		}

		private void FindMaximumTemperature()
		{
			double xIndexToStopAt = XDimension;
			if(IsSymmetricInX)
			{
				xIndexToStopAt = 1;
			}

			double yIndexToStopAt = YDimension;
			if(IsSymmetricInY)
			{
				yIndexToStopAt = 1;
			}

			MaximumTemperature = 0;
			for(int i = 0; i < xIndexToStopAt; i++)
			{
				for(int j = 0; j < yIndexToStopAt; j++)
				{
					if(DiscreteValues[i, j] > MaximumTemperature)
					{
						MaximumTemperature = DiscreteValues[i, j];
					}
				}
			}
		}
	}
}
