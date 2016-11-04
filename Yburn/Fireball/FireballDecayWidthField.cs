using System;
using System.Collections.Generic;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public delegate double DecayWidthRetrievalFunction(
		BottomiumState state,
		double temperature,
		double velocity);

	public class FireballDecayWidthField : StateSpecificFireballField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballDecayWidthField(
			double[] xPosition,
			double[] yPosition,
			List<double> transverseMomenta,
			FireballTemperatureField temperature,
			SimpleFireballField vx,
			SimpleFireballField vy,
			List<double> formationTimes,
			double initialTime,
			DecayWidthRetrievalFunction decayWidthFunction
			)
			: base(FireballFieldType.DecayWidth, xPosition.Length, yPosition.Length,
				  transverseMomenta.Count)
		{
			X = xPosition;
			Y = yPosition;
			TransverseMomenta = transverseMomenta;
			Temperature = temperature;
			VX = vx;
			VY = vy;
			FormationTimes = formationTimes;
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

			SetValues((i, j, k, l) =>
			{
				if(!IsStateAlreadyFormed(k, l, newTime))
				{
					return 0;
				}
				else
				{
					double x;
					double y;
					GetQQStateCoordinates(i, j, BetaT[k, l], newTime, out x, out y);

					if(!IsInDomainOfCalculation(x, y))
					{
						return 0;
					}
					else
					{
						double vQQ = Math.Sqrt(Math.Pow(interpVX.GetValue(x, y), 2)
							+ Math.Pow(interpVY.GetValue(x, y), 2));
						return GetDecayWidth((BottomiumState)l, interpT.GetValue(x, y),
							GetRelativeVelocityInLabFrame(BetaT[k, l], vQQ)) / GammaT[k, l];
					}
				}
			});
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double GetRelativeVelocityInLabFrame(
			double vQGP,
			double vQQ
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

		private readonly List<double> FormationTimes;

		private readonly double InitialTime;

		private readonly DecayWidthRetrievalFunction GetDecayWidth;

		private void Initialize()
		{
			SetTransverseBottomiumVelocityAndLorentzFactor();
			SetValues((i, j, k, l) =>
			{
				return IsStateAlreadyFormed(k, l, InitialTime) ?
					 GetDecayWidth((BottomiumState)l, Temperature[i, j], 0) / GammaT[k, l]
					: double.PositiveInfinity;
			});
		}

		private bool IsStateAlreadyFormed(
			int ptIndex,
			int stateIndex,
			double time
			)
		{
			return time >= GammaT[ptIndex, stateIndex] * FormationTimes[stateIndex];
		}

		// transverse velocity of the bottomia
		private double[,] BetaT;

		// Lorentz factor due to transverse velocity of the bottomia
		private double[,] GammaT;

		private void SetTransverseBottomiumVelocityAndLorentzFactor()
		{
			GammaT = new double[PtDimension, NumberBottomiumStates];
			BetaT = new double[PtDimension, NumberBottomiumStates];

			for(int k = 0; k < PtDimension; k++)
			{
				for(int l = 0; l < NumberBottomiumStates; l++)
				{
					GammaT[k, l] = Math.Sqrt(1.0 + Math.Pow(TransverseMomenta[k] / bbRestMassInGeV(l), 2));
					BetaT[k, l] = Math.Sqrt(1.0 - Math.Pow(GammaT[k, l], -2));
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
			int i,
			int j,
			double qqBeta,
			double time,
			out double x,
			out double y
			)
		{
			double pathLength = qqBeta * time;
			double vxVal = VX[i, j];
			double vyVal = VY[i, j];
			double v = Math.Sqrt(vxVal * vxVal + vyVal * vyVal);

			x = vxVal != 0 ? X[i] + pathLength * vxVal / v : X[i];
			y = vyVal != 0 ? Y[j] + pathLength * vyVal / v : Y[j];
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