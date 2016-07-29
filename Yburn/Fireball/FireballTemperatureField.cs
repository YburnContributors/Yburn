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
            SimpleFireballField temperatureScalingField,
            double initialMaximumTemperature,
            double thermalTime,
            double initialTime
            )
            : base(FireballFieldType.Temperature, xDimension, yDimension)
        {
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
        }

        public void Advance(
            double newTime
            )
        {
            InitializeDiscreteValues((i, j) =>
            {
                return Tnorm[i, j] / Math.Pow(newTime, 1 / 3.0);
            });
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

        // temperature is normalized such that T(0, 0, ThermalTimeFm) = T0
        // for a central collision (ImpactParameter = 0) and TransverseMomentum = 0
        private void InitTnorm()
        {
            double norm = InitialMaximumTemperature * Math.Pow(ThermalTime, 1 / 3.0);
            Tnorm = new SimpleFireballField(FireballFieldType.Tnorm, XDimension, YDimension, (i, j) =>
                {
                    return norm * TemperatureScalingField[i, j];
                });
        }
    }
}