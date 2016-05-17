using System;

namespace Yburn.Fireball
{
	public static class PhysConst
	{
		// conversionant hbarc
		// in MeV fm
		public static readonly double HBARC = 197.3269788;

		public static readonly double FineStructureConstant = 7.2973525664E-3;

		public static readonly double ElementaryCharge = Math.Sqrt(4 * Math.PI * FineStructureConstant);

		// in MeV
		public static readonly double ChargedPionMass = 139.57018;

		// in MeV
		public static readonly double NeutralPionMass = 134.9766;

		// in MeV
		public static readonly double AveragePionMass = (2 * ChargedPionMass + NeutralPionMass) / 3;
	}
}