using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class UpdateBulletCountSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IHudService hudService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<BulletOwner> bulletOwnerPool;
		private EcsFilter filterPlayer;
		private EcsPool<UpdateBulletCountEvent> shootEventPool;


		public UpdateBulletCountSystem(IHudService hudService)
		{
			this.hudService = hudService;
		}

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Player>().Inc<UpdateBulletCountEvent>().End();
			filterPlayer = world.Filter<Player>().End();
			bulletOwnerPool = world.GetPool<BulletOwner>();
			shootEventPool = world.GetPool<UpdateBulletCountEvent>();
			
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var bulletOwner = ref bulletOwnerPool.Get(entity);
				shootEventPool.Del(entity);
				
				hudService.UpdateBullets(bulletOwner.BulletCount,bulletOwner.Type);
			}
		}
	}
}