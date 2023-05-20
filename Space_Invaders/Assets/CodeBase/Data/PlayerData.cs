using System;
using CodeBase.Enums;
using Unity.Mathematics;

namespace CodeBase.Data
{
	[Serializable]
	public class PlayerData
	{
		public float3 Position;
		public PlayerType Type;
		public string PrefabPath;
		public float Speed;
		public float3 GunDirection;
		public float BodySize;
		public BulletType BulletType;
		public int BulletCount;
		public float Health;
	}
}