/**************************************************************************************************
 * Tool class to calculate the gluon distribution function f^pi_g in the pion according to Glück,
 * Reya, Schienbein, Eur. Phys. J. C10 (1999) 313.
 **************************************************************************************************/

using System;

namespace Yburn.QQState
{
	public static class PionGDF
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static double MinEnergy
		{
			get
			{
				return Math.Sqrt(5e5);
			}
		}

		public static double GetValue(
			double bjorkenX, // fraction of the gluon momentum of the pion momentum (0 < x < 1)
			double gluonEnergy
			)
		{
			double ds = s(Math.Max(gluonEnergy, MinEnergy));

			return bjorkenX == 1 ? 0 : (Math.Pow(bjorkenX, a(ds))
				* (A(ds) + B(ds) * Math.Sqrt(bjorkenX) + C(ds) * bjorkenX)
				+ Math.Pow(ds, Alpha) * Math.Exp(-E(ds)
				+ Math.Sqrt(-Eprime(ds) * Math.Pow(ds, Beta) * Math.Log(bjorkenX))))
				* Math.Pow(1.0 - bjorkenX, D(ds)) / bjorkenX;
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		// Helper variables
		private static readonly double Alpha = 0.793;

		private static readonly double Beta = 1.722;

		public static readonly double NloScaleMeV = Math.Sqrt(4e5);

		private static readonly double ReferenceScaleMeV = 299;

		private static double a(
			double s
			)
		{
			return 1.418 - 0.215 * Math.Sqrt(s);
		}

		private static double A(
			double s
			)
		{
			return 5.392 + (0.553 - 0.385 * s) * s;
		}

		private static double B(
			double s
			)
		{
			return -11.928 + 1.844 * s;
		}

		private static double C(
			double s
			)
		{
			return 11.548 + (-4.316 + 0.382 * s) * s;
		}

		private static double D(
			double s
			)
		{
			return 1.347 + 1.135 * s;
		}

		private static double E(
			double s
			)
		{
			return 0.104 + 1.980 * s;
		}

		private static double Eprime(
			double s
			)
		{
			return 2.375 - 0.188 * s;
		}

		private static double s(
			double energyMeV
			)
		{
			return Math.Log(Math.Log(energyMeV / ReferenceScaleMeV)
				/ Math.Log(NloScaleMeV / ReferenceScaleMeV));
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/
	}
}