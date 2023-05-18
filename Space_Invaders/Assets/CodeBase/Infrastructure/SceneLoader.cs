using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure
{
	public class SceneLoader  
	{
		private readonly CoroutineRunner coroutineRunner;

		public SceneLoader(CoroutineRunner coroutineRunner) => 
			this.coroutineRunner = coroutineRunner;

		public void Load(string name, Action onLoaded = null) =>
			coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    
		private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

			while (!waitNextScene.isDone)
				yield return null;
            
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
            
			onLoaded?.Invoke();
		}
	}

	public interface IStaticDataService 
	{
		
	}
	public class LoadLevelState : IPaylodedState<string>
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService staticDataService;

		public LoadLevelState(IGameStateMachine gameStateMachine, 
			IStaticDataService staticDataService)
		{
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
		}

		public void Enter(string sceneName)
		{
		}

		public void Exit()
		{
		}

		private void OnLoaded()
		{
		}


		public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
		{
		}
	}
}