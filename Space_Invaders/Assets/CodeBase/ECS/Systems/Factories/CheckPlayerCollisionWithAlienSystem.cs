using CodeBase.ECS.Systems.LevelCreate;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class CheckPlayerCollisionWithAlienSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter playerFilter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsFilter alienFilter;
		private EcsPool<BodySize> bodySizePool;
		private EcsPool<CollisionHittableWithDamageDealer> collisionEventPool;
		private EcsPool<Used> usedPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			playerFilter = world.Filter<CanCollideWithAlien>().Inc<View>().Exc<Dead>().End();
			alienFilter = world.Filter<CanCollideWithPlayer>().Inc<View>().Exc<Dead>().Inc<DamageDealer>().End();

			positionPool = world.GetPool<Position>();
			bodySizePool = world.GetPool<BodySize>();
			collisionEventPool = world.GetPool<CollisionHittableWithDamageDealer>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var player in playerFilter)
			{
				var playerPosition = positionPool.Get(player).Value;
				var playerSize = bodySizePool.Get(player).Radius;
				foreach (var alien in alienFilter)
				{
					var alienPosition = positionPool.Get(alien).Value;
					var alienSize = bodySizePool.Get(alien).Radius;

					if (MathHelpers.IsSpheresIntersect(playerPosition, alienPosition, playerSize, alienSize))
					{
						var entity = world.NewEntity();
						ref var collision = ref collisionEventPool.Add(entity);
						collision.Hittable = world.PackEntity(player);
						collision.DamageDealer = world.PackEntity(alien);

						break;
					}
				}
			}
		}
	}
}