using System.Collections.Generic;
using System.IO;

namespace Yburn.TestUtil
{
	public class FileCleaner
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static void Clean(
			params string[] fileNames
			)
		{
			List<string> filesToDelete = new List<string>(fileNames);
			DeleteAll(filesToDelete);
		}

		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FileCleaner()
		{
			FilesToDelete = new List<string>();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void MarkForDelete(
			string fileName
			)
		{
			FilesToDelete.Add(fileName);
		}

		public void Clean()
		{
			DeleteAll(FilesToDelete);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		private static void DeleteAll(
			List<string> fileNames
			)
		{
			foreach(string fileName in fileNames)
			{
				DeleteIfExists(fileName);
			}
		}

		private static void DeleteIfExists(
			string fileName
			)
		{
			if(File.Exists(fileName))
			{
				File.Delete(fileName);
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		private List<string> FilesToDelete;
	}
}