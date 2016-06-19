using System;

namespace Yburn.PhysUtil
{
	public static class PhysConst
	{
		// quadratic Casimir in the fundamental representation of SU(3)
		public static readonly double CF = 1.33333333333333;

		// quadratic Casimir in the adjoint representation of SU(3)
		public static readonly double CA = 3;

		public static readonly int NC = 3;

		// Euler-Mascheroni constant
		public static readonly double EULER_GAMMA = 0.577215664901533;

		// conversionant hbarc
		// in MeV fm
		public static readonly double HBARC = 197.3269788;

		public static readonly double FineStructureConstant = 7.2973525664E-3;

		public static readonly double ElementaryCharge = Math.Sqrt(4 * Math.PI * FineStructureConstant);

		// Riemann zeta function zeta(2)
		public static readonly double Zeta2 = 1.64493406684823;

		// Riemann zeta function zeta(3)
		public static readonly double Zeta3 = 1.20205690315959;

		// derivative of the Riemann zeta function zeta'(2)
		public static readonly double Zetap2 = -0.93754825431584;

		// in MeV
		public static readonly double ChargedPionMass = 139.57018;

		// in MeV
		public static readonly double NeutralPionMass = 134.9766;

		// in MeV
		public static readonly double AveragePionMass = (2.0 * ChargedPionMass + NeutralPionMass) / 3.0;

		// in MeV
		public static readonly double BMesonMass = 5279.26;
	}
}