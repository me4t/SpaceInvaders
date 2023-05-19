using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.Factories
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
		private EcsPool<MoveDirection> gunEventPool;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Alien>().Inc<View>().Inc<MoveEvent>().End();

			positionPool = world.GetPool<Position>();
			speedPool = world.GetPool<Speed>();
			moveEventPool = world.GetPool<MoveEvent>();
			gunEventPool = world.GetPool<MoveDirection>();
		}

		public MoveAliensSystem(ITimeService timeService)
		{
			this.timeService = timeService;
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var position = ref positionPool.Get(entity);
				ref var speed = ref speedPool.Get(entity);
				ref var move = ref gunEventPool.Get(entity);

				position.Value += move.Direction * speed.Value * timeService.DeltaTime;
				moveEventPool.Del(entity);
			}
		}
	}
}