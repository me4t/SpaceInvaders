using CodeBase.ECS.Systems.Factories;
using CodeBase.Infrastructure.CoreEngine;
using CodeBase.Infrastructure.GameStateMachine;
using CodeBase.Services.CoroutineRunner;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using CodeBase.ZenjectInstallers;
using Zenject;

namespace CodeBase.CompositionRoot
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindPlayerProgressService();
			BindTimeService();
			BindInputService();
			BindCoroutineRunner();
			BindHudService();
			BindSceneLoader();
			BindLoadingCurtain();
			BindStaticData();
			BindUI();

			BindViewsFactory();
			
			BindGameplayEngine();

			BindGameStateMachine();
		}
		

		private void BindStaticData() => Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
		private void BindPlayerProgressService() => Container.Bind<IPlayerProgressService>().To<PlayerProgressService>().AsSingle();
		private void BindTimeService() => Container.Bind<ITimeService>().To<TimeService>().AsSingle();

		private void BindSceneLoader() => Container.Bind<SceneLoader>().AsSingle();
		private void BindInputService() => Container.BindInterfacesTo<StandaloneInputService>().AsSingle();

		private void BindCoroutineRunner() =>
			Container
				.BindInterfacesTo<HudService>()
				.FromComponentInNewPrefabResource(Path.Hud)
				.AsSingle();
		
		private void BindHudService() =>
			Container
				.Bind<CoroutineRunner>()
				.FromComponentInNewPrefabResource(Path.CoroutineRunner)
				.AsSingle();

		private void BindGameStateMachine()
		{
			Container
				.Bind(typeof(IGameStateMachine), typeof(ITickable))
				.FromSubContainerResolve()
				.ByInstaller<GameStateMachineInstaller>()
				.WithKernel()
				.AsSingle();
		}

		private void BindLoadingCurtain()
		{
			Container
				.Bind<LoadingCurtain>()
				.FromComponentInNewPrefabResource(Path.Curtain)
				.AsSingle();
		}
		private void BindGameplayEngine()
		{
			Container
				.Bind<ICoreEngine>()
				.FromSubContainerResolve()
				.ByInstaller<InstallerECS>()
				.WithKernel()
				.AsSingle();
		}
		private void BindViewsFactory()
		{
			Container
				.Bind<IViewsFactory>()
				.FromSubContainerResolve()
				.ByInstaller<ViewsFactoryInstaller>()
				.WithKernel()
				.AsSingle();
		}
		private void BindUI()
		{
			Container
				.Bind<IWindowService>()
				.FromSubContainerResolve()
				.ByInstaller<InstallerUI>()
				.WithKernel()
				.AsSingle();
		}
	}
}