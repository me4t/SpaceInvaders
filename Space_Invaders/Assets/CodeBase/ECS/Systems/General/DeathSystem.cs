using CodeBase.ECS.Components;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.General
{
	public class DeathSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsPool<ScoreLoot> scoreLootPool;
		private EcsPool<UpdateScore> updateScorePool;
		private EcsPool<Dead> deadPool;
		private EcsPool<DeathEvent> deathEventPool;
		private EcsPool<View> viewPool;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<DeathEvent>().End();
			scoreLootPool = world.GetPool<ScoreLoot>();
			updateScorePool = world.GetPool<UpdateScore>();
			deathEventPool = world.GetPool<DeathEvent>();
			viewPool = world.GetPool<View>();
			deadPool = world.GetPool<Dead>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				if (scoreLootPool.Has(entity))
				{
					ref var scoreLoot = ref scoreLootPool.Get(entity);
					var newEntity = world.NewEntity();
					ref var updateScore = ref updateScorePool.Add(newEntity);
					updateScore.Value = scoreLoot.Value;
				}

				viewPool.Del(entity);
				deathEventPool.Del(entity);
				deadPool.Add(entity);
			}
		}
	}
}