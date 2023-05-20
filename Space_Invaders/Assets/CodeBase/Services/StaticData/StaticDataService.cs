using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using CodeBase.ECS.Systems.Factories;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private Dictionary<AlienType, AlienCreateData> aliens;
		private Dictionary<int, LevelData> levels;
		private Dictionary<PlayerType, PlayerData> players;
		private Dictionary<BulletType, BulletCreateData> bullets;
		private Dictionary<BulletType, BulletLootData> loots;
		private Dictionary<WindowId, WindowData> windowConfigs;

		private const string AlienConfigsPath = "Static Data/Aliens";
		private const string LevelsPath = "Static Data/Levels";
		private const string PlayersPath = "Static Data/Player";
		private const string BulletsPath = "Static Data/Bullets";
		private const string LootBulletsPath = "Static Data/Loot/Bullets";
		private const string StaticDataWindowPath = "Static Data/UI/WindowStaticData";

		public void Load()
		{
			aliens = Resources
				.LoadAll<AlienConfig>(AlienConfigsPath)
				.ToDictionary(x => x.Type, DataFactory.NewAlienData);

			levels = Resources
				.LoadAll<LevelConfig>(LevelsPath)
				.ToDictionary(x => x.Key, DataFactory.NewBulletLootData);

			players = Resources
				.LoadAll<PlayerConfig>(PlayersPath)
				.ToDictionary(x => x.Type, DataFactory.NewPlayerData);

			bullets = Resources
				.LoadAll<BulletConfig>(BulletsPath)
				.ToDictionary(x => x.Type, DataFactory.NewBulletData);

			loots = Resources
				.LoadAll<BulletLootConfig>(LootBulletsPath)
				.ToDictionary(x => x.Type, DataFactory.NewBulletLootData);
			
			windowConfigs = Resources
				.Load<WindowStaticData>(StaticDataWindowPath)
				.Configs
				.ToDictionary(x => x.WindowId, x => x);
			
		}

		public LevelData ForLevelTemplate(int key) =>
			levels.TryGetValue(key, out LevelData staticData)
				? staticData
				: null;

		public int TemplateCount => levels.Keys.Count;

		public AlienCreateData ForAlien(AlienType id) =>
			aliens.TryGetValue(id, out AlienCreateData staticData)
				? staticData
				: null;

		public PlayerData ForPlayer(PlayerType id) =>
			players.TryGetValue(id, out PlayerData staticData)
				? staticData
				: null;

		public BulletCreateData ForBullet(BulletType id) =>
			bullets.TryGetValue(id, out BulletCreateData staticData)
				? staticData
				: null;

		public BulletLootData ForBulletLoot(BulletType id) =>
			loots.TryGetValue(id, out BulletLootData staticData)
				? staticData
				: null;

		public WindowData ForWindow(WindowId windowId)=>
			windowConfigs.TryGetValue(windowId, out WindowData staticData)
				? staticData
				: null;
	}
}