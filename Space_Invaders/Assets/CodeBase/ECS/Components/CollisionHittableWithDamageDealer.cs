using Leopotam.EcsLite;

namespace CodeBase.ECS.Components
{
	public struct CollisionHittableWithDamageDealer
	{
		public EcsPackedEntity Hittable;
		public EcsPackedEntity DamageDealer;
	}
}