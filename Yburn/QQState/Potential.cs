using Meta.Numerics;
using Meta.Numerics.Functions;
using System;
using Yburn.PhysUtil;

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
			double sigma_MeV,
			ColorState colorState
			)
		{
			AlphaSoft = alphaSoft;
			Sigma_fm = sigma_MeV / Constants.HbarC_MeV_fm / Constants.HbarC_MeV_fm;
			ColorState = colorState;

			AssertValidMembers();
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
			double radius_fm
			);

		public double AlphaEff
		{
			get;
			private set;
		}

		public double SigmaEff_MeV
		{
			get
			{
				return SigmaEff_fm * Constants.HbarC_MeV_fm * Constants.HbarC_MeV_fm;
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

		private double Sigma_fm;

		protected double SigmaEff_fm;

		protected ColorState ColorState;

		private void AssertValidMembers()
		{
			if(Sigma_fm < 0)
			{
				throw new Exception("Sigma < 0.");
			}
		}

		protected virtual void SetHelperVariables()
		{
			if(ColorState == ColorState.Singlet)
			{
				AlphaEff = Constants.QuadraticCasimir_Fundamental * AlphaSoft;
				SigmaEff_fm = Sigma_fm;
			}
			else if(ColorState == ColorState.Octet)
			{
				AlphaEff = (Constants.QuadraticCasimir_Fundamental - 0.5 * Constants.QuadraticCasimir_Adjoint) * AlphaSoft;
				SigmaEff_fm = Constants.QuadraticCasimir_Adjoint / Constants.QuadraticCasimir_Fundamental * Sigma_fm;
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
			double sigma_MeV,
			ColorState colorState,
			double temperature_MeV,
			double debyeMass_MeV
			)
			: base(alphaSoft, sigma_MeV, colorState)
		{
			Temperature_MeV = temperature_MeV;
			DebyeMass_MeV = debyeMass_MeV;

			SetHelperVariables();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double Temperature_MeV;

		protected double DebyeMass_MeV;

		protected double AlphaOverDebyeMass_fm;

		protected double SigmaOverDebyeMass_fm;

		protected override void SetHelperVariables()
		{
			base.SetHelperVariables();

			AlphaOverDebyeMass_fm = AlphaEff * Temperature_MeV / Constants.HbarC_MeV_fm;
			SigmaOverDebyeMass_fm = SigmaEff_fm / DebyeMass_MeV * Constants.HbarC_MeV_fm;
		}
	}

	public class ComplexPotential : FiniteTemperaturePotential
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public ComplexPotential(
			double alphaSoft,
			double sigma_MeV,
			ColorState colorState,
			double temperature_MeV,
			double debyeMass_MeV
			)
			: base(alphaSoft, sigma_MeV, colorState, temperature_MeV, debyeMass_MeV)
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
				return SigmaEff_MeV / DebyeMass_MeV - AlphaEff * DebyeMass_MeV;
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
			double radius_fm
			)
		{
			double X = DebyeMass_MeV * radius_fm / Constants.HbarC_MeV_fm;
			if(radius_fm == 0)
			{
				return new Complex(0, 0);
			}
			else if(X <= 10)
			{
				return new Complex(-(AlphaEff / radius_fm + SigmaOverDebyeMass_fm) * Math.Exp(-X),
					-AlphaOverDebyeMass_fm
					* (1.0 - 0.5 * (AdvancedMath.IntegralEi(X) * Math.Exp(-X) * (1.0 / X + 1.0)
					+ AdvancedMath.IntegralE(1, X) * Math.Exp(X) * (1.0 / X - 1.0))));
			}
			else
			{
				double X2 = X * X;
				return new Complex(-(AlphaEff / radius_fm + SigmaOverDebyeMass_fm) * Math.Exp(-X),
					-AlphaOverDebyeMass_fm
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
			double temperature_MeV,
			double debyeMass_MeV
			)
			: base(alphaSoft, 0, colorState, temperature_MeV, debyeMass_MeV)
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
			double sigma_MeV,
			ColorState colorState,
			double temperature_MeV,
			double debyeMass_MeV
			)
			: base(alphaSoft, sigma_MeV, colorState, temperature_MeV, debyeMass_MeV)
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
				double radius_fm
				)
		{
			double PI = Math.PI;
			double xt = TwoPiTemperature_fm * radius_fm;
			double xt2 = xt * xt;
			double xm = DebyeMass_fm * radius_fm;
			double xm2 = xm * xm;
			return new Complex(
				-SigmaOverDebyeMass_fm * Math.Exp(-xm)
				- AlphaEff / radius_fm * (1.0 - Constants.NumberQCDColors * AlphaSoft * xt2 / 36.0 / PI
				+ 3 * Constants.RiemannZetaFunctionAt3 / 4.0 / PI / PI * xt * xm2
				- Constants.RiemannZetaFunctionAt3 * Constants.NumberQCDColors * AlphaSoft * xt * xt2 / 12.0 / PI / PI / PI
				- xm * xm2 / 6.0),
				-AlphaOverDebyeMass_fm * (Constants.NumberQCDColors * Constants.NumberQCDColors * AlphaSoft * AlphaSoft / 6.0
				+ xm2 / 6.0 * (2 * Math.Log(Temperature_MeV / DebyeMass_MeV) + 1
				+ 4 * Math.Log(2) + 2 * Constants.RiemannZetaFunctionDerivativeAt2 / Constants.RiemannZetaFunctionAt2 - 2 * Constants.EulerMascheroniConstant)
				- Math.Log(2) * Constants.NumberQCDColors * AlphaSoft / 9.0 / PI * xt2));
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected double TwoPiTemperature_fm;

		protected double TwoPiTemperature_fm2;

		protected double DebyeMass_fm;

		protected double DebyeMass_fm2;

		protected override void SetHelperVariables()
		{
			base.SetHelperVariables();

			TwoPiTemperature_fm = 2 * Math.PI * Temperature_MeV / Constants.HbarC_MeV_fm;
			TwoPiTemperature_fm2 = TwoPiTemperature_fm * TwoPiTemperature_fm;

			DebyeMass_fm = DebyeMass_MeV / Constants.HbarC_MeV_fm;
			DebyeMass_fm2 = DebyeMass_fm * DebyeMass_fm;
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
			double temperature_MeV,
			double debyeMass_MeV
			)
			: base(alphaSoft, 0, colorState, temperature_MeV, debyeMass_MeV)
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
			double sigma_MeV,
			ColorState colorState,
			double debyeMass_MeV
			)
			: base(alphaSoft, sigma_MeV, colorState, 0, debyeMass_MeV)
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
				return SigmaEff_MeV / DebyeMass_MeV - AlphaEff * DebyeMass_MeV;
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
			double radius_fm
			)
		{
			return new Complex(-(AlphaEff / radius_fm + SigmaOverDebyeMass_fm)
				* Math.Exp(-DebyeMass_MeV * radius_fm / Constants.HbarC_MeV_fm), 0);
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
			double debyeMass_MeV
			)
			: base(alphaSoft, 0, colorState, debyeMass_MeV)
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
			double sigma_MeV,
			ColorState colorState
			)
			: base(alphaSoft, sigma_MeV, colorState)
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
			double radius_fm
			)
		{
			return new Complex(-AlphaEff / radius_fm + SigmaEff_fm * radius_fm, 0);
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
			double radius_fm
			)
		{
			return new Complex(-AlphaEff / radius_fm, 0);
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
			double sigma_MeV,
			ColorState colorState,
			SpinState spinState,
			double spinCouplingRange_fm,
			double spinCouplingStrength_MeV
			)
			: base(alphaSoft, sigma_MeV, colorState)
		{
			SpinState = spinState;
			SpinCouplingRange = spinCouplingRange_fm;
			SpinCouplingStrength = spinCouplingStrength_MeV;

			AssertValidMembers();
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
			double radius_fm
			)
		{
			return new Complex(-AlphaEff / radius_fm + SigmaEff_fm * radius_fm
				+ SpinFactor * Math.Exp(-radius_fm / SpinCouplingRange), 0);
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

		private void AssertValidMembers()
		{
			if(SpinCouplingRange < 0)
			{
				throw new Exception("SpinCouplingRange < 0.");
			}
			if(SpinCouplingStrength < 0)
			{
				throw new Exception("SpinCouplingStrength < 0.");
			}
		}

		protected override void SetHelperVariables()
		{
			base.SetHelperVariables();

			TotalSpin = SpinState == SpinState.Triplet ? 1 : 0;
			SpinFactor = TotalSpin * (TotalSpin + 1) - 2 * QuarkSpin * (QuarkSpin + 1);
			SpinFactor *= SpinCouplingStrength;
		}
	}
}
