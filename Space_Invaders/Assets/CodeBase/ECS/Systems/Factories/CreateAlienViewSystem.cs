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
		private EcsPool<Position> positionPool;

		public CreateAlienViewSystem(IViewsFactory viewsFactory)
		{
			this.viewsFactory = viewsFactory;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Alien>().Exc<View>().Exc<Dead>().End();

			viewPool = world.GetPool<View>();
			alienPool = world.GetPool<Alien>();
			spawnEventPool = world.GetPool<SpawnEvent>();
			positionPool = world.GetPool<Position>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var alien = ref alienPool.Get(entity);
				ref var spawnEvent = ref spawnEventPool.Get(entity);
				var alienView = viewsFactory.CreateAlien(spawnEvent.Path);
				
				ref var view = ref viewPool.Add(entity);
				view.Value = alienView;
				view.Value.UpdatePosition(spawnEvent.Position);
				ref var position = ref positionPool.Add(entity);
				position.Value = spawnEvent.Position;
			}
		}
	}
}