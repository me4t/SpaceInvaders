using CodeBase.ECS.Systems.Alien;
using CodeBase.ECS.Systems.Create;
using CodeBase.ECS.Systems.Gameloop;
using CodeBase.ECS.Systems.General;
using CodeBase.ECS.Systems.General.Bullets;
using CodeBase.ECS.Systems.General.Collisions;
using CodeBase.ECS.Systems.Player;
using CodeBase.ECS.Systems.UI;
using CodeBase.Infrastructure.CoreEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using Zenject;

namespace CodeBase.ZenjectInstallers
{
	public class InstallerECS : Installer<InstallerECS>
	{
		public override void InstallBindings()
		{
			BindEcsSystems();

			Container.BindInterfacesTo<CoreEngine>().AsSingle();
		}

		private void BindEcsSystems()
		{
			BindCommonSystems();
			BindClientSystems();

#if UNITY_EDITOR
				BindDebugSystems();
#endif
            
		}


#if UNITY_EDITOR
		private void BindDebugSystems() => 
			BindSystem<EcsWorldDebugSystem>(); 
#endif
        

		private void BindClientSystems()
		{
          
           
		}

		private void BindCommonSystems()
		{
			BindSystem<PlayerMoveSystem>();
			BindSystem<MovementTimerSystem>();
			BindSystem<MoveAliensSystem>();

			BindSystem<CreateRoundSystem>();
			BindSystem<CreateLootViewSystem>();
			BindSystem<CreateAlienViewSystem>();
			BindSystem<CreatePlayerViewSystem>();
			BindSystem<PlayerShootSystem>();
			BindSystem<CreateBulletViewSystem>();
			BindSystem<FlySystem>();
			BindSystem<CheckPlayerCollisionWithAlienSystem>();
			BindSystem<CheckBulletCollisionSystem>();
			BindSystem<CheckLootCollisionSystem>();
			BindSystem<PickUpLootSystem>();
			BindSystem<HittableSystem>();
			BindSystem<DamageSystem>();
			BindSystem<LootSpawnSystem>();
			BindSystem<DeathSystem>();
			BindSystem<CheckRoundComplete>();
			BindSystem<ScoreSystem>();
			BindSystem<SetNextRoundSystem>();
			BindSystem<UpdateRoundSystem>();
			BindSystem<HpBarSystem>();
			BindSystem<UpdateBulletCountSystem>();
			BindSystem<DestroyViewAliensSystem>();
			BindSystem<DestroyMissingBulletsSystem>();
			BindSystem<UpdateViewPositionSystem>();
			BindSystem<CheckGameOverSystem>();
			BindSystem<CleanUpSystem>();
		}

		void BindSystem<TSystem>() where TSystem : IEcsSystem => 
			Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();
	}
}