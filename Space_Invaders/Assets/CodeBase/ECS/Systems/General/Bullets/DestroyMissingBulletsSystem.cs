using CodeBase.ECS.Components;
using CodeBase.Extensions;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.General.Bullets
{
	public class DestroyMissingBulletsSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsFilter filterRound;
		private EcsPool<Position> positionPool;
		private EcsFilter filterPlayer;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().End();
			filterPlayer = world.Filter<Components.Player>().End();
			positionPool = world.GetPool<Position>();
		}


		public void Run(IEcsSystems systems)
		{
			foreach (var player in filterPlayer)
			{
				ref var playerPosition = ref positionPool.Get(player);

				foreach (var missing in filter)
				{
					ref var position = ref positionPool.Get(missing);
					if (MathHelpers.Distance(position.Value, playerPosition.Value) > Constants.DestroyBulletsDistance)
						world.DelEntity(missing);
				}
			}
		}
	}
}