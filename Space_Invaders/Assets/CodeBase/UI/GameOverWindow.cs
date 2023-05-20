using CodeBase.Infrastructure.GameStateMachine;
using CodeBase.Infrastructure.GameStateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
	public class GameOverWindow : WindowBase
	{
		[SerializeField] private Button restartButton;
		private IGameStateMachine gameStateMachine;

		protected override void OnAwake() => 
			restartButton.onClick.AddListener(Restart);

		[Inject]
		public void Construct(IGameStateMachine gameStateMachine)
		{
			this.gameStateMachine = gameStateMachine;
		}

		private void Restart() => 
			gameStateMachine.Enter<RestartState>();

		public class Factory : PlaceholderFactory<string, GameOverWindow>
		{
		}
		
	}
}