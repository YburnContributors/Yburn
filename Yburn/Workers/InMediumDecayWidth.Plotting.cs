using System;
using System.Diagnostics;
using System.Text;
using Yburn.Fireball;

namespace Yburn.Workers
{
	partial class InMediumDecayWidth
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public Process PlotInMediumDecayWidth()
		{
			CalculateInMediumDecayWidth();

			StringBuilder plotFile = new StringBuilder();
			AppendHeader_InMediumDecayWidth(plotFile);
			plotFile.AppendLine();

			string[][] titleList = new string[DecayWidthEvaluationTypes.Length][];
			for(int i = 0; i < DecayWidthEvaluationTypes.Length; i++)
			{
				titleList[i] = Array.ConvertAll(BottomiumStates,
					state => GetBottomiumStateGnuplotCode(state) + ", "
					+ DecayWidthEvaluationTypes[i].ToUIString());
			}
			AppendPlotCommands(plotFile, "linespoints", titleList);

			WritePlotFile(plotFile);

			return StartGnuplot();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static string GetBottomiumStateGnuplotCode(
			BottomiumState state
			)
		{
			switch(state)
			{
				case BottomiumState.Y1S:
					return "{/Symbol U}(1S)";

				case BottomiumState.x1P:
					return "{/Symbol c}_{b}(1P)";

				case BottomiumState.Y2S:
					return "{/Symbol U}(2S)";

				case BottomiumState.x2P:
					return "{/Symbol c}_{b}(2P)";

				case BottomiumState.Y3S:
					return "{/Symbol U}(3S)";

				case BottomiumState.x3P:
					return "{/Symbol c}_{b}(3P)";

				default:
					throw new Exception("Invalid BottomiumState.");
			}
		}

		private static string GetDecayWidthTypeGnuplotCode(
			DecayWidthType type
			)
		{
			switch(type)
			{
				case DecayWidthType.GammaDamp:
					return "{/Symbol G}^{damp}_{nl}";

				case DecayWidthType.GammaDiss:
					return "{/Symbol G}^{diss}_{nl}";

				case DecayWidthType.GammaTot:
					return "{/Symbol G}^{tot}_{nl}";

				default:
					throw new Exception("Invalid DecayWidthType.");
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void AppendHeader_InMediumDecayWidth(
			StringBuilder plotFile
	)
		{
			plotFile.AppendLine("reset");
			plotFile.AppendLine("set terminal windows enhanced size 1000,800");
			plotFile.AppendLine();
			plotFile.AppendLine("set key spacing 1.1");
			plotFile.AppendLine();
			plotFile.AppendLine("set title \"" + InMediumDecayWidthPlottingTitle + "\"");
			plotFile.AppendLine("set xlabel \"T (MeV)\"");
			plotFile.AppendLine("set ylabel \"" + GetDecayWidthTypeGnuplotCode(DecayWidthType) + " (MeV)\"");
			plotFile.AppendLine();
			for(int i = 8; i > BottomiumStates.Length; i--)
			{
				plotFile.AppendLine(string.Format("unset linetype {0}", i));
			}
			plotFile.AppendLine("set linetype cycle " + BottomiumStates.Length);
		}

		private string InMediumDecayWidthPlottingTitle
		{
			get
			{
				return "In-medium decay widths " + GetDecayWidthTypeGnuplotCode(DecayWidthType)
					+ " for medium velocity |u| = " + MediumVelocity.ToUIString() + "c";
			}
		}
	}
}
