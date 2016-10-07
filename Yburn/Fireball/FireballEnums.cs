using System.ComponentModel;

namespace Yburn.Fireball
{
	public enum BottomiumState
	{
		//Sorted by mass from light to heavy.
		Y1S = 0,
		x1P = 1,
		Y2S = 2,
		x2P = 3,
		Y3S = 4,
		x3P = 5
	};

	public enum DecayWidthType
	{
		None,
		GammaDamp,
		GammaDiss,
		GammaTot
	};

	public enum DecayWidthEvaluationType
	{
		UnshiftedTemperature,
		MaximallyBlueshifted,
		AveragedTemperature,
		AveragedDecayWidth
	};

	public enum ExpansionMode
	{
		Longitudinal,
		Transverse
	};

	public enum FireballFieldType
	{
		ColumnDensityA,
		ColumnDensityB,
		DampingFactor,
		DecayWidth,
		NucleonDensityA,
		NucleonDensityB,
		Ncoll,
		Npart,
		Overlap,
		Temperature,
		TemperatureScalingField,
		Tnorm,
		VX,
		VY,
		UnscaledSuppression,
	};

	public enum ProtonProtonBaseline
	{
		[Description("pp@2.76TeV (CMS2012)")]
		CMS2012,
		[Description("pp@5.02TeV (Not yet available!)")]
		Estimate502TeV
	};

	public enum TemperatureProfile
	{
		Ncoll,
		Npart,
		Ncoll13,
		Npart13,
		NmixPHOBOS13,
		NmixALICE13
	};
}