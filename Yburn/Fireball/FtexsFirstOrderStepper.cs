namespace Yburn.Fireball
{
	internal class FtexsFirstOrderStepper
	{
		public FtexsFirstOrderStepper(
			double gridCellSize,
			int nX,
			int nY,
			double[,] s,
			double[,] vX,
			double[,] vY,
			double[,] mX,
			double[,] mY,
			double[,] dPX,
			double[,] dPY
			)
		{
			GridCellSize = gridCellSize;
			NX = nX;
			NY = nY;
			S = s;
			VX = vX;
			VY = vY;
			MX = mX;
			MY = mY;
			DPX = dPX;
			DPY = dPY;

			VXedge = new double[NX + 1, NY];
			JSX = new double[NX + 1, NY];
			JMXX = new double[NX + 1, NY];
			JMYX = new double[NX + 1, NY];

			VYedge = new double[NX, NY + 1];
			JSY = new double[NX, NY + 1];
			JMXY = new double[NX, NY + 1];
			JMYY = new double[NX, NY + 1];
		}

		public void Advance(double timeStep)
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
		}

		private void UpdateCurrents()
		{
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

		private double GridCellSize;

		// Number of grid points in the spacial direction X
		private int NX;

		// Number of grid points in the spacial direction Y
		private int NY;

		// entropy density and currents
		private double[,] S;

		// x-component of the velocity evaluated at the cell-centers
		private double[,] VX;

		// y-component of the velocity evaluated at the cell-centers
		private double[,] VY;

		// x-component of the momentum density and currents
		private double[,] MX;

		private double[,] JSX;

		private double[,] JSY;

		private double[,] JMXX;

		private double[,] JMXY;

		// y-component of the momentum density and currents
		private double[,] MY;

		private double[,] JMYX;

		private double[,] JMYY;

		// pressure gradients
		private double[,] DPX;

		private double[,] DPY;

		// x-component of the velocity evaluated at the cell-edges
		private double[,] VXedge;

		// y-component of the velocity evaluated at the cell-edges
		private double[,] VYedge;
	}
}