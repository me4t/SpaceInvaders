using System;
using CodeBase.Enums;
using Unity.Mathematics;

namespace CodeBase.Data
{
	[Serializable]
	public class PlayerSpawnPoint
	{
		public float3 PlayerInitialPoint;
		public PlayerType PlayerType;
	}
}