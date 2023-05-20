using Leopotam.EcsLite;

namespace CodeBase.ECS.Components
{
	public struct CollisionPlayerWithLoot
	{
		public EcsPackedEntity Player;
		public EcsPackedEntity Loot;
	}
}