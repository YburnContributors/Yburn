﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Threading;
using Yburn.Tests.Util;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class FireballElectromagneticFieldTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		[TestMethod]
		public void CalculateSingleNucleusMagneticField()
		{
			EuclideanVector3D[] fieldValues = CalculateSingleNucleusMagneticFieldValues();
			AssertCorrectSingleNucleusMagneticFieldValues(fieldValues);
		}

		[TestMethod]
		public void CalculateMagneticField()
		{
			EuclideanVector3D[] fieldValues = CalculateMagneticFieldValues();
			AssertCorrectMagneticFieldValues(fieldValues);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly double EffectiveTime = 0.4;

		private static readonly EuclideanVector3D[] Positions =
			new EuclideanVector3D[] {
				new EuclideanVector3D(0.0, 0.0, 0.0),
				new EuclideanVector3D(1.0, 0.0, 0.0),
				new EuclideanVector3D(1.0, 2.0, 0.0),
				new EuclideanVector3D(1.0, 2.0, 3.0) };

		private static readonly EuclideanVector2D[] PositionsInReactionPlane =
			new EuclideanVector2D[] {
				new EuclideanVector2D(0.0, 0.0),
				new EuclideanVector2D(1.0, 0.0),
				new EuclideanVector2D(1.0, 2.0) };

		private static readonly double Time = 4.0;

		private static FireballParam CreateFireballParam()
		{
			FireballParam param = new FireballParam();

			param.EMFCalculationMethod = EMFCalculationMethod.DiffusionApproximation;
			param.QGPConductivityMeV = 5.8;

			param.BeamRapidity = 7.99;
			param.DiffusenessAFm = 0.546;
			param.DiffusenessBFm = 0.546;
			param.NucleonNumberA = 208;
			param.NucleonNumberB = 208;
			param.NuclearRadiusAFm = 6.62;
			param.NuclearRadiusBFm = 6.62;
			param.ProtonNumberA = 82;
			param.ProtonNumberB = 82;
			param.ImpactParameterFm = 2.0;

			return param;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private EuclideanVector3D[] CalculateSingleNucleusMagneticFieldValues()
		{
			FireballElectromagneticField emf =
				new FireballElectromagneticField(CreateFireballParam());
			PrivateObject privateEMF = new PrivateObject(emf);
			DensityFunction density = (DensityFunction)privateEMF.GetField("ProtonDensityFunctionA");

			EuclideanVector3D[] fieldValues = new EuclideanVector3D[PositionsInReactionPlane.Length];
			for(int i = 0; i < PositionsInReactionPlane.Length; i++)
			{
				fieldValues[i] = emf.CalculateSingleNucleusMagneticField(
						EffectiveTime,
						PositionsInReactionPlane[i],
						density);
			}

			return fieldValues;
		}

		private EuclideanVector3D[] CalculateMagneticFieldValues()
		{
			FireballElectromagneticField emf =
				new FireballElectromagneticField(CreateFireballParam());

			EuclideanVector3D[] fieldValues = new EuclideanVector3D[Positions.Length];
			for(int i = 0; i < Positions.Length; i++)
			{
				fieldValues[i] = emf.CalculateMagneticField(Time, Positions[i]);
			}

			return fieldValues;
		}

		private void AssertCorrectSingleNucleusMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			uint roundedDigits = 11;

			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Y);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X);
			AssertHelper.AssertRoundedEqual(0.08880091919, fieldValues[1].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z);

			AssertHelper.AssertRoundedEqual(-0.16906670076, fieldValues[2].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.08453335038, fieldValues[2].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z);
		}

		private void AssertCorrectMagneticFieldValues(EuclideanVector3D[] fieldValues)
		{
			uint roundedDigits = 9;

			AssertHelper.AssertRoundedEqual(0, fieldValues[0].X);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Y);
			AssertHelper.AssertRoundedEqual(0, fieldValues[0].Z);

			AssertHelper.AssertRoundedEqual(0, fieldValues[1].X);
			AssertHelper.AssertRoundedEqual(0.00334717796, fieldValues[1].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[1].Z);

			AssertHelper.AssertRoundedEqual(-0.00667074897, fieldValues[2].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.00332357101, fieldValues[2].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[2].Z);

			AssertHelper.AssertRoundedEqual(-0.0424120515, fieldValues[3].X, roundedDigits);
			AssertHelper.AssertRoundedEqual(0.0412806221, fieldValues[3].Y, roundedDigits);
			AssertHelper.AssertRoundedEqual(0, fieldValues[3].Z);
		}
	}
}