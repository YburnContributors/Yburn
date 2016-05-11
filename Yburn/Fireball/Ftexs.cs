//////////////////////////////////////////////////////////////////////////////////////////////////////////
// The class Ftexs (Fireball Transverse Expansion Solver) implements a solver specified for the Euler   //
// equations of the relativistic perfect fluid in (quasi) two dimensions. It is specified to the case   //
// of the expanding fireball formed in relativistic heavy ion collisions, i.e. it calculates energy     //
// density and velocity distributions for the case of a transverse expanding medium superposed with a   //
// Bjorken expansion in the longitudinal direction.                                                     //
//                                                                                                      //
// The following set of equations is solved:                                                            //
//                                                                                                      //
// del_Tau S = - del_X ( S VX ) - del_Y ( S VY ),                                                       //
// del_Tau MX = - del_X ( MX VX + P ) - del_Y ( MX VY ),                                                //
// del_Tau MY = - del_X ( MY VX ) - del_Y ( MY VY + P ),                                                //
//                                                                                                      //
// GAMMA = 1/sqrt(1 - VX^2 - VY^2),                                                                     //
// MX = VX Tau GAMMA^2 T^4,                                                                             //
// MY = VY Tau GAMMA^2 T^4,                                                                             //
// P = (1/4) * Tau T^4,                                                                                 //
// S = Tau GAMMA T^3,                                                                                   //
//                                                                                                      //
// where the variables correspond to (up to constant prefactors) Lorentz factor (GAMMA), momentum       //
// density (MX,MY), pressure (P), entropy density (S), temperature (T) and velocity (VX,VY),            //
// respectively, which are functions of the coordinates of space (X,Y) and longitudinal proper time     //
// (Tau).                                                                                               //
//////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;

namespace Yburn.Fireball
{
	public class Ftexs : IDisposable
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Ftexs(
			double gridCellSize,
			double initialTime,
			double maxCFL, // Maximum allowed Courant-Friedrichs-Levy number
			double[,] initialTemperature,
			double[,] initialXvelocity,
			double[,] initialYvelocity,
			double artViscStrength = 0, // Coefficient of artificial viscosity
			string logFile = ""
			)
		{
			T = initialTemperature;
			VX = initialXvelocity;
			VY = initialYvelocity;

			MaxCFL = maxCFL;
			ArtViscStrength = artViscStrength;

			AssertValidMembers();

			NX = T.GetLength(0);
            NY = T.GetLength(1);
			DXY = gridCellSize;

			LogFile = logFile;
			if(!string.IsNullOrEmpty(LogFile))
			{
				LogFileStream = new FileStream(LogFile, FileMode.Create, FileAccess.Write);
				LogStreamWriter = new StreamWriter(LogFileStream);
			}

			Tau = TauI = initialTime;
			NTau = 0;

			S = new double[NX, NY];
			MX = new double[NX, NY];
			MY = new double[NX, NY];
			DPX = new double[NX, NY];
			DPY = new double[NX, NY];
			ArtViscX = new double[NX, NY];
			ArtViscY = new double[NX, NY];

			VXedge = new double[NX + 1, NY];
			JSX = new double[NX + 1, NY];
			JMXX = new double[NX + 1, NY];
			JMYX = new double[NX + 1, NY];

			VYedge = new double[NX, NY + 1];
			JSY = new double[NX, NY + 1];
			JMXY = new double[NX, NY + 1];
			JMYY = new double[NX, NY + 1];

			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					// check if square of the velocity exceeds one
					double dV2 = VX[j, k] * VX[j, k] + VY[j, k] * VY[j, k];
					if(dV2 >= 1)
					{
						throw new Exception("|V| >= 1.");
					}

					if(T[j, k] <= 0)
					{
						throw new Exception("T should only take positive values.");
					}

					// calculate Lorentz factor
					double dGAMMA = 1.0 / Math.Sqrt(1.0 - dV2);
					S[j, k] = Tau * dGAMMA * T[j, k] * T[j, k] * T[j, k];

					// calculate absolute value and components of the momentum density
					double dMTMP = dGAMMA * T[j, k] * S[j, k];
					MX[j, k] = dMTMP * VX[j, k];
					MY[j, k] = dMTMP * VY[j, k];
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
                if (NX == NY)
                {
                    // pressure gradient is zero due to symmetry
                    DPX[0, k] = 0;
                }
                else
                {
                    DPX[0, k] = (Math.Pow(T[0, k], 4) - Math.Pow(T[1, k], 4)) * Tau / 4.0;
                }
				for(int j = 1; j < NX - 1; j++)
				{
					DPX[j, k] = 0.5 * (Math.Pow(T[j + 1, k], 4) - Math.Pow(T[j - 1, k], 4)) * Tau / 4.0;
				}
				DPX[NX - 1, k] = (Math.Pow(T[NX - 1, k], 4) - Math.Pow(T[NX - 2, k], 4)) * Tau / 4.0;
			}

