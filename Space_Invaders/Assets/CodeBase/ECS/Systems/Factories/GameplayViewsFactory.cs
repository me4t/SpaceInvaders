using CodeBase.ECS.Systems.LevelCreate;

namespace CodeBase.ECS.Systems.Factories
{
	public interface IViewsFactory
	{
		AlienView CreateAlien(string path);
		
	}

	public class ViewsFactory : IViewsFactory
	{
		private readonly AlienView.Factory alienFactory;

		public ViewsFactory(AlienView.Factory alienFactory)
		{
			this.alienFactory = alienFactory;
		}
		public AlienView CreateAlien(string path) => 
			alienFactory.Create(path);
	}
}