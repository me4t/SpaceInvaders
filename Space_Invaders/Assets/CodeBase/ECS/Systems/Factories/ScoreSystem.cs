using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class ScoreSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IHudService hudService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private float delay;
		private EcsPool<UpdateScore> updateScorePool;
		private float current;


		public ScoreSystem(IHudService hudService)
		{
			this.hudService = hudService;
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
				current += updateScore.Value;
				world.DelEntity(entity);

			}

			hudService.UpdateScore(current);
		}
	}
}