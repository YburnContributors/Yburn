namespace Yburn.Fireball
{
	public enum BottomiumState
	{
		//Sorted by rest mass from light to heavy.
		Y1S = 0,
		x1P = 1,
		Y2S = 2,
		x2P = 3,
		Y3S = 4,
		x3P = 5
	};

	public enum DopplerShiftEvaluationType
	{
		UnshiftedTemperature,
		MaximallyBlueshifted,
		AveragedTemperature,
		AveragedDecayWidth,
		AveragedLifeTime
	};

	public enum ElectricDipoleAlignment
	{
		Random,
		WeakenBinding,
		StrengthenBinding
	}

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
		NumberCollisions,
		NumberParticipants,
		Overlap,
		Temperature,
		TemperatureScaling,
		TemperatureNormalization,
		VelocityX,
		VelocityY,
		UnscaledSuppression,
		ElectricFieldStrength,
		MagneticFieldStrength
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
