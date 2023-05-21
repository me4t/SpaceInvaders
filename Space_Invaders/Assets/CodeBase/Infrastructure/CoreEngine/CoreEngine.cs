using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.ECS.Components;
using CodeBase.ECS.Factories;
using Leopotam.EcsLite;

namespace CodeBase.Infrastructure.CoreEngine
{
	public class CoreEngine : ICoreEngine
	{
		private EcsWorld world;
		private EcsSystems systems;
		private IEnumerable<IEcsSystem> bindedSystems;
		private EcsFilter gameOverFilter;

		public CoreEngine(IEnumerable<IEcsSystem> bindedSystems)
		{
			this.bindedSystems = bindedSystems;
		}

		public void InitSession(LevelData levelConfig)
		{
			Initialize();
			CreateLevelFromTemplate(levelConfig);
			CreateGameOverFilter();
		}

		public void Tick()
		{
			systems?.Run();
		}

		private void CreateGameOverFilter() =>
			gameOverFilter = world.Filter<GameOverRequest>().End();

		public bool GameOver =>
			gameOverFilter.GetEntitiesCount() > 0;

		private void Initialize()
		{
			world = new EcsWorld();
			systems = new EcsSystems(world);


			InitializeSystems();
		}

		private void InitializeSystems()
		{
			foreach (var system in bindedSystems)
				systems.Add(system);


			systems.Init();
		}

		void CreateLevelFromTemplate(LevelData config) =>
			LevelRequestFactory.Create(world, config, true);
	}
}