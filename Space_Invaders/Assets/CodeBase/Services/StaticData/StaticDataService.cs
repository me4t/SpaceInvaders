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
		private const string AlienConfigsPath = "Static Data/Aliens";
		private const string LevelsPath = "Static Data/Levels";

		public void Load()
		{
			aliens = Resources
				.LoadAll<AlienConfig>(AlienConfigsPath)
				.ToDictionary(x => x.Type, x => x);
			
			
			levels = Resources
				.LoadAll<LevelConfig>(LevelsPath)
				.ToDictionary(x => x.Key, x => x);
		}

		public LevelConfig ForLevel(string sceneName) =>
			levels.TryGetValue(sceneName, out LevelConfig staticData)
				? staticData
				: null;

		public AlienConfig ForAlien(AlienType id) =>
			aliens.TryGetValue(id, out AlienConfig staticData)
				? staticData
				: null;
	}
}