using CodeBase.Configs;
using CodeBase.ECS.Systems.Factories;
using CodeBase.Enums;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.LevelCreate
{
	public class LevelRequestFactory
	{
		public static int Create(EcsWorld world, LevelConfig config)
		{
			var entity = world.NewEntity();

			var reqeustsPool = world.GetPool<LevelCreateRequest>();

			ref var requestComponent = ref reqeustsPool.Add(entity);
			requestComponent.Config = config;

			return entity;
		}
	}

	public class AlienFactory
	{
		public static int Create(EcsWorld world, AlienCreateData config)
		{
			var entity = world.NewEntity();

			var positionPool = world.GetPool<SpawnEvent>();
			ref var position = ref positionPool.Add(entity);
			position.Position = config.Position;
			position.Path = config.PrefabPath;

			var alienPool = world.GetPool<Alien>();
			ref var alien = ref alienPool.Add(entity);

			var bodySize = world.GetPool<BodySize>();
			ref var size = ref bodySize.Add(entity);
			size.Radius = config.BodySize;
			
			
			var healthPool = world.GetPool<Health>();
			ref var health = ref healthPool.Add(entity);
			health.Current = config.Health;
			health.Max = config.Health;
			
			return entity;
		}
	}

	public class PlayerFactory
	{
		public static int Create(EcsWorld world, PlayerCreateData config)
		{
			var entity = world.NewEntity();

			var positionPool = world.GetPool<SpawnEvent>();
			ref var position = ref positionPool.Add(entity);
			position.Position = config.Position;
			position.Path = config.PrefabPath;

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


			return entity;
		}
	}

	public class BulletFactory
	{
		public static int Create(EcsWorld world, BulletCreateData config)
		{
			var entity = world.NewEntity();

			var positionPool = world.GetPool<SpawnEvent>();
			ref var position = ref positionPool.Add(entity);
			position.Position = config.Position;
			position.Path = config.PrefabPath;

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

	public struct SpawnEvent
	{
		public float3 Position;
		public string Path;
	}

	public struct Alien
	{
		public AlienType AlienType;
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

	public struct Position
	{
		public float3 Value;
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

	public struct DamageDealer
	{
		public float Value;
	}

	public struct BodySize
	{
		public float Radius;
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