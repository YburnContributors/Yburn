using System;

namespace Yburn.Fireball
{
	public class GlauberCalculation
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		// COMPETE Collaboration (2002)
		private static double GetTotalppCrossSectionFm2(
			double centerOfMassEnergyTeV
			)
		{
			double sGeV = 1e6 * centerOfMassEnergyTeV * centerOfMassEnergyTeV;
			double sigmamb = 42.6 * Math.Pow(sGeV, -0.46) - 33.4 * Math.Pow(sGeV, -0.545) + 35.5
			   + 0.307 * Math.Pow(Math.Log(sGeV / 29.1), 2);

			return 0.1 * sigmamb;
		}

		// TOTEM Collaboration (2013)
		private static double GetElasticppCrossSectionFm2(
			double centerOfMassEnergyTeV
			)
		{
			double sGeV = 1e6 * centerOfMassEnergyTeV * centerOfMassEnergyTeV;
			double sigmamb = 11.7 - 1.59 * Math.Log(sGeV) + 0.134 * Math.Pow(Math.Log(sGeV), 2);

			return 0.1 * sigmamb;
		}

		private static double GetInelasticppCrossSectionFm2(
			double centerOfMassEnergyTeV
			)
		{
			return GetTotalppCrossSectionFm2(centerOfMassEnergyTeV)
				- GetElasticppCrossSectionFm2(centerOfMassEnergyTeV);
		}

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
			InitInelasticppCrossSection();
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
			if(Param.AreNucleusABIdentical)
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

		private FireballParam Param;

		private double InelasticppCrossSectionFm2;

		private Nucleus NucleusA;

		private Nucleus NucleusB;

		private void AssertValidMembers()
		{
			if(Param.CenterOfMassEnergyTeV <= 0)
			{
				throw new Exception("CenterOfMassEnergy <= 0.");
			}

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

		private void InitInelasticppCrossSection()
		{
			InelasticppCrossSectionFm2
				= GetInelasticppCrossSectionFm2(Param.CenterOfMassEnergyTeV);
		}

		private void InitNucleusAB()
		{
			Nucleus.CreateNucleusPair(Param, out NucleusA, out NucleusB);
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
						X[i] + 0.5 * Param.ImpactParameterFm, 2) + Math.Pow(Y[j], 2))));

				NucleonNumberDensityFieldB = new SimpleFireballField(
					FireballFieldType.NucleonDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusB.GetNucleonNumberDensityPerFm3(Math.Sqrt(Math.Pow(
						X[i] - 0.5 * Param.ImpactParameterFm, 2) + Math.Pow(Y[j], 2))));
			}
			else
			{
				NucleonNumberDensityFieldA = new SimpleFireballField(
					FireballFieldType.NucleonDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusA.GetNucleonNumberDensityPerFm3(Math.Sqrt(Math.Pow(
						X[i] + Param.ImpactParameterFm, 2) + Math.Pow(Y[j], 2))));

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
				(i, j) => InelasticppCrossSectionFm2 * OverlapField[i, j]);
		}

		private void InitNpartField()
		{
			NpartField = new SimpleFireballField(
				FireballFieldType.Npart,
				Param.NumberGridPointsInX,
				Param.NumberGridPointsInY,
				(i, j) =>
					NucleonNumberColumnDensityFieldA[i, j] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSectionFm2 * NucleonNumberColumnDensityFieldB[i, j]
						/ Param.NucleonNumberB, Param.NucleonNumberB))
					+ NucleonNumberColumnDensityFieldB[i, j] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSectionFm2 * NucleonNumberColumnDensityFieldA[i, j]
						/ Param.NucleonNumberA, Param.NucleonNumberA)));
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
						X[i] + 0.5 * Param.ImpactParameterFm, Y[j]));

				NucleonNumberColumnDensityFieldB = new SimpleFireballField(
					FireballFieldType.ColumnDensityB,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusB.GetNucleonNumberColumnDensityPerFm3(
						X[i] - 0.5 * Param.ImpactParameterFm, Y[j]));
			}
			else
			{
				NucleonNumberColumnDensityFieldA = new SimpleFireballField(
					FireballFieldType.ColumnDensityA,
					Param.NumberGridPointsInX,
					Param.NumberGridPointsInY,
					(i, j) => NucleusA.GetNucleonNumberColumnDensityPerFm3(
						X[i] + Param.ImpactParameterFm, Y[j]));

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

			double ncoll = InelasticppCrossSectionFm2 * columnA * columnB;
			double npart
				= columnA * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSectionFm2 * columnB / Param.NucleonNumberB,
					Param.NucleonNumberB))
				+ columnB * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSectionFm2 * columnA / Param.NucleonNumberA,
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