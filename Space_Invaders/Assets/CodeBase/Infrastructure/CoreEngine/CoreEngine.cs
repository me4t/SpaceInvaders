using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Infrastructure.GameStateMachine.States;
using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Infrastructure.CoreEngine
{
	public class CoreEngine : ICoreEngine
	{
		private EcsWorld world;
		private EcsSystems fixedsystems;
		private EcsSystems systems;
		private IEnumerable<IEcsSystem> bindedSystems;
		private IEnumerable<IFixedEcsSystem> _fixedEcsSystems;
		private EcsFilter gameOverFilter;

		public CoreEngine(IEnumerable<IEcsSystem> bindedSystems, IEnumerable<IFixedEcsSystem> fixedEcsSystems)
		{
			this.bindedSystems = bindedSystems;
			_fixedEcsSystems = fixedEcsSystems;
		}

		public void InitSession(LevelConfig levelConfig)
		{
			Debug.Log("Init Session");
			Initialize();
			CreateLevelFromTempleta(levelConfig);
			CreateGameOverFilter();
		}

		public void Tick()
		{
			systems?.Run();
		}

		public void FixedTick()
		{
			fixedsystems?.Run();
		}

		private void CreateGameOverFilter() => 
			gameOverFilter = world.Filter<NextRoundEvent>().End();
		public bool GameOver => 
			gameOverFilter.GetEntitiesCount() > 0;

		private void Initialize()
		{
			world = new EcsWorld();
			systems = new EcsSystems(world);

			fixedsystems = new EcsSystems(world);

			InitializeSystems();
		}

		private void InitializeSystems()
		{
			foreach (var system in bindedSystems)
				systems.Add(system);

			CleanUp();

			foreach (var system in _fixedEcsSystems)
				fixedsystems.Add(system);

			systems.Init();
			fixedsystems.Init();
		}

		private void CleanUp()
		{
		}

		public bool IsSessionEnd => gameOverFilter.GetEntitiesCount() > 0;


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

		void CreateLevelFromTempleta(LevelConfig config) =>
			LevelRequestFactory.Create(world, config,true);
	}
}