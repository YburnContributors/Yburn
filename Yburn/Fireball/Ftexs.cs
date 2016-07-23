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
	public class Ftexs
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Ftexs(
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

		// x-component of the velocity evaluated at the cell-edges
		private double[,] VXedge;

		// y-component of the velocity evaluated at the cell-edges
		private double[,] VYedge;

		// entropy density and currents
		private double[,] S;

		private double[,] JSX;

		private double[,] JSY;

		// x-component of the momentum density and currents
		private double[,] MX;

		private double[,] JMXX;

		private double[,] JMXY;

		// y-component of the momentum density and currents
		private double[,] MY;

		private double[,] JMYX;

		private double[,] JMYY;

		// pressure gradients
		private double[,] DPX;

		private double[,] DPY;

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

			DPX = new double[NX, NY];
			DPY = new double[NX, NY];

			VXedge = new double[NX + 1, NY];

			JSX = new double[NX + 1, NY];

			JMXX = new double[NX + 1, NY];
			JMYX = new double[NX + 1, NY];

			VYedge = new double[NX, NY + 1];

			JSY = new double[NX, NY + 1];

			JMXY = new double[NX, NY + 1];
			JMYY = new double[NX, NY + 1];
			for(int k = 0; k < NY; k++)
			{
				VXedge[0, k] = VX[0, k];
				for(int j = 1; j < NX; j++)
				{
					VXedge[j, k] = 0.5 * (VX[j, k] + VX[j - 1, k]);
				}
				VXedge[NX, k] = VX[NX - 1, k];
			}

			for(int j = 0; j < NX; j++)
			{
				VYedge[j, 0] = VY[j, 0];

				for(int k = 1; k < NY; k++)
				{
					VYedge[j, k] = 0.5 * (VY[j, k] + VY[j, k - 1]);
				}
				VYedge[j, NY] = VY[j, NY - 1];
			}

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

		private void SetMaxCFL()
		{
			MaxCFL = FinalTime - InitialTime < 0 ? -Math.Abs(MaxCFL) : Math.Abs(MaxCFL);
		}

		private void Advance(double timeStep)
		{
			// First we set the currents JS = S * V and JMij = Mi * Vj,
			// which are also used to implement the boundary conditions.
			UpdateCurrents();

			double temp = timeStep / GridCellSize;
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					S[j, k] += -temp * (JSX[j + 1, k] - JSX[j, k] + JSY[j, k + 1] - JSY[j, k]);
					MX[j, k] += -temp * (JMXX[j + 1, k] - JMXX[j, k] + JMXY[j, k + 1] - JMXY[j, k] + DPX[j, k]);
					MY[j, k] += -temp * (JMYX[j + 1, k] - JMYX[j, k] + JMYY[j, k + 1] - JMYY[j, k] + DPY[j, k]);
				}
			}

			UpdateVariables();
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

		private void UpdateCurrents()
		{
			// X-currents
			for(int k = 0; k < NY; k++)
			{
				// boundary conditions at lower X-boundary
				if(NX == NY)
				{
					JSX[0, k] = -JSX[1, k];
					JMXX[0, k] = JMXX[1, k];
					JMYX[0, k] = -JMYX[1, k];
				}
				else
				{
					if(VXedge[0, k] < 0)
					{
						JSX[0, k] = S[0, k] * VXedge[0, k];
						JMXX[0, k] = MX[0, k] * VXedge[0, k];
						JMYX[0, k] = MY[0, k] * VXedge[0, k];
					}
					else
					{
						JSX[0, k] = 0;
						JMXX[0, k] = 0;
						JMYX[0, k] = 0;
					}
				}

				for(int j = 1; j < NX; j++)
				{
					if(VXedge[j, k] > 0)
					{
						JSX[j, k] = S[j - 1, k] * VXedge[j, k];
						JMXX[j, k] = MX[j - 1, k] * VXedge[j, k];
						JMYX[j, k] = MY[j - 1, k] * VXedge[j, k];
					}
					else
					{
						JSX[j, k] = S[j, k] * VXedge[j, k];
						JMXX[j, k] = MX[j, k] * VXedge[j, k];
						JMYX[j, k] = MY[j, k] * VXedge[j, k];
					}
				}

				// boundary conditions at upper X-boundary
				if(VXedge[NX, k] > 0)
				{
					JSX[NX, k] = S[NX - 1, k] * VXedge[NX, k];
					JMXX[NX, k] = MX[NX - 1, k] * VXedge[NX, k];
					JMYX[NX, k] = MY[NX - 1, k] * VXedge[NX, k];
				}
				else
				{
					JSX[NX, k] = 0;
					JMXX[NX, k] = 0;
					JMYX[NX, k] = 0;
				}
			}

			// Y-currents
			for(int j = 0; j < NX; j++)
			{
				// boundary conditions at lower Y-boundary
				JSY[j, 0] = -JSY[j, 1];
				JMXY[j, 0] = -JMXY[j, 1];
				JMYY[j, 0] = JMYY[j, 1];

				for(int k = 1; k < NY; k++)
				{
					if(VYedge[j, k] > 0)
					{
						JSY[j, k] = S[j, k - 1] * VYedge[j, k];
						JMXY[j, k] = MX[j, k - 1] * VYedge[j, k];
						JMYY[j, k] = MY[j, k - 1] * VYedge[j, k];
					}
					else
					{
						JSY[j, k] = S[j, k] * VYedge[j, k];
						JMXY[j, k] = MX[j, k] * VYedge[j, k];
						JMYY[j, k] = MY[j, k] * VYedge[j, k];
					}
				}

				// boundary conditions at upper Y-boundary
				if(VYedge[j, NY] > 0)
				{
					JSY[j, NY] = S[j, NY - 1] * VYedge[j, NY];
					JMXY[j, NY] = MX[j, NY - 1] * VYedge[j, NY];
					JMYY[j, NY] = MY[j, NY - 1] * VYedge[j, NY];
				}
				else
				{
					JSY[j, NY] = 0;
					JMXY[j, NY] = 0;
					JMYY[j, NY] = 0;
				}
			}
		}

		private void UpdateVariables()
		{
			double dGAMMA2, dAbsV, dMperp, dKK;
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					dMperp = Math.Sqrt(MX[j, k] * MX[j,
						k] + MY[j, k] * MY[j, k]);

					// dKK = Tau * dMperp^3 / S^4 / sqrt(27/4)
					dKK = dMperp / S[j, k];
					dKK = CurrentTime * dKK * dKK * dKK / S[j, k] / Math.Sqrt(6.75);

					dGAMMA2 = dKK == 0 ? 0 : (dKK < 1 ? 3 * dKK * Math.Cosh(Arcosh(1.0 / dKK) / 3.0)
										: 3 * dKK * Math.Cos(Math.Acos(1.0 / dKK) / 3.0));
					dGAMMA2 += 1;

					dAbsV = Math.Sqrt(1.0 - 1.0 / dGAMMA2);

					VX[j, k] = dMperp == 0 ? 0 : MX[j, k] / dMperp * dAbsV;
					VY[j, k] = dMperp == 0 ? 0 : MY[j, k] / dMperp * dAbsV;

					T[j, k] = Math.Pow(S[j, k] / CurrentTime / dGAMMA2, 1 / 3.0);
				}
			}

			for(int k = 0; k < NY; k++)
			{
				VXedge[0, k] = VX[0, k];
				for(int j = 1; j < NX; j++)
				{
					VXedge[j, k] = 0.5 * (VX[j, k] + VX[j - 1, k]);
				}
				VXedge[NX, k] = VX[NX - 1, k];
			}

			for(int j = 0; j < NX; j++)
			{
				VYedge[j, 0] = VY[j, 0];
				for(int k = 1; k < NY; k++)
				{
					VYedge[j, k] = 0.5 * (VY[j, k] + VY[j, k - 1]);
				}
				VYedge[j, NY] = VY[j, NY - 1];
			}

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