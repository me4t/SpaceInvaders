using CodeBase.Configs;
using CodeBase.ECS.Systems.Factories;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;

namespace CodeBase.Services.StaticData
{
	public static class DataFactory
	{
		public static PlayerData NewPlayerData(this PlayerConfig x) =>
			new PlayerData
			{
				Type = x.Type,
				PrefabPath = x.Path.ConvertToString(),
				Speed = x.Speed,
				GunDirection = x.GunDirection,
				BodySize = x.Size,
				BulletType = x.BulletType,
				BulletCount = x.BaseBulletCount,
				Health = x.Health
			};
		
		public static BulletLootData NewBulletLootData(this BulletLootConfig x)
		{
			var lootData = new BulletLootData();
			lootData.PrefabPath = x.Path.ConvertToString();
			lootData.BodySize = x.BodySize;
			lootData.BulletType = x.Type;
			lootData.Count = x.Count;
			return lootData;
		}
		public static LevelData NewBulletLootData(this LevelConfig x)
		{
			var lootData = new LevelData();
			lootData.Key = x.Key;
			lootData.PlayerSpawnPoint = x.PlayerSpawnPoint;
			lootData.aliens = x.Aliens;
			return lootData;
		}


		public static AlienCreateData NewAlienData(this AlienConfig x) =>
			new AlienCreateData
			{
				AlienType = x.Type,
				PrefabPath = x.Path.ConvertToString(),
				BodySize = x.Size,
				Health = x.Health,
				Speed = x.Speed,
				GunDirection = x.GunDirection,
				MoveDirection = x.MoveDirection,
				Score = x.Score,
				BulletLoot = x.BulletLoot,
				LootDropChange = x.LootDropChange,
			};

		public static BulletCreateData NewBulletData(this BulletConfig x) =>
			new BulletCreateData
			{
				PrefabPath = x.Path.ConvertToString(),
				BodySize = x.Size,
				Speed = x.Speed,
				Damage = x.Damage,
			};
	}
}