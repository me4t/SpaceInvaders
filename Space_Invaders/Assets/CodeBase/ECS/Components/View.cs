using CodeBase.MonoBehaviourView;
using Leopotam.EcsLite;

namespace CodeBase.ECS.Components
{
	public struct View : IEcsAutoReset<View>
	{
		public IUnityObjectView Value;

		public void AutoReset(ref View c) =>
			c.Value?.DestroyView();
	}
}