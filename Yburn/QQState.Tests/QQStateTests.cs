using Meta.Numerics;
using Meta.Numerics.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using Yburn.PhysUtil;

namespace Yburn.QQState.Tests
{
	[TestClass]
	public class QQStateTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		public void CoulombWaveL0E1000()
		{
			QQStateParam param = GetCommonFreeStateParam();
			param.QuantumNumberL = 0;

			QQFreeState freeState = new QQFreeState(param);
			freeState.SearchEigenfunction();

			double maxDeviation = GetMaxDeviationFromCoulombWave(freeState);
			Assert.IsTrue(maxDeviation < 1e-4);
		}

		[TestMethod]
		public void CoulombWaveL2E1000()
		{
			QQStateParam param = GetCommonFreeStateParam();
			param.QuantumNumberL = 2;

			QQFreeState freeState = new QQFreeState(param);
			freeState.SearchEigenfunction();

			double maxDeviation = GetMaxDeviationFromCoulombWave(freeState);
			Assert.IsTrue(maxDeviation < 1e-4);
		}

		[TestMethod]
		public void HydrogenN1L0()
		{
			QQBoundState boundState = CreateBoundState_HydrogenN1L0();
			boundState.SearchEigenfunction();

			AssertAccuracyAchieved(1e-7, GetMaxDeviationFromHydrogenN1L0(boundState));
		}

		[TestMethod]
		public void HydrogenN2L1()
		{
			QQBoundState boundState = CreateBoundState_HydrogenN2L1();
			boundState.SearchEigenfunction();

			AssertAccuracyAchieved(1e-6, GetMaxDeviationFromHydrogenN2L1(boundState));
		}

		[TestMethod]
		public void FindQuarkMass()
		{
			QQStateParam param = GetCommonBoundStateParam();
			param.AccuracyWaveFunction = 1e-9;
			param.AggressivenessAlpha = 0.5;
			param.AggressivenessEnergy = 40;
			param.Energy_MeV = -141.517335648066;
			param.PotentialType = PotentialType.Tzero;
			param.QuantumNumberL = 0;
			param.Sigma_MeV = 192000;
			param.SoftScale_MeV = 1542.07788957569;
			param.StepNumber = 20000;

			QQBoundState boundState = new QQBoundState(param, 1);

			double MassY1S_MeV = 9460.3;
			boundState.SearchQuarkMass(MassY1S_MeV);

			Assert.IsTrue(
				Math.Abs(boundState.Param.QuarkMass_MeV / param.QuarkMass_MeV - 1.0) < 1e-6);
			Assert.IsTrue(Math.Abs(boundState.BoundMass_MeV / MassY1S_MeV - 1.0) < 1e-6);
		}

