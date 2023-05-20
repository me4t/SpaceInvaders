using CodeBase.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
	public abstract class WindowBase : MonoBehaviour
	{
		[SerializeField] private Button CloseButton;

		private void Awake() =>
			OnAwake();

		private void Start()
		{
			Initialize();
		}

		protected virtual void OnAwake() =>
			CloseButton.onClick.AddListener(() => Destroy(gameObject));

		protected virtual void Initialize()
		{
		}

	}

	public interface IUIFactory
	{
		void CreateGameOverScreen();
	}

	public class WindowService : IWindowService
	{
		private readonly IUIFactory _uiFactory;

		public WindowService(IUIFactory uiFactory)
		{
			_uiFactory = uiFactory;
		}

		public void Open(WindowId windowId)
		{
			switch (windowId)
			{
				case WindowId.None:
					break;
				case WindowId.GameOver:
					_uiFactory.CreateGameOverScreen();
					break;
			}
		}
	}

	public interface IWindowService
	{
		void Open(WindowId windowId);
	}
}