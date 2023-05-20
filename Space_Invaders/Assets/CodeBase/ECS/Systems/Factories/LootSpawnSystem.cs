using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class LootSpawnSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<ScoreLoot> scoreLootPool;
		private EcsPool<SpawnBulletLoot> bulletLootPool;
		private EcsPool<Position> positionPool;


		public LootSpawnSystem(IStaticDataService staticDataService)
		{
			this.staticDataService = staticDataService;
		}
		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<DeathEvent>().End();
			bulletLootPool = world.GetPool<SpawnBulletLoot>();
			positionPool = world.GetPool<Position>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				if (bulletLootPool.Has(entity))
				{
					ref var loot = ref bulletLootPool.Get(entity);
					ref var position = ref positionPool.Get(entity);
					var lootData = staticDataService.ForBulletLoot(loot.Type).DeepCopy();
					lootData.Position = position.Value;
					LootFactory.Create(world, lootData);
				}
			}
		}
	}
}