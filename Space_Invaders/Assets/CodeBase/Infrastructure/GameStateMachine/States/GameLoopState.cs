using CodeBase.CompositionRoot;
using CodeBase.Infrastructure.CoreEngine;
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
			
		} 

		public void FixedTick()
		{
		}

		public void Tick() => coreEngine.Tick();


		public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
		{
		}
	}
}