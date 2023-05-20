using CodeBase.ECS.Systems.LevelCreate;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class CheckBulletCollisionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter collideWithAlien;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsFilter collideWithBullet;
		private EcsPool<BodySize> bodySizePool;
		private EcsPool<CollisionHittableWithDamageDealer> collisionEventPool;
		private EcsPool<Used> usedPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			collideWithAlien = world.Filter<CanCollideWithAlien>().Inc<View>().Exc<Player>().End();
			collideWithBullet = world.Filter<CanCollideWithBullet>().Inc<View>().End();

			positionPool = world.GetPool<Position>();
			bodySizePool = world.GetPool<BodySize>();
			collisionEventPool = world.GetPool<CollisionHittableWithDamageDealer>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var colliderWithAlien in collideWithAlien)
			{
				var damageDealer = positionPool.Get(colliderWithAlien).Value;
				var dealerSize = bodySizePool.Get(colliderWithAlien).Radius;
				foreach (var alien in collideWithBullet)
				{
					var hittable = positionPool.Get(alien).Value;
					var hittableSize = bodySizePool.Get(alien).Radius;

					if (MathHelpers.IsSpheresIntersect(damageDealer, hittable, dealerSize, hittableSize))
					{
						var entity = world.NewEntity();
						ref var collision = ref collisionEventPool.Add(entity);
						collision.DamageDealer = world.PackEntity(colliderWithAlien);
						collision.Hittable = world.PackEntity(alien);

						break;
					}
				}
			}
		}
	}
}