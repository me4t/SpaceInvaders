using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CodeBase.Services.SceneLoader
{
	public class SceneLoaderWebgl:ISceneLoader
	{
		private readonly CoroutineRunner.CoroutineRunner coroutineRunner;

		public SceneLoaderWebgl(CoroutineRunner.CoroutineRunner coroutineRunner) =>
			this.coroutineRunner = coroutineRunner;

		public void Load(string name, Action onLoaded = null) => 
			coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

		private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
		{
		 SceneManager.LoadScene(Constants.CleanUpScene,LoadSceneMode.Single);
		 Scene clear = SceneManager.GetSceneByName(Constants.CleanUpScene);
		 
		 while (!clear.isLoaded)
			 yield return null;
		 
		 SceneManager.SetActiveScene(clear);
		 SceneManager.LoadScene(nextScene,LoadSceneMode.Single);
		 
		 Scene scene = SceneManager.GetSceneByName(nextScene);
			while (!scene.isLoaded)
				yield return null;

			SceneManager.SetActiveScene(scene);
			onLoaded?.Invoke();
		}
	}
}