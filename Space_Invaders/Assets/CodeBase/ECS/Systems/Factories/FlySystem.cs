using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.ECS.Systems.Factories
{
	public class FlySystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<FlyTo> flyToPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().Inc<View>().Inc<FlyTo>().End();

			viewPool = world.GetPool<View>();
			positionPool = world.GetPool<Position>();
			flyToPool = world.GetPool<FlyTo>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var position = ref positionPool.Get(entity);
				ref var flyTo = ref flyToPool.Get(entity);

				float3 newPosition = flyTo.Value;
				position.Value += newPosition * 0.2f;
			}
		}
	}

	public class CheckCollisionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<FlyTo> flyToPool;
		private EcsFilter alienFilter;
		private EcsPool<Damage> damagePool;
		private EcsPool<DamageDealer> damageDealerPool;
		private EcsPool<BodySize> bodySizePool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().Inc<View>().Inc<FlyTo>().End();
			alienFilter = world.Filter<Alien>().Inc<View>().End();

			viewPool = world.GetPool<View>();
			positionPool = world.GetPool<Position>();
			flyToPool = world.GetPool<FlyTo>();
			damagePool = world.GetPool<Damage>();
			damageDealerPool = world.GetPool<DamageDealer>();
			bodySizePool = world.GetPool<BodySize>();
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

					if (DoSpheresIntersect(bulletPosition, alienPosition, bulletSize, alienSize))
					{

						var newEntity = world.NewEntity();
						ref var damage = ref damagePool.Add(newEntity);
						ref var damageDealer = ref damageDealerPool.Get(entity);
						damage.Value = damageDealer.Value;
						damage.Target = world.PackEntity(alien);
						world.DelEntity(entity);

						break;
					}
				}
			}
		}

		bool DoSpheresIntersect(Vector3 pos1, Vector3 pos2, float radius1, float radius2)
		{
			float radius = radius1 + radius2;
			Vector3 delta = pos2 - pos1;
			return delta.sqrMagnitude < radius * radius;
		}
	}

	public class DamageSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<FlyTo> flyToPool;
		private EcsFilter alienFilter;
		private EcsPool<Damage> damagePool;
		private EcsPool<DamageDealer> damageDealerPool;
		private EcsPool<BodySize> bodySizePool;
		private EcsPool<Health> healthPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Damage>().End();
			alienFilter = world.Filter<Alien>().Inc<View>().End();

			viewPool = world.GetPool<View>();
			positionPool = world.GetPool<Position>();
			flyToPool = world.GetPool<FlyTo>();
			damagePool = world.GetPool<Damage>();
			damageDealerPool = world.GetPool<DamageDealer>();
			bodySizePool = world.GetPool<BodySize>();
			healthPool = world.GetPool<Health>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var damage = ref damagePool.Get(entity);
				if (TargetIsAlive(ref damage, out var target))
				{
					ref var health = ref healthPool.Get(target);
					health.Current -= damage.Value;

					if (health.Current <= 0)
					{
						world.DelEntity(target);
					}
				}
			}
		}

		private bool TargetIsAlive(ref Damage damage, out int target)
		{
			return damage.Target.Unpack(world, out target);
		}
	}
}