using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class CreateBulletViewSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IViewsFactory viewsFactory;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;

		public CreateBulletViewSystem(IViewsFactory viewsFactory)
		{
			this.viewsFactory = viewsFactory;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Bullet>().Exc<View>().End();

			viewPool = world.GetPool<View>();
			spawnEventPool = world.GetPool<SpawnEvent>();
			positionPool = world.GetPool<Position>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var spawnEvent = ref spawnEventPool.Get(entity);
				ref var view = ref viewPool.Add(entity);
				view.Value = viewsFactory.CreateBullet(spawnEvent.Path);
				view.Value.UpdatePosition(spawnEvent.Position);
				ref var position = ref positionPool.Add(entity);
				position.Value = spawnEvent.Position;
			}
		}
	}
}