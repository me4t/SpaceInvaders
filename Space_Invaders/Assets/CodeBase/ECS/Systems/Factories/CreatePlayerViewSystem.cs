using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class CreatePlayerViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IViewsFactory viewsFactory;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Player> playerPool;

		public CreatePlayerViewSystem(IViewsFactory viewsFactory)
		{
			this.viewsFactory = viewsFactory;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Player>().Exc<View>().End();

			viewPool = world.GetPool<View>();
			playerPool = world.GetPool<Player>();
			spawnEventPool = world.GetPool<SpawnEvent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var alien = ref playerPool.Get(entity);
				ref var spawnEvent = ref spawnEventPool.Get(entity);
				var alienView = viewsFactory.CreatePlayer(spawnEvent.Path);
				
				ref var view = ref viewPool.Add(entity);
				view.Value = alienView;
				view.Value.UpdatePosition(spawnEvent.Position);
			}
		}
	}
}