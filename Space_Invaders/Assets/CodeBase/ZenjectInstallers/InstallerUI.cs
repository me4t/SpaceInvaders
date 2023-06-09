using CodeBase.Services.WindowService;
using CodeBase.UI;
using CodeBase.UI.UIFactory;
using CodeBase.UI.Windows;
using Zenject;

namespace CodeBase.ZenjectInstallers
{
	public class InstallerUI : Installer<InstallerUI>
	{
		public override void InstallBindings()
		{
			BindGameOverFactory();
			BindUIFactory();
			BindUIWindowsService();
		}

		private void BindUIFactory() => Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

		private void BindGameOverFactory() => Container
			.BindFactory<string, GameOverWindow, GameOverWindow.Factory>()
			.FromFactory<PrefabResourceFactory<GameOverWindow>>();

		private void BindUIWindowsService() => Container.Bind<IWindowService>().To<WindowService>().AsSingle();
	}
}