using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class PlayerShootSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private readonly IInputService inputService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<Position> positionPool;
		private EcsPool<FlyTo> flyToPool;
		private EcsPool<Gun> gunPool;
		private EcsPool<BulletOwner> bulletOwnerPool;
		private EcsPool<UpdateBulletCountEvent> shootEventPool;

		public PlayerShootSystem(IStaticDataService staticDataService,IInputService inputService)
		{
			this.staticDataService = staticDataService;
			this.inputService = inputService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Player>().Inc<View>().End();

			positionPool = world.GetPool<Position>();
			flyToPool = world.GetPool<FlyTo>();
			gunPool = world.GetPool<Gun>();
			bulletOwnerPool = world.GetPool<BulletOwner>();
			shootEventPool = world.GetPool<UpdateBulletCountEvent>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var bulletOwner = ref bulletOwnerPool.Get(entity);

				if (CanFire(bulletOwner))
				{
					var bulletConfig = staticDataService.ForBullet(bulletOwner.Type);
					var bulletData = new BulletCreateData();
					bulletData.Speed = bulletConfig.Speed;
					bulletData.PrefabPath = bulletConfig.Path.ConvertToString();
					bulletData.BodySize = bulletConfig.Size;
					bulletData.Damage = bulletConfig.Damage;
					
					ref var position = ref positionPool.Get(entity);
					bulletData.Position = position.Value;
					var bulletEntity = BulletFactory.Create(world, bulletData);
					ref var flyTo = ref flyToPool.Add(bulletEntity);
					ref var playerGun = ref gunPool.Get(entity);

					flyTo.Value = playerGun.Direction;
					bulletOwner.BulletCount--;
					shootEventPool.Add(entity);
				}
			}
		}

		private bool CanFire(BulletOwner bulletOwner)
		{
			return inputService.IsFire && bulletOwner.BulletCount > 0;
		}
	}
}