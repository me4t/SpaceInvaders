using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.ECS.Systems.Factories
{
	public class PlayerShootSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IViewsFactory viewsFactory;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<View> viewPool;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<FlyTo> flyToPool;
		private EcsPool<Gun> gunPool;

		public PlayerShootSystem(IViewsFactory viewsFactory,IStaticDataService staticDataService)
		{
			this.viewsFactory = viewsFactory;
			this.staticDataService = staticDataService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Player>().Inc<View>().End();

			viewPool = world.GetPool<View>();
			spawnEventPool = world.GetPool<SpawnEvent>();
			positionPool = world.GetPool<Position>();
			flyToPool = world.GetPool<FlyTo>();
			gunPool = world.GetPool<Gun>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				if (Input.GetKeyDown(KeyCode.O))
				{
					var bulletConfig = staticDataService.ForBullet(BulletType.Ice);
					var bulletData = new BulletCreateData();
					bulletData.Speed = bulletConfig.Speed;
					bulletData.PrefabPath = bulletConfig.Path.ConvertToString();
					bulletData.BodySize = bulletConfig.Size;
					bulletData.Damage = bulletConfig.Damage;
					ref var position = ref positionPool.Get(entity);
					bulletData.Position = position.Value;
					var bulletEntity = BulletFactory.Create(world,bulletData);
					ref var flyTo = ref flyToPool.Add(bulletEntity);
					ref var playerGun = ref gunPool.Get(entity);

					
					flyTo.Value = playerGun.Direction;
				}
			}
		}
	}
}