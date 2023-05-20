using CodeBase.Data;
using CodeBase.ECS.Components;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Factories
{
	public class BulletFactory
	{
		public static int Create(EcsWorld world, BulletCreateData config)
		{
			var entity = world.NewEntity();

			var spawnEventPool = world.GetPool<SpawnEvent>();
			ref var spawnEvent = ref spawnEventPool.Add(entity);
			spawnEvent.Position = config.Position;
			spawnEvent.Path = config.PrefabPath;

			var positionPool = world.GetPool<Position>();
			ref var position = ref positionPool.Add(entity);
			position.Value = spawnEvent.Position;

			var playerPool = world.GetPool<Bullet>();
			ref var alien = ref playerPool.Add(entity);
			var speedPool = world.GetPool<Speed>();
			ref var speed = ref speedPool.Add(entity);
			speed.Value = config.Speed;

			var collideWithPool = world.GetPool<CanCollideWithAlien>();
			collideWithPool.Add(entity);


			var bodySize = world.GetPool<BodySize>();
			ref var size = ref bodySize.Add(entity);
			size.Radius = config.BodySize;

			var damageDealerPool = world.GetPool<DamageDealer>();
			ref var damageDealer = ref damageDealerPool.Add(entity);
			damageDealer.Value = config.Damage;

			return entity;
		}
	}
}