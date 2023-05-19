using System;
using System.Collections.Generic;
using CodeBase.Enums;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Static Data/Level")]
	public class LevelConfig : ScriptableObject
	{
		 public int Key;
		 public PlayerSpawnPoint PlayerSpawnPoint;
		 public List<AlienSpawnPoint> aliens;
	}

	[Serializable]
	public class AlienSpawnPoint
	{
		public float3 Position;
		public AlienType AlienType;
	}
	
	[Serializable]
	public class PlayerSpawnPoint
	{
		public float3 PlayerInitialPoint;
		 public PlayerType PlayerType;
	}
}