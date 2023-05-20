using CodeBase.Infrastructure.GameStateMachine;
using CodeBase.Infrastructure.GameStateMachine.States;
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
			Container.BindFactory<IGameStateMachine, RestartState, RestartState.Factory>();
			Container.BindFactory<IGameStateMachine, CreateProgressState, CreateProgressState.Factory>();

			Container.Bind(typeof(IGameStateMachine), typeof(ITickable),typeof(IFixedTickable)).To<GameStateMachine>().AsSingle();
		}
	}
}