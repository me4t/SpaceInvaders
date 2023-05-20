using CodeBase.Configs;
using CodeBase.Data;
using CodeBase.ECS.Systems.Create;
using CodeBase.Enums;
using CodeBase.Extensions;

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
		
		public static BulletLootData NewBulletLootData(this BulletLootConfig x) =>
			new BulletLootData
			{
				PrefabPath = x.Path.ConvertToString(),
				BodySize = x.BodySize,
				BulletType = x.Type,
				Count = x.Count
			};

		public static LevelData NewBulletLootData(this LevelConfig x) =>
			new LevelData
			{
				PlayerSpawnPoint = x.PlayerSpawnPoint,
				aliens = x.Aliens
			};


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