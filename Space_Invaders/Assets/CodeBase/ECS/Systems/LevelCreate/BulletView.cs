using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace CodeBase.ECS.Systems.LevelCreate
{
	public class BulletView : MonoBehaviour,IUnityObjectView
	{
		public void DestroyView() => Destroy(gameObject);

		public void UpdatePosition(float3 position)
		{
		}
		public class Factory : PlaceholderFactory<string, BulletView>
		{
		}
	}
}