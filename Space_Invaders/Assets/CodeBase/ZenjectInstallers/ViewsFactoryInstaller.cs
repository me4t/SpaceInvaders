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
			PlayerViewFactory();
			BulletViewFactory();
			LootViewFactory();
			
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
		private void PlayerViewFactory()
		{
			Container
				.BindFactory<string, PlayerView, PlayerView.Factory>()
				.FromFactory<PrefabResourceFactory<PlayerView>>();
		}
		private void BulletViewFactory()
		{
			Container
				.BindFactory<string, BulletView, BulletView.Factory>()
				.FromFactory<PrefabResourceFactory<BulletView>>();
		}
		private void LootViewFactory()
		{
			Container
				.BindFactory<string, LootView, LootView.Factory>()
				.FromFactory<PrefabResourceFactory<LootView>>();
		}
	}
}