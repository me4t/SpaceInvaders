using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<AlienType, AlienConfig> aliens;
		private Dictionary<int, LevelConfig> levels;
		private Dictionary<PlayerType, PlayerConfig> players;
		private Dictionary<BulletType, BulletConfig> bullets;
		private Dictionary<BulletType, BulletLootConfig> loots;

		private const string AlienConfigsPath = "Static Data/Aliens";
		private const string LevelsPath = "Static Data/Levels";
		private const string PlayersPath = "Static Data/Player";
		private const string BulletsPath = "Static Data/Bullets";
		private const string LootBulletsPath = "Static Data/Loot/Bullets";

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

			bullets = Resources
				.LoadAll<BulletConfig>(BulletsPath)
				.ToDictionary(x => x.Type, x => x);

			loots = Resources
				.LoadAll<BulletLootConfig>(LootBulletsPath)
				.ToDictionary(x => x.Type, x => x);
		}

		public LevelConfig ForLevelTemplate(int key) =>
			levels.TryGetValue(key, out LevelConfig staticData)
				? staticData
				: null;

		public int TemplateCount => levels.Keys.Count;

		public AlienConfig ForAlien(AlienType id) =>
			aliens.TryGetValue(id, out AlienConfig staticData)
				? staticData
				: null;

		public PlayerConfig ForPlayer(PlayerType id) =>
			players.TryGetValue(id, out PlayerConfig staticData)
				? staticData
				: null;

		public BulletConfig ForBullet(BulletType id) =>
			bullets.TryGetValue(id, out BulletConfig staticData)
				? staticData
				: null;

		public BulletLootConfig ForBulletLoot(BulletType id) =>
			loots.TryGetValue(id, out BulletLootConfig staticData)
				? staticData
				: null;
	}
}