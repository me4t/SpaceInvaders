using CodeBase.CompositionRoot;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Infrastructure.CoreEngine;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
	public class LoadLevelState : IPaylodedState<string>
	{
		private readonly IGameStateMachine gameStateMachine;
		private readonly IStaticDataService staticDataService;
		private readonly SceneLoader sceneLoader;
		private readonly ICoreEngine coreEngine;

		public LoadLevelState(IGameStateMachine gameStateMachine, 
			IStaticDataService staticDataService,LoadingCurtain curtain,SceneLoader sceneLoader,ICoreEngine coreEngine)
		{
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
			this.sceneLoader = sceneLoader;
			this.coreEngine = coreEngine;
		}

		public void Enter(string sceneName)
		{
			sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit()
		{
		}

		private void OnLoaded()
		{
			Debug.Log("Scene Loaded");
			var levelConfig = staticDataService.ForLevelTemplate(1);
            
			InitSession(levelConfig);
			PreWarmCore(); 
            
			gameStateMachine.Enter<GameLoopState>();
		}

		private void InitSession(LevelData levelConfig) => 
			coreEngine.InitSession(levelConfig);

		private void PreWarmCore() => 
			coreEngine.Tick();


		public class Factory : PlaceholderFactory<IGameStateMachine, LoadLevelState>
		{
		}
	}

	public interface IFixedEcsSystem:IEcsSystem
	{
        
	}
}