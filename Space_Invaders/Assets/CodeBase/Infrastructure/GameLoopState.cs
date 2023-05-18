using Zenject;

namespace CodeBase.Infrastructure
{
	public class GameLoopState : IState, ITickable,IFixedTickable
	{
		private IGameStateMachine gameStateMachine;

		public GameLoopState(IGameStateMachine gameStateMachine)
		{
		}

		public void Enter()
		{
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


		public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
		{
		}
	}
}