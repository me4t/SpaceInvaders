using CodeBase.ECS.Components;
using CodeBase.Services.HudService;
using CodeBase.Services.ProgressService;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Gameloop
{
	public class UpdateRoundSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IHudService hudService;
		private readonly IPlayerProgressService playerProgressService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;


		public UpdateRoundSystem(IHudService hudService,IPlayerProgressService playerProgressService)
		{
			this.hudService = hudService;
			this.playerProgressService = playerProgressService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<NextRoundEvent>().End();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				var playerProgress = playerProgressService.Progress;
				hudService.UpdateRound(playerProgress.RoundCount);
				world.DelEntity(entity);
			}

		}
	}
}