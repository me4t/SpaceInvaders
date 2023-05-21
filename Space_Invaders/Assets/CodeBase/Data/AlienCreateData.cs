using System;
using System.Collections.Generic;
using CodeBase.Enums;
using Unity.Mathematics;

namespace CodeBase.Data
{
	[Serializable]
	public class AlienCreateData
	{
		public float3 Position;
		public string PrefabPath;
		public float BodySize;
		public float Health;
		public float Speed;
		public float3 GunDirection;
		public float3 MoveDirection;
		public float Score;
		public List<BulletType> BulletLoot;
		public float LootDropChange;
	}
}