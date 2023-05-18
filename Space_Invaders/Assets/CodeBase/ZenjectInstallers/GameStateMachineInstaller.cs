using CodeBase.ECS.Systems.Factories;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Infrastructure;
using Zenject;

namespace CodeBase.ZenjectInstallers
{
	public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindFactory<IGameStateMachine, BootstrapState, BootstrapState.Factory>();
			Container.BindFactory<IGameStateMachine, LoadLevelState, LoadLevelState.Factory>();
			Container.BindFactory<IGameStateMachine, GameLoopState, GameLoopState.Factory>();

			Container.Bind(typeof(IGameStateMachine), typeof(ITickable),typeof(IFixedTickable)).To<GameStateMachine>().AsSingle();
		}
	}
	public class ViewsFactoryInstaller : Installer<ViewsFactoryInstaller>
	{
		public override void InstallBindings()
		{
			AlienViewFactory();

			BindViewsFactory();
		}

		private void BindViewsFactory() => 
			Container.BindInterfacesTo<ViewsFactory>().AsSingle();

		private void AlienViewFactory()
		{
			Container
				.BindFactory<string, AlienView, AlienView.Factory>()
				.FromFactory<PrefabResourceFactory<AlienView>>();
		}
	}
}