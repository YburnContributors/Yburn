using System.Collections.Generic;
using System.IO;

namespace Yburn.Util
{
	/***********************************************************************************************
	 * Instances of the class BatchFileReader read out a parameter file like ParaFileReader.
	 * Additionally one may include job-statements of the kind "Job = [JobTitle]", which may be used
	 * to launch processes. The data is returned in a list of dictionaries. Each dictionary contains
	 * name-value pairs of variables and a job-statment as the last entry.
	 ***********************************************************************************************/

	public class BatchFileReader : FileReader
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void Read(
			string pathFile,
			out List<Dictionary<string, string>> commandList
			)
		{
			List<string> allLines = new List<string>(File.ReadAllLines(pathFile));
			Read(allLines, out commandList);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		protected BatchFileReader()
			: base()
		{
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void Read(
			List<string> allLines,
			out List<Dictionary<string, string>> commandList
			)
		{
			BatchFileReader reader = new BatchFileReader();

			reader.RemoveLinesWithoutEqualSign(allLines);
			reader.RemoveCommentaryLines(allLines);
			reader.RemoveCommentaryWithinLines(allLines);
			reader.RemoveTabsAndSpaces(allLines);
			reader.ExtractCommands(allLines, out commandList);
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private void ExtractCommands(
			List<string> allLines,
			out List<Dictionary<string, string>> commandList
			)
		{
			commandList = new List<Dictionary<string, string>>();

			Dictionary<string, string> dummyNameValueDict = new Dictionary<string, string>();
			foreach(string line in allLines)
			{
				string[] nameValuePair = line.Split(new char[] { '=' }, 2);
				dummyNameValueDict.Add(nameValuePair[0], nameValuePair[1]);

				if(nameValuePair[0] == "Job")
				{
					commandList.Add(dummyNameValueDict);
					dummyNameValueDict = new Dictionary<string, string>();
				}
			}
		}
	}
}