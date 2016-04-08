using System;

namespace Yburn.UI
{
	public class PlotRequestEventArgs : EventArgs
	{
		public PlotRequestEventArgs()
			: base()
		{
		}

		public string Samples;
		public string DataFileName;
	}

	public class AlphaPlotRequestEventArgs : PlotRequestEventArgs
	{
		public AlphaPlotRequestEventArgs()
			: base()
		{
		}

		public string RunningCouplingTypeSelection;
		public string MinEnergy;
		public string MaxEnergy;
	}

	public class PionGDFPlotRequestEventArgs : PlotRequestEventArgs
	{
		public PionGDFPlotRequestEventArgs()
			: base()
		{
		}

		public string EnergyScale;
	}

	public class PotentialPlotRequestEventArgs : PlotRequestEventArgs
	{
		public PotentialPlotRequestEventArgs()
			: base()
		{
		}

		public string AlphaSoft;
		public string ColorState;
		public string DebyeMass;
		public string MinRadius;
		public string MaxRadius;
		public string PotentialType;
		public string Sigma;
		public string SpinState;
		public string SpinCouplingRange;
		public string SpinCouplingStrength;
		public string Temperature;
	}
}