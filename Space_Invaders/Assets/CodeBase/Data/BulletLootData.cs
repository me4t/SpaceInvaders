using System;
using CodeBase.Enums;

namespace CodeBase.Data
{
	[Serializable]
	public class BulletLootData : LootData
	{
		public BulletType BulletType;
		public int Count;
	}
}