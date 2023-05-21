using CodeBase.ECS.Components;
using CodeBase.ECS.Systems.Create;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Gameloop
{
	public class CheckRoundCompleteSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<RoundCreateRequest> roundPool;
		private EcsFilter filterDead;
		private EcsPool<RoundPassed> roundCompletedPool;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<RoundCreateRequest>().Inc<Used>().End();
			filterDead = world.Filter<Components.Alien>().Inc<Dead>().End();
			roundPool = world.GetPool<RoundCreateRequest>();
			roundCompletedPool = world.GetPool<RoundPassed>();
		}


		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var round = ref roundPool.Get(entity);

				if (round.Config.aliens.Count != filterDead.GetEntitiesCount()) continue;

				roundCompletedPool.Add(world.NewEntity());
				world.DelEntity(entity);
			}
		}
	}
}