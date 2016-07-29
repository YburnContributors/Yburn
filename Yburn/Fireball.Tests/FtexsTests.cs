using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yburn.TestUtil;

namespace Yburn.Fireball.Tests
{
    [TestClass]
    public class FtexsTests
    {
        /********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

        [TestMethod]
        public void SolveBjorkenflow()
        {
            SetBjorkenFlowProperties();
            InitializeFtexsSolver();

            FtexsSolver.SolveUntil(FinalTime);

            AssertSolutionsAreEqual(GetEvolvedBjorkenTemperature(), FtexsSolver.T);
        }

        [TestMethod]
        [Ignore]
        public void SolveGubserflow()
        {
            SetGubserFlowProperties();
            InitializeFtexsSolver();

            FtexsSolver.SolveUntil(FinalTime);

            AssertMaxDeviationBelow(GetEvolvedGubserTemperature(), FtexsSolver.T, 3e-2);
            AssertMaxDeviationBelow(GetEvolvedGubserVelocityX(), FtexsSolver.T, 5e-6);
            // AssertMaxDeviationBelow(GetEvolvedGubserVelocityY(), FtexsSolver.T, 5e-6);
        }

        /********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

        private static void AssertSolutionsAreEqual(
            double[,] analyticalSolution,
            double[,] numericalSolution
            )
        {
            AssertEquallySizedFields(analyticalSolution, numericalSolution);

            AssertHelper.AssertRoundedEqual(0, GetMaxDeviation(analyticalSolution, numericalSolution));
        }

        private static void AssertMaxDeviationBelow(
            double[,] analyticalSolution,
            double[,] numericalSolution,
            double accuracy
            )
        {
            AssertEquallySizedFields(analyticalSolution, numericalSolution);

            double maxDeviation = GetMaxDeviation(analyticalSolution, numericalSolution);
            Assert.IsTrue(maxDeviation < accuracy, "MaxDeviation = " + maxDeviation);
        }

        private static void AssertEquallySizedFields(
            double[,] analyticalSolution,
            double[,] numericalSolution
            )
        {
            Assert.AreEqual(analyticalSolution.GetLength(0), numericalSolution.GetLength(0));
            Assert.AreEqual(analyticalSolution.GetLength(1), numericalSolution.GetLength(1));
        }

        private static double GetMaxDeviation(
            double[,] analyticalSolution,
            double[,] numericalSolution
            )
        {
            double maxDeviation = 0;
            for(int i = 0; i < analyticalSolution.GetLength(0); i++)
            {
                for(int j = 0; j < analyticalSolution.GetLength(1); j++)
                {
                    maxDeviation = Math.Max(maxDeviation,
                        Math.Abs(analyticalSolution[i, j] - numericalSolution[i, j]));
                }
            }

            return maxDeviation;
        }

        /********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

        private Ftexs FtexsSolver;

        private double InitialTime;

        private double FinalTime;

        private double MaxCFL;

        private double GridCellSize;

        private int GridCellNumberX;

        private int GridCellNumberY;

        private double[,] Temperature;

        private double[,] VelocityX;

        private double[,] VelocityY;

        private double ArtViscStrength = 0;

        private void InitializeFtexsSolver()
        {
            FtexsSolver = new Ftexs(
                GridCellSize, InitialTime, MaxCFL, Temperature,
                VelocityX, VelocityY, ArtViscStrength);
        }

        private void SetBjorkenFlowProperties()
        {
            InitialTime = 1;
            FinalTime = 2;
            MaxCFL = 0.5;
            GridCellSize = 1;
            GridCellNumberX = 5;
            GridCellNumberY = 5;

            Temperature = GetInitialBjorkenTemperature();
            VelocityX = GetInitialBjorkenXvelocity();
            VelocityY = GetInitialBjorkenYvelocity();
        }

        private double[,] GetInitialBjorkenTemperature()
        {
            double[,] temperature = new double[GridCellNumberX, GridCellNumberY];
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    temperature[i, j] = 1;
                }
            }

            return temperature;
        }

        private double[,] GetInitialBjorkenXvelocity()
        {
            double[,] velocity = new double[GridCellNumberX, GridCellNumberY];
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    velocity[i, j] = 0;
                }
            }

            return velocity;
        }

        private double[,] GetInitialBjorkenYvelocity()
        {
            double[,] velocity = new double[GridCellNumberX, GridCellNumberY];
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    velocity[i, j] = 0;
                }
            }

            return velocity;
        }

        private double[,] GetEvolvedBjorkenTemperature()
        {
            double[,] evolvedTemperature = new double[GridCellNumberX, GridCellNumberY];
            double[,] initialTemperature = GetInitialBjorkenTemperature();
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    evolvedTemperature[i, j] = initialTemperature[i, j]
                        * Math.Pow(InitialTime / FinalTime, 1 / 3.0);
                }
            }

            return evolvedTemperature;
        }

        private double GubserQ;

        private void SetGubserFlowProperties()
        {
            InitialTime = 0.1;
            FinalTime = 4;
            MaxCFL = 0.01;
            GridCellSize = 0.2;
            GridCellNumberX = 40;
            GridCellNumberY = 40;
            GubserQ = 0.25;

            Temperature = GetInitialGubserTemperature();
            VelocityX = GetInitialGubserVelocityX();
            VelocityY = GetInitialGubserVelocityY();
        }

        private double[,] GetInitialGubserTemperature()
        {
            return GetGubserTemperature(InitialTime);
        }

        private double[,] GetInitialGubserVelocityX()
        {
            return GetGubserVelocityX(InitialTime);
        }

        private double[,] GetInitialGubserVelocityY()
        {
            return GetGubserVelocityY(InitialTime);
        }

        private double[,] GetEvolvedGubserTemperature()
        {
            return GetGubserTemperature(FinalTime);
        }

        private double[,] GetEvolvedGubserVelocityX()
        {
            return GetGubserVelocityX(FinalTime);
        }

        private double[,] GetEvolvedGubserVelocityY()
        {
            return GetGubserVelocityY(FinalTime);
        }

        private double[,] GetGubserTemperature(
            double time
            )
        {
            double gubserQSquare = GubserQ * GubserQ;
            double gubserQPower4 = gubserQSquare * gubserQSquare;
            double radiusSquare;
            double[,] temperature = new double[GridCellNumberX, GridCellNumberY];
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    radiusSquare = GridCellSize * GridCellSize * (i * i + j * j);
                    temperature[i, j] =
                         4000.0 * InitialTime * Math.Pow(time, -1 / 3.0) * Math.Pow(2 * GubserQ, 2 / 3.0)
                        * Math.Pow(
                        1 + gubserQSquare * (time * time + radiusSquare)
                        + gubserQPower4 * Math.Pow(time * time - radiusSquare, 2),
                        -1 / 3.0);
                }
            }

            return temperature;
        }

        private double[,] GetGubserVelocityX(
            double time
            )
        {
            double[,] velocityX = new double[GridCellNumberX, GridCellNumberY];
            double[,] velocityR = GetGubserVelocityR(time);
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    double angle = j / Math.Sqrt(i * i + j * j);
                    velocityX[i, j] = velocityR[i, j] * Math.Cos(angle);
                }
            }

            velocityX[0, 0] = 0;

            return velocityX;
        }

        private double[,] GetGubserVelocityY(
            double time
            )
        {
            double[,] velocityY = new double[GridCellNumberX, GridCellNumberY];
            double[,] velocityR = GetGubserVelocityR(time);
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    double angle = j / Math.Sqrt(i * i + j * j);
                    velocityY[i, j] = velocityR[i, j] * Math.Sin(angle);
                }
            }

            velocityY[0, 0] = 0;

            return velocityY;
        }

        private double[,] GetGubserVelocityR(
            double time
            )
        {
            double gubserQSquare = GubserQ * GubserQ;
            double radius;
            double[,] velocity = new double[GridCellNumberX, GridCellNumberY];
            for(int i = 0; i < GridCellNumberX; i++)
            {
                for(int j = 0; j < GridCellNumberY; j++)
                {
                    radius = GridCellSize * Math.Sqrt(i * i + j * j);
                    velocity[i, j] = 2 * gubserQSquare * time * radius
                        / (1 + gubserQSquare * (time * time + radius * radius));
                }
            }

            return velocity;
        }
    }
}