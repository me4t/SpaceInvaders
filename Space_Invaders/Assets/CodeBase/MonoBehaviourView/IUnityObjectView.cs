using Unity.Mathematics;

namespace CodeBase.MonoBehaviourView
{
	public interface IUnityObjectView
	{
		void UpdatePosition(float3 position);
		void DestroyView();
	}
}