using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
	public class BootstrapState : IState
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService _staticDataService;

		public BootstrapState(IGameStateMachine gameStateMachine,IStaticDataService staticDataService)
		{
			this.gameStateMachine = gameStateMachine;
			_staticDataService = staticDataService;
		}

		public void Enter()
		{
			Debug.Log("Entered BootstrapState ");
		}

		private void InitializeServices()
		{
		}

		public void Exit()
		{
            
		}

		public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
		{
		}
	}
}