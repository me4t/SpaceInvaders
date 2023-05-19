using System.Collections.Generic;
using CodeBase.Configs;

namespace CodeBase.ECS.Systems.LevelCreate
{
	public struct RoundCreateRequest
	{
		public LevelData Config;
		public bool withPlayer;
	}

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