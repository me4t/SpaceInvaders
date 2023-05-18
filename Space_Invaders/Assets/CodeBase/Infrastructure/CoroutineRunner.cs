using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class CoroutineRunner : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}
	}
}