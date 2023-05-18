using CodeBase.Configs;
using CodeBase.ECS.Systems.Factories;
using CodeBase.ECS.Systems.LevelCreate;
using CodeBase.Enums;
using ComponentsECS;
using Leopotam.EcsLite;
using Unity.Mathematics;

namespace CodeBase.ECS.Systems.LevelCreate
{
	public class LevelRequestFactory
	{
		public static int Create(EcsWorld world, LevelConfig config)
		{
			var entity = world.NewEntity();

			var reqeustsPool = world.GetPool<LevelCreateRequest>();

			ref var requestComponent = ref reqeustsPool.Add(entity);
			requestComponent.Config = config;

			return entity;
		}
	}

	public class AlienFactory
	{
		public static int Create(EcsWorld world,AlienCreateData config)
		{
			var entity = world.NewEntity();

			var positionPool = world.GetPool<SpawnEvent>();
			ref var position =ref  positionPool.Add(entity);
			position.Position = config.Position;
			position.Path = config.PrefabPath;
			
			var alienPool = world.GetPool<Alien>();
			ref var alien = ref alienPool.Add(entity);

			
			
			
			return entity;
		}

	}
}
namespace ComponentsECS
{
	public struct SpawnEvent
	{
		public float3 Position;
		public string Path;
	}
	public struct Alien
	{
		public AlienType AlienType;
	}
	public struct View
	{
		public IUnityObjectView Value;
	}
}