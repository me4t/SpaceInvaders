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
			BindTimeService();
			BindInputService();
			BindCoroutineRunner();
			BindSceneLoader();
			BindLoadingCurtain();
			BindStaticData();

			BindViewsFactory();
			
			BindGameplayEngine();

			BindGameStateMachine();
		}
		

		private void BindStaticData() => Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
		private void BindTimeService() => Container.Bind<ITimeService>().To<TimeService>().AsSingle();

		private void BindSceneLoader() => Container.Bind<SceneLoader>().AsSingle();
		private void BindInputService() => Container.BindInterfacesTo<StandaloneInputService>().AsSingle();

		private void BindCoroutineRunner() =>
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
	}
}