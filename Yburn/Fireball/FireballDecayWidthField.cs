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

		private static double GetRelativeVelocityInQQFrame(
			double vQQ,
			double vQGP
			)
		{
			return Math.Abs(vQQ - vQGP) / (1.0 - vQQ * vQGP);
		}

		private static double[] SetFormationTimes(
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
			FireballCoordinateSystem system,
			IList<double> transverseMomenta,
			FireballTemperatureField temperature,
			SimpleFireballField vx,
			SimpleFireballField vy,
			FireballElectromagneticField electricField,
			FireballElectromagneticField magneticField,
			Dictionary<BottomiumState, double> formationTimes,
			double initialTime,
			DecayWidthRetrievalFunction decayWidthFunction
			)
			: base(FireballFieldType.DecayWidth, system, transverseMomenta)
		{
			Temperature = temperature;
			VX = vx;
			VY = vy;
			ElectricField = electricField;
			MagneticField = magneticField;
			FormationTimes = SetFormationTimes(formationTimes);
			InitialTime = initialTime;
			GetDecayWidth = decayWidthFunction;

			SetTransverseBottomiumVelocityAndLorentzFactor(out BetaT, out GammaT);
			SetInitialValues();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Advance(
			double newTime
			)
		{
			LinearInterpolation2D interpT = new LinearInterpolation2D(System.XAxis, System.YAxis, Temperature.GetValues());
			LinearInterpolation2D interpVX = new LinearInterpolation2D(System.XAxis, System.YAxis, VX.GetValues());
			LinearInterpolation2D interpVY = new LinearInterpolation2D(System.XAxis, System.YAxis, VY.GetValues());
			LinearInterpolation2D interpE = new LinearInterpolation2D(System.XAxis, System.YAxis, ElectricField.GetValues());
			LinearInterpolation2D interpB = new LinearInterpolation2D(System.XAxis, System.YAxis, MagneticField.GetValues());

			SetValues((xIndex, yIndex, pTIndex, stateIndex) =>
			{
				if(!IsStateAlreadyFormed(pTIndex, stateIndex, newTime))
				{
					return 0;
				}
				else
				{
					GetQQStateCoordinates(xIndex, yIndex, pTIndex, stateIndex, newTime,
						out double x, out double y);

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
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private readonly FireballTemperatureField Temperature;

		// tranverse expansion velocity of the fireball as measured in the lab frame
		private readonly SimpleFireballField VX;

		private readonly SimpleFireballField VY;

		private readonly FireballElectromagneticField ElectricField;

		private readonly FireballElectromagneticField MagneticField;

		private readonly double[] FormationTimes;

		private readonly double InitialTime;

		private readonly DecayWidthRetrievalFunction GetDecayWidth;

		private void SetInitialValues()
		{
			SetValues((x, y, pT, state) => IsStateAlreadyFormed(pT, state, InitialTime) ?
				GetDecayWidth((BottomiumState)state, Temperature[x, y], 0, 0, 0) / GammaT[pT, state]
				: double.PositiveInfinity);
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
		private readonly double[,] BetaT;

		// Lorentz factor due to transverse velocity of the bottomia
		private readonly double[,] GammaT;

		private void SetTransverseBottomiumVelocityAndLorentzFactor(
			out double[,] betaT,
			out double[,] gammaT
			)
		{
			gammaT = new double[PTDimension, NumberBottomiumStates];
			betaT = new double[PTDimension, NumberBottomiumStates];

			for(int pT = 0; pT < PTDimension; pT++)
			{
				for(int state = 0; state < NumberBottomiumStates; state++)
				{
					gammaT[pT, state] = Math.Sqrt(1.0 + Math.Pow(TransverseMomenta[pT] / BottomiumRestMass_GeV(state), 2));
					betaT[pT, state] = Math.Sqrt(1.0 - Math.Pow(gammaT[pT, state], -2));
				}
			}
		}

		private static double BottomiumRestMass_GeV(
			int stateIndex
			)
		{
			BottomiumState state = (BottomiumState)stateIndex;
			switch(state)
			{
				case BottomiumState.Y1S:
					return Constants.RestMassY1S_MeV * 1E-3;

				case BottomiumState.x1P:
					return Constants.RestMassX1P_MeV * 1E-3;

				case BottomiumState.Y2S:
					return Constants.RestMassY2S_MeV * 1E-3;

				case BottomiumState.x2P:
					return Constants.RestMassX2P_MeV * 1E-3;

				case BottomiumState.Y3S:
					return Constants.RestMassY3S_MeV * 1E-3;

				case BottomiumState.x3P:
					return Constants.RestMassX3P_MeV * 1E-3;

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

			x = vxVal != 0 ? System.XAxis[xIndex] + pathLength * vxVal / v : System.XAxis[xIndex];
			y = vyVal != 0 ? System.YAxis[yIndex] + pathLength * vyVal / v : System.YAxis[yIndex];
		}

		private bool IsInDomainOfCalculation(
			double x,
			double y
			)
		{
			return x <= System.XAxis[XDimension - 1]
				&& x >= System.XAxis[0]
				&& y <= System.YAxis[YDimension - 1]
				&& y >= System.YAxis[0];
		}
	}
}
