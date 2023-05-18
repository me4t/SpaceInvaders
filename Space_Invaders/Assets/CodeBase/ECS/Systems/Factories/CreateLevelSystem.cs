using System;
using CodeBase.Configs;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using CodeBase.Infrastructure;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.ECS.Systems.Factories
{
	public class CreateLevelSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter requestsFilter;
		private EcsPool<LevelCreateRequest> requestPool;

		public CreateLevelSystem(IStaticDataService staticDataService)
		{
			this.staticDataService = staticDataService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			requestsFilter = world.Filter<LevelCreateRequest>().End();

			requestPool = world.GetPool<LevelCreateRequest>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in requestsFilter)
			{
				ref var levelCreateRequest = ref requestPool.Get(entity);
				var levelConfig = levelCreateRequest.Config;
				
				foreach (var alien in levelConfig.aliens) 
					CreateAlien(alien);
				
				CreatePlayer(levelConfig.PlayerSpawnPoint);
				
				requestPool.Del(entity);
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
			AlienFactory.Create(world, data);
		}
		private void CreatePlayer(PlayerSpawnPoint spawnPoint)
		{
			var alienConfig = staticDataService.ForPlayer(spawnPoint.PlayerType);
			PlayerCreateData data = new PlayerCreateData();
			data.Position = spawnPoint.PlayerInitialPoint;
			data.Type = spawnPoint.PlayerType;
			data.PrefabPath = alienConfig.Path.ConvertToString();
			PlayerFactory.Create(world, data);
		}
	}


	public class AlienCreateData
	{
		public float3 Position;
		public AlienType AlienType;
		public string PrefabPath;
	}
	public class PlayerCreateData
	{
		public float3 Position;
		public PlayerType Type;
		public string PrefabPath;
	}
	
}