using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class DamageSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Damage> damagePool;
		private EcsPool<Health> healthPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Damage>().End();
			damagePool = world.GetPool<Damage>();
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