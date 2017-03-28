using Meta.Numerics;
using System;
using Yburn.PhysUtil;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public class QQBoundState : QQState
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQBoundState(
			QQStateParam param,
			int quantumNumberN
		)
			: base(param)
		{
			InitSolver();

			UseFixedAlpha = false;
			QuantumNumberN = quantumNumberN;
			RadiusFm = Solver.PositionValues;
			WaveFunctionNorm = new double[Param.StepNumber + 1];

			AssertInputValid();
		}

		/********************************************************************************************
		* Public members, functions and properties
		********************************************************************************************/

		public int QuantumNumberN // = 1,2,3,...
		{
			get;
			private set;
		}

		public int NumberExtrema
		{
			get;
			protected set;
		}

		public bool UseFixedAlpha;

		public double BoundMassMeV
		{
			get
			{
				return 2 * Param.QuarkMassMeV + Param.EnergyMeV + PotentialFm.ValueAtInfinity;
			}
		}

		public double UltraSoftScaleMeV
		{
			get
			{
				return Constants.HbarCMeVFm * PotentialExpectationValueFm();
			}
		}

		public double AlphaUltraSoft
		{
			get
			{
				return AlphaS.Value(UltraSoftScaleMeV);
			}
		}

		// in fm^power
		public double RadiusExpectationValue(
			int power
			)
		{
			if(power < -1)
			{
				throw new Exception("Minimum power is -1.");
			}

			double expectationValue = 0;
			for(int j = 1; j < Param.StepNumber; j++)
			{
				expectationValue += Math.Pow(RadiusFm[j], power) * WaveFunctionNorm[j];
			}
			expectationValue += 0.5 * Math.Pow(RadiusFm[Param.StepNumber], power)
				* WaveFunctionNorm[Param.StepNumber];
			expectationValue *= StepSizeFm;

			return expectationValue;
		}

		// successive change of Energy and GammaDamp in order to find
		// the eigenfunction of the radial hamiltonian
		public override void SearchEigenfunction()
		{
			if(Param.MaxShootingTrials > 0)
			{
				SearchEigenfunctionByShooting();
			}
			else
			{
				CalculateWaveFunctionUpdateStatus();
			}
		}

		public double SearchQuarkMass(
			double desiredBoundMassMeV
			)
		{
			PrepareShooting();

			while(Trials < Param.MaxShootingTrials
				&& !CalculationCancelToken.IsCancellationRequested)
			{
				PerformShooting();
				UpdateQuarkMass(desiredBoundMassMeV);

				if(Math.Abs(desiredBoundMassMeV - BoundMassMeV) < 1e-6)
				{
					break;
				}
			}

			if(Trials >= Param.MaxShootingTrials)
			{
				throw new Exception("QuarkMass could not be adjusted.");
			}

			return Param.QuarkMassMeV;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private bool IsAccuracyAchieved
		{
			get
			{
				return IsAlphaAccuracyAchieved
					&& IsWaveFunctionAccuracyAchieved;
			}
		}

		private bool IsAlphaAccuracyAchieved
		{
			get
			{
				return AchievedAccuracyAlpha < Param.AccuracyAlpha
					|| UseFixedAlpha;
			}
		}

		private double AchievedAccuracyAlpha;

		private bool IsWaveFunctionAccuracyAchieved
		{
			get
			{
				return AchievedAccuracyWaveFunction < Param.AccuracyWaveFunction;
			}
		}

		private double AchievedAccuracyWaveFunction
		{
			get
			{
				return ComplexMath.Abs(WaveFunctionFm[0]);
			}
		}

		private double DesiredAccuracyWaveFunction
		{
			get
			{
				return UseFixedAlpha ?
					Param.AccuracyWaveFunction
					: Param.AccuracyWaveFunction
					* Math.Max(1, AchievedAccuracyAlpha / Param.AccuracyAlpha);
			}
		}

		private double[] WaveFunctionNorm;

		protected void InitSolver()
		{
			Solver = new RseSolver();
			Solver.InitialPosition = Param.MaxRadiusFm;
			Solver.FinalPosition = 0;
			Solver.Samples = Param.StepNumber;
			Solver.RightHandSide = EffectivePotentialMinusEigenvalue;
			Solver.InitialDerivativeValue = new Complex(0, 0);
			Solver.InitialSolutionValue = new Complex(1e-40, 0);

			Solver.Initialize();
		}

		private void AssertInputValid()
		{
			if(QuantumNumberN <= Param.QuantumNumberL)
			{
				throw new Exception("QuantumNumberN <= QuantumNumberL.");
			}
			if(StatusValues != null && StatusValues.Length != 7)
			{
				throw new Exception("Length of StatusValues must be seven.");
			}
		}

		private void UpdateQuarkMass(
			double desiredBoundMassMeV
			)
		{
			Param.QuarkMassMeV = 0.5 * (desiredBoundMassMeV - Param.EnergyMeV);
		}

		private double CalculateSoftScaleMeV()
		{
			return Constants.HbarCMeVFm * RadiusExpectationValue(-1);
		}

		private double PotentialExpectationValueFm()
		{
			Complex expectationValue = 0;
			for(int j = 1; j < Param.StepNumber; j++)
			{
				expectationValue += WaveFunctionNorm[j] * PotentialFm.Value(RadiusFm[j]);
			}
			expectationValue += 0.5 * WaveFunctionNorm[Param.StepNumber]
				* PotentialFm.Value(RadiusFm[Param.StepNumber]);

			return StepSizeFm * ComplexMath.Abs(expectationValue);
		}

		private void CalculateWaveFunctionUpdateStatus()
		{
			CalculateWaveFunction();
			UpdateStatusValues();
		}

		protected void CalculateWaveFunction()
		{
			Trials++;

			SolveSchroedingerEquation();
			CalculateWaveFunctionNorm();
			NormalizeWaveFunction();
			CountExtrema();
		}

		private void SolveSchroedingerEquation()
		{
			Solver.Solve();
			WaveFunctionFm = Solver.SolutionValues;
		}

		private void CalculateWaveFunctionNorm()
		{
			for(int j = 0; j <= Param.StepNumber; j++)
			{
				WaveFunctionNorm[j] = Norm(WaveFunctionFm[j]);
			}
		}

		private void CountExtrema()
		{
			NumberExtrema = 0;
			for(int j = 1; j < Param.StepNumber; j++)
			{
				// extrema of WaveFunction are maxima of WaveFunctionNorm
				if(WaveFunctionNorm[j] > WaveFunctionNorm[j + 1]
					&& WaveFunctionNorm[j] > WaveFunctionNorm[j - 1])
				{
					NumberExtrema++;
				}
			}
		}

		protected void UpdateStatusValues()
		{
			if(StatusValues != null)
			{
				StatusValues[0] = Trials.ToString();
				StatusValues[1] = BoundMassMeV.ToString("G12");
				StatusValues[2] = Param.SoftScaleMeV.ToString("G12");
				StatusValues[3] = Param.EnergyMeV.ToString("G12");
				StatusValues[4] = Param.GammaDampMeV.ToString("G12");
				StatusValues[5] = ComplexMath.Abs(WaveFunctionFm[0])
					.ToString("G4");
				StatusValues[6] = NumberExtrema.ToString();
			}
		}

		private void NormalizeWaveFunction()
		{
			double integral = 0;
			for(int j = 1; j < Param.StepNumber; j++)
			{
				integral += WaveFunctionNorm[j];
			}
			integral += .5 * (WaveFunctionNorm[0] + WaveFunctionNorm[Param.StepNumber]);
			integral *= StepSizeFm;

			double sqrtIntegral = Math.Sqrt(integral);
			for(int j = 0; j <= Param.StepNumber; j++)
			{
				WaveFunctionFm[j] /= sqrtIntegral;
				WaveFunctionNorm[j] /= integral;
			}
		}

		private void SearchEigenfunctionFixedAlpha()
		{
			SearchRegionWithDesiredNumberExtrema();
			SearchAccurately();
		}

		private void SearchRegionWithDesiredNumberExtrema()
		{
			CalculateWaveFunctionUpdateStatus();

			int desiredNumberExtrema = QuantumNumberN - Param.QuantumNumberL;
			while(NumberExtrema != desiredNumberExtrema
				&& Trials < Param.MaxShootingTrials
				&& !CalculationCancelToken.IsCancellationRequested)
			{
				if(NumberExtrema > desiredNumberExtrema)
				{
					Param.EnergyMeV--;
				}
				else if(NumberExtrema < desiredNumberExtrema)
				{
					Param.EnergyMeV++;
				}

				CalculateWaveFunctionUpdateStatus();
			}
		}

		private void SearchEigenfunctionByShooting()
		{
			PrepareShooting();
			PerformShooting();

			if(Trials >= Param.MaxShootingTrials)
			{
				throw new Exception("Eigenfunction could not be found.");
			}
		}

		private void PrepareShooting()
		{
			Trials = 0;
			AchievedAccuracyAlpha = 1;

			if(PotentialFm.IsReal)
			{
				Param.GammaDampMeV = 0;
			}
		}

		private double PreviousSoftScale;

		private double CurrentSoftScale;

		private void PerformShooting()
		{
			PreviousSoftScale = Param.SoftScaleMeV;
			CurrentSoftScale = Param.SoftScaleMeV;

			while(Trials < Param.MaxShootingTrials
				&& !CalculationCancelToken.IsCancellationRequested)
			{
				SearchEigenfunctionFixedAlpha();
				UpdateFromNewWaveFunction();

				if(IsAccuracyAchieved)
				{
					break;
				}
			}
		}

		private void UpdateFromNewWaveFunction()
		{
			if(!UseFixedAlpha)
			{
				UpdateSoftScale();
				UpdateAlpha();
				UpdatePotential();
			}
		}

		private void UpdateSoftScale()
		{
			PreviousSoftScale = CurrentSoftScale;
			CurrentSoftScale = GetNextSoftScale();
			Param.SoftScaleMeV = CurrentSoftScale;
		}

		private double GetNextSoftScale()
		{
			double upperScale = Math.Max(PreviousSoftScale, CurrentSoftScale);
			double lowerScale = Math.Min(PreviousSoftScale, CurrentSoftScale);
			double newScale
				= Param.AggressivenessAlpha * CalculateSoftScaleMeV()
				+ (1.0 - Param.AggressivenessAlpha) * CurrentSoftScale;

			if((newScale < lowerScale && lowerScale == CurrentSoftScale) ||
				(newScale > upperScale && upperScale == CurrentSoftScale))
			{
				// The new step goes in the same direction as the last one.
				return newScale;
			}
			else
			{
				// The new step goes in the opposite direction as the last one. There is a limit of
				// 0.5*(upper-lower) on the new step size to prevent a wobbling about the true solution.
				double middle = 0.5 * (upperScale + lowerScale);
				if(newScale >= lowerScale && lowerScale == CurrentSoftScale)
				{
					return Math.Min(newScale, middle);
				}
				else if(newScale <= upperScale && upperScale == CurrentSoftScale)
				{
					return Math.Max(newScale, middle);
				}
				else
				{
					throw new Exception("Behavior of SoftScale can not be handled properly.");
				}
			}
		}

		private void UpdateAlpha()
		{
			double alphaTemp = AlphaSoft;
			AlphaSoft = AlphaS.Value(CurrentSoftScale);
			AchievedAccuracyAlpha = Math.Abs(AlphaSoft / alphaTemp - 1.0);
		}

		private void UpdatePotential()
		{
			PotentialFm.UpdateAlpha(AlphaSoft);
		}

		private RseShootingSolver ShootingSolver;

		private void SearchAccurately()
		{
			Solver.RightHandSide = EffectivePotential;

			InitShootingSolver();
			ShootingSolver.Solve();
		}

		private void InitShootingSolver()
		{
			ShootingSolver = new RseShootingSolver();
			ShootingSolver.Solver = Solver;
			ShootingSolver.DesiredAccuracy = DesiredAccuracyWaveFunction;
			ShootingSolver.Aggressiveness = Param.AggressivenessEnergy;
			ShootingSolver.MaxTrials = Param.MaxShootingTrials - Trials;
			ShootingSolver.Eigenvalue = GetEigenvalue();
			ShootingSolver.CancellationToken = CalculationCancelToken;
			ShootingSolver.SolutionConstraint = () =>
			{
				return NumberExtrema == QuantumNumberN - Param.QuantumNumberL;
			};
			ShootingSolver.SolutionAccuracyMeasure = () =>
			{
				return ComplexMath.Abs(WaveFunctionFm[0]);
			};
			ShootingSolver.ActionAfterIteration = () =>
			{
				Trials++;

				UpdateWaveFunction();
				UpdateEigenvalue();
				UpdateStatusValues();
			};
		}

		private void UpdateWaveFunction()
		{
			WaveFunctionFm = Solver.SolutionValues;

			CalculateWaveFunctionNorm();
			NormalizeWaveFunction();
			CountExtrema();
		}

		private Complex GetEigenvalue()
		{
			return (new Complex(Param.EnergyMeV, -0.5 * Param.GammaDampMeV))
				* Param.QuarkMassMeV / Constants.HbarCMeVFm / Constants.HbarCMeVFm;
		}

		private void UpdateEigenvalue()
		{
			Param.EnergyMeV = ShootingSolver.Eigenvalue.Re
				* Constants.HbarCMeVFm * Constants.HbarCMeVFm / Param.QuarkMassMeV;
			Param.GammaDampMeV = -2 * ShootingSolver.Eigenvalue.Im
				* Constants.HbarCMeVFm * Constants.HbarCMeVFm / Param.QuarkMassMeV;
		}
	}
}