using CodeBase.ECS.Components;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using CodeBase.Services.TimeService;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Alien
{
	public class MoveAliensSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly ITimeService timeService;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Position> positionPool;
		private EcsPool<Speed> speedPool;
		private EcsPool<MoveEvent> moveEventPool;
		private EcsPool<MoveDirection> moveDirectionPool;

		public MoveAliensSystem(ITimeService timeService)
		{
			this.timeService = timeService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Alien>().Inc<View>().Inc<MoveEvent>().End();

			positionPool = world.GetPool<Position>();
			speedPool = world.GetPool<Speed>();
			moveEventPool = world.GetPool<MoveEvent>();
			moveDirectionPool = world.GetPool<MoveDirection>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var position = ref positionPool.Get(entity);
				ref var speed = ref speedPool.Get(entity);
				ref var move = ref moveDirectionPool.Get(entity);

				position.Value += move.Direction * speed.Value * timeService.DeltaTime;
				moveEventPool.Del(entity);
			}
		}
	}
}