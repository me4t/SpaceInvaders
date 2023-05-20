using CodeBase.Configs;
using CodeBase.Enums;
using CodeBase.Extensions;
using CodeBase.Services.StaticData;
using UnityEngine.SceneManagement;

namespace CodeBase.UI
{
	public class UIFactory : IUIFactory
	{
		private readonly IStaticDataService staticDataService;
		private readonly GameOverWindow.Factory gameWindowFactory;

		public UIFactory(IStaticDataService staticDataService,GameOverWindow.Factory  gameWindowFactory)
		{
			this.staticDataService = staticDataService;
			this.gameWindowFactory = gameWindowFactory;
		}

		public void CreateGameOverScreen()
		{
			var windowConfig = staticDataService.ForWindow(WindowId.GameOver);
			var gameOverWindow = gameWindowFactory.Create(windowConfig.Path.ConvertToString());
			SceneManager.MoveGameObjectToScene(gameOverWindow.gameObject, SceneManager.GetActiveScene());

		}
	}
}