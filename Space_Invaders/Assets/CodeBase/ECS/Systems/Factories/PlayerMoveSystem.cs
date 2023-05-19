using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.Factories
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
			filter = world.Filter<Player>().Inc<View>().End();
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
				newPosition.x += inputService.Horizontal * speed.Value * timeService.DeltaTime;
				newPosition.z += inputService.Vertical * speed.Value * timeService.DeltaTime;

				position.Value = MathHelpers.MoveTowards(position.Value, newPosition, 0.1f);
			}
		}

		private bool InputIsEmpty() => 
			inputService.Horizontal == 0 && inputService.Vertical == 0;
	}

	public class MathHelpers
	{
		public static float3 MoveTowards(float3 current, float3 target, float maxDistanceDelta)
		{
			float deltaX = target.x - current.x;
			float deltaY = target.y - current.y;
			float deltaZ = target.z - current.z;
			float sqdist = deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ;
			if (sqdist == 0 || sqdist <= maxDistanceDelta * maxDistanceDelta)
				return target;
			var dist = math.sqrt(sqdist);
			return new float3(current.x + deltaX / dist * maxDistanceDelta,
				current.y + deltaY / dist * maxDistanceDelta,
				current.z + deltaZ / dist * maxDistanceDelta);
		}
		public static float SqrMagnitude(float3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}
	}
}