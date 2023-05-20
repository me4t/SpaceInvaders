using System;
using System.Collections.Generic;
using CodeBase.Configs;

namespace CodeBase.Data
{
	[Serializable]
	public class LevelData
	{
		public PlayerSpawnPoint PlayerSpawnPoint;
		public List<AlienSpawnPoint> aliens;
	}
}