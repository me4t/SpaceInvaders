using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class CreateLootViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IViewsFactory viewsFactory;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Loot> lootPool;
		private EcsPool<Position> positionPool;

		public CreateLootViewSystem(IViewsFactory viewsFactory)
		{
			this.viewsFactory = viewsFactory;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Loot>().Exc<View>().End();

			viewPool = world.GetPool<View>();
			lootPool = world.GetPool<Loot>();
			spawnEventPool = world.GetPool<SpawnEvent>();
			positionPool = world.GetPool<Position>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var alien = ref lootPool.Get(entity);
				ref var spawnEvent = ref spawnEventPool.Get(entity);
				var alienView = viewsFactory.CreateLoot(spawnEvent.Path);

				ref var view = ref viewPool.Add(entity);
				view.Value = alienView;
				view.Value.UpdatePosition(spawnEvent.Position);
				ref var position = ref positionPool.Add(entity);
				position.Value = spawnEvent.Position;
				
				spawnEventPool.Del(entity);
			}
		}
	}
}