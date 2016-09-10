using System;

namespace Yburn.Fireball
{
	public class GlauberCalculation
	{
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
			InitNucleonDensityFunctionsAB();
			InitNucleonDensityFieldsAB();
			InitColumnDensityFieldsAB();
			InitOverlapField();
			InitNcollField();
			InitNpartField();
			InitTemperatureScalingField();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SimpleFireballField NucleonDensityFieldA
		{
			get;
			private set;
		}

		public SimpleFireballField NucleonDensityFieldB
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
			if(Param.AreParticlesABIdentical)
			{
				// include a factor of 4 because only a quarter has been integrated
				return 4 * Param.GridCellSizeFm * Param.GridCellSizeFm
					* NcollField.TrapezoidalRuleSummedValues();
			}
			else
			{
				// include a factor of 2 because only a half has been integrated
				return 2 * Param.GridCellSizeFm * Param.GridCellSizeFm
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
			if(Param.AreParticlesABIdentical)
			{
				// include a factor of 4 because only a quarter has been integrated
				return 4 * Param.GridCellSizeFm * Param.GridCellSizeFm
					* NpartField.TrapezoidalRuleSummedValues();
			}
			else
			{
				// include a factor of 2 because only a half has been integrated
				return 2 * Param.GridCellSizeFm * Param.GridCellSizeFm
					* NpartField.TrapezoidalRuleSummedValues();
			}
		}

		public SimpleFireballField ColumnDensityFieldA
		{
			get;
			private set;
		}

		public SimpleFireballField ColumnDensityFieldB
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

		private NuclearDensityFunction NucleonDensityFunctionA;

		private NuclearDensityFunction NucleonDensityFunctionB;

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

		private void InitNucleonDensityFunctionsAB()
		{
			NuclearDensityFunction.CreateNucleonDensityFunctionPair(
				Param, out NucleonDensityFunctionA, out NucleonDensityFunctionB);
		}

		private void InitNucleonDensityFieldsAB()
		{
			if(Param.AreParticlesABIdentical)
			{
				NucleonDensityFieldA = new SimpleFireballField(
					FireballFieldType.NucleonDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionA.Value(Math.Sqrt(Math.Pow(
						X[i] + 0.5 * Param.ImpactParameterFm, 2) + Math.Pow(Y[j], 2))));

				NucleonDensityFieldB = new SimpleFireballField(
					FireballFieldType.NucleonDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionB.Value(Math.Sqrt(Math.Pow(
						X[i] - 0.5 * Param.ImpactParameterFm, 2) + Math.Pow(Y[j], 2))));
			}
			else
			{
				NucleonDensityFieldA = new SimpleFireballField(
					FireballFieldType.NucleonDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionA.Value(Math.Sqrt(Math.Pow(
						X[i] + Param.ImpactParameterFm, 2) + Math.Pow(Y[j], 2))));

				NucleonDensityFieldB = new SimpleFireballField(
					FireballFieldType.NucleonDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionB.Value(Math.Sqrt(Math.Pow(
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
					ColumnDensityFieldA[i, j] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSectionFm * ColumnDensityFieldB[i, j]
						/ Param.NucleonNumberB, Param.NucleonNumberB))
					+ ColumnDensityFieldB[i, j] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSectionFm * ColumnDensityFieldA[i, j]
						/ Param.NucleonNumberA, Param.NucleonNumberA)));
		}

		private void InitOverlapField()
		{
			OverlapField = new SimpleFireballField(
				FireballFieldType.Overlap,
				Param.NumberGridPointsInX,
				Param.NumberGridPointsInY,
				(i, j) => ColumnDensityFieldA[i, j] * ColumnDensityFieldB[i, j]);
		}

		private void InitXY()
		{
			X = Param.GenerateDiscreteXAxis();
			Y = Param.GenerateDiscreteYAxis();
		}

		private void InitColumnDensityFieldsAB()
		{
			if(Param.AreParticlesABIdentical)
			{
				ColumnDensityFieldA = new SimpleFireballField(
					FireballFieldType.ColumnDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionA.GetColumnDensity(
						X[i] + 0.5 * Param.ImpactParameterFm, Y[j]));

				ColumnDensityFieldB = new SimpleFireballField(
					FireballFieldType.ColumnDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionB.GetColumnDensity(
						X[i] - 0.5 * Param.ImpactParameterFm, Y[j]));
			}
			else
			{
				ColumnDensityFieldA = new SimpleFireballField(
					FireballFieldType.ColumnDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionA.GetColumnDensity(
						X[i] + Param.ImpactParameterFm, Y[j]));

				ColumnDensityFieldB = new SimpleFireballField(
					FireballFieldType.ColumnDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleonDensityFunctionB.GetColumnDensity(
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
			double columnA = NucleonDensityFunctionA.GetColumnDensity(0, 0);
			double columnB = NucleonDensityFunctionB.GetColumnDensity(0, 0);

			double ncoll = InelasticppCrossSectionFm * columnA * columnB;
			double npart =
				columnA * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSectionFm * columnB / Param.NucleonNumberB,
					Param.NucleonNumberB))
				+ columnB * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSectionFm * columnA / Param.NucleonNumberA,
					Param.NucleonNumberA));

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