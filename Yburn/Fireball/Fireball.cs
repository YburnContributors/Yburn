using System;
using System.Collections.Generic;
using System.Text;
using Yburn.PhysUtil;

namespace Yburn.Fireball
{
	public class Fireball
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public Fireball(
			FireballParam param
			)
		{
			Param = param.Clone();
			LifeTime = -1;

			// The initial step should change the temperature by 1/3 percent for pure Bjorken flow
			// (which is the case in the initial stages)
			TimeFactor = 1e-2;
			CurrentTime = Param.ThermalTime_fm;
			TimeStep = CurrentTime * TimeFactor; //  = 3 * DeltaT(tF)/T(tF) * tF

			AssertValidMembers();

			GlauberCalculation = new GlauberCalculation(Param);

			InitXY();
			InitV();
			InitElectromagneticFields();
			InitTemperature();
			InitDecayWidth();
			InitDampingFactor();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// QGP lifetime in the center of a central collision in fm/c for purely longitudinal expansion
		public double BjorkenLifeTime
		{
			get
			{
				return Param.ThermalTime_fm * Math.Pow(Param.InitialMaximumTemperature_MeV
					/ Param.BreakupTemperature_MeV, 3);
			}
		}

		// time as measured in the longitudinally commoving frame,
		// CurrentTime = Param.FormationTimes_fm, initially
		public double CurrentTime
		{
			get;
			private set;
		}

		// QGP lifetime in the center of a central collision in fm/c
		public double LifeTime
		{
			get;
			private set;
		}

		public double MaximumTemperature
		{
			get
			{
				if(Param.IsSystemSymmetricInY)
				{
					if(Param.IsSystemSymmetricInX)
					{
						return Temperature[0, 0];
					}
					else
					{
						return Temperature.GetMaxValueForFixedY(0);
					}
				}
				else
				{
					return Temperature.GetMaxValue();
				}
			}
		}

		// advance the fireball fields in time by the amount dt (not necessarily small)
		public void Advance(
			double timeInterval
			)
		{
			double endTime = CurrentTime + timeInterval;
			while(CurrentTime < endTime)
			{
				SetTimeStep(endTime);
				CurrentTime += TimeStep;

				AdvanceFields();

				if(MaximumTemperature <= Param.BreakupTemperature_MeV && LifeTime == -1)
				{
					LifeTime = CurrentTime - TimeStep;
				}
			}
		}

		public string FieldsToString(
			List<string> fieldNames,
			List<BottomiumState> states
			)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("#{0,7}{1,8}", "x", "y");

			List<SimpleFireballField> fields = new List<SimpleFireballField>();
			foreach(string fieldName in fieldNames)
			{
				if(IsPTDependent(fieldName))
				{
					for(int k = 0; k < Param.TransverseMomentaGeV.Count; k++)
					{
						foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
						{
							if(states.Contains(state))
							{
								fields.Add(GetFireballField(fieldName, state, k));
								stringBuilder.AppendFormat("{0,20}",
									fieldName + "(" + state.ToString() + ", "
									+ Param.TransverseMomentaGeV[k].ToString() + ")");
							}
						}
					}
				}
				else
				{
					fields.Add(GetFireballField(fieldName));
					stringBuilder.AppendFormat("{0,20}", fieldName.ToString());
				}
			}

			stringBuilder.AppendLine();

			if(fields.Count == 0)
			{
				throw new Exception("No output fields are given.");
			}

			for(int i = 0; i < X.Length; i++)
			{
				for(int j = 0; j < Y.Length; j++)
				{
					stringBuilder.AppendFormat("{0,8}{1,8}",
						X[i].ToString(),
						Y[j].ToString());
					for(int k = 0; k < fields.Count; k++)
					{
						stringBuilder.AppendFormat("{0,20}",
							fields[k][i, j].ToString("G6"));
					}

					stringBuilder.AppendLine();
				}
			}

			return stringBuilder.ToString();
		}

		public double IntegrateFireballField(
			string fieldName,
			BottomiumState state = BottomiumState.Y1S,
			int pTIndex = 0
			)
		{
			SimpleFireballField field = GetFireballField(fieldName, state, pTIndex);

			// include a factor of 4 because only a quarter has been integrated
			return Param.SystemSymmetryFactor * Param.GridCellSize_fm * Param.GridCellSize_fm
				* field.TrapezoidalRuleSummedValues();
		}