			for(int j = 0; j < NX; j++)
			{
				// pressure gradient is zero due to symmetry
				DPY[j, 0] = 0;
				for(int k = 1; k < NY - 1; k++)
				{
					DPY[j, k] = 0.5 * (Math.Pow(T[j, k + 1], 4) - Math.Pow(T[j, k - 1], 4)) * Tau / 4.0;
				}
				DPY[j, NY - 1] = (Math.Pow(T[j, NY - 1], 4) - Math.Pow(T[j, NY - 2], 4)) * Tau / 4.0;
			}

			if(ArtViscStrength == 0)
			{
				for(int j = 0; j < NX; j++)
				{
					for(int k = 0; k < NY; k++)
					{
						ArtViscX[j, k] = ArtViscY[j, k] = 0;
					}
				}
			}
		}

		~Ftexs()
		{
			Dispose(false);
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

		// Do not make this method virtual. A derived class should not be able to override this method.
		public void Dispose()
		{
			Dispose(true);
			// Call GC.SupressFinalize to take this object off the finalization queue
			// and prevent finalization code for this object from executing a second time.
			GC.SuppressFinalize(this);
		}

		public void SolveUntil(
			double dTauF
			)
		{
			TauF = dTauF;
			MaxCFL = TauF - TauI < 0 ? -Math.Abs(MaxCFL) : Math.Abs(MaxCFL);

			while(Tau < TauF)
			{
				DTau = CalcdTau();

				if(Tau + DTau >= TauF)
				{
					DTau = TauF - Tau;
					Tau = TauF;
				}
				else
				{
					Tau += DTau;
				}

				C = DTau / DXY;
				NTau++;

				Advance();

				if(!string.IsNullOrEmpty(LogFile))
				{
					LogStreamWriter.WriteLine("Tau = " + Tau + ", dTau = " + DTau + ", NTau = " + NTau);
				}
			}

			TauI = Tau;
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

		private static bool IsSquareArray(
			double[,] array
			)
		{
			return array.GetLength(0) == array.GetLength(1);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		// Track whether Dispose has been called.
		private bool Disposed = false;

		// initial temporal boundary of the domain of calculation
		private double TauI;

		// final temporal boundary of the domain of calculation
		private double TauF;

		// current time within the calculation
		private double Tau;

		// temporal grid spacing (not necessarily constant throughout the grid)
		private double DTau;

		// Spacial grid spacing (constant throughout the grid)
		private double DXY;

		// Number of grid points in the temporal direction
		private uint NTau;

		// Number of grid points in the spacial direction X
		private int NX;

        // Number of grid points in the spacial direction Y
        private int NY;

        // Maximum allowed Courant-Friedrichs-Levy number
        private double MaxCFL;

		// Name of log file
		private string LogFile;

		// Writing useful info into log file
		private FileStream LogFileStream;

		private StreamWriter LogStreamWriter;

		// helper variable
		private double C;

		// Coefficient of artificial viscosity
		private double ArtViscStrength;

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

		// artificial visocity field
		private double[,] ArtViscX;

		private double[,] ArtViscY;

		private void AssertValidMembers()
		{
//			if(!IsSquareArray(T))
//			{
//				throw new NonSquareArrayException("T");
//			}
//
//			if(!IsSquareArray(VX))
//			{
//				throw new NonSquareArrayException("VX");
//			}
//
//			if(!IsSquareArray(VY))
//			{
//				throw new NonSquareArrayException("VY");
//			}
//
			if(T.Length != VX.Length)
			{
				throw new Exception("VX is not of the same size as T.");
			}

			if(T.Length != VY.Length)
			{
				throw new Exception("VY is not of the same size as T.");
			}

			if(MaxCFL <= 0)
			{
				throw new Exception("MaxCFL <= 0.");
			}

			if(ArtViscStrength < 0)
			{
				throw new Exception("ArtViscStrength < 0.");
			}
		}

		private void Advance()
		{
			// First we set the currents JS = S * V and JMij = Mi * Vj, which are also used to implement the boundary conditions.
			// The large amount of hard-coding required for this procedure is outsourced into the function SetAllJ().
			SetAllJ();

			if(ArtViscStrength > 0)
			{
				SetArtVisc();
			}

			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					S[j, k] += -C * (JSX[j + 1, k] - JSX[j, k] + JSY[j, k + 1] - JSY[j, k]);
					MX[j, k] += -C * (JMXX[j + 1, k] - JMXX[j, k] + JMXY[j, k + 1] - JMXY[j, k] + DPX[j, k]) + ArtViscX[j, k];
					MY[j, k] += -C * (JMYX[j + 1, k] - JMYX[j, k] + JMYY[j, k + 1] - JMYY[j, k] + DPY[j, k]) + ArtViscY[j, k];
				}
			}

			UpdateVars();
		}

		// find out maximum transport velocity from v and dP to adjust dT so that CFLmax is not exceeded
		private double CalcdTau()
		{
			double dVdXmax = 0;
			for(int j = 0; j < NX; j++)
			{
				for(int k = 0; k < NY; k++)
				{
					dVdXmax = Math.Max(dVdXmax, Math.Abs(VX[j, k]) / DXY);
					dVdXmax = Math.Max(dVdXmax, Math.Sqrt(Math.Abs(DPX[j, k] / Math.Pow(T[j, k], 4))) / DXY);
					dVdXmax = Math.Max(dVdXmax, Math.Abs(VY[j, k]) / DXY);
					dVdXmax = Math.Max(dVdXmax, Math.Sqrt(Math.Abs(DPY[j, k] / Math.Pow(T[j, k], 4))) / DXY);
				}
			}

			// If dVdXmax == 0 then V and dP both vanish identically, i.e. we have purely longitudinal.
			// In this case the can jump to the final time TauF right away.
			return dVdXmax == 0 ?
				TauF - Tau :
				MaxCFL / dVdXmax;
		}

		private void SetAllJ()
		{
			// X-currents
			for(int k = 0; k < NY; k++)
			{
				// boundary conditions at lower X-boundary
				JSX[0, k] = -JSX[1, k];
				JMXX[0, k] = JMXX[1, k];
				JMYX[0, k] = -JMYX[1, k];

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
			for(int j = 0; j < NY; j++)
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

		private void SetArtVisc()
		{
			for(int j = 1; j < NX - 1; j++)
			{
				for(int k = 1; k < NX - 1; k++)
				{
					ArtViscX[j, k] = ArtViscStrength * (MX[j + 1, k] + MX[j - 1, k] + MX[j, k + 1] + MX[j, k - 1] - 4 * MX[j, k]);
					ArtViscY[j, k] = ArtViscStrength * (MY[j + 1, k] + MY[j - 1, k] + MY[j, k + 1] + MY[j, k - 1] - 4 * MY[j, k]);
				}
			}

			for(int j = 1; j < NX - 1; j++)
			{
				ArtViscX[j, 0] = ArtViscStrength * (MX[j + 1, 0] + MX[j - 1, 0] + MX[j, 2] - MX[j, 0] - 2 * MX[j, 1]);
				ArtViscY[j, 0] = ArtViscStrength * (MY[j + 1, 0] + MY[j - 1, 0] + MY[j, 2] - MY[j, 0] - 2 * MY[j, 1]);

				ArtViscX[j, NY - 1] = ArtViscStrength * (MX[j + 1, NY - 1] + MX[j - 1, NY - 1] + MX[j, NY - 3] - MX[j, NY - 1] - 2 * MX[j, NY - 2]);
				ArtViscY[j, NY - 1] = ArtViscStrength * (MY[j + 1, NY - 1] + MY[j - 1, NY - 1] + MY[j, NY - 3] - MY[j, NY - 1] - 2 * MY[j, NY - 2]);
			}

			for(int k = 1; k < NY - 1; k++)
			{
				ArtViscX[0, k] = ArtViscStrength * (MX[0, k + 1] + MX[0, k - 1] + MX[2, k] - MX[0, k] - 2 * MX[1, k]);
				ArtViscY[0, k] = ArtViscStrength * (MY[0, k + 1] + MY[0, k - 1] + MY[2, k] - MY[0, k] - 2 * MY[1, k]);

				ArtViscX[NX - 1, k] = ArtViscStrength * (MX[NX - 1, k + 1] + MX[NX - 1, k - 1] + MX[NX - 3, k] - MX[NX - 1, k] - 2 * MX[NX - 2, k]);
				ArtViscY[NX - 1, k] = ArtViscStrength * (MY[NX - 1, k + 1] + MY[NX - 1, k - 1] + MY[NX - 3, k] - MY[NX - 1, k] - 2 * MY[NX - 2, k]);
			}

			ArtViscX[0, 0] = ArtViscStrength * (MX[0, 2] + MX[2, 0] + 2 * (MX[0, 0] - MX[0, 1] - MX[1, 0]));
			ArtViscY[0, NY - 1] = ArtViscStrength * (MY[0, NY - 3] + MY[2, NY - 1] + 2 * (MY[0, NY - 1] - MY[0, NY - 2] - MY[1, NY - 1]));

			ArtViscX[NX - 1, 0] = ArtViscStrength * (MX[NX - 1, 2] + MX[NX - 3, 0]
				+ 2 * (MX[NX - 1, 0] - MX[NX - 1, 1] - MX[NX - 2, 0]));
			ArtViscY[NX - 1, NY - 1] = ArtViscStrength * (MY[NX - 1, NY - 3] + MY[NX - 3, NY - 1]
				+ 2 * (MY[NX - 1, NY - 1] - MY[NX - 1, NY - 2] - MY[NX - 2, NY - 1]));
		}

		private void UpdateVars()
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
					dKK = Tau * dKK * dKK * dKK / S[j, k] / Math.Sqrt(6.75);

					dGAMMA2 = dKK == 0 ? 0 : (dKK < 1 ? 3 * dKK * Math.Cosh(Arcosh(1.0 / dKK) / 3.0)
										: 3 * dKK * Math.Cos(Math.Acos(1.0 / dKK) / 3.0));
					dGAMMA2 += 1;

					dAbsV = Math.Sqrt(1.0 - 1.0 / dGAMMA2);

					// for highly relativistic flows, rounding erros may cause the velocity to equal 1
					if(dAbsV == 1)
					{
						LogStreamWriter.WriteLine("|V| = 1");
						LogStreamWriter.WriteLine(string.Format("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}", "Tau", "j", "k", "KK", "|V|", "GAMMA2"));
						LogStreamWriter.WriteLine(string.Format("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}", Tau, j, k, dKK, dAbsV, dGAMMA2));
					}

					VX[j, k] = dMperp == 0 ? 0 : MX[j, k] / dMperp * dAbsV;
					VY[j, k] = dMperp == 0 ? 0 : MY[j, k] / dMperp * dAbsV;

					T[j, k] = Math.Pow(S[j, k] / Tau / dGAMMA2, 1 / 3.0);
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
				// pressure gradient is zero due to symmetry
				DPX[0, k] = 0;
				for(int j = 1; j < NX - 1; j++)
				{
					DPX[j, k] = 0.5 * (Math.Pow(T[j + 1, k], 4) - Math.Pow(T[j - 1, k], 4)) * Tau / 4.0;
				}
				DPX[NX - 1, k] = (Math.Pow(T[NX - 1, k], 4) - Math.Pow(T[NX - 2, k], 4)) * Tau / 4.0;
			}

			for(int j = 0; j < NX; j++)
			{
				// pressure gradient is zero due to symmetry
				DPY[j, 0] = 0;
				for(int k = 1; k < NY - 1; k++)
				{
					DPY[j, k] = 0.5 * (Math.Pow(T[j, k + 1], 4) - Math.Pow(T[j, k - 1], 4)) * Tau / 4.0;
				}
				DPY[j, NY - 1] = (Math.Pow(T[j, NY - 1], 4) - Math.Pow(T[j, NY - 2], 4)) * Tau / 4.0;
			}
		}

		// If bDisposing equals true, the method has been called by the user's code. Managed and unmanaged resources
		// can be disposed. If disposing equals false, the method has been called by the runtime from inside the
		// finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
		protected virtual void Dispose(
			bool bDisposing
			)
		{
			if(!Disposed)
			{
				if(bDisposing)
				{
					// Dispose managed resources.
				}

				// Dispose unmanaged resources.
				if(LogStreamWriter != null)
				{
					LogStreamWriter.Close();
					LogStreamWriter.Dispose();
					LogStreamWriter = null;
				}

				if(LogFileStream != null)
				{
					LogFileStream.Close();
					LogFileStream.Dispose();
					LogFileStream = null;
				}

				// Note disposing has been done.
				bDisposing = true;
			}
		}
	}

	public class NonSquareArrayException : Exception
	{
		public NonSquareArrayException(
			string fieldName
			)
			: base(fieldName + " is not a square array.")
		{
		}
	}
}