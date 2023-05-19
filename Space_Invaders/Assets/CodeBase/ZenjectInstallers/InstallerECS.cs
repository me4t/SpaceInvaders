using CodeBase.ECS.Systems.Factories;
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
			BindSystem<CheckCollisionSystem>();
			BindSystem<DamageSystem>();
			BindSystem<LootSpawnSystem>();
			BindSystem<DeathSystem>();
			BindSystem<CheckRoundComplete>();
			BindSystem<ScoreSystem>();
			BindSystem<SetNextRoundSystem>();
			BindSystem<UpdateRoundSystem>();
			BindSystem<DestroyViewAliensSystem>();
			BindSystem<UpdateViewPositionSystem>();
			BindSystem<CleanUpSystem>();
		}

		void BindSystem<TSystem>() where TSystem : IEcsSystem => 
			Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();
	}
}