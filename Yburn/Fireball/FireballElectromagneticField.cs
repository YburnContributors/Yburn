using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yburn.Fireball
{
	public class FireballElectromagneticField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballElectromagneticField(
			FireballParam param
			)
		{
			PointChargeEMF = PointChargeElectromagneticField.Create(param);
			InitializeProtonDistributions(param);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public WoodsSaxonPotential ProtonDistributionNucleonA
		{
			get; private set;
		}

		public WoodsSaxonPotential ProtonDistributionNucleonB
		{
			get; private set;
		}

		public EuclideanVector3D CalculateMagneticField(
			double time,
			EuclideanVector3D position,
			double nucleiVelocity,
			double impactParameter
			)
		{
			double lorentzFactor = CalculateLorentzFactor(nucleiVelocity);
			EuclideanVector2D nucleusPosition = new EuclideanVector2D(impactParameter / 2, 0);
			EuclideanVector2D positionInReactionPlane = new EuclideanVector2D(position.X, position.Y);

			// Nucleus A is located at negative x and moves in positive z direction
			EuclideanVector3D fieldNucleusA = CalculateSingleNucleusMagneticField(
				time - position.Z / nucleiVelocity,
				positionInReactionPlane + nucleusPosition,
				lorentzFactor,
				ProtonDistributionNucleonA);

			// Nucleus B is located at positive x and moves in negative z direction
			EuclideanVector3D fieldNucleusB = CalculateSingleNucleusMagneticField(
				time + position.Z / nucleiVelocity,
				positionInReactionPlane - nucleusPosition,
				lorentzFactor,
				ProtonDistributionNucleonB);

			return fieldNucleusA + fieldNucleusB;
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticField(
			double effectiveTime,
			EuclideanVector2D positionInReactionPlane,
			double lorentzFactor,
			WoodsSaxonPotential protonDistribution
			)
		{
			return CalculateSingleNucleusMagneticField_Cartesian(
				effectiveTime,
				positionInReactionPlane,
				lorentzFactor,
				protonDistribution);
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticField_Cartesian(
			double effectiveTime,
			EuclideanVector2D positionInReactionPlane,
			double lorentzFactor,
			WoodsSaxonPotential protonDistribution
			)
		{
			double h = 0.2;
			int steps = 1;
			while(protonDistribution.Value(h * steps) >= 1e-12)
			{
				steps++;
			}

			EuclideanVector3D integral = new EuclideanVector3D(0, 0, 0);

			for(int i = 1 - steps; i <= steps - 1; i++)
			{
				for(int j = 1 - steps; j <= steps - 1; j++)
				{
					integral += protonDistribution.GetColumnDensity(i * h, j * h)
						* PointChargeEMF.CalculatePointChargeMagneticField(
							effectiveTime,
							positionInReactionPlane - new EuclideanVector2D(i * h, j * h),
							lorentzFactor);
				}
			}

			return h * h * integral;
		}

		public EuclideanVector3D CalculateSingleNucleusMagneticField_Polar(
			double effectiveTime,
			EuclideanVector2D positionInReactionPlane,
			double lorentzFactor,
			WoodsSaxonPotential protonDistribution
			)
		{
			double dr = 0.1;
			int radialSteps = 1;
			while(protonDistribution.Value(dr * radialSteps) >= 1e-12)
			{
				radialSteps++;
			}

			int azimutalSteps = 100;
			double dphi = 2 * Math.PI / azimutalSteps;

			EuclideanVector3D integral = new EuclideanVector3D(0, 0, 0);

			// first and last step of radial quadrature vanish
			for(int radialIndex = 1; radialIndex < radialSteps; radialIndex++)
			{
				// first and last step of azimutal quadrature identical
				for(int azimutalIndex = 0; azimutalIndex < azimutalSteps; azimutalIndex++)
				{
					double r = radialIndex * dr;
					double phi = azimutalIndex * dphi;

					integral += r * protonDistribution.GetColumnDensity(r, 0)
						* PointChargeEMF.CalculatePointChargeMagneticField(
							effectiveTime,
							positionInReactionPlane
								- EuclideanVector2D.CreateFromPolarCoordinates(r, phi),
							lorentzFactor);
				}
			}

			return dr * dphi * integral;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private PointChargeElectromagneticField PointChargeEMF;

		private void InitializeProtonDistributions(FireballParam param)
		{
			ProtonDistributionNucleonA = new WoodsSaxonPotential(
				param.NuclearRadiusAFm, param.DiffusenessAFm, param.NucleonNumberA);
			ProtonDistributionNucleonA.NormalizeTo(param.ProtonNumberA);

			ProtonDistributionNucleonB = new WoodsSaxonPotential(
				param.NuclearRadiusBFm, param.DiffusenessBFm, param.NucleonNumberB);
			ProtonDistributionNucleonB.NormalizeTo(param.ProtonNumberB);
		}

		private double CalculateLorentzFactor(
			double velocity
			)
		{
			return 1 / Math.Sqrt(1 - velocity * velocity);
		}
	}
}
