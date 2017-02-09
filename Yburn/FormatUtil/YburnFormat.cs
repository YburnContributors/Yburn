﻿using System.Globalization;
using System.Threading;

namespace Yburn.FormatUtil
{
	public static class YburnFormat
	{
		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		public static readonly CultureInfo YburnCulture = CultureInfo.InvariantCulture;

		public static void UseYburnFormat()
		{
			Thread.CurrentThread.CurrentCulture = YburnCulture;
		}
	}
}
