using CodeBase.Infrastructure;
using CodeBase.ZenjectInstallers;
using Zenject;

namespace CodeBase.CompositionRoot
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindCoroutineRunner();
			BindSceneLoader();
			BindLoadingCurtain();
			BindStaticData();
			BindGameStateMachine();
		}
		private void BindStaticData() => Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();

		private void BindSceneLoader() => Container.Bind<SceneLoader>().AsSingle();

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
	}
}