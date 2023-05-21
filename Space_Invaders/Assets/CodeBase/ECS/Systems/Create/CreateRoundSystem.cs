using CodeBase.Configs;
using CodeBase.Data;
using CodeBase.ECS.Components;
using CodeBase.ECS.Factories;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Create
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
}