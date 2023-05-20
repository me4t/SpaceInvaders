using System;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.SceneLoader
{
	public class SceneLoaderWebgl:ISceneLoader
	{
		public void Load(string nextScene, Action onLoaded = null)
		{
			SceneManager.LoadScene(nextScene);
			Scene scene = SceneManager.GetSceneByName(nextScene);
			SceneManager.SetActiveScene(scene);

			onLoaded?.Invoke();
		}
	}
}