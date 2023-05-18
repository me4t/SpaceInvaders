using Zenject;

namespace CodeBase.Infrastructure
{
	public class LoadLevelState : IPaylodedState<string>
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService staticDataService;

		public LoadLevelState(IGameStateMachine gameStateMachine, 
			IStaticDataService staticDataService)
		{
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
		}

		public void Enter(string sceneName)
		{
		}

		public void Exit()
		{
		}

		private void OnLoaded()
		{
		}


		public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
		{
		}
	}
}