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

	public class StandaloneInputService : IInputService
	{
		public float Horizontal => Input.GetAxis("Horizontal");
		public float Vertical => Input.GetAxis("Vertical");
		public Vector2 Axis => new Vector2(Horizontal, Vertical);
		public bool IsFire => Input.GetKeyDown(KeyCode.O);
	}

	public class TimeService : ITimeService
	{
		public float DeltaTime => Time.deltaTime;

	}

	public interface ITimeService
	{
		float DeltaTime { get; }
	}

	public interface IInputService
	{
		float Horizontal { get; }
		float Vertical { get; }
		Vector2 Axis { get;}
		bool IsFire { get;}
	}
}