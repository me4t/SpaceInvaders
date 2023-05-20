using CodeBase.Services.ProgressService;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class CreateProgressState:IState
	{
		
		private readonly IGameStateMachine gameStateMachine;
		private readonly IPlayerProgressService playerProgressService;

		public CreateProgressState(IGameStateMachine gameStateMachine,IPlayerProgressService playerProgressService )
		{
			this.gameStateMachine = gameStateMachine;
			this.playerProgressService = playerProgressService;
		}
		public void Exit()
		{
		}

		public void Enter()
		{
			playerProgressService.Progress = new PlayerProgress();
			gameStateMachine.Enter<LoadLevelState, string>(Constants.MainSceneName);
		}
		public class Factory : PlaceholderFactory<IGameStateMachine, CreateProgressState>
		{
		}
	}
}