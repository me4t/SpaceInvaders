using CodeBase.Services.StaticData;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class BootstrapState : IState
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService staticDataService;

		public BootstrapState(IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
		{
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
		}

		public void Enter()
		{
			InitializeServices();
			gameStateMachine.Enter<CreateProgressState>();
		}

		private void InitializeServices()
		{
			staticDataService.Load();
		}

		public void Exit()
		{
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
		{
		}
	}
}