using System;
using System.Runtime.InteropServices;

namespace Yburn
{
	/********************************************************************************************
	 * Enums
	 ********************************************************************************************/

	[Flags]
	internal enum EXECUTION_STATE : uint
	{
		ES_AWAYMODE_REQUIRED = 0x00000040,
		ES_CONTINUOUS = 0x80000000,
		ES_DISPLAY_REQUIRED = 0x00000002,
		ES_SYSTEM_REQUIRED = 0x00000001
		// Legacy flag, should not be used.
		// ES_USER_PRESENT = 0x00000004
	}

	internal static class PowerManagement
	{
		/********************************************************************************************
		 * Internal static members, functions and properties
		 ********************************************************************************************/

		internal static void KeepSystemAwake()
		{
			SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS
				| EXECUTION_STATE.ES_SYSTEM_REQUIRED);
		}

		internal static void Reset()
		{
			SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
		}

		/********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
	}
}
