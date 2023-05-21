using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Static Data/Level")]
	public class LevelConfig : ScriptableObject
	{
		public int Key;
		public PlayerSpawnPoint PlayerSpawnPoint;
		[FormerlySerializedAs("aliens")] public List<AlienSpawnPoint> Aliens;
	}
}