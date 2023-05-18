using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<AlienType, AlienConfig> aliens;
		private Dictionary<string, LevelConfig> levels;
		private Dictionary<PlayerType, PlayerConfig> players;
		private const string AlienConfigsPath = "Static Data/Aliens";
		private const string LevelsPath = "Static Data/Levels";
		private const string PlayersPath = "Static Data/Player";

		public void Load()
		{
			aliens = Resources
				.LoadAll<AlienConfig>(AlienConfigsPath)
				.ToDictionary(x => x.Type, x => x);

			levels = Resources
				.LoadAll<LevelConfig>(LevelsPath)
				.ToDictionary(x => x.Key, x => x);

			players = Resources
				.LoadAll<PlayerConfig>(PlayersPath)
				.ToDictionary(x => x.Type, x => x);
		}

		public LevelConfig ForLevel(string sceneName) =>
			levels.TryGetValue(sceneName, out LevelConfig staticData)
				? staticData
				: null;

		public AlienConfig ForAlien(AlienType id) =>
			aliens.TryGetValue(id, out AlienConfig staticData)
				? staticData
				: null;

		public PlayerConfig ForPlayer(PlayerType id) =>
			players.TryGetValue(id, out PlayerConfig staticData)
				? staticData
				: null;
	}
}