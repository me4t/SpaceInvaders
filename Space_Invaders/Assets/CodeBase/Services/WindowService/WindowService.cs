using CodeBase.Enums;
using CodeBase.UI.UIFactory;

namespace CodeBase.Services.WindowService
{
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
}