using System.Collections.Generic;
using System.IO;

namespace Yburn.Util
{
	/***********************************************************************************************
	* Instances of the class ParaFileReader may be used to read out a parameter file and save the
	* names and values of variables assigned by a "=" in pairs of two strings. Commentary may be
	* introced by a "#". Multiple values may be given by inserting a comma. I.e. the text
	*
	* #some commentary
	* Var1  =   0.5   Var2=1
	*
	* Var3=  -3e5   # more commentary
	* Var4 =4,   5.0  6
	*
	* yields the pairs ("Var1", "0.5"), ("Var2", "1"), ("Var3", "-3e5"), ("Var4", "4, 5.0").
	************************************************************************************************/

	public class ParaFileReader : FileReader
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void Read(
			string pathFile,
			out Dictionary<string, string> nameValuePairs
			)
		{
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));
			Read(allLines, out nameValuePairs);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected ParaFileReader()
			: base()
		{
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void Read(
			List<string> allLines,
			out Dictionary<string, string> nameValuePairs
			)
		{
			ParaFileReader reader = new ParaFileReader();

			reader.RemoveLinesWithoutEqualSign(allLines);
			reader.RemoveCommentaryLines(allLines);
			reader.RemoveCommentaryWithinLines(allLines);
			reader.RemoveTabsAndSpaces(allLines);
			reader.ExtractParameterNamesAndValues(allLines, out nameValuePairs);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void ExtractParameterNamesAndValues(
			List<string> allLines,
			out Dictionary<string, string> nameValuePairs
			)
		{
			nameValuePairs = new Dictionary<string, string>();
			foreach(string line in allLines)
			{
				string[] nameValuePair = line.Split(new char[] { '=' }, 2);
				nameValuePairs.Add(nameValuePair[0], nameValuePair[1]);
			}
		}
	}
}