using CodeBase.ECS.Components;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.General.Bullets
{
	public class FlySystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly ITimeService timeService;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<FlyTo> flyToPool;
		private EcsPool<Speed> speedPool;


		public FlySystem(ITimeService timeService)
		{
			this.timeService = timeService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().Inc<View>().Inc<FlyTo>().End();

			positionPool = world.GetPool<Position>();
			flyToPool = world.GetPool<FlyTo>();
			speedPool = world.GetPool<Speed>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var position = ref positionPool.Get(entity);
				ref var flyTo = ref flyToPool.Get(entity);
				ref var speed = ref speedPool.Get(entity);

				float3 newPosition = flyTo.Value;
				position.Value += newPosition * speed.Value * timeService.DeltaTime;
			}
		}
	}
}