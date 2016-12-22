using System;

namespace Yburn.Fireball
{
	public class FireballElectromagneticField : SimpleFireballField
	{
		public static FireballElectromagneticField CreateFireballElectricField(
			FireballParam param,
			double[] xAxis,
			double[] yAxis
			)
		{
			CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);
			Func<double, double, double, double> fieldFunction
				= (tau, x, y) => emf.CalculateElectricFieldPerFm2_LCF(tau, x, y, 2.7).Norm;

			return new FireballElectromagneticField(FireballFieldType.ElectricFieldStrength,
				xAxis, yAxis, fieldFunction, param.EMFUpdateIntervalFm);
		}

		public static FireballElectromagneticField CreateFireballMagneticField(
			FireballParam param,
			double[] xAxis,
			double[] yAxis
			)
		{
			CollisionalElectromagneticField emf = new CollisionalElectromagneticField(param);
			Func<double, double, double, double> fieldFunction
				= (tau, x, y) => emf.CalculateMagneticFieldPerFm2_LCF(tau, x, y, 2.7).Norm;

			return new FireballElectromagneticField(FireballFieldType.MagneticFieldStrength,
				xAxis, yAxis, fieldFunction, param.EMFUpdateIntervalFm);
		}

		public static FireballElectromagneticField CreateZeroField(
			FireballFieldType type,
			double[] xAxis,
			double[] yAxis
			)
		{
			Func<double, double, double, double> fieldFunction = (tau, x, y) => 0;

			return new FireballElectromagneticField(
				type, xAxis, yAxis, fieldFunction, double.PositiveInfinity);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected FireballElectromagneticField(
			FireballFieldType type,
			double[] xAxis,
			double[] yAxis,
			Func<double, double, double, double> fieldFunction,
			double fieldUpdateInterval
			) : base(type, xAxis, yAxis, (x, y) => 0)
		{
			XAxis = xAxis;
			YAxis = yAxis;
			FieldFunction = fieldFunction;

			UpdateInterval = fieldUpdateInterval;
			CurrentTime = 0;
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Advance(
			double newTime
			)
		{
			if(Math.Abs(newTime - CurrentTime) >= UpdateInterval)
			{
				SimpleFireballFieldContinuousFunction function
					= (x, y) => FieldFunction(newTime, x, y);

				SetDiscreteValues(function, XAxis, YAxis);
				CurrentTime = newTime;
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly double[] XAxis;

		private readonly double[] YAxis;

		protected readonly Func<double, double, double, double> FieldFunction;

		private readonly double UpdateInterval;

		private double CurrentTime;
	}
}
