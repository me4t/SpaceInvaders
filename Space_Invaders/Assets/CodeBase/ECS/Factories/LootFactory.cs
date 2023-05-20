using CodeBase.Data;
using CodeBase.ECS.Components;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Factories
{
	public class LootFactory
	{
		public static int Create(EcsWorld world, LootData config)
		{
			var entity = world.NewEntity();

			var lootPool = world.GetPool<Loot>();
			ref var loot = ref lootPool.Add(entity);

			var bodySize = world.GetPool<BodySize>();
			ref var size = ref bodySize.Add(entity);
			size.Radius = config.BodySize;

			var spawnEventPool = world.GetPool<SpawnEvent>();
			ref var spawnEvent = ref spawnEventPool.Add(entity);
			spawnEvent.Position = config.Position;
			spawnEvent.Path = config.PrefabPath;

			var positionPool = world.GetPool<Position>();
			ref var position = ref positionPool.Add(entity);
			position.Value = spawnEvent.Position;

			if (config is BulletLootData bulletLootData)
			{
				var bulletLootPool = world.GetPool<BulletLoot>();
				ref var bulletLoot = ref bulletLootPool.Add(entity);
				bulletLoot.Type = bulletLootData.BulletType;
				bulletLoot.Count = bulletLootData.Count;
			}

			return entity;
		}
	}
}