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
            param.EnergyMeV = -141.517335648066;
            param.PotentialType = PotentialType.Tzero;
            param.QuantumNumberL = 0;
            param.SigmaMeV = 192000;
            param.SoftScaleMeV = 1542.07788957569;
            param.StepNumber = 20000;

            QQBoundState boundState = new QQBoundState(param, 1);

            double MassY1SMeV = 9460.3;
            boundState.SearchQuarkMass(MassY1SMeV);

            Assert.IsTrue(
                Math.Abs(boundState.Param.QuarkMassMeV / param.QuarkMassMeV - 1.0) < 1e-6);
            Assert.IsTrue(Math.Abs(boundState.BoundMassMeV / MassY1SMeV - 1.0) < 1e-6);
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
            QQStateParam param = new QQStateParam();
            param.AccuracyAlpha = 1e-6;
            param.AccuracyWaveFunction = 1e-9;
            param.AggressivenessAlpha = 0.5;
            param.ColorState = ColorState.Octet;
            param.EnergyMeV = 1000;
            param.PotentialType = PotentialType.Tzero_NoString;
            param.QuarkMassMeV = 4800.90885593666;
            param.MaxRadiusFm = 10;
            param.RunningCouplingType = RunningCouplingType.LOperturbative_Cutoff3;
            param.StepNumber = 20000;
            param.SoftScaleMeV = 1420;
            param.TchemMeV = 120;
            param.TcritMeV = 160;

            return param;
        }

        private static QQBoundState CreateBoundState_HydrogenN1L0()
        {
            QQStateParam param = GetCommonBoundStateParam();
            param.AccuracyWaveFunction = 1e-9;
            param.AggressivenessAlpha = 0.7;
            param.EnergyMeV = -390.02077042504118;
            param.PotentialType = PotentialType.Tzero_NoString;
            param.QuantumNumberL = 0;
            param.SoftScaleMeV = 1368.3765139843001;
            param.StepNumber = 30000;

            return new QQBoundState(param, 1);
        }

        private static QQBoundState CreateBoundState_HydrogenN2L1()
        {
            QQStateParam param = GetCommonBoundStateParam();
            param.AccuracyWaveFunction = 1e-8;
            param.AggressivenessAlpha = 0.7;
            param.EnergyMeV = -341.205311278384;
            param.PotentialType = PotentialType.Tzero_NoString;
            param.QuantumNumberL = 1;
            param.SoftScaleMeV = 639.940673158918;
            param.StepNumber = 16000;

            return new QQBoundState(param, 2);
        }

        private static QQStateParam GetCommonBoundStateParam()
        {
            QQStateParam param = new QQStateParam();
            param.AccuracyAlpha = 1e-6;
            param.QuarkMassMeV = 4800.90885593666;
            param.MaxShootingTrials = 5000;
            param.MaxRadiusFm = 10;
            param.RunningCouplingType = RunningCouplingType.LOperturbative_Cutoff3;
            param.TchemMeV = 120;
            param.TcritMeV = 160;

            return param;
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
                freeState.RadiusFm, freeState.WaveFunctionFm,
                GetCoulombWaveArray(freeState));
        }

        private static Complex[] GetCoulombWaveArray(
            QQFreeState freeState
            )
        {
            return GetCoulombWaveArray(
                  freeState.Param.QuantumNumberL, GetAlphaEff(freeState),
                  freeState.Param.QuarkMassMeV, freeState.WaveVectorFm, freeState.RadiusFm);
        }

        private static double GetAlphaEff(
            QQFreeState freeState
            )
        {
            Potential potential = (Potential)(new PrivateObject(freeState)).GetField("PotentialFm");
            return potential.AlphaEff;
        }

        private static Complex[] GetCoulombWaveArray(
            int quantumNumberL,
            double alphaEff,
            double quarkMassMeV,
            double waveVectorFm,
            double[] radiusFm
            )
        {
            double twoOverPi = Math.Sqrt(2 / Math.PI);
            double eta = -0.5 * alphaEff * quarkMassMeV / waveVectorFm / Constants.HbarCMeVFm;

            Complex[] coulombWave = new Complex[radiusFm.Length];
            for(int j = 0; j < radiusFm.Length; j++)
            {
                coulombWave[j] = new Complex(twoOverPi * AdvancedMath.CoulombF(quantumNumberL, eta,
                    waveVectorFm * radiusFm[j]), 0);
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
                builder.AppendFormat("{0,-22}{1,-22}{2,-22}\r\n",
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
                boundState.WaveVectorFm, boundState.RadiusFm);

            return GetMaxDeviation(boundState.RadiusFm, boundState.WaveFunctionFm, hydrogenWave);
        }

        private double GetMaxDeviationFromHydrogenN2L1(
            QQBoundState boundState
            )
        {
            Complex[] hydrogenWave = GetHydrogenWaveN2L1Array(
                boundState.WaveVectorFm, boundState.RadiusFm);

            return GetMaxDeviation(boundState.RadiusFm, boundState.WaveFunctionFm, hydrogenWave);
        }

        private Complex[] GetHydrogenWaveN1L0Array(
            double waveVectorFm,
            double[] radiusFm
            )
        {
            double preFactor = Math.Sqrt(waveVectorFm);
            Complex[] hydrogenWave = new Complex[radiusFm.Length];
            for(int j = 0; j < radiusFm.Length; j++)
            {
                hydrogenWave[j] = preFactor * HydrogenWaveFunctionN1L0(2 * waveVectorFm * radiusFm[j]);
            }

            return hydrogenWave;
        }

        private Complex[] GetHydrogenWaveN2L1Array(
            double waveVectorFm,
            double[] radiusFm
            )
        {
            double preFactor = Math.Sqrt(2 * waveVectorFm);
            Complex[] hydrogenWave = new Complex[radiusFm.Length];
            for(int j = 0; j < radiusFm.Length; j++)
            {
                hydrogenWave[j] = preFactor * HydrogenWaveFunctionN2L1(2 * waveVectorFm * radiusFm[j]);
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
                string.Format("Desired accuracy: {0}\r\nActual accuracy: {1}",
                desiredAccuracy, maxDeviation));
        }
    }
}