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
			InitDensityPotentials();
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
			if(ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				// include a factor of 4 because only a quarter has been integrated
				return 4 * GridCellSize * GridCellSize * Ncoll.TrapezoidalRuleSummedValues();
			}
			else if(ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				// include a factor of 2 because only a half has been integrated
				return 2 * GridCellSize * GridCellSize * Ncoll.TrapezoidalRuleSummedValues();
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}
		}

		public SimpleFireballField Npart
		{
			get;
			private set;
		}

		public double GetTotalNumberParticipants()
		{
			if(ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				// include a factor of 4 because only a quarter has been integrated
				return 4 * GridCellSize * GridCellSize * Npart.TrapezoidalRuleSummedValues();
			}
			else if(ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				// include a factor of 2 because only a half has been integrated
				return 2 * GridCellSize * GridCellSize * Npart.TrapezoidalRuleSummedValues();
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}
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

		// inelastic cross section for pp collisions is 64 mb = 6.4 fm^2 at 2.76 TeV
		private static readonly double InelasticppCrossSectionFm = 6.4;

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private int NumberGridCells;

		private int NumberGridCellsInY;

		private int NumberGridCellsInX;

		private double ImpactParameter;

		private double GridCellSize;

		private WoodsSaxonPotential WSPotentialA;

		private WoodsSaxonPotential WSPotentialB;

		private GaussianDistribution GaussianDistribution;

		private int NucleonNumberB;

		private int NucleonNumberA;

		private double DiffusenessB;

		private double DiffusenessA;

		private double NuclearRadiusB;

		private double NuclearRadiusA;

		private TemperatureProfile TemperatureProfile;

		private ShapeFunction ShapeFunctionB;

		private void SetMembers(
			FireballParam param
			)
		{
			NumberGridCells = param.NumberGridCells;
			GridCellSize = param.GridCellSizeFm;
			ImpactParameter = param.ImpactParamFm;
			NuclearRadiusA = param.NuclearRadiusAFm;
			NuclearRadiusB = param.NuclearRadiusBFm;
			DiffusenessA = param.DiffusenessAFm;
			DiffusenessB = param.DiffusenessBFm;
			NucleonNumberA = param.NucleonNumberA;
			NucleonNumberB = param.NucleonNumberB;
			TemperatureProfile = param.TemperatureProfile;
			ShapeFunctionB = param.ShapeFunctionB;
			NumberGridCellsInX = param.NumberGridCellsInX;
			NumberGridCellsInY = param.NumberGridCellsInY;
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

		private void InitDensityPotentials()
		{
			if(ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				InitWoodsSaxonPotentials();
			}
			else if(ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				InitGaussianWoodsSaxonPotentials();
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}
		}

		private void InitWoodsSaxonPotentials()
		{
			WSPotentialA = new WoodsSaxonPotential(NuclearRadiusA, DiffusenessA, NucleonNumberA);
			WSPotentialA.NormalizeTo(NucleonNumberA);
			WSPotentialB = new WoodsSaxonPotential(NuclearRadiusB, DiffusenessB, NucleonNumberB);
			WSPotentialB.NormalizeTo(NucleonNumberB);
		}

		private void InitGaussianWoodsSaxonPotentials()
		{
			WSPotentialA = new WoodsSaxonPotential(NuclearRadiusA, DiffusenessA, NucleonNumberA);
			WSPotentialA.NormalizeTo(NucleonNumberA);
			GaussianDistribution = new GaussianDistribution(NuclearRadiusB, NucleonNumberB);
			GaussianDistribution.NormalizeTo(NucleonNumberB);
		}

		private void InitNucleonDensityAB()
		{
			if(ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				NucleonDensityA = new SimpleFireballField(FireballFieldType.NucleonDensityA,
					NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
					{
						return WSPotentialA.Value(Math.Sqrt(
							Math.Pow(X[i] + 0.5 * ImpactParameter, 2) + Math.Pow(Y[j], 2)));
					});

				NucleonDensityB = new SimpleFireballField(FireballFieldType.NucleonDensityB,
					NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
					{
						return WSPotentialB.Value(Math.Sqrt(
							Math.Pow(X[i] - 0.5 * ImpactParameter, 2) + Math.Pow(Y[j], 2)));
					});
			}
			else if(ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				NucleonDensityA = new SimpleFireballField(FireballFieldType.NucleonDensityA,
					NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
					{
						return WSPotentialA.Value(Math.Sqrt(
							Math.Pow(X[i] + ImpactParameter, 2) + Math.Pow(Y[j], 2)));
					});

				NucleonDensityB = new SimpleFireballField(FireballFieldType.NucleonDensityB,
					NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
					{
						return GaussianDistribution.Value(Math.Sqrt(
							Math.Pow(X[i], 2) + Math.Pow(Y[j], 2)));
					});
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}
		}

		private void InitNcoll()
		{
			Ncoll = new SimpleFireballField(FireballFieldType.Ncoll, NumberGridCellsInX, NumberGridCellsInY,
				(i, j) =>
				{
					return InelasticppCrossSectionFm * Overlap.Values[i, j];
				});
		}

		private void InitNpart()
		{
			Npart = new SimpleFireballField(FireballFieldType.Npart, NumberGridCellsInX, NumberGridCellsInY,
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
				NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
				{
					return ColumnDensityA.Values[i, j] * ColumnDensityB.Values[i, j];
				});
		}

		private void InitXY()
		{
			X = new double[NumberGridCellsInX];
			Y = new double[NumberGridCellsInY];

			if(ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				for(int j = 0; j < NumberGridCellsInX; j++)
				{
					X[j] = Y[j] = GridCellSize * j;
				}
			}
			else if(ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				for(int j = 0; j < NumberGridCellsInY; j++)
				{
					Y[j] = GridCellSize * j;
				}

				for(int j = 0; j < NumberGridCellsInX; j++)
				{
					X[j] = GridCellSize * (j + 1 - NumberGridCells);
				}
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}
		}

		private void InitColumnDensityAB()
		{
			if(ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				ColumnDensityA = new SimpleFireballField(FireballFieldType.ColumnDensityA,
					NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
					{
						return WSPotentialA.GetColumnDensity(X[i] + 0.5 * ImpactParameter, Y[j]);
					});

				ColumnDensityB = new SimpleFireballField(FireballFieldType.ColumnDensityB,
					NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
					{
						return WSPotentialB.GetColumnDensity(X[i] - 0.5 * ImpactParameter, Y[j]);
					});
			}
			else if(ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				ColumnDensityA = new SimpleFireballField(FireballFieldType.ColumnDensityA,
					 NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
					{
						return WSPotentialA.GetColumnDensity(X[i] + ImpactParameter, Y[j]);
					});

				ColumnDensityB = new SimpleFireballField(FireballFieldType.ColumnDensityB,
					NumberGridCellsInX, NumberGridCells, (i, j) =>
					{
						return GaussianDistribution.GetColumnDensity(X[i], Y[j]);
					});
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}
		}

		private void InitTemperatureScalingField()
		{
			double norm = 1.0 / GetTemperatureScalingFieldNormalization();
			TemperatureScalingField = new SimpleFireballField(
				FireballFieldType.TemperatureScalingField, NumberGridCellsInX, NumberGridCellsInY, (i, j) =>
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
			double columnA;
			double columnB;
			if(ShapeFunctionB == ShapeFunction.WoodsSaxonPotential)
			{
				columnA = WSPotentialA.GetColumnDensity(0, 0);
				columnB = WSPotentialB.GetColumnDensity(0, 0);
			}
			else if(ShapeFunctionB == ShapeFunction.GaussianDistribution)
			{
				columnA = WSPotentialA.GetColumnDensity(0, 0);
				columnB = GaussianDistribution.GetColumnDensity(0, 0);
			}
			else
			{
				throw new Exception("Invalid ShapeFunctionB.");
			}

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