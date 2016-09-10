using System.Collections.Generic;

namespace Yburn
{
	partial class Worker
	{
		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/
		public Dictionary<string, string> VariableNameValuePairs
		{
			get
			{
				return GetVariableNameValuePairs();
			}
			set
			{
				SetVariableNameValuePairs(value ?? new Dictionary<string, string>());
			}
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected string DataFileName = "stdout.txt";
	}
}