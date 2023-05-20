using CodeBase.Infrastructure.CoreEngine;
using CodeBase.Infrastructure.GameStateMachine;
using CodeBase.Services.CoroutineRunner;
using CodeBase.Services.HudService;
using CodeBase.Services.Input;
using CodeBase.Services.ProgressService;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using CodeBase.Services.TimeService;
using CodeBase.Services.ViewsFactory;
using CodeBase.UI;
using CodeBase.ZenjectInstallers;
using UnityEngine;
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

		private void BindSceneLoader()
		{
			Container.Bind<ISceneLoader>().To<AsyncSceneLoader>().AsSingle().When(IsEditor);
			Container.Bind<ISceneLoader>().To<SceneLoaderWebgl>().AsSingle().When(IsWebgl);
		}

		private  bool IsEditor(InjectContext context)
		{
			return Application.isEditor;
		}
		private  bool IsWebgl(InjectContext context)
		{
#if UNITY_WEBGL && !UNITY_EDITOR
			return true;
#endif
			return false; 
		}

		private void BindInputService() => Container.BindInterfacesTo<StandaloneInputService>().AsSingle();

		private void BindHudService() =>
			Container
				.BindInterfacesTo<HudService>()
				.FromComponentInNewPrefabResource(Path.Hud)
				.AsSingle();
		
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