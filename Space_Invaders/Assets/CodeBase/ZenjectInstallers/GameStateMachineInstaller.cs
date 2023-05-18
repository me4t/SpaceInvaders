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
}