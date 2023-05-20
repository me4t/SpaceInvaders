using System;
using Unity.Mathematics;

namespace CodeBase.Data
{
	[Serializable]
	public class LootData
	{
		public float3 Position;
		public string PrefabPath;
		public float BodySize;
	}
}