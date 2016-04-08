using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yburn.Fireball.Tests
{
	[TestClass]
	public class TemperatureDecayWidthListTests
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowIfDecayWidthType_None()
		{
			TemperatureDecayWidthList.GetList(DummyFileName, DecayWidthType.None, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowIfPotentialTypes_Empty()
		{
			TemperatureDecayWidthList.GetList(DummyFileName, DecayWidthType.None, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowIfPotentialTypes_Null()
		{
			TemperatureDecayWidthList.GetList(DummyFileName, DecayWidthType.None, null);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly string DummyFileName = "Fireball.Tests.dll";
	}
}