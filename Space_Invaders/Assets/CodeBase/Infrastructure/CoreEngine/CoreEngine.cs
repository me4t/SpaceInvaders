using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.ECS.Systems.LevelCreate;
using Leopotam.EcsLite;
using UnityEngine;

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

		public void InitSession(LevelConfig levelConfig)
		{
			Debug.Log("Init Session");
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

			CleanUp();

			systems.Init();
		}

		private void CleanUp()
		{
		}

		public void Cleanup()
		{
			if (systems != null)
			{
				systems.Destroy();
				systems = null;
			}

			if (world != null)
			{
				world.Destroy();
				world = null;
				gameOverFilter = null;
			}
		}

		void CreateLevelFromTemplate(LevelConfig config) =>
			LevelRequestFactory.Create(world, config,true);
	}
}