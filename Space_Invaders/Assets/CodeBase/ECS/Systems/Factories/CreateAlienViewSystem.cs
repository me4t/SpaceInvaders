using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class CreateAlienViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IViewsFactory viewsFactory;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Alien> alienPool;

		public CreateAlienViewSystem(IViewsFactory viewsFactory)
		{
			this.viewsFactory = viewsFactory;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Alien>().Exc<View>().Exc<Dead>().End();
			viewPool = world.GetPool<View>();
			spawnEventPool = world.GetPool<SpawnEvent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var spawnEvent = ref spawnEventPool.Get(entity);
				ref var view = ref viewPool.Add(entity);
				view.Value = viewsFactory.CreateAlien(spawnEvent.Path);
				view.Value.UpdatePosition(spawnEvent.Position);
				spawnEventPool.Del(entity);
			}
		}
	}
}