		[TestMethod]
		public void SpinDependentPotential()
		{
			SpinDependentPotential potential = new SpinDependentPotential(
				0.5, 1.92e5, ColorState.Singlet, SpinState.Triplet, 2, 5);
			Assert.AreEqual(5 * 0.5, (double)(new PrivateObject(potential)).GetField("SpinFactor"));

			potential = new SpinDependentPotential(
				0.5, 1.92e5, ColorState.Singlet, SpinState.Singlet, 2, 5);
			Assert.AreEqual(-5 * 1.5, (double)(new PrivateObject(potential)).GetField("SpinFactor"));
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static QQStateParam GetCommonFreeStateParam()
		{
			QQStateParam param = new QQStateParam
			{
				AccuracyAlpha = 1e-6,
				AccuracyWaveFunction = 1e-9,
				AggressivenessAlpha = 0.5,
				ColorState = ColorState.Octet,
				Energy_MeV = 1000,
				GammaDamp_MeV = 0,
				MaxRadius_fm = 10,
				PotentialType = PotentialType.Tzero_NoString,
				QuarkMass_MeV = 4800.90885593666,
				RunningCouplingType = RunningCouplingType.LOperturbative_Cutoff3,
				SoftScale_MeV = 1420,
				StepNumber = 20000,
				Tchem_MeV = 120,
				Tcrit_MeV = 160,
				Temperature_MeV = 0
			};

			return param;
		}

		private static QQStateParam GetCommonBoundStateParam()
		{
			QQStateParam param = new QQStateParam
			{
				AccuracyAlpha = 1e-6,
				ColorState = ColorState.Singlet,
				GammaDamp_MeV = 0,
				MaxRadius_fm = 10,
				MaxShootingTrials = 5000,
				QuarkMass_MeV = 4800.90885593666,
				RunningCouplingType = RunningCouplingType.LOperturbative_Cutoff3,
				Tchem_MeV = 120,
				Tcrit_MeV = 160,
				Temperature_MeV = 0
			};

			return param;
		}

		private static QQBoundState CreateBoundState_HydrogenN1L0()
		{
			QQStateParam param = GetCommonBoundStateParam();
			param.AccuracyWaveFunction = 1e-9;
			param.AggressivenessAlpha = 0.7;
			param.AggressivenessEnergy = 40;
			param.Energy_MeV = -390.02077042504118;
			param.PotentialType = PotentialType.Tzero_NoString;
			param.QuantumNumberL = 0;
			param.SoftScale_MeV = 1368.3765139843001;
			param.StepNumber = 30000;

			return new QQBoundState(param, 1);
		}

		private static QQBoundState CreateBoundState_HydrogenN2L1()
		{
			QQStateParam param = GetCommonBoundStateParam();
			param.AccuracyWaveFunction = 1e-8;
			param.AggressivenessAlpha = 0.7;
			param.AggressivenessEnergy = 0.02;
			param.Energy_MeV = -341.205311278384;
			param.PotentialType = PotentialType.Tzero_NoString;
			param.QuantumNumberL = 1;
			param.SoftScale_MeV = 639.940673158918;
			param.StepNumber = 16000;

			return new QQBoundState(param, 2);
		}

		private static double GetMaxDeviation(
			double[] xValues,
			Complex[] yValues,
			Complex[] analyticValues
			)
		{
			double maxDeviation = 0;
			double currentDeviation;
			for(int i = 0; i < yValues.Length; i++)
			{
				currentDeviation = ComplexMath.Abs(yValues[i] - analyticValues[i]);

				if(double.IsNaN(currentDeviation)
					|| double.IsInfinity(currentDeviation))
				{
					return currentDeviation;
				}

				if(currentDeviation > maxDeviation)
				{
					maxDeviation = currentDeviation;
				}
			}

			return maxDeviation;
		}

		private static double GetMaxDeviationFromCoulombWave(
			QQFreeState freeState
			)
		{
			return GetMaxDeviation(
				freeState.Radius_fm, freeState.WaveFunction_fm,
				GetCoulombWaveArray(freeState));
		}

		private static Complex[] GetCoulombWaveArray(
			QQFreeState freeState
			)
		{
			return GetCoulombWaveArray(
				  freeState.Param.QuantumNumberL, GetAlphaEff(freeState),
				  freeState.Param.QuarkMass_MeV, freeState.WaveVector_fm, freeState.Radius_fm);
		}

		private static double GetAlphaEff(
			QQFreeState freeState
			)
		{
			Potential potential = (Potential)(new PrivateObject(freeState)).GetField("Potential_fm");
			return potential.AlphaEff;
		}

		private static Complex[] GetCoulombWaveArray(
			int quantumNumberL,
			double alphaEff,
			double quarkMass_MeV,
			double waveVector_fm,
			double[] radius_fm
			)
		{
			double twoOverPi = Math.Sqrt(2 / Math.PI);
			double eta = -0.5 * alphaEff * quarkMass_MeV / waveVector_fm / Constants.HbarC_MeV_fm;

			Complex[] coulombWave = new Complex[radius_fm.Length];
			for(int j = 0; j < radius_fm.Length; j++)
			{
				coulombWave[j] = new Complex(twoOverPi * AdvancedMath.CoulombF(quantumNumberL, eta,
					waveVector_fm * radius_fm[j]), 0);
			}

			return coulombWave;
		}

		private static void MakePlotFile(
			double[] positions,
			Complex[] values,
			ComplexFunction analyticValues
			)
		{
			StringBuilder builder = new StringBuilder();
			for(int i = 0; i < positions.Length; i++)
			{
				builder.AppendFormat("{0,-22}{1,-22}{2,-22}" + Environment.NewLine,
					positions[i].ToString(),
					values[i].Re.ToString(),
					analyticValues(positions[i]).Re.ToString());
			}

			File.WriteAllText("RseSolverHydroTest.txt", builder.ToString());
		}

		private static Complex CoulombEffectivePotentialN1L0(
			double x
			)
		{
			return CoulombEffectivePotential(x, 1, 0);
		}

		private static Complex CoulombEffectivePotentialN2L1(
			double x
			)
		{
			return CoulombEffectivePotential(x, 2, 1);
		}

		private static Complex CoulombEffectivePotential(
			double x,
			double n,
			double l
			)
		{
			return new Complex((l * (l + 1) / x - n) / x + 0.25, 0);
		}

		private static Complex HydrogenWaveFunctionN1L0(
			double x
			)
		{
			return new Complex(x * Math.Exp(-0.5 * x), 0);
		}

		private static Complex HydrogenWaveFunctionN2L1(
			double x
			)
		{
			return new Complex(x * x * Math.Exp(-0.5 * x) / Math.Sqrt(24), 0);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private double GetMaxDeviationFromHydrogenN1L0(
			QQBoundState boundState
			)
		{
			Complex[] hydrogenWave = GetHydrogenWaveN1L0Array(
				boundState.WaveVector_fm, boundState.Radius_fm);

			return GetMaxDeviation(boundState.Radius_fm, boundState.WaveFunction_fm, hydrogenWave);
		}

		private double GetMaxDeviationFromHydrogenN2L1(
			QQBoundState boundState
			)
		{
			Complex[] hydrogenWave = GetHydrogenWaveN2L1Array(
				boundState.WaveVector_fm, boundState.Radius_fm);

			return GetMaxDeviation(boundState.Radius_fm, boundState.WaveFunction_fm, hydrogenWave);
		}

		private Complex[] GetHydrogenWaveN1L0Array(
			double waveVector_fm,
			double[] radius_fm
			)
		{
			double preFactor = Math.Sqrt(waveVector_fm);
			Complex[] hydrogenWave = new Complex[radius_fm.Length];
			for(int j = 0; j < radius_fm.Length; j++)
			{
				hydrogenWave[j] = preFactor * HydrogenWaveFunctionN1L0(2 * waveVector_fm * radius_fm[j]);
			}

			return hydrogenWave;
		}

		private Complex[] GetHydrogenWaveN2L1Array(
			double waveVector_fm,
			double[] radius_fm
			)
		{
			double preFactor = Math.Sqrt(2 * waveVector_fm);
			Complex[] hydrogenWave = new Complex[radius_fm.Length];
			for(int j = 0; j < radius_fm.Length; j++)
			{
				hydrogenWave[j] = preFactor * HydrogenWaveFunctionN2L1(2 * waveVector_fm * radius_fm[j]);
			}

			return hydrogenWave;
		}

		private void Normalize(
			Complex[] solution,
			double stepSize,
			double n
			)
		{
			double integral = 0;
			int maxIndex = solution.Length - 1;
			for(int i = 1; i < maxIndex; i++)
			{
				integral += ComplexMath.Abs(solution[i]) * ComplexMath.Abs(solution[i]);
			}
			integral += 0.5 * (ComplexMath.Abs(solution[0]) * ComplexMath.Abs(solution[0])
				+ ComplexMath.Abs(solution[maxIndex]) * ComplexMath.Abs(solution[maxIndex]));
			integral *= stepSize * n / 2.0; // xn = 2*C1*r/n

			double sqrtIntegral = Math.Sqrt(integral);
			for(int j = 0; j <= maxIndex; j++)
			{
				solution[j] /= sqrtIntegral;
			}
		}

		private void AssertAccuracyAchieved(
			double desiredAccuracy,
			double maxDeviation
			)
		{
			Assert.IsTrue(maxDeviation < desiredAccuracy,
				string.Format("Desired accuracy: {0}" + Environment.NewLine
					+ "Actual accuracy: {1}",
					desiredAccuracy, maxDeviation));
		}
	}
}
