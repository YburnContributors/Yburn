using MathNet.Numerics.Interpolation;
using System;
using System.Collections.Generic;
using System.IO;

namespace Yburn.QQState
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	public enum RunningCouplingType
	{
		LOperturbative = 1 << 0,
		LOperturbative_Cutoff1 = 1 << 1,
		LOperturbative_Cutoff3 = 1 << 2,
		NonPerturbative_Fischer = 1 << 3,
		NonPerturbative_ITP = 1 << 4,
	};

	public class RunningCoupling
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public delegate double ParameterizationPointer(double energyMeV);

		public static RunningCoupling Create(
			RunningCouplingType type
			)
		{
			if(type == RunningCouplingType.NonPerturbative_ITP)
			{
				return new NumericalRunningCoupling();
			}

			return new RunningCoupling(type);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public RunningCoupling(
			RunningCouplingType type
			)
		{
			Type = type;
			SetParameterization();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public RunningCouplingType Type
		{
			get;
			private set;
		}

		public ParameterizationPointer Value
		{
			get;
			protected set;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static double AlphaSatIRfixpoint = 8.915 / PhysConst.NC;

		protected static double LOpert(
			double energyMeV
			)
		{
			// equivalent to 0.1197/(1. + 0.1197 * 9.0/(2*PI) * log(E/91200.))
			return PhysConst.PI / 4.5 / Math.Log(energyMeV / QQState.LambdaQCDMeV);
		}

		private static double LOpert_CutoffAt1(
			double energyMeV
			)
		{
			double alphaS = LOpert(energyMeV);
			return (alphaS < 1 && alphaS > 0) ? alphaS : 1;
		}

		private static double LOpert_CutoffAt3(
			double energyMeV
			)
		{
			double alphaS = LOpert(energyMeV);
			return (alphaS < 3 && alphaS > 0) ? alphaS : 3;
		}

		private static double NonPerturbative_Fischer(
			double energyMeV
			)
		{
			double ratioSquared = Math.Pow(energyMeV / QQState.LambdaQCDMeV, 2);
			return (AlphaSatIRfixpoint + ratioSquared * PhysConst.PI / 2.25 *
				(1.0 / Math.Log(ratioSquared) - 1.0 / (ratioSquared - 1.0))) / (1.0 + ratioSquared);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected virtual void SetParameterization()
		{
			switch(Type)
			{
				case RunningCouplingType.LOperturbative:
					Value = LOpert;
					break;

				case RunningCouplingType.LOperturbative_Cutoff1:
					Value = LOpert_CutoffAt1;
					break;

				case RunningCouplingType.LOperturbative_Cutoff3:
					Value = LOpert_CutoffAt3;
					break;

				case RunningCouplingType.NonPerturbative_Fischer:
					Value = NonPerturbative_Fischer;
					break;

				default:
					throw new Exception("Invalid RunningCouplingType.");
			}
		}
	}

	public class NumericalRunningCoupling : RunningCoupling
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public NumericalRunningCoupling()
			: base(RunningCouplingType.NonPerturbative_ITP)
		{
			List<string> allLines = new List<string>(File.ReadAllLines("..\\..\\..\\AlphaS_qQCD.txt"));
			RemoveCommentaryAndEmptyLines(allLines);

			double[] momenta;
			double[] alphas;
			ExtractValues(allLines, out momenta, out alphas);
			alphas = NormalizeToLOperturbative(momenta, alphas);

			AlphaInterpolator = CubicSpline.InterpolateNaturalSorted(momenta, alphas);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void RemoveCommentaryAndEmptyLines(
			List<string> allLines
			)
		{
			for(int j = allLines.Count - 1; j >= 0; j--)
			{
				if(string.IsNullOrWhiteSpace(allLines[j])
					|| allLines[j].StartsWith("#"))
				{
					allLines.RemoveAt(j);
				}
			}
		}

		private double[] NormalizeToLOperturbative(
			double[] momenta,
			double[] alphas
			)
		{
			int samples = momenta.Length;
			double normFactor = LOpert(momenta[samples - 1]) / alphas[samples - 1];
			double[] normedAlphas = new double[samples];
			for(int j = 0; j < samples; j++)
			{
				normedAlphas[j] = alphas[j] * normFactor;
			}

			return normedAlphas;
		}

		private static void ExtractValues(
			List<string> allLines,
			out double[] momenta,
			out double[] alphas
			)
		{
			momenta = new double[allLines.Count];
			alphas = new double[allLines.Count];
			for(int j = allLines.Count - 1; j >= 0; j--)
			{
				string[] values = allLines[j].Split(new char[] { ' ', '\t' },
					StringSplitOptions.RemoveEmptyEntries);

				momenta[j] = double.Parse(values[0]);
				alphas[j] = double.Parse(values[1]);
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private CubicSpline AlphaInterpolator;

		protected override void SetParameterization()
		{
			Value = NonPerturbative_ITP;
		}

		private double NonPerturbative_ITP(
			double energyMeV
			)
		{
			return AlphaInterpolator.Interpolate(energyMeV);
		}
	}
}