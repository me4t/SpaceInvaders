using System;
using System.Collections.Generic;
using CodeBase.Enums;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Configs
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Static Data/Level")]
	public class LevelConfig : ScriptableObject
	{
		 public string Key;
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