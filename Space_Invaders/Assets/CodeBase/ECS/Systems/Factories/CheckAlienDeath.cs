using System;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class CheckAlienDeath : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<RoundCreateRequest> roundPool;
		private EcsFilter filterDeads;
		private EcsPool<NextRoundEvent> nextRoundPool;

		public CheckAlienDeath(IStaticDataService staticDataService)
		{
			this.staticDataService = staticDataService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<RoundCreateRequest>().Inc<Used>().End();
			filterDeads = world.Filter<Alien>().Inc<Dead>().End();
			roundPool = world.GetPool<RoundCreateRequest>();
			nextRoundPool = world.GetPool<NextRoundEvent>();
		}
		

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var round = ref roundPool.Get(entity);

				if (round.Config.aliens.Count != filterDeads.GetEntitiesCount()) continue;
				
				var random = new Random();
				var configKey = random.Next(1,staticDataService.TemplateCount);
				LevelRequestFactory.Create(world,staticDataService.ForLevelTemplate(configKey));
				world.DelEntity(entity);
				foreach (var dead in filterDeads) world.DelEntity(dead);

				nextRoundPool.Add(world.NewEntity());
			}
		}
	}
}