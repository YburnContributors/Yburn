using System;

namespace Yburn.Fireball
{
	public abstract class FireballField
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public FireballField(
			FireballFieldType type,
			int xDimension,
			int yDimension
			)
		{
			Type = type;
			XDimension = xDimension;
			YDimension = yDimension;

			AssertValidMembers();
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public FireballFieldType Type
		{
			get;
			private set;
		}

		/********************************************************************************************
		 * Private/protected members, functions and properties
		 ********************************************************************************************/

		protected int XDimension;

		protected int YDimension;

		private void AssertValidMembers()
		{
			if(!IsValidFireballFieldType())
			{
				throw new InvalidFireballFieldTypeException(Type);
			}
		}

		private bool IsValidFireballFieldType()
		{
			foreach(FireballFieldType type in Enum.GetValues(typeof(FireballFieldType)))
			{
				if(type == Type)
				{
					return true;
				}
			}

			return false;
		}
	}
}
