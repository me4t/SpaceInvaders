using CodeBase.ECS.Systems.LevelCreate;
using UnityEngine.SceneManagement;

namespace CodeBase.ECS.Systems.Factories
{
	public interface IViewsFactory
	{
		IUnityObjectView CreateAlien(string path);
		IUnityObjectView CreatePlayer(string spawnEventPath);
		IUnityObjectView CreateBullet(string spawnEventPath);
		IUnityObjectView CreateLoot(string spawnEventPath);
	}

	public class ViewsFactory : IViewsFactory
	{
		private readonly AlienView.Factory alienFactory;
		private readonly PlayerView.Factory playerFactory;
		private readonly BulletView.Factory bulletFactory;
		private readonly LootView.Factory lootFactory;

		public ViewsFactory(AlienView.Factory alienFactory,PlayerView.Factory playerFactory,BulletView.Factory bulletFactory,LootView.Factory lootFactory)
		{
			this.alienFactory = alienFactory;
			this.playerFactory = playerFactory;
			this.bulletFactory = bulletFactory;
			this.lootFactory = lootFactory;
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

		public IUnityObjectView CreateBullet(string path)
		{
			var view = bulletFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}

		public IUnityObjectView CreateLoot(string path)
		{
			var view = lootFactory.Create(path);
			SceneManager.MoveGameObjectToScene(view.gameObject, SceneManager.GetActiveScene());
			return view;
		}
	}
}