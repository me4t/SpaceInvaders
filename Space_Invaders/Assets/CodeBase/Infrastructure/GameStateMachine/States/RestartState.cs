using CodeBase.Infrastructure.CoreEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class RestartState : IState, ITickable, IFixedTickable
	{
		private readonly ICoreEngine coreEngine;
		private IGameStateMachine gameStateMachine;

		public RestartState(IGameStateMachine gameStateMachine,  ICoreEngine coreEngine)
		{
			this.gameStateMachine = gameStateMachine;
			this.coreEngine = coreEngine;
		}

		public void Enter()
		{
			CleanUp();
			gameStateMachine.Enter<CreateProgressState>();
		}

		private void CleanUp()
		{
			//coreEngine.Cleanup();
		}

		public void Exit()
		{
		}

		public void FixedTick()
		{
		}

		public void Tick()
		{
		}


		public class Factory : PlaceholderFactory<IGameStateMachine, RestartState>
		{
		}
	}
}