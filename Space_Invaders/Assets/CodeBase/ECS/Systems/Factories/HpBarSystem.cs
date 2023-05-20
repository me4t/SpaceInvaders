using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class HpBarSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IHudService hudService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<Health> healthPool;


		public HpBarSystem(IHudService hudService)
		{
			this.hudService = hudService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Player>().End();
			healthPool = world.GetPool<Health>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var health = ref healthPool.Get(entity);
				hudService.UpdateHp(health.Current, health.Max);
			}
		}
	}
}