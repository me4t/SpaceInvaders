using System;
using Unity.Mathematics;

namespace CodeBase.Data
{
	[Serializable]
	public class BulletCreateData
	{
		public float3 Position;
		public string PrefabPath;
		public float Speed;
		public float BodySize;
		public float Damage;
	}
}