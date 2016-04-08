﻿using System;

namespace Yburn.Fireball
{
	public class InvalidFireballFieldTypeException : Exception
	{
		public InvalidFireballFieldTypeException(
			FireballFieldType type
			)
			: base("Invalid FireballFieldType: \"" + type.ToString() + "\"")
		{
		}
	}

	public class InvalidFireballFieldFunctionException : Exception
	{
		public InvalidFireballFieldFunctionException()
			: base("FireballFieldFunction is invalid.")
		{
		}
	}
}