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
			CoordinateSystem system
			)
		{
			Type = type;
			System = system;

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

		protected readonly CoordinateSystem System;

		protected int XDimension
		{
			get
			{
				return System.XAxis.Count;
			}
		}

		protected int YDimension
		{
			get
			{
				return System.YAxis.Count;
			}
		}

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
