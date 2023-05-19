using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "BulletLootConfig", menuName = "Static Data/LootBullet")]
	public class BulletLootConfig : ScriptableObject
	{
		public BulletType Type;
		public ResourcePath Path;
		public float BodySize = 3;
		public int Count = 1;
	}
}