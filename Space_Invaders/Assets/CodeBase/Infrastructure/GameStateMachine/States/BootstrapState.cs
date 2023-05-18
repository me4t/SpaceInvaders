using CodeBase.CompositionRoot;
using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
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