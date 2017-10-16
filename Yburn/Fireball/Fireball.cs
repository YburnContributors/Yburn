using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

			InitCoordinateSystem();
			InitV();
			InitElectromagneticField();
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
				return Temperature.MaximumTemperature;
			}
		}

		public double QGPConductivity
		{
			get
			{
				return Param.QGPConductivity_MeV
					* (MaximumTemperature / Param.QGPFormationTemperature_MeV);
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
			List<FireballFieldType> fieldTypes,
			List<BottomiumState> states
			)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("#{0,7}{1,8}", "x", "y");

			List<SimpleFireballField> fields = new List<SimpleFireballField>();
			foreach(FireballFieldType fieldType in fieldTypes)
			{
				if(IsPTDependent(fieldType))
				{
					for(int k = 0; k < Param.TransverseMomenta_GeV.Count; k++)
					{
						foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
						{
							if(states.Contains(state))
							{
								fields.Add(GetFireballField(fieldType, state, k));
								stringBuilder.AppendFormat("{0,30}", string.Format("{0}({1}, {2})",
									fieldType, state, Param.TransverseMomenta_GeV[k]));
							}
						}
					}
				}
				else
				{
					fields.Add(GetFireballField(fieldType));
					stringBuilder.AppendFormat("{0,30}", fieldType);
				}
			}

			stringBuilder.AppendLine();

			if(fields.Count == 0)
			{
				throw new Exception("No output fields are given.");
			}

			for(int i = 0; i < XAxis.Count; i++)
			{
				for(int j = 0; j < YAxis.Count; j++)
				{
					stringBuilder.AppendFormat("{0,8}{1,8}", XAxis[i], YAxis[j]);
					for(int k = 0; k < fields.Count; k++)
					{
						stringBuilder.AppendFormat("{0,30:G6}", fields[k][i, j]);
					}

					stringBuilder.AppendLine();
				}
			}

			return stringBuilder.ToString();
		}

		public double IntegrateFireballField(
			FireballFieldType fieldType,
			BottomiumState state = BottomiumState.Y1S,
			int pTIndex = 0
			)
		{
			SimpleFireballField field = GetFireballField(fieldType, state, pTIndex);

			return field.IntegrateValues();
		}

		// Calculates the number of binary collisions NcollQGP that occur in the transverse plane
		// where T >= Tcrit and NcollPion in the hadronic medium surrounding it (T < Tcrit),
		// respectively, for the current impact parameter.
		public void CalculateNumberCollisions(
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
				CollisionInQGP += 0.5 * GlauberCalculation.NumberCollisionsField[0, 0];
			}
			else
			{
				CollisionsInHadronicRegion += 0.5 * GlauberCalculation.NumberCollisionsField[0, 0];
			}

			// edges
			for(int i = 1; i < XAxis.Count; i++)
			{
				if(Temperature[i, 0] >= criticalTemperature)
				{
					CollisionInQGP += GlauberCalculation.NumberCollisionsField[i, 0];
				}
				else
				{
					CollisionsInHadronicRegion += GlauberCalculation.NumberCollisionsField[i, 0];
				}
			}

			for(int j = 1; j < YAxis.Count; j++)
			{
				if(Temperature[0, j] >= criticalTemperature)
				{
					CollisionInQGP += GlauberCalculation.NumberCollisionsField[0, j];
				}
				else
				{
					CollisionsInHadronicRegion += GlauberCalculation.NumberCollisionsField[0, j];
				}
			}

			CollisionInQGP *= 0.5;
			CollisionsInHadronicRegion *= 0.5;

			// the rest
			for(int i = 1; i < XAxis.Count; i++)
			{
				for(int j = 1; j < YAxis.Count; j++)
				{
					if(Temperature[i, j] >= criticalTemperature)
					{
						CollisionInQGP += GlauberCalculation.NumberCollisionsField[i, j];
					}
					else
					{
						CollisionsInHadronicRegion += GlauberCalculation.NumberCollisionsField[i, j];
					}
				}
			}

			CollisionInQGP
				*= CoordinateSystem.SymmetryFactor * Param.GridCellSize_fm * Param.GridCellSize_fm;
			CollisionsInHadronicRegion
				*= CoordinateSystem.SymmetryFactor * Param.GridCellSize_fm * Param.GridCellSize_fm;
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
			FireballFieldType fieldType
			)
		{
			return fieldType == FireballFieldType.DampingFactor
				|| fieldType == FireballFieldType.DecayWidth
				|| fieldType == FireballFieldType.UnscaledSuppression;
		}

		private ReadOnlyCollection<double> XAxis
		{
			get
			{
				return CoordinateSystem.XAxis;
			}
		}

		private ReadOnlyCollection<double> YAxis
		{
			get
			{
				return CoordinateSystem.YAxis;
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private FireballParam Param;

		private CoordinateSystem CoordinateSystem;

		private GlauberCalculation GlauberCalculation;

		// Solver for the Euler equations for a relativistic perfect fluid
		private Ftexs Solver;

		private FireballTemperatureField Temperature;

		private FireballDecayWidthField DecayWidth;

		private StateSpecificFireballField DampingFactor;

		private double TimeFactor;

		private double TimeStep;

		// tranverse expansion velocity of the fireball as measured in the lab frame
		private SimpleFireballField VelocityX;

		private SimpleFireballField VelocityY;

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

			if(Param.TransverseMomenta_GeV.Count == 0)
			{
				throw new Exception("TransverseMomenta-array is empty.");
			}

			foreach(double momentum in Param.TransverseMomenta_GeV)
			{
				if(momentum < 0)
				{
					throw new Exception("All TransverseMomenta must be larger than zero.");
				}
			}
		}

		private void InitCoordinateSystem()
		{
			CoordinateSystem = new CoordinateSystem(Param);
		}

		private void InitDecayWidth()
		{
			DecayWidth = new FireballDecayWidthField(
				CoordinateSystem,
				Param.TransverseMomenta_GeV,
				Temperature,
				VelocityX,
				VelocityY,
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
				VelocityX.SetValues(Solver.VX);
				VelocityY.SetValues(Solver.VY);
				Temperature.Advance(Solver);
			}
			else
			{
				Temperature.Advance(CurrentTime);
			}

			if(Param.UseElectricField)
			{
				ElectricField.Advance(CurrentTime, QGPConductivity);
			}
			if(Param.UseMagneticField)
			{
				MagneticField.Advance(CurrentTime, QGPConductivity);
			}

			DecayWidth.Advance(CurrentTime);
			DampingFactor.SetValues((x, y, pT, state) => DampingFactor[x, y, pT, state]
				* Math.Exp(-TimeStep * DecayWidth[x, y, pT, state] / Constants.HbarC_MeV_fm));
		}

		private void InitDampingFactor()
		{
			DampingFactor = new StateSpecificFireballField(
				FireballFieldType.DampingFactor,
				CoordinateSystem,
				Param.TransverseMomenta_GeV,
				(x, y, pT, state) => 1);
		}

		private void InitV()
		{
			VelocityX = new SimpleFireballField(FireballFieldType.VelocityX, CoordinateSystem);
			VelocityY = new SimpleFireballField(FireballFieldType.VelocityY, CoordinateSystem);
		}

		private void InitElectromagneticField()
		{
			if(Param.UseElectricField)
			{
				ElectricField = FireballElectromagneticField.CreateFireballElectricField(Param);
			}
			else
			{
				ElectricField = FireballElectromagneticField.CreateZeroField(
					FireballFieldType.ElectricFieldStrength, CoordinateSystem);
			}

			if(Param.UseMagneticField)
			{
				MagneticField = FireballElectromagneticField.CreateFireballMagneticField(Param);
			}
			else
			{
				MagneticField = FireballElectromagneticField.CreateZeroField(
					FireballFieldType.MagneticFieldStrength, CoordinateSystem);
			}
		}

		private void InitTemperature()
		{
			Temperature = new FireballTemperatureField(
				CoordinateSystem,
				GlauberCalculation.TemperatureScalingField,
				Param.InitialMaximumTemperature_MeV,
				Param.ThermalTime_fm,
				CurrentTime);

			if(Param.ExpansionMode == ExpansionMode.Transverse)
			{
				Solver = new Ftexs(Param.GridCellSize_fm, CurrentTime, 0.25,
					Temperature.GetValues(), VelocityX.GetValues(), VelocityY.GetValues());
			}

			Param.InitialMaximumTemperature_MeV = MaximumTemperature;
		}

		private SimpleFireballField GetFireballField(
			FireballFieldType fieldType,
			BottomiumState state = BottomiumState.Y1S,
			int pTIndex = 0
			)
		{
			if(pTIndex >= Param.TransverseMomenta_GeV.Count || pTIndex < 0)
			{
				throw new Exception("pTIndex is invalid.");
			}

			switch(fieldType)
			{
				case FireballFieldType.ColumnDensityA:
					return GlauberCalculation.NucleonNumberColumnDensityFieldA;

				case FireballFieldType.ColumnDensityB:
					return GlauberCalculation.NucleonNumberColumnDensityFieldB;

				case FireballFieldType.DampingFactor:
					return DampingFactor.GetSimpleFireballField(pTIndex, state);

				case FireballFieldType.DecayWidth:
					return DecayWidth.GetSimpleFireballField(pTIndex, state);

				case FireballFieldType.NucleonDensityA:
					return GlauberCalculation.NucleonNumberDensityFieldA;

				case FireballFieldType.NucleonDensityB:
					return GlauberCalculation.NucleonNumberDensityFieldB;

				case FireballFieldType.NumberCollisions:
					return GlauberCalculation.NumberCollisionsField;

				case FireballFieldType.NumberParticipants:
					return GlauberCalculation.NumberParticipantsField;

				case FireballFieldType.Overlap:
					return GlauberCalculation.OverlapField;

				case FireballFieldType.Temperature:
					return Temperature;

				case FireballFieldType.TemperatureScaling:
					return GlauberCalculation.TemperatureScalingField;

				case FireballFieldType.TemperatureNormalization:
					return Temperature.TemperatureNormalizationField;

				case FireballFieldType.VelocityX:
					return VelocityX;

				case FireballFieldType.VelocityY:
					return VelocityY;

				case FireballFieldType.UnscaledSuppression:
					return GetUnscaledSuppression(state, pTIndex);

				case FireballFieldType.ElectricFieldStrength:
					return ElectricField;

				case FireballFieldType.MagneticFieldStrength:
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
				CoordinateSystem,
				(x, y) => DampingFactor[x, y, pTIndex, (int)state]
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
