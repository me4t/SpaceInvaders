using CodeBase.CompositionRoot;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
	public class GameLoopState : IState, ITickable,IFixedTickable
	{
		private readonly LoadingCurtain loadingCurtain;
		private IGameStateMachine gameStateMachine;

		public GameLoopState(IGameStateMachine gameStateMachine,LoadingCurtain loadingCurtain)
		{
			this.loadingCurtain = loadingCurtain;
		}

		public void Enter()
		{
			Debug.Log("GameLoopState");
			loadingCurtain.Hide();
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