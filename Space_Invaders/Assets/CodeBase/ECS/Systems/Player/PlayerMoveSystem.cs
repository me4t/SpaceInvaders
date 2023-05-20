using CodeBase.ECS.Components;
using CodeBase.Extensions;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.Player
{
	public class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly IInputService inputService;
		private readonly ITimeService timeService;
		private readonly IStaticDataService staticDataService;
		private EcsWorld world;
		private EcsFilter filter;
		private EcsPool<SpawnEvent> spawnEventPool;
		private EcsPool<Speed> speedPool;
		private EcsPool<Position> positionPool;

		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();
			filter = world.Filter<Components.Player>().Inc<View>().End();
			speedPool = world.GetPool<Speed>();
			positionPool = world.GetPool<Position>();
		}

		public PlayerMoveSystem(IInputService inputService, ITimeService timeService)
		{
			this.inputService = inputService;
			this.timeService = timeService;
		}

		public void Run(IEcsSystems systems)
		{
			if (InputIsEmpty()) return;

			foreach (var entity in filter)
			{
				ref var speed = ref speedPool.Get(entity);
				ref var position = ref positionPool.Get(entity);

				var newPosition = position.Value;
				if (IsInBoundsZ(newPosition.z + inputService.Vertical))
					newPosition.z += inputService.Vertical;
				if (IsInBoundsX(newPosition: newPosition.x + inputService.Horizontal))
					newPosition.x += inputService.Horizontal;

				position.Value = MathHelpers.MoveTowards(position.Value, newPosition, speed.Value * timeService.DeltaTime);
			}
		}

		private static bool IsInBoundsX(float3 newPosition) => 
			newPosition.x is > Constants.MinBoardLimitX and < Constants.MaxBoardLimitX;

		private static bool IsInBoundsZ(float3 newPosition) => 
			newPosition.z is > Constants.MinBoardLimitZ and < Constants.MaxBoardLimitZ;

		private bool InputIsEmpty() =>
			inputService.Horizontal == 0 && inputService.Vertical == 0;
	}
}