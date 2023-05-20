using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.ECS.Components;
using CodeBase.Enums;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Factories
{
	public class PlayerFactory
	{
		public static int Create(EcsWorld world, PlayerData config)
		{
			var entity = world.NewEntity();

			var spawnEventPool = world.GetPool<SpawnEvent>();
			ref var spawnEvent = ref spawnEventPool.Add(entity);
			spawnEvent.Position = config.Position;
			spawnEvent.Path = config.PrefabPath;

			var positionPool = world.GetPool<Position>();
			ref var position = ref positionPool.Add(entity);
			position.Value = spawnEvent.Position;

			var playerPool = world.GetPool<Player>();
			ref var alien = ref playerPool.Add(entity);
			var speedPool = world.GetPool<Speed>();
			ref var speed = ref speedPool.Add(entity);
			speed.Value = config.Speed;

			var gunPool = world.GetPool<Gun>();
			ref var gun = ref gunPool.Add(entity);
			gun.Direction = config.GunDirection;

			var bodySize = world.GetPool<BodySize>();
			ref var size = ref bodySize.Add(entity);
			size.Radius = config.BodySize;

			var bulletOwnerPool = world.GetPool<BulletOwner>();
			ref var bulletOwner = ref bulletOwnerPool.Add(entity);
			bulletOwner.Bullets = new Dictionary<BulletType, int>();
			bulletOwner.Bullets.Add(config.BulletType, config.BulletCount);

			var collideWithBulletPool = world.GetPool<CanCollideWithAlien>();
			collideWithBulletPool.Add(entity);

			var update = world.GetPool<UpdateBulletCountEvent>();
			update.Add(entity);

			var healthPool = world.GetPool<Health>();
			ref var health = ref healthPool.Add(entity);
			health.Max = config.Health;
			health.Current = config.Health;
			
			var hittablePool = world.GetPool<Hittable>();
			hittablePool.Add(entity);	

			return entity;
		}
	}
}