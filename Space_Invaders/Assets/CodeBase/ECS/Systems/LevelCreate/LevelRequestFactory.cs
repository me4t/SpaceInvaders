using CodeBase.Configs;
using CodeBase.ECS.Systems.Factories;
using CodeBase.Enums;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.LevelCreate
{
	public class LevelRequestFactory
	{
		public static int Create(EcsWorld world, LevelConfig config,bool withPlayer = false)
		{
			var entity = world.NewEntity();

			var reqeustsPool = world.GetPool<RoundCreateRequest>();

			ref var requestComponent = ref reqeustsPool.Add(entity);
			requestComponent.Config  = new LevelData();
			requestComponent.Config.PlayerSpawnPoint = config.PlayerSpawnPoint;
			requestComponent.Config.aliens = config.aliens;
			requestComponent.Config.Key = config.Key;
			requestComponent.withPlayer = withPlayer;

			return entity;
		}
	}

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
			
			
			var healthPool = world.GetPool<Health>();
			ref var health = ref healthPool.Add(entity);
			health.Current = config.Health;
			health.Max = config.Health;
			
			var speedPool = world.GetPool<Speed>();
			ref var speed = ref speedPool.Add(entity);
			speed.Value = config.Speed;
			
			var gunPool = world.GetPool<Gun>();
			ref var gun =ref  gunPool.Add(entity);
			gun.Direction = config.GunDirection;
			
			var moveDirPool = world.GetPool<MoveDirection>();
			ref var move =ref  moveDirPool.Add(entity);
			move.Direction = config.MoveDirection;
			
			var scoreLootPool = world.GetPool<ScoreLoot>();
			ref var scoreLoot =ref  scoreLootPool.Add(entity);
			scoreLoot.Value = config.Score;
			
			var moveTimePool = world.GetPool<MoveTime>();
			ref var moveTime =ref  moveTimePool.Add(entity);
			moveTime.WalkDelay = 0.5f;

			var random = new System.Random();
			int randomChance = random.Next(0, 100);
			
			if (randomChance < config.LootDropChange)
			{
				var bulletLootPool = world.GetPool<BulletLoot>();
				ref var bulletLoot = ref bulletLootPool.Add(entity);
				var nextInt = random.Next(0, config.BulletLoot.Count);
				bulletLoot.Type = config.BulletLoot[nextInt];
			}

			
			
			return entity;
		}
	}

	public class PlayerFactory
	{
		public static int Create(EcsWorld world, PlayerCreateData config)
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
			bulletOwner.Type = config.BulletType;
			bulletOwner.BulletCount = config.BulletCount;

			var update = world.GetPool<UpdateBulletCountEvent>();
			update.Add(entity);
			
			return entity;
		}
	}

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

			var bodySize = world.GetPool<BodySize>();
			ref var size = ref bodySize.Add(entity);
			size.Radius = config.BodySize;
			
			var damageDealerPool= world.GetPool<DamageDealer>();
			ref var damageDealer = ref damageDealerPool.Add(entity);
			damageDealer.Value = config.Damage;
				
			return entity;
		}
	}
	public class LootFactory
	{
		public static int Create(EcsWorld world, LootData config)
		{
			var entity = world.NewEntity();

			var lootPool = world.GetPool<Loot>();
			ref var loot = ref lootPool.Add(entity);
			

			var spawnEventPool = world.GetPool<SpawnEvent>();
			ref var spawnEvent = ref spawnEventPool.Add(entity);
			spawnEvent.Position = config.Position;
			spawnEvent.Path = config.PrefabPath;

			var positionPool = world.GetPool<Position>();
			ref var position = ref positionPool.Add(entity);
			position.Value = spawnEvent.Position;

			return entity;
		}
	}

	public struct SpawnEvent
	{
		public float3 Position;
		public string Path;
	}
	public struct Loot
	{
	}

	public struct Alien
	{
		public AlienType AlienType;
	}
	public struct Dead
	{
	}
	public struct NextRoundEvent
	{
		
	}
	public struct RoundPassed
	{
		
	}
	

	public struct Player
	{
		public PlayerType PlayerType;
	}

	public struct Bullet
	{
	}
	

	public struct Speed
	{
		public float Value;
	}
	public struct BulletOwner
	{
		public BulletType Type;
		public int BulletCount;
	}

	public struct Position
	{
		public float3 Value;
	}
	public struct BulletLoot
	{
		public BulletType Type;
	}

	public struct FlyTo
	{
		public float3 Value;
	}

	public struct Gun
	{
		public float3 Direction;
	}
	
	public struct Damage
	{
		public float Value;
		public EcsPackedEntity Target;
	}
	public struct MoveEvent
	{
	}
	public struct MoveTime
	{
		public float WanderTime;
		public float WalkDelay;
	}
	public struct UpdateScore
	{
		public float Value;
	}
	public struct DeathEvent
	{
	}
	public struct UpdateBulletCountEvent
	{
	}
	public struct MoveDirection
	{
		public float3 Direction;
	}

	public struct DamageDealer
	{
		public float Value;
	}

	public struct BodySize
	{
		public float Radius;
	}
	public struct ScoreLoot
	{
		public float Value;
	}

	public struct Health
	{
		public float Current;
		public float Max;
	}

	public struct View : IEcsAutoReset<View>
	{
		public IUnityObjectView Value;

		public void AutoReset(ref View c) =>
			c.Value?.DestroyView();
	}
}