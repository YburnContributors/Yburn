﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Yburn.Fireball
{
	public class Fireball : IDisposable
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
			CurrentTime = Param.ThermalTimeFm;
			TimeStep = CurrentTime * TimeFactor; //  = 3 * DeltaT(tF)/T(tF) * tF

			AssertValidMembers();

			GlauberCalculation = new GlauberCalculation(Param);

			InitXY();
			InitV();
			InitTemperature();
			InitDecayWidth();
			InitDampingFactor();
		}

		~Fireball()
		{
			Dispose(false);
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		// QGP lifetime in the center of a central collision in fm/c for purely longitudinal expansion
		public double BjorkenLifeTime
		{
			get
			{
				return Param.ThermalTimeFm * Math.Pow(Param.InitialCentralTemperatureMeV
					/ Param.MinimalCentralTemperatureMeV, 3);
			}
		}

		// time as measured in the longitudinally commoving frame,
		// CurrentTime = Param.FormationTimesFm, initially
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

		public double CentralTemperature
		{
			get
			{
                if (Param.CollisionType == CollisionType.WoodsSaxonAWoodsSaxonB)
                {
                    return Temperature.Values[0, 0];
                }
                else if (Param.CollisionType == CollisionType.WoodsSaxonAGaussianB)
                {
                    // include a factor of 2 because only a half has been integrated
                    return Temperature.GetMaxValue();
                }
                else
                {
                    throw new Exception("Invalid CollisionType.");
                }
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// advance the fireball fields in time by the amount dt (not necessarily small)
		public void Advance(
			double timeIntverval
			)
		{
			double endTime = CurrentTime + timeIntverval;
			while(CurrentTime < endTime)
			{
				SetTimeStep(endTime);
				CurrentTime += TimeStep;

				AdvanceFields();

				if(CentralTemperature <= Param.MinimalCentralTemperatureMeV && LifeTime == -1)
				{
					LifeTime = CurrentTime - TimeStep;
				}
			}
		}

		public string FieldsToString(
			string[] fieldNames,
			string states
			)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("#{0,7}{1,8}", "x", "y");

			List<SimpleFireballField> fields = new List<SimpleFireballField>();
			foreach(string fieldName in fieldNames)
			{
				if(IsPtDependent(fieldName))
				{
					for(int k = 0; k < Param.TransverseMomentaGeV.Length; k++)
					{
						foreach(BottomiumState state in Enum.GetValues(typeof(BottomiumState)))
						{
							if(states.Contains(state.ToString()))
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

			for(int i = 0; i < Param.NumberGridCellsInX; i++)
			{
				for(int j = 0; j < Param.NumberGridCellsInY; j++)
				{
					stringBuilder.AppendFormat("{0,8}{1,8}",
						X[i].ToString(),
						Y[j].ToString());
					for(int k = 0; k < fields.Count; k++)
					{
						stringBuilder.AppendFormat("{0,20}",
							fields[k].Values[i, j].ToString("G6"));
					}

					stringBuilder.AppendLine();
				}
			}

			return stringBuilder.ToString();
		}

		public double IntegrateFireballField(
			string fieldName,
			BottomiumState state = BottomiumState.Y1S,
			int pTindex = 0
			)
		{
			SimpleFireballField field = GetFireballField(fieldName, state, pTindex);

            if (Param.CollisionType == CollisionType.WoodsSaxonAWoodsSaxonB)
            {
                // include a factor of 4 because only a quarter has been integrated
                return 4 * Param.GridCellSizeFm * Param.GridCellSizeFm * field.TrapezoidalRuleSummedValues();
            }
            else if (Param.CollisionType == CollisionType.WoodsSaxonAGaussianB)
            {
                // include a factor of 2 because only a half has been integrated
                return 2 * Param.GridCellSizeFm * Param.GridCellSizeFm * field.TrapezoidalRuleSummedValues();
            }
            else
            {
                throw new Exception("Invalid CollisionType.");
            }
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
			if(Temperature.Values[0, 0] >= criticalTemperature)
			{
				CollisionInQGP += 0.5 * GlauberCalculation.Ncoll.Values[0, 0];
			}
			else
			{
				CollisionsInHadronicRegion += 0.5 * GlauberCalculation.Ncoll.Values[0, 0];
			}

			// edges
			for(int i = 0; i < Param.NumberGridCells; i++)
			{
				if(Temperature.Values[i, 0] >= criticalTemperature)
				{
					CollisionInQGP += GlauberCalculation.Ncoll.Values[i, 0];
				}
				else
				{
					CollisionsInHadronicRegion += GlauberCalculation.Ncoll.Values[i, 0];
				}
			}

			for(int j = 0; j < Param.NumberGridCells; j++)
			{
				if(Temperature.Values[0, j] >= criticalTemperature)
				{
					CollisionInQGP += GlauberCalculation.Ncoll.Values[0, j];
				}
				else
				{
					CollisionsInHadronicRegion += GlauberCalculation.Ncoll.Values[0, j];
				}
			}

			CollisionInQGP *= 0.5;
			CollisionsInHadronicRegion *= 0.5;

			// the rest
			for(int i = 1; i < Param.NumberGridCells; i++)
			{
				for(int j = 1; j < Param.NumberGridCells; j++)
				{
					if(Temperature.Values[i, j] >= criticalTemperature)
					{
						CollisionInQGP += GlauberCalculation.Ncoll.Values[i, j];
					}
					else
					{
						CollisionsInHadronicRegion += GlauberCalculation.Ncoll.Values[i, j];
					}
				}
			}

            if (Param.CollisionType == CollisionType.WoodsSaxonAWoodsSaxonB)
            {
                // include a factor of 4 because only a quarter has been integrated
                CollisionInQGP *= 4 * Param.GridCellSizeFm * Param.GridCellSizeFm;
                CollisionsInHadronicRegion *= 4 * Param.GridCellSizeFm * Param.GridCellSizeFm;
            }
            else if (Param.CollisionType == CollisionType.WoodsSaxonAGaussianB)
            {
                // include a factor of 2 because only a half has been integrated
                CollisionInQGP *= 2 * Param.GridCellSizeFm * Param.GridCellSizeFm;
                CollisionsInHadronicRegion *= 2 * Param.GridCellSizeFm * Param.GridCellSizeFm;
            }
            else
            {
                throw new Exception("Invalid CollisionType.");
            }
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

		private static bool IsPtDependent(
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

		// Track whether Dispose has been called.
		private bool Disposed = false;

		private FireballParam Param;

		private GlauberCalculation GlauberCalculation;

		// Solver for the Euler equations for a relativistic perfect fluid
		private Ftexs Solver;

		private FireballTemperature Temperature;

		private FireballDecayWidth DecayWidth;

		private StateSpecificFireballField DampingFactor;

		private double TimeFactor;

		private double TimeStep;

		// x,y are in the plane perpendicular to the symmetry axis. origin is in the middle between
		// the two center of the nuclei. the x-axis is in the plane that the beam axis spans with the line connecting the two centers
		private double[] X;

		private double[] Y;

		// tranverse expansion velocity of the fireball as measured in the lab frame
		private SimpleFireballField VX;

		private SimpleFireballField VY;

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

			if(Param.GridCellSizeFm <= 0)
			{
				throw new Exception("GridCellSize <= 0.");
			}

			if(Param.NumberGridCells <= 0)
			{
				throw new Exception("NumberGridCells <= 0.");
			}

			if(Param.ImpactParamFm < 0)
			{
				throw new Exception("ImpactParam < 0.");
			}

			if(Param.FormationTimesFm.Length != NumberBottomiumStates)
			{
				throw new Exception("FormationTime-array has invalid size.");
			}

			foreach(double time in Param.FormationTimesFm)
			{
				if(time <= 0)
				{
					throw new Exception("All FormationTimes must be larger than zero.");
				}
			}

			if(Param.InitialCentralTemperatureMeV <= 0)
			{
				throw new Exception("InitialCentralTemperature <= 0.");
			}

			if(Param.MinimalCentralTemperatureMeV <= 0)
			{
				throw new Exception("MinimalCentralTemperature <= 0.");
			}

			if(Param.BeamRapidity < 0)
			{
				throw new Exception("BeamRapidity < 0.");
			}

			if(Param.TransverseMomentaGeV.Length == 0)
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
			DecayWidth = new FireballDecayWidth(X,Y,
				Param.GridCellSizeFm, Param.TransverseMomentaGeV, Temperature, VX, VY,
				Param.FormationTimesFm, CurrentTime, Param.DecayWidthEvaluationType,
				Param.DecayWidthAveragingAngles, Param.TemperatureDecayWidthList);
		}

		private void AdvanceFields()
		{
			if(Param.ExpansionMode == ExpansionMode.Transverse)
			{
				Solver.SolveUntil(CurrentTime);
				VX.Values = Solver.VX;
				VY.Values = Solver.VY;
				Temperature.Advance(Solver);
			}
			else
			{
				Temperature.Advance(CurrentTime);
			}

			DecayWidth.Advance(CurrentTime);
			DampingFactor.SetValues((i, j, k, l) =>
			{
				return DampingFactor.Values[k, l][i, j]
					* Math.Exp(-TimeStep * DecayWidth.Values[k, l][i, j] / PhysConst.HBARC);
			});
		}

		private void InitDampingFactor()
		{
			DampingFactor = new StateSpecificFireballField(FireballFieldType.DampingFactor,
				Param.NumberGridCellsInX, Param.NumberGridCellsInY, Param.TransverseMomentaGeV.Length,
				(i, j, k, l) =>
				{
					return 1;
				});
		}

		private void InitXY()
		{
			X = new double[Param.NumberGridCellsInX];
			Y = new double[Param.NumberGridCellsInY];

            if (Param.CollisionType == CollisionType.WoodsSaxonAWoodsSaxonB)
            {
                for (int j = 0; j < Param.NumberGridCellsInX; j++)
                {
                    X[j] = Y[j] = Param.GridCellSizeFm * j;
                }
            }
            else if (Param.CollisionType == CollisionType.WoodsSaxonAGaussianB)
            {
                for (int j = 0; j < Param.NumberGridCellsInY; j++)
                {
                    Y[j] = Param.GridCellSizeFm * j;
                }

                for (int j = 0; j < Param.NumberGridCellsInX; j++)
                {
                    X[j] = Param.GridCellSizeFm * (j + 1 - Param.NumberGridCells);
                }
            }
            else
            {
                throw new Exception("Invalid CollisionType.");
            }
        }

		private void InitTemperature()
		{
			Temperature = new FireballTemperature(Param.NumberGridCellsInX, Param.NumberGridCellsInY,
				GlauberCalculation.TemperatureScalingField, Param.InitialCentralTemperatureMeV,
				Param.ThermalTimeFm, CurrentTime);

			if(Param.ExpansionMode == ExpansionMode.Transverse)
			{
				Solver = new Ftexs(Param.GridCellSizeFm, CurrentTime, 0.25,
					Temperature.Values, VX.Values, VY.Values, 0, Param.FtexsLogPathFile);
			}

			Param.InitialCentralTemperatureMeV = CentralTemperature;
		}

		private void InitV()
		{
			VX = new SimpleFireballField(FireballFieldType.VX,
				Param.NumberGridCellsInX, Param.NumberGridCellsInY, (i, j) =>
				{
					return 0;
				});

			VY = new SimpleFireballField(FireballFieldType.VY,
				Param.NumberGridCellsInX, Param.NumberGridCellsInY, (i, j) =>
				{
					return 0;
				});
		}

		private SimpleFireballField GetFireballField(
			string fieldName,
			BottomiumState state = BottomiumState.Y1S,
			int pTindex = 0
			)
		{
			if(pTindex >= Param.TransverseMomentaGeV.Length || pTindex < 0)
			{
				throw new Exception("pTindex is invalid.");
			}

			switch(fieldName)
			{
				case "ColumnDensityA":
					return GlauberCalculation.ColumnDensityA;

				case "ColumnDensityB":
					return GlauberCalculation.ColumnDensityB;

				case "DampingFactor":
					return DampingFactor.GetSimpleFireballField(pTindex, state);

				case "DecayWidth":
					return DecayWidth.GetSimpleFireballField(pTindex, state);

				case "NucleonDensityA":
					return GlauberCalculation.NucleonDensityA;

				case "NucleonDensityB":
					return GlauberCalculation.NucleonDensityB;

				case "Ncoll":
					return GlauberCalculation.Ncoll;

				case "Npart":
					return GlauberCalculation.Npart;

				case "Overlap":
					return GlauberCalculation.Overlap;

				case "Temperature":
					return Temperature;

				case "VX":
					return VX;

				case "VY":
					return VY;

				case "UnscaledSuppression":
					return GetUnscaledSuppression(state, pTindex);

				default:
					throw new Exception("Unknown FireballField.");
			}
		}

		private SimpleFireballField GetUnscaledSuppression(
			BottomiumState state,
			int pTindex
			)
		{
			return new SimpleFireballField(FireballFieldType.UnscaledSuppression,
				Param.NumberGridCellsInX, Param.NumberGridCellsInY, (i, j) =>
				{
					return DampingFactor.Values[pTindex, (int)state][i, j]
						* GlauberCalculation.Overlap.Values[i, j];
				});
		}

		private void SetTimeStep(
			double endTime
			)
		{
			TimeStep = Math.Min(endTime - CurrentTime, CurrentTime * TimeFactor);
		}

		protected virtual void Dispose(
			bool isCalledFromUserCode
			)
		{
			if(!Disposed)
			{
				if(isCalledFromUserCode)
				{
					DisposeManagedResources();
				}

				Disposed = true;
			}
		}

		private void DisposeManagedResources()
		{
			if(Solver != null)
			{
				Solver.Dispose();
			}
		}
	}
}