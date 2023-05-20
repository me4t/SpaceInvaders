using System;
using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.Factories
{
	public class CreateRoundSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter requestsFilter;
		private EcsPool<RoundCreateRequest> requestPool;
		private EcsPool<Used> usedPool;
		private EcsPool<NextRoundEvent> nextRoundEvent;
		private EcsPool<UpdateBulletCountEvent> updateBulletPool;

		public CreateRoundSystem(IStaticDataService staticDataService)
		{
			this.staticDataService = staticDataService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			requestsFilter = world.Filter<RoundCreateRequest>().Exc<Used>().End();

			requestPool = world.GetPool<RoundCreateRequest>();
			nextRoundEvent = world.GetPool<NextRoundEvent>();
			updateBulletPool = world.GetPool<UpdateBulletCountEvent>();
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

				if (levelCreateRequest.WithPlayer) CreatePlayer(levelConfig.PlayerSpawnPoint);
				usedPool.Add(entity);
				nextRoundEvent.Add(world.NewEntity());
				updateBulletPool.Add(world.NewEntity());
			}
		}

		private void CreateAlien(AlienSpawnPoint spawnPoint)
		{
			var data = staticDataService.ForAlien(spawnPoint.AlienType);
			data.Position = spawnPoint.Position;
			AlienFactory.Create(world, data);
		}

		private void CreatePlayer(PlayerSpawnPoint spawnPoint)
		{
			var data = staticDataService.ForPlayer(spawnPoint.PlayerType);
			data.Position = spawnPoint.PlayerInitialPoint;
			PlayerFactory.Create(world, data);
		}
	}

	[Serializable]
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
	[Serializable]
	public class PlayerData
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

	[Serializable]
	public class BulletCreateData
	{
		public float3 Position;
		public string PrefabPath;
		public float Speed;
		public float BodySize;
		public float Damage;
	}

	[Serializable]
	public class LootData
	{
		public float3 Position;
		public string PrefabPath;
		public float BodySize;
	}
	[Serializable]
	public class BulletLootData:LootData
	{
		public BulletType BulletType;
		public int Count;
	}

}