using System;

namespace Yburn.PhysUtil
{
	public static class Constants
	{
		// quadratic Casimir in the fundamental representation of SU(3)
		public static readonly double QuadraticCasimir_Fundamental = 4.0 / 3.0; //1.33333333333333;

		// quadratic Casimir in the adjoint representation of SU(3)
		public static readonly double QuadraticCasimir_Adjoint = 3.0;

		public static readonly int NumberQCDColors = 3;

		// Euler-Mascheroni constant
		public static readonly double EulerMascheroniConstant = 0.57721566490153286061;

		// Riemann zeta function zeta(2)
		public static readonly double RiemannZetaFunctionAt2 = 1.64493406684823;

		// Riemann zeta function zeta(3)
		public static readonly double RiemannZetaFunctionAt3 = 1.20205690315959;

		// derivative of the Riemann zeta function zeta'(2)
		public static readonly double RiemannZetaFunctionDerivativeAt2 = -0.93754825431584;

		// in MeV
		public static readonly double BMesonMassMeV = 5279.26;

		/********************************************************************************************
		 * CODATA 2014
		 ********************************************************************************************/

		// conversionant hbar*c
		public static readonly double HbarCMeVFm = 197.3269788;

		public static readonly double FineStructureConstant = 7.2973525664E-3;

		public static readonly double ElementaryCharge = Math.Sqrt(4 * Math.PI * FineStructureConstant);

		/********************************************************************************************
		 * PDG 2015
		 ********************************************************************************************/

		public static readonly double BottomQuarkCharge = -ElementaryCharge / 3.0;

		public static readonly double BottomQuarkMassMeV = 4660;

		public static readonly double Etab1SMassMeV = 9398.0;

		public static readonly double Y1SMassMeV = 9460.30;

		public static readonly double Etab2SMassMeV = 9999;

		public static readonly double Y2SMassMeV = 10023.26;

		public static readonly double BPlusMesonMassMeV = 5279.29;

		public static readonly double BZeroMesonMassMeV = 5279.61;

		public static readonly double PionPlusMassMeV = 139.57018;

		public static readonly double PionZeroMassMeV = 134.9766;

		public static readonly double PionAverageMassMeV = (2.0 * PionPlusMassMeV + PionZeroMassMeV) / 3.0;
	}
}