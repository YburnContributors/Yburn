using System;
using System.Collections.Generic;

namespace Yburn.Fireball
{
	public class FireballDecayWidth : StateSpecificFireballField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballDecayWidth(
			int xDimension,
			int yDimension,
			double gridCellSize,
			double[] transverseMomenta,
			FireballTemperature temperature,
			SimpleFireballField vx,
			SimpleFireballField vy,
			double[] formationTimes,
			double initialTime,
			DecayWidthEvaluationType decayWidthEvaluationType,
			double[] decayWidthAveragingAngles,
			List<KeyValuePair<double, double>>[] temperatureDecayWidthList
			)
			: base(FireballFieldType.DecayWidth, xDimension, yDimension,
				  transverseMomenta.Length)
		{
			InitXY();
			GridCellSize = gridCellSize;
			Temperature = temperature;
			VX = vx;
			VY = vy;
			FormationTimes = formationTimes;
			InitialTime = initialTime;
			TransverseMomenta = transverseMomenta;
			DecayWidthEvaluationType = decayWidthEvaluationType;
			DecayWidthAveragingAngles = decayWidthAveragingAngles;
			TemperatureDecayWidthList = temperatureDecayWidthList;

			Initialize();
		}

		public FireballDecayWidth(
			double[] xPosition,
			double[] yPosition,
			double gridCellSize,
			double[] transverseMomenta,
			FireballTemperature temperature,
			SimpleFireballField vx,
			SimpleFireballField vy,
			double[] formationTimes,
			double initialTime,
			DecayWidthEvaluationType decayWidthEvaluationType,
			double[] decayWidthAveragingAngles,
			List<KeyValuePair<double, double>>[] temperatureDecayWidthList
			)
			: base(FireballFieldType.DecayWidth, xPosition.Length, yPosition.Length,
				  transverseMomenta.Length)
		{
			X = new double[XDimension];
			Y = new double[YDimension];
			X = xPosition;
			Y = yPosition;
			GridCellSize = gridCellSize;
			Temperature = temperature;
			VX = vx;
			VY = vy;
			FormationTimes = formationTimes;
			InitialTime = initialTime;
			TransverseMomenta = transverseMomenta;
			DecayWidthEvaluationType = decayWidthEvaluationType;
			DecayWidthAveragingAngles = decayWidthAveragingAngles;
			TemperatureDecayWidthList = temperatureDecayWidthList;

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

		private FireballTemperature Temperature;

		// tranverse expansion velocity of the fireball as measured in the lab frame
		private SimpleFireballField VX;

		private SimpleFireballField VY;

		private double[] FormationTimes;

		private double InitialTime;

		private void Initialize()
		{
			SetTransverseBottomiumVelocityAndLorentzFactor();
			SetDecayWidthAveragers();
			SetValues((i, j, k, l) =>
			{
				return IsStateAlreadyFormed(k, l, InitialTime) ?
					 GetDecayWidth((BottomiumState)l, Temperature[i, j], 0) / GammaT[k, l]
					: double.PositiveInfinity;
			});
		}

		// x, y are in the plane perpendicular to the symmetry axis. The origin is in the middle
		// between the two center of the nuclei. The x-axis is in the plane that the beam axis spans
		// with the line connecting the two centers.
		private double[] X;

		private double[] Y;

		private double GridCellSize;

		private void InitXY()
		{
			X = new double[XDimension];
			for(int i = 0; i < XDimension; i++)
			{
				X[i] = GridCellSize * i;
			}

			Y = new double[YDimension];
			for(int j = 0; j < YDimension; j++)
			{
				Y[j] = GridCellSize * j;
			}
		}

		private List<DecayWidthAverager> DecayWidthAveragers;

		private DecayWidthEvaluationType DecayWidthEvaluationType;

		private double[] DecayWidthAveragingAngles;

		private List<KeyValuePair<double, double>>[] TemperatureDecayWidthList;

		private void SetDecayWidthAveragers()
		{
			if(DecayWidthEvaluationType == DecayWidthEvaluationType.MaximallyBlueshifted)
			{
				DecayWidthAveragingAngles = new double[] { 0 };
			}

			DecayWidthAveragers = new List<DecayWidthAverager>();
			foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
			{
				if(TemperatureDecayWidthList[(int)state].Count > 0)
				{
					DecayWidthAveragers.Add(new DecayWidthAverager(
						TemperatureDecayWidthList[(int)state], DecayWidthAveragingAngles));
				}
				else
				{
					DecayWidthAveragers.Add(null);
				}
			}
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

		private double[] TransverseMomenta;

		private void SetTransverseBottomiumVelocityAndLorentzFactor()
		{
			GammaT = new double[PtDimension, NumberBottomiumStates];
			BetaT = new double[PtDimension, NumberBottomiumStates];

			for(int k = 0; k < PtDimension; k++)
			{
				for(int l = 0; l < NumberBottomiumStates; l++)
				{
					GammaT[k, l] = Math.Sqrt(1.0 + Math.Pow(TransverseMomenta[k] / bbMass(l), 2));
					BetaT[k, l] = Math.Sqrt(1.0 - Math.Pow(GammaT[k, l], -2));
				}
			}
		}

		// rest masses of the BottomiumState from pdg 2013 in GeV
		private static double bbMass(
			int stateIndex
			)
		{
			BottomiumState state = (BottomiumState)stateIndex;
			switch(state)
			{
				case BottomiumState.Y1S:
					return 9.46030;

				case BottomiumState.x1P:
					return (9.85944 + 3 * 9.89278 + 5 * 9.91221) / 9.0;

				case BottomiumState.Y2S:
					return 10.02326;

				case BottomiumState.x2P:
					return (10.2325 + 3 * 10.25546 + 5 * 10.26865) / 9.0;

				case BottomiumState.Y3S:
					return 10.3552;

				case BottomiumState.x3P:
					return 10.534;

				default:
					throw new Exception("Unknown bbState");
			}
		}

		private double GetDecayWidth(
			BottomiumState state,
			double temperature,
			double velocity
			)
		{
			if(DecayWidthAveragers[(int)state] == null)
			{
				return double.PositiveInfinity;
			}

			switch(DecayWidthEvaluationType)
			{
				case DecayWidthEvaluationType.UnshiftedTemperature:
					return DecayWidthAveragers[(int)state].GetDecayWidth(temperature, 0);

				case DecayWidthEvaluationType.AveragedTemperature:
					return DecayWidthAveragers[(int)state]
						.GetDecayWidthUsingAveragedTemperature(temperature, velocity);

				case DecayWidthEvaluationType.MaximallyBlueshifted:
				case DecayWidthEvaluationType.AveragedDecayWidth:
					return DecayWidthAveragers[(int)state].GetDecayWidth(temperature, velocity);

				default:
					throw new Exception("Invalid DecayWidthEvaluationType.");
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