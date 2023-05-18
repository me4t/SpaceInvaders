using CodeBase.ECS.Systems.Factories;
using CodeBase.Infrastructure.CoreEngine;
using CodeBase.Infrastructure.GameStateMachine.States;
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
			BindSystem<CreateLevelSystem>();
			BindSystem<CreateAlienViewSystem>();
			BindSystem<CreatePlayerViewSystem>();
			BindSystem<UpdateViewPositionSystem>();
		}

		void BindSystem<TSystem>() where TSystem : IEcsSystem => 
			Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();

		void BindFixedSystem<TSystem>() where TSystem : IFixedEcsSystem => 
			Container.Bind<IFixedEcsSystem>().To<TSystem>().AsTransient();
	}
}