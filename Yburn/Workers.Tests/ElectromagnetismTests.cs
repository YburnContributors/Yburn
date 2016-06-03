using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Yburn.Workers.Tests
{
	[TestClass]
	public class ElectromagnetismTests : Electromagnetism
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		[TestMethod]
		[ExpectedException(typeof(System.NotImplementedException))]
		public void CalculateAverageMagneticFieldStrengthTest()
		{
			VariableNameValuePairs = GetElectromagnetismVariables();
			CalculateAverageMagneticFieldStrength();
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static Dictionary<string, string> GetElectromagnetismVariables()
		{
			Dictionary<string, string> nameValuePairs = new Dictionary<string, string>();
			nameValuePairs.Add("DiffusenessA", "0.546");
			nameValuePairs.Add("DiffusenessB", "0.546");
			nameValuePairs.Add("NucleonNumberA", "208");
			nameValuePairs.Add("NucleonNumberB", "208");
			nameValuePairs.Add("NuclearRadiusA", "6.62");
			nameValuePairs.Add("NuclearRadiusB", "6.62");
			nameValuePairs.Add("FormationTimes", "0.3,0.3,0.3,0.3,0.3,0.3");
			nameValuePairs.Add("GridCellSize", "1");
			nameValuePairs.Add("GridRadius", "10");
			nameValuePairs.Add("ShapeFunctionTypeA", "WoodsSaxonPotential");
			nameValuePairs.Add("ShapeFunctionTypeB", "WoodsSaxonPotential");

			return nameValuePairs;
		}
	}
}
