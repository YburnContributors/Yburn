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

		public static readonly double BMesonAverageMassMeV = (2.0 * BPlusMesonMassMeV + BZeroMesonMassMeV) / 3.0;

		public static readonly double PionPlusMassMeV = 139.57018;

		public static readonly double PionZeroMassMeV = 134.9766;

		public static readonly double PionAverageMassMeV = (2.0 * PionPlusMassMeV + PionZeroMassMeV) / 3.0;

		/********************************************************************************************
		 * pp Dimuon Decays (CMS2012 & ?)
		 ********************************************************************************************/

		public static readonly double ProtonProtonDimuonDecaysY1S = 1.0;

		public static readonly double ProtonProtonDimuonDecaysx1P = 0.271;

		public static readonly double ProtonProtonDimuonDecaysY2S = 0.56;

		public static readonly double ProtonProtonDimuonDecaysx2P = 0.105;

		public static readonly double ProtonProtonDimuonDecaysY3S = 0.41;

		/********************************************************************************************
		 * Partial widths (PDG 2015)
		 ********************************************************************************************/

		public static readonly double GammaTot3P = 1e100;

		// Partial widths for GammaCS
		//   public static readonly B3P3S = (8.5 + 3*11.5 + 5*13.9)/9.0;

		//   public static readonly B3P2P = 1e-7;

		//   public static readonly B3P2S = (1.0 + 3*3.5 + 5*6.5)/9.0;

		//   public static readonly B3P1P = 1e-7;

		//   public static readonly B3P1S = (0.29 + 3*3.1 + 5*8.7)/9.0;

		// Partial widths for GammaAFrel
		public static readonly double B3P3S = (7.8 + 3 * 9.4 + 5 * 11.4) / 9.0 / GammaTot3P;

		public static readonly double B3P2P = 1e-7 / GammaTot3P;

		public static readonly double B3P2S = (1.0 + 3 * 2.4 + 5 * 4.4) / 9.0 / GammaTot3P;

		public static readonly double B3P1P = 1e-7 / GammaTot3P;

		public static readonly double B3P1S = (0.33 + 3 * 1.7 + 5 * 4.6) / 9.0 / GammaTot3P;

		public static readonly double B3S2P = (0.131 + 0.126 + 0.059);

		public static readonly double B3S2S = 0.106;

		public static readonly double B3S1P = (0.0099 + 0.0009 + 0.0027);

		public static readonly double B3S1S = (0.0437 + 0.022);

		public static readonly double B2P1S = (0.009 + 3 * (0.0163 + 0.092) + 5 * (0.011 + 0.07)) / 9.0;

		public static readonly double B2P1P = (3 * 0.0091 + 5 * 0.0051) / 9.0;

		public static readonly double B2P2S = (0.046 + 3 * 0.199 + 5 * 0.106) / 9.0;

		public static readonly double B2S1P = (0.069 + 0.0715 + 0.038);

		public static readonly double B2S1S = (0.1785 + 0.086); //(0.1792 + 0.086);

		public static readonly double B1P1S = (0.0176 + 3 * 0.339 + 5 * 0.191) / 9.0;

		public static readonly double B3Smu = 0.0218;

		public static readonly double B2Smu = 0.0193;

		public static readonly double B1Smu = 0.0248;
	}
}