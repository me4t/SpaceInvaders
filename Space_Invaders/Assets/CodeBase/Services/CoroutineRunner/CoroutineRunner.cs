using UnityEngine;

namespace CodeBase.Services.CoroutineRunner
{
	public class CoroutineRunner : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}
	}
}