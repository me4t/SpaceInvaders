using System.Collections.Generic;
using CodeBase.Enums;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "AlienConfig", menuName = "Static Data/Alien")]
	public class AlienConfig : ScriptableObject
	{
		public AlienType Type;
		public ResourcePath Path;
		public float Size;
		public float Health = 5;
		public float Speed = 3;
		public float3 GunDirection = new float3(0, 0, -1);
		public float3 MoveDirection = new float3(0, 0, -1);
		public float Score = 15;

		public List<BulletType> BulletLoot = new List<BulletType>()
		{
			BulletType.Fire,
			BulletType.Ice
		};

		public float LootDropChange = 100;
		
	}
}