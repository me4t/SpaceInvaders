using System;
using System.Collections.Generic;
using CodeBase.Configs;

namespace CodeBase.ECS.Systems.Create
{
	public struct RoundCreateRequest
	{
		public LevelData Config;
		public bool WithPlayer;
	}

	[Serializable]
	public class LevelData
	{
		public int Key;
		public PlayerSpawnPoint PlayerSpawnPoint;
		public List<AlienSpawnPoint> aliens;
	}
	public struct Used
	{
	}
}