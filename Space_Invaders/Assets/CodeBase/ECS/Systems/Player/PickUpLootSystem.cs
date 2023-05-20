using CodeBase.ECS.Components;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Player
{
	public class PickUpLootSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsFilter filterRound;
		private EcsPool<CollisionPlayerWithLoot> collisionEventPool;
		private EcsPool<BulletLoot> bulletLoot;
		private EcsPool<BulletOwner> bulletOwner;
		private EcsPool<UpdateBulletCountEvent> updatePool;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<CollisionPlayerWithLoot>().End();
			collisionEventPool = world.GetPool<CollisionPlayerWithLoot>();
			bulletLoot = world.GetPool<BulletLoot>();
			bulletOwner = world.GetPool<BulletOwner>();
			updatePool = world.GetPool<UpdateBulletCountEvent>();
			
		}


		public void Run(IEcsSystems systems)
		{
			foreach (var collision in filter)
			{
				ref var collisionEvent = ref collisionEventPool.Get(collision);
				if (!collisionEvent.Loot.Unpack(world, out int loot)) continue;
				if (!collisionEvent.Player.Unpack(world, out int player)) continue;

				ref var lootBullet = ref bulletLoot.Get(loot);
				ref var fireBulletOwner = ref bulletOwner.Get(player);
				AddBullets(fireBulletOwner,ref  lootBullet);
				world.DelEntity(loot);
				
				UpdateUI(player);
			}
		}

		private void UpdateUI(int player)
		{
			if (!updatePool.Has(player))
				updatePool.Add(player);
		}

		private  void AddBullets(BulletOwner bulletOwner, ref BulletLoot lootBullet)
		{
			if (bulletOwner.Bullets.ContainsKey(lootBullet.Type))
				bulletOwner.Bullets[lootBullet.Type] += lootBullet.Count;
			else bulletOwner.Bullets.Add(lootBullet.Type, lootBullet.Count);
		}
	}
}