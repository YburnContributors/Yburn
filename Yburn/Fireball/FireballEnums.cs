namespace Yburn.Fireball
{
	public enum BottomiumState
	{
		Y1S,
		x1P,
		Y2S,
		x2P,
		Y3S,
		x3P
	};

	public enum DecayWidthType
	{
		None,
		GammaDamp,
		GammaDiss,
		GammaTot
	};

	public enum ExpansionMode
	{
		Longitudinal,
		Transverse
	};

	public enum DecayWidthEvaluationType
	{
		UnshiftedTemperature,
		MaximallyBlueshifted,
		AveragedTemperature,
		AveragedDecayWidth
	};

	public enum EMFCalculationMethod
	{
		URLimitFourierSynthesis,
		DiffusionApproximation,
		FreeSpace
	}

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
		CMS2012
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