using CodeBase.Infrastructure.CoreEngine;
using CodeBase.UI;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class GameLoopState : IState, ITickable, IFixedTickable
	{
		private readonly LoadingCurtain loadingCurtain;
		private readonly ICoreEngine coreEngine;
		private IGameStateMachine gameStateMachine;

		public GameLoopState(IGameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, ICoreEngine coreEngine)
		{
			this.loadingCurtain = loadingCurtain;
			this.coreEngine = coreEngine;
		}

		public void Enter()
		{
			loadingCurtain.Hide();
		}

		public void Exit()
		{
			loadingCurtain.Show();
		}

		public void FixedTick()
		{
		}

		public void Tick()
		{
			if (coreEngine.GameOver)
				return;

			coreEngine.Tick();
		}


		public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
		{
		}
	}
}