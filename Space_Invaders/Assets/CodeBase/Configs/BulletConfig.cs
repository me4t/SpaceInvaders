using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "BulletConfig", menuName = "Static Data/Bullet")]
	public class BulletConfig : ScriptableObject
	{
		public BulletType Type;
		public ResourcePath Path;
	}
}