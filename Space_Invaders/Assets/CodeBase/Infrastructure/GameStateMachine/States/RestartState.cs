using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class RestartState : IState, ITickable, IFixedTickable
	{
		private IGameStateMachine gameStateMachine;

		public RestartState(IGameStateMachine gameStateMachine)
		{
			this.gameStateMachine = gameStateMachine;
		}

		public void Enter()
		{
			gameStateMachine.Enter<CreateProgressState>();
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