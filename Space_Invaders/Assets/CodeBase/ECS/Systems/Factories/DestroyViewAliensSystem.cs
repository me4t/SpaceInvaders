using CodeBase.ECS.Systems.LevelCreate;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Factories
{
	public class DestroyViewAliensSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Bullet> bulletPool;
		private EcsFilter filterRound;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Alien>().Inc<Dead>().End();
			filterRound = world.Filter<RoundPassed>().End();
		}
		

		public void Run(IEcsSystems systems)
		{
			foreach (var round in filterRound)
			foreach (var dead in filter)
				world.DelEntity(dead);

		}
	}
}