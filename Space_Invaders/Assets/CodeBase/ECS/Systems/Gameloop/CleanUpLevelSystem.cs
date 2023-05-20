using CodeBase.Configs;
using CodeBase.ECS.Components;
using CodeBase.UI;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Gameloop
{
	public class CleanUpLevelSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IWindowService windowService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<GameOverRequest> gameOverPool;
		private EcsFilter gameOverFilter;


		public CleanUpLevelSystem(IWindowService windowService)
		{
			this.windowService = windowService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Player>().Inc<Dead>().End();
			gameOverFilter = world.Filter<GameOverRequest>().End();
			gameOverPool = world.GetPool<GameOverRequest>();
		}

		public void Run(IEcsSystems systems)
		{
			if (gameOverFilter.GetEntitiesCount() > 0) return;
			foreach (var entity in filter)
			{
				var newEntity = world.NewEntity();
				gameOverPool.Add(newEntity);
				windowService.Open(WindowId.GameOver);
			}
		}
	}
}