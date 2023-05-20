using CodeBase.Configs;
using CodeBase.ECS.Components;
using CodeBase.UI;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Gameloop
{
	public class CheckGameOverSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IWindowService windowService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<GameOverRequest> gameOverPool;
		private EcsFilter gameOverFilter;
		private EcsFilter aliensFilter;
		private EcsPool<Position> positionPool;


		public CheckGameOverSystem(IWindowService windowService)
		{
			this.windowService = windowService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Player>().Inc<Dead>().End();
			gameOverFilter = world.Filter<GameOverRequest>().End();
			gameOverPool = world.GetPool<GameOverRequest>();
			positionPool = world.GetPool<Position>();
			aliensFilter = world.Filter<Components.Alien>().Inc<View>().Exc<Dead>().End();

		}

		public void Run(IEcsSystems systems)
		{
			if (gameOverFilter.GetEntitiesCount() > 0) return;
			foreach (var entity in filter)
			{
				GameOver();
				return;
			}
			foreach (var alien in aliensFilter)
			{
				ref var position = ref positionPool.Get(alien);
				if (position.Value.z < Constants.MinBoardLimitZ + Constants.OffsetForBorder)
				{
					GameOver();
					return;
				}
			}
		}

		private void GameOver()
		{
			gameOverPool.Add(world.NewEntity());
			windowService.Open(WindowId.GameOver);
		}
	}
}