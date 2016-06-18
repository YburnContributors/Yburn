using Meta.Numerics;
using Meta.Numerics.Functions;
using System;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum PotentialType
	{
		Complex = 1 << 0,
		Complex_NoString = 1 << 1,
		LowT = 1 << 2,
		LowT_NoString = 1 << 3,
		Real = 1 << 4,
		Real_NoString = 1 << 5,
		Tzero = 1 << 6,
		Tzero_NoString = 1 << 7,
		SpinDependent = 1 << 8,
	};

	public abstract class Potential
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Potential(
			double alphaSoft,
			double sigmaMeV,
			ColorState colorState
			)
		{
			AlphaSoft = alphaSoft;
			SigmaFm = sigmaMeV / PhysConst.HBARC / PhysConst.HBARC;
			ColorState = colorState;

			SetHelperVariables();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public abstract PotentialType PotentialType
		{
			get;
		}

		public abstract bool IsReal
		{
			get;
		}

		public abstract Complex Value(
			double radiusFm
			);

		public double AlphaEff
		{
			get;
			private set;
		}

		public double SigmaEffMeV
		{
			get
			{
				return SigmaEffFm * PhysConst.HBARC * PhysConst.HBARC;
			}
		}

		public void UpdateAlpha(
			double alphaSoft
			)
		{
			AlphaSoft = alphaSoft;
			SetHelperVariables();
		}

		public abstract double ValueAtInfinity
		{
			get;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double AlphaSoft;

		private double SigmaFm;

		protected double SigmaEffFm;

		protected ColorState ColorState;

		protected virtual void SetHelperVariables()
		{
			if(ColorState == ColorState.Singlet)
			{
				AlphaEff = PhysConst.CF * AlphaSoft;
				SigmaEffFm = SigmaFm;
			}
			else if(ColorState == ColorState.Octet)
			{
				AlphaEff = (PhysConst.CF - 0.5 * PhysConst.CA) * AlphaSoft;
				SigmaEffFm = PhysConst.CA / PhysConst.CF * SigmaFm;
			}
		}
	}

	public abstract class FiniteTemperaturePotential : Potential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FiniteTemperaturePotential(
			double alphaSoft,
			double sigmaMeV,
			ColorState colorState,
			double temperatureMeV,
			double debyeMassMeV
			)
			: base(alphaSoft, sigmaMeV, colorState)
		{
			TemperatureMeV = temperatureMeV;
			DebyeMassMeV = debyeMassMeV;

			SetHelperVariables();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double TemperatureMeV;

		protected double DebyeMassMeV;

		protected double AlphaOverDebyeMassFm;

		protected double SigmaOverDebyeMassFm;

		protected override void SetHelperVariables()
		{
			base.SetHelperVariables();

			AlphaOverDebyeMassFm = AlphaEff * TemperatureMeV / PhysConst.HBARC;
			SigmaOverDebyeMassFm = SigmaEffFm / DebyeMassMeV * PhysConst.HBARC;
		}
	}

	public class ComplexPotential : FiniteTemperaturePotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ComplexPotential(
			double alphaSoft,
			double sigmaMeV,
			ColorState colorState,
			double temperatureMeV,
			double debyeMassMeV
			)
			: base(alphaSoft, sigmaMeV, colorState, temperatureMeV, debyeMassMeV)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.Complex;
			}
		}

		public override double ValueAtInfinity
		{
			get
			{
				return SigmaEffMeV / DebyeMassMeV - AlphaEff * DebyeMassMeV;
			}
		}

		public override bool IsReal
		{
			get
			{
				return false;
			}
		}

		public override Complex Value(
			double radiusFm
			)
		{
			double X = DebyeMassMeV * radiusFm / PhysConst.HBARC;
			if(radiusFm == 0)
			{
				return new Complex(0, 0);
			}
			else if(X <= 10)
			{
				return new Complex(-(AlphaEff / radiusFm + SigmaOverDebyeMassFm) * Math.Exp(-X),
					-AlphaOverDebyeMassFm
					* (1.0 - 0.5 * (AdvancedMath.IntegralEi(X) * Math.Exp(-X) * (1.0 / X + 1.0)
					+ AdvancedMath.IntegralE(1, X) * Math.Exp(X) * (1.0 / X - 1.0))));
			}
			else
			{
				double X2 = X * X;
				return new Complex(-(AlphaEff / radiusFm + SigmaOverDebyeMassFm) * Math.Exp(-X),
					-AlphaOverDebyeMassFm
					* (1.0 - (((40.0 / X2 + 1.0) * 18.0 / X2 + 1.0) * 4.0 / X2 + 1.0) * 2.0 / X2));
			}
		}
	}

	public class ComplexPotential_NoString : ComplexPotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ComplexPotential_NoString(
			double alphaSoft,
			ColorState colorState,
			double temperatureMeV,
			double debyeMassMeV
			)
			: base(alphaSoft, 0, colorState, temperatureMeV, debyeMassMeV)
		{
		}

		/********************************************************************************************
		* Public members, functions and properties
		********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.Complex_NoString;
			}
		}
	}

	public class LowTemperaturePotential : FiniteTemperaturePotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public LowTemperaturePotential(
			double alphaSoft,
			double sigmaMeV,
			ColorState colorState,
			double temperatureMeV,
			double debyeMassMeV
			)
			: base(alphaSoft, sigmaMeV, colorState, temperatureMeV, debyeMassMeV)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.LowT;
			}
		}

		public override double ValueAtInfinity
		{
			get
			{
				return 0; // actually infinity
			}
		}

		public override bool IsReal
		{
			get
			{
				return false;
			}
		}

		public override Complex Value(
				double radiusFm
				)
		{
			double PI = Math.PI;
			double xt = TwoPiTemperatureFm * radiusFm;
			double xt2 = xt * xt;
			double xm = DebyeMassFm * radiusFm;
			double xm2 = xm * xm;
			return new Complex(
				-SigmaOverDebyeMassFm * Math.Exp(-xm)
				- AlphaEff / radiusFm * (1.0 - PhysConst.NC * AlphaSoft * xt2 / 36.0 / PI
				+ 3 * PhysConst.Zeta3 / 4.0 / PI / PI * xt * xm2
				- PhysConst.Zeta3 * PhysConst.NC * AlphaSoft * xt * xt2 / 12.0 / PI / PI / PI
				- xm * xm2 / 6.0),
				-AlphaOverDebyeMassFm * (PhysConst.NC * PhysConst.NC * AlphaSoft * AlphaSoft / 6.0
				+ xm2 / 6.0 * (2 * Math.Log(TemperatureMeV / DebyeMassMeV) + 1
				+ 4 * Math.Log(2) + 2 * PhysConst.Zetap2 / PhysConst.Zeta2 - 2 * PhysConst.EULER_GAMMA)
				- Math.Log(2) * PhysConst.NC * AlphaSoft / 9.0 / PI * xt2));
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double TwoPiTemperatureFm;

		protected double TwoPiTemperatureFm2;

		protected double DebyeMassFm;

		protected double DebyeMassFm2;

		protected override void SetHelperVariables()
		{
			base.SetHelperVariables();

			TwoPiTemperatureFm = 2 * Math.PI * TemperatureMeV / PhysConst.HBARC;
			TwoPiTemperatureFm2 = TwoPiTemperatureFm * TwoPiTemperatureFm;

			DebyeMassFm = DebyeMassMeV / PhysConst.HBARC;
			DebyeMassFm2 = DebyeMassFm * DebyeMassFm;
		}
	}

	public class LowTemperaturePotential_NoString : LowTemperaturePotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public LowTemperaturePotential_NoString(
			double alphaSoft,
			ColorState colorState,
			double temperatureMeV,
			double debyeMassMeV
			)
			: base(alphaSoft, 0, colorState, temperatureMeV, debyeMassMeV)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.LowT_NoString;
			}
		}
	}

	public class RealPotential : FiniteTemperaturePotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public RealPotential(
			double alphaSoft,
			double sigmaMeV,
			ColorState colorState,
			double debyeMassMeV
			)
			: base(alphaSoft, sigmaMeV, colorState, 0, debyeMassMeV)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.Real;
			}
		}

		public override double ValueAtInfinity
		{
			get
			{
				return SigmaEffMeV / DebyeMassMeV - AlphaEff * DebyeMassMeV;
			}
		}

		public override bool IsReal
		{
			get
			{
				return true;
			}
		}

		public override Complex Value(
			double radiusFm
			)
		{
			return new Complex(-(AlphaEff / radiusFm + SigmaOverDebyeMassFm)
				* Math.Exp(-DebyeMassMeV * radiusFm / PhysConst.HBARC), 0);
		}
	}

	public class RealPotential_NoString : RealPotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public RealPotential_NoString(
			double alphaSoft,
			ColorState colorState,
			double debyeMassMeV
			)
			: base(alphaSoft, 0, colorState, debyeMassMeV)
		{
		}

		/********************************************************************************************
		* Public members, functions and properties
		********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.Real_NoString;
			}
		}
	}

	public class VacuumPotential : Potential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public VacuumPotential(
			double alphaSoft,
			double sigmaMeV,
			ColorState colorState
			)
			: base(alphaSoft, sigmaMeV, colorState)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.Tzero;
			}
		}

		public override double ValueAtInfinity
		{
			get
			{
				return 0; // actually infinity
			}
		}

		public override bool IsReal
		{
			get
			{
				return true;
			}
		}

		public override Complex Value(
			double radiusFm
			)
		{
			return new Complex(-AlphaEff / radiusFm + SigmaEffFm * radiusFm, 0);
		}
	}

	public class VacuumPotential_NoString : VacuumPotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public VacuumPotential_NoString(
			double alphaSoft,
			ColorState colorState
			)
			: base(alphaSoft, 0, colorState)
		{
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.Tzero_NoString;
			}
		}

		public override Complex Value(
			double radiusFm
			)
		{
			return new Complex(-AlphaEff / radiusFm, 0);
		}
	}

	public class SpinDependentPotential : VacuumPotential
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public SpinDependentPotential(
			double alphaSoft,
			double sigmaMeV,
			ColorState colorState,
			SpinState spinState,
			double spinCouplingRangeFm,
			double spinCouplingStrengthMeV
			)
			: base(alphaSoft, sigmaMeV, colorState)
		{
			SpinState = spinState;
			SpinCouplingRange = spinCouplingRangeFm;
			SpinCouplingStrength = spinCouplingStrengthMeV;
			SetHelperVariables();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public override PotentialType PotentialType
		{
			get
			{
				return PotentialType.SpinDependent;
			}
		}

		public override Complex Value(
			double radiusFm
			)
		{
			return new Complex(-AlphaEff / radiusFm + SigmaEffFm * radiusFm
				+ SpinFactor * Math.Exp(-radiusFm / SpinCouplingRange), 0);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double QuarkSpin = 0.5;

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected SpinState SpinState
		{
			get;
			private set;
		}

		private double SpinCouplingStrength;

		private double SpinCouplingRange;

		private double SpinFactor;

		private double TotalSpin;

		protected override void SetHelperVariables()
		{
			base.SetHelperVariables();

			TotalSpin = SpinState == SpinState.Triplet ? 1 : 0;
			SpinFactor = TotalSpin * (TotalSpin + 1) - 2 * QuarkSpin * (QuarkSpin + 1);
			SpinFactor *= SpinCouplingStrength;
		}
	}
}