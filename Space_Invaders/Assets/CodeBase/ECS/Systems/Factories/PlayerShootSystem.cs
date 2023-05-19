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
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				if (inputService.IsFire)
				{
					ref var bulletOwner = ref bulletOwnerPool.Get(entity);
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
				}
			}
		}
	}
}