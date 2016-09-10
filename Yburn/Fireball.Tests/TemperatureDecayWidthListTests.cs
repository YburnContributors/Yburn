using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
			TemperatureDecayWidthListHelper.GetList(DummyFileName, DecayWidthType.None, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowIfPotentialTypes_Empty()
		{
			TemperatureDecayWidthListHelper.GetList(DummyFileName, DecayWidthType.None, null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ThrowIfPotentialTypes_Null()
		{
			TemperatureDecayWidthListHelper.GetList(DummyFileName, DecayWidthType.None, null);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static readonly string DummyFileName = "Fireball.Tests.dll";
	}
}