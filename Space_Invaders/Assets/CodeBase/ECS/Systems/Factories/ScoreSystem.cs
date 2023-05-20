using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class ScoreSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IHudService hudService;
		private readonly IPlayerProgressService playerProgressService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private float delay;
		private EcsPool<UpdateScore> updateScorePool;


		public ScoreSystem(IHudService hudService,IPlayerProgressService playerProgressService)
		{
			this.hudService = hudService;
			this.playerProgressService = playerProgressService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<UpdateScore>().End();
			updateScorePool = world.GetPool<UpdateScore>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var updateScore = ref updateScorePool.Get(entity);
				playerProgressService.Progress.Score += updateScore.Value;
				world.DelEntity(entity);

			}
			hudService.UpdateScore(playerProgressService.Progress.Score);
		}
	}
}