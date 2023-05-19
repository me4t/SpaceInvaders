using System;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class SetNextRoundSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<NextRoundEvent> nextRoundPool;

		public SetNextRoundSystem(IStaticDataService staticDataService)
		{
			this.staticDataService = staticDataService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<RoundPassed>().End();
			nextRoundPool = world.GetPool<NextRoundEvent>();
		}
		

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				var random = new Random();
				var configKey = random.Next(1,staticDataService.TemplateCount);
				LevelRequestFactory.Create(world,staticDataService.ForLevelTemplate(configKey));
				nextRoundPool.Add(world.NewEntity());
			}
		}
	}
}