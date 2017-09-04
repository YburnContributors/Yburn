namespace Yburn.Fireball
{
	internal class FtexsLaxWendroffStepper
	{
		public FtexsLaxWendroffStepper(
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

			JSX = new double[NX + 1, NY + 1];
			JMXX = new double[NX + 1, NY + 1];
			JMYX = new double[NX + 1, NY + 1];

			JSY = new double[NX + 1, NY + 1];
			JMXY = new double[NX + 1, NY + 1];
			JMYY = new double[NX + 1, NY + 1];

			Shalf = new double[NX + 1, NY + 1];
			MXhalf = new double[NX + 1, NY + 1];
			MYhalf = new double[NX + 1, NY + 1];
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

		private double[,] Shalf;

		private double[,] MXhalf;

		private double[,] MYhalf;
	}
}
