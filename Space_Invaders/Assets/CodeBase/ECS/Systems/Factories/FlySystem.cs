using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.Factories
{
	public class FlySystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<FlyTo> flyToPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().Inc<View>().Inc<FlyTo>().End();

			positionPool = world.GetPool<Position>();
			flyToPool = world.GetPool<FlyTo>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var position = ref positionPool.Get(entity);
				ref var flyTo = ref flyToPool.Get(entity);

				float3 newPosition = flyTo.Value;
				position.Value += newPosition * 0.2f;
			}
		}
	}
}