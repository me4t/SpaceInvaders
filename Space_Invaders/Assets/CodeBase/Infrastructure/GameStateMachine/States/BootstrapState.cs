using CodeBase.Services.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class BootstrapState : IState
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService staticDataService;

		public BootstrapState(IGameStateMachine gameStateMachine,IStaticDataService staticDataService)
		{
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
		}

		public void Enter()
		{
			Debug.Log("Entered BootstrapState ");
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