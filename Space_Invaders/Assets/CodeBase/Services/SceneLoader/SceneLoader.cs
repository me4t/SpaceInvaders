using System;
using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.SceneLoader
{
	public class SceneLoader  
	{
		private readonly CoroutineRunner.CoroutineRunner coroutineRunner;

		public SceneLoader(CoroutineRunner.CoroutineRunner coroutineRunner) => 
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
}