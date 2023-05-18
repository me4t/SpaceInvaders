using CodeBase.CompositionRoot;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService staticDataService;
		private readonly LoadingCurtain loadingCurtain;

		public BootstrapState(IGameStateMachine gameStateMachine,IStaticDataService staticDataService,LoadingCurtain loadingCurtain)
		{
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
			this.loadingCurtain = loadingCurtain;
		}

		public void Enter()
		{
			Debug.Log("Entered BootstrapState ");
			InitializeServices();
			gameStateMachine.Enter<LoadLevelState, string>(Constants.MainSceneName);
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