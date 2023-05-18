using CodeBase.Infrastructure;
using Zenject;

namespace CodeBase.CompositionRoot
{
	public class GameInstaller:MonoInstaller
	{
		public override void InstallBindings()
		{
			BindGameBootstrapperFactory();			
		}

		private void BindGameBootstrapperFactory()
		{
			Container
				.BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
				.FromComponentInNewPrefabResource(Path.GameBootstrapper);
		}
	}

	public static class Path
	{
		public static string GameBootstrapper = "Infrastructure/Bootstrapper";
	}

}