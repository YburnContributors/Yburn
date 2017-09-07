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
			Radius_fm = Solver.PositionValues;
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

		public double BoundMass_MeV
		{
			get
			{
				return 2 * Param.QuarkMass_MeV + Param.Energy_MeV + Potential_fm.ValueAtInfinity;
			}
		}

		public double UltraSoftScale_MeV
		{
			get
			{
				return Constants.HbarC_MeV_fm * PotentialExpectationValue_fm();
			}
		}

		public double AlphaUltraSoft
		{
			get
			{
				return AlphaS.Value(UltraSoftScale_MeV);
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
				expectationValue += Math.Pow(Radius_fm[j], power) * WaveFunctionNorm[j];
			}
			expectationValue += 0.5 * Math.Pow(Radius_fm[Param.StepNumber], power)
				* WaveFunctionNorm[Param.StepNumber];
			expectationValue *= StepSize_fm;

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
			double desiredBoundMass_MeV
			)
		{
			PrepareShooting();

			while(Trials < Param.MaxShootingTrials
				&& !CalculationCancelToken.IsCancellationRequested)
			{
				PerformShooting();
				UpdateQuarkMass(desiredBoundMass_MeV);

				if(Math.Abs(desiredBoundMass_MeV - BoundMass_MeV) < 1e-6)
				{
					break;
				}
			}

			if(Trials >= Param.MaxShootingTrials)
			{
				throw new Exception("QuarkMass could not be adjusted.");
			}

			return Param.QuarkMass_MeV;
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
				return ComplexMath.Abs(WaveFunction_fm[0]);
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
			Solver = new RseSolver
			{
				InitialPosition = Param.MaxRadius_fm,
				FinalPosition = 0,
				Samples = Param.StepNumber,
				RightHandSide = EffectivePotentialMinusEigenvalue,
				InitialDerivativeValue = new Complex(0, 0),
				InitialSolutionValue = new Complex(1e-40, 0)
			};

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
			double desiredBoundMass_MeV
			)
		{
			Param.QuarkMass_MeV = 0.5 * (desiredBoundMass_MeV - Param.Energy_MeV);
		}

		private double CalculateSoftScale_MeV()
		{
			return Constants.HbarC_MeV_fm * RadiusExpectationValue(-1);
		}

		private double PotentialExpectationValue_fm()
		{
			Complex expectationValue = 0;
			for(int j = 1; j < Param.StepNumber; j++)
			{
				expectationValue += WaveFunctionNorm[j] * Potential_fm.Value(Radius_fm[j]);
			}
			expectationValue += 0.5 * WaveFunctionNorm[Param.StepNumber]
				* Potential_fm.Value(Radius_fm[Param.StepNumber]);

			return StepSize_fm * ComplexMath.Abs(expectationValue);
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
			WaveFunction_fm = Solver.SolutionValues;
		}

		private void CalculateWaveFunctionNorm()
		{
			for(int j = 0; j <= Param.StepNumber; j++)
			{
				WaveFunctionNorm[j] = Norm(WaveFunction_fm[j]);
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
				StatusValues[1] = BoundMass_MeV.ToString("G12");
				StatusValues[2] = Param.SoftScale_MeV.ToString("G12");
				StatusValues[3] = Param.Energy_MeV.ToString("G12");
				StatusValues[4] = Param.GammaDamp_MeV.ToString("G12");
				StatusValues[5] = ComplexMath.Abs(WaveFunction_fm[0])
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
			integral *= StepSize_fm;

			double sqrtIntegral = Math.Sqrt(integral);
			for(int j = 0; j <= Param.StepNumber; j++)
			{
				WaveFunction_fm[j] /= sqrtIntegral;
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
					Param.Energy_MeV--;
				}
				else if(NumberExtrema < desiredNumberExtrema)
				{
					Param.Energy_MeV++;
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

			if(Potential_fm.IsReal)
			{
				Param.GammaDamp_MeV = 0;
			}
		}

		private double PreviousSoftScale;

		private double CurrentSoftScale;

		private void PerformShooting()
		{
			PreviousSoftScale = Param.SoftScale_MeV;
			CurrentSoftScale = Param.SoftScale_MeV;

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
			Param.SoftScale_MeV = CurrentSoftScale;
		}

		private double GetNextSoftScale()
		{
			double upperScale = Math.Max(PreviousSoftScale, CurrentSoftScale);
			double lowerScale = Math.Min(PreviousSoftScale, CurrentSoftScale);
			double newScale
				= Param.AggressivenessAlpha * CalculateSoftScale_MeV()
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
			Potential_fm.UpdateAlpha(AlphaSoft);
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
			ShootingSolver = new RseShootingSolver
			{
				Solver = Solver,
				DesiredAccuracy = DesiredAccuracyWaveFunction,
				Aggressiveness = Param.AggressivenessEnergy,
				MaxTrials = Param.MaxShootingTrials - Trials,
				Eigenvalue = GetEigenvalue(),
				CancellationToken = CalculationCancelToken,
				SolutionConstraint = () => NumberExtrema == QuantumNumberN - Param.QuantumNumberL,
				SolutionAccuracyMeasure = () => ComplexMath.Abs(WaveFunction_fm[0]),
				ActionAfterIteration = () =>
				{
					Trials++;
					UpdateWaveFunction();
					UpdateEigenvalue();
					UpdateStatusValues();
				}
			};
		}

		private void UpdateWaveFunction()
		{
			WaveFunction_fm = Solver.SolutionValues;

			CalculateWaveFunctionNorm();
			NormalizeWaveFunction();
			CountExtrema();
		}

		private Complex GetEigenvalue()
		{
			return (new Complex(Param.Energy_MeV, -0.5 * Param.GammaDamp_MeV))
				* Param.QuarkMass_MeV / Constants.HbarC_MeV_fm / Constants.HbarC_MeV_fm;
		}

		private void UpdateEigenvalue()
		{
			Param.Energy_MeV = ShootingSolver.Eigenvalue.Re
				* Constants.HbarC_MeV_fm * Constants.HbarC_MeV_fm / Param.QuarkMass_MeV;
			Param.GammaDamp_MeV = -2 * ShootingSolver.Eigenvalue.Im
				* Constants.HbarC_MeV_fm * Constants.HbarC_MeV_fm / Param.QuarkMass_MeV;
		}
	}
}
