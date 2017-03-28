/**************************************************************************************************
 * The class Ftexs (Fireball Transverse Expansion Solver) implements a solver specified for the
 * Euler equations of the relativistic perfect fluid in (quasi) two dimensions. It is specified to
 * the case of the expanding fireball formed in relativistic heavy ion collisions, i.e. it
 * calculates energy density and velocity distributions for the case of a transverse expanding
 * medium superposed with a Bjorken expansion in the longitudinal direction. The following set of
 * equations is solved:
 *
 * del_Tau S = - del_X ( S VX ) - del_Y ( S VY ),
 * del_Tau MX = - del_X ( MX VX + P ) - del_Y ( MX VY ),
 * del_Tau MY = - del_X ( MY VX ) - del_Y ( MY VY + P ),
 *
 * GAMMA = 1/sqrt(1 - VX^2 - VY^2),
 * MX = VX Tau GAMMA^2 T^4,
 * MY = VY Tau GAMMA^2 T^4,
 * P = (1/4) * Tau T^4,
 * S = Tau GAMMA T^3,
 *
 * where the variables correspond to (up to constant prefactors) Lorentz factor (GAMMA), momentum
 * density (MX,MY), pressure (P), entropy density (S), temperature (T) and velocity (VX,VY),
 * respectively, which are functions of the coordinates of space (X,Y) and longitudinal proper time
 * (Tau).
 **************************************************************************************************/

using System;

