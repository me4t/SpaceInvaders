using System.Collections;
using UnityEngine;
using Zenject;

namespace CodeBase.CompositionRoot
{
	public class LoadingCurtain : MonoBehaviour
	{
		public CanvasGroup Curtain;
		private Coroutine coroutine;

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		public void Show()
		{
			KillOldCoroutine();
			gameObject.SetActive(true);
			Curtain.alpha = 1;
		}
    
		public void Hide()
		{
			KillOldCoroutine();
			coroutine = StartCoroutine(DoFadeIn());
		}

		private void KillOldCoroutine()
		{
			if (coroutine != null)
				StopCoroutine(coroutine);
		}

		private IEnumerator DoFadeIn()
		{
			while (Curtain.alpha > 0)
			{
				Curtain.alpha -= 0.03f;
				yield return new WaitForSeconds(0.03f);
			}
      
			gameObject.SetActive(false);
		}
	}
}