using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace CodeBase.MonoBehaviourView
{
	public class BulletView : MonoBehaviour,IUnityObjectView
	{
		public void DestroyView() => Destroy(gameObject);

		public void UpdatePosition(float3 position) => 
			transform.position = position;

		public class Factory : PlaceholderFactory<string, BulletView>
		{
		}
	}
}