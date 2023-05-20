using CodeBase.Infrastructure.CoreEngine;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class GameLoopState : IState, ITickable,IFixedTickable
	{
		private readonly LoadingCurtain loadingCurtain;
		private readonly ICoreEngine coreEngine;
		private IGameStateMachine gameStateMachine;

		public GameLoopState(IGameStateMachine gameStateMachine,LoadingCurtain loadingCurtain,ICoreEngine coreEngine)
		{
			this.loadingCurtain = loadingCurtain;
			this.coreEngine = coreEngine;
		}

		public void Enter()
		{
			Debug.Log("GameLoopState");
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
			{
				Debug.Log("game over");
				return;
			}

			coreEngine.Tick();
		}


		public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
		{
		}
	}
}