		// Calculates the number of binary collisions NcollQGP that occur in the transverse plane
		// where T >= Tcrit and NcollPion in the hadronic medium surrounding it (T < Tcrit),
		// respectively, for the current impact parameter.
		public void CalculateNcolls(
			double criticalTemperature,
			out double CollisionInQGP,
			out double CollisionsInHadronicRegion
			)
		{
			CollisionInQGP = 0;
			CollisionsInHadronicRegion = 0;

			// center point
			if(Temperature[0, 0] >= criticalTemperature)
			{
				CollisionInQGP += 0.5 * GlauberCalculation.NcollField[0, 0];
			}
			else
			{
				CollisionsInHadronicRegion += 0.5 * GlauberCalculation.NcollField[0, 0];
			}

			// edges
			for(int i = 1; i < X.Length; i++)
			{
				if(Temperature[i, 0] >= criticalTemperature)
				{
					CollisionInQGP += GlauberCalculation.NcollField[i, 0];
				}
				else
				{
					CollisionsInHadronicRegion += GlauberCalculation.NcollField[i, 0];
				}
			}

			for(int j = 1; j < Y.Length; j++)
			{
				if(Temperature[0, j] >= criticalTemperature)
				{
					CollisionInQGP += GlauberCalculation.NcollField[0, j];
				}
				else
				{
					CollisionsInHadronicRegion += GlauberCalculation.NcollField[0, j];
				}
			}

			CollisionInQGP *= 0.5;
			CollisionsInHadronicRegion *= 0.5;

			// the rest
			for(int i = 1; i < X.Length; i++)
			{
				for(int j = 1; j < Y.Length; j++)
				{
					if(Temperature[i, j] >= criticalTemperature)
					{
						CollisionInQGP += GlauberCalculation.NcollField[i, j];
					}
					else
					{
						CollisionsInHadronicRegion += GlauberCalculation.NcollField[i, j];
					}
				}
			}

			CollisionInQGP
				*= Param.SystemSymmetryFactor * Param.GridCellSize_fm * Param.GridCellSize_fm;
			CollisionsInHadronicRegion
				*= Param.SystemSymmetryFactor * Param.GridCellSize_fm * Param.GridCellSize_fm;
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static int NumberBottomiumStates
		{
			get
			{
				return Enum.GetValues(typeof(BottomiumState)).Length;
			}
		}

		private static bool IsPTDependent(
			string fieldName
			)
		{
			return fieldName == "DampingFactor"
				|| fieldName == "DecayWidth"
				|| fieldName == "UnscaledSuppression";
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;

		private GlauberCalculation GlauberCalculation;

		// Solver for the Euler equations for a relativistic perfect fluid
		private Ftexs Solver;

		private FireballTemperatureField Temperature;

		private FireballDecayWidthField DecayWidth;

		private StateSpecificFireballField DampingFactor;

		private double TimeFactor;

		private double TimeStep;

		// x, y are in the plane perpendicular to the symmetry axis. The origin is in the middle
		// between the two center of the nuclei. The x-axis is in the plane that the beam axis spans
		// with the line connecting the two centers.
		private double[] X;

		private double[] Y;

		// tranverse expansion velocity of the fireball as measured in the lab frame
		private SimpleFireballField VX;

		private SimpleFireballField VY;

		private FireballElectromagneticField ElectricField;

		private FireballElectromagneticField MagneticField;

		private void AssertValidMembers()
		{
			if(Param.NucleonNumberA <= 0)
			{
				throw new Exception("NucleonNumberA <= 0.");
			}

			if(Param.NucleonNumberB <= 0)
			{
				throw new Exception("NucleonNumberB <= 0.");
			}

			if(Param.GridCellSize_fm <= 0)
			{
				throw new Exception("GridCellSize <= 0.");
			}

			if(Param.GridRadius_fm <= 0)
			{
				throw new Exception("GridRadius <= 0.");
			}

			if(Param.ImpactParameter_fm < 0)
			{
				throw new Exception("ImpactParameter < 0.");
			}

			foreach(double time in Param.FormationTimes_fm.Values)
			{
				if(time <= 0)
				{
					throw new Exception("All FormationTimes must be larger than zero.");
				}
			}

			if(Param.InitialMaximumTemperature_MeV <= 0)
			{
				throw new Exception("InitialMaximumTemperature <= 0.");
			}

			if(Param.BreakupTemperature_MeV <= 0)
			{
				throw new Exception("BreakupTemperature <= 0.");
			}

			if(Param.TransverseMomentaGeV.Count == 0)
			{
				throw new Exception("TransverseMomenta-array is empty.");
			}

			foreach(double momentum in Param.TransverseMomentaGeV)
			{
				if(momentum < 0)
				{
					throw new Exception("All TransverseMomenta must be larger than zero.");
				}
			}
		}

		private void InitDecayWidth()
		{
			DecayWidth = new FireballDecayWidthField(
				X,
				Y,
				Param.TransverseMomentaGeV,
				Temperature,
				VX,
				VY,
				ElectricField,
				MagneticField,
				Param.FormationTimes_fm,
				CurrentTime,
				Param.DecayWidthRetrievalFunction);
		}

		private void AdvanceFields()
		{
			if(Param.ExpansionMode == ExpansionMode.Transverse)
			{
				Solver.SolveUntil(CurrentTime);
				VX.SetDiscreteValues(Solver.VX);
				VY.SetDiscreteValues(Solver.VY);
				Temperature.Advance(Solver);
			}
			else
			{
				Temperature.Advance(CurrentTime);
			}

			ElectricField.Advance(CurrentTime);
			MagneticField.Advance(CurrentTime);
			DecayWidth.Advance(CurrentTime);
			DampingFactor.SetValues((x, y, pT, state) => DampingFactor.Values[pT, state][x, y]
				* Math.Exp(-TimeStep * DecayWidth.Values[pT, state][x, y] / Constants.HbarC_MeV_fm));
		}

		private void InitDampingFactor()
		{
			DampingFactor = new StateSpecificFireballField(
				FireballFieldType.DampingFactor,
				X.Length,
				Y.Length,
				Param.TransverseMomentaGeV.Count,
				(x, y, pT, state) => 1);
		}

		private void InitXY()
		{
			X = Param.XAxis;
			Y = Param.YAxis;
		}

		private void InitV()
		{
			VX = new SimpleFireballField(
				FireballFieldType.VX,
				X.Length,
				Y.Length,
				(x, y) => 0);

			VY = new SimpleFireballField(
				FireballFieldType.VY,
				X.Length,
				Y.Length,
				(x, y) => 0);
		}

		private void InitElectromagneticFields()
		{
			if(Param.UseElectricField)
			{
				ElectricField = FireballElectromagneticField.CreateFireballElectricField(Param, X, Y);
			}
			else
			{
				ElectricField = FireballElectromagneticField.CreateZeroField(
					FireballFieldType.ElectricFieldStrength, X, Y);
			}

			if(Param.UseMagneticField)
			{
				MagneticField = FireballElectromagneticField.CreateFireballMagneticField(Param, X, Y);
			}
			else
			{
				MagneticField = FireballElectromagneticField.CreateZeroField(
					FireballFieldType.MagneticFieldStrength, X, Y);
			}
		}

		private void InitTemperature()
		{
			Temperature = new FireballTemperatureField(
				X.Length,
				Y.Length,
				GlauberCalculation.TemperatureScalingField,
				Param.InitialMaximumTemperature_MeV,
				Param.ThermalTime_fm,
				CurrentTime);

			if(Param.ExpansionMode == ExpansionMode.Transverse)
			{
				Solver = new Ftexs(Param.GridCellSize_fm, CurrentTime, 0.25,
					Temperature.GetDiscreteValues(), VX.GetDiscreteValues(), VY.GetDiscreteValues());
			}

			Param.InitialMaximumTemperature_MeV = MaximumTemperature;
		}

		private SimpleFireballField GetFireballField(
			string fieldName,
			BottomiumState state = BottomiumState.Y1S,
			int pTIndex = 0
			)
		{
			if(pTIndex >= Param.TransverseMomentaGeV.Count || pTIndex < 0)
			{
				throw new Exception("pTIndex is invalid.");
			}

			switch(fieldName)
			{
				case "ColumnDensityA":
					return GlauberCalculation.NucleonNumberColumnDensityFieldA;

				case "ColumnDensityB":
					return GlauberCalculation.NucleonNumberColumnDensityFieldB;

				case "DampingFactor":
					return DampingFactor.GetSimpleFireballField(pTIndex, state);

				case "DecayWidth":
					return DecayWidth.GetSimpleFireballField(pTIndex, state);

				case "NucleonDensityA":
					return GlauberCalculation.NucleonNumberDensityFieldA;

				case "NucleonDensityB":
					return GlauberCalculation.NucleonNumberDensityFieldB;

				case "Ncoll":
					return GlauberCalculation.NcollField;

				case "Npart":
					return GlauberCalculation.NpartField;

				case "Overlap":
					return GlauberCalculation.OverlapField;

				case "Temperature":
					return Temperature;

				case "TemperatureScalingField":
					return GlauberCalculation.TemperatureScalingField;

				case "VX":
					return VX;

				case "VY":
					return VY;

				case "UnscaledSuppression":
					return GetUnscaledSuppression(state, pTIndex);

				case "ElectricFieldStrength":
					return ElectricField;

				case "MagneticFieldStrength":
					return MagneticField;

				default:
					throw new Exception("Unknown FireballField.");
			}
		}

		private SimpleFireballField GetUnscaledSuppression(
			BottomiumState state,
			int pTIndex
			)
		{
			return new SimpleFireballField(
				FireballFieldType.UnscaledSuppression,
				X.Length,
				Y.Length,
				(x, y) => DampingFactor.Values[pTIndex, (int)state][x, y]
					* GlauberCalculation.OverlapField[x, y]);
		}

		private void SetTimeStep(
			double endTime
			)
		{
			TimeStep = Math.Min(endTime - CurrentTime, CurrentTime * TimeFactor);
		}
	}
}
