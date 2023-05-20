using System;
using CodeBase.ECS.Components;
using CodeBase.ECS.Factories;
using CodeBase.Services.ProgressService;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Gameloop
{
	public class SetNextRoundSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private readonly IPlayerProgressService playerProgressService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;

		public SetNextRoundSystem(IStaticDataService staticDataService,IPlayerProgressService playerProgressService)
		{
			this.staticDataService = staticDataService;
			this.playerProgressService = playerProgressService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<RoundPassed>().End();
		}
		

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				var random = new Random();
				var configKey = random.Next(1,staticDataService.TemplateCount);
				playerProgressService.Progress.RoundCount++;
				LevelRequestFactory.Create(world,staticDataService.ForLevelTemplate(configKey));
			}
		}
	}
}