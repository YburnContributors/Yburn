using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Yburn.FileUtil.Tests
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
				  "parameterName1 = parameterValue1" + Environment.NewLine
				+ "parameterName2 = parameterValue2" + Environment.NewLine
				+ "parameterName3 = parameterValue3" + Environment.NewLine);
		}

		[TestMethod]
		public void SkipNullOrEmptyNames()
		{
			NameValuePairs.Add("parameterName1", "parameterValue1");
			NameValuePairs.Add("", "parameterValue3");
			NameValuePairs.Add("parameterName2", "parameterValue2");
			AssertGivesParaFileText(
				  "parameterName1 = parameterValue1" + Environment.NewLine
				+ "parameterName2 = parameterValue2" + Environment.NewLine);
		}

		[TestMethod]
		public void SkipNullOrEmptyValues()
		{
			NameValuePairs.Add("parameterName1", "parameterValue1");
			NameValuePairs.Add("parameterName3", "");
			NameValuePairs.Add("parameterName4", null);
			NameValuePairs.Add("parameterName2", "parameterValue2");
			AssertGivesParaFileText(
				  "parameterName1 = parameterValue1" + Environment.NewLine
				+ "parameterName2 = parameterValue2" + Environment.NewLine);
		}

		[TestMethod]
		public void GivenDifferentNameLengths_AlignEqualSigns_LeftAlignNamesAndValues()
		{
			NameValuePairs.Add("shortName", "parameterValue1");
			NameValuePairs.Add("middleName", "parameterValue2");
			NameValuePairs.Add("longestName", "parameterValue3");
			AssertGivesParaFileText(
				  "shortName   = parameterValue1" + Environment.NewLine
				+ "middleName  = parameterValue2" + Environment.NewLine
				+ "longestName = parameterValue3" + Environment.NewLine);
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