namespace Yburn.Fireball
{
	public class FtexsLaxWendroff
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FtexsLaxWendroff(
			double gridCellSize,
			double initialTime,
			double maxCFL, // Maximum allowed Courant-Friedrichs-Levy number
			double[,] initialTemperature,
			double[,] initialXvelocity,
			double[,] initialYvelocity
			)
		{
			GridCellSize = gridCellSize;
			T = initialTemperature;
			NX = T.GetLength(0);
			NY = T.GetLength(1);
			VX = initialXvelocity;
			VY = initialYvelocity;
			CurrentTime = InitialTime = initialTime;
			MaxCFL = maxCFL;

			AssertValidInput();
			InitializeFields();

			JSX = new double[NX + 1, NY + 1];
			JMXX = new double[NX + 1, NY + 1];
			JMYX = new double[NX + 1, NY + 1];
			VXedge = new double[NX + 1, NY + 1];

			JSY = new double[NX + 1, NY + 1];
			JMXY = new double[NX + 1, NY + 1];
			JMYY = new double[NX + 1, NY + 1];
			VYedge = new double[NX + 1, NY + 1];

			Shalf = new double[NX + 1, NY + 1];
			MXhalf = new double[NX + 1, NY + 1];
			MYhalf = new double[NX + 1, NY + 1];
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// temperature
		public double[,] T
		{
			get;
			private set;
		}

		// x-component of the velocity evaluated at the cell-centers
		public double[,] VX
		{
			get;
			private set;
		}

		// y-component of the velocity evaluated at the cell-centers
		public double[,] VY
		{
			get;
			private set;
		}

		public void SolveUntil(
			double finalTime
			)
		{
			FinalTime = finalTime;
			SetMaxCFL();

			while(CurrentTime < FinalTime)
			{
				double timeStep = CalculateNextTimeStep();
				CurrentTime += timeStep;
				Advance(timeStep);
				UpdateVariables();
			}

			InitialTime = CurrentTime;
		}

		/********************************************************************************************
 		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double Arcosh(
			double x
			)
		{
			return Math.Log(x + Math.Sqrt(x * x - 1));
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double InitialTime;

		private double FinalTime;

		private double CurrentTime;

		private double GridCellSize;

		// Number of grid points in the spacial direction X
		private int NX;

		// Number of grid points in the spacial direction Y
		private int NY;

		// Maximum allowed Courant-Friedrichs-Levy number
		private double MaxCFL;

		// entropy density and currents
		private double[,] S;

		// x-component of the momentum density and currents
		private double[,] MX;

		// y-component of the momentum density and currents
		private double[,] MY;

		// pressure gradients
		private double[,] DPX;

		private double[,] DPY;

		private double[,] JSX;

		private double[,] JSY;

		private double[,] JMXX;

		private double[,] JMXY;

		private double[,] JMYX;

		private double[,] JMYY;

		// x-component of the velocity evaluated at the cell-edges
		private double[,] VXedge;

		// y-component of the velocity evaluated at the cell-edges
		private double[,] VYedge;

		private double[,] Shalf;

		private double[,] MXhalf;

		private double[,] MYhalf;

		private void AssertValidInput()
		{
			if(VX.GetLength(0) != NX
				|| VX.GetLength(1) != NY)
			{
				throw new Exception("Dimensions of VX are not compatible with T.");
			}

			if(VY.GetLength(0) != NX
				|| VY.GetLength(1) != NY)
			{
				throw new Exception("Dimensions of VY are not compatible with T.");
			}

			AssertPositiveT();
			AssertValidVelocity();

			if(MaxCFL <= 0)
			{
				throw new Exception("MaxCFL <= 0.");
			}
		}

		private void AssertPositiveT()
		{
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					if(T[j, k] <= 0)
					{
						throw new Exception("T should only take positive values.");
					}
				}
			}
		}

		private void AssertValidVelocity()
		{
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					double vSquared = VX[j, k] * VX[j, k] + VY[j, k] * VY[j, k];
					if(vSquared >= 1)
					{
						throw new Exception("|V| >= 1.");
					}
				}
			}
		}

		private double Gamma(
			int j,
			int k
			)
		{
			return 1.0 / Math.Sqrt(1.0 - VX[j, k] * VX[j, k] - VY[j, k] * VY[j, k]);
		}

		private void InitializeFields()
		{
			InitializeEntropyDensity();
			InitializeMomentumDensity();
			InitializePressureGradient();
		}

		private void InitializeEntropyDensity()
		{
			S = new double[NX, NY];
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					S[j, k] = CurrentTime * Gamma(j, k) * T[j, k] * T[j, k] * T[j, k];
				}
			}
		}

		private void InitializeMomentumDensity()
		{
			MX = new double[NX, NY];
			MY = new double[NX, NY];
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					MX[j, k] = Gamma(j, k) * T[j, k] * S[j, k] * VX[j, k];
					MY[j, k] = Gamma(j, k) * T[j, k] * S[j, k] * VY[j, k];
				}
			}
		}

		private void InitializePressureGradient()
		{
			DPX = new double[NX, NY];
			DPY = new double[NX, NY];
			UpdatePressureGradient();
		}

		private void SetMaxCFL()
		{
			MaxCFL = FinalTime - InitialTime < 0 ? -Math.Abs(MaxCFL) : Math.Abs(MaxCFL);
		}

		private double CalculateNextTimeStep()
		{
			double gradVmax = GetMaxVelocityGradient();
			if(gradVmax == 0)
			{
				// If dVdXmax == 0 we have purely longitudinal expansion and can jump to the final time.
				return FinalTime - CurrentTime;
			}
			else
			{
				//adjust the time step so that CFLmax is not exceeded
				return Math.Min(MaxCFL / gradVmax, FinalTime - CurrentTime);
			}
		}

		private double GetMaxVelocityGradient()
		{
			// find out maximum transport velocity from v and dP
			double gradVmax = 0;
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					gradVmax = Math.Max(gradVmax, Math.Abs(VX[j, k]) / GridCellSize);
					gradVmax = Math.Max(gradVmax, Math.Abs(VY[j, k]) / GridCellSize);
					gradVmax = Math.Max(gradVmax,
						Math.Sqrt(Math.Abs(DPX[j, k] / Math.Pow(T[j, k], 4))) / GridCellSize);
					gradVmax = Math.Max(gradVmax,
						Math.Sqrt(Math.Abs(DPY[j, k] / Math.Pow(T[j, k], 4))) / GridCellSize);
				}
			}

			return gradVmax;
		}

		public void Advance(double timeStep)
		{
			UpdateCurrents();
			double temp = 0.25 * timeStep / GridCellSize;
			for(int j = 1; j < NX; j++)
			{
				for(int k = 1; k < NY; k++)
				{
					Shalf[j, k] = 0.25 * (S[j - 1, k - 1] + S[j - 1, k] + S[j, k - 1] + S[j, k])
						- temp * (JSX[j, k] - JSX[j - 1, k] + JSY[j, k] - JSY[j, k - 1]);
					MXhalf[j, k] = 0.25 * (MX[j - 1, k - 1] + MX[j - 1, k] + MX[j, k - 1] + MX[j, k])
						- temp * (JMXX[j, k] - JMXX[j - 1, k] + JMXY[j, k] - JMXY[j, k - 1] + 4 * DPX[j, k]);
					MYhalf[j, k] = 0.25 * (MY[j - 1, k - 1] + MY[j - 1, k] + MY[j, k - 1] + MY[j, k])
						- temp * (JMYX[j, k] - JMYX[j - 1, k] + JMYY[j, k] - JMYY[j, k - 1] + 4 * DPY[j, k]);
				}
			}

			Shalf[0, 0] = Shalf[1, 1];
			MXhalf[0, 0] = MXhalf[1, 1];
			MYhalf[0, 0] = MYhalf[1, 1];

			for(int j = 1; j < NX; j++)
			{
				Shalf[j, NY] = Shalf[j, NY - 1];
				MXhalf[j, NY] = MXhalf[j, NY - 1];
				MYhalf[j, NY] = -MYhalf[j, NY - 1];
			}
			for(int k = 1; k < NY; k++)
			{
				Shalf[NX, k] = Shalf[NX - 1, k];
				MXhalf[NX, k] = -MXhalf[NX - 1, k];
				MYhalf[NX, k] = MYhalf[NX - 1, k];
			}

			Shalf[NX, NY] = Shalf[NX - 1, NY - 1];
			MXhalf[NX, NY] = MXhalf[NX - 1, NY - 1];
			MYhalf[NX, NY] = MYhalf[NX - 1, NY - 1];

			UpdateHalfCurrents();

			temp = 0.5 * timeStep / GridCellSize;
			for(int j = 0; j < NX - 1; j++)
			{
				for(int k = 0; k < NY - 1; k++)
				{
					S[j, k] += -temp * (JSX[j + 1, k] - JSX[j, k] + JSY[j, k + 1] - JSY[j, k]);
					MX[j, k] += -temp * (JMXX[j + 1, k] - JMXX[j, k] + JMXY[j, k + 1] - JMXY[j, k] + DPX[j, k]);
					MY[j, k] += -temp * (JMYX[j + 1, k] - JMYX[j, k] + JMYY[j, k + 1] - JMYY[j, k] + DPY[j, k]);
				}
			}
		}

		private void UpdateCurrents()
		{
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					JSX[j, k] = S[j, k] * VX[j, k];
					JSY[j, k] = S[j, k] * VY[j, k];

					JMXX[j, k] = MX[j, k] * VX[j, k];
					JMXY[j, k] = MX[j, k] * VY[j, k];

					JMYX[j, k] = MY[j, k] * VX[j, k];
					JMYY[j, k] = MY[j, k] * VY[j, k];
				}
			}
		}

		private void UpdateHalfCurrents()
		{
			for(int j = 1; j < NX - 1; j++)
			{
				for(int k = 1; k < NY - 1; k++)
				{
					JSX[j, k] = Shalf[j, k] * 0.5 * (VX[j, k] + VX[j - 1, k]);
					JSY[j, k] = Shalf[j, k] * 0.5 * (VY[j, k] + VY[j, k - 1]);

					JMXX[j, k] = MXhalf[j, k] * 0.5 * (VX[j, k] + VX[j - 1, k]);
					JMXY[j, k] = MXhalf[j, k] * 0.5 * (VY[j, k] + VY[j, k - 1]);

					JMYX[j, k] = MYhalf[j, k] * 0.5 * (VX[j, k] + VX[j - 1, k]);
					JMYY[j, k] = MYhalf[j, k] * 0.5 * (VY[j, k] + VY[j, k - 1]);
				}
			}

			for(int j = 0; j < NX; j++)
			{
				JSX[j, NY - 1] = JSX[j, NY - 2];
				JMXX[j, NY - 1] = JMXX[j, NY - 2];
				JMYX[j, NY - 1] = JMYX[j, NY - 2];
				JSY[j, NY - 1] = JSY[j, NY - 2];
				JMXY[j, NY - 1] = JMXY[j, NY - 2];
				JMYY[j, NY - 1] = JMYY[j, NY - 2];
			}
			for(int k = 0; k < NY; k++)
			{
				JSX[NX - 1, k] = JSX[NX - 2, k];
				JMXX[NX - 1, k] = JMXX[NX - 2, k];
				JMYX[NX - 1, k] = JMYX[NX - 2, k];
				JSY[NX - 1, k] = JSY[NX - 2, k];
				JMXY[NX - 1, k] = JMXY[NX - 2, k];
				JMYY[NX - 1, k] = JMYY[NX - 2, k];
			}

			JSX[NX, NY] = JSX[NX - 1, NY - 1];
			JMXX[NX, NY] = JMXX[NX - 1, NY - 1];
			JMYX[NX, NY] = JMYX[NX - 1, NY - 1];
			JSY[NX, NY] = JSY[NX - 1, NY - 1];
			JMXY[NX, NY] = JMXY[NX - 1, NY - 1];
			JMYY[NX, NY] = JMYY[NX - 1, NY - 1];
		}

		private void UpdateVariables()
		{
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					double mPerp = Math.Sqrt(MX[j, k] * MX[j, k] + MY[j, k] * MY[j, k]);
					double gammaSquared = GetLorentzFactorSquared(j, k, mPerp);
					double absV = Math.Sqrt(1.0 - 1.0 / gammaSquared);

					VX[j, k] = mPerp == 0 ? 0 : absV * MX[j, k] / mPerp;
					VY[j, k] = mPerp == 0 ? 0 : absV * MY[j, k] / mPerp;
					T[j, k] = Math.Pow(S[j, k] / CurrentTime / gammaSquared, 1 / 3.0);
				}
			}

			UpdatePressureGradient();
		}

		private double GetLorentzFactorSquared(int j, int k, double mPerp)
		{
			// temp = Tau * dMperp^3 / S^4 / sqrt(27/4),
			// where Tau * dMperp^3 / S^4 = gamma^-1 - gamma^-3
			// and hence 0 <= temp <= 1
			double temp = CurrentTime * Math.Pow(mPerp, 3) / Math.Pow(S[j, k], 4) / Math.Sqrt(6.75);
			if(temp == 0)
			{
				return 1.0;
			}
			else if(temp < 1)
			{
				return 1.0 + 3 * temp * Math.Cosh(Arcosh(1.0 / temp) / 3.0);
			}
			else
			{
				return 1.0 + 3 * temp * Math.Cos(Math.Acos(1.0 / temp) / 3.0);
			}
		}

		private void UpdatePressureGradient()
		{
			for(int k = 0; k < NY; k++)
			{
				if(NX == NY)
				{
					// pressure gradient is zero due to symmetry
					DPX[0, k] = 0;
				}
				else
				{
					DPX[0, k] = (Math.Pow(T[1, k], 4) - Math.Pow(T[0, k], 4)) * CurrentTime / 4.0;
				}
				for(int j = 1; j < NX - 1; j++)
				{
					DPX[j, k] = 0.5 * (Math.Pow(T[j + 1, k], 4) - Math.Pow(T[j - 1, k], 4)) * CurrentTime / 4.0;
				}
				DPX[NX - 1, k] = (Math.Pow(T[NX - 1, k], 4) - Math.Pow(T[NX - 2, k], 4)) * CurrentTime / 4.0;
			}

			for(int j = 0; j < NX; j++)
			{
				// pressure gradient is zero due to symmetry
				DPY[j, 0] = 0;
				for(int k = 1; k < NY - 1; k++)
				{
					DPY[j, k] = 0.5 * (Math.Pow(T[j, k + 1], 4) - Math.Pow(T[j, k - 1], 4)) * CurrentTime / 4.0;
				}
				DPY[j, NY - 1] = (Math.Pow(T[j, NY - 1], 4) - Math.Pow(T[j, NY - 2], 4)) * CurrentTime / 4.0;
			}
		}
	}
}