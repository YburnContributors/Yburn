using System;

namespace Yburn.Fireball.Tests
{
	public static class DummyDecayWidthProvider
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double GetDummyDecayWidth(
			BottomiumState state,
			double temperature,
			double velocity,
			double electricFieldStrength,
			double magneticFieldStrength
			)
		{
			double decayWidth = GetQuadraticDummyDecayWidth(state, temperature);

			if(temperature < 150)
			{
				return 0;
			}
			else if(decayWidth > 700)
			{
				return double.PositiveInfinity;
			}
			else
			{
				return decayWidth;
			}
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double GetQuadraticDummyDecayWidth(
			BottomiumState state,
			double temperature
			)
		{
			switch(state)
			{
				case BottomiumState.Y1S:
					return temperature * temperature / 450;

				case BottomiumState.x1P:
					return temperature * temperature / 125;

				case BottomiumState.Y2S:
					return temperature * temperature / 128;

				case BottomiumState.x2P:
				case BottomiumState.Y3S:
					return temperature * temperature / 66.7;

				case BottomiumState.x3P:
					return temperature * temperature / 37.5;

				default:
					throw new Exception("Invalid BottomiumState.");
			}
		}
	}
}
