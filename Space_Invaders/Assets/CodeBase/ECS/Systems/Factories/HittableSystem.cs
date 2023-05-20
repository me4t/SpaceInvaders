using CodeBase.ECS.Systems.LevelCreate;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.ECS.Systems.Factories
{
	public class HittableSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsFilter filterRound;
		private EcsPool<CollisionHittableWithDamageDealer> collisionEventPool;
		private EcsPool<Damage> damagePool;
		private EcsPool<DamageDealer> damageDealerPool;
		private EcsPool<Bullet> bulletPool;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<CollisionHittableWithDamageDealer>().End();
			collisionEventPool = world.GetPool<CollisionHittableWithDamageDealer>();
			damagePool = world.GetPool<Damage>();
			damageDealerPool = world.GetPool<DamageDealer>();
			bulletPool = world.GetPool<Bullet>();
		}


		public void Run(IEcsSystems systems)
		{
			foreach (var collision in filter)
			{
				ref var collisionEvent = ref collisionEventPool.Get(collision);
				if (!collisionEvent.Hittable.Unpack(world, out int hittable)) continue;
				if (!collisionEvent.DamageDealer.Unpack(world, out int dealer)) continue;
				
				Debug.Log(dealer);

				var newEntity = world.NewEntity();
				ref var damage = ref damagePool.Add(newEntity);
				
				Debug.Log(damageDealerPool.Has(dealer));
				ref var damageDealer = ref damageDealerPool.Get(dealer);
				damage.Value = damageDealer.Value;
				damage.Target = world.PackEntity(hittable);
				if (bulletPool.Has(dealer)) world.DelEntity(dealer);
			}
		}
	}
}