using System.Collections.Generic;
using CodeBase.ECS.Components;
using CodeBase.ECS.Factories;
using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Services.Input;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Player
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

		public PlayerShootSystem(IStaticDataService staticDataService, IInputService inputService)
		{
			this.staticDataService = staticDataService;
			this.inputService = inputService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Player>().Inc<View>().End();

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

				if (inputService.IsIceFire)
					Shoot(BulletType.Ice, entity, bulletOwner.Bullets);
				else if (inputService.IsFireMagic)
					Shoot(BulletType.Fire, entity, bulletOwner.Bullets);
			}
		}

		private void Shoot(BulletType bullet, int entity, Dictionary<BulletType, int> bullets)
		{
			if (NoAmmo(bullet, bullets)) return;

			var bulletData = staticDataService.ForBullet(bullet).DeepCopy();
			ref var position = ref positionPool.Get(entity);
			bulletData.Position = position.Value;
			var bulletEntity = BulletFactory.Create(world, bulletData);
			ref var flyTo = ref flyToPool.Add(bulletEntity);
			ref var playerGun = ref gunPool.Get(entity);

			flyTo.Value = playerGun.Direction;
			bullets[bullet] -= 1;
			shootEventPool.Add(entity);
		}

		private bool NoAmmo(BulletType bullet, Dictionary<BulletType, int> bullets)
		{
			if (!bullets.ContainsKey(bullet)) return true;
			if (bullets[bullet] == 0) return true;
			return false;
		}
	}
}