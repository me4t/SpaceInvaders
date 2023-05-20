using CodeBase.Data;
using CodeBase.ECS.Components;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Factories
{
	public class AlienFactory
	{
		public static int Create(EcsWorld world, AlienCreateData config)
		{
			var entity = world.NewEntity();

			var spawnEventPool = world.GetPool<SpawnEvent>();
			ref var spawnEvent = ref spawnEventPool.Add(entity);
			spawnEvent.Position = config.Position;
			spawnEvent.Path = config.PrefabPath;

			var positionPool = world.GetPool<Position>();
			ref var position = ref positionPool.Add(entity);
			position.Value = spawnEvent.Position;

			var alienPool = world.GetPool<Alien>();
			ref var alien = ref alienPool.Add(entity);

			var bodySize = world.GetPool<BodySize>();
			ref var size = ref bodySize.Add(entity);
			size.Radius = config.BodySize;

			var hittablePool = world.GetPool<Hittable>();
			ref var hittable = ref hittablePool.Add(entity);

			var damageDealerPool = world.GetPool<DamageDealer>();
			ref var dealer = ref damageDealerPool.Add(entity);
			dealer.Value = 10;


			var healthPool = world.GetPool<Health>();
			ref var health = ref healthPool.Add(entity);
			health.Current = config.Health;
			health.Max = config.Health;

			var speedPool = world.GetPool<Speed>();
			ref var speed = ref speedPool.Add(entity);
			speed.Value = config.Speed;

			var gunPool = world.GetPool<Gun>();
			ref var gun = ref gunPool.Add(entity);
			gun.Direction = config.GunDirection;

			var moveDirPool = world.GetPool<MoveDirection>();
			ref var move = ref moveDirPool.Add(entity);
			move.Direction = config.MoveDirection;

			var scoreLootPool = world.GetPool<ScoreLoot>();
			ref var scoreLoot = ref scoreLootPool.Add(entity);
			scoreLoot.Value = config.Score;

			var moveTimePool = world.GetPool<MoveTime>();
			ref var moveTime = ref moveTimePool.Add(entity);
			moveTime.WalkDelay = 0.5f;

			var collideWithBulletPool = world.GetPool<CanCollideWithBullet>();
			collideWithBulletPool.Add(entity);

			var collideWithPlayerPool = world.GetPool<CanCollideWithPlayer>();
			collideWithPlayerPool.Add(entity);


			var random = new System.Random();
			int randomChance = random.Next(0, 100);

			if (randomChance < config.LootDropChange)
			{
				var bulletLootPool = world.GetPool<SpawnBulletLoot>();
				ref var bulletLoot = ref bulletLootPool.Add(entity);
				var nextInt = random.Next(0, config.BulletLoot.Count);
				bulletLoot.Type = config.BulletLoot[nextInt];
			}


			return entity;
		}
	}
}