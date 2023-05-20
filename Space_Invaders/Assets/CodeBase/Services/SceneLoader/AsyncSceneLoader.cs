using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.SceneLoader
{
	public class AsyncSceneLoader:ISceneLoader
	{
		private readonly CoroutineRunner.CoroutineRunner coroutineRunner;

		public AsyncSceneLoader(CoroutineRunner.CoroutineRunner coroutineRunner) =>
			this.coroutineRunner = coroutineRunner;

		public void Load(string name, Action onLoaded = null) =>
			coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

		private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

			while (!waitNextScene.isDone)
				yield return null;

			Scene scene = SceneManager.GetSceneByName(nextScene);
			SceneManager.SetActiveScene(scene);

			onLoaded?.Invoke();
		}
	}

	public interface ISceneLoader
	{
		void Load(string name, Action onLoaded = null);
	}
}