using System;

namespace Yburn
{
	[Serializable]
	public class DeclineWhileBusyException : Exception
	{
		public DeclineWhileBusyException()
			: base()
		{
		}
	}

	[Serializable]
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