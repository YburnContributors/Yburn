using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Yburn.Util.Tests
{
	[TestClass]
	public class ParaFileWriterTests
	{
		/********************************************************************************************
         * Public members, functions and properties
         ********************************************************************************************/

		[TestInitialize]
		public void TestInitialize()
		{
			NameValuePairs = new Dictionary<string, string>();
		}

		[TestMethod]
		public void GivenNullOrEmpty_ReturnEmptyString()
		{
			NameValuePairs = null;
			AssertGivesParaFileText("");

			NameValuePairs = new Dictionary<string, string>();
			AssertGivesParaFileText("");
		}

		[TestMethod]
		public void GivenValidValues_FormNameValuePairs()
		{
			NameValuePairs.Add("parameterName1", "parameterValue1");
			NameValuePairs.Add("parameterName2", "parameterValue2");
			NameValuePairs.Add("parameterName3", "parameterValue3");
			AssertGivesParaFileText(
				  "parameterName1 = parameterValue1\r\n"
				+ "parameterName2 = parameterValue2\r\n"
				+ "parameterName3 = parameterValue3\r\n");
		}

		[TestMethod]
		public void SkipNullOrEmptyNames()
		{
			NameValuePairs.Add("parameterName1", "parameterValue1");
			NameValuePairs.Add("", "parameterValue3");
			NameValuePairs.Add("parameterName2", "parameterValue2");
			AssertGivesParaFileText(
				  "parameterName1 = parameterValue1\r\n"
				+ "parameterName2 = parameterValue2\r\n");
		}

		[TestMethod]
		public void SkipNullOrEmptyValues()
		{
			NameValuePairs.Add("parameterName1", "parameterValue1");
			NameValuePairs.Add("parameterName3", "");
			NameValuePairs.Add("parameterName4", null);
			NameValuePairs.Add("parameterName2", "parameterValue2");
			AssertGivesParaFileText(
				  "parameterName1 = parameterValue1\r\n"
				+ "parameterName2 = parameterValue2\r\n");
		}

		[TestMethod]
		public void GivenDifferentNameLengths_AlignEqualSigns_LeftAlignNamesAndValues()
		{
			NameValuePairs.Add("shortName", "parameterValue1");
			NameValuePairs.Add("middleName", "parameterValue2");
			NameValuePairs.Add("longestName", "parameterValue3");
			AssertGivesParaFileText(
				  "shortName   = parameterValue1\r\n"
				+ "middleName  = parameterValue2\r\n"
				+ "longestName = parameterValue3\r\n");
		}

		/********************************************************************************************
         * Private/protected static members, functions and properties
         ********************************************************************************************/

		private Dictionary<string, string> NameValuePairs;

		private void AssertGivesParaFileText(
			string expectedText
			)
		{
			Assert.AreEqual(expectedText, ParaFileWriter.GetParaFileText(NameValuePairs));
		}
	}
}