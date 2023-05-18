using CodeBase.ECS.Systems.Factories;
using CodeBase.ECS.Systems.LevelCreate;
using Zenject;

namespace CodeBase.ZenjectInstallers
{
	public class ViewsFactoryInstaller : Installer<ViewsFactoryInstaller>
	{
		public override void InstallBindings()
		{
			AlienViewFactory();

			BindViewsFactory();
		}

		private void BindViewsFactory() => 
			Container.BindInterfacesTo<ViewsFactory>().AsSingle();

		private void AlienViewFactory()
		{
			Container
				.BindFactory<string, AlienView, AlienView.Factory>()
				.FromFactory<PrefabResourceFactory<AlienView>>();
		}
	}
}