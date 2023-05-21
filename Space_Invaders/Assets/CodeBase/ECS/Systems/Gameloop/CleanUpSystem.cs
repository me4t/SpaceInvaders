using CodeBase.ECS.Components;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.Gameloop
{
	public class CleanUpSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;
		private EcsFilter filter;
		private EcsFilter filterNextRound;
		private EcsFilter collisionHittableFilter;
		private EcsFilter bulletCountFilter;


		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<RoundPassed>().End();
			filterNextRound = world.Filter<NextRoundEvent>().End();
			collisionHittableFilter = world.Filter<CollisionHittableWithDamageDealer>().End();
			bulletCountFilter = world.Filter<UpdateBulletCountEvent>().End();
		}


		public void Run(IEcsSystems systems)
		{
			CleanRoundCompleted();
			CleanNextRoundEvent();
			CleanCollisionHittableWithDamageDealer();
			CleanBulletCountEvent();
		}

		private void CleanRoundCompleted()
		{
			foreach (var entity in filter) world.DelEntity(entity);
		}

		private void CleanCollisionHittableWithDamageDealer()
		{
			foreach (var entity in collisionHittableFilter) world.DelEntity(entity);
		}

		private void CleanBulletCountEvent()
		{
			foreach (var entity in bulletCountFilter) world.DelEntity(entity);
		}

		private void CleanNextRoundEvent()
		{
			foreach (var entity in filterNextRound) world.DelEntity(entity);
		}
	}
}