using System;

namespace Yburn
{
	public class DeclineWhileBusyException : Exception
	{
		public DeclineWhileBusyException()
			: base()
		{
		}
	}

	public class InvalidOutputPathException : Exception
	{
		public InvalidOutputPathException()
			: base(InvalidOutputPathMessage)
		{
		}

		private static string InvalidOutputPathMessage
		{
			get
			{
				return string.Format("\r\nThe path \"{0}\" is not accessible. Set a new output path.\r\n",
					YburnConfigFile.OutputPath);
			}
		}
	}
}