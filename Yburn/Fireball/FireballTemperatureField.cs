using System;

namespace Yburn.Fireball
{
	public class FireballTemperatureField : SimpleFireballField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballTemperatureField(
			CoordinateSystem system,
			SimpleFireballField temperatureScalingField,
			double initialMaximumTemperature,
			double thermalTime,
			double initialTime
			)
			: base(FireballFieldType.Temperature, system)
		{
			TemperatureScalingField = temperatureScalingField;
			InitialMaximumTemperature = initialMaximumTemperature;
			ThermalTime = thermalTime;
			InitialTime = initialTime;

			AssertValidMembers();

			InitTemperatureNormalizationField();
			Advance(InitialTime);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Advance(
			Ftexs solver
			)
		{
			Values = solver.T;
			FindMaximumTemperature();
		}

		public void Advance(
			double newTime
			)
		{
			SetValues((x, y) => TemperatureNormalizationField[x, y] / Math.Pow(newTime, 1 / 3.0));
			FindMaximumTemperature();
		}

		public double MaximumTemperature
		{
			get;
			private set;
		}

		// auxiliary field for the calculation of the temperature profile Temperature
		public SimpleFireballField TemperatureNormalizationField
		{
			get;
			private set;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double InitialMaximumTemperature;

		private readonly double InitialTime;

		private readonly double ThermalTime;

		private readonly SimpleFireballField TemperatureScalingField;

		private void AssertValidMembers()
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

		// temperature is normalized such that T(0, 0, ThermalTime_fm) = T0
		// for a central collision (ImpactParameter = 0) and TransverseMomentum = 0
		private void InitTemperatureNormalizationField()
		{
			double norm = InitialMaximumTemperature * Math.Pow(ThermalTime, 1 / 3.0);

			TemperatureNormalizationField = new SimpleFireballField(
				FireballFieldType.TemperatureNormalization,
				System,
				(x, y) => norm * TemperatureScalingField[x, y]);
		}

		private void FindMaximumTemperature()
		{
			MaximumTemperature = Values[0, 0];

			if(!System.IsCollisionSymmetric)
			{
				for(int i = 1; i < XDimension; i++)
				{
					double temperature = Values[i, 0];

					if(temperature > MaximumTemperature)
					{
						MaximumTemperature = temperature;
					}
				}
			}
		}
	}
}
