using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.Factories
{
	public class CheckCollisionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsFilter alienFilter;
		private EcsPool<Damage> damagePool;
		private EcsPool<DamageDealer> damageDealerPool;
		private EcsPool<BodySize> bodySizePool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().Inc<View>().Inc<FlyTo>().End();
			alienFilter = world.Filter<Alien>().Inc<View>().Exc<Dead>().End();

			positionPool = world.GetPool<Position>();
			damagePool = world.GetPool<Damage>();
			damageDealerPool = world.GetPool<DamageDealer>();
			bodySizePool = world.GetPool<BodySize>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				var bulletPosition = positionPool.Get(entity).Value;
				var bulletSize = bodySizePool.Get(entity).Radius;
				foreach (var alien in alienFilter)
				{
					var alienPosition = positionPool.Get(alien).Value;
					var alienSize = bodySizePool.Get(alien).Radius;

					if (IsSpheresIntersect(bulletPosition, alienPosition, bulletSize, alienSize))
					{

						var newEntity = world.NewEntity();
						ref var damage = ref damagePool.Add(newEntity);
						ref var damageDealer = ref damageDealerPool.Get(entity);
						damage.Value = damageDealer.Value;
						damage.Target = world.PackEntity(alien);
						world.DelEntity(entity);

						break;
					}
				}
			}
		}

		private bool IsSpheresIntersect(float3 pos1, float3 pos2, float radius1, float radius2)
		{
			float radius = radius1 + radius2;
			var delta = pos2 - pos1;
			return MathHelpers.SqrMagnitude(delta) < radius * radius;
		}
		
	}
}