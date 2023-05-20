using CodeBase.ECS.Systems.Create;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Factories
{
	public class LevelRequestFactory
	{
		public static int Create(EcsWorld world, LevelData config, bool withPlayer = false)
		{
			var entity = world.NewEntity();

			var reqeustsPool = world.GetPool<RoundCreateRequest>();

			ref var requestComponent = ref reqeustsPool.Add(entity);
			requestComponent.Config = config;
			requestComponent.WithPlayer = withPlayer;			
			return entity;
		}
	}
}