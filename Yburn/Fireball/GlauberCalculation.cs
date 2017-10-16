using System;
using System.Collections.ObjectModel;

namespace Yburn.Fireball
{
	public class GlauberCalculation
	{
		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		// COMPETE Collaboration (2002)
		private static double GetTotalppCrossSection_fm2(
			double centerOfMassEnergy_TeV
			)
		{
			double s_GeV = 1e6 * centerOfMassEnergy_TeV * centerOfMassEnergy_TeV;
			double sigma_mb = 42.6 * Math.Pow(s_GeV, -0.46) - 33.4 * Math.Pow(s_GeV, -0.545) + 35.5
			   + 0.307 * Math.Pow(Math.Log(s_GeV / 29.1), 2);

			return 0.1 * sigma_mb;
		}

		// TOTEM Collaboration (2013)
		private static double GetElasticppCrossSection_fm2(
			double centerOfMassEnergy_TeV
			)
		{
			double s_GeV = 1e6 * centerOfMassEnergy_TeV * centerOfMassEnergy_TeV;
			double sigma_mb = 11.7 - 1.59 * Math.Log(s_GeV) + 0.134 * Math.Pow(Math.Log(s_GeV), 2);

			return 0.1 * sigma_mb;
		}

		private static double GetInelasticppCrossSection_fm2(
			double centerOfMassEnergy_TeV
			)
		{
			return GetTotalppCrossSection_fm2(centerOfMassEnergy_TeV)
				- GetElasticppCrossSection_fm2(centerOfMassEnergy_TeV);
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

			InitCoordinateSystem();
			InitInelasticppCrossSection();
			InitNucleusAB();
			InitNucleonNumberDensityFieldsAB();
			InitNucleonNumberColumnDensityFieldsAB();
			InitOverlapField();
			InitNumberCollisionsField();
			InitNumberParticipantsField();
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

		public SimpleFireballField NumberCollisionsField
		{
			get;
			private set;
		}

		public double TotalNumberCollisions
		{
			get
			{
				return NumberCollisionsField.IntegrateValues();
			}
		}

		public SimpleFireballField NumberParticipantsField
		{
			get;
			private set;
		}

		public double TotalNumberParticipants
		{
			get
			{
				return NumberParticipantsField.IntegrateValues();
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

		private CoordinateSystem CoordinateSystem;

		private double InelasticppCrossSection_fm2;

		private Nucleus NucleusA;

		private Nucleus NucleusB;

		private ReadOnlyCollection<double> XAxis
		{
			get
			{
				return CoordinateSystem.XAxis;
			}
		}

		private ReadOnlyCollection<double> YAxis
		{
			get
			{
				return CoordinateSystem.YAxis;
			}
		}

		private void AssertValidMembers()
		{
			if(Param.CenterOfMassEnergy_TeV <= 0)
			{
				throw new Exception("CenterOfMassEnergy <= 0.");
			}

			if(Param.GridCellSize_fm <= 0)
			{
				throw new Exception("GridCellSize <= 0.");
			}

			if(Param.GridRadius_fm <= 0)
			{
				throw new Exception("GridRadius <= 0.");
			}

			if(Param.ImpactParameter_fm < 0)
			{
				throw new Exception("ImpactParameter < 0.");
			}
		}

		private void InitCoordinateSystem()
		{
			CoordinateSystem = new CoordinateSystem(Param);
		}

		private void InitInelasticppCrossSection()
		{
			InelasticppCrossSection_fm2
				= GetInelasticppCrossSection_fm2(Param.CenterOfMassEnergy_TeV);
		}

		private void InitNucleusAB()
		{
			Nucleus.CreateNucleusPair(Param, out NucleusA, out NucleusB);
		}

		private void InitNucleonNumberDensityFieldsAB()
		{
			NucleonNumberDensityFieldA = new SimpleFireballField(
				FireballFieldType.NucleonDensityA,
				CoordinateSystem,
				(x, y) => NucleusA.GetNucleonNumberDensity_per_fm3(Math.Sqrt(Math.Pow(
					XAxis[x] - Param.NucleusPositionA, 2) + Math.Pow(YAxis[y], 2))));

			NucleonNumberDensityFieldB = new SimpleFireballField(
				FireballFieldType.NucleonDensityB,
				CoordinateSystem,
				(x, y) => NucleusB.GetNucleonNumberDensity_per_fm3(Math.Sqrt(Math.Pow(
					XAxis[x] - Param.NucleusPositionB, 2) + Math.Pow(YAxis[y], 2))));
		}

		private void InitNumberCollisionsField()
		{
			NumberCollisionsField = new SimpleFireballField(
				FireballFieldType.NumberCollisions,
				CoordinateSystem,
				(x, y) => InelasticppCrossSection_fm2 * OverlapField[x, y]);
		}

		private void InitNumberParticipantsField()
		{
			NumberParticipantsField = new SimpleFireballField(
				FireballFieldType.NumberParticipants,
				CoordinateSystem,
				(x, y) =>
					NucleonNumberColumnDensityFieldA[x, y] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSection_fm2 * NucleonNumberColumnDensityFieldB[x, y]
						/ Param.NucleonNumberB, Param.NucleonNumberB))
					+ NucleonNumberColumnDensityFieldB[x, y] * (1.0 - Math.Pow(
						1.0 - InelasticppCrossSection_fm2 * NucleonNumberColumnDensityFieldA[x, y]
						/ Param.NucleonNumberA, Param.NucleonNumberA)));
		}

		private void InitOverlapField()
		{
			OverlapField = new SimpleFireballField(
				FireballFieldType.Overlap,
				CoordinateSystem,
				(x, y) => NucleonNumberColumnDensityFieldA[x, y] * NucleonNumberColumnDensityFieldB[x, y]);
		}

		private void InitNucleonNumberColumnDensityFieldsAB()
		{
			NucleonNumberColumnDensityFieldA = new SimpleFireballField(
				FireballFieldType.ColumnDensityA,
				CoordinateSystem,
				(x, y) => NucleusA.GetNucleonNumberColumnDensity_per_fm3(
					XAxis[x] - Param.NucleusPositionA, YAxis[y]));

			NucleonNumberColumnDensityFieldB = new SimpleFireballField(
				FireballFieldType.ColumnDensityB,
				CoordinateSystem,
				(x, y) => NucleusB.GetNucleonNumberColumnDensity_per_fm3(
					XAxis[x] - Param.NucleusPositionB, YAxis[y]));
		}

		private void InitTemperatureScalingField()
		{
			double norm = 1.0 / GetTemperatureScalingFieldNormalization();

			TemperatureScalingField = new SimpleFireballField(
				FireballFieldType.TemperatureScaling,
				CoordinateSystem,
				(x, y) =>
					{
						switch(Param.TemperatureProfile)
						{
							case TemperatureProfile.Ncoll:
								return norm * NumberCollisionsField[x, y];

							case TemperatureProfile.Npart:
								return norm * NumberParticipantsField[x, y];

							case TemperatureProfile.Ncoll13:
								return norm * Math.Pow(NumberCollisionsField[x, y], 1 / 3.0);

							case TemperatureProfile.Npart13:
								return norm * Math.Pow(NumberParticipantsField[x, y], 1 / 3.0);

							case TemperatureProfile.NmixPHOBOS13:
								return norm * Math.Pow(GetNmixPHOBOS(
									NumberCollisionsField[x, y], NumberParticipantsField[x, y]), 1 / 3.0);

							case TemperatureProfile.NmixALICE13:
								return norm * Math.Pow(GetNmixALICE(
									NumberCollisionsField[x, y], NumberParticipantsField[x, y]), 1 / 3.0);

							default:
								throw new Exception("Invalid Profile.");
						}
					});
		}

		private double GetTemperatureScalingFieldNormalization()
		{
			double columnA = NucleusA.GetNucleonNumberColumnDensity_per_fm3(0, 0);
			double columnB = NucleusB.GetNucleonNumberColumnDensity_per_fm3(0, 0);

			double ncoll = InelasticppCrossSection_fm2 * columnA * columnB;
			double npart
				= columnA * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSection_fm2 * columnB / Param.NucleonNumberB,
					Param.NucleonNumberB))
				+ columnB * (1.0 - Math.Pow(
					1.0 - InelasticppCrossSection_fm2 * columnA / Param.NucleonNumberA,
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
