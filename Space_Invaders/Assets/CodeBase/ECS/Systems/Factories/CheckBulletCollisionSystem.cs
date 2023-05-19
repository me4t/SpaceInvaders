using CodeBase.ECS.Systems.LevelCreate;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.ECS.Systems.Factories
{
	public class CheckBulletCollisionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsFilter alienFilter;
		private EcsPool<BodySize> bodySizePool;
		private EcsPool<CollisionHittableWithBullet> collisionEventPool;
		private EcsPool<Used> usedPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().Inc<View>().Inc<FlyTo>().End();
			alienFilter = world.Filter<Alien>().Inc<View>().Exc<Dead>().End();

			positionPool = world.GetPool<Position>();
			bodySizePool = world.GetPool<BodySize>();
			collisionEventPool = world.GetPool<CollisionHittableWithBullet>();
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

					if (MathHelpers.IsSpheresIntersect(bulletPosition, alienPosition, bulletSize, alienSize))
					{
						Debug.Log("Was collision");
						var collision = world.NewEntity();
						ref var collisionHittableWithBullet = ref collisionEventPool.Add(collision);
						collisionHittableWithBullet.Bullet = world.PackEntity(entity);
						collisionHittableWithBullet.Hittable = world.PackEntity(alien);

						/*var newEntity = world.NewEntity();
						ref var damage = ref damagePool.Add(newEntity);
						ref var damageDealer = ref damageDealerPool.Get(entity);
						damage.Value = damageDealer.Value;
						damage.Target = world.PackEntity(alien);*/

						break;
					}
				}
			}
		}
	}
}