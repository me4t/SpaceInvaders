using System;
using CodeBase.Enums;
using Unity.Mathematics;

namespace CodeBase.Data
{
	[Serializable]
	public class AlienSpawnPoint
	{
		public float3 Position;
		public AlienType AlienType;
	}
}