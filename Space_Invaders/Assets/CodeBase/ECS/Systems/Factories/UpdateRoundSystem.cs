using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class UpdateRoundSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IHudService hudService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private float delay;
		private EcsPool<NextRoundEvent> updateScorePool;
		private int current;


		public UpdateRoundSystem(IHudService hudService)
		{
			this.hudService = hudService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<NextRoundEvent>().End();
			updateScorePool = world.GetPool<NextRoundEvent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var updateScore = ref updateScorePool.Get(entity);
				current++;
				world.DelEntity(entity);
			}

			hudService.UpdateRound(current);
		}
	}
}