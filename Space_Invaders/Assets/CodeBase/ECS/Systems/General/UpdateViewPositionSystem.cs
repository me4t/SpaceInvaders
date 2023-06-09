using CodeBase.ECS.Components;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Systems.General
{
	public class UpdateViewPositionSystem : IEcsInitSystem, IEcsRunSystem
	{
		EcsFilter filter;

		EcsPool<View> viewPools;
		EcsPool<Position> positionPool;

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			filter = world.Filter<View>().Inc<Position>().End();
			viewPools = world.GetPool<View>();
			positionPool = world.GetPool<Position>();
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in filter)
			{
				ref var view = ref viewPools.Get(entity);
				ref var position = ref positionPool.Get(entity);

				view.Value.UpdatePosition(position.Value);
			}
		}
	}
}