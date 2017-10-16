using System;

namespace Yburn.Fireball
{
	public class FireballElectromagneticField : SimpleFireballField
	{
		public static FireballElectromagneticField CreateFireballElectricField(
			FireballParam param
			)
		{
			return new FireballElectromagneticField(
				FireballFieldType.ElectricFieldStrength,
				new CoordinateSystem(param),
				new CollisionalElectromagneticField(param).CalculateAverageElectricFieldStrength,
				param.EMFUpdateInterval_fm);
		}

		public static FireballElectromagneticField CreateFireballMagneticField(
			FireballParam param
			)
		{
			return new FireballElectromagneticField(
				FireballFieldType.MagneticFieldStrength,
				new CoordinateSystem(param),
				new CollisionalElectromagneticField(param).CalculateAverageMagneticFieldStrength,
				param.EMFUpdateInterval_fm);
		}

		public static FireballElectromagneticField CreateZeroField(
			FireballFieldType type,
			CoordinateSystem system
			)
		{
			Func<double, double, double, double, double> fieldFunction = (tau, x, y, sigma) => 0;

			return new FireballElectromagneticField(
				type, system, fieldFunction, double.PositiveInfinity);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected FireballElectromagneticField(
			FireballFieldType type,
			CoordinateSystem system,
			Func<double, double, double, double, double> fieldFunction,
			double fieldUpdateInterval
			) : base(type, system)
		{
			FieldFunction = fieldFunction;

			UpdateInterval = fieldUpdateInterval;
			CurrentTime = 0;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Advance(
			double newTime,
			double qgpConductivity
			)
		{
			if(Math.Abs(newTime - CurrentTime) >= UpdateInterval)
			{
				SimpleFireballFieldFunction function = (x, y) => FieldFunction(
					newTime, System.XAxis[x], System.YAxis[y], qgpConductivity);

				SetValues(function);
				CurrentTime = newTime;
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected readonly Func<double, double, double, double, double> FieldFunction;

		private readonly double UpdateInterval;

		private double CurrentTime;
	}
}
