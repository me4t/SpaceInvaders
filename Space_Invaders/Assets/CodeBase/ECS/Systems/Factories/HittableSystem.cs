using CodeBase.ECS.Systems.LevelCreate;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class HittableSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsFilter filterRound;
		private EcsPool<CollisionHittableWithBullet> collisionEventPool;
		private EcsPool<Damage> damagePool;
		private EcsPool<DamageDealer> damageDealerPool;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<CollisionHittableWithBullet>().End();
			collisionEventPool = world.GetPool<CollisionHittableWithBullet>();
			damagePool = world.GetPool<Damage>();
			damageDealerPool = world.GetPool<DamageDealer>();
		}


		public void Run(IEcsSystems systems)
		{
			foreach (var collision in filter)
			{
				ref var collisionEvent = ref collisionEventPool.Get(collision);
				if (!collisionEvent.Hittable.Unpack(world, out int hittable)) continue;
				if (!collisionEvent.Bullet.Unpack(world, out int bullet)) continue;


				var newEntity = world.NewEntity();
				ref var damage = ref damagePool.Add(newEntity);
				ref var damageDealer = ref damageDealerPool.Get(bullet);
				damage.Value = damageDealer.Value;
				damage.Target = world.PackEntity(hittable);
				world.DelEntity(bullet);
			}
		}
	}
}