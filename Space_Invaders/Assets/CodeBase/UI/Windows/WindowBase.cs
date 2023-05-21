using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
	public abstract class WindowBase : MonoBehaviour
	{
		[SerializeField] private Button CloseButton;

		private void Awake() =>
			OnAwake();

		protected virtual void OnAwake() =>
			CloseButton.onClick.AddListener(() => Destroy(gameObject));


	}
}