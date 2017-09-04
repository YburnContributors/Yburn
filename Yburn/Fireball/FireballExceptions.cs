using System;

namespace Yburn.Fireball
{
	[Serializable]
	public class InvalidFireballFieldTypeException : Exception
	{
		public InvalidFireballFieldTypeException(
			FireballFieldType type
			)
			: base("Invalid FireballFieldType: \"" + type.ToString() + "\"")
		{
		}
	}

	[Serializable]
	public class InvalidFireballFieldFunctionException : Exception
	{
		public InvalidFireballFieldFunctionException()
			: base("FireballFieldFunction is invalid.")
		{
		}
	}
}
