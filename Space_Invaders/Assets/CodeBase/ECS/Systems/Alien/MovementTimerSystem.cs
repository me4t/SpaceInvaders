using CodeBase.ECS.Components;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Alien
{
	public class MovementTimerSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly ITimeService timeService;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<MoveEvent> moveEventPool;
		private EcsPool<MoveTime> moveTimePool;


		public MovementTimerSystem(ITimeService timeService)
		{
			this.timeService = timeService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Alien>().Inc<View>().End();
			moveEventPool = world.GetPool<MoveEvent>();
			moveTimePool = world.GetPool<MoveTime>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var moveTime = ref moveTimePool.Get(entity);
				moveTime.WanderTime += timeService.DeltaTime;

				if (moveTime.WanderTime > moveTime.WalkDelay)
				{
					moveEventPool.Add(entity);
					moveTime.WanderTime = 0;
				}
			}
		}
	}
}