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
			SetMembers(param);
			AssertValidMembers();

			InitXY();
			InitWoodsSaxonPotentials();
			InitNucleonDensityAB();
			InitColumnDensityAB();
			InitOverlap();
			InitNcoll();
			InitNpart();
			InitTemperatureScalingField();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public SimpleFireballField NucleonDensityA
		{
			get;
			private set;
		}

		public SimpleFireballField NucleonDensityB
		{
			get;
			private set;
		}

		public SimpleFireballField Ncoll
		{
			get;
			private set;
		}

		public double GetTotalNumberCollisions()
		{
			// include a fcator of 4 because only a quarter has been integrated
			return 4 * GridCellSize * GridCellSize * Ncoll.TrapezoidalRuleSummedValues();
		}

		public SimpleFireballField Npart
		{
			get;
			private set;
		}

		public double GetTotalNumberParticipants()
		{
			// include a fcator of 4 because only a quarter has been integrated
			return 4 * GridCellSize * GridCellSize * Npart.TrapezoidalRuleSummedValues();
		}

		public SimpleFireballField ColumnDensityA
		{
			get;
			private set;
		}

		public SimpleFireballField ColumnDensityB
		{
			get;
			private set;
		}

		public SimpleFireballField Overlap
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
			return 0.5 * (1.0 - NmixPHOBOSFittingConstant) * npart + NmixPHOBOSFittingConstant * ncoll;
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

		// inelastic cross section for pp collisions is 64 mb = 6.4 fm^-2 at 2.76 TeV
		private static readonly double InelasticppCrossSectionFm = 6.4;

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private int NumberGridCells;

		private double ImpactParameter;

		private double GridCellSize;

		private WoodsSaxonPotential WSPotentialA;

		private WoodsSaxonPotential WSPotentialB;

		private int NucleonNumberB;

		private int NucleonNumberA;

		private double DiffusenessB;

		private double DiffusenessA;

		private double NuclearRadiusB;

		private double NuclearRadiusA;

		private TemperatureProfile TemperatureProfile;

		private void SetMembers(
			FireballParam param
			)
		{
			NumberGridCells = param.NumberGridCells;
			GridCellSize = param.GridCellSizeFm;
			ImpactParameter = param.ImpactParamFm;
			NuclearRadiusA = param.NuclearRadiusFmA;
			NuclearRadiusB = param.NuclearRadiusFmB;
			DiffusenessA = param.DiffusenessFmA;
			DiffusenessB = param.DiffusenessFmB;
			NucleonNumberA = param.NucleonNumberA;
			NucleonNumberB = param.NucleonNumberB;
			TemperatureProfile = param.TemperatureProfile;
		}

		private void AssertValidMembers()
		{
			if(GridCellSize <= 0)
			{
				throw new Exception("GridCellSize <= 0.");
			}

			if(NumberGridCells <= 0)
			{
				throw new Exception("NumberGridCells <= 0.");
			}

			if(ImpactParameter < 0)
			{
				throw new Exception("ImpactParam < 0.");
			}
		}

		// x,y are in the plane perpendicular to the symmetry axis. origin is in the middle between
		// the two center of the nuclei. the x-axis is in the plane that the beam axis spans with the line connecting the two centers
		private double[] X;

		private double[] Y;

		private void InitWoodsSaxonPotentials()
		{
			WSPotentialA = new WoodsSaxonPotential(NuclearRadiusA, DiffusenessA, NucleonNumberA);
			WSPotentialA.NormalizeTo(NucleonNumberA);
			WSPotentialB = new WoodsSaxonPotential(NuclearRadiusB, DiffusenessB, NucleonNumberB);
			WSPotentialB.NormalizeTo(NucleonNumberB);
		}

		private void InitNucleonDensityAB()
		{
			NucleonDensityA = new SimpleFireballField(FireballFieldType.NucleonDensityA,
				NumberGridCells, NumberGridCells, (i, j) =>
				{
					return WSPotentialA.Value(Math.Sqrt(
						Math.Pow(X[i] + 0.5 * ImpactParameter, 2) + Math.Pow(Y[j], 2)));
				});

			NucleonDensityB = new SimpleFireballField(FireballFieldType.NucleonDensityB,
				NumberGridCells, NumberGridCells, (i, j) =>
				{
					return WSPotentialB.Value(Math.Sqrt(
						Math.Pow(X[i] - 0.5 * ImpactParameter, 2) + Math.Pow(Y[j], 2)));
				});
		}

		private void InitNcoll()
		{
			Ncoll = new SimpleFireballField(FireballFieldType.Ncoll, NumberGridCells, NumberGridCells,
				(i, j) =>
				{
					return InelasticppCrossSectionFm * Overlap.Values[i, j];
				});
		}

		private void InitNpart()
		{
			Npart = new SimpleFireballField(FireballFieldType.Npart, NumberGridCells, NumberGridCells,
				(i, j) =>
				{
					return ColumnDensityA.Values[i, j]
						* (1.0 - Math.Pow(1.0 - InelasticppCrossSectionFm * ColumnDensityB.Values[i, j]
								/ NucleonNumberB, NucleonNumberB))
						+ ColumnDensityB.Values[i, j]
						* (1.0 - Math.Pow(1.0 - InelasticppCrossSectionFm * ColumnDensityA.Values[i, j]
								/ NucleonNumberA, NucleonNumberA));
				});
		}

		private void InitOverlap()
		{
			Overlap = new SimpleFireballField(FireballFieldType.Overlap,
				NumberGridCells, NumberGridCells, (i, j) =>
				{
					return ColumnDensityA.Values[i, j] * ColumnDensityB.Values[i, j];
				});
		}

		private void InitXY()
		{
			X = new double[NumberGridCells];
			Y = new double[NumberGridCells];

			for(int j = 0; j < NumberGridCells; j++)
			{
				X[j] = Y[j] = GridCellSize * j;
			}
		}

		private void InitColumnDensityAB()
		{
			ColumnDensityA = new SimpleFireballField(FireballFieldType.ColumnDensityA,
				NumberGridCells, NumberGridCells, (i, j) =>
				{
					return WSPotentialA.GetColumnDensity(X[i] + 0.5 * ImpactParameter, Y[j]);
				});

			ColumnDensityB = new SimpleFireballField(FireballFieldType.ColumnDensityB,
				NumberGridCells, NumberGridCells, (i, j) =>
				{
					return WSPotentialB.GetColumnDensity(X[i] - 0.5 * ImpactParameter, Y[j]);
				});
		}

		private void InitTemperatureScalingField()
		{
			double norm = 1.0 / GetTemperatureScalingFieldNormalization();
			TemperatureScalingField = new SimpleFireballField(
				FireballFieldType.TemperatureScalingField, NumberGridCells, NumberGridCells, (i, j) =>
			{
				switch(TemperatureProfile)
				{
					case TemperatureProfile.Ncoll:
						return norm * Ncoll.Values[i, j];

					case TemperatureProfile.Npart:
						return norm * Npart.Values[i, j];

					case TemperatureProfile.Ncoll13:
						return norm * Math.Pow(Ncoll.Values[i, j], 1 / 3.0);

					case TemperatureProfile.Npart13:
						return norm * Math.Pow(Npart.Values[i, j], 1 / 3.0);

					case TemperatureProfile.NmixPHOBOS13:
						return norm * Math.Pow(GetNmixPHOBOS(Ncoll.Values[i, j], Npart.Values[i, j]), 1 / 3.0);

					case TemperatureProfile.NmixALICE13:
						return norm * Math.Pow(GetNmixALICE(Ncoll.Values[i, j], Npart.Values[i, j]), 1 / 3.0);

					default:
						throw new Exception("Invalid Profile.");
				}
			});
		}

		private double GetTemperatureScalingFieldNormalization()
		{
			double columnA = WSPotentialA.GetColumnDensity(0, 0);
			double columnB = WSPotentialB.GetColumnDensity(0, 0);

			double ncoll = InelasticppCrossSectionFm * columnA * columnB;
			double npart = columnA * (1.0 - Math.Pow(1.0 - InelasticppCrossSectionFm * columnB
				/ NucleonNumberB, NucleonNumberB))
				+ columnB * (1.0 - Math.Pow(1.0 - InelasticppCrossSectionFm * columnA
				/ NucleonNumberA, NucleonNumberA));

			switch(TemperatureProfile)
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