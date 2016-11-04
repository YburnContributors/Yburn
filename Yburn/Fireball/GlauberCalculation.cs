using System;

namespace Yburn.Fireball
{
	public class GlauberCalculation
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double GetNmixPHOBOS(
			double ncoll,
			double npart
			)
		{
			return 0.5 * (1.0 - NmixPHOBOSFittingConstant) * npart
				+ NmixPHOBOSFittingConstant * ncoll;
		}

		private static readonly double NmixPHOBOSFittingConstant = 0.145;

		private static double GetNmixALICE(
			 double ncoll,
			 double npart
			 )
		{
			return NmixALICEFittingConstant * npart + (1 - NmixALICEFittingConstant) * ncoll;
		}

		private static readonly double NmixALICEFittingConstant = 0.8;

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public GlauberCalculation(
			FireballParam param
			)
		{
			Param = param.Clone();

			AssertValidMembers();

			InitXY();
			InitNucleusAB();
			InitNucleonNumberDensityFieldsAB();
			InitNucleonNumberColumnDensityFieldsAB();
			InitOverlapField();
			InitNcollField();
			InitNpartField();
			InitTemperatureScalingField();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SimpleFireballField NucleonNumberDensityFieldA
		{
			get;
			private set;
		}

		public SimpleFireballField NucleonNumberDensityFieldB
		{
			get;
			private set;
		}

		public SimpleFireballField NcollField
		{
			get;
			private set;
		}

		public double GetTotalNumberCollisions()
		{
			if(Param.AreNucleusABIdentical)
			{
				// include a factor of 4 because only a quarter has been integrated
				return 4 * Param.GridCellSizeFm.Value * Param.GridCellSizeFm.Value
					* NcollField.TrapezoidalRuleSummedValues();
			}
			else
			{
				// include a factor of 2 because only a half has been integrated
				return 2 * Param.GridCellSizeFm.Value * Param.GridCellSizeFm.Value
					* NcollField.TrapezoidalRuleSummedValues();
			}
		}

		public SimpleFireballField NpartField
		{
			get;
			private set;
		}

		public double GetTotalNumberParticipants()
		{
			if(Param.AreNucleusABIdentical)
			{
				// include a factor of 4 because only a quarter has been integrated
				return 4 * Param.GridCellSizeFm.Value * Param.GridCellSizeFm.Value
					* NpartField.TrapezoidalRuleSummedValues();
			}
			else
			{
				// include a factor of 2 because only a half has been integrated
				return 2 * Param.GridCellSizeFm.Value * Param.GridCellSizeFm.Value
					* NpartField.TrapezoidalRuleSummedValues();
			}
		}

		public SimpleFireballField NucleonNumberColumnDensityFieldA
		{
			get;
			private set;
		}

		public SimpleFireballField NucleonNumberColumnDensityFieldB
		{
			get;
			private set;
		}

		public SimpleFireballField OverlapField
		{
			get;
			private set;
		}

		public SimpleFireballField TemperatureScalingField
		{
			get;
			private set;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double InelasticppCrossSectionFm
		{
			get
			{
				switch(Param.ProtonProtonBaseline)
				{
					// inelastic cross section for pp collisions is 64 mb = 6.4 fm^2 at 2.76 TeV
					case ProtonProtonBaseline.CMS2012:
						return 6.4;

					// inelastic cross section for pp collisions is 68 mb = 6.8 fm^2 at 5.02 TeV
					case ProtonProtonBaseline.Estimate502TeV:
						return 6.8;

					default:
						throw new Exception("Invalid Baseline.");
				}
			}
		}

		private FireballParam Param;

		private Nucleus NucleusA;

		private Nucleus NucleusB;

		private void AssertValidMembers()
		{
			if(Param.GridCellSizeFm <= 0)
			{
				throw new Exception("GridCellSize <= 0.");
			}

			if(Param.NumberGridPoints <= 0)
			{
				throw new Exception("NumberGridPoints <= 0.");
			}

			if(Param.ImpactParameterFm < 0)
			{
				throw new Exception("ImpactParameter < 0.");
			}
		}

		// x, y are in the plane perpendicular to the symmetry axis. The origin is in the middle
		// between the two center of the nuclei. The x-axis is in the plane that the beam axis spans
		// with the line connecting the two centers.
		private double[] X;

		private double[] Y;

		private void InitNucleusAB()
		{
			Nucleus.CreateNucleusPair(
				Param, out NucleusA, out NucleusB);
		}

		private void InitNucleonNumberDensityFieldsAB()
		{
			if(Param.AreNucleusABIdentical)
			{
				NucleonNumberDensityFieldA = new SimpleFireballField(
					FireballFieldType.NucleonDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusA.GetNucleonNumberDensityPerFm3(Math.Sqrt(Math.Pow(
						X[i] + 0.5 * Param.ImpactParameterFm.Value, 2) + Math.Pow(Y[j], 2))));

				NucleonNumberDensityFieldB = new SimpleFireballField(
					FireballFieldType.NucleonDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusB.GetNucleonNumberDensityPerFm3(Math.Sqrt(Math.Pow(
						X[i] - 0.5 * Param.ImpactParameterFm.Value, 2) + Math.Pow(Y[j], 2))));
			}
			else
			{
				NucleonNumberDensityFieldA = new SimpleFireballField(
					FireballFieldType.NucleonDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusA.GetNucleonNumberDensityPerFm3(Math.Sqrt(Math.Pow(
						X[i] + Param.ImpactParameterFm.Value, 2) + Math.Pow(Y[j], 2))));

				NucleonNumberDensityFieldB = new SimpleFireballField(
					FireballFieldType.NucleonDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusB.GetNucleonNumberDensityPerFm3(Math.Sqrt(Math.Pow(
						X[i], 2) + Math.Pow(Y[j], 2))));
			}
		}

		private void InitNcollField()
		{
			NcollField = new SimpleFireballField(
				FireballFieldType.Ncoll,
				Param.NumberGridPointsInX,
				Param.NumberGridPointsInY,
				(i, j) => InelasticppCrossSectionFm * OverlapField[i, j]);
		}

		private void InitNpartField()
		{
			NpartField = new SimpleFireballField(
				FireballFieldType.Npart,
				Param.NumberGridPointsInX,
				Param.NumberGridPointsInY,
				(i, j) =>
					NucleonNumberColumnDensityFieldA[i, j] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSectionFm * NucleonNumberColumnDensityFieldB[i, j]
						/ Param.NucleonNumberB.Value, Param.NucleonNumberB.Value))
					+ NucleonNumberColumnDensityFieldB[i, j] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSectionFm * NucleonNumberColumnDensityFieldA[i, j]
						/ Param.NucleonNumberA.Value, Param.NucleonNumberA.Value)));
		}

		private void InitOverlapField()
		{
			OverlapField = new SimpleFireballField(
				FireballFieldType.Overlap,
				Param.NumberGridPointsInX,
				Param.NumberGridPointsInY,
				(i, j) => NucleonNumberColumnDensityFieldA[i, j] * NucleonNumberColumnDensityFieldB[i, j]);
		}

		private void InitXY()
		{
			X = Param.GenerateDiscreteXAxis().ToArray();
			Y = Param.GenerateDiscreteYAxis().ToArray();
		}

		private void InitNucleonNumberColumnDensityFieldsAB()
		{
			if(Param.AreNucleusABIdentical)
			{
				NucleonNumberColumnDensityFieldA = new SimpleFireballField(
					FireballFieldType.ColumnDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusA.GetNucleonNumberColumnDensityPerFm3(
						X[i] + 0.5 * Param.ImpactParameterFm.Value, Y[j]));

				NucleonNumberColumnDensityFieldB = new SimpleFireballField(
					FireballFieldType.ColumnDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusB.GetNucleonNumberColumnDensityPerFm3(
						X[i] - 0.5 * Param.ImpactParameterFm.Value, Y[j]));
			}
			else
			{
				NucleonNumberColumnDensityFieldA = new SimpleFireballField(
					FireballFieldType.ColumnDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusA.GetNucleonNumberColumnDensityPerFm3(
						X[i] + Param.ImpactParameterFm.Value, Y[j]));

				NucleonNumberColumnDensityFieldB = new SimpleFireballField(
					FireballFieldType.ColumnDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusB.GetNucleonNumberColumnDensityPerFm3(
						X[i], Y[j]));
			}
		}

		private void InitTemperatureScalingField()
		{
			double norm = 1.0 / GetTemperatureScalingFieldNormalization();

			TemperatureScalingField = new SimpleFireballField(
				FireballFieldType.TemperatureScalingField,
				Param.NumberGridPointsInX,
				Param.NumberGridPointsInY,
				(i, j) =>
					{
						switch(Param.TemperatureProfile)
						{
							case TemperatureProfile.Ncoll:
								return norm * NcollField[i, j];

							case TemperatureProfile.Npart:
								return norm * NpartField[i, j];

							case TemperatureProfile.Ncoll13:
								return norm * Math.Pow(NcollField[i, j], 1 / 3.0);

							case TemperatureProfile.Npart13:
								return norm * Math.Pow(NpartField[i, j], 1 / 3.0);

							case TemperatureProfile.NmixPHOBOS13:
								return norm * Math.Pow(GetNmixPHOBOS(
									NcollField[i, j], NpartField[i, j]), 1 / 3.0);

							case TemperatureProfile.NmixALICE13:
								return norm * Math.Pow(GetNmixALICE(
									NcollField[i, j], NpartField[i, j]), 1 / 3.0);

							default:
								throw new Exception("Invalid Profile.");
						}
					});
		}

		private double GetTemperatureScalingFieldNormalization()
		{
			double columnA = NucleusA.GetNucleonNumberColumnDensityPerFm3(0, 0);
			double columnB = NucleusB.GetNucleonNumberColumnDensityPerFm3(0, 0);

			double ncoll = InelasticppCrossSectionFm * columnA * columnB;
			double npart =
				columnA * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSectionFm * columnB / Param.NucleonNumberB.Value,
					Param.NucleonNumberB.Value))
				+ columnB * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSectionFm * columnA / Param.NucleonNumberA.Value,
					Param.NucleonNumberA.Value));

			switch(Param.TemperatureProfile)
			{
				case TemperatureProfile.Ncoll:
					return ncoll;

				case TemperatureProfile.Npart:
					return npart;

				case TemperatureProfile.Ncoll13:
					return Math.Pow(ncoll, 1 / 3.0);

				case TemperatureProfile.Npart13:
					return Math.Pow(npart, 1 / 3.0);

				case TemperatureProfile.NmixPHOBOS13:
					return Math.Pow(GetNmixPHOBOS(ncoll, npart), 1 / 3.0);

				case TemperatureProfile.NmixALICE13:
					return Math.Pow(GetNmixALICE(ncoll, npart), 1 / 3.0);

				default:
					throw new Exception("Invalid Profile.");
			}
		}
	}
}