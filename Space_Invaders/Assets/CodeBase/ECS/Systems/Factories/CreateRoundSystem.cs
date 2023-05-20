using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.ECS.Systems.Factories
{
	public class CreateRoundSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter requestsFilter;
		private EcsPool<RoundCreateRequest> requestPool;
		private EcsPool<Used> usedPool;

		public CreateRoundSystem(IStaticDataService staticDataService)
		{
			this.staticDataService = staticDataService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			requestsFilter = world.Filter<RoundCreateRequest>().Exc<Used>().End();

			requestPool = world.GetPool<RoundCreateRequest>();
			usedPool = world.GetPool<Used>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in requestsFilter)
			{
				ref var levelCreateRequest = ref requestPool.Get(entity);
				var levelConfig = levelCreateRequest.Config;

				foreach (var alien in levelConfig.aliens)
					CreateAlien(alien);

				if (levelCreateRequest.withPlayer) CreatePlayer(levelConfig.PlayerSpawnPoint);
				usedPool.Add(entity);
				Debug.Log("CreateLeveL System");
			}
		}

		private void CreateAlien(AlienSpawnPoint spawnPoint)
		{
			var alienConfig = staticDataService.ForAlien(spawnPoint.AlienType);
			AlienCreateData data = new AlienCreateData();
			data.Position = spawnPoint.Position;
			data.AlienType = spawnPoint.AlienType;
			data.PrefabPath = alienConfig.Path.ConvertToString();
			data.BodySize = alienConfig.Size;
			data.Health = alienConfig.Health;
			data.Speed = alienConfig.Speed;
			data.GunDirection = alienConfig.GunDirection;
			data.MoveDirection = alienConfig.MoveDirection;
			data.Score = alienConfig.Score;
			data.BulletLoot = alienConfig.BulletLoot;
			data.LootDropChange = alienConfig.LootDropChange;
			AlienFactory.Create(world, data);
		}

		private void CreatePlayer(PlayerSpawnPoint spawnPoint)
		{
			var alienConfig = staticDataService.ForPlayer(spawnPoint.PlayerType);
			PlayerCreateData data = new PlayerCreateData();
			data.Position = spawnPoint.PlayerInitialPoint;
			data.Type = spawnPoint.PlayerType;
			data.PrefabPath = alienConfig.Path.ConvertToString();
			data.Speed = alienConfig.Speed;
			data.GunDirection = alienConfig.GunDirection;
			data.BodySize = alienConfig.Size;
			data.BulletType = alienConfig.BulletType;
			data.BulletCount = alienConfig.BaseBulletCount;
			data.Health = alienConfig.Health; 
			PlayerFactory.Create(world, data);
		}
	}

	public class AlienCreateData
	{
		public float3 Position;
		public AlienType AlienType;
		public string PrefabPath;
		public float BodySize;
		public float Health;
		public float Speed;
		public float3 GunDirection;
		public float3 MoveDirection;
		public float Score;
		public List<BulletType> BulletLoot;
		public float LootDropChange;
	}

	public class PlayerCreateData
	{
		public float3 Position;
		public PlayerType Type;
		public string PrefabPath;
		public float Speed;
		public float3 GunDirection;
		public float BodySize;
		public BulletType BulletType;
		public int BulletCount;
		public float Health;
	}

	public class BulletCreateData
	{
		public float3 Position;
		public string PrefabPath;
		public float Speed;
		public float BodySize;
		public float Damage;
	}

	public class LootData
	{
		public float3 Position;
		public string PrefabPath;
		public float BodySize;
	}
	public class BulletLootData:LootData
	{
		public BulletType BulletType;
		public int Count;
	}

}