using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.CompositionRoot
{
	public class CreateLevelSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsWorld world;

	
		public void Init(IEcsSystems systems)
		{
			world = systems.GetWorld();

			
		}

		public void Run(IEcsSystems systems)
		{
			Debug.Log("CreateLeveL System");
		}
	}
}