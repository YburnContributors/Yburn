using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public delegate double DecayWidthRetrievalFunction(
		BottomiumState state,
		double temperature,
		double velocity,
		double electricField,
		double magneticField
		);

	public class FireballDecayWidthField : StateSpecificFireballField
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private double[] SetFormationTimes(
			Dictionary<BottomiumState, double> dictionary
			)
		{
			double[] formationTimes = new double[NumberBottomiumStates];

			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				formationTimes[(int)state] = dictionary[state];
			}

			return formationTimes;
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballDecayWidthField(
			double[] xAxis,
			double[] yAxis,
			List<double> transverseMomenta,
			FireballTemperatureField temperature,
			SimpleFireballField vx,
			SimpleFireballField vy,
			FireballElectromagneticField electricField,
			FireballElectromagneticField magneticField,
			Dictionary<BottomiumState, double> formationTimes,
			double initialTime,
			DecayWidthRetrievalFunction decayWidthFunction
			)
			: base(FireballFieldType.DecayWidth, xAxis.Length, yAxis.Length,
				  transverseMomenta.Count)
		{
			X = xAxis;
			Y = yAxis;
			TransverseMomenta = transverseMomenta;
			Temperature = temperature;
			VX = vx;
			VY = vy;
			ElectricField = electricField;
			MagneticField = magneticField;
			FormationTimes = SetFormationTimes(formationTimes);
			InitialTime = initialTime;
			GetDecayWidth = decayWidthFunction;

			Initialize();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Advance(
			double newTime
			)
		{
			LinearInterpolation2D interpT = new LinearInterpolation2D(X, Y, Temperature.GetDiscreteValues());
			LinearInterpolation2D interpVX = new LinearInterpolation2D(X, Y, VX.GetDiscreteValues());
			LinearInterpolation2D interpVY = new LinearInterpolation2D(X, Y, VY.GetDiscreteValues());
			LinearInterpolation2D interpE = new LinearInterpolation2D(X, Y, ElectricField.GetDiscreteValues());
			LinearInterpolation2D interpB = new LinearInterpolation2D(X, Y, MagneticField.GetDiscreteValues());

			SetValues((xIndex, yIndex, pTIndex, stateIndex) =>
			{
				if(!IsStateAlreadyFormed(pTIndex, stateIndex, newTime))
				{
					return 0;
				}
				else
				{
					double x;
					double y;
					GetQQStateCoordinates(xIndex, yIndex, pTIndex, stateIndex, newTime, out x, out y);

					if(!IsInDomainOfCalculation(x, y))
					{
						return 0;
					}
					else
					{
						double vQGP = Math.Sqrt(Math.Pow(interpVX.GetValue(x, y), 2)
							+ Math.Pow(interpVY.GetValue(x, y), 2));

						return GetDecayWidth(
							(BottomiumState)stateIndex,
							interpT.GetValue(x, y),
							GetRelativeVelocityInQQFrame(BetaT[pTIndex, stateIndex], vQGP),
							interpE.GetValue(x, y),
							interpB.GetValue(x, y)) / GammaT[pTIndex, stateIndex];
					}
				}
			});
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double GetRelativeVelocityInQQFrame(
			double vQQ,
			double vQGP
			)
		{
			return Math.Abs(vQQ - vQGP) / (1.0 - vQQ * vQGP);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// x, y are in the plane perpendicular to the symmetry axis. The origin is in the middle
		// between the two center of the nuclei. The x-axis is in the plane that the beam axis spans
		// with the line connecting the two centers.
		private readonly double[] X;

		private readonly double[] Y;

		private readonly List<double> TransverseMomenta;

		private readonly FireballTemperatureField Temperature;

		// tranverse expansion velocity of the fireball as measured in the lab frame
		private readonly SimpleFireballField VX;

		private readonly SimpleFireballField VY;

		private readonly FireballElectromagneticField ElectricField;

		private readonly FireballElectromagneticField MagneticField;

		private readonly double[] FormationTimes;

		private readonly double InitialTime;

		private readonly DecayWidthRetrievalFunction GetDecayWidth;

		private void Initialize()
		{
			SetTransverseBottomiumVelocityAndLorentzFactor();
			SetValues((x, y, pT, state) =>
			{
				return IsStateAlreadyFormed(pT, state, InitialTime) ?
					 GetDecayWidth((BottomiumState)state, Temperature[x, y], 0, 0, 0) / GammaT[pT, state]
					: double.PositiveInfinity;
			});
		}

		private bool IsStateAlreadyFormed(
			int pTIndex,
			int stateIndex,
			double time
			)
		{
			return time >= GammaT[pTIndex, stateIndex] * FormationTimes[stateIndex];
		}

		// transverse velocity of the bottomia
		private double[,] BetaT;

		// Lorentz factor due to transverse velocity of the bottomia
		private double[,] GammaT;

		private void SetTransverseBottomiumVelocityAndLorentzFactor()
		{
			GammaT = new double[PTDimension, NumberBottomiumStates];
			BetaT = new double[PTDimension, NumberBottomiumStates];

			for(int pT = 0; pT < PTDimension; pT++)
			{
				for(int state = 0; state < NumberBottomiumStates; state++)
				{
					GammaT[pT, state] = Math.Sqrt(1.0 + Math.Pow(TransverseMomenta[pT] / bbRestMassInGeV(state), 2));
					BetaT[pT, state] = Math.Sqrt(1.0 - Math.Pow(GammaT[pT, state], -2));
				}
			}
		}

		private static double bbRestMassInGeV(
			int stateIndex
			)
		{
			BottomiumState state = (BottomiumState)stateIndex;
			switch(state)
			{
				case BottomiumState.Y1S:
					return Constants.RestMassY1SMeV * 1E-3;

				case BottomiumState.x1P:
					return Constants.RestMassX1PMeV * 1E-3;

				case BottomiumState.Y2S:
					return Constants.RestMassY2SMeV * 1E-3;

				case BottomiumState.x2P:
					return Constants.RestMassX2PMeV * 1E-3;

				case BottomiumState.Y3S:
					return Constants.RestMassY3SMeV * 1E-3;

				case BottomiumState.x3P:
					return Constants.RestMassX3PMeV * 1E-3;

				default:
					throw new Exception("Unknown bbState");
			}
		}

		private void GetQQStateCoordinates(
			int xIndex,
			int yIndex,
			int pTIndex,
			int stateIndex,
			double time,
			out double x,
			out double y
			)
		{
			double pathLength = BetaT[pTIndex, stateIndex] * time;
			double vxVal = VX[xIndex, yIndex];
			double vyVal = VY[xIndex, yIndex];
			double v = Math.Sqrt(vxVal * vxVal + vyVal * vyVal);

			x = vxVal != 0 ? X[xIndex] + pathLength * vxVal / v : X[xIndex];
			y = vyVal != 0 ? Y[yIndex] + pathLength * vyVal / v : Y[yIndex];
		}

		private bool IsInDomainOfCalculation(
			double x,
			double y
			)
		{
			return x <= X[XDimension - 1]
				&& x >= X[0]
				&& y <= Y[YDimension - 1]
				&& y >= Y[0];
		}
	}
}
