using CodeBase.Enums;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Static Data/Player")]
	public class PlayerConfig : ScriptableObject
	{
		public PlayerType Type; 
		public ResourcePath Path;
		public float Speed = 3;
		public float3 GunDirection = new float3(0,0,1);
		public float Size;
		public BulletType BulletType = BulletType.Fire;
		public int BaseBulletCount = 100;
		public int Health = 100;
	}
}