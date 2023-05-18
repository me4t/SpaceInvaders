using CodeBase.ECS.Systems.LevelCreate;
using UnityEngine.SceneManagement;

namespace CodeBase.ECS.Systems.Factories
{
	public interface IViewsFactory
	{
		IUnityObjectView CreateAlien(string path);

		IUnityObjectView CreatePlayer(string spawnEventPath);
	}

	public class ViewsFactory : IViewsFactory
	{
		private readonly AlienView.Factory alienFactory;
		private readonly PlayerView.Factory playerFactory;

		public ViewsFactory(AlienView.Factory alienFactory,PlayerView.Factory playerFactory)
		{
			this.alienFactory = alienFactory;
			this.playerFactory = playerFactory;
		}
		public IUnityObjectView CreateAlien(string path)
		{
			var view = alienFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}

		public IUnityObjectView CreatePlayer(string path)
		{
			var view = playerFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}
	}
}