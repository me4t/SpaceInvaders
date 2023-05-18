using CodeBase.ECS.Systems.Factories;
using CodeBase.Infrastructure;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using Zenject;

namespace CodeBase.CompositionRoot
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
			BindSystem<CreateLevelSystem>();
			BindSystem<CreateAlienViewSystem>();
		}

		void BindSystem<TSystem>() where TSystem : IEcsSystem => 
			Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();

		void BindFixedSystem<TSystem>() where TSystem : IFixedEcsSystem => 
			Container.Bind<IFixedEcsSystem>().To<TSystem>().AsTransient();
	}
}