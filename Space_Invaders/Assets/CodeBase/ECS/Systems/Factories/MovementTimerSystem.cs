using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class MovementTimerSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly ITimeService timeService;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private float delay;
		private float walkDelay = 0.5f;
		private EcsPool<MoveEvent> moveEventPool;


		public MovementTimerSystem(ITimeService timeService)
		{
			this.timeService = timeService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Alien>().Inc<View>().End();
			moveEventPool = world.GetPool<MoveEvent>();
		}

		public void Run(IEcsSystems systems)
		{
			delay += timeService.DeltaTime;
			if (delay > walkDelay)
			{
				delay = 0;
				foreach (var entity in filter)
				{
					moveEventPool.Add(entity);
				}
			}
		}
	}
}