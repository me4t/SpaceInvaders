using CodeBase.ECS.Components;
using CodeBase.Extensions;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.General.Collisions
{
	public class CheckLootCollisionSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter playerFilter;
		private EcsFilter lootFilter;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<BodySize> bodySizePool;
		private EcsPool<CollisionPlayerWithLoot> collisionEventPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			playerFilter = world.Filter<Components.Player>().Inc<View>().End();
			lootFilter = world.Filter<Loot>().Inc<View>().Exc<Dead>().End();

			positionPool = world.GetPool<Position>();
			bodySizePool = world.GetPool<BodySize>();
			collisionEventPool = world.GetPool<CollisionPlayerWithLoot>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var player in playerFilter)
			{
				var playerPosition = positionPool.Get(player).Value;
				var playerSize = bodySizePool.Get(player).Radius;

				foreach (var loot in lootFilter)
				{
					var alienPosition = positionPool.Get(loot).Value;
					var alienSize = bodySizePool.Get(loot).Radius;

					if (MathHelpers.IsSpheresIntersect(playerPosition, alienPosition, playerSize, alienSize))
					{
						ref var collisionHittableWithBullet = ref collisionEventPool.Add(world.NewEntity());
						collisionHittableWithBullet.Player = world.PackEntity(player);
						collisionHittableWithBullet.Loot = world.PackEntity(loot);
					}
				}
			}
		}
	}
}